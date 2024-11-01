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
    /// Interaction logic for ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public ClientPage()
        {
            InitializeComponent();
            UpdTable();
        }

        public void UpdTable()
        {
            var Clients = App.Context.RentCar_Client.ToList();
            Table.ItemsSource = Clients;
        }

        private void LoadedBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdTable();
        }

        private void BlackListBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите добавить клиента в черный список?", "Черный список", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var currentCl = (sender as Button).DataContext as Entities.RentCar_Client;
                int cl_del = currentCl.ClientId;
                var Cl = App.Context.RentCar_Client.Find(cl_del);
                if (Cl != null)
                {
                    Cl.BlackList = true;
                    App.Context.SaveChanges();
                    MessageBox.Show("Клиент добавлен в черный список!", "Уведомление",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        public void FilterClient()
        {
            var client = App.Context.RentCar_Client.ToList();
            client = client
            .Where(p =>
                p.Surname.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.Name.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                p.Fathername.ToLower().Contains(SearchTextBox.Text.ToLower()))
            .ToList();
            Table.ItemsSource = client;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterClient();
        }
    }
}
