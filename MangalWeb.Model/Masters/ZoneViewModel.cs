using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangalWebProject.Models
{
    public class ZoneViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Zone Name is Required")]
        [StringLength(20)]
        public string ZoneName { get; set; }

        public string operation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}