using MangalWeb.Model.Entity;
using MangalWeb.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MangalWeb.Repository.Repository
{
    public class PreSanctionRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();
        MessageActionRepository _MessageActionRepository = new MessageActionRepository();

        #region Public Methods
        public IQueryable<TGLKYC_BasicDetails> GetCustomerList()
        {
            var kycIDs = _context.TGLPreSanctions.Select(p => p.KYCID);
            return _context.TGLKYC_BasicDetails.Where(k => !kycIDs.Contains(k.KYCID) && k.Status != "System Rejected").OrderByDescending(order => order.KYCID);
        }
        public IQueryable<TGLPreSanction> GetPreSanctions()
        {
            return _context.TGLPreSanctions;
        }
        public TGLPreSanction GetPreSanction(int preSanctionId)
        {
            return _context.TGLPreSanctions.Where(ps => ps.PreSanctionID == preSanctionId).FirstOrDefault();
        }

        public TGLPreSanction AddPreSanction(TGLPreSanction model, out Dictionary<string, string> errors)
        {
            errors = new Dictionary<string, string>();

            var dbRecord = _context.TGLPreSanctions.Add(model);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                errors.Add("PreSanctionError", "Error adding record.");
            }
            return dbRecord;
        }
        public TGLPreSanction UpdatePreSanction(TGLPreSanction model, out Dictionary<string, string> errors)
        {
            errors = new Dictionary<string, string>();

            var dbRecord = _context.TGLPreSanctions.Where(ps => ps.PreSanctionID == model.PreSanctionID).FirstOrDefault();
            if (dbRecord == null)
            {
                errors.Add("NoRecordFound", "No Record Found.");
                return new TGLPreSanction();
            }

            ValidateRecord(model, errors);
            if (errors.Count > 0)
            {
                return new TGLPreSanction();
            }

            UpdatePreSanction(dbRecord, model);
            
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                errors.Add("PreSanctionError", "Error updating record.");
            }

            return dbRecord;
        }

        public bool DeletePreSanction(int preSanctionID, out Dictionary<string, string> errors)
        {
            errors = new Dictionary<string, string>();

            var dbRecord = _context.TGLPreSanctions.Where(ps => ps.PreSanctionID == preSanctionID).FirstOrDefault();
            if (dbRecord == null)
            {
                errors.Add("NoRecordFound", "No Record Found.");
                return false;
            }

            _context.TGLPreSanctions.Remove(dbRecord);
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                errors.Add("PreSanctionError", "Error updating record.");
            }

            return false;
        }
        public TGLPreSanction UpdatePreSanctionApprove(TGLPreSanction model, out Dictionary<string, string> errors)
        {
            errors = new Dictionary<string, string>();

            var dbRecord = _context.TGLPreSanctions.Where(ps => ps.PreSanctionID == model.PreSanctionID).FirstOrDefault();
            if (dbRecord == null)
            {
                errors.Add("NoRecordFound", "No Record Found.");
                return new TGLPreSanction();
            }
            var messageRecords = _context.MessageActionUsers.Where(mes => mes.MessageActionID == dbRecord.MessageActionID).ToList();
            if (messageRecords.Count <= 0)
            {
                errors.Add("NoRecordFound", "No Record Found.");
                return new TGLPreSanction();
            }

            messageRecords.ForEach(e => e.IsRead = true);

            dbRecord.ApproverComments = model.ApproverComments;
            dbRecord.Status = model.Status;
            dbRecord.LastUpdatedDate = DateTime.Now;

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                errors.Add("PreSanctionError", "Error updating record.");
            }

            return dbRecord;
        }
        
        public int DeleteMessageAction(int messageActionId)
        {
            var dbRecord = _context.TGLPreSanctions.Where(p => p.MessageActionID == messageActionId).FirstOrDefault();
            if(dbRecord!=null)
            {
                dbRecord.MessageActionID = null;
                dbRecord.Status = null;
                dbRecord.ApproverComments = null;

                _context.SaveChanges();
                return _context.DeleteMessageAction(messageActionId);

            }

            
            return 0;
        }
        public GetDeviationRange_Result GetDeviationRange(int parentId, decimal range)
        {            
           return  _context.GetDeviationRange(parentId, range).FirstOrDefault();

        }
       
        public void UpdateActionIDToPreSanction(int preSanctionId, int messageActionId)
        {
            var dbRecord = _context.TGLPreSanctions.Where(p => p.PreSanctionID == preSanctionId).FirstOrDefault();

            if(dbRecord!=null)
            {
                dbRecord.MessageActionID = messageActionId;
                _context.SaveChanges();
            }
        }
        public TGLKYC_BasicDetails GetCustomerById(int kycID)
        {
            return _context.TGLKYC_BasicDetails.Where(k => k.KYCID == kycID).FirstOrDefault();
        }
        
        public PreSanctionDetailsVM ToViewModelPreSanction(KYCBasicDetailsVM model)
        {
            PreSanctionDetailsVM preSanctionDetailsVM = new PreSanctionDetailsVM();
            Random rand = new Random(100);
            int cid = rand.Next(000000000, 999999999) + 1;
            preSanctionDetailsVM.TransactionId = "T" + cid.ToString();
            preSanctionDetailsVM.ApplicationNo = model.ApplicationNo;
            preSanctionDetailsVM.AppliedDate = model.AppliedDate.ToShortDateString();
            preSanctionDetailsVM.CustomerId = model.CustomerID;
            preSanctionDetailsVM.KycId = model.KYCID;
                return preSanctionDetailsVM;
        }       

        public List<UserDetail> GetAllRMByBranch()
        {
            return _context.UserDetails.ToList();
        }
        public List<Mst_LoanPupose> GetLoanPurposes()
        {
            return _context.Mst_LoanPupose.ToList();
        }

        public void UpdateKYCStatus(List<KYCBasicDetailsVM> kycList)
        {
            foreach(var kyc in kycList)
            {
                var dbRecord = _context.TGLKYC_BasicDetails.Where(k => k.KYCID == kyc.KYCID && string.IsNullOrEmpty(k.Status)).FirstOrDefault();
                if(dbRecord!=null)
                {
                    dbRecord.Status = kyc.Status;
                }
            }
            _context.SaveChanges();
        }
        #endregion Public Methods

        #region Private Methods
        private void ValidateRecord(TGLPreSanction model,  Dictionary<string, string> errors)
        {
            if(_context.TGLSanctionDisburse_BasicDetails.Any(sd=>sd.KYCID== model.KYCID))
            {
                errors.Add("InUse", "Record can not be updated,since it is in use.");
            }
        }
        private void UpdatePreSanction(TGLPreSanction dbRecord,TGLPreSanction model)
        {
            dbRecord.Comments = model.Comments;
            dbRecord.KYCID = model.KYCID;
            dbRecord.LastUpdatedBy = model.CreatedBy;
            dbRecord.LastUpdatedDate = DateTime.Now;
            dbRecord.LoanPurposeID = model.LoanPurposeID;
            dbRecord.LoanType = model.LoanType;
            dbRecord.LTV = model.LTV;
            dbRecord.ProductID = model.ProductID;
            dbRecord.RMID = model.RMID;
            dbRecord.ROI = model.ROI;
            dbRecord.SchemeID = model.SchemeID;
            dbRecord.Tenure = model.Tenure;
            dbRecord.TransactionID = model.TransactionID;
            dbRecord.ReqLoanAmount = model.ReqLoanAmount;
            dbRecord.ResidenceVerification = model.ResidenceVerification;
            
        }
        #endregion Private Methods
    }
}
