using Microsoft.Win32;
using RentCar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Interaction logic for CarsPage.xaml
    /// </summary>
    public partial class CarsPage : Page
    {
        public CarsPage()
        {
            InitializeComponent();
            UpdTable();
        }

        public void UpdTable()
        {
            var Car = App.Context.RentCar_Car.ToList();
            Table.ItemsSource = Car;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataStorage.action = "add";
            Window window = new CarAction();
            window.Show();
        }

        private void UpdBtn_Click(object sender, RoutedEventArgs e)
        {
            DataStorage.action = "upd";
            var currentCar = (sender as Button).DataContext as Entities.RentCar_Car; 
            DataStorage.carid = currentCar.CarId;

            Window window = new CarAction();
            window.Show();
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить автомобиль?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var button = sender as Button;
                var row = button.CommandParameter as RentCar_Car;

                if (row != null)
                {
                    var relatedAgreements = App.Context.RentCar_RentalAgreement
                        .Where(agreement => agreement.CarId == row.CarId)
                        .ToList();

                    foreach (var agreement in relatedAgreements)
                    {
                        App.Context.RentCar_RentalAgreement.Remove(agreement);
                    }

                    App.Context.RentCar_Car.Remove(row);
                    App.Context.SaveChanges();

                    MessageBox.Show("Автомобиль успешно удалены");
                    UpdTable();
                }
                else
                {
                    MessageBox.Show("Что-то пошло не так!");
                }
            }
            else
            {
                MessageBox.Show("Изменения отменены");
            }
        }

        private void LoadedBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdTable();
        }

        public void FilterCar()
        {
            var cars = App.Context.RentCar_Car.ToList();
            cars = cars
            .Where(p =>
                p.CarBrand.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.CarBrand.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.YearOfRelease.ToString().Contains(SearchTextBox.Text.ToLower()) ||
                p.RentCar_CarClasses.ClassName.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.RentCar_TransmissionType.TransmissionName.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.CarPower.ToString().Contains(SearchTextBox.Text.ToLower()) ||
                p.EngineSize.ToString().Contains(SearchTextBox.Text.ToLower()))
            .ToList();
            Table.ItemsSource = cars;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterCar();
        }
    }
}
