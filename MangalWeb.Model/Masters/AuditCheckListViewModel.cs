using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangalWeb.Model.Masters
{
    public class AuditCheckListViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Effective Date is required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public string EffectiveDate { get; set; }

        [Required(ErrorMessage = "Please Select Category Audit")]
        public int CategoryAudit { get; set; }
        public string CategoryAuditStr { get; set; }

        [Required(ErrorMessage = "Audit CheckPoint is required")]
        [StringLength(100,ErrorMessage = "Audit Check Point can not be more than 100 characters")]
        public string AuditCheckPoint { get; set; }

        [Required(ErrorMessage = "Please Select Status")]
        public string Status { get; set; }

        public string operation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}