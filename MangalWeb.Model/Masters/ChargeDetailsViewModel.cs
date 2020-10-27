using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangalWeb.Model.Masters
{
    public class ChargeDetailsViewModel
    {
        public int ID { get; set; }
        public int ChargeRefId { get; set; }

        [Required(ErrorMessage ="Loan Amount >= is required")]
        public double LoanAmountGreaterthan { get; set; }

        [Required(ErrorMessage = "Loan Amount <= is required")]
        public double LoanAmountLessthan { get; set; }

        [Required(ErrorMessage = "Charge Amount is required")]
        public double ChargeAmount { get; set; }

        [Required(ErrorMessage = "Please Select Charge Type")]
        public string ChargeType { get; set; }
    }
}