using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Transaction
{
   public class DocumentUploadViewModel
    {
        public int ID { get; set; }

        public int TransactionId { get; set; }
        public int KycId { get; set; }
        public string TransactionNumber { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Date is Required")]
        public string DocDate { get; set; }
        [Required(ErrorMessage = "Please Select Customer")]
        public string CustomerId { get; set; }
        public string ApplicationNo { get; set; }
        public string LoanAccountNo { get; set; }

        public string Comments { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public int CompanyId { get; set; }

        public string operation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public DocumentUploadDetailsVM DocumentUploadVM { get; set; }

        public List<DocumentUploadDetailsVM> DocumentUploadList { get; set; }

    }
}
