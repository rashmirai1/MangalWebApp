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
    
    public partial class Mst_AuditCheckList
    {
        public int Acl_Id { get; set; }
        public System.DateTime Acl_EffectiveDate { get; set; }
        public short Acl_Categoryofaudit { get; set; }
        public string Acl_CheckPoint { get; set; }
        public short Acl_Status { get; set; }
        public Nullable<System.DateTime> Acl_RecordCreated { get; set; }
        public Nullable<System.DateTime> Acl_RecordUpdated { get; set; }
        public Nullable<int> Acl_RecordCreatedBy { get; set; }
        public Nullable<int> Acl_RecordUpdatedBy { get; set; }
    }
}
