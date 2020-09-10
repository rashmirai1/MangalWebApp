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
    
    public partial class tbl_PreSanctionDetails
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_PreSanctionDetails()
        {
            this.tbl_ResidenceVerification = new HashSet<tbl_ResidenceVerification>();
        }
    
        public int Id { get; set; }
        public Nullable<int> KycId { get; set; }
        public string TransactionId { get; set; }
        public string NewTopUp { get; set; }
        public string CustomerId { get; set; }
        public Nullable<System.DateTime> AppliedDate { get; set; }
        public string ResidenceVerification { get; set; }
        public string ApplicationNo { get; set; }
        public string RM { get; set; }
        public string Product { get; set; }
        public string PurposeofLoan { get; set; }
        public Nullable<int> Scheme { get; set; }
        public string ReqLoanAmount { get; set; }
        public string Tenure { get; set; }
        public string ROI { get; set; }
        public string LTV { get; set; }
        public string Comments { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
    
        public virtual TGLKYC_BasicDetails TGLKYC_BasicDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_ResidenceVerification> tbl_ResidenceVerification { get; set; }
    }
}