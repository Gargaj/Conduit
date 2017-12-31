using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conduit
{
  public partial class SelectRunnableDialog : Form
  {
    List<Runners.Runnable> _runnables;
    public Runners.Runnable SelectedRunnable { get; private set; }
    public SelectRunnableDialog(List<Runners.Runnable> runnables)
    {
      InitializeComponent();

      _runnables = runnables;

      var y = 10;
      foreach (var runnable in _runnables)
      {
        var button = new Button();
        button.DialogResult = System.Windows.Forms.DialogResult.OK;
        button.Location = new System.Drawing.Point(10, y);
        button.Name = Path.GetFileName(runnable.Path);
        button.Size = new System.Drawing.Size(300, 25);
        button.Text = $"({runnable.Runner.Name}) {button.Name}";
        button.UseVisualStyleBackColor = true;
        button.Tag = runnable;
        button.Click += Button_Click;
        y += button.Size.Height;
        Size = new Size(Size.Width, y + 80);
        this.Controls.Add(button);
      }
    }

    private void Button_Click(object sender, EventArgs e)
    {
      SelectedRunnable = (Runners.Runnable)((Button)sender).Tag;
    }
  }
}
