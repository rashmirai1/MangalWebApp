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
    
    public partial class tblCityMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCityMaster()
        {
            this.Mst_PinCode = new HashSet<Mst_PinCode>();
        }
    
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int StateID { get; set; }
        public string CityStdCode { get; set; }
        public string CityBlackListed { get; set; }
    
        public virtual tblStateMaster tblStateMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mst_PinCode> Mst_PinCode { get; set; }
    }
}
