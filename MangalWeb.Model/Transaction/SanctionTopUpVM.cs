using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Transaction
{
   public class SanctionTopUpVM
    {
        public string Cdate { get; set; }
        public decimal LoanAmout { get; set; }
        public int SID { get; set; }
        public decimal ROI { get; set; }
        public decimal NetLoanAmtSanctioned { get; set; }
        public decimal CLP { get; set; }
        public decimal CLI { get; set; }
        public decimal BCLI { get; set; }
        public decimal CLPI { get; set; }
        public decimal CLC { get; set; }
        public decimal CLO { get; set; }
        public decimal BalanceLoanAmt { get; set; }
        public int SDID { get; set; }
        public int BCPID { get; set; }
        public string LoanInterestToDate { get; set; }
        public string InterestFromDate { get; set; }
        public string InterestToDate { get; set; }
        public decimal RecvInterest { get; set; }
        public string OSInterestFromDate { get; set; }
        public string OSInterestToDate { get; set; }
        public decimal OSIntAmt { get; set; }
        public string AdvInterestFromDate { get; set; }
        public string AdvInterestToDate { get; set; }
        public decimal AdvInterestAmount { get; set; }
        public string LastReceiveDate { get; set; }
        public DateTime CustLoanDate { get; set; }
        public decimal LastOSInt { get; set; }
    }
}
