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
    
    public partial class Mst_PurityMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Mst_PurityMaster()
        {
            this.TGLSanctionDisburse_GoldItemDetails = new HashSet<TGLSanctionDisburse_GoldItemDetails>();
        }
    
        public int id { get; set; }
        public string PurityName { get; set; }
        public Nullable<int> PurityType { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TGLSanctionDisburse_GoldItemDetails> TGLSanctionDisburse_GoldItemDetails { get; set; }
    }
}
