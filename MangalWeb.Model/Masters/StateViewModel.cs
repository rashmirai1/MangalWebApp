using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangalWeb.Model.Masters
{
    public class StateViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "State Code is Required*")]
        [StringLength(10)]
        public string StateCode { get; set; }

        [Required(ErrorMessage = "State Name is Required*")]
        [StringLength(20)]
        public string StateName { get; set; }

        [Required(ErrorMessage = "Please Select Country")]
        public int CountryId { get; set; }

        public string operation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}