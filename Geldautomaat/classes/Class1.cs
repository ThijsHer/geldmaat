using System;
using System.Collections.Generic;
using System.Linq;
using Geldautomaat.classes;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace Geldautomaat.classes
{
    internal class Class1
    {
        private MySqlConnection conn;
        private MySqlConnection conn2;
        public Class1()
        {
            string myConnectionString = "server=localhost;uid=root;" +
                "pwd=;database=geld_automaat";
 
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                conn2 = new MySql.Data.MySqlClient.MySqlConnection();
                conn2.ConnectionString = myConnectionString;
                conn2.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                Console.WriteLine("conn failed :( ");
            }
        }

        
       
    }


    public class User
    {
        public static int Id;
    }



}
           
        
    

