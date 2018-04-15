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
  class DOSBox : IRunner
  {
    private List<string> _extensions = new List<string>()
    {
      "*.exe",
      "*.com",
    };

    public string Name => "DOSBox";
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

    public void Run(string path)
    {
      string dosboxPath = Settings.Options.DOSBoxPath;
      bool dosboxFound = false;
      if (dosboxPath != null && File.Exists(dosboxPath))
      {
        dosboxFound = true;
      }
      else
      {
        if (MessageBox.Show("DOSBox executable not found; do you want to set a path now?", "Conduit", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          OptionsDialog dlg = new OptionsDialog();
          DialogResult result = dlg.ShowDialogWithOpenTab("tabDOSBox");
          if (result == DialogResult.OK)
          {
            dosboxPath = Settings.Options.DOSBoxPath;
            if (dosboxPath != null && File.Exists(dosboxPath))
            {
              dosboxFound = true;
            }
          }
        }
      }

      if (dosboxFound)
      {
        ProcessStartInfo startInfo = new ProcessStartInfo(dosboxPath);
        List<string> arguments = new List<string>();
        arguments.Add($"\"{path}\"");
        arguments.Add($"-noautoexec");
        arguments.Add($"-exit");
        startInfo.Arguments = string.Join(" ",arguments);
        Process.Start(startInfo);
      }
    }
  }
}
