namespace ZiplineClient
{
    partial class DownloadForm
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
            this.dfDownloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.dfDownloadingLabel = new System.Windows.Forms.Label();
            this.dfCancelButton = new System.Windows.Forms.Button();
            this.dfTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.dfTableLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // dfDownloadProgressBar
            // 
            this.dfTableLayout.SetColumnSpan(this.dfDownloadProgressBar, 3);
            this.dfDownloadProgressBar.Location = new System.Drawing.Point(3, 3);
            this.dfDownloadProgressBar.Name = "dfDownloadProgressBar";
            this.dfDownloadProgressBar.Size = new System.Drawing.Size(288, 29);
            this.dfDownloadProgressBar.TabIndex = 0;
            // 
            // dfDownloadingLabel
            // 
            this.dfDownloadingLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dfDownloadingLabel.AutoSize = true;
            this.dfDownloadingLabel.Location = new System.Drawing.Point(3, 40);
            this.dfDownloadingLabel.Name = "dfDownloadingLabel";
            this.dfDownloadingLabel.Size = new System.Drawing.Size(78, 15);
            this.dfDownloadingLabel.TabIndex = 1;
            this.dfDownloadingLabel.Text = "Downloading";
            // 
            // dfCancelButton
            // 
            this.dfCancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.dfCancelButton.Location = new System.Drawing.Point(100, 67);
            this.dfCancelButton.Name = "dfCancelButton";
            this.dfCancelButton.Size = new System.Drawing.Size(93, 27);
            this.dfCancelButton.TabIndex = 2;
            this.dfCancelButton.Text = "Cancel";
            this.dfCancelButton.UseVisualStyleBackColor = true;
            this.dfCancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // dfTableLayout
            // 
            this.dfTableLayout.ColumnCount = 3;
            this.dfTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.dfTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.dfTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.dfTableLayout.Controls.Add(this.dfDownloadProgressBar, 0, 0);
            this.dfTableLayout.Controls.Add(this.dfDownloadingLabel, 0, 1);
            this.dfTableLayout.Controls.Add(this.dfCancelButton, 1, 2);
            this.dfTableLayout.Location = new System.Drawing.Point(10, 10);
            this.dfTableLayout.Margin = new System.Windows.Forms.Padding(1);
            this.dfTableLayout.Name = "dfTableLayout";
            this.dfTableLayout.RowCount = 3;
            this.dfTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.dfTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.dfTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.dfTableLayout.Size = new System.Drawing.Size(294, 97);
            this.dfTableLayout.TabIndex = 3;
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 117);
            this.Controls.Add(this.dfTableLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "DownloadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Download";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DownloadForm_FormClosing);
            this.Shown += new System.EventHandler(this.DownloadForm_Shown);
            this.dfTableLayout.ResumeLayout(false);
            this.dfTableLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ProgressBar dfDownloadProgressBar;
        private TableLayoutPanel dfTableLayout;
        private Label dfDownloadingLabel;
        private Button dfCancelButton;
    }
}