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
            this.components = new System.ComponentModel.Container();
            this.mfCentralTabControl = new System.Windows.Forms.TabControl();
            this.mfBrowserTab = new System.Windows.Forms.TabPage();
            this.mfBrowserTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.mfFavoritesDataGrid = new System.Windows.Forms.DataGridView();
            this.mfFavoritesDataGridGUIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mfFavoritesDataGridOwnerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mfFavoritesDataGridFilenameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mfFavoritesDataGridFilesizeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mfFavoritesDataGridAccessColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.mfFilesContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.requestAccessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mfMainDataGrid = new System.Windows.Forms.DataGridView();
            this.mfMainDataGridGUIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mfMainDataGridOwnerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mfMainDataGridFilenameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mfMainDataGridFilesizeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mfMainDataGridAccessColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.mfOnlineUsersLabel = new System.Windows.Forms.Label();
            this.mfOnlineUsersList = new System.Windows.Forms.ListBox();
            this.mfUsersContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mfNotificationsLabel = new System.Windows.Forms.Label();
            this.mfNotificationsList = new System.Windows.Forms.ListBox();
            this.mfFavoritesLabel = new System.Windows.Forms.Label();
            this.mfMyFilesTab = new System.Windows.Forms.TabPage();
            this.mfMyFilesTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.mfAddNewFileLabel = new System.Windows.Forms.Label();
            this.mfUsersToShareList = new System.Windows.Forms.ListBox();
            this.mfUsersSharedWithList = new System.Windows.Forms.ListBox();
            this.mfAuthorizedUsersList = new System.Windows.Forms.ListBox();
            this.mfAuthorizedUsersLabel = new System.Windows.Forms.Label();
            this.mfMyFilesDataGrid = new System.Windows.Forms.DataGridView();
            this.mfMyFilesDataGridGUIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mfMyFilesDataGridFilenameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mfMyFilesDataGridFilesizeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mfPersonalFilesContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shareWithToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mfUsersShareListLabel = new System.Windows.Forms.Label();
            this.mfSharedUsersLabel = new System.Windows.Forms.Label();
            this.mfUnshareButton = new System.Windows.Forms.Button();
            this.mfShareButton = new System.Windows.Forms.Button();
            this.mfSelectFileButton = new System.Windows.Forms.Button();
            this.mfAcceptFileButton = new System.Windows.Forms.Button();
            this.mfNewFileFilenameTextBox = new System.Windows.Forms.TextBox();
            this.mfNewFileFilenameLabel = new System.Windows.Forms.Label();
            this.mfOptionsTab = new System.Windows.Forms.TabPage();
            this.mfCentralTabControl.SuspendLayout();
            this.mfBrowserTab.SuspendLayout();
            this.mfBrowserTableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mfFavoritesDataGrid)).BeginInit();
            this.mfFilesContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mfMainDataGrid)).BeginInit();
            this.mfUsersContextMenu.SuspendLayout();
            this.mfMyFilesTab.SuspendLayout();
            this.mfMyFilesTableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mfMyFilesDataGrid)).BeginInit();
            this.mfPersonalFilesContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mfCentralTabControl
            // 
            this.mfCentralTabControl.Controls.Add(this.mfBrowserTab);
            this.mfCentralTabControl.Controls.Add(this.mfMyFilesTab);
            this.mfCentralTabControl.Controls.Add(this.mfOptionsTab);
            this.mfCentralTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mfCentralTabControl.Font = new System.Drawing.Font("Century", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mfCentralTabControl.Location = new System.Drawing.Point(0, 0);
            this.mfCentralTabControl.Margin = new System.Windows.Forms.Padding(2);
            this.mfCentralTabControl.Name = "mfCentralTabControl";
            this.mfCentralTabControl.SelectedIndex = 0;
            this.mfCentralTabControl.Size = new System.Drawing.Size(680, 715);
            this.mfCentralTabControl.TabIndex = 0;
            this.mfCentralTabControl.SelectedIndexChanged += new System.EventHandler(this.CentralTabControl_SelectedIndexChanged);
            // 
            // mfBrowserTab
            // 
            this.mfBrowserTab.Controls.Add(this.mfBrowserTableLayout);
            this.mfBrowserTab.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mfBrowserTab.Location = new System.Drawing.Point(4, 32);
            this.mfBrowserTab.Name = "mfBrowserTab";
            this.mfBrowserTab.Padding = new System.Windows.Forms.Padding(3);
            this.mfBrowserTab.Size = new System.Drawing.Size(672, 679);
            this.mfBrowserTab.TabIndex = 0;
            this.mfBrowserTab.Text = "Browser";
            this.mfBrowserTab.UseVisualStyleBackColor = true;
            // 
            // mfBrowserTableLayout
            // 
            this.mfBrowserTableLayout.ColumnCount = 2;
            this.mfBrowserTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.mfBrowserTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.mfBrowserTableLayout.Controls.Add(this.mfFavoritesDataGrid, 0, 3);
            this.mfBrowserTableLayout.Controls.Add(this.mfMainDataGrid, 0, 0);
            this.mfBrowserTableLayout.Controls.Add(this.mfOnlineUsersLabel, 1, 0);
            this.mfBrowserTableLayout.Controls.Add(this.mfOnlineUsersList, 1, 1);
            this.mfBrowserTableLayout.Controls.Add(this.mfNotificationsLabel, 1, 2);
            this.mfBrowserTableLayout.Controls.Add(this.mfNotificationsList, 1, 3);
            this.mfBrowserTableLayout.Controls.Add(this.mfFavoritesLabel, 0, 2);
            this.mfBrowserTableLayout.Location = new System.Drawing.Point(3, 3);
            this.mfBrowserTableLayout.Name = "mfBrowserTableLayout";
            this.mfBrowserTableLayout.RowCount = 4;
            this.mfBrowserTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.450705F));
            this.mfBrowserTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.54929F));
            this.mfBrowserTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.mfBrowserTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 211F));
            this.mfBrowserTableLayout.Size = new System.Drawing.Size(666, 673);
            this.mfBrowserTableLayout.TabIndex = 0;
            // 
            // mfFavoritesDataGrid
            // 
            this.mfFavoritesDataGrid.AllowUserToAddRows = false;
            this.mfFavoritesDataGrid.AllowUserToDeleteRows = false;
            this.mfFavoritesDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mfFavoritesDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mfFavoritesDataGridGUIDColumn,
            this.mfFavoritesDataGridOwnerColumn,
            this.mfFavoritesDataGridFilenameColumn,
            this.mfFavoritesDataGridFilesizeColumn,
            this.mfFavoritesDataGridAccessColumn});
            this.mfFavoritesDataGrid.ContextMenuStrip = this.mfFilesContextMenu;
            this.mfFavoritesDataGrid.Location = new System.Drawing.Point(3, 464);
            this.mfFavoritesDataGrid.Name = "mfFavoritesDataGrid";
            this.mfFavoritesDataGrid.ReadOnly = true;
            this.mfFavoritesDataGrid.RowHeadersVisible = false;
            this.mfFavoritesDataGrid.RowTemplate.Height = 25;
            this.mfFavoritesDataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mfFavoritesDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mfFavoritesDataGrid.Size = new System.Drawing.Size(426, 206);
            this.mfFavoritesDataGrid.TabIndex = 6;
            // 
            // mfFavoritesDataGridGUIDColumn
            // 
            this.mfFavoritesDataGridGUIDColumn.HeaderText = "GUID";
            this.mfFavoritesDataGridGUIDColumn.Name = "mfFavoritesDataGridGUIDColumn";
            this.mfFavoritesDataGridGUIDColumn.ReadOnly = true;
            this.mfFavoritesDataGridGUIDColumn.Visible = false;
            // 
            // mfFavoritesDataGridOwnerColumn
            // 
            this.mfFavoritesDataGridOwnerColumn.HeaderText = "Owner";
            this.mfFavoritesDataGridOwnerColumn.Name = "mfFavoritesDataGridOwnerColumn";
            this.mfFavoritesDataGridOwnerColumn.ReadOnly = true;
            // 
            // mfFavoritesDataGridFilenameColumn
            // 
            this.mfFavoritesDataGridFilenameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.mfFavoritesDataGridFilenameColumn.HeaderText = "Filename";
            this.mfFavoritesDataGridFilenameColumn.Name = "mfFavoritesDataGridFilenameColumn";
            this.mfFavoritesDataGridFilenameColumn.ReadOnly = true;
            // 
            // mfFavoritesDataGridFilesizeColumn
            // 
            this.mfFavoritesDataGridFilesizeColumn.HeaderText = "Filesize";
            this.mfFavoritesDataGridFilesizeColumn.Name = "mfFavoritesDataGridFilesizeColumn";
            this.mfFavoritesDataGridFilesizeColumn.ReadOnly = true;
            // 
            // mfFavoritesDataGridAccessColumn
            // 
            this.mfFavoritesDataGridAccessColumn.HeaderText = "Access";
            this.mfFavoritesDataGridAccessColumn.Name = "mfFavoritesDataGridAccessColumn";
            this.mfFavoritesDataGridAccessColumn.ReadOnly = true;
            this.mfFavoritesDataGridAccessColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.mfFavoritesDataGridAccessColumn.Width = 70;
            // 
            // mfFilesContextMenu
            // 
            this.mfFilesContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadToolStripMenuItem,
            this.requestAccessToolStripMenuItem});
            this.mfFilesContextMenu.Name = "mfContextMenu";
            this.mfFilesContextMenu.Size = new System.Drawing.Size(156, 48);
            this.mfFilesContextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.FileContextMenu_ItemClicked);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.downloadToolStripMenuItem.Text = "Download";
            // 
            // requestAccessToolStripMenuItem
            // 
            this.requestAccessToolStripMenuItem.Name = "requestAccessToolStripMenuItem";
            this.requestAccessToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.requestAccessToolStripMenuItem.Text = "Request Access";
            // 
            // mfMainDataGrid
            // 
            this.mfMainDataGrid.AllowUserToAddRows = false;
            this.mfMainDataGrid.AllowUserToDeleteRows = false;
            this.mfMainDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mfMainDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mfMainDataGridGUIDColumn,
            this.mfMainDataGridOwnerColumn,
            this.mfMainDataGridFilenameColumn,
            this.mfMainDataGridFilesizeColumn,
            this.mfMainDataGridAccessColumn});
            this.mfMainDataGrid.ContextMenuStrip = this.mfFilesContextMenu;
            this.mfMainDataGrid.Location = new System.Drawing.Point(3, 3);
            this.mfMainDataGrid.Name = "mfMainDataGrid";
            this.mfMainDataGrid.ReadOnly = true;
            this.mfMainDataGrid.RowHeadersVisible = false;
            this.mfBrowserTableLayout.SetRowSpan(this.mfMainDataGrid, 2);
            this.mfMainDataGrid.RowTemplate.Height = 25;
            this.mfMainDataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mfMainDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mfMainDataGrid.Size = new System.Drawing.Size(426, 417);
            this.mfMainDataGrid.TabIndex = 0;
            // 
            // mfMainDataGridGUIDColumn
            // 
            this.mfMainDataGridGUIDColumn.HeaderText = "GUID";
            this.mfMainDataGridGUIDColumn.Name = "mfMainDataGridGUIDColumn";
            this.mfMainDataGridGUIDColumn.ReadOnly = true;
            this.mfMainDataGridGUIDColumn.Visible = false;
            // 
            // mfMainDataGridOwnerColumn
            // 
            this.mfMainDataGridOwnerColumn.HeaderText = "Owner";
            this.mfMainDataGridOwnerColumn.Name = "mfMainDataGridOwnerColumn";
            this.mfMainDataGridOwnerColumn.ReadOnly = true;
            // 
            // mfMainDataGridFilenameColumn
            // 
            this.mfMainDataGridFilenameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.mfMainDataGridFilenameColumn.HeaderText = "Filename";
            this.mfMainDataGridFilenameColumn.Name = "mfMainDataGridFilenameColumn";
            this.mfMainDataGridFilenameColumn.ReadOnly = true;
            // 
            // mfMainDataGridFilesizeColumn
            // 
            this.mfMainDataGridFilesizeColumn.HeaderText = "Filesize";
            this.mfMainDataGridFilesizeColumn.Name = "mfMainDataGridFilesizeColumn";
            this.mfMainDataGridFilesizeColumn.ReadOnly = true;
            // 
            // mfMainDataGridAccessColumn
            // 
            this.mfMainDataGridAccessColumn.HeaderText = "Access";
            this.mfMainDataGridAccessColumn.Name = "mfMainDataGridAccessColumn";
            this.mfMainDataGridAccessColumn.ReadOnly = true;
            this.mfMainDataGridAccessColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.mfMainDataGridAccessColumn.Width = 70;
            // 
            // mfOnlineUsersLabel
            // 
            this.mfOnlineUsersLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mfOnlineUsersLabel.AutoSize = true;
            this.mfOnlineUsersLabel.Location = new System.Drawing.Point(489, 5);
            this.mfOnlineUsersLabel.Name = "mfOnlineUsersLabel";
            this.mfOnlineUsersLabel.Size = new System.Drawing.Size(119, 25);
            this.mfOnlineUsersLabel.TabIndex = 1;
            this.mfOnlineUsersLabel.Text = "Online Users";
            // 
            // mfOnlineUsersList
            // 
            this.mfOnlineUsersList.ContextMenuStrip = this.mfUsersContextMenu;
            this.mfOnlineUsersList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mfOnlineUsersList.ItemHeight = 25;
            this.mfOnlineUsersList.Location = new System.Drawing.Point(435, 38);
            this.mfOnlineUsersList.Name = "mfOnlineUsersList";
            this.mfOnlineUsersList.Size = new System.Drawing.Size(228, 382);
            this.mfOnlineUsersList.TabIndex = 2;
            // 
            // mfUsersContextMenu
            // 
            this.mfUsersContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
            this.mfUsersContextMenu.Name = "mfUsersContextMenu";
            this.mfUsersContextMenu.Size = new System.Drawing.Size(181, 48);
            this.mfUsersContextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.UsersContextMenu_ItemClicked);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            // 
            // mfNotificationsLabel
            // 
            this.mfNotificationsLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mfNotificationsLabel.AutoSize = true;
            this.mfNotificationsLabel.Location = new System.Drawing.Point(489, 429);
            this.mfNotificationsLabel.Name = "mfNotificationsLabel";
            this.mfNotificationsLabel.Size = new System.Drawing.Size(119, 25);
            this.mfNotificationsLabel.TabIndex = 3;
            this.mfNotificationsLabel.Text = "Notifications";
            // 
            // mfNotificationsList
            // 
            this.mfNotificationsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mfNotificationsList.ItemHeight = 25;
            this.mfNotificationsList.Location = new System.Drawing.Point(435, 464);
            this.mfNotificationsList.Name = "mfNotificationsList";
            this.mfNotificationsList.Size = new System.Drawing.Size(228, 206);
            this.mfNotificationsList.TabIndex = 4;
            // 
            // mfFavoritesLabel
            // 
            this.mfFavoritesLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mfFavoritesLabel.AutoSize = true;
            this.mfFavoritesLabel.Location = new System.Drawing.Point(173, 429);
            this.mfFavoritesLabel.Name = "mfFavoritesLabel";
            this.mfFavoritesLabel.Size = new System.Drawing.Size(86, 25);
            this.mfFavoritesLabel.TabIndex = 5;
            this.mfFavoritesLabel.Text = "Favorites";
            // 
            // mfMyFilesTab
            // 
            this.mfMyFilesTab.Controls.Add(this.mfMyFilesTableLayout);
            this.mfMyFilesTab.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mfMyFilesTab.Location = new System.Drawing.Point(4, 32);
            this.mfMyFilesTab.Name = "mfMyFilesTab";
            this.mfMyFilesTab.Padding = new System.Windows.Forms.Padding(3);
            this.mfMyFilesTab.Size = new System.Drawing.Size(672, 679);
            this.mfMyFilesTab.TabIndex = 1;
            this.mfMyFilesTab.Text = "My Files";
            this.mfMyFilesTab.UseVisualStyleBackColor = true;
            // 
            // mfMyFilesTableLayout
            // 
            this.mfMyFilesTableLayout.ColumnCount = 4;
            this.mfMyFilesTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.mfMyFilesTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.mfMyFilesTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.mfMyFilesTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.mfMyFilesTableLayout.Controls.Add(this.mfAddNewFileLabel, 0, 2);
            this.mfMyFilesTableLayout.Controls.Add(this.mfUsersToShareList, 3, 3);
            this.mfMyFilesTableLayout.Controls.Add(this.mfUsersSharedWithList, 1, 3);
            this.mfMyFilesTableLayout.Controls.Add(this.mfAuthorizedUsersList, 3, 1);
            this.mfMyFilesTableLayout.Controls.Add(this.mfAuthorizedUsersLabel, 3, 0);
            this.mfMyFilesTableLayout.Controls.Add(this.mfMyFilesDataGrid, 0, 0);
            this.mfMyFilesTableLayout.Controls.Add(this.mfUsersShareListLabel, 3, 2);
            this.mfMyFilesTableLayout.Controls.Add(this.mfSharedUsersLabel, 1, 2);
            this.mfMyFilesTableLayout.Controls.Add(this.mfUnshareButton, 2, 5);
            this.mfMyFilesTableLayout.Controls.Add(this.mfShareButton, 2, 4);
            this.mfMyFilesTableLayout.Controls.Add(this.mfSelectFileButton, 0, 3);
            this.mfMyFilesTableLayout.Controls.Add(this.mfAcceptFileButton, 0, 6);
            this.mfMyFilesTableLayout.Controls.Add(this.mfNewFileFilenameTextBox, 0, 5);
            this.mfMyFilesTableLayout.Controls.Add(this.mfNewFileFilenameLabel, 0, 4);
            this.mfMyFilesTableLayout.Location = new System.Drawing.Point(3, 3);
            this.mfMyFilesTableLayout.Name = "mfMyFilesTableLayout";
            this.mfMyFilesTableLayout.RowCount = 7;
            this.mfMyFilesTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.450705F));
            this.mfMyFilesTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.54929F));
            this.mfMyFilesTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.mfMyFilesTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.mfMyFilesTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.mfMyFilesTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.mfMyFilesTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.mfMyFilesTableLayout.Size = new System.Drawing.Size(666, 673);
            this.mfMyFilesTableLayout.TabIndex = 1;
            // 
            // mfAddNewFileLabel
            // 
            this.mfAddNewFileLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mfAddNewFileLabel.AutoSize = true;
            this.mfAddNewFileLabel.Location = new System.Drawing.Point(38, 436);
            this.mfAddNewFileLabel.Name = "mfAddNewFileLabel";
            this.mfAddNewFileLabel.Size = new System.Drawing.Size(123, 25);
            this.mfAddNewFileLabel.TabIndex = 10;
            this.mfAddNewFileLabel.Text = "Add New File";
            // 
            // mfUsersToShareList
            // 
            this.mfUsersToShareList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mfUsersToShareList.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mfUsersToShareList.ItemHeight = 21;
            this.mfUsersToShareList.Location = new System.Drawing.Point(467, 471);
            this.mfUsersToShareList.Name = "mfUsersToShareList";
            this.mfMyFilesTableLayout.SetRowSpan(this.mfUsersToShareList, 4);
            this.mfUsersToShareList.Size = new System.Drawing.Size(196, 199);
            this.mfUsersToShareList.TabIndex = 5;
            // 
            // mfUsersSharedWithList
            // 
            this.mfUsersSharedWithList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mfUsersSharedWithList.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mfUsersSharedWithList.ItemHeight = 21;
            this.mfUsersSharedWithList.Location = new System.Drawing.Point(202, 471);
            this.mfUsersSharedWithList.Name = "mfUsersSharedWithList";
            this.mfMyFilesTableLayout.SetRowSpan(this.mfUsersSharedWithList, 4);
            this.mfUsersSharedWithList.Size = new System.Drawing.Size(193, 199);
            this.mfUsersSharedWithList.TabIndex = 4;
            // 
            // mfAuthorizedUsersList
            // 
            this.mfAuthorizedUsersList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mfAuthorizedUsersList.ItemHeight = 25;
            this.mfAuthorizedUsersList.Location = new System.Drawing.Point(467, 39);
            this.mfAuthorizedUsersList.Name = "mfAuthorizedUsersList";
            this.mfAuthorizedUsersList.Size = new System.Drawing.Size(196, 388);
            this.mfAuthorizedUsersList.TabIndex = 2;
            // 
            // mfAuthorizedUsersLabel
            // 
            this.mfAuthorizedUsersLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mfAuthorizedUsersLabel.AutoSize = true;
            this.mfAuthorizedUsersLabel.Location = new System.Drawing.Point(487, 5);
            this.mfAuthorizedUsersLabel.Name = "mfAuthorizedUsersLabel";
            this.mfAuthorizedUsersLabel.Size = new System.Drawing.Size(156, 25);
            this.mfAuthorizedUsersLabel.TabIndex = 1;
            this.mfAuthorizedUsersLabel.Text = "Authorized Users";
            // 
            // mfMyFilesDataGrid
            // 
            this.mfMyFilesDataGrid.AllowUserToAddRows = false;
            this.mfMyFilesDataGrid.AllowUserToDeleteRows = false;
            this.mfMyFilesDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mfMyFilesDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mfMyFilesDataGridGUIDColumn,
            this.mfMyFilesDataGridFilenameColumn,
            this.mfMyFilesDataGridFilesizeColumn});
            this.mfMyFilesTableLayout.SetColumnSpan(this.mfMyFilesDataGrid, 3);
            this.mfMyFilesDataGrid.ContextMenuStrip = this.mfPersonalFilesContextMenu;
            this.mfMyFilesDataGrid.Location = new System.Drawing.Point(3, 3);
            this.mfMyFilesDataGrid.Name = "mfMyFilesDataGrid";
            this.mfMyFilesDataGrid.ReadOnly = true;
            this.mfMyFilesDataGrid.RowHeadersVisible = false;
            this.mfMyFilesTableLayout.SetRowSpan(this.mfMyFilesDataGrid, 2);
            this.mfMyFilesDataGrid.RowTemplate.Height = 25;
            this.mfMyFilesDataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mfMyFilesDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mfMyFilesDataGrid.Size = new System.Drawing.Size(458, 418);
            this.mfMyFilesDataGrid.TabIndex = 0;
            // 
            // mfMyFilesDataGridGUIDColumn
            // 
            this.mfMyFilesDataGridGUIDColumn.HeaderText = "GID";
            this.mfMyFilesDataGridGUIDColumn.Name = "mfMyFilesDataGridGUIDColumn";
            this.mfMyFilesDataGridGUIDColumn.ReadOnly = true;
            this.mfMyFilesDataGridGUIDColumn.Visible = false;
            // 
            // mfMyFilesDataGridFilenameColumn
            // 
            this.mfMyFilesDataGridFilenameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.mfMyFilesDataGridFilenameColumn.HeaderText = "Filename";
            this.mfMyFilesDataGridFilenameColumn.Name = "mfMyFilesDataGridFilenameColumn";
            this.mfMyFilesDataGridFilenameColumn.ReadOnly = true;
            // 
            // mfMyFilesDataGridFilesizeColumn
            // 
            this.mfMyFilesDataGridFilesizeColumn.HeaderText = "Filesize";
            this.mfMyFilesDataGridFilesizeColumn.Name = "mfMyFilesDataGridFilesizeColumn";
            this.mfMyFilesDataGridFilesizeColumn.ReadOnly = true;
            // 
            // mfPersonalFilesContextMenu
            // 
            this.mfPersonalFilesContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.shareWithToolStripMenuItem});
            this.mfPersonalFilesContextMenu.Name = "mfPersonalFilesContextMenu";
            this.mfPersonalFilesContextMenu.Size = new System.Drawing.Size(132, 48);
            this.mfPersonalFilesContextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.PersonalFileContextMenu_ItemClicked);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // shareWithToolStripMenuItem
            // 
            this.shareWithToolStripMenuItem.Name = "shareWithToolStripMenuItem";
            this.shareWithToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.shareWithToolStripMenuItem.Text = "Share With";
            // 
            // mfUsersShareListLabel
            // 
            this.mfUsersShareListLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mfUsersShareListLabel.AutoSize = true;
            this.mfUsersShareListLabel.Location = new System.Drawing.Point(519, 436);
            this.mfUsersShareListLabel.Name = "mfUsersShareListLabel";
            this.mfUsersShareListLabel.Size = new System.Drawing.Size(91, 25);
            this.mfUsersShareListLabel.TabIndex = 8;
            this.mfUsersShareListLabel.Text = "Users List";
            // 
            // mfSharedUsersLabel
            // 
            this.mfSharedUsersLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mfSharedUsersLabel.AutoSize = true;
            this.mfSharedUsersLabel.Location = new System.Drawing.Point(229, 436);
            this.mfSharedUsersLabel.Name = "mfSharedUsersLabel";
            this.mfSharedUsersLabel.Size = new System.Drawing.Size(139, 25);
            this.mfSharedUsersLabel.TabIndex = 9;
            this.mfSharedUsersLabel.Text = "Share File With";
            // 
            // mfUnshareButton
            // 
            this.mfUnshareButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.mfUnshareButton.Enabled = false;
            this.mfUnshareButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mfUnshareButton.Location = new System.Drawing.Point(401, 573);
            this.mfUnshareButton.Name = "mfUnshareButton";
            this.mfUnshareButton.Size = new System.Drawing.Size(60, 29);
            this.mfUnshareButton.TabIndex = 7;
            this.mfUnshareButton.Text = ">>";
            this.mfUnshareButton.UseVisualStyleBackColor = true;
            this.mfUnshareButton.Click += new System.EventHandler(this.NewFileUnshareButton_Click);
            // 
            // mfShareButton
            // 
            this.mfShareButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.mfShareButton.Enabled = false;
            this.mfShareButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mfShareButton.Location = new System.Drawing.Point(401, 538);
            this.mfShareButton.Name = "mfShareButton";
            this.mfShareButton.Size = new System.Drawing.Size(60, 29);
            this.mfShareButton.TabIndex = 6;
            this.mfShareButton.Text = "<<";
            this.mfShareButton.UseVisualStyleBackColor = true;
            this.mfShareButton.Click += new System.EventHandler(this.NewFileShareButton_Click);
            // 
            // mfSelectFileButton
            // 
            this.mfSelectFileButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mfSelectFileButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mfSelectFileButton.Location = new System.Drawing.Point(43, 477);
            this.mfSelectFileButton.Name = "mfSelectFileButton";
            this.mfSelectFileButton.Size = new System.Drawing.Size(113, 32);
            this.mfSelectFileButton.TabIndex = 11;
            this.mfSelectFileButton.Text = "Select File";
            this.mfSelectFileButton.UseVisualStyleBackColor = true;
            this.mfSelectFileButton.Click += new System.EventHandler(this.NewFileSelectButton_Click);
            // 
            // mfAcceptFileButton
            // 
            this.mfAcceptFileButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mfAcceptFileButton.Enabled = false;
            this.mfAcceptFileButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mfAcceptFileButton.Location = new System.Drawing.Point(43, 631);
            this.mfAcceptFileButton.Name = "mfAcceptFileButton";
            this.mfAcceptFileButton.Size = new System.Drawing.Size(113, 32);
            this.mfAcceptFileButton.TabIndex = 12;
            this.mfAcceptFileButton.Text = "Accept";
            this.mfAcceptFileButton.UseVisualStyleBackColor = true;
            this.mfAcceptFileButton.Click += new System.EventHandler(this.NewFileAcceptButton_Click);
            // 
            // mfNewFileFilenameTextBox
            // 
            this.mfNewFileFilenameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.mfNewFileFilenameTextBox.Enabled = false;
            this.mfNewFileFilenameTextBox.Location = new System.Drawing.Point(3, 573);
            this.mfNewFileFilenameTextBox.Name = "mfNewFileFilenameTextBox";
            this.mfNewFileFilenameTextBox.Size = new System.Drawing.Size(193, 33);
            this.mfNewFileFilenameTextBox.TabIndex = 13;
            // 
            // mfNewFileFilenameLabel
            // 
            this.mfNewFileFilenameLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.mfNewFileFilenameLabel.AutoSize = true;
            this.mfNewFileFilenameLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mfNewFileFilenameLabel.Location = new System.Drawing.Point(70, 553);
            this.mfNewFileFilenameLabel.Name = "mfNewFileFilenameLabel";
            this.mfNewFileFilenameLabel.Size = new System.Drawing.Size(59, 17);
            this.mfNewFileFilenameLabel.TabIndex = 14;
            this.mfNewFileFilenameLabel.Text = "Filename";
            // 
            // mfOptionsTab
            // 
            this.mfOptionsTab.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mfOptionsTab.Location = new System.Drawing.Point(4, 32);
            this.mfOptionsTab.Name = "mfOptionsTab";
            this.mfOptionsTab.Padding = new System.Windows.Forms.Padding(3);
            this.mfOptionsTab.Size = new System.Drawing.Size(672, 679);
            this.mfOptionsTab.TabIndex = 2;
            this.mfOptionsTab.Text = "Options";
            this.mfOptionsTab.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 715);
            this.Controls.Add(this.mfCentralTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zipline Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.mfCentralTabControl.ResumeLayout(false);
            this.mfBrowserTab.ResumeLayout(false);
            this.mfBrowserTableLayout.ResumeLayout(false);
            this.mfBrowserTableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mfFavoritesDataGrid)).EndInit();
            this.mfFilesContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mfMainDataGrid)).EndInit();
            this.mfUsersContextMenu.ResumeLayout(false);
            this.mfMyFilesTab.ResumeLayout(false);
            this.mfMyFilesTableLayout.ResumeLayout(false);
            this.mfMyFilesTableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mfMyFilesDataGrid)).EndInit();
            this.mfPersonalFilesContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private ContextMenuStrip mfFilesContextMenu;
        private ToolStripMenuItem downloadToolStripMenuItem;
        private ToolStripMenuItem requestAccessToolStripMenuItem;
        private ContextMenuStrip mfUsersContextMenu;
        private ToolStripMenuItem refreshToolStripMenuItem;
        private ContextMenuStrip mfPersonalFilesContextMenu;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem shareWithToolStripMenuItem;
    }
}