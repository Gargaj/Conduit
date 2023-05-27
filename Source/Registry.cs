using Conduit.Runners;
using Conduit.Sites;
using Conduit.Unpackers;
using System.Collections.Generic;

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
      Sites.Add(new CSDb());

      Unpackers = new List<IUnpacker>();
      Unpackers.Add(new SevenZip());
      Unpackers.Add(new Zip());
      Unpackers.Add(new Gzip());

      Runners = new List<IRunner>();
      Runners.Add(new WindowsExecutable());
      Runners.Add(new VICE());
      Runners.Add(new DOSBox());
      Runners.Add(new Browser());
      Runners.Add(new Media());
      Runners.Add(new PICO8());
    }
  }
}
