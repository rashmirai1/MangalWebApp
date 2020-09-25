using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangalWeb.Model.Masters
{
    public class GstViewModel
    {
        public int ID { get; set; }
        public int EditID { get; set; }

        [Required(ErrorMessage = "Effective From is Required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public string EffectiveFrom { get; set; }

        public string CGST { get; set; }

        public string SGST { get; set; }

        public string IGST { get; set; }

        [Required(ErrorMessage = "Account Head 1")]
        public int CGSTAccountNo { get; set; }
        public int? SGSTAccountNo { get; set; }
        public string CGSTAccountName { get; set; }
        public string SGSTAccountName { get; set; }

        public string operation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}