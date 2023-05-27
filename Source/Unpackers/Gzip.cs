using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Conduit.Unpackers
{
  public class Gzip : IUnpacker
  {
    public event EventHandler<UnpackingProgressArgs> ProgressChanged;
    public string Error { get; private set; }

    public bool CanUnpack(string archiveFile)
    {
      using (var fs = File.OpenRead(archiveFile))
      {
        if (fs == null) return false;
        if (fs.ReadByte() != '\x1f') return false;
        if (fs.ReadByte() != '\x8b') return false;
        return true;
      }
    }

    public async Task<bool> Unpack(string archiveFile, string targetDirectoryPath)
    {
      var unpackedFilename = Path.GetFileName(new System.Text.RegularExpressions.Regex("\\.gz$").Replace(archiveFile, ""));
      try
      {
        using (var compressedStream = File.OpenRead(archiveFile))
        {
          using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
          {
            using (var resultStream = File.OpenWrite(Path.Combine(targetDirectoryPath, unpackedFilename)))
            {
              ProgressChanged?.Invoke(this, new UnpackingProgressArgs()
              {
                TotalFiles = 1,
                CurrentFile = 0,
                CurrentFilename = unpackedFilename,
              });

              zipStream.CopyTo(resultStream);
              return true;
            }
          }
        }
      }
      catch(Exception e)
      {
        Error = e.ToString();
        return false;
      }
    }
  }
}
