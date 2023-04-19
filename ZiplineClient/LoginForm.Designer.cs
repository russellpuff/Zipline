namespace ZiplineClient
{
    partial class LoginForm
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
            lfUsernameTextBox = new TextBox();
            lfUsernameLabel = new Label();
            lfPasswordLabel = new Label();
            lfPasswordTextBox = new TextBox();
            lfMainTableLayout = new TableLayoutPanel();
            lfLoginButton = new Button();
            lfMainTableLayout.SuspendLayout();
            SuspendLayout();
            // 
            // lfUsernameTextBox
            // 
            lfMainTableLayout.SetColumnSpan(lfUsernameTextBox, 3);
            lfUsernameTextBox.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            lfUsernameTextBox.Location = new Point(3, 27);
            lfUsernameTextBox.MaxLength = 20;
            lfUsernameTextBox.Name = "lfUsernameTextBox";
            lfUsernameTextBox.Size = new Size(256, 33);
            lfUsernameTextBox.TabIndex = 1;
            lfUsernameTextBox.TextChanged += UsernameTextBox_TextChanged;
            // 
            // lfUsernameLabel
            // 
            lfUsernameLabel.AutoSize = true;
            lfMainTableLayout.SetColumnSpan(lfUsernameLabel, 3);
            lfUsernameLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lfUsernameLabel.Location = new Point(3, 0);
            lfUsernameLabel.Name = "lfUsernameLabel";
            lfUsernameLabel.Size = new Size(81, 21);
            lfUsernameLabel.TabIndex = 2;
            lfUsernameLabel.Text = "Username";
            // 
            // lfPasswordLabel
            // 
            lfPasswordLabel.AutoSize = true;
            lfMainTableLayout.SetColumnSpan(lfPasswordLabel, 3);
            lfPasswordLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lfPasswordLabel.Location = new Point(3, 64);
            lfPasswordLabel.Name = "lfPasswordLabel";
            lfPasswordLabel.Size = new Size(76, 21);
            lfPasswordLabel.TabIndex = 3;
            lfPasswordLabel.Text = "Password";
            // 
            // lfPasswordTextBox
            // 
            lfMainTableLayout.SetColumnSpan(lfPasswordTextBox, 3);
            lfPasswordTextBox.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            lfPasswordTextBox.Location = new Point(3, 91);
            lfPasswordTextBox.Name = "lfPasswordTextBox";
            lfPasswordTextBox.PasswordChar = '*';
            lfPasswordTextBox.Size = new Size(256, 33);
            lfPasswordTextBox.TabIndex = 4;
            // 
            // lfMainTableLayout
            // 
            lfMainTableLayout.ColumnCount = 3;
            lfMainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27F));
            lfMainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46F));
            lfMainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27F));
            lfMainTableLayout.Controls.Add(lfUsernameLabel, 0, 0);
            lfMainTableLayout.Controls.Add(lfPasswordTextBox, 0, 3);
            lfMainTableLayout.Controls.Add(lfUsernameTextBox, 0, 1);
            lfMainTableLayout.Controls.Add(lfPasswordLabel, 0, 2);
            lfMainTableLayout.Controls.Add(lfLoginButton, 1, 4);
            lfMainTableLayout.Location = new Point(12, 12);
            lfMainTableLayout.Name = "lfMainTableLayout";
            lfMainTableLayout.RowCount = 5;
            lfMainTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            lfMainTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            lfMainTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            lfMainTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            lfMainTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 9F));
            lfMainTableLayout.Size = new Size(262, 175);
            lfMainTableLayout.TabIndex = 5;
            // 
            // lfLoginButton
            // 
            lfLoginButton.Anchor = AnchorStyles.None;
            lfLoginButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lfLoginButton.Location = new Point(80, 135);
            lfLoginButton.Name = "lfLoginButton";
            lfLoginButton.Size = new Size(99, 32);
            lfLoginButton.TabIndex = 0;
            lfLoginButton.Text = "Login";
            lfLoginButton.UseVisualStyleBackColor = true;
            lfLoginButton.Click += LoginButton_ClickAsync;
            // 
            // LoginForm
            // 
            AcceptButton = lfLoginButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(286, 193);
            Controls.Add(lfMainTableLayout);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Zipline Client Login";
            lfMainTableLayout.ResumeLayout(false);
            lfMainTableLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TextBox lfUsernameTextBox;
        private Label lfUsernameLabel;
        private Label lfPasswordLabel;
        private TextBox lfPasswordTextBox;
        private TableLayoutPanel lfMainTableLayout;
        private Button lfLoginButton;
    }
}