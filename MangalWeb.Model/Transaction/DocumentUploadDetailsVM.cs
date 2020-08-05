using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MangalWeb.Model.Transaction
{
    public class DocumentUploadDetailsVM
    {
        public int ID { get; set; }
        public int DocumentTypeId { get; set; }
        public int DocumentId { get; set; }
        public string DocumentTypeName { get; set; }
        public string DocumentName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Date is Required")]
        public DateTime? ExpiryDate { get; set; }
        public string UploadDocName { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public HttpPostedFileBase UploadedFile { get; set; }
    }
}
