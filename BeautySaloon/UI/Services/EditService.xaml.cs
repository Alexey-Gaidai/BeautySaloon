using BeautySaloon.Data;
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

namespace BeautySaloon.UI.Services
{
    /// <summary>
    /// Логика взаимодействия для EditService.xaml
    /// </summary>
    public partial class EditService : Window
    {
        private Data.Services service;
        public EditService()
        {
            InitializeComponent();
        }

        public EditService(Data.Services service)
        {
            InitializeComponent();
            this.service = service;
            Init();
        }

        private void Init()
        {
            ServiceIDTextBox.Text = service.ID.ToString();
            ServiceNameTextBox.Text = service.Service_Name;
            ServiceCostTextBox.Text = service.Service_Cost.ToString();
        }

        //сохранение изменений
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Data.Services service = AppConnect.SaloonDB.Services.Find(int.Parse(ServiceIDTextBox.Text));
                service.Service_Cost = decimal.Parse(ServiceCostTextBox.Text);
                service.Service_Name = ServiceNameTextBox.Text;
                AppConnect.SaloonDB.SaveChanges();
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
