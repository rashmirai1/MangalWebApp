using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MangalWebProject.Models
{
    public class ChargeDetailsViewModel
    {
        public int ID { get; set; }
        public int ChargeRefId { get; set; }

        public decimal LoanAmountGreaterthan { get; set; }
        public decimal LoanAmountLessthan { get; set; }
        public decimal ChargeAmount { get; set; }
        public short ChargeType { get; set; }

        public string ChargeTypeStr { get; set; }

    }
}