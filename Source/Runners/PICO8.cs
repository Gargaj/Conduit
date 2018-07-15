using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conduit.Runners
{
  class PICO8 : IRunner
  {
    private List<string> _extensions = new List<string>()
    {
      "*.p8.png",
      "*.p8",
    };

    public string Name => "PICO-8";
    public uint Priority => 100;

    private List<string> GetMatches(string demoDir)
    {
      List<string> files = new List<string>();
      foreach (var ext in _extensions)
      {
        files.AddRange(Directory.GetFiles(demoDir, ext, SearchOption.AllDirectories));
      }
      return files;
    }
    public List<string> GetRunnableFiles(string demoDir)
    {
      List<string> result = null;
      if (File.GetAttributes(demoDir).HasFlag(FileAttributes.Directory))
      {
        result = GetMatches(demoDir);
      }
      else
      {
        var files = GetMatches(Path.GetDirectoryName(demoDir));
        result = files.Contains(demoDir) ? new List<string>() { demoDir } : new List<string>();
      }
      return result.Where(s => !WindowsExecutable.IsWindowsExecutable(s)).ToList();
    }

    public bool Run(string path)
    {
      string pico8Path = Settings.Options.PICO8Path;
      bool pico8Found = false;
      if (pico8Path != null && File.Exists(pico8Path))
      {
        pico8Found = true;
      }
      else
      {
        if (MessageBox.Show("PICO-8 executable not found; do you want to set a path now?", "Conduit", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          OptionsDialog dlg = new OptionsDialog();
          DialogResult result = dlg.ShowDialogWithOpenTab("tabPICO8");
          if (result == DialogResult.OK)
          {
            pico8Path = Settings.Options.PICO8Path;
            if (pico8Path != null && File.Exists(pico8Path))
            {
              pico8Found = true;
            }
          }
        }
      }

      if (pico8Found)
      {
        ProcessStartInfo startInfo = new ProcessStartInfo(pico8Path);
        List<string> arguments = new List<string>();
        arguments.Add($"-run");
        arguments.Add($"\"{path}\"");
        startInfo.Arguments = string.Join(" ",arguments);
        var process = Process.Start(startInfo);
        return process != null;
      }
      return false;
    }
  }
}
