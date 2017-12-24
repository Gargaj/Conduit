using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Runners
{
  class VICE : IRunner
  {
    private List<string> _extensions = new List<string>()
    {
      "*.t64",
      "*.d64",
      "*.prg",
    };

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

    public void Run(string path)
    {
      string vicePath = Settings.Options.VicePath;
      if (vicePath == null || !File.Exists(vicePath))
      {
        return;
      }

      ProcessStartInfo startInfo = new ProcessStartInfo(vicePath);
      startInfo.Arguments = Path.GetFileName(path);
      startInfo.WorkingDirectory = Path.GetDirectoryName(path);
      Process.Start(startInfo);
    }
  }
}
