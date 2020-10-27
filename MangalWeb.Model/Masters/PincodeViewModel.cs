using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangalWeb.Model.Masters
{
    public class PincodeViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Pincode is required")]
        [StringLength(7, ErrorMessage ="Pin Code can not be more than 7 characters")]
        public string Pincode { get; set; }

        [Required(ErrorMessage = "Area is required")]
        [StringLength(20, ErrorMessage = "Area Name can not be more than 20 characters")]
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