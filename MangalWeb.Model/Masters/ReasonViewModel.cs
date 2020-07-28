using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangalWebProject.Models
{
    public class ReasonViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Reason is Required")]
        [StringLength(150)]
        public string ReasonName { get; set; }

        [Required(ErrorMessage = "Please Select Status")]
        public short Status { get; set; }

        public string StatusStr { get; set; }

        public string operation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}