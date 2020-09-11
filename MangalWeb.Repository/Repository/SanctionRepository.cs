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
    public class SanctionRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public string GetLoanDate()
        {
            return DateTime.Now.ToShortDateString();
        }

        public int GetMaxTransactionId()
        {
            return _context.TGLSanctionDisburse_BasicDetails.Any() ? _context.TGLSanctionDisburse_BasicDetails.Max(x => x.SID) + 1 : 1;
        }

        public List<tbl_GLChargeMaster_BasicInfo> GetChargeList()
        {
            return _context.tbl_GLChargeMaster_BasicInfo.ToList();
        }

        public string GetLoanNo()
        {
            var localPar = new SqlParameter("@LoanDate", DateTime.Now.ToString("yyyy-MM-dd"));
            var result = _context.Database.SqlQuery<string>("EXEC Gl_SanctionDisburse_GoldLoanNo_RTR @LoanDate=@LoanDate", localPar).FirstOrDefault();
            return result;
        }

        public List<UserDetail> FillCashOutwardBy()
        {
            return _context.UserDetails.ToList();
        }

        public List<UserDetail> FillGoldInwardBy()
        {
            return _context.UserDetails.ToList();
        }

        public List<tblaccountmaster> FillBankAccount()
        {
            //return _context.tblaccountmasters.Where(x => x.GPID == 71 || x.GPID == 11).ToList();
            return _context.tblaccountmasters.ToList();
        }

        public List<tblaccountmaster> FillCashAccount()
        {
            //return _context.tblaccountmasters.Where(x => x.GPID == 70).ToList();
            return _context.tblaccountmasters.ToList();
        }

        public List<SanctionDisbursementVM> GetSanctionDisbursementList()
        {
            var list = new List<SanctionDisbursementVM>();
            int fyid = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
            int branchid = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);
            var FyIdPar = new SqlParameter("@FYID", fyid);
            var BranchIdPar = new SqlParameter("@BranchId", branchid);
            var parameters = new List<SqlParameter>();
            parameters.Add(FyIdPar);
            parameters.Add(BranchIdPar);

            var tablelist = _context.Database.SqlQuery<SanctionDisbursementVM>("EXEC GL_SanctionDisburse_KYC_RTR @FYID=@FYID,@BranchId=@BranchId", parameters.ToArray()).ToList();
            foreach (var item in tablelist)
            {
                var model = new SanctionDisbursementVM();
                model.KYCID = item.KYCID;
                model.CustomerID = item.CustomerID;
                model.AppliedDate = item.AppliedDate;
                model.LoanAccountNo = item.LoanAccountNo;
                model.CustomerName = item.CustomerName;
                model.PANNo = item.PANNo;
                model.MobileNo = item.MobileNo;
                list.Add(model);
            }
            return list;
        }

        public void SaveUpdateRecord(SanctionDisbursementVM model)
        {
            TGLSanctionDisburse_BasicDetails tblSanction = new TGLSanctionDisburse_BasicDetails();
            if (model.ID <= 0)
            {
                model.ID = _context.tblCityMasters.Any() ? _context.tblCityMasters.Max(x => x.CityID) + 1 : 1;
                tblSanction.SDID = model.ID;
                _context.TGLSanctionDisburse_BasicDetails.Add(tblSanction);
            }
            else
            {
                tblSanction = _context.TGLSanctionDisburse_BasicDetails.Where(x => x.SDID == model.ID).FirstOrDefault();
            }
            _context.SaveChanges();
        }

        public SanctionDisbursementVM GetSanctionDisbursementListById(int KycId)
        {
            var model = new SanctionDisbursementVM();
            int fyid = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
            int branchid = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);
            var KycIdPar = new SqlParameter("@KYCID", KycId);
            var FyIdPar = new SqlParameter("@FYID", fyid);
            var BranchIdPar = new SqlParameter("@BranchId", branchid);
            var parameters = new List<SqlParameter>();
            parameters.Add(KycIdPar);
            parameters.Add(FyIdPar);
            parameters.Add(BranchIdPar);

            model = _context.Database.SqlQuery<SanctionDisbursementVM>("EXEC GL_SanctionDisburse_KYC_Details_RTR @KYCID=@KYCID,@FYID=@FYID,@BranchId=@BranchId", parameters.ToArray()).FirstOrDefault();

            model.EligibleLoanAmountValuationDetailsVMList = new List<EligibleLoanAmountValuationDetailsVM>();
            return model;
        }
    }
}
