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
    
    public partial class tbl_OrnamentEvaluation
    {
        public int Id { get; set; }
        public int ValuatorOneId { get; set; }
        public Nullable<int> KYCID { get; set; }
        public string TransactionId { get; set; }
        public string CustomerID { get; set; }
        public string ApplicationNo { get; set; }
        public byte[] ConsolidatedImage { get; set; }
        public string ContentType { get; set; }
        public string ImageName { get; set; }
        public Nullable<decimal> LTVPerc { get; set; }
        public Nullable<decimal> EligibleLoanAmount { get; set; }
        public Nullable<decimal> SanctionLoanAmount { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public int CompId { get; set; }
        public int FinancialYearId { get; set; }
        public int BranchId { get; set; }
    }
}
