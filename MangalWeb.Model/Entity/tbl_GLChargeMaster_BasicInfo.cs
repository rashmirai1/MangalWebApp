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
    
    public partial class tbl_GLChargeMaster_BasicInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_GLChargeMaster_BasicInfo()
        {
            this.tbl_GLChargeMaster_Details = new HashSet<tbl_GLChargeMaster_Details>();
        }
    
        public int CID { get; set; }
        public string ChargeName { get; set; }
        public System.DateTime ReferenceDate { get; set; }
        public string Status { get; set; }
        public int FYID { get; set; }
        public int BranchID { get; set; }
        public Nullable<int> CMPId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_GLChargeMaster_Details> tbl_GLChargeMaster_Details { get; set; }
    }
}
