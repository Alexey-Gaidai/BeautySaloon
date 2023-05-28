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

    public partial class Consumables
    {
        public int ID { get; set; }
        public int Type_ID { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }
        public System.DateTime Date_Added { get; set; }
    
        public virtual Consumable_Type Consumable_Type { get; set; }

        // Метод добавления расходного материала
        public static void AddConsumable(Consumables consumable)
        {
            try
            {
                AppConnect.SaloonDB.Consumables.Add(consumable);
                AppConnect.SaloonDB.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Метод изменения расходного материала
        public static void UpdateConsumable(Consumables consumable)
        {
            try
            {
                var existingConsumable = AppConnect.SaloonDB.Consumables.Find(consumable.ID);
                if (existingConsumable != null)
                {
                    // Обновление свойств расходного материала
                    existingConsumable.Type_ID = consumable.Type_ID;
                    existingConsumable.Model = consumable.Model;
                    existingConsumable.Brand = consumable.Brand;
                    existingConsumable.Quantity = consumable.Quantity;
                    existingConsumable.Date_Added = consumable.Date_Added;

                    AppConnect.SaloonDB.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Метод удаления расходного материала
        public static void DeleteConsumable(int id)
        {
            try
            {
                var consumable = AppConnect.SaloonDB.Consumables.Find(id);
                if (consumable != null)
                {
                    AppConnect.SaloonDB.Consumables.Remove(consumable);
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
