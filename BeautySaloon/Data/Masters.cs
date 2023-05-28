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
    using System.Windows;

    public partial class Masters
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Masters()
        {
            this.Record_Log = new HashSet<Record_Log>();
            this.Salary = new HashSet<Salary>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Record_Log> Record_Log { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Salary> Salary { get; set; }

        public static void AddMaster(Masters master)
        {
            try
            {
                AppConnect.SaloonDB.Masters.Add(master);
                AppConnect.SaloonDB.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Метод изменения мастера
        public static void UpdateMaster(Masters master)
        {
            try
            {
                var existingMaster = AppConnect.SaloonDB.Masters.Find(master.ID);
                if (existingMaster != null)
                {
                    // обновление свойств клиента
                    existingMaster.Name = master.Name;
                    existingMaster.Note = master.Note;

                    AppConnect.SaloonDB.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Метод удаления мастера
        public static void DeleteMaster(int id)
        {
            try
            {
                var master = AppConnect.SaloonDB.Masters.Find(id);
                if (master != null)
                {
                    AppConnect.SaloonDB.Masters.Remove(master);
                    AppConnect.SaloonDB.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
