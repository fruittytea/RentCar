using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RentCar
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Entities.user92_dbEntities Context { get; } = new Entities.user92_dbEntities();
        public static Entities.RentCar_Client currentClient = new Entities.RentCar_Client();
        public static Entities.RentCar_Employee currentUser = new Entities.RentCar_Employee();
    }
}
