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
using Word = Microsoft.Office.Interop.Word;

namespace RentCar
{
    /// <summary>
    /// Interaction logic for OrderIsReady.xaml
    /// </summary>
    public partial class OrderIsReady : Page
    {
        public OrderIsReady()
        {
            InitializeComponent();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var SelectedClient = App.Context.RentCar_Client.Where(p => p.ClientId == DataStorage.ClientIdOrder).FirstOrDefault();
            var SelectedCar = App.Context.RentCar_Car.Where(p => p.CarId == DataStorage.CarIdOrder).FirstOrDefault();
            var SelectedEmployee = App.Context.RentCar_Employee.Where(p => p.EmployeeId == DataStorage.UserId).FirstOrDefault();

            var Days = Convert.ToInt32((DataStorage.orderfinish - DataStorage.orderstart).TotalDays)+1;
            int Price = SelectedCar.RentCost * Days;

            var Date = DateTime.Now.Date;

            // Создаем экземпляр приложения Word
            Word.Application wordApp = new Word.Application();
            Word.Document wordDoc = wordApp.Documents.Add();

            // Заполняем документ данными
            string title = "Договор на аренду автомобиля";
            string content = SelectedClient.Surname+" "+ SelectedClient.Name + " " + SelectedClient.Fathername + ", беру в аренду автомобиль "+
                SelectedCar.CarBrand + " " + SelectedCar.CarModel + " " + SelectedCar.CarNumber + "." + SelectedCar.CarRegion + " в сервисе для аренды автомобиля 'RentCar' " +
                "на срок с "+ DataStorage.orderstart.Day+"."+ DataStorage.orderstart.Month +"." + DataStorage.orderstart.Year + " по " +
                ""+ DataStorage.orderfinish.Day + "." + DataStorage.orderfinish.Month + "." + DataStorage.orderfinish.Year +". " +
                "\nОбязуюсь вернуть автомобиль в срок, не подвергать автомобиль опасности, следить за состоянием автомобиля. При несоблюдении правил обязуюсь оплатить" +
                " штраф в размере 10 000₽ и оплатить ремонт автомобиля (при поломке автотранспорта)." +
                "\nСтоимость аренды за период составляет "+ Price + "₽" +
                "\n\n"+ SelectedClient.Surname + " " + SelectedClient.Name + " " + SelectedClient.Fathername + " __________________" +
                "\n\n"+ SelectedEmployee.Surname + " " + SelectedEmployee.Name + " " + SelectedEmployee.Fathername + " __________________" +
                "\n" + Date.Day +"."+Date.Month+"."+Date.Year;

            // Добавляем заголовок
            Word.Paragraph titleParagraph = wordDoc.Content.Paragraphs.Add();
            titleParagraph.Range.Text = title;
            titleParagraph.Range.Font.Name = "Times New Roman"; // Устанавливаем шрифт
            titleParagraph.Range.Font.Bold = 1; // Жирный шрифт
            titleParagraph.Range.Font.Size = 16; // Размер шрифта
            titleParagraph.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter; // Выравнивание по центру
            titleParagraph.Range.InsertParagraphAfter();

            // Добавляем содержимое
            Word.Paragraph contentParagraph = wordDoc.Content.Paragraphs.Add();
            Word.Range range = contentParagraph.Range;

            // Устанавливаем текст
            range.Text = content;

            // Применяем форматирование к Range
            range.Font.Name = "Times New Roman"; // Устанавливаем шрифт
            range.Font.Bold = 0; // Обычный шрифт
            range.Font.Size = 14; // Размер шрифта
            range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify; // Выравнивание по ширине
            contentParagraph.Range.InsertParagraphAfter();

            // Отображаем документ пользователю
            wordApp.Visible = true;

            // После того как пользователь закончит редактирование, они могут сохранить документ
            // Вы можете добавить обработку для закрытия приложения Word, если это необходимо
        }
    }
}
