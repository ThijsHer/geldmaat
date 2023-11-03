using Geldautomaat.classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Geldautomaat.pages
{

    /// <summary>
    /// Interaction logic for Saldo.xaml
    /// </summary>
    public partial class Saldo : Page
    {
        private MySqlConnection conn;
        public Saldo()
        {
            InitializeComponent();
            Console.WriteLine(User.Id);
            string myConnectionString = "server=localhost;uid=root;" + "pwd=;database=geld_automaat";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                String query = "SELECT saldo FROM user WHERE iduser = @userid";
                MySqlCommand sqlCmd = new MySqlCommand(query, conn);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@userid", MySqlDbType.Int64).Value = User.Id;


                MySqlDataReader reader = sqlCmd.ExecuteReader();
                if (!reader.HasRows) return;

                while (reader.Read())
                {
                    saldoblock.Text = "€" + reader["saldo"].ToString();
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                MessageBox.Show("conn failed :( ");
            }
            finally
            {
                conn.Close();
            }

        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
