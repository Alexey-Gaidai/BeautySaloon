using BeautySaloon.Data;
using BeautySaloon.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для AddRecord.xaml
    /// </summary>
    public partial class AddRecord : Window
    {
        private List<Data.Masters> masters;
        private List<Data.Clients> clients;
        private List<Data.Services> services;
        public AddRecord()
        {
            InitializeComponent();
            InitLists();
            InitComboBoxes();
            
            
        }

        private void InitLists()
        {
            try
            {
                masters = AppConnect.SaloonDB.Masters.ToList();
                clients = AppConnect.SaloonDB.Clients.ToList();
                services = AppConnect.SaloonDB.Services.ToList();
            }
            catch(Exception ex)
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

        private void fillTimeComboBox()
        {
            var selectedMaster = (MasterComboBox.SelectedItem as Data.Masters).Name;

            List<TimeSpan> availableTimes = new List<TimeSpan>();

            TimeSpan startTime = new TimeSpan(10, 0, 0);
            TimeSpan endTime = new TimeSpan(20, 0, 0);
            TimeSpan interval = TimeSpan.FromMinutes(30);

            for (TimeSpan time = startTime; time <= endTime; time += interval)
            {
                availableTimes.Add(time);
            }
            if (DateDatePicker.Text == "")
            {
                MessageBox.Show("Выберите дату!");
            }
            else
            {
                var recordsForThisDate = AppConnect.SaloonDB.Record_Log.Where(x => x.Date == DateDatePicker.SelectedDate.Value).ToList();
                MessageBox.Show(recordsForThisDate.Count.ToString());
                foreach (var item in recordsForThisDate)
                {
                    if (availableTimes.Contains(item.Time) && selectedMaster == item.Masters.Name)
                    {
                        availableTimes.Remove(item.Time);
                    }
                }

                TimeComboBox.ItemsSource = availableTimes;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                Record_Log record = new Record_Log();
                Client_Services clientServices = new Client_Services();
                Data.Salary salary = new Data.Salary();

                Data.Clients selectedClient = ClientComboBox.SelectedItem as Data.Clients;
                Data.Services selectedservice = ServiceComboBox.SelectedItem as Data.Services;
                Data.Masters selectedMaster = MasterComboBox.SelectedItem as Data.Masters;
                if (selectedClient != null && selectedservice != null)
                {
                    clientServices.Client_ID = selectedClient.ID;
                    clientServices.Service_ID = selectedservice.ID;
                }

                if (selectedClient != null && selectedservice != null && selectedMaster != null && ClientComboBox.Text != "" && ServiceComboBox.Text != "" && MasterComboBox.Text != "" && DateDatePicker.Text != "" && TimeComboBox.Text != "" && PaymentTypeComboBox.SelectedValue != null && MaterialCostTextBox.Text != "" && PaymentTypeComboBox.SelectedValue != null)
                {
                    Data.Client_Services.AddClientService(clientServices);

                    record.Date = DateDatePicker.SelectedDate.Value;
                    record.Time = TimeSpan.Parse(TimeComboBox.Text);
                    record.Client_Service_ID = clientServices.ID;
                    record.Master_ID = selectedMaster.ID;
                    record.Payment_Type = PaymentTypeComboBox.SelectedValue.ToString();
                    record.Note = NoteTextBox.Text;
                    Data.Record_Log.AddRecordLog(record);


                    salary.Master_ID = selectedMaster.ID;
                    salary.Date = DateDatePicker.SelectedDate.Value;
                    salary.Material_Cost = decimal.Parse(MaterialCostTextBox.Text);
                    salary.Record_ID = record.ID;
                    decimal cost = AppConnect.SaloonDB.Services.Where(x => x.ID == clientServices.Service_ID).Select(x => x.Service_Cost).FirstOrDefault();
                    decimal revenue = cost - decimal.Parse(MaterialCostTextBox.Text);
                    decimal masterSalary = revenue * 0.5m;
                    decimal saloonRevenue = revenue - masterSalary;
                    salary.Service_ID = clientServices.Service_ID;
                    salary.Master_Salary = masterSalary;
                    salary.Salon_Revenue = saloonRevenue;

                    Data.Salary.AddSalary(salary);

                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Заполните все поля!");
                }
                
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void MasterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            fillTimeComboBox();
        }

        private void DateDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            
           if(MasterComboBox.SelectedItem != null)
            {
                fillTimeComboBox();
            }
        }
    }
}
