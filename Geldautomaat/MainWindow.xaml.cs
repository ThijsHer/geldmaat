using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Geldautomaat.classes;
using Geldautomaat.pages;
using MySql.Data.MySqlClient;




namespace Geldautomaat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
       


        public MainWindow() 
        {
            InitializeComponent();
            //Startpage startpage = new Startpage();
            //NavigationService.Navigate(StartPage);

        }

     
    }
}

