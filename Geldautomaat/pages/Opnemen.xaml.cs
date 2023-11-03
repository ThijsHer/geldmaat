using Geldautomaat.classes;
using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Geldautomaat.pages
{
    /// <summary>
    /// Interaction logic for Opnemen.xaml
    /// </summary>
    public partial class Opnemen : Page
    {
        string myConnectionString = "server=localhost;uid=root;" + "pwd=;database=geld_automaat";
        private MySqlConnection conn;

        private int opnemenCount = 0;
        private DateTime lastOpnemenDate;
        public int amount = 0;

        public Opnemen()
        {
            InitializeComponent();
            lastOpnemenDate = DateTime.Now.Date;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Opnemen2(object sender, RoutedEventArgs e)
        {
            conn = new MySqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open();

            DateTime currentDate = DateTime.Now.Date;
            if (currentDate > lastOpnemenDate)
            {
                opnemenCount = 0;
                lastOpnemenDate = currentDate;
            }

            if (opnemenCount >= 3)
            {
                MessageBox.Show("U hebt uw maximaal aantal opnames voor vandaag gebruikt (3 keer).");
                return;
            }

            int opnemenAmount;
            if (!int.TryParse(opnemen_box.Text, out opnemenAmount) || opnemenAmount <= 0)
            {
                MessageBox.Show("Voer een positief nummer in alsjeblieft.");
                return;
            }

            opnemenAmount = Math.Max(opnemenAmount, 0);

            int currentBalance = GetCurrentBalance();
            int newBalance = currentBalance - opnemenAmount;

            if (newBalance < 0)
            {
                MessageBox.Show("Niet genoeg saldo voor deze transactie.");
                return;
            }

            InsertTransaction(opnemenAmount);

            UpdateUserBalance(newBalance);

            opnemenCount++;

            MessageBox.Show($"Opnemen gelukt van {opnemenAmount} euros. Jouw balance is nu {newBalance} euro.");
        }

        private int GetCurrentBalance()
        {
            int balance = 0;
            String query = "SELECT saldo FROM user WHERE iduser = @userid";
            MySqlCommand sqlCmd = new MySqlCommand(query, conn);
            sqlCmd.CommandType = System.Data.CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@userid", MySqlDbType.Int64).Value = User.Id;

            using (MySqlDataReader reader = sqlCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    balance = unchecked((int)reader["saldo"]);
                }
            }

            return balance;
        }

        private void UpdateUserBalance(int newBalance)
        {
            string query2 = "UPDATE user SET saldo = @amount WHERE iduser = @userid";
            using (MySqlCommand sqlCmd2 = new MySqlCommand(query2, conn))
            {
                sqlCmd2.Parameters.AddWithValue("@userid", User.Id);
                sqlCmd2.Parameters.AddWithValue("@amount", newBalance);
                sqlCmd2.ExecuteNonQuery();
            }
        }

        private void InsertTransaction(int amount)
        {
            string query = "INSERT INTO transactions (amount, positive, user_iduser, date) VALUES (@amount, 1, @user_iduser, @date)";
            MySqlCommand sqlCmd = new MySqlCommand(query, conn);
            sqlCmd.Parameters.AddWithValue("@amount", amount);
            sqlCmd.Parameters.AddWithValue("@user_iduser", User.Id);
            sqlCmd.Parameters.AddWithValue("@date", DateTime.Now);

            sqlCmd.ExecuteNonQuery();
        }
    }
}