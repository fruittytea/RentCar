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

namespace RentCar
{
    /// <summary>
    /// Interaction logic for EmployeeHome.xaml
    /// </summary>
    public partial class EmployeeHome : Page
    {
        public EmployeeHome()
        {
            InitializeComponent();

            HelloUser();
            FIO.Text = DataStorage.Surname + " " + DataStorage.Name + " " + DataStorage.Fathername;
            CardsFullFill();
        }

        private void HelloUser()
        {
            var timeOfDay = DateTime.Now;
            int currentHour = timeOfDay.Hour;

            if (currentHour >= 12 && currentHour <= 16)
            {
                DayTime.Text = "Добрый день,";
            }
            else if (currentHour > 16 && currentHour <= 23)
            {
                DayTime.Text = "Добрый вечер,";
            }
            else if (currentHour == 24 || currentHour <= 4)
            {
                DayTime.Text = "Доброй ночи,";
            }
            else if (currentHour >= 5 && currentHour < 12)
            {
                DayTime.Text = "Доброе утро,";
            }
        }

        private void CardsFullFill()
        {
            var Day = DateTime.Now;
            DateTime DateDay = Day.Date;
            string currentDate = Convert.ToString(Day.Date);
            var expAgr = App.Context.RentCar_RentalAgreement.Count(p => p.AgreementFinishDate == DateDay);
            ExpireAgreement.Text = Convert.ToString(expAgr);

            var ThisMonth = Day.Month;
            var NowAgr = App.Context.RentCar_RentalAgreement.Where(p => p.RentCar_Employee.EmployeeId == DataStorage.UserId).Count(p => p.AgreementStartDate.Month >= ThisMonth);
            IssuedMonth.Text = Convert.ToString(NowAgr);
        }

        private void UpdBtn_Click(object sender, RoutedEventArgs e)
        {
            Window window = new EmplUpdate();
            window.Show();
        }
    }
}
