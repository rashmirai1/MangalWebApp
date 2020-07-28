using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangalWebProject.Models
{
    public class ChargeViewModel
    {

        public int ID { get; set; }

        [Required(ErrorMessage = "Charge Name is Required")]
        [StringLength(20)]
        public string ChargeName { get; set; }

        [Required(ErrorMessage = "Reference Date is Required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime ReferenceDate { get; set; }

        [Required(ErrorMessage ="Please Select Status")]
        public short Status { get; set; }
        public string StatusStr { get; set; }

        public string operation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ChargeDetailsViewModel chargeDetailsViewModel { get; set; }

        public virtual IList<ChargeDetailsViewModel> chargeDetailsCollection { get; set; }
    }
}