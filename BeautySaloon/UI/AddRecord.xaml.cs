using BeautySaloon.Data;
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

        public AddRecord()
        {
            InitializeComponent();
            PaymentTypeComboBox.Items.Add("Cash");
            PaymentTypeComboBox.Items.Add("Debit Card");
            PaymentTypeComboBox.SelectedValue = PaymentTypeComboBox.Items[0];
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Record_Log record = new Record_Log();
                Client_Services clientServices = new Client_Services();
                Data.Salary salary = new Data.Salary();

                clientServices.Client_ID = Convert.ToInt32(ClientIdTextBox.Text);
                clientServices.Service_ID = Convert.ToInt32(ServiceIdTextBox.Text);
                AppConnect.SaloonDB.Client_Services.Add(clientServices);
                AppConnect.SaloonDB.SaveChanges();
                MessageBox.Show(clientServices.ID.ToString());
                
                record.Date = DateDatePicker.SelectedDate.Value;
                record.Time = TimeSpan.Parse(TimeTextBox.Text);
                record.Client_Service_ID = clientServices.ID;
                record.Master_ID = Convert.ToInt32(MasterIdTextBox.Text);
                record.Payment_Type = PaymentTypeComboBox.SelectedValue.ToString();
                record.Material_Cost = decimal.Parse(MaterialCostTextBox.Text);
                record.Note = NoteTextBox.Text;
                AppConnect.SaloonDB.Record_Log.Add(record);
                AppConnect.SaloonDB.SaveChanges();
                

                salary.Master_ID = Convert.ToInt32(MasterIdTextBox.Text);
                salary.Date = DateDatePicker.SelectedDate.Value;
                salary.Material_Cost = decimal.Parse(MaterialCostTextBox.Text);
                decimal cost = AppConnect.SaloonDB.Services.Where(x => x.ID == clientServices.Service_ID).Select(x => x.Service_Cost).FirstOrDefault();
                decimal revenue = cost - decimal.Parse(MaterialCostTextBox.Text);
                decimal masterSalary = revenue * 0.3m;
                decimal saloonRevenue = revenue - masterSalary;
                salary.Service_ID = clientServices.Service_ID;
                salary.Master_Salary = masterSalary;
                salary.Salon_Revenue = saloonRevenue;

                AppConnect.SaloonDB.Salary.Add(salary);
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
