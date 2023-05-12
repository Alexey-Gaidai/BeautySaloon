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
    /// Логика взаимодействия для EditMaster.xaml
    /// </summary>
    public partial class EditMaster : Window
    {
        private Data.Masters master;
        public EditMaster()
        {
            InitializeComponent();
            Init();
        }

        public EditMaster(Data.Masters _master)
        {
            InitializeComponent();
            master = _master;
            Init();
        }

        private void Init()
        {
            IDTextBox.Text = master.ID.ToString();
            NameTextBox.Text = master.Name;
            NoteTextBox.Text = master.Note;
        }

        private void AddMaster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Data.Masters master = AppConnect.SaloonDB.Masters.Find(int.Parse(IDTextBox.Text));
                master.Name =  NameTextBox.Text;
                master.Note = NoteTextBox.Text;
                AppConnect.SaloonDB.SaveChanges();
                this.DialogResult = true;
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
