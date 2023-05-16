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

namespace BeautySaloon.UI.Salary
{
    /// <summary>
    /// Логика взаимодействия для PageSalary.xaml
    /// </summary>
    public partial class PageSalary : Page
    {
        List<Data.Salary> rawSalaries;
        List<Data.Masters> Masters;
        public PageSalary()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                rawSalaries = AppConnect.SaloonDB.Salary.ToList();
                Masters = AppConnect.SaloonDB.Masters.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private List<Salaries> CalcSalaries()
        {
            List<Salaries> salaries = new List<Salaries>();

            // Получите данные из базы данных и рассчитайте зарплаты

            // Пример расчета зарплаты для каждого мастера
            foreach (Data.Masters master in Masters)
            {
                // Получите записи зарплаты из базы данных для данного мастера
                List<Data.Salary> salaryRecords = GetSalaryRecordsForMaster(master.ID);

                // Рассчитайте общее количество услуг, стоимость материалов и зарплату мастера
                int totalNumberOfServices = salaryRecords.Count();
                decimal totalMaterialCost = salaryRecords.Sum(record => record.Material_Cost);
                decimal totalSalonRevenue = salaryRecords.Sum(record => record.Salon_Revenue);
                decimal totalMasterSalary = salaryRecords.Sum(record => record.Master_Salary);

                // Создайте объект Salary и добавьте его в список зарплат
                Salaries salary = new Salaries
                {
                    MasterName = master.Name,
                    NumberOfServices = totalNumberOfServices,
                    MaterialCost = totalMaterialCost,
                    MasterSalary = totalMasterSalary,
                    SalonRevenue = totalSalonRevenue
                };

                salaries.Add(salary);
            }

            return salaries;
        }

        private List<Data.Salary> GetSalaryRecordsForMaster(int iD)
        {
            DateTime startDate = startDatePicker.SelectedDate.Value;
            DateTime endDate = endDatePicker.SelectedDate.Value;
            return rawSalaries.Where(s => s.Master_ID == iD && s.Date <= endDate && s.Date >= startDate).ToList();
        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            SalaryDataGrid.ItemsSource = CalcSalaries();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.GoBack();
        }

        private void Calc_Button_Click(object sender, RoutedEventArgs e)
        {
            SalaryDataGrid.ItemsSource = CalcSalaries();
        }
    }
}
