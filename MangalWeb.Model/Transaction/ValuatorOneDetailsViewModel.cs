using System;
using System.Collections.Generic;
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
        public int OrnamentId { get; set; }
        public string OrnamentName { get; set; }
        public HttpPostedFileBase OrnamentImage { get; set; }
        public int Qty { get; set; }
        public int PurityId { get; set; }
        public string PurityName { get; set; }
        public decimal GrossWeight { get; set; }
        public decimal Duductions { get; set; }
        public decimal NetWeight { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalValuation { get; set; }
    }
}
