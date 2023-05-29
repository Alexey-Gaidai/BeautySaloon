//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BeautySaloon.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;

    public partial class Salary
    {
        public int ID { get; set; }
        public System.DateTime Date { get; set; }
        public int Master_ID { get; set; }
        public int Service_ID { get; set; }
        public int Record_ID { get; set; }
        public decimal Material_Cost { get; set; }
        public decimal Master_Salary { get; set; }
        public decimal Salon_Revenue { get; set; }
    
        public virtual Masters Masters { get; set; }
        public virtual Record_Log Record_Log { get; set; }
        public virtual Services Services { get; set; }

        public static void AddSalary(Salary salary)
        {
            try
            {
                AppConnect.SaloonDB.Salary.Add(salary);
                AppConnect.SaloonDB.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void UpdateSalary(Salary salary)
        {
            try
            {
                var existingSalary = AppConnect.SaloonDB.Salary.Find(salary.ID);
                if (existingSalary != null)
                {
                    // Обновление свойств заработной платы
                    decimal cost = AppConnect.SaloonDB.Services.Where(x => x.ID == salary.Service_ID).Select(x => x.Service_Cost).FirstOrDefault();
                    decimal revenue = cost - salary.Material_Cost;
                    decimal masterSalary = revenue * 0.5m;
                    decimal saloonRevenue = revenue - masterSalary;
                    existingSalary.Master_ID = salary.Master_ID;
                    existingSalary.Date = salary.Date;
                    existingSalary.Service_ID = salary.Service_ID;
                    existingSalary.Material_Cost = salary.Material_Cost;
                    existingSalary.Master_Salary = masterSalary;
                    existingSalary.Salon_Revenue = saloonRevenue;
                    existingSalary.Record_ID = salary.Record_ID;

                    AppConnect.SaloonDB.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void DeleteSalary(int recordId)
        {
            try
            {
                var salary = AppConnect.SaloonDB.Salary.Where(s => s.Record_ID == recordId).FirstOrDefault();
                if (salary != null)
                {
                    AppConnect.SaloonDB.Salary.Remove(salary);
                    AppConnect.SaloonDB.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static List<Data.Salary> GetSalaryRecordsByMasterId(DateTime startDate, DateTime endDate, int ID)
        {
            return AppConnect.SaloonDB.Salary
                .Where(s => s.Master_ID == ID && s.Date >= startDate && s.Date <= endDate)
                .ToList();
        }
    }
}
