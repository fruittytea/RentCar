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
    /// Interaction logic for ActiveOrder.xaml
    /// </summary>
    public partial class ActiveOrder : Page
    {
        public ActiveOrder()
        {
            InitializeComponent();
            UpdTable();
        }

        private void UpdTable()
        {
            var Day = DateTime.Now;
            DateTime DateDay = Day.Date;

            var Order = App.Context.RentCar_RentalAgreement.Where(p => p.AgreementStartDate <= DateDay && p.AgreementFinishDate >= DateDay).ToList();

            Table.ItemsSource = Order;
        }

        public void FilterOrder()
        {
            var orders = App.Context.RentCar_RentalAgreement.ToList();
            orders = orders
            .Where(p =>
                p.RentCar_Car.CarNumber.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.RentCar_Car.CarModel.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.RentCar_Car.CarBrand.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.RentCar_Employee.Surname.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.RentCar_Employee.Name.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.RentCar_Employee.Fathername.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.RentCar_Client.Surname.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.RentCar_Client.Name.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.RentCar_Client.Fathername.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.RentCar_AgreementStatus.StatusName.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.AgreementStartDate.ToString("dd.MM.yyyy").Contains(SearchTextBox.Text.ToLower()) ||
                p.AgreementFinishDate.ToString("dd.MM.yyyy").Contains(SearchTextBox.Text.ToLower()))
            .ToList();
            Table.ItemsSource = orders;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterOrder();
        }

        private void LoadedBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdTable();
        }
    }
}
