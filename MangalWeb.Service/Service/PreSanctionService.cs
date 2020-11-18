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
        MessageActionRepository _MessageActionRepository = new MessageActionRepository();

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
                var dbRecord = _preSanctionRepository.AddPreSanction(preSanction, out errors);

                if (dbRecord != null)
                {
                    model.PreSanctionID = dbRecord.PreSanctionID;
                    if (errors.Count <= 0)
                    {
                        AddDeviationRange(model);
                    }
                }
            }
            else if (model.PreSanctionID > 0 && model.IsApproval == "1")
            {

                preSanction.Status = model.DeviationApprove;
                preSanction.ApproverComments = model.ApproverComment;

                var dbRecord = _preSanctionRepository.UpdatePreSanctionApprove(preSanction, out errors);
            }
            else if (model.PreSanctionID > 0)
            {

                var dbRecod = _preSanctionRepository.UpdatePreSanction(preSanction, out errors);
                if (errors.Count <= 0)
                {
                    _preSanctionRepository.DeleteMessageAction(dbRecod.MessageActionID ?? 0);
                    AddDeviationRange(model);
                }

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
        public void AddDeviationRange(TGLPreSanctionVM model)
        {
            string pageUrl = "/PreSanction/GetPreSanctionForApprove/" + model.PreSanctionID;
            string message = string.Format("Deviation raised on presanction-Customer({0})", model.CustomerID);
            string remarks = string.Empty;

            var roiDeviation = _preSanctionRepository.GetDeviationRange(1, model.ROI ?? 0);
            if(roiDeviation.IsDeviation??false)
            {
                remarks = string.Format("Deviation raised for ROI which is beyond the range");             

            }

            var tenureDeviation = _preSanctionRepository.GetDeviationRange(5, model.Tenure ?? 0);
            if(tenureDeviation.IsDeviation??false)
            {
                remarks += string.Format(", Deviation raised for Tenure which is beyond the range");
            }

            if(!string.IsNullOrEmpty(remarks))
            {
                var actionId = _MessageActionRepository.AddMessageAction(0, message, remarks, pageUrl, roiDeviation.UserCategoryId ?? 0, true, model.CreatedBy ?? 0);
                _preSanctionRepository.UpdateActionIDToPreSanction(model.PreSanctionID, actionId);
            }


        }
        #endregion Public Methods

        #region Private Methods
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
                ResidenceVerification = model.ResidenceVerification,
                TransactionID = model.TransactionID,
                FYID = model.FYID,
                CMPID = model.CMPID,
                BranchID = model.BranchID,

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
               TransactionID=model.TransactionID,
               ApproverComment=model.ApproverComments,
               DeviationApprove=model.Status

            };
        }
        #endregion Private Methods
    }
}
