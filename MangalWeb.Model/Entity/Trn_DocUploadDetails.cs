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
    
    public partial class Trn_DocUploadDetails
    {
        public int Id { get; set; }
        public int KycId { get; set; }
        public int DocumentTypeId { get; set; }
        public int DocumentId { get; set; }
        public string SpecifyOther { get; set; }
        public string NameonDocument { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] UploadFile { get; set; }
        public Nullable<int> VerifiedBy { get; set; }
        public string Status { get; set; }
        public string ReasonForRejection { get; set; }
    }
}
