using MangalWeb.Model.Entity;
using MangalWeb.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Repository.Repository
{
    public class DocumentVerificationRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<DocumentUploadViewModel> DocumentUploadList()
        {
            return _context.Database.SqlQuery<DocumentUploadViewModel>("GetDocumentUpload").ToList();
        }

        public void SaveUpdateRecord(DocumentUploadViewModel DocUploadViewModel)
        {
            Trn_DocUploadDetails tbldocuploaddetails = new Trn_DocUploadDetails();
            try
            {
                //update the data in Charge Details table
                foreach (var p in DocUploadViewModel.DocumentUploadList)
                {
                    var FindRateobject = _context.Trn_DocUploadDetails.Where(x => x.Id == p.ID && x.KycId == DocUploadViewModel.KycId).FirstOrDefault();
                    FindRateobject.VerifiedBy = 1;
                    FindRateobject.Status = p.Status;
                    FindRateobject.ReasonForRejection = p.ReasonForRejection;
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DocumentUploadViewModel GetDoumentUploadById(int id)
        {
            DocumentUploadViewModel documentUploadViewModel = new DocumentUploadViewModel();
            //get document upload table
            //var docupload = _context.Trn_DocumentUpload.Where(x => x.KycId == id).FirstOrDefault();
            var docupload = _context.Database.SqlQuery<DocumentUploadViewModel>("GetDocumentUploadById @id", new SqlParameter("@id", id)).FirstOrDefault();
            var docuploaddetails = _context.Trn_DocUploadDetails.Where(x => x.KycId == docupload.KycId).ToList();
            documentUploadViewModel = ToViewModelDocUpload(docupload, docuploaddetails);
            return documentUploadViewModel;
        }

        #region ToViewModelDocUpload
        public DocumentUploadViewModel ToViewModelDocUpload(DocumentUploadViewModel model, List<Trn_DocUploadDetails> DocUploadTrnList)
        {
            //var model = new DocumentUploadViewModel
            //{
            //    KycId = docupload.KycId,
            //    TransactionNumber = docupload.TransactionNumber,
            //    DocDate = Convert.ToDateTime(docupload.DocDate).ToShortDateString(),
            //    CustomerId = docupload.CustomerId,
            //    ApplicationNo = docupload.ApplicationNo,
            //    LoanAccountNo = docupload.LoanAccountNo,
            //    TransactionId = docupload.TransactionId,
            //    ID = docupload.DocId,
            //    Comments = docupload.Comments
            //};

            List<DocumentUploadDetailsVM> DocTrnViewModelList = new List<DocumentUploadDetailsVM>();
            foreach (var c in DocUploadTrnList)
            {
                var TrnViewModel = new DocumentUploadDetailsVM
                {
                    ID = c.Id,
                    DocumentTypeId = (int)c.DocumentTypeId,
                    DocumentTypeName = _context.Mst_DocumentType.Where(x => x.Id == c.DocumentTypeId).Select(x => x.Name).FirstOrDefault(),
                    DocumentName = _context.tblDocumentMasters.Where(x => x.DocumentID == c.DocumentId).Select(x => x.DocumentName).FirstOrDefault(),
                    DocumentId = (int)c.DocumentId,
                    ExpiryDate = c.ExpiryDate,
                    UploadDocName = c.UploadFile,
                    FileName = c.FileName,
                    FileExtension = c.ContentType,
                    KycId = c.KycId,
                    Status=c.Status,
                    VerifiedBy=c.VerifiedBy,
                    ReasonForRejection=c.ReasonForRejection
                };
                DocTrnViewModelList.Add(TrnViewModel);
            }
            model.DocumentUploadList = DocTrnViewModelList;
            return model;
        }
        #endregion
    }
}
