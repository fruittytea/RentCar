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
using System.Windows.Shapes;

namespace RentCar
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
            MainFrame.Navigate(new AdminHome());
        }

        private void CarsPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CarsPage());
            ButtonColor();
            CarsPage.Background = new SolidColorBrush(Color.FromRgb(43, 45, 66));
        }

        private void ButtonColor()
        {
            CarsPage.Background = new SolidColorBrush(Color.FromRgb(239, 35, 60));
            EmployesPage.Background = new SolidColorBrush(Color.FromRgb(239, 35, 60));
            ClientsPage.Background = new SolidColorBrush(Color.FromRgb(239, 35, 60));
            StatisticsPage.Background = new SolidColorBrush(Color.FromRgb(239, 35, 60));
            OrdersPage.Background = new SolidColorBrush(Color.FromRgb(239, 35, 60));
        }

        private void ExidBtn_Click(object sender, RoutedEventArgs e)
        {
            Window window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            ButtonColor();
            MainFrame.Navigate(new AdminHome());
        }

        private void EmployesPage_Click(object sender, RoutedEventArgs e)
        {
            ButtonColor();
            MainFrame.Navigate(new EmployeePage());
            EmployesPage.Background = new SolidColorBrush(Color.FromRgb(43, 45, 66));
        }

        private void ClientsPage_Click(object sender, RoutedEventArgs e)
        {
            ButtonColor();
            MainFrame.Navigate(new ClientPage());
            ClientsPage.Background = new SolidColorBrush(Color.FromRgb(43, 45, 66));
        }

        private void StatisticsPage_Click(object sender, RoutedEventArgs e)
        {
            ButtonColor();
            MainFrame.Navigate(new ReportPage());
            StatisticsPage.Background = new SolidColorBrush(Color.FromRgb(43, 45, 66));
        }

        private void OrdersPage_Click(object sender, RoutedEventArgs e)
        {
            ButtonColor();
            MainFrame.Navigate(new ActiveOrder());
            OrdersPage.Background = new SolidColorBrush(Color.FromRgb(43, 45, 66));
        }
    }
}
