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

namespace BeautySaloon.UI.Consumables
{
    /// <summary>
    /// Логика взаимодействия для AddConsumables.xaml
    /// </summary>
    public partial class AddConsumables : Window
    {
        List<Data.Consumable_Type> types;
        public AddConsumables()
        {
            InitializeComponent();
            types = AppConnect.SaloonDB.Consumable_Type.ToList();
            TypeComboBox.ItemsSource = types;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // создание новой услуги
                Data.Consumables consumable = new Data.Consumables
                {
                    Type_ID = types.Find(t => t.Name == TypeComboBox.Text).ID,
                    Model = ModelTextBox.Text,
                    Brand = BrandTextBox.Text,
                    Quantity = Convert.ToInt32(QuantityTextBox.Text),
                    Date_Added = DateTime.Now,
                };
                // добавление услуги в базу данных
                Data.Consumables.AddConsumable(consumable);
                // закрытие окна
                DialogResult = true;
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
