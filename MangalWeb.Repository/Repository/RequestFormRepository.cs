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
           // model.DocumentUploadList = DocTrnViewModelList;
            return model;
        }
        #endregion

        public void SaveRecord(KYCBasicDetailsVM model)
        {
            try
            {
                TGLKYC_BasicDetails tGLKYC_Basic = _context.TGLKYC_BasicDetails.Where(x => x.KYCID == model.KYCID).FirstOrDefault();
                if (model != null)
                {
                    tGLKYC_Basic.Age = model.Age;
                    tGLKYC_Basic.AppFName = model.AppFName;
                    tGLKYC_Basic.AppliedDate = DateTime.Now;
                    tGLKYC_Basic.BldgHouseName = model.BldgHouseName;
                    tGLKYC_Basic.BldgPlotNo = model.BldgPlotNo;
                    tGLKYC_Basic.BranchID = model.BranchID;
                    tGLKYC_Basic.Children = model.Children;
                    tGLKYC_Basic.CityID = model.CityID;
                    tGLKYC_Basic.CmpID = model.CmpID;
                    tGLKYC_Basic.CustomerID = model.CustomerID;
                    tGLKYC_Basic.DealerID = model.DealerID;
                    tGLKYC_Basic.Designation = model.Designation;
                    tGLKYC_Basic.EmailID = model.EmailID;
                    tGLKYC_Basic.EmploymentType = model.EmploymentType;
                    tGLKYC_Basic.ExistingCustomerID = model.ExistingCustomerID;
                    tGLKYC_Basic.ExistingPLCaseNo = model.ExistingPLCaseNo;
                    tGLKYC_Basic.FYID = model.FYID;
                    tGLKYC_Basic.Gender = model.Gender;
                    tGLKYC_Basic.IndustriesType = model.IndustriesType;
                    tGLKYC_Basic.isActive = true;
                    tGLKYC_Basic.Landmark = model.Landmark;
                    tGLKYC_Basic.LoanpurposeID = model.LoanpurposeID;
                    tGLKYC_Basic.MaritalStatus = model.MaritalStatus;
                    tGLKYC_Basic.MobileNo = model.MobileNo;
                    tGLKYC_Basic.Occupation = model.Occupation;
                    tGLKYC_Basic.OfficeAddress = model.OfficeAddress;
                    tGLKYC_Basic.OperatorID = model.OperatorID;
                    tGLKYC_Basic.OrganizationName = model.OrganizationName;
                    tGLKYC_Basic.PANNo = model.PANNo;
                    tGLKYC_Basic.PresentIncome = model.PresentIncome;
                    tGLKYC_Basic.Road = model.Road;
                    tGLKYC_Basic.RoomBlockNo = model.RoomBlockNo;
                    tGLKYC_Basic.SourceofApplication = model.SourceofApplication;
                    tGLKYC_Basic.SourceofApplicationID = model.SourceofApplicationID;
                    tGLKYC_Basic.SourceSpecification = model.SourceSpecification;
                    tGLKYC_Basic.SpecifyEmployment = model.SpecifyEmployment;
                    tGLKYC_Basic.SpecifyIndustries = model.SpecifyIndustries;
                    tGLKYC_Basic.SpecifyLoanPurpose = model.SpecifyLoanPurpose;
                    tGLKYC_Basic.Spouse = model.Spouse;
                    tGLKYC_Basic.StateID = model.StateID;
                    tGLKYC_Basic.TelephoneNo = model.TelephoneNo;                   
                    tGLKYC_Basic.VerificationCode = model.VerificationCode;
                    tGLKYC_Basic.ZoneID = model.ZoneID;
                    tGLKYC_Basic.KYCDate = DateTime.Now;
                    tGLKYC_Basic.AdhaarNo = model.AdhaarNo;
                    tGLKYC_Basic.ApplicationNo = model.ApplicationNo;
                    tGLKYC_Basic.SourceofApplicationID = model.SourceofApplicationID;
                    tGLKYC_Basic.ApplicantPrefix = model.ApplicantPrefix;
                    tGLKYC_Basic.MotherName = model.MotherName;
                    tGLKYC_Basic.Father_Spouse = model.Father_Spouse;
                    tGLKYC_Basic.CKYCNo = model.CKYCNo;
                    tGLKYC_Basic.SourceType = model.SourceType;
                    tGLKYC_Basic.OccupationOther = model.OccupationOther;
                    tGLKYC_Basic.IndustryOther = model.IndustryOther;
                    tGLKYC_Basic.NomineeMobileNo = model.NomineeMobileNo;
                    tGLKYC_Basic.NomineePanNo = model.NomineePanNo;
                    tGLKYC_Basic.NomineeAdharNo = model.NomineeAdharNo;
                    tGLKYC_Basic.PinCode = model.PinCode;
                    tGLKYC_Basic.Distance = model.Distance;
                    tGLKYC_Basic.Area = model.Area;
                    tGLKYC_Basic.UpdatedBy = model.UpdatedBy;
                    tGLKYC_Basic.UpdatedDate = DateTime.Now;
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
