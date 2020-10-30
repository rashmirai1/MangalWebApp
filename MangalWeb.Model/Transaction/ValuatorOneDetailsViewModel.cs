using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MangalWeb.Model.Transaction
{
    public class ValuatorOneDetailsViewModel
    {
        public int ID { get; set; }
        public int ValuatorOneId { get; set; }
        [Required(ErrorMessage ="Please Select Ornament")]
        public int OrnamentId { get; set; }
        public string OrnamentName { get; set; }

        public HttpPostedFileBase ValuationImage { get; set; }
        public byte[] ValuationImageFile { get; set; }
        public string ContentType { get; set; }
        public string ImageName { get; set; }
        [Required(ErrorMessage = "Please Enter Quantity")]
        public int Qty { get; set; }
        [Required(ErrorMessage = "Please Select Purity")]
        public int PurityId { get; set; }
        public string PurityName { get; set; }
        [Required(ErrorMessage = "Please Enter Gross Weight")]
        public decimal GrossWeight { get; set; }

        public decimal Deductions { get; set; }
        public decimal NetWeight { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
        
    }
}
