
using System;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Windows.Controls;
using Geldautomaat.classes;
using System.Diagnostics;

namespace Geldautomaat.pages
{
    public partial class Adminpage : Page
    {
        public Adminpage()
        {
            InitializeComponent();

            string connectionString = "server=localhost;uid=root;pwd=;database=geld_automaat";

            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                string query = "SELECT iduser, username, rekeningnummer, admin FROM user";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGrid.ItemsSource = dataTable.DefaultView;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Createuser createuser = new Createuser();
            NavigationService.Navigate(createuser);
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            DataRowView dataRow = (DataRowView)button.DataContext;

            int iduser = Convert.ToInt32(dataRow["iduser"]);
            string user = dataRow["username"].ToString();
            int rekeningnummer = Convert.ToInt32(dataRow["rekeningnummer"]);
            int admin = Convert.ToInt32(dataRow["admin"]);
            Debug.WriteLine(iduser);
            Debug.WriteLine(user);
            Debug.WriteLine(rekeningnummer);
            Debug.WriteLine(admin);

            string connectionString = "server=localhost;uid=root;pwd=;database=geld_automaat";
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                string query = "UPDATE user SET username = @user, rekeningnummer = @rekeningnummer, admin = @admin WHERE iduser = @iduser";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.Add("@iduser", MySqlDbType.Int64).Value = iduser;
                cmd.Parameters.Add("@user", MySqlDbType.VarChar).Value = user;
                cmd.Parameters.Add("@rekeningnummer", MySqlDbType.Int64).Value = rekeningnummer;
                cmd.Parameters.Add("@admin", MySqlDbType.Int64).Value = admin;
                Debug.WriteLine(cmd);
                cmd.ExecuteNonQuery();

            
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }  
}