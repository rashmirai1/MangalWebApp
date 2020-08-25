using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Transaction
{
    public class GLBorrowerCopyReport
    {
        public string ApplicantName { get; set; }
        public string LoanAccountNo { get; set; }
        public int? DocumentId { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string SchemeName { get; set; }
        public decimal SchemeLoanAmount { get; set; }
        public string NameofNominee { get; set; }
        public string NomineeRelation { get; set; }
        public string NomineeDateofBirth { get; set; }
        public string NomineeSpouseName { get; set; }
        public string Occupation { get; set; }
        public string PresentAddress { get; set; }
        public string PresentAddressPincode { get; set; }

    }
}
