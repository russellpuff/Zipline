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
            lfLoginButton = new Button();
            lfUsernameTextBox = new TextBox();
            lfUsernameLabel = new Label();
            lfPasswordLabel = new Label();
            lfPasswordTextBox = new TextBox();
            SuspendLayout();
            // 
            // lfLoginButton
            // 
            lfLoginButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lfLoginButton.Location = new Point(88, 152);
            lfLoginButton.Name = "lfLoginButton";
            lfLoginButton.Size = new Size(99, 32);
            lfLoginButton.TabIndex = 0;
            lfLoginButton.Text = "Login";
            lfLoginButton.UseVisualStyleBackColor = true;
            lfLoginButton.Click += LoginButton_Click;
            // 
            // lfUsernameTextBox
            // 
            lfUsernameTextBox.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            lfUsernameTextBox.Location = new Point(12, 33);
            lfUsernameTextBox.MaxLength = 20;
            lfUsernameTextBox.Name = "lfUsernameTextBox";
            lfUsernameTextBox.Size = new Size(258, 33);
            lfUsernameTextBox.TabIndex = 1;
            lfUsernameTextBox.TextChanged += UsernameTextBox_TextChanged;
            // 
            // lfUsernameLabel
            // 
            lfUsernameLabel.AutoSize = true;
            lfUsernameLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lfUsernameLabel.Location = new Point(12, 9);
            lfUsernameLabel.Name = "lfUsernameLabel";
            lfUsernameLabel.Size = new Size(81, 21);
            lfUsernameLabel.TabIndex = 2;
            lfUsernameLabel.Text = "Username";
            // 
            // lfPasswordLabel
            // 
            lfPasswordLabel.AutoSize = true;
            lfPasswordLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lfPasswordLabel.Location = new Point(12, 78);
            lfPasswordLabel.Name = "lfPasswordLabel";
            lfPasswordLabel.Size = new Size(76, 21);
            lfPasswordLabel.TabIndex = 3;
            lfPasswordLabel.Text = "Password";
            // 
            // lfPasswordTextBox
            // 
            lfPasswordTextBox.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            lfPasswordTextBox.Location = new Point(12, 102);
            lfPasswordTextBox.Name = "lfPasswordTextBox";
            lfPasswordTextBox.PasswordChar = '*';
            lfPasswordTextBox.Size = new Size(258, 33);
            lfPasswordTextBox.TabIndex = 4;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(286, 193);
            Controls.Add(lfPasswordTextBox);
            Controls.Add(lfPasswordLabel);
            Controls.Add(lfUsernameLabel);
            Controls.Add(lfUsernameTextBox);
            Controls.Add(lfLoginButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Zipline Client Login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button lfLoginButton;
        private TextBox lfUsernameTextBox;
        private Label lfUsernameLabel;
        private Label lfPasswordLabel;
        private TextBox lfPasswordTextBox;
    }
}