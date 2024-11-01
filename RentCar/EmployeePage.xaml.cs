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
    /// Interaction logic for EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        public EmployeePage()
        {
            InitializeComponent();
            UpdTable();
        }

        private void UpdTable()
        {
            var Employee = App.Context.RentCar_Employee.Where(p => p.Status != 2).ToList();
            Table.ItemsSource = Employee;
        }

        private void LoadedBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdTable();
        }

        private void DelEmp_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите уволить сотрудника?", "Увольнение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var currentEmp = (sender as Button).DataContext as Entities.RentCar_Employee;
                int us_del = currentEmp.EmployeeId;
                var Emp = App.Context.RentCar_Employee.Find(us_del);
                if (Emp != null)
                {
                    Emp.Status = 2;
                    App.Context.SaveChanges();
                    MessageBox.Show("Сотрудник уволен!", "Уведомление",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void AddEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            Window window = new AddEmployee();
            window.Show();
        }

        public void FilterEmployee()
        {
            var empl = App.Context.RentCar_Employee.ToList();
            empl = empl
            .Where(p =>
                p.Surname.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.Name.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.Fathername.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.RentCar_EmployeeRole.RoleName.ToLower().Contains(SearchTextBox.Text.ToLower()))
            .ToList();
            Table.ItemsSource = empl;
        }
        
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterEmployee();
        }
    }
}
