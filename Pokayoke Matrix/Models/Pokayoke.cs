using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokayoke_Matrix.Models
{
    public class Pokayoke
    {
        public int Id { get; set; }
        public int epn1_id { get; set; }
        public int epn2_id { get; set; }
        public int project_id { get; set; }
        public string created_at { get; set; } = DateTime.Now.ToShortDateString();
    }
}
