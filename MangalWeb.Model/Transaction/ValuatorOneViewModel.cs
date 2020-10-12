using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Transaction
{
    public class ValuatorOneViewModel
    {
        public int ID { get; set; }

        public int TransactionId { get; set; }
        public int KycId { get; set; }
        public int PreSanctionId { get; set; }
        [Required(ErrorMessage = "Please Select Customer")]
        public string CustomerId { get; set; }
        public string ApplicationNo { get; set; }
        public string AppliedDate { get; set; }

        public string Comments { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public int CompanyId { get; set; }

        public string operation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ValuatorOneDetailsViewModel ValuatorOneDetailsVM { get; set; }

        public List<ValuatorOneDetailsViewModel> ValuatorOneDetailsList { get; set; }

    }
}
