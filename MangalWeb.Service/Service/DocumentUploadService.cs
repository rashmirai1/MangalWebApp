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

        public List<tblDocumentMaster> GetDocumentMasterList()
        {
            return _documentUploadRepository.GetDocumentMasterList();
        }

        public List<TGLKYC_BasicDetails> GetKYCList()
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

        //public string getuploaddocuments(int id)
        //{
        //   //return _documentUploadRepository.getuploaddocuments(id);
        //}

        public void DeleteRecord(int id)
        {
            _documentUploadRepository.DeleteRecord(id);
        }

        public DocumentUploadViewModel GetUploadDocumentById(int id)
        {
            var product = _documentUploadRepository.SetRecordinEdit(id);
            return product;
        }

    }
}
