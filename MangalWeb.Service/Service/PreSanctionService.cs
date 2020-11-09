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
    public class PreSanctionService
    {
        DocumentVerificationRepository _documentVerificationRepository = new DocumentVerificationRepository();
        PreSanctionRepository _preSanctionRepository = new PreSanctionRepository();

        public DocumentUploadViewModel GetAllDocumentUpload(int id)
        {
            return _documentVerificationRepository.GetDoumentUploadById(id);
        }
        public void SaveUpdateRecord(PreSanctionDetailsVM model)
        {
            _preSanctionRepository.SaveUpdateRecord(model);
        }
        public TGLPreSanctionVM SavePreSanction(TGLPreSanctionVM model)
        {
            var preSanction = CreateTGLPreSanction(model);

            if(model.PreSanctionID<=0)
            {
                preSanction.CreatedDate = DateTime.Now;
                var dbRecord = _preSanctionRepository.AddPreSanction(preSanction);

                if(dbRecord!=null)
                {
                    return CreateTGLPreSanctionVM(dbRecord);
                }
            }
            return null;
        }
        public List<KYCBasicDetailsVM> GetCustomerList()
        {
            DateTime now = DateTime.Now;
            var kycDetails = _preSanctionRepository.GetCustomerList().ToList();
            var kycList = kycDetails.Select(s => new KYCBasicDetailsVM
            {
                KYCID = s.KYCID,
                CustomerID = s.CustomerID,
                ApplicationNo = s.ApplicationNo,
                AppliedDate = Convert.ToDateTime(s.ApplicationNo),
                //Status = s.AppliedDate > now.AddHours(-24) && s.AppliedDate <= now ? "" : "System Rejected"
            }).ToList();
            return kycList;
        }

        public List<UserDetail> GetAllRMByBranch()
        {
            return _preSanctionRepository.GetAllRMByBranch();
        }
        public List<Mst_LoanPupose> GetLoanPurposes()
        {
            return _preSanctionRepository.GetLoanPurposes();
        }
        public TGLPreSanctionVM GetCustomerById(int id)
        {
            var customer = _preSanctionRepository.GetCustomerById(id);
            return ToViewModelPreSanctionVM(customer);
        }
        private TGLPreSanctionVM ToViewModelPreSanctionVM(TGLKYC_BasicDetails model)
        {
            Random rand = new Random(100);
            int cid = rand.Next(000000000, 999999999) + 1;

            return new TGLPreSanctionVM
            {
                TransactionID = "T" + cid.ToString(),
                ApplicationNo = model.ApplicationNo,
                AppliedDate = model.AppliedDate.ToString("dd/MM/yyyy"),
                CustomerID = model.CustomerID,
                KYCID = model.KYCID
            };
        }
        private TGLPreSanction CreateTGLPreSanction(TGLPreSanctionVM model)
        {
            return new TGLPreSanction
            {
                PreSanctionID = model.PreSanctionID,
                KYCID = model.KYCID,
                LoanType = model.LoanType,
                LoanPurposeID = model.LoanPurposeID,
                Comments = model.Comments,
                CreatedBy = model.CreatedBy,
                LTV = model.LTV,
                ProductID = model.ProductID,
                ReqLoanAmount = model.ReqLoanAmount,
                RMID = model.RMID,
                ROI = model.ROI,
                SchemeID = model.SchemeID,
                Tenure = model.Tenure,
                ResidenceVerification=model.ResidenceVerification,                

            };
        }
        private TGLPreSanctionVM CreateTGLPreSanctionVM(TGLPreSanction model)
        {
            return new TGLPreSanctionVM
            {
                PreSanctionID = model.PreSanctionID,
                KYCID = model.PreSanctionID,
                LoanType = model.LoanType,
                LoanPurposeID = model.LoanPurposeID,
                Comments = model.Comments,
                CreatedBy = model.CreatedBy,
                LTV = model.LTV,
                ProductID = model.ProductID,
                ReqLoanAmount = model.ReqLoanAmount,
                RMID = model.RMID,
                ROI = model.ROI,
                SchemeID = model.SchemeID,
                Tenure = model.Tenure,
                ResidenceVerification = model.ResidenceVerification,

            };
        }
    }
}
