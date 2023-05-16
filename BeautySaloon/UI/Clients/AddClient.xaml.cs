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

namespace BeautySaloon.UI.Clients
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        public AddClient()
        {
            InitializeComponent();
        }

        private void AddClient_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // создание новой услуги
                Data.Clients client = new Data.Clients()
                {
                    Name = NameTextBox.Text,
                    Phone_Number = PhoneNumberTextBox.Text,
                    Note = NoteTextBox.Text
                };
                // добавление услуги в базу данных
                AppConnect.SaloonDB.Clients.Add(client);
                AppConnect.SaloonDB.SaveChanges();
                // закрытие окна
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
