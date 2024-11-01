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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EnterBtn_Click(object sender, RoutedEventArgs e)
        {
            var User = App.Context.RentCar_Employee.FirstOrDefault(p => (p.Phone == LoginTB.Text || p.Email == LoginTB.Text) && p.Password == PasswordPB.Password && p.Status ==1);
            if (User != null)
            {
                App.currentUser = User;

                DataStorage.Surname = User.Surname;
                DataStorage.Name = User.Name;
                DataStorage.Fathername = User.Fathername;
                DataStorage.UserId = User.EmployeeId;
                DataStorage.UserRole = User.Role;

                HelloUser();

                if (App.currentUser.Role == 1) //Админ
                {
                    Window window = new Admin();
                    window.Show();
                    this.Close();
                }
                else if (App.currentUser.Role == 2) //Консультант
                {
                    Window user = new Employee();
                    user.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Извините, но мы не нашли ваш аккаунт, возможно вы ввели неверный логин или пароль.");
            }
        }

        private void HelloUser()
        {
            var timeOfDay = DateTime.Now;
            int currentHour = timeOfDay.Hour;

            if (currentHour >= 12 && currentHour <= 16)
            {
                MessageBox.Show("Добрый день, " + DataStorage.Name + " " + DataStorage.Fathername + "!");
            }
            else if (currentHour > 16 && currentHour <= 23)
            {
                MessageBox.Show("Добрый вечер, " + DataStorage.Name + " " + DataStorage.Fathername + "!");
            }
            else if (currentHour == 24 || currentHour <= 4)
            {
                MessageBox.Show("Доброй ночи, " + DataStorage.Name + " " + DataStorage.Fathername + "!");
            }
            else if (currentHour >= 5 && currentHour < 12)
            {
                MessageBox.Show("Доброе утро, " + DataStorage.Name + " " + DataStorage.Fathername + "!");
            }
        }

        private void LoginTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            string currentText = LoginTB.Text;

            if (currentText.StartsWith("7") && !currentText.StartsWith("+"))
            {
                LoginTB.Text = "+" + currentText + " (";
                LoginTB.CaretIndex = LoginTB.Text.Length;
            }

            if (currentText.Length == 7)
            {
                LoginTB.Text = currentText + ") ";
                LoginTB.CaretIndex = LoginTB.Text.Length;
            }

            if (currentText.Length == 12)
            {
                LoginTB.Text = currentText + "-";
                LoginTB.CaretIndex = LoginTB.Text.Length;
            }

            if (currentText.Length == 15)
            {
                LoginTB.Text = currentText + "-";
                LoginTB.CaretIndex = LoginTB.Text.Length;
            }
        }
    }
}
