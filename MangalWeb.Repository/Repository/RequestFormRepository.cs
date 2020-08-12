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
    public class RequestFormRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<Mst_SourceofApplication> GetSourceOfApplicationList()
        {
            return _context.Mst_SourceofApplication.ToList();
        }

        public List<DocumentUploadViewModel> GetKYCList()
        {
            return _context.Database.SqlQuery<DocumentUploadViewModel>("GetKYCDetailsForDocument").ToList();
        }

        public int GetMaxTransactionId()
        {
            return _context.Trn_RequestForm.Any() ? _context.Trn_RequestForm.Max(x => x.Id) : 1;
        }

        public IList<Mst_PinCode> GetAllPincodes()
        {
            return _context.Mst_PinCode.ToList();
        }

        public DocumentUploadViewModel GetDoumentUploadById(int id)
        {
            DocumentUploadViewModel documentUploadViewModel = new DocumentUploadViewModel();
            //get document upload table
            var docupload = _context.Trn_DocumentUpload.Where(x => x.KycId == id).FirstOrDefault();
            var docuploaddetails = _context.Trn_DocUploadDetails.Where(x => x.KycId == docupload.KycId).ToList();
            documentUploadViewModel = ToViewModelDocUpload(docupload, docuploaddetails);
            return documentUploadViewModel;
        }

        #region ToViewModelDocUpload
        public DocumentUploadViewModel ToViewModelDocUpload(Trn_DocumentUpload docupload, List<Trn_DocUploadDetails> DocUploadTrnList)
        {
            var model = new DocumentUploadViewModel
            {
                KycId = docupload.KycId,
                TransactionNumber = docupload.TransactionNumber,
                DocDate = Convert.ToDateTime(docupload.DocDate).ToShortDateString(),
                CustomerId = docupload.CustomerId,
                ApplicationNo = docupload.ApplicationNo,
                LoanAccountNo = docupload.LoanAccountNo,
                TransactionId = docupload.TransactionId,
                ID = docupload.DocId,
                Comments = docupload.Comments
            };
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
                    Status = c.Status,
                    VerifiedBy = c.VerifiedBy,
                    ReasonForRejection = c.ReasonForRejection
                };
                DocTrnViewModelList.Add(TrnViewModel);
            }
            model.DocumentUploadList = DocTrnViewModelList;

            return model;
        }
        #endregion

        public void SaveRecord(RequestFormViewModel model)
        {
            try
            {
                TGLKYC_BasicDetails tGLKYC_Basic = _context.TGLKYC_BasicDetails.Where(x => x.KYCID == model.KYCID).FirstOrDefault();
                if (model != null)
                {
                    tGLKYC_Basic.BldgHouseName = model.BldgHouseName;
                    tGLKYC_Basic.BldgPlotNo = model.BldgPlotNo;
                    tGLKYC_Basic.BranchID = model.BranchID;
                    tGLKYC_Basic.CityID = model.CityID;
                    tGLKYC_Basic.CmpID = model.CmpID;
                    tGLKYC_Basic.CustomerID = model.CustomerID;
                    tGLKYC_Basic.EmailID = model.EmailID;
                    tGLKYC_Basic.FYID = model.FYID;
                    tGLKYC_Basic.Landmark = model.Landmark;
                    tGLKYC_Basic.MobileNo = model.MobileNo;
                    tGLKYC_Basic.OfficeAddress = model.OfficeAddress;
                    tGLKYC_Basic.Road = model.Road;
                    tGLKYC_Basic.RoomBlockNo = model.RoomBlockNo;
                    tGLKYC_Basic.StateID = model.StateID;
                    tGLKYC_Basic.TelephoneNo = model.TelephoneNo;                   
                    tGLKYC_Basic.ZoneID = model.ZoneID;
                    tGLKYC_Basic.KYCDate = model.KYCDate;
                    tGLKYC_Basic.AdhaarNo = model.AdhaarNo;
                    tGLKYC_Basic.ApplicationNo = model.ApplicationNo;
                    tGLKYC_Basic.PinCode = model.PinCode;
                    tGLKYC_Basic.Distance = model.Distance;
                    tGLKYC_Basic.Area = model.Area;
                    tGLKYC_Basic.UpdatedDate = DateTime.Now;
                    _context.SaveChanges();
                }
                var requestform = new Trn_RequestForm
                {
                    Id = model.TransactionId,
                    KycId = model.KYCID,
                    SanctionId = model.SanctionId,
                    creationdate=DateTime.Now
                };
                _context.Trn_RequestForm.Add(requestform);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
