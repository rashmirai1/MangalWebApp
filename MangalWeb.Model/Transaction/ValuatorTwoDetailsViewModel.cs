using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MangalWeb.Model.Transaction
{
    public class ValuatorTwoDetailsViewModel
    {
        public int ID { get; set; }
        public int ValuatorTwoId { get; set; }
        public int ValOneDetailsId { get; set; }
        public int OrnamentId { get; set; }
        public string OrnamentName { get; set; }
        public HttpPostedFileBase ValuationImage { get; set; }
        public byte[] ValuationImageFile { get; set; }
        public string ContentType { get; set; }
        public string ImageName { get; set; }
        public int Qty { get; set; }
        public int PurityId { get; set; }
        public string PurityName { get; set; }
        public decimal GrossWeight { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetWeight { get; set; }
        public decimal Rate { get; set; }
        public decimal ValOneTotal { get; set; }
        public decimal ValTwoTotal { get; set; }
    }
}
