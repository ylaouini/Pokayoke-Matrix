using Pokayoke_Matrix.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokayoke_Matrix
{
    public partial class Form1 : Form
    {

        FileDialog   picLeft, picRight ,picFront, picTop, picBottom, picBack;
      
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form_users form_Config = new Form_users();
            form_Config.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Form_login form_Login = new Form_login();
            if (form_Login.ShowDialog() ==  DialogResult.OK)
            {
               // button3.Visible = true;
                //button4.Visible = true;
                lblFullName.Text = Variables.fullName;
                lblPersonnelID.Text = Variables.personnelID;
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            Form_Config form_Config = new Form_Config();    
            form_Config.ShowDialog();
        }

        private void btnAddEPN_Click(object sender, EventArgs e)
        {

            //Test input
            if ( String.IsNullOrWhiteSpace(txtEPN.Text)) { return; }

            //Test image

            if(picFront.FileName == null || picBack.FileName == null || picRight.FileName == null || picLeft.FileName == null || picTop.FileName == null || picBottom.FileName == null)
            {
                return;
            }

            // Insert EPN

            bool isConnector = true;

            Epn epn = new Epn();
            epn.name = txtEPN.Text;

            if (switchIsClip.Checked)
            {
                isConnector = false;
                if (switchIsClipSupport.Checked)
                {
                    epn.isSupportClip = 1;
                }
            }

            if (!isConnector) epn.isConnector = 0;

            int idEpn =  SqliteDataAccess.SaveEpns(epn);


            //copy pictures

            try
            {
                
                File.Copy(picFront.FileName, Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_FrontSide" + Path.GetExtension(picFront.FileName));
                File.Copy(picBack.FileName, Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_BackSide" + Path.GetExtension(picBack.FileName));
                File.Copy(picLeft.FileName, Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_LeftSide" + Path.GetExtension(picLeft.FileName));
                File.Copy(picRight.FileName, Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_RightSide" + Path.GetExtension(picRight.FileName));
                File.Copy(picTop.FileName, Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_TopSide" + Path.GetExtension(picTop.FileName));
                File.Copy(picBottom.FileName, Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_BottomSide" + Path.GetExtension(picBottom.FileName));

            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine();

                return;
            }
            

            // Insert Pictures
            Picture picture =  new Picture();
            picture.epn_id = idEpn;
          //  picture.left_side = picLeft.FileName


            FillDataGrideViewEpn();
        }

        private void FillDataGrideViewEpn()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("EPN", typeof(string));

            dgvAllEpn.ColumnHeadersHeight = 25;

            foreach (Epn epn in SqliteDataAccess.LoadEpns("SELECT * FROM tb_epns"))
            {
                dt.Rows.Add(epn.id, epn.name);
            }

            dgvAllEpn.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FillDataGrideViewEpn();
            dgvUsers.DataSource = SqliteDataAccess.LoadUsers();
            FillDataGrideViewConnectors();
            FillDataGrideViewClips();
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

        private void FillDataGrideViewClips()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("EPN", typeof(string));
            dt.Columns.Add("Count of Pokayakes", typeof(int));

            dgvClips.ColumnHeadersHeight = 25;

            foreach (Epn epn in SqliteDataAccess.LoadEpns("SELECT tb_epns.id , tb_epns.name, tb_epns.isConnector ,count(tb_pokayoke.id) AS CountOfPokayoke FROM tb_epns INNER JOIN tb_pokayoke ON tb_epns.id = tb_pokayoke.epn1_id GROUP BY tb_epns.id,tb_epns.name HAVING tb_epns.isConnector=0"))
            {
                dt.Rows.Add(epn.id, epn.name, epn.CountOfPokayoke);
            }

            dgvClips.DataSource = dt;
        }

        private void FillDataGrideViewConnectors()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("EPN", typeof(string));
            dt.Columns.Add("Count of Pokayakes", typeof(int));
            dgvConnectors.ColumnHeadersHeight = 25;

           // foreach (Epn epn in SqliteDataAccess.LoadEpns("SELECT * FROM tb_epns WHERE isConnector=1"))
            foreach (Epn epn in SqliteDataAccess.LoadEpns("SELECT tb_epns.id , tb_epns.name, tb_epns.isConnector ,count(tb_pokayoke.id) AS CountOfPokayoke FROM tb_epns INNER JOIN tb_pokayoke ON tb_epns.id = tb_pokayoke.epn1_id GROUP BY tb_epns.id,tb_epns.name HAVING tb_epns.isConnector=1"))
            {
                dt.Rows.Add(epn.id, epn.name,epn.CountOfPokayoke);
            }

            dgvConnectors.DataSource = dt;
        }

        private void picfrontSide_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            string picFrontSidePath = openFileDialog1.FileName;

            this.picFront = openFileDialog1;
            picfrontSide.ImageLocation = picFrontSidePath;

     /*       try
            {
                //File.Copy(picFrontSidePath,Application.StartupPath + "\\FilePDF\\" + txtAddEPN.Text + "_" + comboBoxSupplier.Text + "_" + txtOT.Text + ".pdf");
                Console.WriteLine(Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_FrontSide" + Path.GetExtension(openFileDialog1.FileName));
                File.Copy(picFrontSidePath, Application.StartupPath + "\\Pictures\\" + txtEPN.Text +"_FrontSide"+ Path.GetExtension(openFileDialog1.FileName));

            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show(ex.Message , "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine();

                return;
            }*/

        }

        private void switchIsClip_CheckedChanged(object sender, EventArgs e)
        {
            if (switchIsClip.Checked)
            {
                lblClipSupport.Visible = true;
                switchIsClipSupport.Visible = true;
            }
            else
            {
                lblClipSupport.Visible = false;
                switchIsClipSupport.Visible = false;
            }
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
           Console.WriteLine( this.picFront.FileName);
        }
    }
}
