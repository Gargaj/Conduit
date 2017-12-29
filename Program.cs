using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Conduit
{
  static class Program
  {
    private static void RegisterProtocolWindows(string exePath)
    {
      const string keyName = "Conduit";
      var root = Microsoft.Win32.Registry.ClassesRoot;
      var subkey = root.OpenSubKey(keyName);
      if (subkey != null)
      {
        root.DeleteSubKeyTree(keyName);
      }
      subkey = root.CreateSubKey(keyName);
      subkey.SetValue("URL Protocol", "");

      var icon = subkey.CreateSubKey("DefaultIcon");
      icon.SetValue(null, "\""+ exePath+"\",0");

      subkey.CreateSubKey("shell").CreateSubKey("open").CreateSubKey("command").SetValue(null, "\"" + exePath + "\" -openURL %1");
    }
    private static void RegisterProtocolXDG(string exePath)
    {
      const string name = "Conduit";

      string desktopFile =
"[Desktop Entry]\n" +
"Type=Application\n" +
"Version=1.0\n" +
"Name=" + name + "\n" +
"Exec=mono " + exePath + " -openURL\n" +
"Terminal=false\n";

      string dir = Environment.GetEnvironmentVariable("XDG_DATA_HOME");
      if (String.IsNullOrEmpty(dir))
      {
        dir = Environment.GetEnvironmentVariable("HOME");
        if (String.IsNullOrEmpty(dir))
        {
          dir = "/home/" + Environment.GetEnvironmentVariable("USER"); // good enough
        }
        dir += "/.local/share";
      }

      dir += "/applications";

      if (!Directory.Exists(dir))
      {
        Directory.CreateDirectory(dir);
      }

      string file = dir + "/conduit.desktop";

      File.WriteAllText(file, desktopFile);

      // $ xdg-settings set default-url-scheme-handler conduit conduit.desktop
      var psi = new ProcessStartInfo("xdg-settings", "set default-url-scheme-handler conduit conduit.desktop");
      Process.Start(psi).WaitForExit();
    }

    private static void RegisterProtocol()
    {
      if (Environment.OSVersion.Platform.HasFlag(PlatformID.Win32NT))
      {
        RegisterProtocolWindows(System.Reflection.Assembly.GetEntryAssembly().Location);
      }
      else if (Environment.OSVersion.Platform.HasFlag(PlatformID.Unix))
      {
        RegisterProtocolXDG(System.Reflection.Assembly.GetEntryAssembly().Location);
      }
    }

    private class Asset
    {
      public string browser_download_url { get; set; }
    }
    private class Release
    {
      public string url { get; set; }
      public string tag_name { get; set; }
      public string published_at { get; set; }
      public Asset[] assets { get; set; }
    }
    private async static Task CheckForUpdates()
    {
      string url = "https://api.github.com/repos/Gargaj/Conduit/releases";
      using (var wc = new System.Net.WebClient())
      {
        wc.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
        string contents = "";
        try
        {
          contents = await wc.DownloadStringTaskAsync(url);
        }
        catch (Exception)
        {
        }

        var response = JsonConvert.DeserializeObject<Release[]>(contents);
        if (response != null)
        {
          var release = response?.OrderBy(s => s.published_at)?.FirstOrDefault();
          if (release != null)
          {
            string tag_name = release.tag_name;
            Version ourVersion = Assembly.GetEntryAssembly().GetName().Version;
            Version latestVersion = new Version(tag_name.Substring(0,1) == "v" ? tag_name.Substring(1) : tag_name);
            if (latestVersion.CompareTo(ourVersion) > 0)
            {
              if (MessageBox.Show($"A new version of Conduit is available: {tag_name}\n\nDo you want to download it?", "Conduit version check", MessageBoxButtons.YesNo) == DialogResult.Yes)
              {
                Process.Start(release.assets.Count() > 0 ? release.assets[0].browser_download_url : release.url);
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
      Registry.Initialize();
      Settings.LoadSettings();

      bool registerOnly = false;
      string openURL = null;
      for (var i = 0; i < args.Length; i++)
      {
        if (args[i] == "-openURL")
        {
          openURL = args[++i];
        }
        else if (args[i] == "-registerScheme")
        {
          registerOnly = true;
        }
      }

      if (registerOnly)
      {
        RegisterProtocol();
        return;
      }

      #if !DEBUG
      var task = Task.Run(async () => { await CheckForUpdates(); });
      #endif

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      if (openURL != null)
      {
        Application.Run(new DownloadDialog(openURL));
      }
      else
      {
        RegisterProtocol();
        Application.Run(new MainForm());
      }

      #if !DEBUG 
      if (!task.IsCompleted) task.Wait();
      #endif
    }
  }
}
