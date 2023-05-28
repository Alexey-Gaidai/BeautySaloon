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

    public partial class Clients
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clients()
        {
            this.Client_Services = new HashSet<Client_Services>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone_Number { get; set; }
        public string Note { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Client_Services> Client_Services { get; set; }

        public static void AddClient(Clients client)
        {
            try
            {
                AppConnect.SaloonDB.Clients.Add(client);
                AppConnect.SaloonDB.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void UpdateClient(Clients client)
        {
            try
            {
                var existingClient = AppConnect.SaloonDB.Clients.Find(client.ID);
                if (existingClient != null)
                {
                    // обновление свойств клиента
                    existingClient.Name = client.Name;
                    existingClient.Phone_Number = client.Phone_Number;
                    existingClient.Note = client.Note;

                    AppConnect.SaloonDB.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void DeleteClient(int id)
        {
            try
            {
                var client = AppConnect.SaloonDB.Clients.Find(id);
                if (client != null)
                {
                    AppConnect.SaloonDB.Clients.Remove(client);
                    AppConnect.SaloonDB.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static Clients GetClientByID(int id)
        {
            try
            {
                return AppConnect.SaloonDB.Clients.FirstOrDefault(c => c.ID == id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
