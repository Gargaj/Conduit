using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conduit.Runners
{
  class Browser : IRunner
  {
    private List<string> _extensions = new List<string>()
    {
      "*.html",
      "*.htm",
    };

    public string Name => "Browser";
    public uint Priority => 75; // Not 100, html may be used as data or nfo stuff

    private List<string> GetMatches(string demoDir)
    {
      List<string> files = new List<string>();
      foreach (var ext in _extensions)
      {
        files.AddRange(Directory.GetFiles(demoDir, ext, SearchOption.AllDirectories));
      }
      return files;
    }
    public List<string> GetRunnableFiles(string demoDir)
    {
      if (File.GetAttributes(demoDir).HasFlag(FileAttributes.Directory))
      {
        return GetMatches(demoDir);
      }
      else
      {
        var files = GetMatches(Path.GetDirectoryName(demoDir));
        return files.Contains(demoDir) ? new List<string>() { demoDir } : new List<string>();
      }
    }

    public bool Run(string path)
    {
      string browserPath = Settings.Options.BrowserPath;
      bool browserFound = false;
      if (browserPath != null && File.Exists(browserPath))
      {
        browserFound = true;
      }
      else
      {
        if (MessageBox.Show("Browser executable not found; do you want to set a path now?", "Conduit", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          OptionsDialog dlg = new OptionsDialog();
          DialogResult result = dlg.ShowDialogWithOpenTab("tabBrowser");
          if (result == DialogResult.OK)
          {
            browserPath = Settings.Options.BrowserPath;
            if (browserPath != null && File.Exists(browserPath))
            {
              browserFound = true;
            }
          }
        }
      }

      if (browserFound)
      {
        ProcessStartInfo startInfo = new ProcessStartInfo(browserPath);
        var arguments = "";
        if (browserPath.Contains("Chrome") && Settings.Options.BrowserAddFileAccessFlag) arguments = "--allow-file-access-from-files ";
        var encodedPath = path;
        path = path.Replace("\\", "/");
        path = System.Web.HttpUtility.UrlPathEncode(path);
        arguments += $"\"file://{path}\"";
        startInfo.Arguments = arguments;
        startInfo.WorkingDirectory = Path.GetDirectoryName(path);
        var process = Process.Start(startInfo);
        return process != null;
      }
      return false;
    }
  }
}
