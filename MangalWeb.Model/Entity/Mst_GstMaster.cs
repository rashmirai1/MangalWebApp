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
    
    public partial class Mst_GstMaster
    {
        public int Gst_RefId { get; set; }
        public System.DateTime Gst_EffectiveFrom { get; set; }
        public string Gst_CGST { get; set; }
        public string Gst_SGST { get; set; }
        public string Gst_IGST { get; set; }
        public Nullable<System.DateTime> Gst_RecordCreated { get; set; }
        public Nullable<System.DateTime> Gst_RecordUpdated { get; set; }
        public Nullable<int> Gst_RecordCreatedBy { get; set; }
        public Nullable<int> Gst_RecordUpdatedBy { get; set; }
    }
}
