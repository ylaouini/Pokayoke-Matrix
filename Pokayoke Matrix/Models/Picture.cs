using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokayoke_Matrix.Models
{
    public class Picture
    {
        public int id { get; set; }
        public string front_side { get; set; }
        public string back_side { get; set; }
        public string left_side { get; set; }
        public string right_side { get; set; }
        public string top_side { get; set; }
        public string bottom_side { get; set; }
        public int epn_id { get; set; }

    }
}
