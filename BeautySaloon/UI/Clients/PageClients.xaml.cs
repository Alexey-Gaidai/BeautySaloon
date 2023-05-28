using BeautySaloon.Data;
using BeautySaloon.UI.Masters;
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

namespace BeautySaloon.UI.Clients
{
    /// <summary>
    /// Логика взаимодействия для PageClients.xaml
    /// </summary>
    public partial class PageClients : Page
    {
        public PageClients()
        {
            InitializeComponent();
            LoadClients();
        }

        private void LoadClients()
        {
            try
            {
                ClientsDataGrid.CanUserAddRows = false;
                ClientsDataGrid.ItemsSource = AppConnect.SaloonDB.Clients.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddClient_Button_Click(object sender, RoutedEventArgs e)
        {
            AddClient addClient = new AddClient();
            bool? result = addClient.ShowDialog();

            if (result == true)
            {
                MessageBox.Show("Добавлено!");
                // обновление data grid view
                LoadClients();
            }
            else
            {
                MessageBox.Show("Не добавлено!");
            }
        }

        private void EditClient_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Data.Clients selectedClient = (Data.Clients)ClientsDataGrid.SelectedItem;
                // проверка на выбор услуги
                if (selectedClient == null)
                {
                    MessageBox.Show("Выберите услугу!");
                    return;
                }
                // открытие окна редактирования услуги
                EditClient editClient = new EditClient(selectedClient);
                bool? result = editClient.ShowDialog();

                if (result == true)
                {
                    MessageBox.Show("Изменено!");
                    // обновление data grid view
                    LoadClients();
                }
                else
                {
                    MessageBox.Show("Не изменено!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteClient_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // поиск записи по id
                int id;
                if (ClientsDataGrid.SelectedItems.Count != 0 && ClientsDataGrid.SelectedItems.Count < 2)
                {
                    id = (ClientsDataGrid.SelectedItem as Data.Clients).ID;
                }
                else
                {
                    MessageBox.Show("Выберите клиента!");
                    return;
                }
                // удаление записи из базы данных
                Data.Clients.DeleteClient(id);
                // обновление data grid view
                LoadClients();
                MessageBox.Show("Удалено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.GoBack();
        }
    }
}
