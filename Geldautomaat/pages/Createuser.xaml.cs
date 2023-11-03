using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Geldautomaat.pages
{
    public partial class Createuser : Page
    {
        public Createuser()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string user = userNameTextBox.Text;
            string plainPassword = passwordTextBox.Text; 

            string hashedPassword = HashPassword(plainPassword);

            string rekeningnummer = rekeningnummerTextBox.Text;

            string connectionString = "server=localhost;uid=root;pwd=;database=geld_automaat";

            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                
                string query = "INSERT INTO user (username, password, rekeningnummer, saldo, admin) VALUES (@user, @password, @rekeningnummer, 0, 0)";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@password", hashedPassword);
                cmd.Parameters.AddWithValue("@rekeningnummer", rekeningnummer);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("User created successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to create the user.");
                }
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