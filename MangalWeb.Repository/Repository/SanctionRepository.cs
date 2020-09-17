using MangalWeb.Model.Entity;
using MangalWeb.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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

        #region [SanctionDisbursment_PRI]
        public void SanctionDisbursment_PRI(string operation, string value, SanctionDisbursementVM model)
        {
            using (var context = new MangalDBNewEntities())
            {
                using (DbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    if (operation != "Delete")
                    {
                        if (operation == "Save")
                        {
                            var getMaxSanctionId = context.TGLSanctionDisburse_BasicDetails.Any() ? context.TGLSanctionDisburse_BasicDetails.Max(x => x.SDID) + 1 : 1;
                            value = getMaxSanctionId.ToString();

                            var count = _context.GL_SanctionDisburse_PRI(operation, "GOLDITEM", Convert.ToInt32(value), model.LoanType, Convert.ToDateTime(model.TransactionDate),
                                model.LoanAccountNo, model.KYCID, model.EligibleLoanAmount, model.SanctionLoanAmount, 0, model.NetPayable, model.ChqDDNEFT, model.ChqDDNEFTNo,
                               Convert.ToDateTime(model.ChqDDNEFTDate), 0, 0, 0, 0, 0, model.SchemeId, Convert.ToDateTime(model.ChqDDNEFTDate), model.ProofOfOwnerShipImage,
                                "", 0, model.CashOutwardbyNo, model.GoldInwardByNo,
                                1, 1, 1, 1, model.CashAccountNo, model.CashAmount, model.BankAccountNo, model.BankAmount, model.PaymentMode,
                                0, 0, 1, 1, 1, 1, 1, 1, 1, "", "");

                            int GPID = 67; //Group ID of Sundry Debtors
                            int LedgerID = 0;
                            int DJEID = 0;
                            int DJERefNo = 0;
                            string DJERefType = "DJEGL";
                            string DJEReferenceNo = string.Empty;

                            var maxaccountId = _context.tblaccountmasters.Any() ? _context.tblaccountmasters.Max(x => x.AccountID) + 1 : 1;
                            int AccountID = Convert.ToInt32(maxaccountId);
                            //insert the record in Account Master
                            tblaccountmaster tblaccmaster = new tblaccountmaster();
                            tblaccmaster.AccountID = AccountID;
                            tblaccmaster.Name = model.CustomerName;
                            tblaccmaster.Alies = model.LoanAccountNo;
                            tblaccmaster.GPID = GPID;
                            tblaccmaster.OpeningBalance = 0;
                            tblaccmaster.DrCr = "Dr";
                            tblaccmaster.PanNo = model.PANNo;
                            tblaccmaster.InterestRate = 0;
                            tblaccmaster.InterestMethod = "-";
                            tblaccmaster.GroupCompany = "-";
                            tblaccmaster.Address = model.CustomerAddress;
                            tblaccmaster.BankAcNo = "0";
                            tblaccmaster.MICRNo = 0;
                            tblaccmaster.AreaID = model.AreaId;
                            tblaccmaster.Telephone = model.TelephoneNo;
                            tblaccmaster.Mobile = model.MobileNo;
                            tblaccmaster.Fax = "-";
                            tblaccmaster.Email = model.EmailId;
                            tblaccmaster.Suspended = "No";
                            tblaccmaster.GSTIN = "";

                            _context.tblaccountmasters.Add(tblaccmaster);
                            _context.SaveChanges();
                        }

                        int result1 = 1;
                        bool datasaved = false;
                        if (result1 > 0)
                        {
                            datasaved = true;
                        }
                        else
                        {
                            datasaved = false;

                        }
                    }
                }
            }
        }
        #endregion [SanctionDisbursment_PRI]

        #region GetLoanDate
        public string GetLoanDate()
        {
            return DateTime.Now.ToShortDateString();
        }
        #endregion

        #region GetMaxTransactionId
        public int GetMaxTransactionId()
        {
            return _context.TGLSanctionDisburse_BasicDetails.Any() ? _context.TGLSanctionDisburse_BasicDetails.Max(x => (int)x.SID) + 1 : 1;
        }
        #endregion

        #region GetChargeList
        public List<tbl_GLChargeMaster_BasicInfo> GetChargeList()
        {
            return _context.tbl_GLChargeMaster_BasicInfo.ToList();
        }
        #endregion

        #region GetLoanNo
        public string GetLoanNo()
        {
            //var localPar = new SqlParameter("@LoanDate", DateTime.Now.ToString("yyyy-MM-dd"));
            //var result = _context.Database.SqlQuery<string>("EXEC Gl_SanctionDisburse_GoldLoanNo_RTR @LoanDate=@LoanDate", localPar).FirstOrDefault();

            var result = _context.Gl_SanctionDisburse_GoldLoanNo_RTR(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))).FirstOrDefault();
            return result;
        }
        #endregion

        #region FillCashOutwardBy
        public List<UserDetail> FillCashOutwardBy()
        {
            return _context.UserDetails.ToList();
        }
        #endregion

        #region FillGoldInwardBy
        public List<UserDetail> FillGoldInwardBy()
        {
            return _context.UserDetails.ToList();
        }
        #endregion

        #region FillChargeList
        public List<tbl_GLChargeMaster_BasicInfo> FillChargeList()
        {
            return _context.tbl_GLChargeMaster_BasicInfo.Where(x => x.Status == "Active").OrderBy(x => x.ChargeName).ToList();
        }
        #endregion

        #region FillBankAccount
        public List<tblaccountmaster> FillBankAccount()
        {
            return _context.tblaccountmasters.Where(x => x.GPID == 71 || x.GPID == 11).ToList();
        }
        #endregion

        #region FillCashAccount
        public List<tblaccountmaster> FillCashAccount()
        {
            return _context.tblaccountmasters.Where(x => x.GPID == 70).ToList();
        }
        #endregion

        #region FillChargeAccount
        public List<tblaccountmaster> FillChargeAccount()
        {
            return _context.tblaccountmasters.Where(x => x.Suspended == "No").OrderBy(x => x.Name).ToList();
        }
        #endregion

        #region GetSanctionDisbursementList
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
        #endregion

        #region SaveUpdateRecord
        public void SaveUpdateRecord(SanctionDisbursementVM model)
        {
            TGLSanctionDisburse_BasicDetails tblSanction = new TGLSanctionDisburse_BasicDetails();
            if (model.ID <= 0)
            {
                model.ID = _context.TGLSanctionDisburse_BasicDetails.Any() ? _context.TGLSanctionDisburse_BasicDetails.Max(x => x.SDID) + 1 : 1;
                tblSanction.SDID = model.ID;
                _context.TGLSanctionDisburse_BasicDetails.Add(tblSanction);
            }
            else
            {
                tblSanction = _context.TGLSanctionDisburse_BasicDetails.Where(x => x.SDID == model.ID).FirstOrDefault();
            }
            _context.SaveChanges();
        }
        #endregion

        #region GetSanctionDisbursementListById
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

            //model = _context.Database.SqlQuery<SanctionDisbursementVM>("EXEC GL_SanctionDisburse_KYC_Details_RTR @KYCID=@KYCID,@FYID=@FYID,@BranchId=@BranchId", parameters.ToArray()).FirstOrDefault();

            var result = _context.GL_SanctionDisburse_KYC_Details_RTR(KycId, fyid, branchid).FirstOrDefault();

            model.EligibleLoanAmountValuationDetailsVMList = new List<EligibleLoanAmountValuationDetailsVM>();

            return model;
        }
        #endregion
    }
}
