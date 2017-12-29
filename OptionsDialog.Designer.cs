namespace Conduit
{
  partial class OptionsDialog
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsDialog));
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabGeneral = new System.Windows.Forms.TabPage();
      this.label3 = new System.Windows.Forms.Label();
      this.textDemoPath = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.tabVICE = new System.Windows.Forms.TabPage();
      this.butBrowseVICE = new System.Windows.Forms.Button();
      this.textVicePath = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.buttonOK = new System.Windows.Forms.Button();
      this.tabDOSBox = new System.Windows.Forms.TabPage();
      this.butBrowseDosbox = new System.Windows.Forms.Button();
      this.textDosboxPath = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.tabControl1.SuspendLayout();
      this.tabGeneral.SuspendLayout();
      this.tabVICE.SuspendLayout();
      this.tabDOSBox.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabControl1
      // 
      this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tabControl1.Controls.Add(this.tabGeneral);
      this.tabControl1.Controls.Add(this.tabVICE);
      this.tabControl1.Controls.Add(this.tabDOSBox);
      this.tabControl1.Location = new System.Drawing.Point(10, 8);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(585, 172);
      this.tabControl1.TabIndex = 0;
      // 
      // tabGeneral
      // 
      this.tabGeneral.Controls.Add(this.label3);
      this.tabGeneral.Controls.Add(this.textDemoPath);
      this.tabGeneral.Controls.Add(this.label1);
      this.tabGeneral.Location = new System.Drawing.Point(4, 22);
      this.tabGeneral.Name = "tabGeneral";
      this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
      this.tabGeneral.Size = new System.Drawing.Size(577, 146);
      this.tabGeneral.TabIndex = 0;
      this.tabGeneral.Text = "General";
      this.tabGeneral.UseVisualStyleBackColor = true;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(11, 51);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(417, 78);
      this.label3.TabIndex = 2;
      this.label3.Text = resources.GetString("label3.Text");
      // 
      // textDemoPath
      // 
      this.textDemoPath.Location = new System.Drawing.Point(14, 28);
      this.textDemoPath.Name = "textDemoPath";
      this.textDemoPath.Size = new System.Drawing.Size(552, 20);
      this.textDemoPath.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(11, 12);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(111, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Demo download path:";
      // 
      // tabVICE
      // 
      this.tabVICE.Controls.Add(this.butBrowseVICE);
      this.tabVICE.Controls.Add(this.textVicePath);
      this.tabVICE.Controls.Add(this.label2);
      this.tabVICE.Location = new System.Drawing.Point(4, 22);
      this.tabVICE.Name = "tabVICE";
      this.tabVICE.Padding = new System.Windows.Forms.Padding(3);
      this.tabVICE.Size = new System.Drawing.Size(577, 146);
      this.tabVICE.TabIndex = 1;
      this.tabVICE.Text = "VICE";
      this.tabVICE.UseVisualStyleBackColor = true;
      // 
      // butBrowseVICE
      // 
      this.butBrowseVICE.Location = new System.Drawing.Point(469, 28);
      this.butBrowseVICE.Name = "butBrowseVICE";
      this.butBrowseVICE.Size = new System.Drawing.Size(92, 20);
      this.butBrowseVICE.TabIndex = 4;
      this.butBrowseVICE.Text = "Browse...";
      this.butBrowseVICE.UseVisualStyleBackColor = true;
      this.butBrowseVICE.Click += new System.EventHandler(this.butBrowseVICE_Click);
      // 
      // textVicePath
      // 
      this.textVicePath.Location = new System.Drawing.Point(14, 28);
      this.textVicePath.Name = "textVicePath";
      this.textVicePath.Size = new System.Drawing.Size(449, 20);
      this.textVicePath.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(11, 12);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(113, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "VICE executable path:";
      // 
      // buttonCancel
      // 
      this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonCancel.Location = new System.Drawing.Point(511, 186);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(84, 27);
      this.buttonCancel.TabIndex = 1;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      // 
      // buttonOK
      // 
      this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.buttonOK.Location = new System.Drawing.Point(421, 186);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new System.Drawing.Size(84, 27);
      this.buttonOK.TabIndex = 2;
      this.buttonOK.Text = "OK";
      this.buttonOK.UseVisualStyleBackColor = true;
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      // 
      // tabDOSBox
      // 
      this.tabDOSBox.Controls.Add(this.butBrowseDosbox);
      this.tabDOSBox.Controls.Add(this.textDosboxPath);
      this.tabDOSBox.Controls.Add(this.label4);
      this.tabDOSBox.Location = new System.Drawing.Point(4, 22);
      this.tabDOSBox.Name = "tabDOSBox";
      this.tabDOSBox.Padding = new System.Windows.Forms.Padding(3);
      this.tabDOSBox.Size = new System.Drawing.Size(577, 146);
      this.tabDOSBox.TabIndex = 2;
      this.tabDOSBox.Text = "DOSBox";
      this.tabDOSBox.UseVisualStyleBackColor = true;
      // 
      // butBrowseDosbox
      // 
      this.butBrowseDosbox.Location = new System.Drawing.Point(469, 28);
      this.butBrowseDosbox.Name = "butBrowseDosbox";
      this.butBrowseDosbox.Size = new System.Drawing.Size(92, 20);
      this.butBrowseDosbox.TabIndex = 7;
      this.butBrowseDosbox.Text = "Browse...";
      this.butBrowseDosbox.UseVisualStyleBackColor = true;
      this.butBrowseDosbox.Click += new System.EventHandler(this.butBrowseDosbox_Click);
      // 
      // textDosboxPath
      // 
      this.textDosboxPath.Location = new System.Drawing.Point(14, 28);
      this.textDosboxPath.Name = "textDosboxPath";
      this.textDosboxPath.Size = new System.Drawing.Size(449, 20);
      this.textDosboxPath.TabIndex = 6;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(11, 12);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(130, 13);
      this.label4.TabIndex = 5;
      this.label4.Text = "DOSBox executable path:";
      // 
      // OptionsDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(607, 225);
      this.Controls.Add(this.buttonOK);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.tabControl1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "OptionsDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Conduit options and settings";
      this.Load += new System.EventHandler(this.OptionsDialog_Load);
      this.tabControl1.ResumeLayout(false);
      this.tabGeneral.ResumeLayout(false);
      this.tabGeneral.PerformLayout();
      this.tabVICE.ResumeLayout(false);
      this.tabVICE.PerformLayout();
      this.tabDOSBox.ResumeLayout(false);
      this.tabDOSBox.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabGeneral;
    private System.Windows.Forms.TabPage tabVICE;
    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.TextBox textDemoPath;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textVicePath;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button butBrowseVICE;
    private System.Windows.Forms.TabPage tabDOSBox;
    private System.Windows.Forms.Button butBrowseDosbox;
    private System.Windows.Forms.TextBox textDosboxPath;
    private System.Windows.Forms.Label label4;
  }
}