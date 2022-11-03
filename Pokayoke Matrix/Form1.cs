using Pokayoke_Matrix.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokayoke_Matrix
{
    public partial class Form1 : Form
    {

        FileDialog   picLeftFile, picRightFile ,picFrontFile, picTopFile, picBottomFile, picBackFile;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);


        public Form1()
        {
            InitializeComponent();
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



        private void btnAddEPN_Click(object sender, EventArgs e)
        {

            //Test input
            if ( String.IsNullOrWhiteSpace(txtEPN.Text)) { return; }

            //Test if EPN existe
            if (SqliteDataAccess.ModelEpnExists(txtEPN.Text)) MessageBox.Show("Epn already Exist");
            

            //Test image

            if(picFrontFile == null || picBackFile == null || picRightFile == null || picLeftFile == null || picTopFile == null || picBottomFile == null)
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
/*                if (switchIsClipSupport.Checked)
                {
                    epn.isSupportClip = 1;
                }*/
            }

            if (!isConnector) epn.isConnector = 0;

            int idEpn =  SqliteDataAccess.SaveEpns(epn);


            //copy pictures

            try
            {
                
                File.Copy(picfrontSide.ImageLocation, Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_FrontSide" + Path.GetExtension(picFrontFile.FileName));
                File.Copy(picBackSide.ImageLocation, Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_BackSide" + Path.GetExtension(picBackFile.FileName));
                File.Copy(picLeftSide.ImageLocation, Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_LeftSide" + Path.GetExtension(picLeftFile.FileName));
                File.Copy(picRightSide.ImageLocation, Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_RightSide" + Path.GetExtension(picRightFile.FileName));
                File.Copy(picTopSide.ImageLocation, Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_TopSide" + Path.GetExtension(picTopFile.FileName));
                File.Copy(picBottomSide.ImageLocation, Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_BottomSide" + Path.GetExtension(picBottomFile.FileName));
                
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


            //Update Tables
            FillDataGrideViewEpn();
            FillDataGrideViewClips();
            FillDataGrideViewConnectors();


            //init input
            txtEPN.Text = "";
            picLeftSide.ImageLocation = "";
            picRightSide.ImageLocation = "";
            picTopSide.ImageLocation = "";
            picBottomSide.ImageLocation = "";
            picfrontSide.ImageLocation = "";
            picBackSide.ImageLocation = "";
            
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
            dt.Columns.Add("Type", typeof(string));

            dgvAllEpn.ColumnHeadersHeight = 25;
            dgvAllConfig.ColumnHeadersHeight = 25;

            string type = "Connector";
            foreach (Epn epn in SqliteDataAccess.LoadEpns("SELECT * FROM tb_epns"))
            {
                if (epn.isConnector != 1)
                {
                    type = "Clip";
                }
                dt.Rows.Add(epn.id, epn.name, type);
            }

            dgvAllEpn.DataSource = dt;
            dgvAllConfig.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FillDataGrideViewEpn();
            FillDataGrideViewUser();
            FillDataGrideViewConnectors();
            FillDataGrideViewClips();
            FillComboBoxProject();
        }

        private void FillComboBoxProject()
        {

            Dictionary<int, string> items = new Dictionary<int, string>();
            foreach (Project project in SqliteDataAccess.LoadProject())
            {

                items.Add(project.Id, project.name);
            }

            if (items.Count > 0)
            {
                comboBoxProjects.DisplayMember = "Value";
                comboBoxProjects.ValueMember = "Key";
                comboBoxProjects.DataSource = new BindingSource(items, null);
                comboBoxProjects.BindingContext = this.BindingContext;
            }
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
            openFileDialog1.Title = "Right Side";
            openFileDialog1.ShowDialog();
            this.picRightFile = openFileDialog1;
            picRightSide.ImageLocation = this.picRightFile.FileName;
        }

        private void picLeftSide_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Left Side";
            openFileDialog1.ShowDialog();
            this.picLeftFile = openFileDialog1;
            picLeftSide.ImageLocation = this.picLeftFile.FileName;
        }

        private void picTopSide_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Top Side";
            openFileDialog1.ShowDialog();
            this.picTopFile = openFileDialog1;
            picTopSide.ImageLocation = this.picTopFile.FileName;
        }

        private void picBottomSide_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Bottom Side";
            openFileDialog1.ShowDialog();
            this.picBottomFile = openFileDialog1;
            picBottomSide.ImageLocation = this.picBottomFile.FileName;
        }

        private void picBackSide_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Back Side";
            openFileDialog1.ShowDialog();
            this.picBackFile = openFileDialog1;
            picBackSide.ImageLocation = this.picFrontFile.FileName;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Console.WriteLine(tableLayoutPanel1.Width);
            tableLayoutPanel1.Height = tableLayoutPanel1.Width / 6;
        }

        private void dgvConnectors_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Form_pokayoke pokayoke = new Form_pokayoke();
            
            int epn_id = (int)dgvConnectors.SelectedCells[0].Value;
            foreach (Epn epn in SqliteDataAccess.LoadEpns("SELECT * FROM tb_epns INNER JOIN (tb_pictures) ON tb_epns.id = tb_pictures.epn_id WHERE tb_epns.id="+epn_id))
            {
        /*        Console.WriteLine("name : " + epn.name);
                Console.WriteLine("id : " + epn.id);
                Console.WriteLine("front_side : " + epn.front_side);
                Console.WriteLine("back_side : " + epn.back_side);
                Console.WriteLine("left_side : " + epn.left_side);
                Console.WriteLine("right_side : " + epn.right_side);
                Console.WriteLine("top_side : " + epn.top_side);
                Console.WriteLine("bottom_side : " + epn.bottom_side);*/
                pokayoke.picture.back_side = epn.back_side;
                pokayoke.picture.front_side = epn.front_side;
                pokayoke.picture.left_side = epn.front_side;
                pokayoke.picture.right_side = epn.right_side;
                pokayoke.picture.top_side = epn.top_side;
                pokayoke.picture.bottom_side = epn.bottom_side;
            }
           
            pokayoke.epn_id = epn_id;
            pokayoke.ShowDialog();
        }

        private void txtSearchCon_TextChanged(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txtSearchCon.Text))
            {
                FillDataGrideViewConnectors();
                return;
            }
            if (String.IsNullOrWhiteSpace(txtSearchCon.Text))
            {
                return;
            }
            

             DataTable dt = new DataTable();
             dt.Columns.Add("ID", typeof(int));
             dt.Columns.Add("EPN", typeof(string));
             dt.Columns.Add("Count of Pokayakes", typeof(int));
             dt.Columns.Add("Created By", typeof(string));
             dgvConnectors.ColumnHeadersHeight = 25;

             foreach (Epn epn in SqliteDataAccess.LoadEpns("SELECT tb_epns.id , tb_epns.name, tb_epns.isConnector, tb_epns.updated_at, tb_users.fullName ,count(tb_epns.id) AS CountOfPokayoke FROM tb_users INNER JOIN (tb_epns INNER JOIN tb_pokayoke ON tb_epns.id = tb_pokayoke.epn1_id or tb_epns.id = tb_pokayoke.epn2_id ) ON tb_users.id = tb_epns.created_by GROUP BY tb_epns.id,tb_epns.name HAVING tb_epns.isConnector=1 AND tb_epns.name LIKE '%"+txtSearchCon.Text+"%'"))
             {
                 dt.Rows.Add(epn.id, epn.name, epn.CountOfPokayoke, epn.fullName);
             }

             dgvConnectors.DataSource = dt;
        }

        private void txtSearchClips_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtSearchClips.Text))
            {
                FillDataGrideViewClips();
                return;
            }
            if (String.IsNullOrWhiteSpace(txtSearchClips.Text))
            {
                return;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("EPN", typeof(string));
            dt.Columns.Add("Count of Pokayakes", typeof(int));
            dt.Columns.Add("Created By", typeof(string));
            dgvClips.ColumnHeadersHeight = 25;

            foreach (Epn epn in SqliteDataAccess.LoadEpns("SELECT tb_epns.id , tb_epns.name, tb_epns.isConnector, tb_epns.updated_at, tb_users.fullName ,count(tb_epns.id) AS CountOfPokayoke FROM tb_users INNER JOIN (tb_epns INNER JOIN tb_pokayoke ON tb_epns.id = tb_pokayoke.epn1_id or tb_epns.id = tb_pokayoke.epn2_id ) ON tb_users.id = tb_epns.created_by GROUP BY tb_epns.id,tb_epns.name HAVING tb_epns.isConnector=0 AND tb_epns.name LIKE '%" + txtSearchCon.Text + "%'"))
            {
                dt.Rows.Add(epn.id, epn.name, epn.CountOfPokayoke, epn.fullName);
            }

            dgvClips.DataSource = dt;
        }


        private void dgvAllConfig_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblEpnConfig.Text = dgvAllConfig.SelectedCells[1].Value.ToString();

            LoadPokayoke();
           
        }

        private void LoadPokayoke()
        {

            if (dgvAllConfig.Rows.Count < 1)
            {
                return;
            }
            string epn_id = dgvAllConfig.SelectedCells[0].Value.ToString();

            int project_id = ((KeyValuePair<int, string>)comboBoxProjects.SelectedItem).Key;


            DataTable dataTablePokayoke = new DataTable();
            dataTablePokayoke.Columns.Add("ID", typeof(int));
            dataTablePokayoke.Columns.Add("Name", typeof(string));

            // load pokayoke from epn2_id
            foreach (Pokayoke pokayoke in SqliteDataAccess.Loadpokayoke("SELECT * FROM tb_epns INNER JOIN tb_pokayoke ON tb_epns.id = tb_pokayoke.epn2_id WHERE tb_epns.id =" + epn_id + " AND tb_pokayoke.project_id = " + project_id))
            {
                dataTablePokayoke.Rows.Add(pokayoke.Id, pokayoke.epn1_id);
            }

            // load pokayoke from epn1_id
            foreach (Pokayoke pokayoke in SqliteDataAccess.Loadpokayoke("SELECT * FROM tb_epns INNER JOIN tb_pokayoke ON tb_epns.id = tb_pokayoke.epn1_id WHERE tb_epns.id =" + epn_id + " AND tb_pokayoke.project_id = " + project_id))
            {
                dataTablePokayoke.Rows.Add(pokayoke.Id, pokayoke.epn2_id);
            }

            //fill pokayoke table
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            for (int i = 0; i < dataTablePokayoke.Rows.Count; i++)
            {

                foreach (Epn epn in SqliteDataAccess.LoadEpns("SELECT id, name FROM tb_epns where id =" + dataTablePokayoke.Rows[i][1].ToString()))
                {
                    dataTable.Rows.Add(epn.id, epn.name);
                }
            }

            dgvPokayoke.DataSource = dataTable;

            //fill not pokayoke table

            int isConnector = 1;
            if (dgvAllConfig.SelectedCells[2].Value.ToString() != "Connector")
            {
                isConnector = 0;
            }
            DataTable datatabelNotPokayoke = new DataTable();
            datatabelNotPokayoke.Columns.Add("ID", typeof(int));
            datatabelNotPokayoke.Columns.Add("Name", typeof(string));


            foreach (Epn epn in SqliteDataAccess.LoadEpns("SELECT tb_epns.id, tb_epns.name FROM tb_epns WHERE tb_epns.id NOT IN( SELECT tb_pokayoke.epn1_id FROM tb_epns INNER JOIN tb_pokayoke ON tb_epns.id = tb_pokayoke.epn1_id or tb_epns.id = tb_pokayoke.epn2_id WHERE tb_epns.id = "+epn_id+"   UNION SELECT tb_pokayoke.epn2_id FROM tb_epns INNER JOIN tb_pokayoke ON tb_epns.id = tb_pokayoke.epn1_id or tb_epns.id = tb_pokayoke.epn2_id WHERE tb_epns.id = "+epn_id+") AND tb_epns.id !="+ epn_id))
            {
                datatabelNotPokayoke.Rows.Add(epn.id, epn.name);
            }

            dgvNotPokayoke.DataSource = datatabelNotPokayoke;
        }

        private void btnAddPokayoke_Click(object sender, EventArgs e)
        {
            if(dgvNotPokayoke.Rows.Count < 1) return;

            int epn_1 = (int)dgvAllConfig.SelectedCells[0].Value;
            int epn_2 = (int)dgvNotPokayoke.SelectedCells[0].Value;
            int project_id = ((KeyValuePair<int, string>)comboBoxProjects.SelectedItem).Key;

            Pokayoke pokayoke = new Pokayoke();
          /*  Review review = new Review();*/
            pokayoke.epn1_id = epn_1;
            pokayoke.epn2_id = epn_2;
            pokayoke.project_id = project_id;

            SqliteDataAccess.SavePokayoke(pokayoke);

            /*review.pokayoke_id = SqliteDataAccess.SavePokayoke(pokayoke);
            SqliteDataAccess.SaveReview(review);*/

            FillDataGrideViewConnectors();
            FillDataGrideViewClips();
            LoadPokayoke();
        }

        private void comboBoxProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPokayoke();
        }

        private void btnRemovePokayoke_Click(object sender, EventArgs e)
        {
            if (dgvPokayoke.Rows.Count < 1) return;
            SqliteDataAccess.DeletePokayoke((int)dgvAllConfig.SelectedCells[0].Value, (int)dgvPokayoke.SelectedCells[0].Value);

            FillDataGrideViewConnectors();
            FillDataGrideViewClips();
            LoadPokayoke();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {

            if(WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
                btnMaximize.Image = global::Pokayoke_Matrix.Properties.Resources.maximize;
            }
            else
            {
                WindowState = FormWindowState.Maximized;
                btnMaximize.Image = global::Pokayoke_Matrix.Properties.Resources.minimize__4_;
                
            }
            
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void txtSearchEPN_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtSearchCon.Text))
            {
                FillDataGrideViewConnectors();
                return;
            }
            if (String.IsNullOrWhiteSpace(txtSearchCon.Text))
            {
                return;
            }


            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("EPN", typeof(string));
            dt.Columns.Add("Count of Pokayakes", typeof(int));
            dt.Columns.Add("Created By", typeof(string));
            dgvConnectors.ColumnHeadersHeight = 25;

            foreach (Epn epn in SqliteDataAccess.LoadEpns("SELECT tb_epns.id , tb_epns.name, tb_epns.isConnector, tb_epns.updated_at, tb_users.fullName ,count(tb_epns.id) AS CountOfPokayoke FROM tb_users INNER JOIN (tb_epns INNER JOIN tb_pokayoke ON tb_epns.id = tb_pokayoke.epn1_id or tb_epns.id = tb_pokayoke.epn2_id ) ON tb_users.id = tb_epns.created_by GROUP BY tb_epns.id,tb_epns.name HAVING tb_epns.isConnector=1 AND tb_epns.name LIKE '%" + txtSearchCon.Text + "%'"))
            {
                dt.Rows.Add(epn.id, epn.name, epn.CountOfPokayoke, epn.fullName);
            }

            dgvConnectors.DataSource = dt;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabHome"];
        }

        private void btnConnectors_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabConnectors"];
        }

        private void btnClips_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabClips"];
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabUsers"];
        }

        private void btnEpns_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabEpn"];
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabConfig"];
        }

        private void gunaPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void FillDataGrideViewClips()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("EPN", typeof(string));
            dt.Columns.Add("Count of Pokayakes", typeof(int));
            dt.Columns.Add("Created By", typeof(string));

            
            dgvClips.ColumnHeadersHeight = 25;

            foreach (Epn epn in SqliteDataAccess.LoadEpns("SELECT tb_epns.id, tb_epns.name, tb_users.fullName,count(tb_pokayoke.id) as CountOfPokayoke FROM tb_epns INNER JOIN tb_users LEFT JOIN tb_pokayoke ON tb_epns.id = tb_pokayoke.epn1_id or tb_epns.id = tb_pokayoke.epn2_id GROUP BY tb_epns.id  HAVING tb_epns.isConnector = 0"))
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
            dt.Columns.Add("Created By", typeof(string));
            dgvConnectors.ColumnHeadersHeight = 25;

             foreach (Epn epn in SqliteDataAccess.LoadEpns("SELECT tb_epns.id, tb_epns.name, tb_users.fullName,count(tb_pokayoke.id) as CountOfPokayoke FROM tb_epns INNER JOIN tb_users LEFT JOIN tb_pokayoke ON tb_epns.id = tb_pokayoke.epn1_id or tb_epns.id = tb_pokayoke.epn2_id GROUP BY tb_epns.id  HAVING tb_epns.isConnector = 1"))
             {
                 dt.Rows.Add(epn.id, epn.name, epn.CountOfPokayoke, epn.fullName);
             }

            dgvConnectors.DataSource = dt;

        }

        private void picfrontSide_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Front Side";
            openFileDialog1.ShowDialog();
            this.picFrontFile = openFileDialog1;
            picfrontSide.ImageLocation = this.picFrontFile.FileName;
        }



        private void gunaButton1_Click(object sender, EventArgs e)
        {
           Console.WriteLine( this.picFrontFile.FileName);
        }
    }
}
