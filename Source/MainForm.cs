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
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();
    }

    private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      OptionsDialog dlg = new OptionsDialog();
      DialogResult result = dlg.ShowDialog();
    }
  }
}
