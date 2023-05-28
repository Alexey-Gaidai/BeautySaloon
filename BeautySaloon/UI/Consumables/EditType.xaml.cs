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
    /// Логика взаимодействия для EditType.xaml
    /// </summary>
    public partial class EditType : Window
    {
        private Data.Consumable_Type type;
        public EditType()
        {
            InitializeComponent();
        }

        public EditType(Data.Consumable_Type _type)
        {
            type = _type;
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            try
            {
                if (type != null)
                {
                    typeIDTextBox.Text = type.ID.ToString();
                    typeNameTextBox.Text = type.Name;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddType_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                type.Name = typeNameTextBox.Text;
                Data.Consumable_Type.UpdateConsumableType(type);
                this.DialogResult = true;
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
