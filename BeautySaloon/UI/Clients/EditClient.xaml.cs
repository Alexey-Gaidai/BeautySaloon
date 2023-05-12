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
    /// Логика взаимодействия для EditClient.xaml
    /// </summary>
    public partial class EditClient : Window
    {
        private Data.Clients client;
        public EditClient()
        {
            InitializeComponent();
        }

        public EditClient(Data.Clients _client)
        {
            InitializeComponent();
            client = _client;
            Init();
        }

        private void Init()
        {
            IDTextBox.Text = client.ID.ToString();
            NameTextBox.Text = client.Name;
            PhoneNumberTextBox.Text = client.Phone_Number;
            NoteTextBox.Text = client.Note;
        }

        private void EditClient_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Data.Clients client = AppConnect.SaloonDB.Clients.Find(int.Parse(IDTextBox.Text));
                client.Name = NameTextBox.Text;
                client.Phone_Number = PhoneNumberTextBox.Text;
                client.Note = NoteTextBox.Text;
                AppConnect.SaloonDB.SaveChanges();
                this.DialogResult = true;
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
