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
        public EditRecord()
        {
            InitializeComponent();
        }
        public EditRecord(Record_Log _record)
        {
            record = _record;
            InitializeComponent();
            PaymentTypeComboBox.Items.Add("Cash");
            PaymentTypeComboBox.Items.Add("Debit Card");
            Init();
        }

        // инициализация полей и текстбоксов
        private void Init()
        {
            try
            {
                Client_Services client_Services = new Client_Services();
                client_Services = AppConnect.SaloonDB.Client_Services.Find(record.Client_Service_ID);

                IdTextBox.Text = record.ID.ToString();
                ClientIdTextBox.Text = client_Services.Client_ID.ToString();
                ServiceIdTextBox.Text = client_Services.Service_ID.ToString();
                MasterIdTextBox.Text = record.Master_ID.ToString();
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
                Record_Log record = AppConnect.SaloonDB.Record_Log.Find(int.Parse(IdTextBox.Text));
                Client_Services client_Services = AppConnect.SaloonDB.Client_Services.Find(record.Client_Service_ID);
                // изменение записи
                client_Services.Client_ID = int.Parse(ClientIdTextBox.Text);
                client_Services.Service_ID = int.Parse(ServiceIdTextBox.Text);
                record.Date = DateTime.Parse(DateDatePicker.Text);
                record.Time = TimeSpan.Parse(TimeTextBox.Text);
                record.Payment_Type = PaymentTypeComboBox.SelectedValue.ToString();
                record.Material_Cost = decimal.Parse(MaterialCostTextBox.Text);
                record.Note = NoteTextBox.Text;
                // сохранение изменений
                AppConnect.SaloonDB.SaveChanges();
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
