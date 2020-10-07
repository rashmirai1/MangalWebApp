using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Transaction
{
    public class SanctionEMICalculatorVM
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string TotalDays { get; set; }
        public string AllDaysTillDate { get; set; }
        public string LoanAmount { get; set; }
        public string ROIID { get; set; }
        public string ROI { get; set; }
        public string InterestAmount { get; set; }
        public string PrevOSInterest { get; set; }
    }
}
