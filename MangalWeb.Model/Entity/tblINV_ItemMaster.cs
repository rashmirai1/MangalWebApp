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
    
    public partial class tblINV_ItemMaster
    {
        public int ItemID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public System.DateTime Date { get; set; }
        public string QuantityType { get; set; }
        public string Returnable { get; set; }
        public Nullable<int> UnitID { get; set; }
        public Nullable<int> OpeningStock { get; set; }
        public string OpRate { get; set; }
        public string OpAmount { get; set; }
        public Nullable<System.DateTime> OpStockDate { get; set; }
        public Nullable<int> FinancialYrID { get; set; }
        public string HSNCode { get; set; }
    }
}