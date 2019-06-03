namespace Conduit
{
  partial class DownloadDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadDialog));
      this.downloadProgress = new System.Windows.Forms.ProgressBar();
      this.downloadText = new System.Windows.Forms.Label();
      this.unpackText = new System.Windows.Forms.Label();
      this.unpackProgress = new System.Windows.Forms.ProgressBar();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // downloadProgress
      // 
      this.downloadProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.downloadProgress.Location = new System.Drawing.Point(112, 31);
      this.downloadProgress.Name = "downloadProgress";
      this.downloadProgress.Size = new System.Drawing.Size(460, 20);
      this.downloadProgress.TabIndex = 0;
      // 
      // downloadText
      // 
      this.downloadText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.downloadText.AutoEllipsis = true;
      this.downloadText.Location = new System.Drawing.Point(112, 12);
      this.downloadText.MaximumSize = new System.Drawing.Size(523, 13);
      this.downloadText.Name = "downloadText";
      this.downloadText.Size = new System.Drawing.Size(460, 13);
      this.downloadText.TabIndex = 1;
      this.downloadText.Text = "Downloading...";
      this.downloadText.UseMnemonic = false;
      // 
      // unpackText
      // 
      this.unpackText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.unpackText.Location = new System.Drawing.Point(112, 61);
      this.unpackText.MaximumSize = new System.Drawing.Size(523, 13);
      this.unpackText.Name = "unpackText";
      this.unpackText.Size = new System.Drawing.Size(460, 13);
      this.unpackText.TabIndex = 3;
      this.unpackText.Text = "Unpacking...";
      this.unpackText.UseMnemonic = false;
      // 
      // unpackProgress
      // 
      this.unpackProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.unpackProgress.Location = new System.Drawing.Point(112, 80);
      this.unpackProgress.Name = "unpackProgress";
      this.unpackProgress.Size = new System.Drawing.Size(460, 20);
      this.unpackProgress.TabIndex = 2;
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = global::Conduit.Properties.Resources._96;
      this.pictureBox1.Location = new System.Drawing.Point(12, 12);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(94, 96);
      this.pictureBox1.TabIndex = 4;
      this.pictureBox1.TabStop = false;
      // 
      // DownloadDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(584, 122);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.unpackText);
      this.Controls.Add(this.unpackProgress);
      this.Controls.Add(this.downloadText);
      this.Controls.Add(this.downloadProgress);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "DownloadDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Conduit - downloading demo...";
      this.Load += new System.EventHandler(this.DownloadDialog_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ProgressBar downloadProgress;
    private System.Windows.Forms.Label downloadText;
    private System.Windows.Forms.Label unpackText;
    private System.Windows.Forms.ProgressBar unpackProgress;
    private System.Windows.Forms.PictureBox pictureBox1;
  }
}

