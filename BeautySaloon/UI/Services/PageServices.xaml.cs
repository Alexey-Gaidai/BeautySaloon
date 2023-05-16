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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeautySaloon.UI.Services
{
    /// <summary>
    /// Логика взаимодействия для PageServices.xaml
    /// </summary>
    public partial class PageServices : Page
    {
        public PageServices()
        {
            InitializeComponent();
            LoadServices();
        }
        //инициализация страницы
        private void LoadServices()
        {
            try
            {
                ServicesDataGrid.CanUserAddRows = false;
                ServicesDataGrid.ItemsSource = AppConnect.SaloonDB.Services.ToList();
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
        // добавление услуги
        private void ServiceAdd_Button_Click(object sender, RoutedEventArgs e)
        {
            AddService addService = new AddService();
            bool? result = addService.ShowDialog();

            if (result == true)
            {
                MessageBox.Show("Добавлено!");
                // обновление data grid view
                LoadServices();
            }
            else
            {
                MessageBox.Show("Не добавлено!");
            }
        }

        // редактирование выбранной услуги 
        private void ServiceEdit_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // получение выбранной услуги
                Data.Services selectedService = (Data.Services)ServicesDataGrid.SelectedItem;
                // проверка на выбор услуги
                if (selectedService == null)
                {
                    MessageBox.Show("Выберите услугу!");
                    return;
                }
                // открытие окна редактирования услуги
                EditService editService = new EditService(selectedService);
                bool? result = editService.ShowDialog();

                if (result == true)
                {
                    MessageBox.Show("Изменено!");
                    // обновление data grid view
                    LoadServices();
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

        private void ServiceDelete_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // поиск записи по id
                int id;
                if (ServicesDataGrid.SelectedItems.Count != 0 && ServicesDataGrid.SelectedItems.Count < 2)
                {
                    id = (ServicesDataGrid.SelectedItem as Data.Services).ID;
                }
                else
                {
                    MessageBox.Show("Выберите запись!");
                    return;
                }
                var service = AppConnect.SaloonDB.Services.Find(id);
                // удаление записи из базы данных
                AppConnect.SaloonDB.Services.Remove(service);
                AppConnect.SaloonDB.SaveChanges();
                // обновление data grid view
                LoadServices();
                MessageBox.Show("Удалено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
