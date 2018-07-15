using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Runners
{
  public class Runnable
  {
    public IRunner Runner { get; set; }
    public string Path { get; set; }
  }
  public interface IRunner
  {
    List<string> GetRunnableFiles(string demoDir);

    string Name { get; }

    // This needs a bit of explanation:
    // Certain runnable files (like JPG) are valid, but they may also just "happen to be there"
    // with other demos. Priority levels define whether a certain file is more important than
    // others, i.e. if there is an EXE in the archive, the JPGs (that have a lower priority)
    // are not shown.
    uint Priority { get; }

    bool Run(string path);
  }
}
