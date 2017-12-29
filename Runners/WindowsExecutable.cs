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

    private bool IsWindowsExecutable(string filePath)
    {
      try
      {
        // Read in the DLL or EXE and get the timestamp
        using (FileStream stream = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
        {
          BinaryReader reader = new BinaryReader(stream);
          if (reader.ReadByte() != 'M') return false;
          if (reader.ReadByte() != 'Z') return false;
          stream.Seek(0x3C, SeekOrigin.Begin);
          stream.Seek(reader.ReadUInt32(), SeekOrigin.Begin);
          if (reader.ReadByte() != 'P') return false;
          if (reader.ReadByte() != 'E') return false;
        }
      }
      catch (Exception e)
      {
        return false;
      }
      return true;
    }
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
      return result.Where(s => IsWindowsExecutable(s)).ToList();
    }

    public void Run(string path)
    {
      ProcessStartInfo startInfo = new ProcessStartInfo(Path.GetFileName(path));
      startInfo.WorkingDirectory = Path.GetDirectoryName(path);
      Process.Start(startInfo);
    }
  }
}
