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
            SuspendLayout();
            // 
            // lfLoginButton
            // 
            lfLoginButton.Location = new Point(103, 124);
            lfLoginButton.Name = "lfLoginButton";
            lfLoginButton.Size = new Size(75, 23);
            lfLoginButton.TabIndex = 0;
            lfLoginButton.Text = "Login";
            lfLoginButton.UseVisualStyleBackColor = true;
            lfLoginButton.Click += LoginButton_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(291, 298);
            Controls.Add(lfLoginButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LoginForm";
            ResumeLayout(false);
        }

        #endregion

        private Button lfLoginButton;
    }
}