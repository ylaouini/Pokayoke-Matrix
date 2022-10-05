namespace Pokayoke_Matrix
{
    partial class Form_login
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
            this.txtPersonnelID = new Guna.UI.WinForms.GunaTextBox();
            this.txtPassword = new Guna.UI.WinForms.GunaTextBox();
            this.gunaLabel1 = new Guna.UI.WinForms.GunaLabel();
            this.gunaLabel2 = new Guna.UI.WinForms.GunaLabel();
            this.btnLogin = new Guna.UI.WinForms.GunaButton();
            this.lblError = new Guna.UI.WinForms.GunaLabel();
            this.SuspendLayout();
            // 
            // txtPersonnelID
            // 
            this.txtPersonnelID.BaseColor = System.Drawing.Color.White;
            this.txtPersonnelID.BorderColor = System.Drawing.Color.Silver;
            this.txtPersonnelID.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPersonnelID.FocusedBaseColor = System.Drawing.Color.White;
            this.txtPersonnelID.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtPersonnelID.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txtPersonnelID.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPersonnelID.Location = new System.Drawing.Point(27, 57);
            this.txtPersonnelID.Name = "txtPersonnelID";
            this.txtPersonnelID.PasswordChar = '\0';
            this.txtPersonnelID.SelectedText = "";
            this.txtPersonnelID.Size = new System.Drawing.Size(245, 32);
            this.txtPersonnelID.TabIndex = 1;
            this.txtPersonnelID.Text = "651";
            // 
            // txtPassword
            // 
            this.txtPassword.BaseColor = System.Drawing.Color.White;
            this.txtPassword.BorderColor = System.Drawing.Color.Silver;
            this.txtPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPassword.FocusedBaseColor = System.Drawing.Color.White;
            this.txtPassword.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtPassword.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPassword.Location = new System.Drawing.Point(27, 132);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '\0';
            this.txtPassword.SelectedText = "";
            this.txtPassword.Size = new System.Drawing.Size(245, 32);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.Text = "password";
            // 
            // gunaLabel1
            // 
            this.gunaLabel1.AutoSize = true;
            this.gunaLabel1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaLabel1.Location = new System.Drawing.Point(23, 34);
            this.gunaLabel1.Name = "gunaLabel1";
            this.gunaLabel1.Size = new System.Drawing.Size(91, 20);
            this.gunaLabel1.TabIndex = 3;
            this.gunaLabel1.Text = "Personnel ID";
            // 
            // gunaLabel2
            // 
            this.gunaLabel2.AutoSize = true;
            this.gunaLabel2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaLabel2.Location = new System.Drawing.Point(23, 109);
            this.gunaLabel2.Name = "gunaLabel2";
            this.gunaLabel2.Size = new System.Drawing.Size(70, 20);
            this.gunaLabel2.TabIndex = 4;
            this.gunaLabel2.Text = "Password";
            // 
            // btnLogin
            // 
            this.btnLogin.AnimationHoverSpeed = 0.07F;
            this.btnLogin.AnimationSpeed = 0.03F;
            this.btnLogin.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.btnLogin.BorderColor = System.Drawing.Color.Black;
            this.btnLogin.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnLogin.FocusedColor = System.Drawing.Color.Empty;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Image = null;
            this.btnLogin.ImageSize = new System.Drawing.Size(20, 20);
            this.btnLogin.Location = new System.Drawing.Point(27, 205);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.btnLogin.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnLogin.OnHoverForeColor = System.Drawing.Color.White;
            this.btnLogin.OnHoverImage = null;
            this.btnLogin.OnPressedColor = System.Drawing.Color.Black;
            this.btnLogin.Size = new System.Drawing.Size(160, 42);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "Login";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(215, 215);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(41, 20);
            this.lblError.TabIndex = 6;
            this.lblError.Text = "Error";
            this.lblError.Visible = false;
            // 
            // Form_login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 285);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.gunaLabel2);
            this.Controls.Add(this.gunaLabel1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtPersonnelID);
            this.Name = "Form_login";
            this.Text = "From_login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI.WinForms.GunaTextBox txtPersonnelID;
        private Guna.UI.WinForms.GunaTextBox txtPassword;
        private Guna.UI.WinForms.GunaLabel gunaLabel1;
        private Guna.UI.WinForms.GunaLabel gunaLabel2;
        private Guna.UI.WinForms.GunaButton btnLogin;
        private Guna.UI.WinForms.GunaLabel lblError;
    }
}