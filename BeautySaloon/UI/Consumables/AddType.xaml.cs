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
    /// Логика взаимодействия для AddType.xaml
    /// </summary>
    public partial class AddType : Window
    {
        public AddType()
        {
            InitializeComponent();
        }

        private void AddType_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // создание новой услуги
                Data.Consumable_Type type = new Data.Consumable_Type
                {
                    Name = typeNameTextBox.Text,
                };
                // добавление услуги в базу данных
                Data.Consumable_Type.AddConsumableType(type);
                // закрытие окна
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
