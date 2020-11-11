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
    
    public partial class TGLPreSanction
    {
        public int PreSanctionID { get; set; }
        public Nullable<int> KYCID { get; set; }
        public string LoanType { get; set; }
        public Nullable<int> RMID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> LoanPurposeID { get; set; }
        public Nullable<int> SchemeID { get; set; }
        public Nullable<decimal> ReqLoanAmount { get; set; }
        public Nullable<int> Tenure { get; set; }
        public Nullable<decimal> ROI { get; set; }
        public Nullable<decimal> LTV { get; set; }
        public string ResidenceVerification { get; set; }
        public string Comments { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<int> FYID { get; set; }
        public Nullable<int> CMPID { get; set; }
        public Nullable<int> BranchID { get; set; }
        public string TransactionID { get; set; }
    
        public virtual Mst_LoanPupose Mst_LoanPupose { get; set; }
        public virtual Mst_Product Mst_Product { get; set; }
        public virtual TGLKYC_BasicDetails TGLKYC_BasicDetails { get; set; }
        public virtual TSchemeMaster_BasicDetails TSchemeMaster_BasicDetails { get; set; }
        public virtual UserDetail UserDetail { get; set; }
    }
}