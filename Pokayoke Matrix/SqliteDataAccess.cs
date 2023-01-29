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
using System.Collections;

namespace Pokayoke_Matrix
{
    public class SqliteDataAccess
    {


        private static string LoadConnectionString(string id = "Default")
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
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(@"INSERT INTO tb_users (fullName, personnelID, password, role, email, created_by, created_at) 
                            VALUES
                            (@fullName, @personnelID, @password, @role, @email, @created_by, @created_at)", user);
            }
        }

        public static bool Login(string personnelID, string password)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<User>("SELECT * FROM tb_users WHERE personnelID=" + personnelID);

                if (output.ToList().Count != 1) return false;

                if (output.First().password != password) return false;


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
                //var output = cnn.Query<Epn>(query, new DynamicParameters());
                var output = cnn.Query<Epn>(query, new DynamicParameters());
                return output.ToList();
            }
        }

        public static Epn LoadEpn(string query)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                //var output = cnn.Query<Epn>(query, new DynamicParameters());

                return cnn.QueryFirst<Epn>(query, new DynamicParameters()); ;
            }
        }


        public static int SaveEpns(Epn epn)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                int lastId = Convert.ToInt32(cnn.ExecuteScalar(@"INSERT INTO tb_epns (name, isConnector, created_by, updated_by, created_at, updated_at) 
                            VALUES
                            (@name, @isConnector, @created_by, @updated_by, @created_at, @updated_at);
                            select last_insert_rowid()", epn));

                //Console.WriteLine(lastId);

                return lastId;
            }
        }

        public static int UpdateEpns(Epn epn)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string query = @"update tb_epns set name= '"+epn.name+"', isConnector = '"+epn.isConnector+"', updated_by = '"+epn.updated_by+"', updated_at = '"+epn.updated_at+"' where id = "+epn.id+"";
                //Console.WriteLine("test:"+query);
                int lastId = Convert.ToInt32(cnn.ExecuteScalar(query, epn));

                return lastId;
            }
        }

        //picture

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

        public static void UpdateEpnsPictures(Picture picture,int epn_id)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                int lastId = Convert.ToInt32(cnn.ExecuteScalar(@"UPDATE tb_pictures SET front_side = '@front_side', right_side = '@right_side', left_side = '@left_side', top_side = '@top_side', bottom_side = '@bottom_side', back_side = '@back_side' 
                            where epn_id = "+epn_id, picture));

            }
        }


        //pokayoke

        public static int SavePokayoke(Pokayoke pokayoke)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                int lastId = Convert.ToInt32(cnn.ExecuteScalar(@"INSERT INTO tb_pokayoke (epn1_id, epn2_id, project_id, created_at) 
                            VALUES
                            (@epn1_id, @epn2_id, @project_id, @created_at);
                            select last_insert_rowid()", pokayoke));


                return lastId;
            }
        }

        public static List<Pokayoke> Loadpokayoke(string query)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Pokayoke>(query, new DynamicParameters());
                return output.ToList();
            }
        }

        public static void DeletePokayoke(int epn1, int epn2, int project_id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Query("DELETE FROM tb_pokayoke WHERE (tb_pokayoke.epn1_id = " + epn1 + " and tb_pokayoke.epn2_id = " + epn2 + " and tb_pokayoke.project_id = "+project_id+") OR (tb_pokayoke.epn1_id = " + epn2 + " and tb_pokayoke.epn2_id = " + epn1 + " and tb_pokayoke.project_id = "+project_id+")", new DynamicParameters());
            }
        }

        public static string getDateOfPoka(int epn1, int epn2)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var query = @"SELECT created_at FROM tb_pokayoke where epn1_id = " + epn1 + " and epn2_id = " + epn2 + " or " +
                    "epn1_id = " + epn2 + " and epn2_id = " + epn1;

                string date =  (String)cnn.ExecuteScalar(query);
                return date;
            }
        }


        //reviews

        public static int SaveReview(Review review)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                int lastId = Convert.ToInt32(cnn.ExecuteScalar(@"INSERT INTO tb_reviews (review, reviewd_by, pokayoke_id) 
                            VALUES
                            (@review, @reviewd_by, @pokayoke_id);
                            select last_insert_rowid()", review));

                return lastId;
            }
        }

        //Project

        public static List<Project> LoadProject()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                //var output = cnn.Query<Epn>(query, new DynamicParameters());
                var output = cnn.Query<Project>("SELECT * FROM tb_projects", new DynamicParameters());
                return output.ToList();
            }
        }

        //TOOLS

        public static bool ModelEpnExists(string name)
        {

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Pokayoke>("SELECT * FROM tb_epns WHERE name = '"+name+"'", new DynamicParameters());
                output.Count();

                if (output.Count() > 0)
                {
                    return true;
                }
            }

            return false;
        }


    }
}
