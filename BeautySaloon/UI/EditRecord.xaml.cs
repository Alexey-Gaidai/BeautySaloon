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

namespace BeautySaloon.UI
{
    /// <summary>
    /// Логика взаимодействия для EditRecord.xaml
    /// </summary>
    public partial class EditRecord : Window
    {
        private Record_Log record;
        private List<Data.Clients> clients;
        private List<Data.Masters> masters;
        private List<Data.Services> services;

        public EditRecord()
        {
            InitializeComponent();
        }
        public EditRecord(Record_Log _record)
        {
            record = _record;
            InitializeComponent();
            InitLists();
            InitComboBoxes();
            
            Init();
        }

        private void InitLists()
        {
            try
            {
                masters = AppConnect.SaloonDB.Masters.ToList();
                clients = AppConnect.SaloonDB.Clients.ToList();
                services = AppConnect.SaloonDB.Services.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitComboBoxes()
        {
            PaymentTypeComboBox.Items.Add("Cash");
            PaymentTypeComboBox.Items.Add("Debit Card");
            PaymentTypeComboBox.SelectedValue = PaymentTypeComboBox.Items[0];
            ClientComboBox.ItemsSource = clients;
            MasterComboBox.ItemsSource = masters;
            ServiceComboBox.ItemsSource = services;
        }

    // инициализация полей и текстбоксов
    private void Init()
        {
            try
            {
                Client_Services client_Services = new Client_Services();
                client_Services = Data.Client_Services.GetClientServicesByID(record.Client_Service_ID);
                IdTextBox.Text = record.ID.ToString();
                ClientComboBox.SelectedItem = clients.Find(c => c.ID == client_Services.Client_ID);
                ServiceComboBox.SelectedItem = services.Find(s => s.ID == client_Services.Service_ID);
                MasterComboBox.SelectedItem = masters.Find(m => m.ID == record.Master_ID);
                DateDatePicker.Text = record.Date.ToString();
                TimeTextBox.Text = record.Time.ToString();
                PaymentTypeComboBox.SelectedValue = record.Payment_Type.ToString();
                MaterialCostTextBox.Text = record.Material_Cost.ToString();
                NoteTextBox.Text = record.Note.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // редактирвание записи
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // получение записи из базы данных
                Client_Services client_Services = Data.Client_Services.GetClientServicesByID(record.Client_Service_ID);

                Data.Clients selectedClient = ClientComboBox.SelectedItem as Data.Clients;
                Data.Services selectedservice = ServiceComboBox.SelectedItem as Data.Services;
                Data.Masters selectedMaster = MasterComboBox.SelectedItem as Data.Masters;

                // изменение записи
                client_Services.Client_ID = selectedClient.ID;
                client_Services.Service_ID = selectedservice.ID;
                record.Date = DateTime.Parse(DateDatePicker.Text);
                record.Time = TimeSpan.Parse(TimeTextBox.Text);
                record.Master_ID = selectedMaster.ID;
                record.Payment_Type = PaymentTypeComboBox.SelectedValue.ToString();
                record.Material_Cost = decimal.Parse(MaterialCostTextBox.Text);
                record.Note = NoteTextBox.Text;
                // сохранение изменений
                Data.Record_Log.UpdateRecordLog(record);
                // закрытие окна
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
