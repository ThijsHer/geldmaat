using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Geldautomaat.classes;
using MySql.Data.MySqlClient;

namespace Geldautomaat.pages
{
    public partial class ChoicePage : Page
    {
        public ChoicePage()
        {
            InitializeComponent();

            CheckUserAdminStatus();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Saldo(object sender, RoutedEventArgs e)
        {
            Saldo saldo = new Saldo();
            NavigationService.Navigate(saldo);
        }

        private void Button_Storten(object sender, RoutedEventArgs e)
        {
            Storten storten = new Storten();
            NavigationService.Navigate(storten);
        }

        private void Btn_Opnemen(object sender, RoutedEventArgs e)
        {
            Opnemen opnemen = new Opnemen();
            NavigationService.Navigate(opnemen);
        }

        private void trans_button_Click(object sender, RoutedEventArgs e)
        {
            Transactions transactions = new Transactions();
            NavigationService.Navigate(transactions);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Adminpage adminpage = new Adminpage();
            NavigationService.Navigate(adminpage);
        }

        private void CheckUserAdminStatus()
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=geld_automaat";

            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                MessageBox.Show("User.Id: " + User.Id);
                connection.Open();

                string query = "SELECT admin FROM user WHERE iduser = @userId";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@userId", User.Id);

                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    int adminStatus = Convert.ToInt32(result);

                    MessageBox.Show("Admin status: " + adminStatus);

                    if (adminStatus == 1)
                    {
                        MessageBox.Show("User is an admin."); 
                        adminButton.IsEnabled = true;
                        adminTextBlock.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        MessageBox.Show("User is not an admin.");
                        adminButton.IsEnabled = false;
                        adminTextBlock.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    MessageBox.Show("No admin status found for this user.");
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
    }
}