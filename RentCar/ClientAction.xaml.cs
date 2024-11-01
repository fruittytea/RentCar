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
using System.Windows.Shapes;
using static OfficeOpenXml.ExcelErrorValue;

namespace RentCar
{
    /// <summary>
    /// Interaction logic for ClientAction.xaml
    /// </summary>
    public partial class ClientAction : Window
    {
        public ClientAction()
        {
            InitializeComponent();

            if (DataStorage.action == "add")
            {
                LabelText.Text = "Добавление нового клиента";
            }
            if (DataStorage.action == "upd")
            {
                LabelText.Text = "Изменение данных клиента";

                var OldClient = App.Context.RentCar_Client.Where(p => p.ClientId==DataStorage.ClientId).FirstOrDefault();

                SurnameTB.Text = OldClient.Surname;
                NameTB.Text = OldClient.Name;
                FathernameTB.Text = OldClient.Fathername;
                PhoneTB.Text = OldClient.Phone;
                EmailTB.Text = OldClient.Email;
                PassportSeriesTB.Text = OldClient.PasportSeries;
                PassportNumberTB.Text = OldClient.PasportNumber;
                DateOfBirthTB.SelectedDate = OldClient.DateOfBirth;
                AddressTB.Text = OldClient.Address;
                DriverSeriesTB.Text = OldClient.DriversLicenseSeries;
                DriverNumTB.Text = OldClient.DriversLicenseNumber;
                StDate.SelectedDate = OldClient.DateOfIssue;
                FinDate.SelectedDate = OldClient.ExpirationDate;
            }
        }

        private static bool IsTextAllowed(string text)
        {
            return int.TryParse(text, out _);
        }

        private void PhoneTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            string currentText = PhoneTB.Text;

            if (currentText.StartsWith("7") && !currentText.StartsWith("+"))
            {
                PhoneTB.Text = "+" + currentText + " (";
                PhoneTB.CaretIndex = PhoneTB.Text.Length;
            }

            if (currentText.Length == 7)
            {
                PhoneTB.Text = currentText + ") ";
                PhoneTB.CaretIndex = PhoneTB.Text.Length;
            }

            if (currentText.Length == 12)
            {
                PhoneTB.Text = currentText + "-";
                PhoneTB.CaretIndex = PhoneTB.Text.Length;
            }

            if (currentText.Length == 15)
            {
                PhoneTB.Text = currentText + "-";
                PhoneTB.CaretIndex = PhoneTB.Text.Length;
            }
        }

        private void PassportSeriesTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void PassportNumberTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void DriverSeriesTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void DriverNumTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(DataStorage.action == "add")
            {
                if (SurnameTB.Text != null && NameTB.Text != null && FathernameTB.Text != null && PhoneTB.Text != null && EmailTB.Text != null &&
                        PassportSeriesTB.Text != null && PassportNumberTB.Text != null && DateOfBirthTB.SelectedDate != null &&
                        AddressTB.Text != null && DriverSeriesTB.Text != null && DriverNumTB.Text != null && StDate.SelectedDate != null && FinDate.SelectedDate != null)
                {
                    try
                    {
                        var newclient = new RentCar_Client
                        {
                            Surname = SurnameTB.Text,
                            Name = NameTB.Text,
                            Fathername = FathernameTB.Text,
                            Phone = PhoneTB.Text,
                            Email = EmailTB.Text,
                            PasportSeries = PassportSeriesTB.Text,
                            PasportNumber = PassportNumberTB.Text,
                            DateOfBirth = Convert.ToDateTime(DateOfBirthTB.SelectedDate),
                            Address = AddressTB.Text,
                            DriversLicenseSeries = DriverSeriesTB.Text,
                            DriversLicenseNumber = DriverNumTB.Text,
                            DateOfIssue = Convert.ToDateTime(StDate.SelectedDate),
                            ExpirationDate = Convert.ToDateTime(FinDate.SelectedDate),
                            Complaints = false,
                            BlackList = false
                        };
                        App.Context.RentCar_Client.Add(newclient);
                        App.Context.SaveChanges();
                        MessageBox.Show("Клиент добавлен!", "Уведомление",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Что-то пошло не так :(");
                    }
                }
                else
                {
                    MessageBox.Show("Вы заполнили не все поля! Пожалуйста, перепроверьте!");
                }
            }

            if (DataStorage.action == "upd")
            {
                try
                {
                    var currentClient = App.Context.RentCar_Client.Find(DataStorage.ClientId);
                    if (currentClient != null)
                    {
                        currentClient.Surname = SurnameTB.Text;
                        currentClient.Name = NameTB.Text;
                        currentClient.Fathername = FathernameTB.Text;
                        currentClient.Phone = PhoneTB.Text;
                        currentClient.Email = EmailTB.Text;
                        currentClient.PasportSeries = PassportSeriesTB.Text;
                        currentClient.PasportNumber = PassportNumberTB.Text;
                        currentClient.DateOfBirth = Convert.ToDateTime(DateOfBirthTB.SelectedDate);
                        currentClient.Address = AddressTB.Text;
                        currentClient.DriversLicenseSeries = DriverSeriesTB.Text;
                        currentClient.DriversLicenseNumber = DriverNumTB.Text;
                        currentClient.DateOfIssue = Convert.ToDateTime(StDate.SelectedDate);
                        currentClient.ExpirationDate = Convert.ToDateTime(FinDate.SelectedDate);
                        App.Context.SaveChanges();
                        MessageBox.Show("Данные клиента успешно изменены!", "Уведомление",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка");
                }
            }
        }

        private void ExidBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
