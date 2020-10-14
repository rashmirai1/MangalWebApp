
namespace MangalWeb.Model.Transaction
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class PreSanctionDetailsVM
    {
        public int Id { get; set; }
        [Required]
        public Nullable<int> KycId { get; set; }
        [Required]
        public string TransactionId { get; set; }
        [Required]
        public string NewTopUp { get; set; }
        [Required]
        public string CustomerId { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> AppliedDate { get; set; }
        [Required]
        public string ApplicationNo { get; set; }
        [Required]
        public int RM { get; set; }
        [Required]
        public int Product { get; set; }
        [Required]
        public string PurposeofLoan { get; set; }
        [Required]
        public int Scheme { get; set; }
        [Required]
        public string ReqLoanAmount { get; set; }
        [Required]
        public string Tenure { get; set; }
        [Required]
        public string ROI { get; set; }
        [Required]
        public string LTV { get; set; }
        [Required]
        public string Comments { get; set; }
        [Required]
        public string ResidenceVerification { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
    }
}
