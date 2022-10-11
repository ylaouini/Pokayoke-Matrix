﻿using Pokayoke_Matrix.Models;
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
    public partial class Form_pokayoke : Form
    {

        public Epn epn;
        public Picture picture =  new Picture();

        public Form_pokayoke()
        {
            InitializeComponent();
        }

        private void Form_pokayoke_Load(object sender, EventArgs e)
        {


          /*  Console.WriteLine("front_side : " + picture.front_side);
            Console.WriteLine("back_side : " + picture.back_side);
            Console.WriteLine("left_side : " + picture.left_side);
            Console.WriteLine("right_side : " + picture.right_side);
            Console.WriteLine("top_side : " + picture.top_side);
            Console.WriteLine("bottom_side : " + picture.bottom_side);*/


            picBackSide.ImageLocation = picture.back_side;
            picTopSide.ImageLocation = picture.top_side;
            picLeftSide.ImageLocation = picture.left_side;
            picRightSide.ImageLocation = picture.right_side;
            picBottomSide.ImageLocation = picture.bottom_side;
            picfrontSide .ImageLocation = picture.front_side;

            flowLayoutPanel1.Controls.Clear();


            DataTable tableConnector = new DataTable();

            //tableConnector = await GetConnectorListAsync();
            List<UserControl> ts = new List<UserControl>();

            PokayokeList[] lists = new PokayokeList[5];
            //ModuleValidat[] module = new  ModuleValidat[tableConnector.Rows.Count];

            for (int i = 0; i < 5; i++)
            {
                lists[i] = new PokayokeList();
               // lists[i].



                /* module[i] = new ModuleValidat();
                 module[i].Id = tableConnector.Rows[i][0].ToString();
                 module[i].Name1 = tableConnector.Rows[i][1].ToString();
                 module[i].Epn = tableConnector.Rows[i][2].ToString();
                 module[i].Type = tableConnector.Rows[i][4].ToString();
                 module[i].Description = tableConnector.Rows[i][5].ToString();
                 if (tableConnector.Rows[i][3].ToString() != "")
                 {
                     module[i].Color = Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(239)))), ((int)(((byte)(206)))));
                     module[i].ForeColor1 = Color.Black;
                 }
                 // error[i].Icon = Image.FromFile("C:\\Users\\Yassine\\Downloads\\caution.png");*/
               // ts.Add(lists[]);
                ts.Add((PokayokeList)lists[i]);
            }

            flowLayoutPanel1.Controls.AddRange( ts.ToArray());
        }
    }
}