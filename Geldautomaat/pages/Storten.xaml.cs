using Geldautomaat.classes;
using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Geldautomaat.pages
{
    /// <summary>
    /// Interaction logic for Storten.xaml
    /// </summary>
    public partial class Storten : Page
    {
        public Storten()
        {
            InitializeComponent();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Storten(object sender, RoutedEventArgs e)
        {
            int kosten = 2;
            int amount = int.Parse(amount_box.Text) - kosten;

            string myConnectionString = "server=localhost;uid=root;pwd=;database=geld_automaat";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(myConnectionString))
                {
                    conn.Open();

                    string query = "SELECT saldo FROM user WHERE iduser = @userid";
                    using (MySqlCommand sqlCmd = new MySqlCommand(query, conn))
                    {
                        sqlCmd.Parameters.AddWithValue("@userid", User.Id);
                        int start_amount = Convert.ToInt32(sqlCmd.ExecuteScalar());

                        int stort_amount = start_amount + amount;

                        string query2 = "UPDATE user SET saldo = @amount WHERE iduser = @userid";
                        using (MySqlCommand sqlCmd2 = new MySqlCommand(query2, conn))
                        {
                            sqlCmd2.Parameters.AddWithValue("@userid", User.Id);
                            sqlCmd2.Parameters.AddWithValue("@amount", stort_amount);
                            sqlCmd2.ExecuteNonQuery();

                            InsertTransaction(stort_amount, User.Id);
                        }

                        MessageBox.Show("Storten Gelukt!");
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void InsertTransaction(int amount, int userId)
        {
            string myConnectionString = "server=localhost;uid=root;pwd=;database=geld_automaat";
            string query = "INSERT INTO transactions (amount, positive, user_iduser, date) VALUES (@amount, 1, @user_iduser, @date)";
            using (MySqlConnection conn = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand sqlCmd = new MySqlCommand(query, conn))
                {
                    int transactionAmount = int.Parse(amount_box.Text);

                    sqlCmd.Parameters.AddWithValue("@amount", transactionAmount);
                    sqlCmd.Parameters.AddWithValue("@user_iduser", userId);
                    sqlCmd.Parameters.AddWithValue("@date", DateTime.Now);

                    conn.Open();
                    sqlCmd.ExecuteNonQuery();
                }
            }
        }
    }
}