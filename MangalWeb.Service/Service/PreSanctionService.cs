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
       
        PreSanctionRepository _preSanctionRepository = new PreSanctionRepository();

        #region Public Methods
        public List<TGLPreSanction> GetPreSanctions()
        {
            return _preSanctionRepository.GetPreSanctions().ToList();
        }
        public TGLPreSanctionVM GetPreSanction(int preSanctionId)
        {
            var preSanction = _preSanctionRepository.GetPreSanction(preSanctionId);
            return CreateTGLPreSanctionVM(preSanction);
        }
        public TGLPreSanctionVM SavePreSanction(TGLPreSanctionVM model, out Dictionary<string, string> errors)
        {
            errors = new Dictionary<string, string>();

            var preSanction = CreateTGLPreSanction(model);

            if (model.PreSanctionID <= 0)
            {
                preSanction.CreatedDate = DateTime.Now;
                var dbRecord = _preSanctionRepository.AddPreSanction(preSanction,out errors);

                if (dbRecord != null)
                {
                    model.PreSanctionID = dbRecord.PreSanctionID;
                    return model;
                }
            }
            else if (model.PreSanctionID > 0)
            {
                _preSanctionRepository.UpdatePreSanction(preSanction, out errors);
            }

            return model;
        }
        public bool DeletePreSanction(int preSanctionID, out Dictionary<string, string> errors)
        {
            return _preSanctionRepository.DeletePreSanction(preSanctionID, out errors);
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
                AppliedDate = s.AppliedDate,
                Status = s.AppliedDate > now.AddHours(-24) && s.AppliedDate <= now ? "" : "System Rejected"
            }).ToList();

            var rejKYCs = kycList.Where(k => k.Status == "System Rejected").ToList();
            if(rejKYCs.Count>0)
            {
                _preSanctionRepository.UpdateKYCStatus(rejKYCs);
            }

            var apprKYCs= kycList.Where(k => k.Status != "System Rejected").ToList();

            return apprKYCs;
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
        #endregion Public Methods

        #region Private Methods
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
                TransactionID=model.TransactionID,
                FYID=model.FYID,
                CMPID=model.CMPID,
                BranchID=model.BranchID,


            };
        }
        private TGLPreSanctionVM CreateTGLPreSanctionVM(TGLPreSanction model)
        {
            return new TGLPreSanctionVM
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
                ResidenceVerification = model.ResidenceVerification,
               CustomerID=model.TGLKYC_BasicDetails.CustomerID,
               ApplicationNo=model.TGLKYC_BasicDetails.ApplicationNo,
               AppliedDate= model.TGLKYC_BasicDetails.AppliedDate.ToString("dd/MM/yyyy"),
               TransactionID=model.TransactionID
               

            };
        }
        #endregion Private Methods
    }
}
