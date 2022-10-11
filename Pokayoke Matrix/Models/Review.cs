using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokayoke_Matrix.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int review { get; set; } = 1;
        public int reviewd_by  { get; set; } = Variables.id;
        public int pokayoke_id { get; set; }
    }
}
