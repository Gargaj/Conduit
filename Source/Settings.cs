using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Conduit
{
  public class Settings
  {
    public class OptionsObject
    {
      public string DemoPath { get; set; }
      public string VicePath { get; set; }
      public string DOSBoxPath { get; set; }
      public string BrowserPath { get; set; }
      public bool BrowserAddFileAccessFlag { get; set; }
    }
    public static OptionsObject Options { get; set; }
    private static string _optionsFilename = "options.json";

    private static string OptionsFilename
    {
      get
      {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Conduit", _optionsFilename);
      }
    }

    public static void LoadSettings()
    {
      Options = new OptionsObject();
      Options.DemoPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Demos", "[GROUP]");

      try
      {
        var str = File.ReadAllText(OptionsFilename);
        Options = JsonConvert.DeserializeObject<OptionsObject>(str);
      }
      catch (IOException)
      {
      }
    }
    public static void SaveSettings()
    {
      var str = JsonConvert.SerializeObject(Options, Formatting.Indented);
      if (!Directory.Exists(Path.GetDirectoryName(OptionsFilename)))
      {
        Directory.CreateDirectory(Path.GetDirectoryName(OptionsFilename));
      }
      File.WriteAllText(OptionsFilename, str);
    }
  }
}
