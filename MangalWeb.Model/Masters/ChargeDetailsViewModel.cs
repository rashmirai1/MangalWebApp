using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MangalWeb.Model.Masters
{
    public class ChargeDetailsViewModel
    {
        public int ID { get; set; }
        public int ChargeRefId { get; set; }

        public double LoanAmountGreaterthan { get; set; }
        public double LoanAmountLessthan { get; set; }
        public double ChargeAmount { get; set; }
        public string ChargeType { get; set; }
    }
}