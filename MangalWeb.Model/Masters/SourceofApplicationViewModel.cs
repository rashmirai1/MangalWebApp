using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWebProject.Models
{
    public class SourceofApplicationViewModel
    {
        public int ID { get; set; }
        public int EditID { get; set; }

        [Required(ErrorMessage = "Source Name is Required")]
        [StringLength(30)]
        //[Remote("doesSourceNameExist", "SourceofApplication", ErrorMessage = "Source Name Already Exists.")]
        public string SourceName { get; set; }

        [Required(ErrorMessage = "Please Select Category")]
        public short SourceCategory { get; set; }

        [Required(ErrorMessage = "Please Select Status")]
        public short SourceStatus { get; set; }

        public string SourceCategirystr { get; set; }
        public string SourceStatusstr { get; set; }

        public string operation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}