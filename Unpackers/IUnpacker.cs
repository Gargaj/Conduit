using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Unpackers
{
  public class UnpackingProgressArgs : EventArgs
  {
    public int TotalFiles { get; set; }
    public int CurrentFile { get; set; }
    public string CurrentFilename { get; set; }
  }
  public interface IUnpacker
  {
    event EventHandler<UnpackingProgressArgs> ProgressChanged;
    bool CanUnpack(string archiveFile);
    Task<bool> Unpack(string archiveFile, string targetDirectoryPath);
  }
}
