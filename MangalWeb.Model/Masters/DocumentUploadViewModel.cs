using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangalWebProject.Models
{
    public class DocumentUploadViewModel
    {
        public int ID { get; set; }

        public int TransactionId { get; set; }
        public string TransactionNumber { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Date is Required")]
        public string DocDate { get; set; }
        [Required(ErrorMessage = "Customer Selection is Required")]
        public string CustomerId { get; set; }
        public string ApplicationNo { get; set; }
        public string LoanAccountNo { get; set; }

        public int DocumentTypeId { get; set; }
        public int DocumentId { get; set; }
        public string DocumentTypeName { get; set; }
        public string DocumentName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Date is Required")]
        public string ExpiryDate { get; set; }
        public string UploadDocName { get; set; }
        public HttpPostedFileBase UploadedFile { get; set; }
        public string Comments { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public int CompanyId { get; set; }
        public int VerifiedBy { get; set; }
        public string Status { get; set; }
        public  string ReasonForRejection { get; set; }
        public string VerifyComment { get; set; }

        public string operation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public List<DocumentUploadViewModel> DocumentUploadList { get; set; }
    }
}