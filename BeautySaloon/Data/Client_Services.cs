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
    
    public partial class Client_Services
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client_Services()
        {
            this.Record_Log = new HashSet<Record_Log>();
        }
    
        public int ID { get; set; }
        public int Client_ID { get; set; }
        public int Service_ID { get; set; }
        public string Note { get; set; }
    
        public virtual Clients Clients { get; set; }
        public virtual Services Services { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Record_Log> Record_Log { get; set; }
    }
}