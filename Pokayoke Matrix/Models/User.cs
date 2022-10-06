using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokayoke_Matrix.Models
{
    public class User
    {

        public int id { get; set; }
        public string fullName { get; set; }
        public string personnelID { get; set; }
        public string password { get; set; } = "password";

        public string role { get; set; }
        public string email { get; set; }
        public int created_by { get; set; }
        public string created_at { get; set; } = DateTime.Now.ToShortDateString();

    }
}
