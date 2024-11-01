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
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : Window
    {
        public Employee()
        {
            InitializeComponent();
            MainFrame.Navigate(new EmployeeHome());
        }

        private void ButtonColor()
        {
            ClientsPage.Background = new SolidColorBrush(Color.FromRgb(239, 35, 60));
            OrdersPage.Background = new SolidColorBrush(Color.FromRgb(239, 35, 60));
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            ButtonColor();
            MainFrame.Navigate(new EmployeeHome());
        }

        private void ExidBtn_Click(object sender, RoutedEventArgs e)
        {
            Window window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void ClientsPage_Click(object sender, RoutedEventArgs e)
        {
            ButtonColor();
            MainFrame.Navigate(new ClientOrderPage());
            ClientsPage.Background = new SolidColorBrush(Color.FromRgb(43, 45, 66));
        }

        private void OrdersPage_Click(object sender, RoutedEventArgs e)
        {
            ButtonColor();
            MainFrame.Navigate(new ActiveEmployeeOrder());
            OrdersPage.Background = new SolidColorBrush(Color.FromRgb(43, 45, 66));
        }
    }
}
