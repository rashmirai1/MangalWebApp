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
    
    public partial class GetKYCDetailsForRequestForm_Result
    {
        public int KycId { get; set; }
        public string CustomerId { get; set; }
        public string ApplicationNo { get; set; }
        public string LoanAccountNo { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string BldgHouseName { get; set; }
        public string Road { get; set; }
        public string BldgPlotNo { get; set; }
        public string RoomBlockNo { get; set; }
        public Nullable<int> StateID { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<int> ZoneID { get; set; }
        public string Landmark { get; set; }
        public string Distance { get; set; }
        public string PinCode { get; set; }
        public string Area { get; set; }
        public Nullable<System.DateTime> KYCDate { get; set; }
    }
}