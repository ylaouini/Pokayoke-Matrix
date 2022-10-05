using Pokayoke_Matrix.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Dapper;
using System.Windows.Forms;

namespace Pokayoke_Matrix
{
    public class SqliteDataAccess
    {


        private static string LoadConnectionString(string id ="Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        //Users

       
        public static List<User> LoadUsers()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<User>("SELECT * FROM tb_users", new DynamicParameters());
                return output.ToList();
            }
        }


        public static void SaveUsers(User user)
        {
            using(IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(@"INSERT INTO tb_users (fullName, personnelID, password, role, email, created_by, created_at) 
                            VALUES
                            (@fullName, @personnelID, @password, @role, @email, @created_by, @created_at)", user);
            }
        }

        public static bool Login(string personnelID, string password)
        {
            using (IDbConnection cnn =  new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<User>("SELECT * FROM tb_users WHERE personnelID="+personnelID);

                if (output.ToList().Count != 1) return false;

                if (output.First().password != password ) return false;

              
                Variables.id = output.First().id;
                Variables.email = output.First().email;
                Variables.personnelID = personnelID;
                Variables.fullName = output.First().fullName;
                Console.WriteLine(output.First().fullName);

            }

            
                return true;
        }


        //EPN

        public static List<Epn> LoadEpns(string query)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Epn>(query, new DynamicParameters());
                return output.ToList();
            }
        }


        public static int SaveEpns(Epn epn)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                int lastId = Convert.ToInt32(cnn.ExecuteScalar(@"INSERT INTO tb_epns (name, isConnector, isSupportClip, created_by, updated_by, created_at, updated_at) 
                            VALUES
                            (@name, @isConnector, @isSupportClip, @created_by, @updated_by, @created_at, @updated_at);
                            select last_insert_rowid()",epn));

                //Console.WriteLine(lastId);

               return lastId;
            }
        }

        public static void SaveEpnsPictures(Picture picture)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                int lastId = Convert.ToInt32(cnn.ExecuteScalar(@"INSERT INTO tb_pictures (front_side, right_side, left_side, top_side, bottom_side, back_side, epn_id) 
                            VALUES
                            (@front_side, @right_side, @left_side, @top_side, @bottom_side, @back_side, @epn_id);
                            select last_insert_rowid()", picture));

            }
        }
    }
}
