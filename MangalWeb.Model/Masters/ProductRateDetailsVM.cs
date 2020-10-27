using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangalWeb.Model.Masters
{
    public class ProductRateDetailsVM
    {
        public int ID { get; set; }
        public int ChargeRefId { get; set; }

        [Required(ErrorMessage = "Please Select Purity")]
        public int Purity { get; set; }
        public string PurityStr { get; set; }

        [Required(ErrorMessage = "Gross Rate is required")]
        public decimal GrossRate { get; set; }

        [Required(ErrorMessage = "Please Select Deduction Type")]
        public string DeductionsType { get; set; }

        [Required(ErrorMessage = "Deduction Amount is required")]
        public decimal DeductionAmount { get; set; }

        [Required(ErrorMessage = "Net Rate is required")]
        public decimal NetRate { get; set; }

        public string operation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}