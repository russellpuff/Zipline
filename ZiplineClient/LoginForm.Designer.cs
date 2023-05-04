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
            this.lfUsernameTextBox = new System.Windows.Forms.TextBox();
            this.lfUsernameLabel = new System.Windows.Forms.Label();
            this.lfPasswordLabel = new System.Windows.Forms.Label();
            this.lfPasswordTextBox = new System.Windows.Forms.TextBox();
            this.lfMainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lfLoginButton = new System.Windows.Forms.Button();
            this.lfMainTableLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // lfUsernameTextBox
            // 
            this.lfMainTableLayout.SetColumnSpan(this.lfUsernameTextBox, 3);
            this.lfUsernameTextBox.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lfUsernameTextBox.Location = new System.Drawing.Point(3, 27);
            this.lfUsernameTextBox.MaxLength = 20;
            this.lfUsernameTextBox.Name = "lfUsernameTextBox";
            this.lfUsernameTextBox.Size = new System.Drawing.Size(256, 33);
            this.lfUsernameTextBox.TabIndex = 1;
            this.lfUsernameTextBox.TextChanged += new System.EventHandler(this.UsernameTextBox_TextChanged);
            // 
            // lfUsernameLabel
            // 
            this.lfUsernameLabel.AutoSize = true;
            this.lfMainTableLayout.SetColumnSpan(this.lfUsernameLabel, 3);
            this.lfUsernameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lfUsernameLabel.Location = new System.Drawing.Point(3, 0);
            this.lfUsernameLabel.Name = "lfUsernameLabel";
            this.lfUsernameLabel.Size = new System.Drawing.Size(81, 21);
            this.lfUsernameLabel.TabIndex = 2;
            this.lfUsernameLabel.Text = "Username";
            // 
            // lfPasswordLabel
            // 
            this.lfPasswordLabel.AutoSize = true;
            this.lfMainTableLayout.SetColumnSpan(this.lfPasswordLabel, 3);
            this.lfPasswordLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lfPasswordLabel.Location = new System.Drawing.Point(3, 64);
            this.lfPasswordLabel.Name = "lfPasswordLabel";
            this.lfPasswordLabel.Size = new System.Drawing.Size(76, 21);
            this.lfPasswordLabel.TabIndex = 3;
            this.lfPasswordLabel.Text = "Password";
            // 
            // lfPasswordTextBox
            // 
            this.lfMainTableLayout.SetColumnSpan(this.lfPasswordTextBox, 3);
            this.lfPasswordTextBox.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lfPasswordTextBox.Location = new System.Drawing.Point(3, 91);
            this.lfPasswordTextBox.Name = "lfPasswordTextBox";
            this.lfPasswordTextBox.PasswordChar = '*';
            this.lfPasswordTextBox.Size = new System.Drawing.Size(256, 33);
            this.lfPasswordTextBox.TabIndex = 4;
            this.lfPasswordTextBox.TextChanged += new System.EventHandler(this.PasswordTextBox_TextChanged);
            // 
            // lfMainTableLayout
            // 
            this.lfMainTableLayout.ColumnCount = 3;
            this.lfMainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.lfMainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46F));
            this.lfMainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.lfMainTableLayout.Controls.Add(this.lfUsernameLabel, 0, 0);
            this.lfMainTableLayout.Controls.Add(this.lfPasswordTextBox, 0, 3);
            this.lfMainTableLayout.Controls.Add(this.lfUsernameTextBox, 0, 1);
            this.lfMainTableLayout.Controls.Add(this.lfPasswordLabel, 0, 2);
            this.lfMainTableLayout.Controls.Add(this.lfLoginButton, 1, 4);
            this.lfMainTableLayout.Location = new System.Drawing.Point(12, 12);
            this.lfMainTableLayout.Name = "lfMainTableLayout";
            this.lfMainTableLayout.RowCount = 5;
            this.lfMainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.lfMainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.lfMainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.lfMainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.lfMainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.lfMainTableLayout.Size = new System.Drawing.Size(262, 175);
            this.lfMainTableLayout.TabIndex = 5;
            // 
            // lfLoginButton
            // 
            this.lfLoginButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lfLoginButton.Enabled = false;
            this.lfLoginButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lfLoginButton.Location = new System.Drawing.Point(80, 135);
            this.lfLoginButton.Name = "lfLoginButton";
            this.lfLoginButton.Size = new System.Drawing.Size(99, 32);
            this.lfLoginButton.TabIndex = 0;
            this.lfLoginButton.Text = "Login";
            this.lfLoginButton.UseVisualStyleBackColor = true;
            this.lfLoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.lfLoginButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 193);
            this.Controls.Add(this.lfMainTableLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zipline Client Login";
            this.lfMainTableLayout.ResumeLayout(false);
            this.lfMainTableLayout.PerformLayout();
            this.ResumeLayout(false);

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