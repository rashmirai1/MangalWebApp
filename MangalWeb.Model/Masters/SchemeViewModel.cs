﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangalWeb.Model.Masters
{
    public class SchemeViewModel
    {
        public SchemeViewModel()
        {
            SchemeEffectiveROIModel = new SchemeEffectiveROIVM();
            SchemeEffectiveROIList = new List<SchemeEffectiveROIVM>();
        }
        public int SchemeId { get; set; }

        [Required(ErrorMessage = "Please Select Product")]
        public int Product { get; set; }
        public string ProductStr { get; set; }

        [Required(ErrorMessage = "Please Select Purity")]
        public List<int> Purity { get; set; }

        [Required(ErrorMessage = "Please Select Scheme Type")]
        public string SchemeType { get; set; }

        [Required(ErrorMessage = "Please Select Frequency")]
        public string Frequency { get; set; }

        public int EditID { get; set; }

        [Required(ErrorMessage = "Scheme Name is Required")]
        [StringLength(30, ErrorMessage = "Scheme Name can not be greater than 30 characters")]
        public string SchemeName { get; set; }

        [Required(ErrorMessage = "Min Tenure is Required")]
        [RegularExpression(@"^(\d{1,3})$", ErrorMessage = "error Message")]
        public int MinTenure { get; set; }

        [Required(ErrorMessage = "Max Tenure is Required")]
        [RegularExpression(@"^(\d{1,3})$", ErrorMessage = "error Message")]
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
        //[RegularExpression(@"[0-9]{0,}\.[0-9]{2}", ErrorMessage = "error Message")]
        public decimal? MaxROIPerc { get; set; }

        [Required(ErrorMessage = "Grace Period is Required")]
        [RegularExpression(@"^(\d{1,3})$", ErrorMessage = "error Message")]
        public int? GracePeriod { get; set; }

        [Required(ErrorMessage = "Lock In Period is Required")]
        [RegularExpression(@"^(\d{1,2})$", ErrorMessage = "error Message")]
        public int? LockInPeriod { get; set; }

        [Required(ErrorMessage = "Processing Fee Type is Required")]
        public string ProcessingFeeType { get; set; }

        [Required(ErrorMessage = "Processing Charges is Required")]
        public decimal? ProcessingCharges { get; set; }

        [Required(ErrorMessage = "Max Processing Charges is Required")]
        public decimal? MaxProcessingCharge { get; set; }

        [Required(ErrorMessage = "Status is Required")]
        public string Status { get; set; }

        public string operation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public SchemeEffectiveROIVM SchemeEffectiveROIModel { get; set; }
        public List<SchemeEffectiveROIVM> SchemeEffectiveROIList { get; set; }
    }
}