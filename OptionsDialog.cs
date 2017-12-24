using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conduit
{
  public partial class OptionsDialog : Form
  {
    public OptionsDialog()
    {
      InitializeComponent();
    }

    private void OptionsDialog_Load(object sender, EventArgs e)
    {
      textDemoPath.Text = Settings.Options.DemoPath;
      textVicePath.Text = Settings.Options.VicePath;
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      Settings.Options.DemoPath = textDemoPath.Text;
      Settings.Options.VicePath = textVicePath.Text;
      Settings.SaveSettings();
      DialogResult = DialogResult.OK;
    }
  }
}
