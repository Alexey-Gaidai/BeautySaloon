using BeautySaloon.Data;
using BeautySaloon.UI;
using BeautySaloon.UI.Clients;
using BeautySaloon.UI.Consumables;
using BeautySaloon.UI.Masters;
using BeautySaloon.UI.Salary;
using BeautySaloon.UI.Services;
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

namespace BeautySaloon
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppConnect.SaloonDB = new BeautySaloonEntities();
            AppFrame.frameMain = FrmMain;
            FrmMain.Navigate(new MainPage());
        }

        private void FrmMain_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is PageSalary)
            {
                programTitle.Text = "Зарплаты мастеров";
            }
            else if (e.Content is PageClients)
            {
                programTitle.Text = "Наши клиенты";
            }
            else if (e.Content is PageServices)
            {
                programTitle.Text = "Наши услуги";
            }
            else if (e.Content is PageConsumables)
            {
                programTitle.Text = "Материалы для работы";
            }
            else if (e.Content is PageMasters)
            {
                programTitle.Text = "Мастера";
            }
            else
            {
                programTitle.Text = "Салон красоты";
            }
        }
    }
}
