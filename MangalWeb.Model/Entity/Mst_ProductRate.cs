//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MangalWeb.Model.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Mst_ProductRate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Mst_ProductRate()
        {
            this.Mst_ProductRateDetails = new HashSet<Mst_ProductRateDetails>();
        }
    
        public int Pr_Id { get; set; }
        public System.DateTime Pr_Date { get; set; }
        public int Pr_Product { get; set; }
        public Nullable<System.DateTime> Pr_RecordCreated { get; set; }
        public Nullable<System.DateTime> Pr_RecordUpdated { get; set; }
        public Nullable<int> Pr_RecordCreatedBy { get; set; }
        public Nullable<int> Pr_RecordUpdatedBy { get; set; }
    
        public virtual Mst_Product Mst_Product { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mst_ProductRateDetails> Mst_ProductRateDetails { get; set; }
    }
}