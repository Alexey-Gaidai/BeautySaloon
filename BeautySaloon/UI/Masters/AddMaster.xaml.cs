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

namespace BeautySaloon.UI.Masters
{
    /// <summary>
    /// Логика взаимодействия для AddMaster.xaml
    /// </summary>
    public partial class AddMaster : Window
    {
        public AddMaster()
        {
            InitializeComponent();
        }

        private void AddMaster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // создание нового мастера
                Data.Masters master = new Data.Masters
                {
                    Name = NameTextBox.Text,
                    Note = NoteTextBox.Text
                };
                // добавление мастера в базу данных
                Data.Masters.AddMaster(master);
                // закрытие окна
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
