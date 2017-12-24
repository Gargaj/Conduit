using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Runners
{
  public interface IRunner
  {
    List<string> GetRunnableFiles(string demoDir);
    void Run(string path);
  }
}
