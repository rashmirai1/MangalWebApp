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

        [Required(ErrorMessage = "State Code is required")]
        [StringLength(10,ErrorMessage ="State Code can not be more than 10 characters")]
        public string StateCode { get; set; }

        [Required(ErrorMessage = "State Name is required")]
        [StringLength(20, ErrorMessage = "State Name can not be more than 20 characters")]
        public string StateName { get; set; }

        [Required(ErrorMessage = "Please Select Ckyc state")]
        public int CkycStateId { get; set; }

        [Required(ErrorMessage = "Please Select Country")]
        public int CountryId { get; set; }

        public string operation { get; set; }
    }
}