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
using System.Windows.Shapes;

namespace RentCar
{
    /// <summary>
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        public AddEmployee()
        {
            InitializeComponent();
            var RoleCB = App.Context.RentCar_EmployeeRole.Select(p => p.RoleName).ToList();
            RoleTB.ItemsSource = RoleCB;
        }

        private void ExidBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SurnameTB.Text != null && NameTB.Text != null && PhoneTB.Text != null
                    && EmailTB.Text != null && PasswordTB.Password != null && RoleTB.Text != null)
            {
                var IDRole = App.Context.RentCar_EmployeeRole.Where(p => p.RoleName ==RoleTB.Text).Select(p => p.RoleId).FirstOrDefault();
                try
                {
                    var newempl = new RentCar_Employee
                    {
                        Surname = SurnameTB.Text,
                        Name = NameTB.Text,
                        Fathername = FathernameTB.Text,
                        Phone = PhoneTB.Text,
                        Email = EmailTB.Text,
                        Password = PasswordTB.Password,
                        Role = IDRole,
                        Status = 1
                    };
                    App.Context.RentCar_Employee.Add(newempl);
                    App.Context.SaveChanges();
                    MessageBox.Show("Сотрудник добавлен!", "Уведомление",
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
                MessageBox.Show("Вы заполнили не все поля!");
            }
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
    }
}
