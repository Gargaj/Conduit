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

    private async void MainForm_Load(object sender, EventArgs e)
    {
      tabSites.TabPages.Clear();
      foreach (var site in Registry.Sites)
      {
        foreach (var siteList in site.ProdLists)
        {
          var page = new TabPage($"{site.Name}: {siteList}");
          tabSites.TabPages.Add(page);

          var list = new ListBox();
          list.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top;
          list.Width = page.Width;
          list.Height = page.Height;
          list.Visible = false;
          page.Controls.Add(list);

          IEnumerable<Sites.SiteProdInfo> prodList = null;
          try
          {
            prodList = await site.RetrieveProdList(siteList);
          }
          catch (Exception)
          {
            continue;
          }
          if (prodList != null)
          {
            list.Visible = true;
            foreach (var prod in prodList)
            {
              list.Items.Add(prod.Name);
            }
            list.Tag = new KeyValuePair<string, List<Sites.SiteProdInfo>>(site.ID, prodList.ToList());
          }
          list.MouseDoubleClick += ProdList_MouseDoubleClick;
        }
      }
    }

    private void ProdList_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      var listBox = sender as ListBox;
      int index = listBox.IndexFromPoint(e.Location);
      KeyValuePair<string, List<Sites.SiteProdInfo>> data = (KeyValuePair<string, List<Sites.SiteProdInfo>>)listBox.Tag;
      Sites.SiteProdInfo prodInfo = data.Value[index];

      var download = new DownloadDialog($"conduit://{data.Key}/prod/{prodInfo.ID}");
      DialogResult result = download.ShowDialog();
    }
  }
}
