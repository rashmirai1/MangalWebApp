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
    
    public partial class FLedgerMaster
    {
        public int LedgerID { get; set; }
        public string ReferenceNo { get; set; }
        public string RefType { get; set; }
        public System.DateTime RefDate { get; set; }
        public int AccountID { get; set; }
        public Nullable<double> Debit { get; set; }
        public Nullable<double> Credit { get; set; }
        public string Narration { get; set; }
        public Nullable<int> ContraAccID { get; set; }
        public string ReconcileDate { get; set; }
        public int FinanceYear { get; set; }
    }
}
