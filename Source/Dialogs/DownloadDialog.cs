using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Forms;
using Conduit.Sites;
using Conduit.Unpackers;

namespace Conduit
{
  public partial class DownloadDialog : Form
  {
    private Uri _openURL;

    public DownloadDialog(string openURL)
    {
      _openURL = new Uri(openURL);
      InitializeComponent();
    }

    private async Task<SiteProdInfo> GetDownloadURL()
    {
      var site = Registry.Sites.FirstOrDefault(s => s.ID == _openURL.Host);
      if (site == null)
      {
        return null;
      }

      return await site.RetrieveProdInfo(_openURL);
    }

    private void TransformURL(ref string url)
    {
      if (url.StartsWith("https://files.scene.org/view/"))
      {
        url = url.Replace("https://files.scene.org/view/", "https://files.scene.org/get:hu-http/");
      }
    }

    public string GetFormattedFileSize(int size)
    {
      double len = size;
      int order = 0;
      string[] sizes = { "B", "KB", "MB", "GB", "TB" };
      while (len >= 1024 && order < sizes.Length - 1)
      {
        order++;
        len = len / 1024;
      }
      return String.Format("{0:0.##} {1}", len, sizes[order]);
    }

    private async Task<string> DownloadFile(string url, string targetPath)
    {
      var filename = Path.GetFileName(new Uri(url).LocalPath);
      var localFileName = Path.Combine(targetPath, filename);

      if (File.Exists(localFileName))
      {
        downloadText.Text = "Already downloaded.";
        return localFileName;
      }

      downloadText.Text = $"Starting download from {url}...";
      downloadProgress.Maximum = 0;

      string finalURL = url;
      WebResponse response = null;
      do
      {
        // File size problem due to C# runtime bug: https://stackoverflow.com/a/34846577
        var request = WebRequest.Create(finalURL);
        if (request is HttpWebRequest)
        {
          (request as HttpWebRequest).AllowAutoRedirect = false;
        }
        response = await request.GetResponseAsync();
        if (request is HttpWebRequest)
        {
          if (response.Headers["Location"] != null)
          {
            finalURL = response.Headers["Location"];
            // Handle if redirect has changed the filename
            filename = Path.GetFileName(new Uri(finalURL).LocalPath);
          }
          if (response.Headers["Content-Disposition"] != null)
          {
            var regex = new System.Text.RegularExpressions.Regex("filename=['\"]?([^'\"]*)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            var match = regex.Match(response.Headers["Content-Disposition"]);
            if (match != null && match.Groups.Count == 2)
            {
              filename = match.Groups[1].Value;
            }
          }
          if (response.ContentLength > 0)
          {
            downloadProgress.Maximum = (int)response.ContentLength;
          }
        }
      } while (response.Headers["Location"] != null);

      if (string.IsNullOrWhiteSpace(filename))
      {
        filename = "download.zip";
        MessageBox.Show($"Unable to deduce filename from {finalURL}; will rename it to {filename} and see where it goes...", "Conduit error: Download dodgy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }

      localFileName = Path.Combine(targetPath, filename);

      var stream = response.GetResponseStream();

      int bufferSize = 1024 * 1024;
      var bytes = new byte[bufferSize];
      var bytesRead = 0;
      var tmpFile = localFileName + ".$$$";
      if (File.Exists(tmpFile))
      {
        File.Delete(tmpFile);
      }
      using (FileStream fileStream = File.Open(tmpFile, FileMode.CreateNew))
      {
        var totalBytes = 0;
        do
        {
          bytesRead = await stream.ReadAsync(bytes, 0, bufferSize);
          fileStream.Write(bytes, 0, bytesRead);
          downloadProgress.Increment(bytesRead);
          if (downloadProgress.Maximum == 0)
          {
            downloadText.Text = $"Downloading [{filename}] ({GetFormattedFileSize(totalBytes)})...";
          }
          else
          {
            downloadText.Text = $"Downloading [{filename}] ({GetFormattedFileSize(totalBytes)} / {GetFormattedFileSize(downloadProgress.Maximum)})...";
          }
          totalBytes += bytesRead;
        } while (bytesRead > 0);
      }
      downloadText.Text = "Download finished!";

      if (File.Exists(localFileName))
      {
        File.Delete(localFileName);
      }
      File.Move(tmpFile, localFileName);

      return localFileName;
    }

    public static string Sanitize(string s)
    {
      return System.Text.RegularExpressions.Regex.Replace(s == null ? "[unknown]" : s, @"[^a-zA-Z0-9\-_\.]+", "-").ToLower();
    }

    private IUnpacker FindUnpacker(string archiveFile)
    {
      foreach (var unpacker in Registry.Unpackers)
      {
        if (unpacker.CanUnpack(archiveFile))
        {
          return unpacker;
        }
      }
      return null;
    }

    private async Task<bool> UnpackFile(IUnpacker unpacker, string archiveFile, string targetDir)
    {
      unpacker.ProgressChanged += (object sender, UnpackingProgressArgs e) =>
      {
        Dispatcher.CurrentDispatcher.Invoke(() =>
        {
          unpackProgress.Maximum = e.TotalFiles;
          unpackProgress.Value = e.CurrentFile;
          unpackText.Text = $"Unpacking {e.CurrentFilename}";
        });
      };
      Directory.CreateDirectory(targetDir);
      return await unpacker.Unpack(archiveFile, targetDir);
    }

    private async Task WatchDemo()
    {
      var site = Registry.Sites.FirstOrDefault(s => s.ID == _openURL.Host);
      if (site == null)
      {
        return;
      }

      var prodInfo = await GetDownloadURL();
      if (prodInfo == null)
      {
        return;
      }

      this.Text = "Conduit - downloading demo: " + prodInfo.Name;

      var url = prodInfo.DownloadLink;
      TransformURL(ref url);

      var path = Settings.Options.DemoPath;
      path = path.Replace("[GROUP]", Sanitize(prodInfo.Group));
      path = path.Replace("[YEAR]", prodInfo.ReleaseDate.Year.ToString());
      Directory.CreateDirectory(path);

      var localFileName = await DownloadFile(url, path);
      if (localFileName == null)
      {
        return;
      }

      var extractPath = Path.Combine(path, Path.GetFileNameWithoutExtension(localFileName));
      var unpacker = FindUnpacker(localFileName);
      var extractSuccessful = false;
      if (unpacker == null)
      {
        // We couldn't find an unpacker to the file; maybe we can just run it?
        extractPath = localFileName;
      }
      else
      {
        extractSuccessful = await UnpackFile(unpacker, localFileName, extractPath);
      }
      List<Runners.Runnable> runnables = new List<Runners.Runnable>();
      var runners = Registry.Runners.OrderByDescending(s => s.Priority);
      var lastPriority = runners.First().Priority;
      foreach (var runner in runners)
      {
        if (lastPriority != runner.Priority && runnables.Count > 0)
        {
          // If we've reached a different priority level, but we already have something to run, don't bother.
          break;
        }
        runnables.AddRange(runner.GetRunnableFiles(extractPath).Select(s => new Runners.Runnable() { Path = s, Runner = runner }));
      }
      if (runnables.Count == 0)
      {
        // Nothing found, error
        if (unpacker != null && !extractSuccessful)
        {
          // We found an unpacker but it failed
          MessageBox.Show($"There's been an error unpacking the following file:\n{localFileName}", "Conduit error: Unpack failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else if (unpacker != null && extractSuccessful)
        {
          // We found and successfully used an unpacker, but couldn't find a runner
          MessageBox.Show($"We couldn't find a way to run the following file:\n{localFileName}", "Conduit error: Run failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else if (unpacker == null)
        {
          // We couldn't find an unpacker
          MessageBox.Show($"We couldn't find a way to unpack the following file:\n{localFileName}", "Conduit error: Unpack failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      else if (runnables.Count == 1)
      {
        var result = runnables[0].Runner.Run(runnables[0].Path);
        if (!result)
        {
          MessageBox.Show($"There was an error starting this file:\n{runnables[0].Path}", "Conduit error: Run failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      else if (runnables.Count > 1)
      {
        SelectRunnableDialog dlg = new SelectRunnableDialog(runnables);
        DialogResult dlgResult = dlg.ShowDialog();
        if (dlgResult == DialogResult.OK)
        {
          if (dlg.SelectedRunnable != null)
          {
            var result = dlg.SelectedRunnable.Runner.Run(dlg.SelectedRunnable.Path);
            if (!result)
            {
              MessageBox.Show($"There was an error starting this file:\n{dlg.SelectedRunnable.Path}", "Conduit error: Run failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          }
        }
      }
    }

    private async void DownloadDialog_Load(object sender, EventArgs args)
    {
      try
      {
        await WatchDemo();
      }
      catch (Exception)
      {
        throw;
      }
      Close();
    }
  }
}
