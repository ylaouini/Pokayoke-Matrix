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
        public string password
        {
            get
            {
                //System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                //  System.Text.Decoder decoder = encoding.GetDecoder();
                //  byte[] todecod_byte = Convert.FromBase64String(password);
                //  int charcount = encoding.GetCharCount(todecod_byte, 0, todecod_byte.Length);
                //  char[] decoded_char = new char[charcount];
                //  encoding.GetChars(todecod_byte, 0, todecod_byte.Length, decoded_char, 0);
                //  string pass = new String(decoded_char);
                //  return pass;

                return "password";
            }

            set {}
        }
        public string role { get; set; }
        public string email { get; set; }
        public int created_by { get; set; }
        public string created_at
        {
            get
            {
                return DateTime.Now.ToShortDateString();
            }
        }

    }
}
