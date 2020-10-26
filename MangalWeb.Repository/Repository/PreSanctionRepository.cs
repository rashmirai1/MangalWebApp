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

        public IQueryable<TGLKYC_BasicDetails> GetCustomerList()
        {
          
            return _context.TGLKYC_BasicDetails;
            //foreach(var item in kYCBasicDetailsVMs)
            //{
            //    DateTime now = DateTime.Now;
            //    if (item.AppliedDate > now.AddHours(-24) && item.AppliedDate <= now)
            //    {
            //        item.Status = "";
            //    }
            //    else
            //    {
            //        item.Status = "System Rejected";
            //    }
            //}
            //return kYCBasicDetailsVMs;
        }

        public void SaveUpdateRecord(PreSanctionDetailsVM model)
        {
            try
            {
                tbl_PreSanctionDetails tbl_PreSanctionDetails = new tbl_PreSanctionDetails();
                tbl_PreSanctionDetails.CreatedBy = model.CreatedBy;
                tbl_PreSanctionDetails.CreatedDate = DateTime.Now;
                tbl_PreSanctionDetails.ApplicationNo = model.ApplicationNo;
                //tbl_PreSanctionDetails.AppliedDate = model.AppliedDate;
                tbl_PreSanctionDetails.Comments = model.Comments;
                tbl_PreSanctionDetails.CustomerId = model.CustomerId;
                tbl_PreSanctionDetails.IsActive = true;
                tbl_PreSanctionDetails.KycId = model.KycId;
                tbl_PreSanctionDetails.LTV = model.LTV;
                tbl_PreSanctionDetails.NewTopUp = model.NewTopUp;
                tbl_PreSanctionDetails.Product = model.Product;
                tbl_PreSanctionDetails.PurposeofLoan = model.PurposeofLoan;
                tbl_PreSanctionDetails.ReqLoanAmount = model.ReqLoanAmount;
                tbl_PreSanctionDetails.ResidenceVerification = model.ResidenceVerification;
                tbl_PreSanctionDetails.RM = model.RM;
                tbl_PreSanctionDetails.ROI = model.ROI;
                tbl_PreSanctionDetails.Scheme = model.Scheme;
                tbl_PreSanctionDetails.Tenure = model.Tenure;
                tbl_PreSanctionDetails.TransactionId = model.TransactionId;
                _context.tbl_PreSanctionDetails.Add(tbl_PreSanctionDetails);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGLPreSanction AddPreSanction(TGLPreSanction model)
        {
            var dbRecord = _context.TGLPreSanctions.Add(model);
            _context.SaveChanges();
            return dbRecord;
        }

        public TGLKYC_BasicDetails GetCustomerById(int kycID)
        {
            return _context.TGLKYC_BasicDetails.Where(k => k.KYCID == kycID).FirstOrDefault();
        }

        #region ToViewModelPreSanction
        public PreSanctionDetailsVM ToViewModelPreSanction(KYCBasicDetailsVM model)
        {
            PreSanctionDetailsVM preSanctionDetailsVM = new PreSanctionDetailsVM();
            Random rand = new Random(100);
            int cid = rand.Next(000000000, 999999999) + 1;
            preSanctionDetailsVM.TransactionId = "T" + cid.ToString();
            preSanctionDetailsVM.ApplicationNo = model.ApplicationNo;
            preSanctionDetailsVM.AppliedDate = model.AppliedDate;
            preSanctionDetailsVM.CustomerId = model.CustomerID;
            preSanctionDetailsVM.KycId = model.KYCID;
                return preSanctionDetailsVM;
        }
        #endregion

        public List<UserDetail> GetAllRMByBranch()
        {
            return _context.UserDetails.ToList();
        }
        public List<Mst_LoanPupose> GetLoanPurposes()
        {
            return _context.Mst_LoanPupose.ToList();
        }
    }
}
