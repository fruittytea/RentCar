using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
using OfficeOpenXml;
using System.ComponentModel;

namespace RentCar
{
    /// <summary>
    /// Interaction logic for ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        public ReportPage()
        {
            InitializeComponent();
        }
        DateTime startdate;
        DateTime finishdate;
        public static int QuantityRentStart { get; set; }
        public static int QuantityRentMoney { get; set; }

        private void ToFormBtn_Click(object sender, RoutedEventArgs e)
        {
            var nowdate = DateTime.Now;
            if (nowdate > FinishDateTB.SelectedDate)
            {
                if (StartDateDP.SelectedDate.HasValue)
                    startdate = StartDateDP.SelectedDate.Value;
                else
                    MessageBox.Show("Пожалуйста, заполните дату начала периода!");

                if (FinishDateTB.SelectedDate.HasValue)
                    finishdate = FinishDateTB.SelectedDate.Value;
                else
                    MessageBox.Show("Пожалуйста, заполните дату окончания периода!");

                var QRentS = App.Context.RentCar_RentalAgreement
                    .Where(p => p.AgreementStartDate >= startdate && p.AgreementStartDate <= finishdate)
                    .Count();
                QuantityRentStart = QRentS;

                var rentalData = (from agreement in App.Context.RentCar_RentalAgreement
                                  join car in App.Context.RentCar_Car on agreement.CarId equals car.CarId
                                  where agreement.AgreementStartDate >= startdate && agreement.AgreementStartDate <= finishdate
                                  select new
                                  {
                                      AgreementFinishDate = agreement.AgreementFinishDate, // Убедитесь, что это DateTime?
                                      AgreementStartDate = agreement.AgreementStartDate,
                                      RentCost = car.RentCost // Убедитесь, что это int или int?
                                  }).ToList(); // Получаем данные в список

                double totalEarnings = 0;

                foreach (var data in rentalData)
                {
                    double rentalDays;

                    // Проверяем, есть ли значение AgreementFinishDate
                    if (data.AgreementFinishDate != null) // Проверка на null
                    {
                        rentalDays = (finishdate - data.AgreementStartDate).TotalDays;
                    }
                    else
                    {
                        rentalDays = (DateTime.Now - data.AgreementStartDate).TotalDays; // Если нет, считаем до текущей даты
                    }

                    totalEarnings += rentalDays * data.RentCost;
                    QuantityRentMoney = Convert.ToInt32(totalEarnings);
                }


                string filePath = "Отчет за период.xlsx";
                CreateExcelDocument(filePath, startdate, finishdate);

                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show("Дата окончания периода еще не наступила! Пожалуйста, выберите другую!");
            }
        }

        private static void CreateExcelDocument(string filepath, DateTime startdate, DateTime finishdate)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage())
            {
                // Создание нового рабочего листа
                var worksheet = package.Workbook.Worksheets.Add("Даты");

                // Заполнение ячеек
                worksheet.Cells[1, 1].Value = "Дата начала";
                worksheet.Cells[1, 2].Value = startdate.ToString("dd.MM.yyyy");

                worksheet.Cells[2, 1].Value = "Дата окончания";
                worksheet.Cells[2, 2].Value = finishdate.ToString("dd.MM.yyyy");

                worksheet.Cells[4, 1].Value = "За этот период было заключено";
                worksheet.Cells[4, 2].Value = QuantityRentStart.ToString();
                worksheet.Cells[4, 3].Value = "договоров";

                worksheet.Cells[5, 1].Value = "За этот период было заработано";
                worksheet.Cells[5, 2].Value = QuantityRentMoney.ToString();
                worksheet.Cells[5, 3].Value = "рублей";


                // Сохранение файла
                package.SaveAs(new FileInfo(filepath));
            }
        }
    }
}
