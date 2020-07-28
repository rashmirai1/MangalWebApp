using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangalWebProject.Models
{
    public class SchemeViewModel
    {
        public int SchemeId { get; set; }

        [Required(ErrorMessage = "Please Select Product")]
        public short Product { get; set; }
        public string ProductStr { get; set; }

        [Required(ErrorMessage = "Please Select Purity")]
        public List<int> Purity { get; set; }

        [Required(ErrorMessage = "Please Select Scheme Type")]
        public short SchemeType { get; set; }
        public string SchemeTypeStr { get; set; }

        [Required(ErrorMessage = "Please Select Frequency")]
        public short Frequency { get; set; }
        public string FrequencyStr { get; set; }

        public int EditID { get; set; }

        [Required(ErrorMessage = "Scheme Name is Required")]
        [StringLength(30)]
        public string SchemeName { get; set; }

        [Required(ErrorMessage = "Min Tenure is Required")]
        public int MinTenure { get; set; }

        [Required(ErrorMessage = "Max Tenure is Required")]
        public int MaxTenure { get; set; }

        [Required(ErrorMessage = "Min Loan Amount is Required")]
        public decimal? MinLoanAmount { get; set; }

        [Required(ErrorMessage = "Max Loan Amount is Required")]
        public decimal? MaxLoanAmount { get; set; }

        [Required(ErrorMessage = "Min LTV % is Required")]
        public decimal? MinLTVPerc { get; set; }

        [Required(ErrorMessage = "Max LTV % is Required")]
        public decimal? MaxLTVPerc { get; set; }

        [Required(ErrorMessage = "Min ROI % is Required")]
        public decimal? MinROIPerc { get; set; }

        [Required(ErrorMessage = "Max ROI % is Required")]
        public decimal? MaxROIPerc { get; set; }

        [Required(ErrorMessage = "Grace Period is Required")]
        public int? GracePeriod { get; set; }

        [Required(ErrorMessage = "Effective ROI % is Required")]
        public decimal? EffectiveROIPerc { get; set; }

        [Required(ErrorMessage = "Lock In Period is Required")]
        public int? LockInPeriod { get; set; }

        [Required(ErrorMessage = "ProcessingFeeType is Required")]
        public short? ProcessingFeeType { get; set; }

        [Required(ErrorMessage = "Processing Charges is Required")]
        public decimal? ProcessingCharges { get; set; }

        [Required(ErrorMessage = "Status is Required")]
        public short? Status { get; set; }
        public string Statusstr { get; set; }

        public string operation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}