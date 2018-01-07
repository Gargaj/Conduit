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
          await Task.Run(() =>
          {
            try
            {
              if (entry.FullName.EndsWith("/"))
              {
                Directory.CreateDirectory(Path.Combine(targetDirectoryPath, entry.FullName));
              }
              else
              {
                entry.ExtractToFile(Path.Combine(targetDirectoryPath, entry.FullName));
              }
            }
            catch (IOException)
            {
            }
          });
        }
      }
      return true;
    }
  }
}
