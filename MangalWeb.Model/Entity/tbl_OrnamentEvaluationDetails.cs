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
    
    public partial class tbl_OrnamentEvaluationDetails
    {
        public int Id { get; set; }
        public int ValTwoId { get; set; }
        public Nullable<int> SDID { get; set; }
        public int KycId { get; set; }
        public int OrnamentId { get; set; }
        public byte[] OrnamentImage { get; set; }
        public string ContentType { get; set; }
        public string ImageName { get; set; }
        public Nullable<int> Qty { get; set; }
        public int PurityId { get; set; }
        public Nullable<decimal> GrossWt { get; set; }
        public Nullable<decimal> Deduction { get; set; }
        public Nullable<decimal> NtWt { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> Total { get; set; }
    }
}
