using BeautySaloon.Data;
using BeautySaloon.Model;
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

namespace BeautySaloon.UI
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        List<Data.Clients> clients;
        List<Data.Masters> masters;
        List<Data.Services> services;
        public MainPage()
        {
            InitializeComponent();
            LoadRecords();
        }

        private void LoadRecords()
        {
            try
            {
                clients = AppConnect.SaloonDB.Clients.ToList();
                masters = AppConnect.SaloonDB.Masters.ToList();
                services = AppConnect.SaloonDB.Services.ToList();

                List<Records> records = GetRecords();
                //очистка data grid view
                RecordLogDataGrid.ItemsSource = null;
                RecordLogDataGrid.CanUserAddRows = false;
                // загрузка данных из базы данных
                RecordLogDataGrid.ItemsSource = records;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private List<Records> GetRecords()
        {
            List<Records> records = new List<Records>();

            try
            {
                var query = from log in AppConnect.SaloonDB.Record_Log
                            join clientService in AppConnect.SaloonDB.Client_Services on log.Client_Service_ID equals clientService.ID
                            join client in AppConnect.SaloonDB.Clients on clientService.Client_ID equals client.ID
                            join service in AppConnect.SaloonDB.Services on clientService.Service_ID equals service.ID
                            join master in AppConnect.SaloonDB.Masters on log.Master_ID equals master.ID
                            select new Records
                            {
                                ID = log.ID,
                                Date = log.Date,
                                Time = log.Time,
                                ClientName = client.Name,
                                ClientPhone = client.Phone_Number,
                                ServiceName = service.Service_Name,
                                MasterName = master.Name,
                                PaymentType = log.Payment_Type,
                                ServiceCost = service.Service_Cost,
                                MaterialCost = log.Material_Cost,
                                Note = log.Note
                            };

                records = query.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return records;
        }

        //
        private void AddRecord_Button_Click(object sender, RoutedEventArgs e)
        {
            AddRecord addRecord = new AddRecord();
            bool? result = addRecord.ShowDialog();

            if (result == true)
            {
                MessageBox.Show("Добавлено!");
                // обновление data grid view
                LoadRecords();
            }
            else
            {
                MessageBox.Show("Не добавлено!");
            }
        }

        private void DeleteRecord_Button_Click(object sender, RoutedEventArgs e)
        {
            // удаление выбранной в data grid view записи
            try
            {
                // поиск записи по id
                int id;
                if (RecordLogDataGrid.SelectedItems.Count != 0 && RecordLogDataGrid.SelectedItems.Count < 2)
                {
                    id = (RecordLogDataGrid.SelectedItem as Records).ID;
                }
                else
                {
                    MessageBox.Show("Выберите запись!");
                    return;
                }
                Data.Record_Log.DeleteRecordLog(id);
                // удаление записи из базы данных
                // обновление data grid view
                LoadRecords();
                MessageBox.Show("Удалено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // редактирование записи выбранной записи
        private void Editrecord_Button_Click(object sender, RoutedEventArgs e)
        {
            // поиск записи по id
            int id;
            if (RecordLogDataGrid.SelectedItems.Count != 0 && RecordLogDataGrid.SelectedItems.Count < 2)
            {
                id = (RecordLogDataGrid.SelectedItem as Records).ID;
            }
            else
            {
                MessageBox.Show("Выберите запись!");
                return;
            }
            var record = AppConnect.SaloonDB.Record_Log.Find(id);
            // открытие окна редактирования
            EditRecord editRecord = new EditRecord(record);
            bool? result = editRecord.ShowDialog();
            if (result == true)
            {
                // обновление data grid view
                LoadRecords();
                MessageBox.Show("Изменено!");
            }
            else
            {
                MessageBox.Show("Не изменено!");
            }

        }

        private void Consumables_Button_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageConsumables());
        }

        private void Services_Button_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageServices());
        }

        private void Clients_Button_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageClients());
        }

        private void Masters_Button_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageMasters());
        }

        private void Salarys_Button_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageSalary());
        }
    }
}
