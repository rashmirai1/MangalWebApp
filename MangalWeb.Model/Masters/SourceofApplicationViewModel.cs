using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Model.Masters
{
    public class SourceofApplicationViewModel
    {
        public int ID { get; set; }
        public int EditID { get; set; }

        [Required(ErrorMessage = "Source Name is required")]
        [StringLength(30, ErrorMessage = "Source Name can not be more than 30 characters")]
        //[Remote("doesSourceNameExist", "SourceofApplication", ErrorMessage = "Source Name Already Exists.")]
        public string SourceName { get; set; }

        [Required(ErrorMessage = "Please Select Category")]
        public string SourceCategory { get; set; }

        [Required(ErrorMessage = "Please Select Status")]
        public string SourceStatus { get; set; }

        public string operation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}