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
    /// Логика взаимодействия для EditConsumable.xaml
    /// </summary>
    public partial class EditConsumable : Window
    {
        private Data.Consumables consumable;
        private List<Data.Consumable_Type> types;
        public EditConsumable()
        {
            InitializeComponent();
        }

        public EditConsumable(Data.Consumables _consumable)
        {
            consumable = _consumable;
            types = AppConnect.SaloonDB.Consumable_Type.ToList();
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            try
            {
                IDTextBox.Text = consumable.ID.ToString();
                TypeComboBox.ItemsSource = types;
                TypeComboBox.SelectedItem = types.Find(t => t.Name == consumable.Consumable_Type.Name);
                ModelTextBox.Text = consumable.Model;
                BrandTextBox.Text = consumable.Brand;
                QuantityTextBox.Text = consumable.Quantity.ToString();
                DatePicker.SelectedDate = consumable.Date_Added;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                consumable.Type_ID = types.Find(t => t.Name == TypeComboBox.Text).ID;
                consumable.Model = ModelTextBox.Text;
                consumable.Brand = BrandTextBox.Text;
                consumable.Quantity = Convert.ToInt32(QuantityTextBox.Text);
                consumable.Date_Added = DatePicker.SelectedDate.Value;
                Data.Consumables.UpdateConsumable(consumable);
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
