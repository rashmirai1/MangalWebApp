using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangalWebProject.Models
{
    public class AuditCheckListViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Effective Date is Required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime EffectiveDate { get; set; }

        [Required(ErrorMessage = "Please Select Category Audit")]
        public short CategoryAudit { get; set; }

        [Required(ErrorMessage = "Audit CheckPoint is Required")]
        public string AuditCheckPoint { get; set; }

        [Required(ErrorMessage = "Please Select Status")]
        public short Status { get; set; }

        public string CategoryAuditStr { get; set; }
        public string StatusStr { get; set; }

        public string operation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}