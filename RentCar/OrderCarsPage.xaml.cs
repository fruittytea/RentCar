using RentCar.Entities;
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
    /// Interaction logic for OrderCarsPage.xaml
    /// </summary>
    public partial class OrderCarsPage : Page
    {
        public OrderCarsPage()
        {
            InitializeComponent();
            UpdTable();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterCar();
        }

        public void FilterCar()
        {
            var cars = App.Context.RentCar_Car.ToList();
            cars = cars
                .Where(p =>
                    !p.RentCar_RentalAgreement.Any(agreement =>
                        agreement.CarId == p.CarId &&
                        agreement.Status==3)
                    &&
                    (p.CarBrand.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                     p.YearOfRelease.ToString().Contains(SearchTextBox.Text.ToLower()) ||
                     p.RentCar_CarClasses.ClassName.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                     p.RentCar_TransmissionType.TransmissionName.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                     p.CarPower.ToString().Contains(SearchTextBox.Text.ToLower()) ||
                     p.EngineSize.ToString().Contains(SearchTextBox.Text.ToLower()))
                )
                .ToList();

            Table.ItemsSource = cars;
        }

        public void UpdTable()
        {
            var Car = App.Context.RentCar_Car
                .Where(p => !p.RentCar_RentalAgreement.Any(agreement =>
                        agreement.CarId == p.CarId &&
                        agreement.Status == 3))
                .ToList();
            Table.ItemsSource = Car;
        }

        private void SelectBtn_Click_1(object sender, RoutedEventArgs e)
        {
            var currentCar = (sender as Button).DataContext as Entities.RentCar_Car;
            DataStorage.CarIdOrder = currentCar.CarId;

            try
            {
                var newAgr = new RentCar_RentalAgreement
                {
                    ClientId = Convert.ToInt32(DataStorage.ClientIdOrder),
                    CarId = Convert.ToInt32(DataStorage.CarIdOrder),
                    EmployeeId = Convert.ToInt32(DataStorage.UserId),
                    AgreementStartDate = Convert.ToDateTime(DataStorage.orderstart),
                    AgreementFinishDate = Convert.ToDateTime(DataStorage.orderfinish),
                    Status = 3
                };
                App.Context.RentCar_RentalAgreement.Add(newAgr);
                App.Context.SaveChanges();
                DataStorage.SelOrder = newAgr.RentalAgreementId;

                NavigationService.Navigate(new OrderIsReady());
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так :(");
            }
        }
    }
}
