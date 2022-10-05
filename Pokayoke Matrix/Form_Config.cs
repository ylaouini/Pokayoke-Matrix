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
    public partial class Form_Config : Form
    {
        public Form_Config()
        {
            InitializeComponent();
        }

        private void btnAddEPN_Click(object sender, EventArgs e)
        {
            bool isConnector = true;

            if (rbClip.Checked) isConnector = false;

            Epn epn = new Epn();
            epn.name = txtEPN.Text;

            if (!isConnector) epn.isConnector = 0;

            SqliteDataAccess.SaveEpns(epn);

            FillDataGrideViewEpn();
        }

        private void Form_Config_Load(object sender, EventArgs e)
        {
            FillDataGrideViewEpn();
        }

        private void FillDataGrideViewEpn()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("EPN", typeof(string));

            foreach (Epn epn in SqliteDataAccess.LoadEpns("SELECT * FROM tb_epns"))
            {
                dt.Rows.Add(epn.id, epn.name);
            }

            dgvAllEpn.DataSource = dt;
        }

        private void gunaLabel3_Click(object sender, EventArgs e)
        {

        }

        private void gunaLabel2_Click(object sender, EventArgs e)
        {

        }

        private void rbClip_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbConnector_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void gunaLabel1_Click(object sender, EventArgs e)
        {

        }

        private void dgvAllEpn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtEPN_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
