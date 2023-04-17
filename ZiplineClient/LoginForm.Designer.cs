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
            this.lfLoginButton = new System.Windows.Forms.Button();
            this.lfTestButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lfLoginButton
            // 
            this.lfLoginButton.Location = new System.Drawing.Point(108, 53);
            this.lfLoginButton.Name = "lfLoginButton";
            this.lfLoginButton.Size = new System.Drawing.Size(75, 23);
            this.lfLoginButton.TabIndex = 0;
            this.lfLoginButton.Text = "Login";
            this.lfLoginButton.UseVisualStyleBackColor = true;
            // 
            // lfTestButton
            // 
            this.lfTestButton.Location = new System.Drawing.Point(107, 162);
            this.lfTestButton.Name = "lfTestButton";
            this.lfTestButton.Size = new System.Drawing.Size(75, 23);
            this.lfTestButton.TabIndex = 1;
            this.lfTestButton.Text = "test";
            this.lfTestButton.UseVisualStyleBackColor = true;
            this.lfTestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 298);
            this.Controls.Add(this.lfTestButton);
            this.Controls.Add(this.lfLoginButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Button lfLoginButton;
        private Button lfTestButton;
    }
}