using System;
using System.Windows.Forms;

namespace Conduit
{
  static class Program
  {
    private static void RegisterProtocolWindows(string exePath)
    {
      const string keyName = "Conduit";
      var root = Microsoft.Win32.Registry.ClassesRoot;
      var subkey = root.OpenSubKey(keyName);
      if (subkey != null)
      {
        root.DeleteSubKeyTree(keyName);
      }
      subkey = root.CreateSubKey(keyName);
      subkey.SetValue("URL Protocol", "");

      var icon = subkey.CreateSubKey("DefaultIcon");
      icon.SetValue(null, "\""+ exePath+"\",0");

      subkey.CreateSubKey("shell").CreateSubKey("open").CreateSubKey("command").SetValue(null, "\"" + exePath + "\" -openURL %1");
    }

    private static void RegisterProtocol()
    {
      if (Environment.OSVersion.Platform.HasFlag(PlatformID.Win32NT))
      {
        RegisterProtocolWindows(System.Reflection.Assembly.GetEntryAssembly().Location);
      }
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
      Registry.Initialize();
      Settings.LoadSettings();

      bool registerOnly = false;
      string openURL = null;
      for (var i = 0; i < args.Length; i++)
      {
        if (args[i] == "-openURL")
        {
          openURL = args[++i];
        }
        else if (args[i] == "-registerScheme")
        {
          registerOnly = true;
        }
      }

      if (registerOnly)
      {
        RegisterProtocol();
        return;
      }

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      if (openURL != null)
      {
        Application.Run(new DownloadDialog(openURL));
      }
      else
      {
        RegisterProtocol();
        Application.Run(new MainForm());
      }
    }
  }
}
