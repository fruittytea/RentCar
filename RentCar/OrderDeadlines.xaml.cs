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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RentCar
{
    /// <summary>
    /// Interaction logic for OrderDeadlines.xaml
    /// </summary>
    public partial class OrderDeadlines : Page
    {
        public OrderDeadlines()
        {
            InitializeComponent();
        }

        private void ToFormBtn_Click(object sender, RoutedEventArgs e)
        {
            var DateNow = DateTime.Now;

            if ((StartDateDP.SelectedDate < DateNow || FinishDateDP.SelectedDate< DateNow) && StartDateDP.SelectedDate>FinishDateDP.SelectedDate)
            {
                MessageBox.Show("Невозможно оформить аренду задним числом. Пожалуйста, введите новые сроки!");
            }
            else
            {
                DataStorage.orderstart = Convert.ToDateTime(StartDateDP.SelectedDate);
                DataStorage.orderfinish = Convert.ToDateTime(FinishDateDP.SelectedDate);

                NavigationService.Navigate(new OrderCarsPage());
            }
        }
    }
}
