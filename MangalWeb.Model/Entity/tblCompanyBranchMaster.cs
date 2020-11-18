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
    
    public partial class tblCompanyBranchMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCompanyBranchMaster()
        {
            this.Mst_UserBranch = new HashSet<Mst_UserBranch>();
        }
    
        public int BID { get; set; }
        public string BranchName { get; set; }
        public int CompID { get; set; }
        public string BranchCode { get; set; }
        public int BranchType { get; set; }
        public System.DateTime InceptionDate { get; set; }
        public Nullable<System.DateTime> RentPeriodAgreed { get; set; }
        public string Address { get; set; }
        public int Pincode { get; set; }
        public string ContactPerson { get; set; }
        public string MobileNo { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public Nullable<System.DateTime> DateWEF { get; set; }
        public Nullable<short> Status { get; set; }
        public Nullable<System.DateTime> RecordCreated { get; set; }
        public Nullable<System.DateTime> RecordUpdated { get; set; }
        public Nullable<int> RecordCreatedBy { get; set; }
        public Nullable<int> RecordUpdatedBy { get; set; }
    
        public virtual Mst_PinCode Mst_PinCode { get; set; }
        public virtual Mst_BranchType Mst_BranchType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mst_UserBranch> Mst_UserBranch { get; set; }
    }
}
