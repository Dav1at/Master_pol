//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Master_pol.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Partners
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Partners()
        {
            this.Sale_history = new HashSet<Sale_history>();
        }
    
        public int id { get; set; }
        public string type_partner { get; set; }
        public string name { get; set; }
        public string contact_person { get; set; }
        public long phonne { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public Nullable<long> inn { get; set; }
        public int reiting { get; set; }
        public Nullable<int> discount { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale_history> Sale_history { get; set; }
    }
}
