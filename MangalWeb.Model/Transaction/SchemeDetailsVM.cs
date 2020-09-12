using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Transaction
{
    public class SchemeDetailsVM
    {
        public int SID { get; set; }
        public Nullable<int> Tenure { get; set; }
        public Nullable<decimal> MaxLoanAmt { get; set; }
        public Nullable<decimal> Ltv { get; set; }
        public Nullable<decimal> ROI { get; set; }
    }
}
