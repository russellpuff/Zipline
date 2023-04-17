namespace ZiplineClient
{
    partial class MainForm
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
            mfCentralTabControl = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            mfMainDataGrid = new DataGridView();
            mfMainDataGridGIDColumn = new DataGridViewTextBoxColumn();
            mfMainDataGridOwnerColumn = new DataGridViewTextBoxColumn();
            mfMainDataGridFilenameColumn = new DataGridViewTextBoxColumn();
            mfMainDataGridFilesizeColumn = new DataGridViewTextBoxColumn();
            mfMainDataGridAccessColumn = new DataGridViewImageColumn();
            label1 = new Label();
            listView1 = new ListView();
            label2 = new Label();
            listView2 = new ListView();
            mfCentralTabControl.SuspendLayout();
            tabPage1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mfMainDataGrid).BeginInit();
            SuspendLayout();
            // 
            // mfCentralTabControl
            // 
            mfCentralTabControl.Controls.Add(tabPage1);
            mfCentralTabControl.Controls.Add(tabPage2);
            mfCentralTabControl.Controls.Add(tabPage3);
            mfCentralTabControl.Dock = DockStyle.Fill;
            mfCentralTabControl.Font = new Font("Century", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            mfCentralTabControl.Location = new Point(0, 0);
            mfCentralTabControl.Margin = new Padding(2);
            mfCentralTabControl.Name = "mfCentralTabControl";
            mfCentralTabControl.SelectedIndex = 0;
            mfCentralTabControl.Size = new Size(680, 715);
            mfCentralTabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(tableLayoutPanel1);
            tabPage1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            tabPage1.Location = new Point(4, 32);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(672, 679);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Browser";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(783, 722);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "My Files";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(783, 722);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Options";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutPanel1.Controls.Add(mfMainDataGrid, 0, 0);
            tableLayoutPanel1.Controls.Add(label1, 1, 0);
            tableLayoutPanel1.Controls.Add(listView1, 1, 1);
            tableLayoutPanel1.Controls.Add(label2, 1, 2);
            tableLayoutPanel1.Controls.Add(listView2, 1, 3);
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.450705F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 91.54929F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 211F));
            tableLayoutPanel1.Size = new Size(666, 673);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // mfMainDataGrid
            // 
            mfMainDataGrid.AllowUserToAddRows = false;
            mfMainDataGrid.AllowUserToDeleteRows = false;
            mfMainDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            mfMainDataGrid.Columns.AddRange(new DataGridViewColumn[] { mfMainDataGridGIDColumn, mfMainDataGridOwnerColumn, mfMainDataGridFilenameColumn, mfMainDataGridFilesizeColumn, mfMainDataGridAccessColumn });
            mfMainDataGrid.Location = new Point(3, 3);
            mfMainDataGrid.Name = "mfMainDataGrid";
            mfMainDataGrid.ReadOnly = true;
            mfMainDataGrid.RowHeadersVisible = false;
            tableLayoutPanel1.SetRowSpan(mfMainDataGrid, 2);
            mfMainDataGrid.RowTemplate.Height = 25;
            mfMainDataGrid.ScrollBars = ScrollBars.Vertical;
            mfMainDataGrid.Size = new Size(426, 417);
            mfMainDataGrid.TabIndex = 0;
            // 
            // mfMainDataGridGIDColumn
            // 
            mfMainDataGridGIDColumn.HeaderText = "GID";
            mfMainDataGridGIDColumn.Name = "mfMainDataGridGIDColumn";
            mfMainDataGridGIDColumn.ReadOnly = true;
            mfMainDataGridGIDColumn.Visible = false;
            // 
            // mfMainDataGridOwnerColumn
            // 
            mfMainDataGridOwnerColumn.HeaderText = "Owner";
            mfMainDataGridOwnerColumn.Name = "mfMainDataGridOwnerColumn";
            mfMainDataGridOwnerColumn.ReadOnly = true;
            // 
            // mfMainDataGridFilenameColumn
            // 
            mfMainDataGridFilenameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            mfMainDataGridFilenameColumn.HeaderText = "Filename";
            mfMainDataGridFilenameColumn.Name = "mfMainDataGridFilenameColumn";
            mfMainDataGridFilenameColumn.ReadOnly = true;
            // 
            // mfMainDataGridFilesizeColumn
            // 
            mfMainDataGridFilesizeColumn.HeaderText = "Filesize";
            mfMainDataGridFilesizeColumn.Name = "mfMainDataGridFilesizeColumn";
            mfMainDataGridFilesizeColumn.ReadOnly = true;
            // 
            // mfMainDataGridAccessColumn
            // 
            mfMainDataGridAccessColumn.HeaderText = "Access";
            mfMainDataGridAccessColumn.Name = "mfMainDataGridAccessColumn";
            mfMainDataGridAccessColumn.ReadOnly = true;
            mfMainDataGridAccessColumn.Width = 70;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(489, 5);
            label1.Name = "label1";
            label1.Size = new Size(119, 25);
            label1.TabIndex = 1;
            label1.Text = "Online Users";
            // 
            // listView1
            // 
            listView1.Location = new Point(435, 38);
            listView1.Name = "listView1";
            listView1.Size = new Size(228, 382);
            listView1.TabIndex = 2;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Location = new Point(489, 429);
            label2.Name = "label2";
            label2.Size = new Size(119, 25);
            label2.TabIndex = 3;
            label2.Text = "Notifications";
            // 
            // listView2
            // 
            listView2.Location = new Point(435, 464);
            listView2.Name = "listView2";
            listView2.Size = new Size(228, 206);
            listView2.TabIndex = 4;
            listView2.UseCompatibleStateImageBehavior = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(680, 715);
            Controls.Add(mfCentralTabControl);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Zipline Client";
            mfCentralTabControl.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)mfMainDataGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl mfCentralTabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TableLayoutPanel tableLayoutPanel1;
        private DataGridView mfMainDataGrid;
        private DataGridViewTextBoxColumn mfMainDataGridGIDColumn;
        private DataGridViewTextBoxColumn mfMainDataGridOwnerColumn;
        private DataGridViewTextBoxColumn mfMainDataGridFilenameColumn;
        private DataGridViewTextBoxColumn mfMainDataGridFilesizeColumn;
        private DataGridViewImageColumn mfMainDataGridAccessColumn;
        private Label label1;
        private ListView listView1;
        private Label label2;
        private ListView listView2;
    }
}