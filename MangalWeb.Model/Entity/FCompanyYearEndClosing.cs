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
    
    public partial class FCompanyYearEndClosing
    {
        public int ID { get; set; }
        public int FinancialyearID { get; set; }
        public int CompID { get; set; }
        public int AccountID { get; set; }
        public Nullable<double> OpeningBalanceDebit { get; set; }
        public Nullable<double> OpeningBalanceCredit { get; set; }
        public Nullable<double> CurrentDebit { get; set; }
        public Nullable<double> CurrentCredit { get; set; }
        public Nullable<double> ClosingBalanceDebit { get; set; }
        public Nullable<double> ClosingBalanceCredit { get; set; }
    }
}
