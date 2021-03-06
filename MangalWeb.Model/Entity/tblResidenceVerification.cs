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
    
    public partial class tblResidenceVerification
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public int KycId { get; set; }
        public int PreSanctionId { get; set; }
        public System.DateTime DateofVisit { get; set; }
        public string TimeofVisit { get; set; }
        public string PersonVisitedName { get; set; }
        public string RelationWithCustomer { get; set; }
        public string FamilyMemberDetails { get; set; }
        public string AddressCategory { get; set; }
        public string ResidenceCode { get; set; }
        public string BldgHouseName { get; set; }
        public string BldgPlotNo { get; set; }
        public string Road { get; set; }
        public string RoomBlockNo { get; set; }
        public string Landmark { get; set; }
        public string Distance { get; set; }
        public int PinCode { get; set; }
        public int ResidingAtThisAddress_Months { get; set; }
        public int ResidingAtThisAddress_Years { get; set; }
        public int UserId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string Comments { get; set; }
        public bool IsActive { get; set; }
    
        public virtual TGLPreSanction TGLPreSanction { get; set; }
    }
}
