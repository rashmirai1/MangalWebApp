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
    
    public partial class TBankCash_ReceiptDetails
    {
        public int BCRID { get; set; }
        public string RefType { get; set; }
        public int RefNo { get; set; }
        public string ReferenceNo { get; set; }
        public System.DateTime RefDate { get; set; }
        public int VoucherNo { get; set; }
        public int BankCashAccID { get; set; }
        public int ReceivedFrom { get; set; }
        public Nullable<double> Amount { get; set; }
        public string ChqNo { get; set; }
        public Nullable<System.DateTime> ChqDate { get; set; }
        public Nullable<int> BankID { get; set; }
        public string Narration { get; set; }
        public string BouncingRefNo { get; set; }
        public Nullable<int> BouncingRefNoID { get; set; }
        public string ItemSeqNo { get; set; }
        public string RecBookNo { get; set; }
        public Nullable<int> RecNo { get; set; }
        public Nullable<int> RecExeID { get; set; }
        public Nullable<int> CollExeID { get; set; }
        public Nullable<int> LedgerID { get; set; }
        public string Mode { get; set; }
        public string SubMode { get; set; }
        public string ParentCheck { get; set; }
        public int FinanceYear { get; set; }
        public Nullable<int> RecExeEmployeeID { get; set; }
        public Nullable<int> CollExeEmployeeID { get; set; }
    }
}
