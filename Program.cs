using System;
using System.Diagnostics;
using System.IO;
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
    private static void RegisterProtocolXDG(string exePath)
    {
      const string name = "Conduit";

      string desktopFile =
"[Desktop Entry]\n" +
"Type=Application\n" +
"Version=1.0\n" +
"Name=" + name + "\n" +
"Exec=mono " + exePath + " -openURL\n" +
"Terminal=false\n";

      string dir = Environment.GetEnvironmentVariable("XDG_DATA_HOME");
      if (String.IsNullOrEmpty(dir))
      {
        dir = Environment.GetEnvironmentVariable("HOME");
        if (String.IsNullOrEmpty(dir))
        {
          dir = "/home/" + Environment.GetEnvironmentVariable("USER"); // good enough
        }
        dir += "/.local/share";
      }

      dir += "/applications";

      if (!Directory.Exists(dir))
      {
        Directory.CreateDirectory(dir);
      }

      string file = dir + "/conduit.desktop";

      File.WriteAllText(file, desktopFile);

      // $ xdg-settings set default-url-scheme-handler conduit conduit.desktop
      var psi = new ProcessStartInfo("xdg-settings", "set default-url-scheme-handler conduit conduit.desktop");
      Process.Start(psi).WaitForExit();
    }

    private static void RegisterProtocol()
    {
      if (Environment.OSVersion.Platform.HasFlag(PlatformID.Win32NT))
      {
        RegisterProtocolWindows(System.Reflection.Assembly.GetEntryAssembly().Location);
      }
      else if (Environment.OSVersion.Platform.HasFlag(PlatformID.Unix))
      {
        RegisterProtocolXDG(System.Reflection.Assembly.GetEntryAssembly().Location);
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
