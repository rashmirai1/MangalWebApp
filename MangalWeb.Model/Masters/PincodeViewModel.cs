using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangalWebProject.Models
{
    public class PincodeViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Pincode is Required")]
        [StringLength(7)]
        public string Pincode { get; set; }

        [Required(ErrorMessage = "Area is Required")]
        [StringLength(20)]
        public string AreaName { get; set; }

        [Required(ErrorMessage = "Please Select City")]
        public int CityId { get; set; }

        public string StateName { get; set; }

        [Required(ErrorMessage = "Please Select Zone")]
        public int ZoneId { get; set; }

        public string operation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}