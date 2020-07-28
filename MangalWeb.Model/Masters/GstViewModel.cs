using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangalWebProject.Models
{
    public class GstViewModel
    {
        public int ID { get; set; }
        public int EditID { get; set; }

        [Required(ErrorMessage = "Effective From is Required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime EffectiveFrom { get; set; }

        public string CGST { get; set; }

        public string SGST { get; set; }

        public string IGST { get; set; }

        public string operation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}