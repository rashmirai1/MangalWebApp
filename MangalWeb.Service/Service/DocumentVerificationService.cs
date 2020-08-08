using MangalWeb.Model.Entity;
using MangalWeb.Model.Transaction;
using MangalWeb.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Service.Service
{
    public class DocumentVerificationService
    {
        DocumentVerificationRepository _documentVerificationRepository = new DocumentVerificationRepository();

        public DocumentUploadViewModel GetAllDocumentUpload(int id)
        {
            return _documentVerificationRepository.GetDoumentUploadById(id);
        }
        public void SaveUpdateRecord(DocumentUploadViewModel DocUploadViewModel)
        {
            _documentVerificationRepository.SaveUpdateRecord(DocUploadViewModel);
        }

        public List<DocumentUploadViewModel> GetDocumentUploadList()
        {
            return _documentVerificationRepository.DocumentUploadList();
        }

        public DocumentUploadViewModel GetDoumentUploadById(int id)
        {
            return _documentVerificationRepository.GetDoumentUploadById(id);
        }
    }
}
