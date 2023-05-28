using BeautySaloon.Data;
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

namespace BeautySaloon.UI.Masters
{
    /// <summary>
    /// Логика взаимодействия для PageMasters.xaml
    /// </summary>
    public partial class PageMasters : Page
    {
        public PageMasters()
        {
            InitializeComponent();
            LoadMasters();
        }

        private void LoadMasters()
        {
            try
            {
                MastersDataGrid.CanUserAddRows = false;
                MastersDataGrid.ItemsSource = AppConnect.SaloonDB.Masters.ToList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.GoBack();
        }

        private void MastersDelete_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // поиск записи по id
                int id;
                if (MastersDataGrid.SelectedItems.Count != 0 && MastersDataGrid.SelectedItems.Count < 2)
                {
                    id = (MastersDataGrid.SelectedItem as Data.Masters).ID;
                }
                else
                {
                    MessageBox.Show("Выберите мастера!");
                    return;
                }
                // удаление записи из базы данных
                Data.Masters.DeleteMaster(id);
                // обновление data grid view
                LoadMasters();
                MessageBox.Show("Удалено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MastersAdd_Button_Click(object sender, RoutedEventArgs e)
        {
            AddMaster addMaster = new AddMaster();
            bool? result = addMaster.ShowDialog();

            if (result == true)
            {
                MessageBox.Show("Добавлено!");
                // обновление data grid view
                LoadMasters();
            }
            else
            {
                MessageBox.Show("Не добавлено!");
            }
        }

        private void MastersEdit_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Data.Masters selectedMaster = (Data.Masters)MastersDataGrid.SelectedItem;
                // проверка на выбор услуги
                if (selectedMaster == null)
                {
                    MessageBox.Show("Выберите услугу!");
                    return;
                }
                // открытие окна редактирования услуги
                EditMaster editMaster = new EditMaster(selectedMaster);
                bool? result = editMaster.ShowDialog();

                if (result == true)
                {
                    MessageBox.Show("Изменено!");
                    // обновление data grid view
                    LoadMasters();
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
    }
}
