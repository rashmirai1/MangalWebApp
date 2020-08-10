using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Transaction
{
    public class KYCDocumentDetailsVM
    {
        public int DID { get; set; }
        public Nullable<int> KYCID { get; set; }
        public Nullable<int> Serialno { get; set; }
        public Nullable<int> DocumentID { get; set; }
        public string OtherDoc { get; set; }
        public string DocName { get; set; }
        public string ImagePath { get; set; }
        public string VerifiedBy { get; set; }
        public Nullable<int> Empld { get; set; }
        public string NameOnDoc { get; set; }
        public string ImageUrl { get; set; }
        public byte[] ImageName { get; set; }
    }
}
