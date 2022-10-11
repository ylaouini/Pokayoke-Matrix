using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokayoke_Matrix.Models
{
    public class Epn 
    {

        public int id { get; set; }   
        public string name { get; set; }   
        public int  isConnector { get; set; } = 1;
        public int isSupportClip { get; set; } = 0;
        public int  created_by { get; set; } = Variables.id;
        public int  updated_by { get; set; } = Variables.id;
        public string created_at { get; set; } = DateTime.Now.ToShortDateString();
        public string  updated_at { get; set; } = DateTime.Now.ToShortDateString();

        public string fullName { get; set; }

        public int CountOfPokayoke { get; set; }

        public string front_side { get; set; }
        public string back_side { get; set; }
        public string left_side { get; set; }
        public string right_side { get; set; }
        public string top_side { get; set; }
        public string bottom_side { get; set; }
        public int epn_id { get; set; }


    }
}
