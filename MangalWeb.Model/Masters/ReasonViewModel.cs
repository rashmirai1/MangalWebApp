using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangalWeb.Model.Masters
{
    public class ReasonViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Reason is required")]
        [StringLength(150,ErrorMessage ="Reason can not be greater than 150 characters")]
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