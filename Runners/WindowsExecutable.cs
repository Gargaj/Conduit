using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Runners
{
  public class WindowsExecutable : IRunner
  {
    private List<string> _extensions = new List<string>()
    {
      "*.exe",
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
      ProcessStartInfo startInfo = new ProcessStartInfo(Path.GetFileName(path));
      startInfo.WorkingDirectory = Path.GetDirectoryName(path);
      Process.Start(startInfo);
    }
  }
}
