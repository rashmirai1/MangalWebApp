using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangalWebProject.Models
{
    public class ProductRateViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Date is Required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public string ProductRateDate { get; set; }

        [Required(ErrorMessage = "Please Select Product")]
        public short Product { get; set; }
        public string ProductStr { get; set; }

        public string operation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ProductRateDetailsVM ProductDetailsViewModel { get; set; }

        public List<ProductRateDetailsVM> ProductRateList { get; set; }
    }
}