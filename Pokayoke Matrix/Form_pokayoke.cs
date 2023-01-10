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
            Console.WriteLine("test :" + picfrontSide.ErrorImage.ToString());
            this.epn = SqliteDataAccess.LoadEpn("select * from tb_epns WHERE tb_epns.id = " + epn_id);

            Console.WriteLine(File.Exists(picture.front_side));
            
            lblEpn.Text = epn.name;
            /* picBackSide.ImageLocation = picture.back_side;
             picTopSide.ImageLocation = picture.top_side;
             picLeftSide.ImageLocation = picture.left_side;
             picRightSide.ImageLocation = picture.right_side;
             picBottomSide.ImageLocation = picture.bottom_side;
             picfrontSide .ImageLocation = picture.front_side;*/

            try
            {
                picBackSide.Image = Image.FromFile(picture.back_side);
                picTopSide.Image = Image.FromFile(picture.top_side);
                picLeftSide.Image = Image.FromFile(picture.left_side);
                picRightSide.Image = Image.FromFile(picture.right_side);
                picBottomSide.Image = Image.FromFile(picture.bottom_side);
                picfrontSide.Image = Image.FromFile(picture.front_side);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.GetType().Name+" (" + ex.Message+")");
            }
           


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

                //lists[0].picBackSide.ImageLocation = epn.back_side ;
                //lists[0].picBottomSide.ImageLocation = epn.bottom_side;
                //lists[0].picLeftSide.ImageLocation = epn.left_side;
                //lists[0].picRightSide.ImageLocation = epn.right_side;
                //lists[0].picTopSide.ImageLocation = epn.top_side;
                //lists[0].picfrontSide.ImageLocation = epn.front_side;
                try
                {
                    lists[0].picBackSide.Image = Image.FromFile(epn.back_side);
                    lists[0].picBottomSide.Image = Image.FromFile(epn.bottom_side);
                    lists[0].picLeftSide.Image = Image.FromFile(epn.left_side);
                    lists[0].picRightSide.Image = Image.FromFile(epn.right_side);
                    lists[0].picTopSide.Image = Image.FromFile(epn.top_side);
                    lists[0].picfrontSide.Image = Image.FromFile(epn.front_side);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetType().Name + " (" + ex.Message + ")");
                }


                lists[0].lblDateOfPokayokeCreate.Text = SqliteDataAccess.getDateOfPoka(epn.id, epn_id);

                ts.Add(lists[0]);
            }

            flowLayoutPanel1.Controls.AddRange( ts.ToArray());
        }
    }
}
