using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Transaction
{
   public class ChargeSanctionVM
    {
        public int ID { get; set; }
        public int SantionId { get; set; }
        public int? ChargeId { get; set; }
        public double Charges { get; set; }
        public double? Amount { get; set; }
        public string ChargeName { get; set; }
        public int? AccountId { get; set; }
        public string AccountName { get; set; }
        public string ChargeType { get; set; }
    }
}
