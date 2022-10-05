using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokayoke_Matrix
{
    public partial class Form_login : Form
    {
        public Form_login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtPersonnelID.Text == string.Empty || txtPassword.Text == string.Empty)
            {
                lblError.Visible = true;
                return;
            }

            if ( SqliteDataAccess.Login(txtPersonnelID.Text,txtPassword.Text))
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
