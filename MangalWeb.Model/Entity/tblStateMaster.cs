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
    
    public partial class tblStateMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblStateMaster()
        {
            this.tblCityMasters = new HashSet<tblCityMaster>();
        }
    
        public int StateID { get; set; }
        public string StateName { get; set; }
        public int countryID { get; set; }
        public string StateCode { get; set; }
        public Nullable<int> CkycStateId { get; set; }
    
        public virtual tbl_CountryMaster tbl_CountryMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCityMaster> tblCityMasters { get; set; }
    }
}
