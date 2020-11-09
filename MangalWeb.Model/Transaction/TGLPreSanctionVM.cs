using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MangalWeb.Model.Transaction
{
    public class TGLPreSanctionVM
    {
        public int PreSanctionID { get; set; }
        public Nullable<int> KYCID { get; set; }

        [Required(ErrorMessage = "Loan Type is required")]
        public string LoanType { get; set; }

        [Required(ErrorMessage = "RM is required")]
        public Nullable<int> RMID { get; set; }

        [Required(ErrorMessage = "Product is required")]
        public Nullable<int> ProductID { get; set; }

        [Required(ErrorMessage = "Loan Purpos is required")]
        public Nullable<int> LoanPurposeID { get; set; }

        [Required(ErrorMessage = "Scheme is required")]
        public Nullable<int> SchemeID { get; set; }

        [Required(ErrorMessage = "Req. Loan Amount is required")]
        public Nullable<decimal> ReqLoanAmount { get; set; }

        [Required(ErrorMessage = "Tenure is required")]
        public Nullable<int> Tenure { get; set; }

        [Required(ErrorMessage = "ROI is required")]
        public Nullable<decimal> ROI { get; set; }

        [Required(ErrorMessage = "LTV is required")]
        public Nullable<decimal> LTV { get; set; }

        public string ResidenceVerification { get; set; }

        [Required(ErrorMessage = "Comments is required")]
        public string Comments { get; set; }
        public Nullable<int> CreatedBy { get; set; }

        [Display(Name = "Customer ID")]
        [Required(ErrorMessage = "Customer ID is required")]
        public string CustomerID { get; set; }

        [Required(ErrorMessage = "Application No is required")]
        public string ApplicationNo { get; set; }

        [Required(ErrorMessage = "Transaction ID is required")]
        public string TransactionID { get; set; }

        [Required(ErrorMessage = "Applied Date is required")]
        public string AppliedDate { get; set; }


        public IEnumerable<SelectListItem> RMList { get; set; }
        public IEnumerable<SelectListItem> LoanPurposes { get; set; }
        public IEnumerable<SelectListItem> Schemes { get; set; }
        public IEnumerable<SelectListItem> Products { get; set; }
    }
}
