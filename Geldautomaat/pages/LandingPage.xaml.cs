using Geldautomaat.classes;
using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Geldautomaat.pages
{
    public partial class LandingPage : Page
    {
        private MySqlConnection conn;

        public LandingPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string myConnectionString = "server=localhost;uid=root;" + "pwd=;database=geld_automaat";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string hashedPassword = HashPassword(txtPassword.Password);

                String query = "SELECT * FROM user WHERE rekeningnummer=@reknmr AND password=@password";
                MySqlCommand sqlCmd = new MySqlCommand(query, conn);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@reknmr", txtUsername.Text);
                sqlCmd.Parameters.AddWithValue("@password", hashedPassword);

                MySqlDataReader reader = sqlCmd.ExecuteReader();
                if (!reader.HasRows) return;

                while (reader.Read())
                {
                    User.Id = (int)reader["iduser"];
                    ChoicePage choixepage = new ChoicePage();
                    NavigationService.Navigate(choixepage);
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

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}