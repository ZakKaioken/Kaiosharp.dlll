using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Kaiosharp
{
    public class KaioSql
    {
        //removed sensitive information so rebuild the sql connection variables more securly later
        static string ServerName = ""; //the ip or dns i think
        static string Username = ""; //sql username
        static string Password = ""; //sql password
        static string Database = ""; //selected sql database
        static MySqlConnection connection = new MySqlConnection("Server=" + ServerName + ";Database=" + Database + ";Uid=" + Username + ";Pwd=" + Password + ";Sslmode=none");
        static MySqlCommand command = connection.CreateCommand();

        public static string[,] Select(string[] selects, string from, string where = "")
        {
            //handles query building
            if (selects.Length > 0) //
            {
                string selectsx = "";
                if (selects.Length > 1)
                {
                   selectsx = string.Join(", ", selects);
                } else
                {
                    selectsx = selects[0];
                }
                string sqlstring = "SELECT " + selectsx + " FROM " + from;

                
                if (where != "")
                {
                    sqlstring += " WHERE " + where;
                }

                //start sql connection and insert sql
                connection.Open();
                command.CommandText = sqlstring;
                //new string grid (x)
                List<List<string>> SQLGrid = new List<List<string>>();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    //new colum for current row
                    List<string> columns = new List<string>();
                    for (int i = 0; i < selects.Length; i++)
                    {
                        //add current column to row
                        columns.Add(reader.GetString(selects[i]));
                    }
                    //add row to rows
                    SQLGrid.Add(columns);

                    //new 2d string array to hold the files
                    string[,] moku = new string[SQLGrid.Count, SQLGrid[0].Count];

                    //build 2d array from 2d list
                    for (int o = 0; o < SQLGrid.Count; o++)
                    {
                        for (int p = 0; p < SQLGrid[o].Count; p++)
                        {
                            moku[o, p] = SQLGrid[o][p];
                        }
                    }
                    connection.Close();
                    return moku;

                }

                

            }
            return null;
        }


       public static string[,] RunSQL(string sql, string[] items)
        {
            List<List<string>> SQLGrid = new List<List<string>>();
            
            connection.Open();
            command.CommandText = sql;
            var reader = command.ExecuteReader();
            
                while (reader.Read()) 
                {
                List<string> columns = new List<string>();
                for (int i = 0; i < items.Length; i++ )
                {
                    columns.Add(reader.GetString(items[i]));
                }
                SQLGrid.Add(columns);
                   // Columns += "[r]";
                }

            string[,] moku = new string[SQLGrid.Count,SQLGrid[0].Count];

                
                for (int o = 0; o < SQLGrid.Count; o++)
            {
                for (int p = 0; p < SQLGrid[o].Count; p++)
                {
                    moku[o, p] = SQLGrid[o][p];
                }
            }
                
            connection.Close();
            return moku;
        }

        public static int SQLCommand (string sql)
        {
            connection.Open();
            command.CommandText = sql;
            return command.ExecuteNonQuery();
        }


        public static string Genmd5 (string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)

            {

                sb.Append(hash[i].ToString("X2"));

            }

            return sb.ToString();
        }

    }

    public class UsePass
    {
        public string username;
        public string password;
        public string usepass;

        public UsePass(string user, string pass)
        {
            username = user;
            password = pass;
            usepass = Generate(user, pass);
        }

        string Generate(string user, string pass)
        {
            SHA1 shx = SHA1.Create();
            string usepassx = string.Concat(user.ToUpper(), ':', pass.ToUpper());
            byte[] bytes = shx.ComputeHash(Encoding.UTF8.GetBytes(usepassx));
            var hash = new System.Text.StringBuilder();
            foreach (byte bytx in bytes)
            {
                hash.Append(bytx.ToString("x2"));
            }
            shx.Dispose();
            return hash.ToString();

        }

    }
}
