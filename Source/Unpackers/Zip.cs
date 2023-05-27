using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Windows.Forms;

namespace Conduit.Unpackers
{
  public class Zip : IUnpacker
  {
    public event EventHandler<UnpackingProgressArgs> ProgressChanged;
    public string Error { get; private set;  }

    public bool CanUnpack(string archiveFile)
    {
      using (FileStream fs = File.OpenRead(archiveFile))
      {
        if (fs == null) return false;
        if (fs.ReadByte() != 'P') return false;
        if (fs.ReadByte() != 'K') return false;
        return true;
      }
    }

    public async Task<bool> Unpack(string archiveFile, string targetDirectoryPath)
    {
      using (ZipArchive archive = ZipFile.OpenRead(archiveFile))
      {
        var i = 0;
        foreach (ZipArchiveEntry entry in archive.Entries)
        {
          ProgressChanged?.Invoke(this, new UnpackingProgressArgs()
          {
            TotalFiles = archive.Entries.Count,
            CurrentFile = i++,
            CurrentFilename = entry.FullName,
          });
          bool result = await Task.Run(() =>
          {
            try
            {
              if (!entry.FullName.EndsWith("/") )
              {
                var path = Path.Combine(targetDirectoryPath, entry.FullName);
                if(File.Exists(path))
                {
                  return true;
                }
                var dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                  Directory.CreateDirectory(dir);
                }
                entry.ExtractToFile(path);
              }
              return true;
            }
            catch (IOException e)
            {
              Error = e.ToString();
              return false;
            }
          });
          if (!result)
          {
            return false;
          }
        }
      }
      return true;
    }
  }
}
