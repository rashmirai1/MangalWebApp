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
   public class DocumentUploadService
    {
        DocumentUploadRepository _documentUploadRepository = new DocumentUploadRepository();

        public DocumentUploadViewModel GetAllDocumentUpload()
        {
            return _documentUploadRepository.GetAllDocumentUpload();
        }

        public List<Mst_DocumentType> GetDocumentTypeList()
        {
            return _documentUploadRepository.GetDocumentTypeList();
        }

        public List<tblDocumentMaster> GetDocumentMasterById(int id)
        {
            return _documentUploadRepository.GetDocumentMasterById(id);
        }

        public bool GetExpiryDate(int id)
        {
            return _documentUploadRepository.GetExpiryDate(id);
        }

        public List<tblDocumentMaster> GetDocumentMasterList()
        {
            return _documentUploadRepository.GetDocumentMasterList();
        }

        public List<DocumentUploadViewModel> GetKYCList()
        {
            return _documentUploadRepository.GetKYCList();
        }

        public DocumentUploadViewModel GetCustomerById(int id)
        {
            return _documentUploadRepository.GetCustomerById(id);
        }

        public List<DocumentUploadViewModel> SetModalList()
        {
            return _documentUploadRepository.SetModalList();
        }

        public void SaveRecord(DocumentUploadViewModel model)
        {
            _documentUploadRepository.SaveUpdateRecord(model);
        }

        public Trn_DocUploadDetails GetUploadDocuments(int id)
        {
            return _documentUploadRepository.GetUploadDocuments(id);
        }

        public void DeleteRecord(int id)
        {
            _documentUploadRepository.DeleteRecord(id);
        }

        public DocumentUploadViewModel GetUploadDocumentById(int id)
        {
            var product = _documentUploadRepository.SetRecordinEdit(id);
            return product;
        }

        public DocumentUploadDetailsVM AddDocumentinSession(DocumentUploadDetailsVM documentUploadDetailsVM)
        {
            return _documentUploadRepository.AddDocumentinSession(documentUploadDetailsVM);
        }

    }
}
