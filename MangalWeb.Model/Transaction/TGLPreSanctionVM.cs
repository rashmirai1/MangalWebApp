﻿using System;
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
        [StringLength(400,ErrorMessage = "Comments length should not be exceeded 400")]
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

        [Required(ErrorMessage = "Approve/Reject is required")]
        public string DeviationApprove { get; set; }

        [Required(ErrorMessage = "Approver Comment is required")]
        public string ApproverComment { get; set; }

        public string IsApproval { get; set; }



        public Nullable<int> FYID { get; set; }
        public Nullable<int> CMPID { get; set; }
        public Nullable<int> BranchID { get; set; }

        public IEnumerable<SelectListItem> RMList { get; set; }
        public IEnumerable<SelectListItem> LoanPurposes { get; set; }
        public IEnumerable<SelectListItem> Schemes { get; set; }
        public IEnumerable<SelectListItem> Products { get; set; }
    }
}
