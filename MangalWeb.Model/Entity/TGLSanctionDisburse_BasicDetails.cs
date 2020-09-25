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
    
    public partial class TGLSanctionDisburse_BasicDetails
    {
        public int SDID { get; set; }
        public string RefType { get; set; }
        public string RefMon { get; set; }
        public string RefYr { get; set; }
        public Nullable<int> RefID { get; set; }
        public string LoanType { get; set; }
        public Nullable<System.DateTime> LoanDate { get; set; }
        public string GoldLoanNo { get; set; }
        public Nullable<int> KYCID { get; set; }
        public Nullable<int> OperatorID { get; set; }
        public Nullable<decimal> EligibleLoanAmt { get; set; }
        public Nullable<decimal> NetLoanAmtSanctioned { get; set; }
        public Nullable<decimal> NetLoanPayable { get; set; }
        public Nullable<int> BankCashAccID { get; set; }
        public string CheqNEFTDD { get; set; }
        public string CheqNEFTDDNo { get; set; }
        public Nullable<System.DateTime> CheqNEFTDDDate { get; set; }
        public Nullable<decimal> TotalGrossWeight { get; set; }
        public Nullable<decimal> TotalNetWeight { get; set; }
        public Nullable<decimal> TotalQuantity { get; set; }
        public Nullable<decimal> Totalvalue { get; set; }
        public Nullable<decimal> TotalRate { get; set; }
        public Nullable<int> SID { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public byte[] OwnershipProofImagePath { get; set; }
        public string CIBILScore { get; set; }
        public byte[] ItemImage { get; set; }
        public Nullable<int> BCPID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> FYID { get; set; }
        public Nullable<int> BranchID { get; set; }
        public Nullable<int> CMPID { get; set; }
        public string isActive { get; set; }
        public Nullable<int> CashInOutID { get; set; }
        public Nullable<int> GLInOutID { get; set; }
        public Nullable<int> CashOutWardById { get; set; }
        public Nullable<int> GoldInWardById { get; set; }
        public Nullable<int> CashAccID { get; set; }
        public Nullable<decimal> BankAmount { get; set; }
        public Nullable<decimal> CashAmount { get; set; }
        public string PaymentMode { get; set; }
        public Nullable<System.DateTime> BankPaymentDate { get; set; }
        public string Packetweight { get; set; }
        public string LockerNo { get; set; }
        public string Remark { get; set; }
    }
}
