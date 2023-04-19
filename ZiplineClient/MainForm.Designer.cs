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
            components = new System.ComponentModel.Container();
            mfCentralTabControl = new TabControl();
            mfBrowserTab = new TabPage();
            mfBrowserTableLayout = new TableLayoutPanel();
            mfFavoritesDataGrid = new DataGridView();
            mfFavoritesDataGridGUIDColumn = new DataGridViewTextBoxColumn();
            mfFavoritesDataGridOwnerColumn = new DataGridViewTextBoxColumn();
            mfFavoritesDataGridFilenameColumn = new DataGridViewTextBoxColumn();
            mfFavoritesDataGridFilesizeColumn = new DataGridViewTextBoxColumn();
            mfFavoritesDataGridAccessColumn = new DataGridViewCheckBoxColumn();
            mfFilesContextMenu = new ContextMenuStrip(components);
            downloadToolStripMenuItem = new ToolStripMenuItem();
            requestAccessToolStripMenuItem = new ToolStripMenuItem();
            mfMainDataGrid = new DataGridView();
            mfMainDataGridGUIDColumn = new DataGridViewTextBoxColumn();
            mfMainDataGridOwnerColumn = new DataGridViewTextBoxColumn();
            mfMainDataGridFilenameColumn = new DataGridViewTextBoxColumn();
            mfMainDataGridFilesizeColumn = new DataGridViewTextBoxColumn();
            mfMainDataGridAccessColumn = new DataGridViewCheckBoxColumn();
            mfOnlineUsersLabel = new Label();
            mfOnlineUsersList = new ListBox();
            mfNotificationsLabel = new Label();
            mfNotificationsList = new ListBox();
            mfFavoritesLabel = new Label();
            mfMyFilesTab = new TabPage();
            mfMyFilesTableLayout = new TableLayoutPanel();
            mfAddNewFileLabel = new Label();
            mfUsersToShareList = new ListBox();
            mfUsersSharedWithList = new ListBox();
            mfAuthorizedUsersList = new ListBox();
            mfAuthorizedUsersLabel = new Label();
            mfMyFilesDataGrid = new DataGridView();
            mfMyFilesDataGridGUIDColumn = new DataGridViewTextBoxColumn();
            mfMyFilesDataGridFilenameColumn = new DataGridViewTextBoxColumn();
            mfMyFilesDataGridFilesizeColumn = new DataGridViewTextBoxColumn();
            mfUsersShareListLabel = new Label();
            mfSharedUsersLabel = new Label();
            mfUnshareButton = new Button();
            mfShareButton = new Button();
            mfSelectFileButton = new Button();
            mfAcceptFileButton = new Button();
            mfNewFileFilenameTextBox = new TextBox();
            mfNewFileFilenameLabel = new Label();
            mfOptionsTab = new TabPage();
            button1 = new Button();
            mfUsersContextMenu = new ContextMenuStrip(components);
            refreshToolStripMenuItem = new ToolStripMenuItem();
            mfCentralTabControl.SuspendLayout();
            mfBrowserTab.SuspendLayout();
            mfBrowserTableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mfFavoritesDataGrid).BeginInit();
            mfFilesContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mfMainDataGrid).BeginInit();
            mfMyFilesTab.SuspendLayout();
            mfMyFilesTableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mfMyFilesDataGrid).BeginInit();
            mfOptionsTab.SuspendLayout();
            mfUsersContextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // mfCentralTabControl
            // 
            mfCentralTabControl.Controls.Add(mfBrowserTab);
            mfCentralTabControl.Controls.Add(mfMyFilesTab);
            mfCentralTabControl.Controls.Add(mfOptionsTab);
            mfCentralTabControl.Dock = DockStyle.Fill;
            mfCentralTabControl.Font = new Font("Century", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            mfCentralTabControl.Location = new Point(0, 0);
            mfCentralTabControl.Margin = new Padding(2);
            mfCentralTabControl.Name = "mfCentralTabControl";
            mfCentralTabControl.SelectedIndex = 0;
            mfCentralTabControl.Size = new Size(680, 715);
            mfCentralTabControl.TabIndex = 0;
            mfCentralTabControl.SelectedIndexChanged += CentralTabControl_SelectedIndexChanged;
            // 
            // mfBrowserTab
            // 
            mfBrowserTab.Controls.Add(mfBrowserTableLayout);
            mfBrowserTab.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            mfBrowserTab.Location = new Point(4, 32);
            mfBrowserTab.Name = "mfBrowserTab";
            mfBrowserTab.Padding = new Padding(3);
            mfBrowserTab.Size = new Size(672, 679);
            mfBrowserTab.TabIndex = 0;
            mfBrowserTab.Text = "Browser";
            mfBrowserTab.UseVisualStyleBackColor = true;
            // 
            // mfBrowserTableLayout
            // 
            mfBrowserTableLayout.ColumnCount = 2;
            mfBrowserTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            mfBrowserTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            mfBrowserTableLayout.Controls.Add(mfFavoritesDataGrid, 0, 3);
            mfBrowserTableLayout.Controls.Add(mfMainDataGrid, 0, 0);
            mfBrowserTableLayout.Controls.Add(mfOnlineUsersLabel, 1, 0);
            mfBrowserTableLayout.Controls.Add(mfOnlineUsersList, 1, 1);
            mfBrowserTableLayout.Controls.Add(mfNotificationsLabel, 1, 2);
            mfBrowserTableLayout.Controls.Add(mfNotificationsList, 1, 3);
            mfBrowserTableLayout.Controls.Add(mfFavoritesLabel, 0, 2);
            mfBrowserTableLayout.Location = new Point(3, 3);
            mfBrowserTableLayout.Name = "mfBrowserTableLayout";
            mfBrowserTableLayout.RowCount = 4;
            mfBrowserTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 8.450705F));
            mfBrowserTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 91.54929F));
            mfBrowserTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            mfBrowserTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 211F));
            mfBrowserTableLayout.Size = new Size(666, 673);
            mfBrowserTableLayout.TabIndex = 0;
            // 
            // mfFavoritesDataGrid
            // 
            mfFavoritesDataGrid.AllowUserToAddRows = false;
            mfFavoritesDataGrid.AllowUserToDeleteRows = false;
            mfFavoritesDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            mfFavoritesDataGrid.Columns.AddRange(new DataGridViewColumn[] { mfFavoritesDataGridGUIDColumn, mfFavoritesDataGridOwnerColumn, mfFavoritesDataGridFilenameColumn, mfFavoritesDataGridFilesizeColumn, mfFavoritesDataGridAccessColumn });
            mfFavoritesDataGrid.ContextMenuStrip = mfFilesContextMenu;
            mfFavoritesDataGrid.Location = new Point(3, 464);
            mfFavoritesDataGrid.Name = "mfFavoritesDataGrid";
            mfFavoritesDataGrid.ReadOnly = true;
            mfFavoritesDataGrid.RowHeadersVisible = false;
            mfFavoritesDataGrid.RowTemplate.Height = 25;
            mfFavoritesDataGrid.ScrollBars = ScrollBars.Vertical;
            mfFavoritesDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            mfFavoritesDataGrid.Size = new Size(426, 206);
            mfFavoritesDataGrid.TabIndex = 6;
            // 
            // mfFavoritesDataGridGUIDColumn
            // 
            mfFavoritesDataGridGUIDColumn.HeaderText = "GUID";
            mfFavoritesDataGridGUIDColumn.Name = "mfFavoritesDataGridGUIDColumn";
            mfFavoritesDataGridGUIDColumn.ReadOnly = true;
            mfFavoritesDataGridGUIDColumn.Visible = false;
            // 
            // mfFavoritesDataGridOwnerColumn
            // 
            mfFavoritesDataGridOwnerColumn.HeaderText = "Owner";
            mfFavoritesDataGridOwnerColumn.Name = "mfFavoritesDataGridOwnerColumn";
            mfFavoritesDataGridOwnerColumn.ReadOnly = true;
            // 
            // mfFavoritesDataGridFilenameColumn
            // 
            mfFavoritesDataGridFilenameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            mfFavoritesDataGridFilenameColumn.HeaderText = "Filename";
            mfFavoritesDataGridFilenameColumn.Name = "mfFavoritesDataGridFilenameColumn";
            mfFavoritesDataGridFilenameColumn.ReadOnly = true;
            // 
            // mfFavoritesDataGridFilesizeColumn
            // 
            mfFavoritesDataGridFilesizeColumn.HeaderText = "Filesize";
            mfFavoritesDataGridFilesizeColumn.Name = "mfFavoritesDataGridFilesizeColumn";
            mfFavoritesDataGridFilesizeColumn.ReadOnly = true;
            // 
            // mfFavoritesDataGridAccessColumn
            // 
            mfFavoritesDataGridAccessColumn.HeaderText = "Access";
            mfFavoritesDataGridAccessColumn.Name = "mfFavoritesDataGridAccessColumn";
            mfFavoritesDataGridAccessColumn.ReadOnly = true;
            mfFavoritesDataGridAccessColumn.Resizable = DataGridViewTriState.True;
            mfFavoritesDataGridAccessColumn.Width = 70;
            // 
            // mfFilesContextMenu
            // 
            mfFilesContextMenu.Items.AddRange(new ToolStripItem[] { downloadToolStripMenuItem, requestAccessToolStripMenuItem });
            mfFilesContextMenu.Name = "mfContextMenu";
            mfFilesContextMenu.Size = new Size(156, 48);
            mfFilesContextMenu.ItemClicked += FileContextMenu_ItemClicked;
            // 
            // downloadToolStripMenuItem
            // 
            downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            downloadToolStripMenuItem.Size = new Size(155, 22);
            downloadToolStripMenuItem.Text = "Download";
            // 
            // requestAccessToolStripMenuItem
            // 
            requestAccessToolStripMenuItem.Name = "requestAccessToolStripMenuItem";
            requestAccessToolStripMenuItem.Size = new Size(155, 22);
            requestAccessToolStripMenuItem.Text = "Request Access";
            // 
            // mfMainDataGrid
            // 
            mfMainDataGrid.AllowUserToAddRows = false;
            mfMainDataGrid.AllowUserToDeleteRows = false;
            mfMainDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            mfMainDataGrid.Columns.AddRange(new DataGridViewColumn[] { mfMainDataGridGUIDColumn, mfMainDataGridOwnerColumn, mfMainDataGridFilenameColumn, mfMainDataGridFilesizeColumn, mfMainDataGridAccessColumn });
            mfMainDataGrid.ContextMenuStrip = mfFilesContextMenu;
            mfMainDataGrid.Location = new Point(3, 3);
            mfMainDataGrid.Name = "mfMainDataGrid";
            mfMainDataGrid.ReadOnly = true;
            mfMainDataGrid.RowHeadersVisible = false;
            mfBrowserTableLayout.SetRowSpan(mfMainDataGrid, 2);
            mfMainDataGrid.RowTemplate.Height = 25;
            mfMainDataGrid.ScrollBars = ScrollBars.Vertical;
            mfMainDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            mfMainDataGrid.Size = new Size(426, 417);
            mfMainDataGrid.TabIndex = 0;
            // 
            // mfMainDataGridGUIDColumn
            // 
            mfMainDataGridGUIDColumn.HeaderText = "GUID";
            mfMainDataGridGUIDColumn.Name = "mfMainDataGridGUIDColumn";
            mfMainDataGridGUIDColumn.ReadOnly = true;
            mfMainDataGridGUIDColumn.Visible = false;
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
            mfMainDataGridAccessColumn.Resizable = DataGridViewTriState.True;
            mfMainDataGridAccessColumn.Width = 70;
            // 
            // mfOnlineUsersLabel
            // 
            mfOnlineUsersLabel.Anchor = AnchorStyles.None;
            mfOnlineUsersLabel.AutoSize = true;
            mfOnlineUsersLabel.Location = new Point(489, 5);
            mfOnlineUsersLabel.Name = "mfOnlineUsersLabel";
            mfOnlineUsersLabel.Size = new Size(119, 25);
            mfOnlineUsersLabel.TabIndex = 1;
            mfOnlineUsersLabel.Text = "Online Users";
            // 
            // mfOnlineUsersList
            // 
            mfOnlineUsersList.ContextMenuStrip = mfUsersContextMenu;
            mfOnlineUsersList.Dock = DockStyle.Fill;
            mfOnlineUsersList.ItemHeight = 25;
            mfOnlineUsersList.Location = new Point(435, 38);
            mfOnlineUsersList.Name = "mfOnlineUsersList";
            mfOnlineUsersList.Size = new Size(228, 382);
            mfOnlineUsersList.TabIndex = 2;
            // 
            // mfNotificationsLabel
            // 
            mfNotificationsLabel.Anchor = AnchorStyles.None;
            mfNotificationsLabel.AutoSize = true;
            mfNotificationsLabel.Location = new Point(489, 429);
            mfNotificationsLabel.Name = "mfNotificationsLabel";
            mfNotificationsLabel.Size = new Size(119, 25);
            mfNotificationsLabel.TabIndex = 3;
            mfNotificationsLabel.Text = "Notifications";
            // 
            // mfNotificationsList
            // 
            mfNotificationsList.Dock = DockStyle.Fill;
            mfNotificationsList.ItemHeight = 25;
            mfNotificationsList.Location = new Point(435, 464);
            mfNotificationsList.Name = "mfNotificationsList";
            mfNotificationsList.Size = new Size(228, 206);
            mfNotificationsList.TabIndex = 4;
            // 
            // mfFavoritesLabel
            // 
            mfFavoritesLabel.Anchor = AnchorStyles.None;
            mfFavoritesLabel.AutoSize = true;
            mfFavoritesLabel.Location = new Point(173, 429);
            mfFavoritesLabel.Name = "mfFavoritesLabel";
            mfFavoritesLabel.Size = new Size(86, 25);
            mfFavoritesLabel.TabIndex = 5;
            mfFavoritesLabel.Text = "Favorites";
            // 
            // mfMyFilesTab
            // 
            mfMyFilesTab.Controls.Add(mfMyFilesTableLayout);
            mfMyFilesTab.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            mfMyFilesTab.Location = new Point(4, 32);
            mfMyFilesTab.Name = "mfMyFilesTab";
            mfMyFilesTab.Padding = new Padding(3);
            mfMyFilesTab.Size = new Size(672, 679);
            mfMyFilesTab.TabIndex = 1;
            mfMyFilesTab.Text = "My Files";
            mfMyFilesTab.UseVisualStyleBackColor = true;
            // 
            // mfMyFilesTableLayout
            // 
            mfMyFilesTableLayout.ColumnCount = 4;
            mfMyFilesTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            mfMyFilesTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            mfMyFilesTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            mfMyFilesTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            mfMyFilesTableLayout.Controls.Add(mfAddNewFileLabel, 0, 2);
            mfMyFilesTableLayout.Controls.Add(mfUsersToShareList, 3, 3);
            mfMyFilesTableLayout.Controls.Add(mfUsersSharedWithList, 1, 3);
            mfMyFilesTableLayout.Controls.Add(mfAuthorizedUsersList, 3, 1);
            mfMyFilesTableLayout.Controls.Add(mfAuthorizedUsersLabel, 3, 0);
            mfMyFilesTableLayout.Controls.Add(mfMyFilesDataGrid, 0, 0);
            mfMyFilesTableLayout.Controls.Add(mfUsersShareListLabel, 3, 2);
            mfMyFilesTableLayout.Controls.Add(mfSharedUsersLabel, 1, 2);
            mfMyFilesTableLayout.Controls.Add(mfUnshareButton, 2, 5);
            mfMyFilesTableLayout.Controls.Add(mfShareButton, 2, 4);
            mfMyFilesTableLayout.Controls.Add(mfSelectFileButton, 0, 3);
            mfMyFilesTableLayout.Controls.Add(mfAcceptFileButton, 0, 6);
            mfMyFilesTableLayout.Controls.Add(mfNewFileFilenameTextBox, 0, 5);
            mfMyFilesTableLayout.Controls.Add(mfNewFileFilenameLabel, 0, 4);
            mfMyFilesTableLayout.Location = new Point(3, 3);
            mfMyFilesTableLayout.Name = "mfMyFilesTableLayout";
            mfMyFilesTableLayout.RowCount = 7;
            mfMyFilesTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 8.450705F));
            mfMyFilesTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 91.54929F));
            mfMyFilesTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            mfMyFilesTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            mfMyFilesTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            mfMyFilesTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            mfMyFilesTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            mfMyFilesTableLayout.Size = new Size(666, 673);
            mfMyFilesTableLayout.TabIndex = 1;
            // 
            // mfAddNewFileLabel
            // 
            mfAddNewFileLabel.Anchor = AnchorStyles.None;
            mfAddNewFileLabel.AutoSize = true;
            mfAddNewFileLabel.Location = new Point(38, 436);
            mfAddNewFileLabel.Name = "mfAddNewFileLabel";
            mfAddNewFileLabel.Size = new Size(123, 25);
            mfAddNewFileLabel.TabIndex = 10;
            mfAddNewFileLabel.Text = "Add New File";
            // 
            // mfUsersToShareList
            // 
            mfUsersToShareList.Dock = DockStyle.Fill;
            mfUsersToShareList.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            mfUsersToShareList.ItemHeight = 21;
            mfUsersToShareList.Location = new Point(467, 471);
            mfUsersToShareList.Name = "mfUsersToShareList";
            mfMyFilesTableLayout.SetRowSpan(mfUsersToShareList, 4);
            mfUsersToShareList.Size = new Size(196, 199);
            mfUsersToShareList.TabIndex = 5;
            // 
            // mfUsersSharedWithList
            // 
            mfUsersSharedWithList.Dock = DockStyle.Fill;
            mfUsersSharedWithList.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            mfUsersSharedWithList.ItemHeight = 21;
            mfUsersSharedWithList.Location = new Point(202, 471);
            mfUsersSharedWithList.Name = "mfUsersSharedWithList";
            mfMyFilesTableLayout.SetRowSpan(mfUsersSharedWithList, 4);
            mfUsersSharedWithList.Size = new Size(193, 199);
            mfUsersSharedWithList.TabIndex = 4;
            // 
            // mfAuthorizedUsersList
            // 
            mfAuthorizedUsersList.Dock = DockStyle.Fill;
            mfAuthorizedUsersList.ItemHeight = 25;
            mfAuthorizedUsersList.Location = new Point(467, 39);
            mfAuthorizedUsersList.Name = "mfAuthorizedUsersList";
            mfAuthorizedUsersList.Size = new Size(196, 388);
            mfAuthorizedUsersList.TabIndex = 2;
            // 
            // mfAuthorizedUsersLabel
            // 
            mfAuthorizedUsersLabel.Anchor = AnchorStyles.None;
            mfAuthorizedUsersLabel.AutoSize = true;
            mfAuthorizedUsersLabel.Location = new Point(487, 5);
            mfAuthorizedUsersLabel.Name = "mfAuthorizedUsersLabel";
            mfAuthorizedUsersLabel.Size = new Size(156, 25);
            mfAuthorizedUsersLabel.TabIndex = 1;
            mfAuthorizedUsersLabel.Text = "Authorized Users";
            // 
            // mfMyFilesDataGrid
            // 
            mfMyFilesDataGrid.AllowUserToAddRows = false;
            mfMyFilesDataGrid.AllowUserToDeleteRows = false;
            mfMyFilesDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            mfMyFilesDataGrid.Columns.AddRange(new DataGridViewColumn[] { mfMyFilesDataGridGUIDColumn, mfMyFilesDataGridFilenameColumn, mfMyFilesDataGridFilesizeColumn });
            mfMyFilesTableLayout.SetColumnSpan(mfMyFilesDataGrid, 3);
            mfMyFilesDataGrid.Location = new Point(3, 3);
            mfMyFilesDataGrid.Name = "mfMyFilesDataGrid";
            mfMyFilesDataGrid.ReadOnly = true;
            mfMyFilesDataGrid.RowHeadersVisible = false;
            mfMyFilesTableLayout.SetRowSpan(mfMyFilesDataGrid, 2);
            mfMyFilesDataGrid.RowTemplate.Height = 25;
            mfMyFilesDataGrid.ScrollBars = ScrollBars.Vertical;
            mfMyFilesDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            mfMyFilesDataGrid.Size = new Size(458, 418);
            mfMyFilesDataGrid.TabIndex = 0;
            // 
            // mfMyFilesDataGridGUIDColumn
            // 
            mfMyFilesDataGridGUIDColumn.HeaderText = "GID";
            mfMyFilesDataGridGUIDColumn.Name = "mfMyFilesDataGridGUIDColumn";
            mfMyFilesDataGridGUIDColumn.ReadOnly = true;
            mfMyFilesDataGridGUIDColumn.Visible = false;
            // 
            // mfMyFilesDataGridFilenameColumn
            // 
            mfMyFilesDataGridFilenameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            mfMyFilesDataGridFilenameColumn.HeaderText = "Filename";
            mfMyFilesDataGridFilenameColumn.Name = "mfMyFilesDataGridFilenameColumn";
            mfMyFilesDataGridFilenameColumn.ReadOnly = true;
            // 
            // mfMyFilesDataGridFilesizeColumn
            // 
            mfMyFilesDataGridFilesizeColumn.HeaderText = "Filesize";
            mfMyFilesDataGridFilesizeColumn.Name = "mfMyFilesDataGridFilesizeColumn";
            mfMyFilesDataGridFilesizeColumn.ReadOnly = true;
            // 
            // mfUsersShareListLabel
            // 
            mfUsersShareListLabel.Anchor = AnchorStyles.None;
            mfUsersShareListLabel.AutoSize = true;
            mfUsersShareListLabel.Location = new Point(519, 436);
            mfUsersShareListLabel.Name = "mfUsersShareListLabel";
            mfUsersShareListLabel.Size = new Size(91, 25);
            mfUsersShareListLabel.TabIndex = 8;
            mfUsersShareListLabel.Text = "Users List";
            // 
            // mfSharedUsersLabel
            // 
            mfSharedUsersLabel.Anchor = AnchorStyles.None;
            mfSharedUsersLabel.AutoSize = true;
            mfSharedUsersLabel.Location = new Point(229, 436);
            mfSharedUsersLabel.Name = "mfSharedUsersLabel";
            mfSharedUsersLabel.Size = new Size(139, 25);
            mfSharedUsersLabel.TabIndex = 9;
            mfSharedUsersLabel.Text = "Share File With";
            // 
            // mfUnshareButton
            // 
            mfUnshareButton.Anchor = AnchorStyles.Top;
            mfUnshareButton.Enabled = false;
            mfUnshareButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            mfUnshareButton.Location = new Point(401, 573);
            mfUnshareButton.Name = "mfUnshareButton";
            mfUnshareButton.Size = new Size(60, 29);
            mfUnshareButton.TabIndex = 7;
            mfUnshareButton.Text = ">>";
            mfUnshareButton.UseVisualStyleBackColor = true;
            mfUnshareButton.Click += NewFileUnshareButton_Click;
            // 
            // mfShareButton
            // 
            mfShareButton.Anchor = AnchorStyles.Bottom;
            mfShareButton.Enabled = false;
            mfShareButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            mfShareButton.Location = new Point(401, 538);
            mfShareButton.Name = "mfShareButton";
            mfShareButton.Size = new Size(60, 29);
            mfShareButton.TabIndex = 6;
            mfShareButton.Text = "<<";
            mfShareButton.UseVisualStyleBackColor = true;
            mfShareButton.Click += NewFileShareButton_Click;
            // 
            // mfSelectFileButton
            // 
            mfSelectFileButton.Anchor = AnchorStyles.None;
            mfSelectFileButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            mfSelectFileButton.Location = new Point(43, 477);
            mfSelectFileButton.Name = "mfSelectFileButton";
            mfSelectFileButton.Size = new Size(113, 32);
            mfSelectFileButton.TabIndex = 11;
            mfSelectFileButton.Text = "Select File";
            mfSelectFileButton.UseVisualStyleBackColor = true;
            mfSelectFileButton.Click += NewFileSelectButton_Click;
            // 
            // mfAcceptFileButton
            // 
            mfAcceptFileButton.Anchor = AnchorStyles.None;
            mfAcceptFileButton.Enabled = false;
            mfAcceptFileButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            mfAcceptFileButton.Location = new Point(43, 631);
            mfAcceptFileButton.Name = "mfAcceptFileButton";
            mfAcceptFileButton.Size = new Size(113, 32);
            mfAcceptFileButton.TabIndex = 12;
            mfAcceptFileButton.Text = "Accept";
            mfAcceptFileButton.UseVisualStyleBackColor = true;
            mfAcceptFileButton.Click += NewFileAcceptButton_Click;
            // 
            // mfNewFileFilenameTextBox
            // 
            mfNewFileFilenameTextBox.Anchor = AnchorStyles.Top;
            mfNewFileFilenameTextBox.Enabled = false;
            mfNewFileFilenameTextBox.Location = new Point(3, 573);
            mfNewFileFilenameTextBox.Name = "mfNewFileFilenameTextBox";
            mfNewFileFilenameTextBox.Size = new Size(193, 33);
            mfNewFileFilenameTextBox.TabIndex = 13;
            // 
            // mfNewFileFilenameLabel
            // 
            mfNewFileFilenameLabel.Anchor = AnchorStyles.Bottom;
            mfNewFileFilenameLabel.AutoSize = true;
            mfNewFileFilenameLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            mfNewFileFilenameLabel.Location = new Point(70, 553);
            mfNewFileFilenameLabel.Name = "mfNewFileFilenameLabel";
            mfNewFileFilenameLabel.Size = new Size(59, 17);
            mfNewFileFilenameLabel.TabIndex = 14;
            mfNewFileFilenameLabel.Text = "Filename";
            // 
            // mfOptionsTab
            // 
            mfOptionsTab.Controls.Add(button1);
            mfOptionsTab.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            mfOptionsTab.Location = new Point(4, 32);
            mfOptionsTab.Name = "mfOptionsTab";
            mfOptionsTab.Padding = new Padding(3);
            mfOptionsTab.Size = new Size(672, 679);
            mfOptionsTab.TabIndex = 2;
            mfOptionsTab.Text = "Options";
            mfOptionsTab.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(543, 6);
            button1.Name = "button1";
            button1.Size = new Size(121, 63);
            button1.TabIndex = 0;
            button1.Text = "ping server";
            button1.UseVisualStyleBackColor = true;
            button1.Click += TEST_CLICK;
            // 
            // mfUsersContextMenu
            // 
            mfUsersContextMenu.Items.AddRange(new ToolStripItem[] { refreshToolStripMenuItem });
            mfUsersContextMenu.Name = "mfUsersContextMenu";
            mfUsersContextMenu.Size = new Size(114, 26);
            // 
            // refreshToolStripMenuItem
            // 
            refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            refreshToolStripMenuItem.Size = new Size(113, 22);
            refreshToolStripMenuItem.Text = "Refresh";
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
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Zipline Client";
            FormClosing += MainForm_FormClosing;
            mfCentralTabControl.ResumeLayout(false);
            mfBrowserTab.ResumeLayout(false);
            mfBrowserTableLayout.ResumeLayout(false);
            mfBrowserTableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)mfFavoritesDataGrid).EndInit();
            mfFilesContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mfMainDataGrid).EndInit();
            mfMyFilesTab.ResumeLayout(false);
            mfMyFilesTableLayout.ResumeLayout(false);
            mfMyFilesTableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)mfMyFilesDataGrid).EndInit();
            mfOptionsTab.ResumeLayout(false);
            mfUsersContextMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl mfCentralTabControl;
        private TabPage mfBrowserTab;
        private TabPage mfMyFilesTab;
        private TabPage mfOptionsTab;
        private TableLayoutPanel mfBrowserTableLayout;
        private DataGridView mfMainDataGrid;
        private Label mfOnlineUsersLabel;
        private ListBox mfOnlineUsersList;
        private Label mfNotificationsLabel;
        private ListBox mfNotificationsList;
        private Label mfFavoritesLabel;
        private DataGridView mfFavoritesDataGrid;
        private TableLayoutPanel mfMyFilesTableLayout;
        private DataGridView mfMyFilesDataGrid;
        private ListBox mfUsersSharedWithList;
        private ListBox mfAuthorizedUsersList;
        private Label mfAuthorizedUsersLabel;
        private Label mfAddNewFileLabel;
        private ListBox mfUsersToShareList;
        private Label mfUsersShareListLabel;
        private Label mfSharedUsersLabel;
        private Button mfUnshareButton;
        private Button mfShareButton;
        private Button mfSelectFileButton;
        private Button mfAcceptFileButton;
        private TextBox mfNewFileFilenameTextBox;
        private Label mfNewFileFilenameLabel;
        private DataGridViewTextBoxColumn mfMyFilesDataGridGUIDColumn;
        private DataGridViewTextBoxColumn mfMyFilesDataGridFilenameColumn;
        private DataGridViewTextBoxColumn mfMyFilesDataGridFilesizeColumn;
        private DataGridViewTextBoxColumn mfMainDataGridGUIDColumn;
        private DataGridViewTextBoxColumn mfMainDataGridOwnerColumn;
        private DataGridViewTextBoxColumn mfMainDataGridFilenameColumn;
        private DataGridViewTextBoxColumn mfMainDataGridFilesizeColumn;
        private DataGridViewCheckBoxColumn mfMainDataGridAccessColumn;
        private DataGridViewTextBoxColumn mfFavoritesDataGridGUIDColumn;
        private DataGridViewTextBoxColumn mfFavoritesDataGridOwnerColumn;
        private DataGridViewTextBoxColumn mfFavoritesDataGridFilenameColumn;
        private DataGridViewTextBoxColumn mfFavoritesDataGridFilesizeColumn;
        private DataGridViewCheckBoxColumn mfFavoritesDataGridAccessColumn;
        private Button button1;
        private ContextMenuStrip mfFilesContextMenu;
        private ToolStripMenuItem downloadToolStripMenuItem;
        private ToolStripMenuItem requestAccessToolStripMenuItem;
        private ContextMenuStrip mfUsersContextMenu;
        private ToolStripMenuItem refreshToolStripMenuItem;
    }
}