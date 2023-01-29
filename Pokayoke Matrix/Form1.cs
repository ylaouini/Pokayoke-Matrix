using Guna.UI.WinForms;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Pokayoke_Matrix
{
    public partial class Form1 : Form
    {

      //  FileDialog   picLeftFile, picRightFile ,picFrontFile, picTopFile, picBottomFile, picBackFile;

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

                lblFullName.Text = Variables.fullName;
                lblPersonnelID.Text = Variables.personnelID;

                btnUsers.Visible = true;
                btnConfig.Visible = true;
                btnEpns.Visible = true;

                btnLogin.Visible = false;
            }

        }



        private void btnAddEPN_Click(object sender, EventArgs e)
        {

            //Test input
            if ( String.IsNullOrWhiteSpace(txtEPN.Text)) { return; }

            //Test image

            if (picFrontSide.ImageLocation == null || picBottomSide.ImageLocation == null || picTopSide.ImageLocation == null)
            {
                return;
            }


           

                //Test if EPN existe
                if (SqliteDataAccess.ModelEpnExists(txtEPN.Text)) MessageBox.Show("Epn already Exist");

                // Insert EPN

                bool isConnector = true;

                Epn epn = new Epn();
                epn.name = txtEPN.Text;

                if (switchIsClip.Checked)
                {
                    isConnector = false;
                }

                if (!isConnector) epn.isConnector = 0;

                int idEpn = SqliteDataAccess.SaveEpns(epn);

                //copy pictures

                try
                {
                    File.Copy(picTopSide.ImageLocation, Application.StartupPath + "\\Pictures\\" + idEpn + "_TopSide" + Path.GetExtension(picTopSide.ImageLocation),true);
                    File.Copy(picBottomSide.ImageLocation, Application.StartupPath + "\\Pictures\\" + idEpn + "_BottomSide" + Path.GetExtension(picBottomSide.ImageLocation),true);
                    File.Copy(picFrontSide.ImageLocation, Application.StartupPath + "\\Pictures\\" + idEpn + "_FrontSide" + Path.GetExtension(picFrontSide.ImageLocation),true);

                    if (picBackSide.ImageLocation != null)
                    {
                        File.Copy(picBackSide.ImageLocation, Application.StartupPath + "\\Pictures\\" + idEpn + "_BackSide" + Path.GetExtension(picBackSide.ImageLocation),true);
                    }

                    if (picLeftSide.ImageLocation != null)
                    {
                        File.Copy(picLeftSide.ImageLocation, Application.StartupPath + "\\Pictures\\" + idEpn + "_LeftSide" + Path.GetExtension(picLeftSide.ImageLocation),true);
                    }

                    if (picRightSide.ImageLocation != null)
                    {
                        File.Copy(picRightSide.ImageLocation, Application.StartupPath + "\\Pictures\\" + idEpn + "_RightSide" + Path.GetExtension(picRightSide.ImageLocation),true);
                    }

                }
                catch (System.IO.IOException ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }


                // Insert Pictures
                Picture picture = new Picture();
                picture.epn_id = idEpn;

                picture.top_side = txtEPN.Text + "_TopSide" + Path.GetExtension(picTopSide.ImageLocation);
                picture.bottom_side = txtEPN.Text + "_BottomSide" + Path.GetExtension(picBottomSide.ImageLocation);
                picture.front_side = txtEPN.Text + "_FrontSide" + Path.GetExtension(picFrontSide.ImageLocation);

                if (picBackSide.ImageLocation != null)
                {
                    picture.back_side = txtEPN.Text + "_BackSide" + Path.GetExtension(picBackSide.ImageLocation);
                }

                if (picLeftSide.ImageLocation != null)
                {
                    picture.left_side = txtEPN.Text + "_LeftSide" + Path.GetExtension(picLeftSide.ImageLocation);
                }

                if (picRightSide.ImageLocation != null)
                {
                    picture.right_side = txtEPN.Text + "_RightSide" + Path.GetExtension(picRightSide.ImageLocation);
                }

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
            picFrontSide.ImageLocation = "";
            picBackSide.ImageLocation = "";

            /*picFrontFile = null;
            picBackFile = null;
            picRightFile = null;
            picLeftFile = null;
            picTopFile = null;
            picBottomFile = null;*/


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

            
            foreach (Epn epn in SqliteDataAccess.LoadEpns("SELECT * FROM tb_epns"))
            {
                string type = "Connector";
                Console.WriteLine(epn.isConnector);
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
           
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Picture (*.jpeg, .jpg, .png)|*.jpeg;*.jpg;*.png",
            };
            openFileDialog.Title = "Right Side";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                picRightSide.ImageLocation = openFileDialog.FileName;
            }
        }

        private void picLeftSide_Click(object sender, EventArgs e)
        {
           
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Picture (*.jpeg, .jpg, .png)|*.jpeg;*.jpg;*.png",
            };
            openFileDialog.Title = "Left Side";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                picLeftSide.ImageLocation = openFileDialog.FileName;
            }
        }

        private void picTopSide_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Picture (*.jpeg, .jpg, .png)|*.jpeg;*.jpg;*.png",
            };
            openFileDialog.Title = "Top Side";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                picTopSide.ImageLocation = openFileDialog.FileName;
            }
        }

        private void picBottomSide_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Picture (*.jpeg, .jpg, .png)|*.jpeg;*.jpg;*.png",
            };
            openFileDialog.Title = "Bottom Side";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                picBottomSide.ImageLocation = openFileDialog.FileName;
            }
        }

        private void picBackSide_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Picture (*.jpeg, .jpg, .png)|*.jpeg;*.jpg;*.png",
            };
            openFileDialog.Title = "Back Side";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                picBackSide.ImageLocation = openFileDialog.FileName;
            }
               
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            tableLayoutPanel1.Height = tableLayoutPanel1.Width / 6;
        }

        private void dgvConnectors_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Form_pokayoke pokayoke = new Form_pokayoke();
            
            int epn_id = (int)dgvConnectors.SelectedCells[0].Value;
            foreach (Epn epn in SqliteDataAccess.LoadEpns("SELECT * FROM tb_epns INNER JOIN (tb_pictures) ON tb_epns.id = tb_pictures.epn_id WHERE tb_epns.id="+epn_id))
            {
     
                pokayoke.picture.back_side = Application.StartupPath + "\\Pictures\\" + epn.back_side;
                pokayoke.picture.front_side = Application.StartupPath + "\\Pictures\\" + epn.front_side;
                pokayoke.picture.left_side = Application.StartupPath + "\\Pictures\\" + epn.left_side;
                pokayoke.picture.right_side = Application.StartupPath + "\\Pictures\\" + epn.right_side;
                pokayoke.picture.top_side = Application.StartupPath + "\\Pictures\\" + epn.top_side;
                pokayoke.picture.bottom_side = Application.StartupPath + "\\Pictures\\" + epn.bottom_side;
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

            if (comboBoxProjects.SelectedItem == null)
            {
                MessageBox.Show("Please add at least one project");
                return;
            }
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


            foreach (Epn epn in SqliteDataAccess.LoadEpns("SELECT tb_epns.id, tb_epns.name FROM tb_epns WHERE tb_epns.isConnector = "+isConnector+" and tb_epns.id NOT IN( SELECT tb_pokayoke.epn1_id FROM tb_epns INNER JOIN tb_pokayoke ON tb_epns.id = tb_pokayoke.epn1_id or tb_epns.id = tb_pokayoke.epn2_id WHERE tb_epns.id = "+epn_id+ " and tb_pokayoke.project_id = "+project_id+"  UNION SELECT tb_pokayoke.epn2_id FROM tb_epns INNER JOIN tb_pokayoke ON tb_epns.id = tb_pokayoke.epn1_id or tb_epns.id = tb_pokayoke.epn2_id WHERE tb_epns.id = " + epn_id+ " and tb_pokayoke.project_id = "+project_id+") AND tb_epns.id !=" + epn_id))
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
            int project_id = ((KeyValuePair<int, string>)comboBoxProjects.SelectedItem).Key;
            if (dgvPokayoke.Rows.Count < 1) return;
            SqliteDataAccess.DeletePokayoke((int)dgvAllConfig.SelectedCells[0].Value, (int)dgvPokayoke.SelectedCells[0].Value,project_id);

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
            if (String.IsNullOrEmpty(txtSearchEPN.Text))
            {
                FillDataGrideViewEpn();
                return;
            }
            if (String.IsNullOrWhiteSpace(txtSearchEPN.Text))
            {
                return;
            }


            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("EPN", typeof(string));
            dt.Columns.Add("Type", typeof(string));
          //  dgvConnectors.ColumnHeadersHeight = 25;

            foreach (Epn epn in SqliteDataAccess.LoadEpns("SELECT tb_epns.id , tb_epns.name, tb_epns.isConnector FROM tb_epns WHERE tb_epns.name LIKE '%" + txtSearchEPN.Text + "%'"))
            {
                string type = "Connector";
                if (epn.isConnector == 0) type = "Clip";
  
                dt.Rows.Add(epn.id, epn.name, type);
            }

            dgvAllConfig.DataSource = dt;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabHome"];
            lblTitleForm.Text = "Home";
        }

        private void btnConnectors_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabConnectors"];
            lblTitleForm.Text = "Connectors";
        }

        private void btnClips_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabClips"];
            lblTitleForm.Text = "Clips";
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabUsers"];
        }

        private void btnEpns_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabEpn"];
            lblTitleForm.Text = "Epns";
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabConfig"];
            lblTitleForm.Text = "Configuration";
        }

        private void btnExportClips_Click(object sender, EventArgs e)
        {

        }

       


        private void dgvClips_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Form_pokayoke pokayoke = new Form_pokayoke();

            int epn_id = (int)dgvClips.SelectedCells[0].Value;
            foreach (Epn epn in SqliteDataAccess.LoadEpns("SELECT * FROM tb_epns INNER JOIN (tb_pictures) ON tb_epns.id = tb_pictures.epn_id WHERE tb_epns.id=" + epn_id))
            {

                pokayoke.picture.back_side = epn.back_side;
                pokayoke.picture.front_side = epn.front_side;
                pokayoke.picture.left_side = epn.left_side;
                pokayoke.picture.right_side = epn.right_side;
                pokayoke.picture.top_side = epn.top_side;
                pokayoke.picture.bottom_side = epn.bottom_side;
            }

            pokayoke.epn_id = epn_id;
            pokayoke.ShowDialog();
        }



        private void dgvAllEpn_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdateEPN.Visible = true;
            btnAddEPN.Visible = false;

            //var path pictures

            string pathBack, pathFront, pathRight, pathLeft, pathTop, pathBottom;

            int epn_id = (int)dgvAllEpn.SelectedCells[0].Value;
            lblEpn_id.Text = epn_id.ToString();
            txtEPN.Text = dgvAllEpn.SelectedCells[1].Value.ToString();

            switchIsClip.Checked = false;
            if ((string)dgvAllEpn.SelectedCells[2].Value == "Clip")
            {
                switchIsClip.Checked = true;
            }

            //Initial images
            /* picBackSide.Image = null;
             picFrontSide.Image = null;
             picLeftSide.Image = null;
             picRightSide.Image = null;
             picTopSide.Image = null;
             picBottomSide.Image = null;*/

            pathBack = Application.StartupPath + "\\Pictures\\" + epn_id + "_BackSide.jpg";
            pathFront = Application.StartupPath + "\\Pictures\\" + epn_id + "_FrontSide.jpg";
            pathLeft = Application.StartupPath + "\\Pictures\\" + epn_id + "_LeftSide.jpg";
            pathRight = Application.StartupPath + "\\Pictures\\" + epn_id + "_RightSide.jpg";
            pathTop = Application.StartupPath + "\\Pictures\\" + epn_id + "_TopSide.jpg";
            pathBottom = Application.StartupPath + "\\Pictures\\" + epn_id + "_BottomSide.jpg";

        /*    pathBack = Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_BackSide.jpg";
            pathFront = Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_FrontSide.jpg";
            pathLeft = Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_LeftSide.jpg";
            pathRight = Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_RightSide.jpg";
            pathTop = Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_TopSide.jpg";
            pathBottom = Application.StartupPath + "\\Pictures\\" + txtEPN.Text + "_BottomSide.jpg";*/


            if (File.Exists(pathBack))
            {
                picBackSide.ImageLocation = pathBack;
                btnTheme(btnBackPicture, picBackSide, "add");
            }
            else
            {

                btnTheme(btnBackPicture, picBackSide, "del");
            }


            if (File.Exists(pathFront)) 
            { 
                picFrontSide.ImageLocation = pathFront;
                btnTheme(btnFrontPicture, picFrontSide, "add");
            }
            else
            {
                btnTheme(btnFrontPicture, picFrontSide, "del");
            }

            if (File.Exists(pathLeft))
            {
                picLeftSide.ImageLocation = pathLeft;
                btnTheme(btnLeftPicture, picLeftSide, "add");
            }
            else
            {
                btnTheme(btnLeftPicture, picLeftSide, "del");
            }

            if (File.Exists(pathRight))
            {
                picRightSide.ImageLocation = pathRight;
                btnTheme(btnRightPicture, picRightSide, "add");
            }
            else
            {
                btnTheme(btnRightPicture, picRightSide, "del");
            }


            if (File.Exists(pathTop))
            {
                picTopSide.ImageLocation = pathTop;
                btnTheme(btnTopPicture, picTopSide, "add");
            }
            else
            {
                btnTheme(btnTopPicture, picTopSide, "del");
            }


            if (File.Exists(pathBottom))
            {
                picBottomSide.ImageLocation = pathBottom;
                btnTheme(btnBottomPicture, picBottomSide, "add");
            }
            else
            {
                btnTheme(btnBottomPicture, picBottomSide, "del");
            }

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
           // dt.Columns.Add("Created By", typeof(string));

            
            dgvClips.ColumnHeadersHeight = 25;

            foreach (Epn epn in SqliteDataAccess.LoadEpns("SELECT tb_epns.id, tb_epns.name,count(tb_pokayoke.id) as CountOfPokayoke,tb_epns.created_by FROM tb_epns LEFT JOIN tb_pokayoke ON tb_epns.id = tb_pokayoke.epn1_id or tb_epns.id = tb_pokayoke.epn2_id GROUP BY tb_epns.id  HAVING tb_epns.isConnector = 0"))
            {
                //dt.Rows.Add(epn.id, epn.name, epn.CountOfPokayoke);
                dt.Rows.Add(epn.id, epn.name);
            }

            dgvClips.DataSource = dt;
        }

        private void FillDataGrideViewConnectors()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("EPN", typeof(string));
            dt.Columns.Add("Count of Pokayakes", typeof(int));
           // dt.Columns.Add("Created By", typeof(int));
            dgvConnectors.ColumnHeadersHeight = 25;

             foreach (Epn epn in SqliteDataAccess.LoadEpns("SELECT tb_epns.id, tb_epns.name,count(tb_pokayoke.id) as CountOfPokayoke,tb_epns.created_by FROM tb_epns LEFT JOIN tb_pokayoke ON tb_epns.id = tb_pokayoke.epn1_id or tb_epns.id = tb_pokayoke.epn2_id GROUP BY tb_epns.id  HAVING tb_epns.isConnector = 1"))
             {
                // dt.Rows.Add(epn.id, epn.name, epn.CountOfPokayoke, epn.created_by);
                 dt.Rows.Add(epn.id, epn.name, epn.CountOfPokayoke);
             }

            dgvConnectors.DataSource = dt;

        }

        private void picfrontSide_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { 
            Filter = "Picture (*.jpeg, .jpg, .png)|*.jpeg;*.jpg;*.png",
            };
            openFileDialog.Title = "Front Side";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                picFrontSide.ImageLocation = openFileDialog.FileName;
            }

        }

        private void txtSearchEPNNotSimilar_TextChanged(object sender, EventArgs e)
        {
            (dgvNotPokayoke.DataSource as DataTable).DefaultView.RowFilter = String.Format("Name like '%" + txtSearchEPNNotSimilar.Text + "%'");
        }

        private void txtSearchEPNSimilar_TextChanged(object sender, EventArgs e)
        {
            (dgvPokayoke.DataSource as DataTable).DefaultView.RowFilter = String.Format("Name like '%" + txtSearchEPNSimilar.Text + "%'");
        }

        private void btnFrontPicture_Click(object sender, EventArgs e)
        {
            if (btnFrontPicture.Text == "+ Add" && picFrontSide.Image != null)
            {
                File.Copy(picFrontSide.ImageLocation,Application.StartupPath+"\\Pictures\\"+lblEpn_id.Text+"_FrontSide.jpg");
                btnTheme(btnFrontPicture,picFrontSide,"add");
                return;
            }

            if (btnFrontPicture.Text == "- Delete" && picFrontSide.Image != null)
            {
                File.Delete(picFrontSide.ImageLocation);
                btnTheme(btnFrontPicture, picFrontSide, "del");
                return;
            }
        }

        public void btnTheme(GunaButton btnPicture,GunaPictureBox pictureBox,string type)
        {
            if (type == "add")
            {
                pictureBox.Enabled = false;
                btnPicture.Visible = true;
                btnPicture.BaseColor = Color.Red;
                btnPicture.OnHoverBaseColor = Color.FromArgb(255, 128, 128);
                btnPicture.Text = "- Delete";
                btnPicture.ForeColor = Color.White;
            }
            else
            {
                btnPicture.Visible = true;
                btnPicture.BaseColor = Color.Lime;
                btnPicture.OnHoverBaseColor = Color.FromArgb(128, 255, 128);
                btnPicture.Text = "+ Add";
                btnPicture.ForeColor = Color.Black;
                btnPicture.OnHoverForeColor = Color.Black;
                pictureBox.Enabled = true;
                pictureBox.Image = null;
            }
        }

        private void btnUpdateEPN_Click(object sender, EventArgs e)
        {
            bool isConnector = true;

            Epn epn = new Epn();
            epn.name = txtEPN.Text;
            epn.id = Int32.Parse(lblEpn_id.Text);

            if (switchIsClip.Checked)
            {
                isConnector = false;
            }

            if (!isConnector) epn.isConnector = 0;

            SqliteDataAccess.UpdateEpns(epn);

            btnUpdateEPN.Visible = false;
            btnAddEPN.Visible = true;

            btnFrontPicture.Visible = false;
            btnBackPicture.Visible = false;
            btnBottomPicture.Visible = false;
            btnTopPicture.Visible = false;
            btnLeftPicture.Visible = false;
            btnRightPicture.Visible = false;

            picBackSide.Enabled = false;
            picBottomSide.Enabled = false;
            picFrontSide.Enabled = false;
            picRightSide.Enabled = false;
            picTopSide.Enabled = false;
            picLeftSide.Enabled = false;

            //Update Tables
            FillDataGrideViewEpn();
            FillDataGrideViewClips();
            FillDataGrideViewConnectors();
        }

        private void btnRightPicture_Click(object sender, EventArgs e)
        {
            if (btnRightPicture.Text == "+ Add" && picRightSide.Image != null)
            {
                File.Copy(picRightSide.ImageLocation, Application.StartupPath + "\\Pictures\\" + lblEpn_id.Text + "_RightSide.jpg");
                btnTheme(btnRightPicture, picRightSide, "add");
                return;
            }


            if (btnRightPicture.Text == "- Delete" && picRightSide.Image != null)
            {
                File.Delete(picRightSide.ImageLocation);
                btnTheme(btnRightPicture, picRightSide, "del");
                return;
            }
        }

        private void btnLeftPicture_Click(object sender, EventArgs e)
        {
            if (btnLeftPicture.Text == "+ Add" && picLeftSide.Image != null)
            {
                File.Copy(picLeftSide.ImageLocation, Application.StartupPath + "\\Pictures\\" + lblEpn_id.Text + "_LeftSide.jpg");
                btnTheme(btnLeftPicture, picLeftSide, "add");
                return;
            }

            if (btnLeftPicture.Text == "-Delete" && picLeftSide.Image != null)
            {
                File.Delete(picLeftSide.ImageLocation);
                btnTheme(btnLeftPicture, picLeftSide, "del");
                return;
            }
        }

        private void btnTopPicture_Click(object sender, EventArgs e)
        {
            if (btnTopPicture.Text == "+ Add" && picTopSide.Image != null)
            {
                File.Copy(picTopSide.ImageLocation, Application.StartupPath + "\\Pictures\\" + lblEpn_id.Text + "_TopSide.jpg");
                btnTheme(btnTopPicture, picTopSide, "add");
                return;
            }
            if (btnTopPicture.Text == "- Delete" && picTopSide.Image != null)
            {
                File.Delete(picTopSide.ImageLocation);
                btnTheme(btnTopPicture, picTopSide, "del");
                return;
            }
        }

        private void btnBottomPicture_Click(object sender, EventArgs e)
        {
            if (btnBottomPicture.Text == "+ Add" && picBottomSide.Image != null)
            {
                File.Copy(picBottomSide.ImageLocation, Application.StartupPath + "\\Pictures\\" + lblEpn_id.Text + "_BottomSide.jpg");
                btnTheme(btnBottomPicture, picBottomSide, "add");
                return;
            }

            if (btnBottomPicture.Text == "- Delete" && picBottomSide.Image != null)
            {
                File.Delete(picBottomSide.ImageLocation);
                btnTheme(btnBottomPicture, picBottomSide, "del");
                return;
            }
        }

        private void btnBackPicture_Click(object sender, EventArgs e)
        {
            if (btnBackPicture.Text == "+ Add" && picBackSide.Image != null)
            {
                File.Copy(picBackSide.ImageLocation, Application.StartupPath + "\\Pictures\\" + lblEpn_id.Text + "_BackSide.jpg");
                btnTheme(btnBackPicture, picBackSide, "add");
                return;
            }
            if (btnBackPicture.Text == "- Delete" && picBackSide.Image != null)
            {
                File.Delete(picBackSide.ImageLocation);
                btnTheme(btnBackPicture, picBackSide, "del");
                return;
            }
        }
    }
}
