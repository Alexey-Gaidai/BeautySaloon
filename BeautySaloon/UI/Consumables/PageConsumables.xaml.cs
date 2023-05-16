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

namespace BeautySaloon.UI.Consumables
{
    /// <summary>
    /// Логика взаимодействия для PageConsumables.xaml
    /// </summary>
    public partial class PageConsumables : Page
    {
        public PageConsumables()
        {
            InitializeComponent();
            LoadConsumables();
        }

        private void LoadConsumables()
        {
            try
            {
                ConsumablesDataGrid.CanUserAddRows = false;
                ConsumablesDataGrid.ItemsSource = AppConnect.SaloonDB.Consumables.ToList();
                TypesDataGrid.CanUserAddRows = false;
                TypesDataGrid.ItemsSource = AppConnect.SaloonDB.Consumable_Type.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddConsumable_Button_Click(object sender, RoutedEventArgs e)
        {
            AddConsumables addConsumables = new AddConsumables();
            bool? result = addConsumables.ShowDialog();

            if (result == true)
            {
                MessageBox.Show("Добавлено!");
                // обновление data grid view
                LoadConsumables();
            }
            else
            {
                MessageBox.Show("Не добавлено!");
            }
        }

        private void EditConsumable_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Data.Consumables selectedConsumable = (Data.Consumables)ConsumablesDataGrid.SelectedItem;
                // проверка на выбор услуги
                if (selectedConsumable == null)
                {
                    MessageBox.Show("Выберите материал!");
                    return;
                }
                // открытие окна редактирования услуги
                EditConsumable editConsumable = new EditConsumable(selectedConsumable);
                bool? result = editConsumable.ShowDialog();

                if (result == true)
                {
                    MessageBox.Show("Изменено!");
                    // обновление data grid view
                    LoadConsumables();
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

        private void DeleteConsumable_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // поиск записи по id
                int id;
                if (ConsumablesDataGrid.SelectedItems.Count != 0 && ConsumablesDataGrid.SelectedItems.Count < 2)
                {
                    id = (ConsumablesDataGrid.SelectedItem as Data.Consumables).ID;
                }
                else
                {
                    MessageBox.Show("Выберите материал!");
                    return;
                }
                var consumable = AppConnect.SaloonDB.Consumables.Find(id);
                // удаление записи из базы данных
                AppConnect.SaloonDB.Consumables.Remove(consumable);
                AppConnect.SaloonDB.SaveChanges();
                // обновление data grid view
                LoadConsumables();
                MessageBox.Show("Удалено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddType_Button_Click(object sender, RoutedEventArgs e)
        {
            AddType addType = new AddType();
            bool? result = addType.ShowDialog();

            if (result == true)
            {
                MessageBox.Show("Добавлено!");
                // обновление data grid view
                LoadConsumables();
            }
            else
            {
                MessageBox.Show("Не добавлено!");
            }
        }

        private void EditType_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Data.Consumable_Type selectedType = (Data.Consumable_Type)TypesDataGrid.SelectedItem;
                // проверка на выбор услуги
                if (selectedType == null)
                {
                    MessageBox.Show("Выберите услугу!");
                    return;
                }
                // открытие окна редактирования услуги
                EditType editType = new EditType(selectedType);
                bool? result = editType.ShowDialog();

                if (result == true)
                {
                    MessageBox.Show("Изменено!");
                    // обновление data grid view
                    LoadConsumables();
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

        private void DeleteType_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // поиск записи по id
                int id;
                if (TypesDataGrid.SelectedItems.Count != 0 && TypesDataGrid.SelectedItems.Count < 2)
                {
                    id = (TypesDataGrid.SelectedItem as Data.Consumable_Type).ID;
                }
                else
                {
                    MessageBox.Show("Выберите тип!");
                    return;
                }
                var type = AppConnect.SaloonDB.Consumable_Type.Find(id);
                // удаление записи из базы данных
                AppConnect.SaloonDB.Consumable_Type.Remove(type);
                AppConnect.SaloonDB.SaveChanges();
                // обновление data grid view
                LoadConsumables();
                MessageBox.Show("Удалено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.GoBack();
        }
    }
}
