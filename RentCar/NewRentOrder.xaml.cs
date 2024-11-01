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
    /// Interaction logic for NewRentOrder.xaml
    /// </summary>
    public partial class NewRentOrder : Window
    {
        public NewRentOrder()
        {
            InitializeComponent();
            OrderFrame.Navigate(new OrderDeadlines());
        }

        private void ExidBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
