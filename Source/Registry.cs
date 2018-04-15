using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conduit.Runners;
using Conduit.Sites;
using Conduit.Unpackers;

namespace Conduit
{
  public static class Registry
  {
    public static List<ISite> Sites { get; private set; }
    public static List<IUnpacker> Unpackers { get; private set; }
    public static List<IRunner> Runners { get; private set; }
    public static void Initialize()
    {
      Sites = new List<ISite>();
      Sites.Add(new Pouet());
      Sites.Add(new Demozoo());

      Unpackers = new List<IUnpacker>();
      Unpackers.Add(new Zip());

      Runners = new List<IRunner>();
      Runners.Add(new WindowsExecutable());
      Runners.Add(new VICE());
      Runners.Add(new DOSBox());
      Runners.Add(new Media());
    }
  }
}
