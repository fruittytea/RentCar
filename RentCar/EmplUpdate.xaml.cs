using Microsoft.Win32;
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
using System.IO;
using static OfficeOpenXml.ExcelErrorValue;

namespace RentCar
{
    /// <summary>
    /// Interaction logic for EmplUpdate.xaml
    /// </summary>
    public partial class EmplUpdate : Window
    {
        public EmplUpdate()
        {
            InitializeComponent();

            var MyProfile = App.Context.RentCar_Employee.Where(p => p.EmployeeId == DataStorage.UserId).FirstOrDefault();

            SurnameTB.Text = MyProfile.Surname;
            NameTB.Text = MyProfile.Name;
            FathernameTB.Text = MyProfile.Fathername;
            PhoneTB.Text = MyProfile.Phone;
            EmailTB.Text = MyProfile.Email;
            PasswordTB.Text = MyProfile.Password;
            byte[] photoConv = MyProfile.Photo;

            if (photoConv != null && photoConv.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(photoConv))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = ms;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad; 
                    bitmap.EndInit();
                    bitmap.Freeze();

                    EmpImage.Source = bitmap; // Присваиваем ImageSource
                }
            }
        }

        private byte[] _mainImageData;

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var currentEmp = App.Context.RentCar_Employee.Find(DataStorage.UserId);
                if (currentEmp != null)
                {
                    currentEmp.Surname=SurnameTB.Text;
                    currentEmp.Name = NameTB.Text;
                    currentEmp.Fathername = FathernameTB.Text;
                    currentEmp.Phone = PhoneTB.Text;
                    currentEmp.Email = EmailTB.Text;
                    currentEmp.Password = PasswordTB.Text;
                    if (_mainImageData != null)
                    {
                        currentEmp.Photo = _mainImageData;
                    }
                    App.Context.SaveChanges();

                    MessageBox.Show("Данные успешно изменены!", "Уведомление",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка");
            }
        }

        private void SelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image |*.png; *.jpg; *.jpeg";
            if (ofd.ShowDialog() == true)
            {
                _mainImageData = File.ReadAllBytes(ofd.FileName);
                EmpImage.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(_mainImageData);
            }
        }

        private void ExidBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
