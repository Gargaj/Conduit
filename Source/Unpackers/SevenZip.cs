using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Conduit.Unpackers
{
  public class SevenZip : IUnpacker
  {
    public event EventHandler<UnpackingProgressArgs> ProgressChanged;
    public string Error { get; private set; }

    public bool CanUnpack(string archiveFile)
    {
      // Assume EXE is not an archive
      if (Path.GetExtension(archiveFile) == ".exe")
      {
        return false;
      }

      try
      {
        new SevenZipExtractor.ArchiveFile(archiveFile);
        return true;
      }
      catch
      {
        return false;
      }
    }

    public async Task<bool> Unpack(string archiveFilepath, string targetDirectoryPath)
    {
      using (var archiveFile = new SevenZipExtractor.ArchiveFile(archiveFilepath))
      {
        var i = 0;
        foreach (var entry in archiveFile.Entries)
        {
          ProgressChanged?.Invoke(this, new UnpackingProgressArgs()
          {
            TotalFiles = archiveFile.Entries.Count,
            CurrentFile = i++,
            CurrentFilename = entry.FileName,
          });

          try
          {
            if (entry.FileName.EndsWith("/"))
            {
              continue;
            }
            var path = Path.Combine(targetDirectoryPath, entry.FileName);
            if (File.Exists(path))
            {
              continue;
            }
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
              Directory.CreateDirectory(dir);
            }
            entry.Extract(path); // COM-object call, has to be on main thread
            await Task.Delay(TimeSpan.FromMilliseconds(10)); // Let UI thread breathe a bit
          }
          catch (IOException e)
          {
            Error = e.ToString();
            return false;
          }
        }
      }
      return true;
    }
  }
}
