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
    
    public partial class tbl_UserCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_UserCategory()
        {
            this.UserDetails = new HashSet<UserDetail>();
        }
    
        public int refid { get; set; }
        public string Name { get; set; }
        public Nullable<int> Organhieraechyid { get; set; }
        public Nullable<bool> status1 { get; set; }
    
        public virtual User_Category_Hierarchy User_Category_Hierarchy { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
