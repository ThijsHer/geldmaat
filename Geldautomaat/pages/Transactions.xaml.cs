using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MySql.Data.MySqlClient;
using Geldautomaat.classes;

namespace Geldautomaat.pages
{
    public partial class Transactions : Page
    {
        public Transactions()
        {
            InitializeComponent();

            PopulateLastThreeTransactions();
        }

        private void PopulateLastThreeTransactions()
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=Geld_automaat";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT amount, positive, date, user_iduser FROM transactions WHERE user_iduser = @userId ORDER BY date DESC LIMIT 3";

                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@userId", User.Id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int i = 0;
                        while (reader.Read() && i < 3)
                        {
                            string amountText = reader["amount"].ToString();
                            bool isPositive = reader.GetBoolean("positive");

                            if (!isPositive)
                            {
                                amountText = "-" + amountText;
                            }

                            string amount = amountText + " EUR";
                            string date = reader["date"].ToString();

                            Label label = new Label
                            {
                                Content = $"Aantal: {amount}, Datum: {date}",
                                FontSize = 16
                            };

                            label.Background = isPositive ? Brushes.Green : Brushes.Red;

                            if (i == 0)
                                transactionLabel1.Content = label;
                            else if (i == 1)
                                transactionLabel2.Content = label;
                            else if (i == 2)
                                transactionLabel3.Content = label;
                            i++;
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        // Other methods and event handlers can be defined here.
    }
}