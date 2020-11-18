using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Transaction
{
    public class EligibleLoanAmountValuationDetailsVM
    {
        public int ID { get; set; }
        public int? SDID { get; set; }
        public int? OrnamentId { get; set; }
        public string OrnamentName { get; set; }
        public int? Qty { get; set; }
        public int? PurityNo { get; set; }
        public string PurityName { get; set; }
        public decimal? GrossWeight { get; set; }
        public decimal? Deductions { get; set; }
        public decimal? NetWeight { get; set; }
        public decimal? RatePerGram { get; set; }
        public decimal? Value { get; set; }
        public string ImageName { get; set; }
        public string ContentType { get; set; }
    }
}
