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
using RentCar.Entities;
using System.Security.Policy;
//using static System.Net.WebRequestMethods;

namespace RentCar
{
    /// <summary>
    /// Interaction logic for CarAction.xaml
    /// </summary>
    public partial class CarAction : Window
    {
        public CarAction()
        {
            InitializeComponent();

            var CBClass = App.Context.RentCar_CarClasses.Select(p => p.ClassName).ToList();
            ClassTB.ItemsSource = CBClass;

            var CSTType = App.Context.RentCar_TransmissionType.Select(p => p.TransmissionName).ToList();
            TTypeTB.ItemsSource = CSTType;

            var CBColor = App.Context.RentCar_CarColors.Select(p => p.Color).ToList();
            ColTB.ItemsSource = CBColor;

            if(DataStorage.action == "add")
            {
                LabelText.Text = "Добавить автомобиль";
            }

            if(DataStorage.action == "upd")
            {
                LabelText.Text = "Изменить автомобиль";

                var OldCar = App.Context.RentCar_Car.Where(p => p.CarId == DataStorage.carid).FirstOrDefault();

                CarBrandTB.Text = OldCar.CarBrand;
                CarModelTB.Text = OldCar.CarModel;
                YTB.Text = Convert.ToString(OldCar.YearOfRelease);
                PriceTB.Text = Convert.ToString(OldCar.RentCost);
                ClassTB.Text = OldCar.RentCar_CarClasses.ClassName;
                TTypeTB.Text = OldCar.RentCar_TransmissionType.TransmissionName;
                ColTB.Text = OldCar.RentCar_CarColors.Color;
                ValueTB.Text = Convert.ToString(OldCar.EngineSize);
                PowerTB.Text = Convert.ToString(OldCar.CarPower);
                CarNumberTB.Text = OldCar.CarNumber;
                CarRegionTB.Text = Convert.ToString(OldCar.CarRegion);

                byte[] photoConv = OldCar.Photo; // Предполагаем, что это ваш массив байтов

                if (photoConv != null && photoConv.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(photoConv))
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.StreamSource = ms;
                        bitmap.CacheOption = BitmapCacheOption.OnLoad; // Опционально, если вам нужно сохранить изображение в памяти
                        bitmap.EndInit();
                        bitmap.Freeze(); // Опционально, если вы хотите сделать объект потокобезопасным

                        CarImage.Source = bitmap; // Присваиваем ImageSource
                    }
                }
            }
        }

        private void ExidBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void YTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            return int.TryParse(text, out _);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void ValueTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void PowerTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var SelectClass = App.Context.RentCar_CarClasses.Where(p => p.ClassName == ClassTB.Text).Select(p => p.CarClassId).FirstOrDefault();
            var SelectTType = App.Context.RentCar_TransmissionType.Where(p => p.TransmissionName == TTypeTB.Text).Select(p => p.TypeId).FirstOrDefault();
            var SelectColor = App.Context.RentCar_CarColors.Where(p => p.Color == ColTB.Text).Select(p => p.ColorId).FirstOrDefault();

            if (DataStorage.action == "add")
            {
                if (CarBrandTB.Text != null && CarModelTB.Text != null && YTB.Text != null
                    && PriceTB.Text != null && ClassTB.Text != null && TTypeTB.Text != null
                    && ColTB.Text != null && ValueTB.Text != null && PowerTB.Text != null && _mainImageData != null
                    && CarNumberTB.Text != null && CarRegionTB.Text != null)
                {
                    try
                    {
                        var newcar = new RentCar_Car
                        {
                            CarBrand = CarBrandTB.Text,
                            CarModel = CarModelTB.Text,
                            YearOfRelease = int.Parse(YTB.Text),
                            RentCost = int.Parse(PriceTB.Text),
                            CarClass = SelectClass,
                            TransmissionType = SelectTType,
                            Color = SelectColor,
                            EngineSize = int.Parse(ValueTB.Text),
                            CarPower = int.Parse(PowerTB.Text),
                            CarNumber = CarNumberTB.Text,
                            CarRegion = int.Parse(CarRegionTB.Text),
                            Photo = _mainImageData
                        };
                        App.Context.RentCar_Car.Add(newcar);
                        App.Context.SaveChanges();
                        MessageBox.Show("Автомобиль добавлен!", "Уведомление",
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
            else if (DataStorage.action == "upd")
            {
                if (_mainImageData != null)
                {
                    try
                    {
                        var currentCar = App.Context.RentCar_Car.Find(DataStorage.carid);
                        if (currentCar != null)
                        {
                            currentCar.CarBrand = CarBrandTB.Text;
                            currentCar.CarModel = CarModelTB.Text;
                            currentCar.YearOfRelease = int.Parse(YTB.Text);
                            currentCar.RentCost = int.Parse(PriceTB.Text);
                            currentCar.CarClass = SelectClass;
                            currentCar.TransmissionType = SelectTType;
                            currentCar.Color = SelectColor;
                            currentCar.EngineSize = int.Parse(ValueTB.Text);
                            currentCar.CarPower = int.Parse(PowerTB.Text);
                            currentCar.CarNumber = CarNumberTB.Text;
                            currentCar.CarRegion = int.Parse(CarRegionTB.Text);
                            currentCar.Photo = _mainImageData;
                            App.Context.SaveChanges();
                            MessageBox.Show("Автомобиль изменен!", "Уведомление",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Произошла ошибка");
                    }
                }
                else
                {
                    try
                    {
                        var currentCar = App.Context.RentCar_Car.Find(DataStorage.carid);
                        if (currentCar != null)
                        {
                            currentCar.CarBrand = CarBrandTB.Text;
                            currentCar.CarModel = CarModelTB.Text;
                            currentCar.YearOfRelease = int.Parse(YTB.Text);
                            currentCar.RentCost = int.Parse(PriceTB.Text);
                            currentCar.CarClass = SelectClass;
                            currentCar.TransmissionType = SelectTType;
                            currentCar.Color = SelectColor;
                            currentCar.EngineSize = int.Parse(ValueTB.Text);
                            currentCar.CarPower = int.Parse(PowerTB.Text);
                            currentCar.CarNumber = CarNumberTB.Text;
                            currentCar.CarRegion = int.Parse(CarRegionTB.Text);
                            App.Context.SaveChanges();
                            MessageBox.Show("Автомобиль изменен!", "Уведомление",
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
        }
        private byte[] _mainImageData;

        private void SelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image |*.png; *.jpg; *.jpeg";
            if (ofd.ShowDialog() == true)
            {
                _mainImageData = File.ReadAllBytes(ofd.FileName);
                CarImage.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(_mainImageData);
            }
        }

        private void CarRegionTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}
