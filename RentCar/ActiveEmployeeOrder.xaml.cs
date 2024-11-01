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
using static OfficeOpenXml.ExcelErrorValue;

namespace RentCar
{
    /// <summary>
    /// Interaction logic for ActiveEmployeeOrder.xaml
    /// </summary>
    public partial class ActiveEmployeeOrder : Page
    {
        public ActiveEmployeeOrder()
        {
            InitializeComponent();
            UpdTable();
        }

        private void LoadedBtn_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            UpdTable();
        }

        private void UpdTable()
        {
            var Order = App.Context.RentCar_RentalAgreement.Where(p => p.Status == 3).ToList();

            Table.ItemsSource = Order;
        }

        public void FilterOrder()
        {
            var orders = App.Context.RentCar_RentalAgreement.ToList();
            orders = orders
            .Where(p =>
                p.Status == 3 &&
                (p.RentCar_Car.CarNumber.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.RentCar_Car.CarModel.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.RentCar_Car.CarBrand.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.RentCar_Employee.Surname.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.RentCar_Employee.Name.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.RentCar_Employee.Fathername.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.RentCar_Client.Surname.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.RentCar_Client.Name.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.RentCar_Client.Fathername.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.AgreementStartDate.ToString("dd.MM.yyyy").Contains(SearchTextBox.Text.ToLower()) ||
                p.AgreementFinishDate.ToString("dd.MM.yyyy").Contains(SearchTextBox.Text.ToLower())))
            .ToList();
            Table.ItemsSource = orders;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterOrder();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            var current = (sender as Button).DataContext as Entities.RentCar_RentalAgreement;
            int AgrId = current.RentalAgreementId;

            if (MessageBox.Show("Вы действительно хотите завершить договор?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    var currentAgr = App.Context.RentCar_RentalAgreement.Find(AgrId);
                    if (currentAgr != null)
                    {
                        currentAgr.Status = 4;
                        App.Context.SaveChanges();
                        MessageBox.Show("Аренда завершена!", "Уведомление",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка");
                }
            }
        }

        private void EarlyBtn_Click(object sender, RoutedEventArgs e)
        {
            var current = (sender as Button).DataContext as Entities.RentCar_RentalAgreement;
            int AgrId = current.RentalAgreementId;

            if (MessageBox.Show("Вы действительно хотите завершить договор?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    var currentAgr = App.Context.RentCar_RentalAgreement.Find(AgrId);
                    if (currentAgr != null)
                    {
                        currentAgr.Status = 5;
                        App.Context.SaveChanges();
                        MessageBox.Show("Аренда завершена!", "Уведомление",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка");
                }
            }

        }

        private void BadBtn_Click(object sender, RoutedEventArgs e)
        {
            var current = (sender as Button).DataContext as Entities.RentCar_RentalAgreement;
            int AgrId = current.RentalAgreementId;

            if (MessageBox.Show("Вы действительно хотите завершить договор с нареканием клиенту?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    var currentAgr = App.Context.RentCar_RentalAgreement.Find(AgrId);
                    if (currentAgr != null)
                    {
                        currentAgr.Status = 4;
                        App.Context.SaveChanges();
                        MessageBox.Show("Аренда завершена!", "Уведомление",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    var BadClient = currentAgr.ClientId;

                    var currentBadClient = App.Context.RentCar_Client.Find(BadClient);
                    if (currentBadClient != null)
                    {
                        currentBadClient.Complaints = true;
                        App.Context.SaveChanges();
                        MessageBox.Show("Не забудьте добавить клиента в черный список!", "Уведомление",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка");
                }
            }

        }
    }
}
