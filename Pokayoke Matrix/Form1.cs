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

        FileDialog   picLeftFile, picRightFile ,picFrontFile, picTopFile, picBottomFile, picBackFile;
      
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

            if(picFrontFile.FileName == null || picBackFile.FileName == null || picRightFile.FileName == null || picLeftFile.FileName == null || picTopFile.FileName == null || picBottomFile.FileName == null)
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
                
                File.Copy(picFrontFile.FileName, Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_FrontSide" + Path.GetExtension(picFrontFile.FileName));
                File.Copy(picBackFile.FileName, Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_BackSide" + Path.GetExtension(picBackFile.FileName));
                File.Copy(picLeftFile.FileName, Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_LeftSide" + Path.GetExtension(picLeftFile.FileName));
                File.Copy(picRightFile.FileName, Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_RightSide" + Path.GetExtension(picRightFile.FileName));
                File.Copy(picTopFile.FileName, Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_TopSide" + Path.GetExtension(picTopFile.FileName));
                File.Copy(picBottomFile.FileName, Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_BottomSide" + Path.GetExtension(picBottomFile.FileName));
                
            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            

            // Insert Pictures
            Picture picture =  new Picture();
            picture.epn_id = idEpn;

            picture.front_side = Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_FrontSide" + Path.GetExtension(picFrontFile.FileName);
            picture.back_side = Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_BackSide" + Path.GetExtension(picBackFile.FileName);
            picture.left_side = Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_LeftSide" + Path.GetExtension(picLeftFile.FileName);
            picture.right_side = Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_RightSide" + Path.GetExtension(picRightFile.FileName);
            picture.top_side = Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_TopSide" + Path.GetExtension(picTopFile.FileName);
            picture.bottom_side = Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_BottomSide" + Path.GetExtension(picBottomFile.FileName);

            SqliteDataAccess.SaveEpnsPictures(picture);

            FillDataGrideViewEpn();
        }

        private void FillDataGrideViewUser()
        {

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Personnel ID");
            dataTable.Columns.Add("Role");
            dataTable.Columns.Add("Email");
            dataTable.Columns.Add("Created By");
            dataTable.Columns.Add("Created At");

            dgvUsers.ColumnHeadersHeight = 25;

            foreach (User user in SqliteDataAccess.LoadUsers())
            {
                dataTable.Rows.Add(user.id, user.fullName, user.personnelID, user.role, user.email, user.created_by, user.created_at);
            }
            dgvUsers.DataSource = dataTable;
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
            FillDataGrideViewUser();
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
            user.created_by = Variables.id;

            SqliteDataAccess.SaveUsers(user);

            FillDataGrideViewUser();
        }

        private void picRightSide_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            this.picRightFile = openFileDialog1;
            picRightSide.ImageLocation = this.picRightFile.FileName;
        }

        private void picLeftSide_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            this.picLeftFile = openFileDialog1;
            picLeftSide.ImageLocation = this.picLeftFile.FileName;
        }

        private void picTopSide_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            this.picTopFile = openFileDialog1;
            picTopSide.ImageLocation = this.picTopFile.FileName;
        }

        private void picBottomSide_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            this.picBottomFile = openFileDialog1;
            picBottomSide.ImageLocation = this.picBottomFile.FileName;
        }

        private void picBackSide_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            this.picBackFile = openFileDialog1;
            picBackSide.ImageLocation = this.picFrontFile.FileName;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Console.WriteLine(tableLayoutPanel1.Width);
            tableLayoutPanel1.Height = tableLayoutPanel1.Width / 6;
        }


        private void FillDataGrideViewClips()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("EPN", typeof(string));
            dt.Columns.Add("Count of Pokayakes", typeof(int));

            dgvClips.ColumnHeadersHeight = 25;

            foreach (Epn epn in SqliteDataAccess.LoadEpns("SELECT tb_epns.id , tb_epns.name, tb_epns.isConnector ,count(tb_epns.id) AS CountOfPokayoke FROM tb_epns INNER JOIN tb_pokayoke ON tb_epns.id = tb_pokayoke.epn1_id or tb_epns.id = tb_pokayoke.epn2_id   GROUP BY tb_epns.id,tb_epns.name HAVING tb_epns.isConnector=0"))
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
             foreach (Epn epn in SqliteDataAccess.LoadEpns("SELECT tb_epns.id , tb_epns.name, tb_epns.isConnector ,count(tb_epns.id) AS CountOfPokayoke FROM tb_epns INNER JOIN tb_pokayoke ON tb_epns.id = tb_pokayoke.epn1_id or tb_epns.id = tb_pokayoke.epn2_id   GROUP BY tb_epns.id,tb_epns.name HAVING tb_epns.isConnector=1"))
             {
                 dt.Rows.Add(epn.id, epn.name,epn.CountOfPokayoke);
             }

            dgvConnectors.DataSource = dt;

        }

        private void picfrontSide_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            this.picFrontFile = openFileDialog1;
            picfrontSide.ImageLocation = this.picFrontFile.FileName;
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
           Console.WriteLine( this.picFrontFile.FileName);
        }
    }
}
