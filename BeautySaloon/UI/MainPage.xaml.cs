using BeautySaloon.Data;
using BeautySaloon.UI.Clients;
using BeautySaloon.UI.Consumables;
using BeautySaloon.UI.Masters;
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

namespace BeautySaloon.UI
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            LoadRecords();
        }

        private void LoadRecords()
        {
            try
            {
                //очистка data grid view
                RecordLogDataGrid.ItemsSource = null;
                // загрузка данных из базы данных
                var records = AppConnect.SaloonDB.Record_Log.ToList();
                RecordLogDataGrid.ItemsSource = records;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                    id = (RecordLogDataGrid.SelectedItem as Record_Log).ID;
                }
                else
                {
                    MessageBox.Show("Выберите запись!");
                    return;
                }
                var record = AppConnect.SaloonDB.Record_Log.Find(id);
                // удаление записи из базы данных
                AppConnect.SaloonDB.Record_Log.Remove(record);
                AppConnect.SaloonDB.SaveChanges();
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
                id = (RecordLogDataGrid.SelectedItem as Record_Log).ID;
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
            
        }
    }
}
