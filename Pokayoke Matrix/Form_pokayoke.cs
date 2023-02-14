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
    public partial class Form_pokayoke : Form
    {

        public int epn_id;
        public Epn epn;
        public Picture picture =  new Picture();

        public Form_pokayoke()
        {
            InitializeComponent();
        }

        private void Form_pokayoke_Load(object sender, EventArgs e)
        {
            this.epn = SqliteDataAccess.LoadEpn("select * from tb_epns WHERE tb_epns.id = " + epn_id);

            
            lblEpn.Text = epn.name;

            picRightSide.ImageLocation = picture.right_side;
            picLeftSide.ImageLocation = picture.left_side;
            picBackSide.ImageLocation = picture.back_side;
            picBottomSide.ImageLocation = picture.bottom_side;
            picfrontSide.ImageLocation  = picture.front_side;
            picTopSide.ImageLocation = picture.top_side;



            flowLayoutPanel1.Controls.Clear();
            DataTable tableConnector = new DataTable();
            List<Epn> epns = new List<Epn>(); 

            foreach (Pokayoke pokayoke in SqliteDataAccess.Loadpokayoke("SELECT tb_pokayoke.epn1_id FROM tb_epns INNER JOIN tb_pokayoke ON tb_epns.id = tb_pokayoke.epn1_id or tb_epns.id = tb_pokayoke.epn2_id WHERE tb_epns.id = " + epn_id + "  " +
                                                                 " UNION SELECT tb_pokayoke.epn2_id FROM tb_epns INNER JOIN tb_pokayoke ON tb_epns.id = tb_pokayoke.epn1_id or tb_epns.id = tb_pokayoke.epn2_id WHERE tb_epns.id = " + epn_id))
            {

                if (pokayoke.epn1_id != epn_id)
                {
                    epns.Add(SqliteDataAccess.LoadEpn("SELECT tb_epns.id, tb_epns.name, back_side, bottom_side, front_side, top_side,left_side,right_side FROM tb_epns INNER JOIN tb_pictures ON tb_epns.id = tb_pictures.epn_id WHERE tb_epns.id = " + pokayoke.epn1_id));
                }
              
            }

            List<UserControl> ts = new List<UserControl>();
            PokayokeList[] lists = new PokayokeList[epns.Count];



            foreach (Epn epn in epns)
            {
                lists[0] = new PokayokeList();
                lists[0].lblEpn.Text = epn.name;

                lists[0].picBackSide.ImageLocation = Application.StartupPath + "\\Pictures\\" + epn.id + "_BackSide.jpg";
                lists[0].picLeftSide.ImageLocation = Application.StartupPath + "\\Pictures\\" + epn.id + "_LeftSide.jpg";
                lists[0].picRightSide.ImageLocation = Application.StartupPath + "\\Pictures\\" + epn.id + "_RightSide.jpg";

                lists[0].picBottomSide.Image = Image.FromFile(Application.StartupPath + "\\Pictures\\" + epn.id + "_BottomSide.jpg");
                lists[0].picTopSide.Image = Image.FromFile(Application.StartupPath + "\\Pictures\\" + epn.id + "_TopSide.jpg");
                lists[0].picfrontSide.Image = Image.FromFile(Application.StartupPath + "\\Pictures\\" + epn.id + "_FrontSide.jpg");


                lists[0].lblDateOfPokayokeCreate.Text = SqliteDataAccess.getDateOfPoka(epn.id, epn_id);

                ts.Add(lists[0]);
            }

            flowLayoutPanel1.Controls.AddRange( ts.ToArray());
        }
    }
}
