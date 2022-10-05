namespace Pokayoke_Matrix
{
    partial class Form_Config
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtEPN = new Guna.UI.WinForms.GunaLineTextBox();
            this.btnAddEPN = new Guna.UI.WinForms.GunaButton();
            this.gunaLabel1 = new Guna.UI.WinForms.GunaLabel();
            this.rbConnector = new Guna.UI.WinForms.GunaMediumRadioButton();
            this.rbClip = new Guna.UI.WinForms.GunaMediumRadioButton();
            this.gunaLabel2 = new Guna.UI.WinForms.GunaLabel();
            this.gunaLabel3 = new Guna.UI.WinForms.GunaLabel();
            this.dgvAllEpn = new Guna.UI.WinForms.GunaDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllEpn)).BeginInit();
            this.SuspendLayout();
            // 
            // txtEPN
            // 
            this.txtEPN.BackColor = System.Drawing.Color.White;
            this.txtEPN.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEPN.FocusedLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtEPN.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEPN.LineColor = System.Drawing.Color.Gainsboro;
            this.txtEPN.Location = new System.Drawing.Point(182, 27);
            this.txtEPN.Name = "txtEPN";
            this.txtEPN.PasswordChar = '\0';
            this.txtEPN.SelectedText = "";
            this.txtEPN.Size = new System.Drawing.Size(224, 30);
            this.txtEPN.TabIndex = 1;
            this.txtEPN.TextChanged += new System.EventHandler(this.txtEPN_TextChanged);
            // 
            // btnAddEPN
            // 
            this.btnAddEPN.AnimationHoverSpeed = 0.07F;
            this.btnAddEPN.AnimationSpeed = 0.03F;
            this.btnAddEPN.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.btnAddEPN.BorderColor = System.Drawing.Color.Black;
            this.btnAddEPN.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAddEPN.FocusedColor = System.Drawing.Color.Empty;
            this.btnAddEPN.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAddEPN.ForeColor = System.Drawing.Color.White;
            this.btnAddEPN.Image = null;
            this.btnAddEPN.ImageSize = new System.Drawing.Size(20, 20);
            this.btnAddEPN.Location = new System.Drawing.Point(478, 15);
            this.btnAddEPN.Name = "btnAddEPN";
            this.btnAddEPN.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.btnAddEPN.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnAddEPN.OnHoverForeColor = System.Drawing.Color.White;
            this.btnAddEPN.OnHoverImage = null;
            this.btnAddEPN.OnPressedColor = System.Drawing.Color.Black;
            this.btnAddEPN.Size = new System.Drawing.Size(160, 42);
            this.btnAddEPN.TabIndex = 2;
            this.btnAddEPN.Text = "Add";
            this.btnAddEPN.Click += new System.EventHandler(this.btnAddEPN_Click);
            // 
            // gunaLabel1
            // 
            this.gunaLabel1.AutoSize = true;
            this.gunaLabel1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaLabel1.Location = new System.Drawing.Point(55, 37);
            this.gunaLabel1.Name = "gunaLabel1";
            this.gunaLabel1.Size = new System.Drawing.Size(36, 20);
            this.gunaLabel1.TabIndex = 3;
            this.gunaLabel1.Text = "EPN";
            this.gunaLabel1.Click += new System.EventHandler(this.gunaLabel1_Click);
            // 
            // rbConnector
            // 
            this.rbConnector.BaseColor = System.Drawing.Color.White;
            this.rbConnector.CheckedOffColor = System.Drawing.Color.Gray;
            this.rbConnector.CheckedOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.rbConnector.FillColor = System.Drawing.Color.White;
            this.rbConnector.Location = new System.Drawing.Point(373, 103);
            this.rbConnector.Name = "rbConnector";
            this.rbConnector.Size = new System.Drawing.Size(20, 20);
            this.rbConnector.TabIndex = 5;
            this.rbConnector.CheckedChanged += new System.EventHandler(this.rbConnector_CheckedChanged);
            // 
            // rbClip
            // 
            this.rbClip.BaseColor = System.Drawing.Color.White;
            this.rbClip.CheckedOffColor = System.Drawing.Color.Gray;
            this.rbClip.CheckedOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.rbClip.FillColor = System.Drawing.Color.White;
            this.rbClip.Location = new System.Drawing.Point(182, 103);
            this.rbClip.Name = "rbClip";
            this.rbClip.Size = new System.Drawing.Size(20, 20);
            this.rbClip.TabIndex = 6;
            this.rbClip.CheckedChanged += new System.EventHandler(this.rbClip_CheckedChanged);
            // 
            // gunaLabel2
            // 
            this.gunaLabel2.AutoSize = true;
            this.gunaLabel2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaLabel2.Location = new System.Drawing.Point(408, 103);
            this.gunaLabel2.Name = "gunaLabel2";
            this.gunaLabel2.Size = new System.Drawing.Size(77, 20);
            this.gunaLabel2.TabIndex = 7;
            this.gunaLabel2.Text = "Connector";
            this.gunaLabel2.Click += new System.EventHandler(this.gunaLabel2_Click);
            // 
            // gunaLabel3
            // 
            this.gunaLabel3.AutoSize = true;
            this.gunaLabel3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaLabel3.Location = new System.Drawing.Point(128, 103);
            this.gunaLabel3.Name = "gunaLabel3";
            this.gunaLabel3.Size = new System.Drawing.Size(35, 20);
            this.gunaLabel3.TabIndex = 8;
            this.gunaLabel3.Text = "Clip";
            this.gunaLabel3.Click += new System.EventHandler(this.gunaLabel3_Click);
            // 
            // dgvAllEpn
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvAllEpn.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAllEpn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAllEpn.BackgroundColor = System.Drawing.Color.White;
            this.dgvAllEpn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAllEpn.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvAllEpn.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAllEpn.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAllEpn.ColumnHeadersHeight = 4;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAllEpn.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAllEpn.EnableHeadersVisualStyles = false;
            this.dgvAllEpn.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvAllEpn.Location = new System.Drawing.Point(12, 184);
            this.dgvAllEpn.Name = "dgvAllEpn";
            this.dgvAllEpn.RowHeadersVisible = false;
            this.dgvAllEpn.RowHeadersWidth = 51;
            this.dgvAllEpn.RowTemplate.Height = 24;
            this.dgvAllEpn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAllEpn.Size = new System.Drawing.Size(252, 395);
            this.dgvAllEpn.TabIndex = 9;
            this.dgvAllEpn.Theme = Guna.UI.WinForms.GunaDataGridViewPresetThemes.Guna;
            this.dgvAllEpn.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvAllEpn.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvAllEpn.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvAllEpn.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvAllEpn.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvAllEpn.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvAllEpn.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvAllEpn.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvAllEpn.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvAllEpn.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.dgvAllEpn.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvAllEpn.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvAllEpn.ThemeStyle.HeaderStyle.Height = 4;
            this.dgvAllEpn.ThemeStyle.ReadOnly = false;
            this.dgvAllEpn.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvAllEpn.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvAllEpn.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.dgvAllEpn.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvAllEpn.ThemeStyle.RowsStyle.Height = 24;
            this.dgvAllEpn.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvAllEpn.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvAllEpn.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAllEpn_CellContentClick);
            // 
            // Form_Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1226, 731);
            this.Controls.Add(this.dgvAllEpn);
            this.Controls.Add(this.gunaLabel3);
            this.Controls.Add(this.gunaLabel2);
            this.Controls.Add(this.rbClip);
            this.Controls.Add(this.rbConnector);
            this.Controls.Add(this.gunaLabel1);
            this.Controls.Add(this.btnAddEPN);
            this.Controls.Add(this.txtEPN);
            this.Name = "Form_Config";
            this.Text = "Form_Config";
            this.Load += new System.EventHandler(this.Form_Config_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllEpn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI.WinForms.GunaLineTextBox txtEPN;
        private Guna.UI.WinForms.GunaButton btnAddEPN;
        private Guna.UI.WinForms.GunaLabel gunaLabel1;
        private Guna.UI.WinForms.GunaMediumRadioButton rbConnector;
        private Guna.UI.WinForms.GunaMediumRadioButton rbClip;
        private Guna.UI.WinForms.GunaLabel gunaLabel2;
        private Guna.UI.WinForms.GunaLabel gunaLabel3;
        private Guna.UI.WinForms.GunaDataGridView dgvAllEpn;
    }
}