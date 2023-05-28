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
    /// Логика взаимодействия для AddService.xaml
    /// </summary>
    public partial class AddService : Window
    {
        public AddService()
        {
            InitializeComponent();
        }

        private void AddService_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // создание новой услуги
                Data.Services service = new Data.Services()
                {
                    Service_Name = NameTextBox.Text,
                    Service_Cost = Convert.ToDecimal(CostTextBox.Text)
                };
                // добавление услуги в базу данных
                Data.Services.AddService(service);
                // закрытие окна
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
