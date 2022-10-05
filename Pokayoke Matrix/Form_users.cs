using Pokayoke_Matrix.Models;
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
    public partial class Form_users : Form
    {
        public Form_users()
        {
            InitializeComponent();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            User user = new User();

            user.fullName = txtFullName.Text;
            user.email = txtEmail.Text;
            user.personnelID = txtPersonnelID.Text;
            user.role = cbRole.Text;
            user.created_by = 1;

            SqliteDataAccess.SaveUsers(user);

            dgvUsers.DataSource = SqliteDataAccess.LoadUsers();

        }

        private void Form_users_Load(object sender, EventArgs e)
        {
            dgvUsers.DataSource = SqliteDataAccess.LoadUsers();
        }
    }
}
