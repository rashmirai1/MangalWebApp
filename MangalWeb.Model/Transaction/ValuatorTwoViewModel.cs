using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MangalWeb.Model.Transaction
{
    public class ValuatorTwoViewModel
    {
        public int ID { get; set; }

        public string TransactionId { get; set; }
        public int KycId { get; set; }
        public int ProductId { get; set; }
        public int ValuatorOneId { get; set; }
        public string AppliedDate { get; set; }
        [Required(ErrorMessage = "Please Select Customer")]
        public HttpPostedFileBase ConsolidatedImage { get; set; }
        public byte[] ConsolidatedImageFile { get; set; }
        public string ContentType { get; set; }
        public string ImageName { get; set; }
        public string CustomerId { get; set; }
        public string ApplicationNo { get; set; }
        public decimal EligibleLoanAmount { get; set; }
        public decimal LTVPerc { get; set; }
        public decimal MaxLtv { get; set; }
        public decimal SanctionLoanAmount { get; set; }

        public string Comments { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public int CompanyId { get; set; }

        public string operation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ValuatorTwoDetailsViewModel ValuatorTwoDetailsVM { get; set; }

        public List<ValuatorTwoDetailsViewModel> ValuatorTwoDetailsList { get; set; }

    }
}
