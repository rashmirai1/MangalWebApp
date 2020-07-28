using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangalWebProject.Models
{
    public class PenaltySlabViewModel
    {
        public int ID { get; set; }
        public int EditID { get; set; }

        [Required(ErrorMessage = "Date is Required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public string Datewef { get; set; }

        [Required(ErrorMessage = "Please Enter Penalty")]
        public decimal PenaltyAmount { get; set; }

        [Required(ErrorMessage = "Please Select Status")]
        public int AccountHead { get; set; }

        public string AccountHeadStr { get; set; }

        public string operation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}