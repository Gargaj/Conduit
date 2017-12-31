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
    void Run(string path);
  }
}
