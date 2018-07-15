using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Runners
{
  public class Media : IRunner
  {
    private List<string> _extensions = new List<string>()
    {
      "*.avi",
      "*.mpg",
      "*.mp4",
      "*.mov",

      "*.jpg",
      "*.jpeg",
      "*.gif",
      "*.png",

      "*.mp3",
      "*.ogg",
      "*.flac",
    };

    public string Name => "Media";
    public uint Priority => 10;

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
      List<string> result = null;
      if (File.GetAttributes(demoDir).HasFlag(FileAttributes.Directory))
      {
        result = GetMatches(demoDir);
      }
      else
      {
        var files = GetMatches(Path.GetDirectoryName(demoDir));
        result = files.Contains(demoDir) ? new List<string>() { demoDir } : new List<string>();
      }
      return result;
    }

    public bool Run(string path)
    {
      ProcessStartInfo startInfo = new ProcessStartInfo(Path.GetFileName(path));
      startInfo.WorkingDirectory = Path.GetDirectoryName(path);
      var process = Process.Start(startInfo);
      return process != null;
    }
  }
}
