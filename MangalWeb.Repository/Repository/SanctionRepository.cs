using MangalWeb.Model;
using MangalWeb.Model.Entity;
using MangalWeb.Model.Transaction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace MangalWeb.Repository.Repository
{
    public class SanctionRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        #region GetMaxTransactionId
        public int GetMaxTransactionId()
        {
            return _context.TGLSanctionDisburse_BasicDetails.Any() ? _context.TGLSanctionDisburse_BasicDetails.Max(x => (int)x.SDID) + 1 : 1;
        }
        #endregion        

        #region GetChargeDetails
        public ChargeSanctionVM GetChargeDetails(int chargeid, decimal sanctionloanamt, string ptype, decimal pcharge)
        {
            var model = new ChargeSanctionVM();
            //var data = _context.GL_SanctionDisburse_Charges_RTR(chargeid, sanctionloanamt).FirstOrDefault();
            model = _context.Database.SqlQuery<ChargeSanctionVM>("GL_SanctionDisburse_Charges_RTR @CID,@SanctionAmt",
                new SqlParameter("CID", chargeid),
                new SqlParameter("SanctionAmt", sanctionloanamt)).FirstOrDefault();
            //if (data != null)
            //{
            //    ModelReflection.MapObjects(data, model);
            //}
            //if (ptype == "Amount")
            //{
            //    if (model.Amount > pcharge)
            //    {
            //        model.Amount = pcharge;
            //        model.Charges = pcharge;
            //    }
            //}
            //else if (ptype == "Percentage")
            //{
            //    if (model.Amount > pcharge)
            //    {
            //        model.Amount = Convert.ToDecimal(sanctionloanamt) * pcharge / 100;
            //        model.Charges = model.Amount;
            //    }
            //}
            return model;
        }
        #endregion

        #region DeleteRecord
        public void DeleteRecord(int id)
        {
            using (DbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var count = _context.SP_SanctionDisburse_Delete(id);
                    if (count > 0)
                    {
                        SanctionDisbursementVM model = new SanctionDisbursementVM();
                        model.ID = id;
                        DeleteSanctionRecord(model, transaction);
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        #endregion

        #region SanctionDisbursment_PRI
        public void SanctionDisbursment_PRI(string operation, SanctionDisbursementVM model)
        {
            using (DbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    int value = model.TransactionId;
                    DateTime? strChqDate = null;
                    DateTime? strBankPaymentDate = null;
                    double CGSTAmount = 0;
                    double SGSTAmount = 0;
                    //for save and update both
                    if (operation != "Delete")
                    {
                        if (model.PaymentMode == "Chq/DD/NEFT" || model.PaymentMode == "Both")
                        {
                            if (model.CheqNEFTDDDate.Trim() != "" && model.BankPaymentDate.Trim() != "")
                            {
                                strChqDate = Convert.ToDateTime(model.CheqNEFTDDDate);
                                strBankPaymentDate = Convert.ToDateTime(model.BankPaymentDate);
                            }
                            else
                            {
                                strChqDate = null; model.CheqNEFTDDDate = ""; strBankPaymentDate = null;
                            }
                        }
                        DateTime? GoldInwardDate = null;
                        if (model.GoldInwardDate != null)
                        {
                            GoldInwardDate = Convert.ToDateTime(model.GoldInwardDate);
                        }
                        //insert or update record in sanction disbursement, cash inout and gold in out details table
                        var count = _context.SP_SaveSanctionDisbursement(operation, value, model.LoanType, Convert.ToDateTime(model.TransactionDate),
                        model.LoanAccountNo, model.KYCID, model.EligibleLoanAmount, model.SanctionLoanAmount, model.NetPayable, model.CheqNEFTDD, model.CheqNEFTDDNo,
                       strChqDate, model.TotalGrossWeight, model.TotalNetWeight, model.TotalQuantity, model.TotalValue, model.TotalRatePerGram,
                       model.SchemeId, Convert.ToDateTime(model.InterestRepaymentDate), model.ProofOfOwnerShipImageFile, model.FileName, model.ContentType, "", 0,
                        model.CashOutwardbyNo,model.GoldInwardByNo, model.CreatedBy, model.FinancialYearId, model.BranchId, model.CompanyId, model.CashAccountNo,
                        model.CashAmount, model.BankCashAccID, model.BankAmount, model.PaymentMode, 0, strBankPaymentDate, model.LockerNo, model.PacketWeight, model.RackNo,
                        model.Remark, GoldInwardDate, model.PreSanctionId, model.GSTId,model.CGSTAmount, model.SGSTAmount, model.ProcessingFeeAccountId, model.SchemeProcessingCharge);
                        //update gold item details
                        var tblgolditem = _context.tbl_OrnamentEvaluationDetails.Where(x => x.KycId == model.KYCID).ToList();
                        foreach (var golditem in tblgolditem)
                        {
                            golditem.SDID = Convert.ToInt32(value);
                            _context.SaveChanges();
                        }
                    }
                    if (operation == "Save")
                    {
                        if (model.LoanType == "New")
                        {
                            #region save method
                            //insert record in charge details table
                            foreach (var citem in model.ChargeDetailList)
                            {
                                if (citem != null)
                                {
                                    citem.ID = _context.TGLSanctionDisburse_ChargesDetails.Any() ? _context.TGLSanctionDisburse_ChargesDetails.Max(x => x.CHID) + 1 : 1;
                                    if (citem.ChargeId > 0)
                                    {
                                        var countt = _context.SP_InsertRecordInSanctionChargeDetails(citem.ID, value, citem.CDetailsID, Convert.ToDouble(citem.Charges), Convert.ToDouble(citem.Amount), citem.AccountId, citem.ChargeId, citem.GstId);
                                    }
                                    if (citem.GstId > 0)
                                    {
                                        var countt = _context.SP_InsertRecordInSanctionChargeDetails(citem.ID, value, citem.CDetailsID, Convert.ToDouble(citem.Charges), Convert.ToDouble(citem.Amount), citem.AccountId, citem.ChargeId, citem.GstId);
                                    }
                                }
                            }
                            int GPID = 67; //Group ID of Sundry Debtors
                            int LedgerID = 0;
                            int DJERefNo = 0;
                            string DJERefType = "DJEGL";
                            int BCPID = 0;
                            int VoucherNo = 0;
                            int BankCashAccID = 0;
                            string DJEReferenceNo = string.Empty;
                            double DebitAmount = 0, CreditAmount = 0, BankAmount = 0, CashAmount = 0;
                            string Narration = "";
                            int ContraAccID = 0;
                            //insert record in account master and fsystemgenerated entry master
                            var accountcount = _context.SP_InsertRecordInAccountMaster(model.CustomerName, model.LoanAccountNo, GPID, model.PANNo, model.CustomerAddress,
                                model.AreaId, model.TelephoneNo, model.MobileNo, model.EmailId, model.FinancialYearId);
                            //get  max account id
                            var getMaxAccountId = _context.tblaccountmasters.Any() ? _context.tblaccountmasters.Max(x => x.AccountID) : 1;
                            //get max id from tbankcash_paymentdetails
                            BCPID = _context.TBankCash_PaymentDetails.Any() ? _context.TBankCash_PaymentDetails.Max(x => x.BCPID) + 1 : 1;
                            //get max voucher no from tbankcash_paymentdetails
                            VoucherNo = _context.TBankCash_PaymentDetails.Any() ? _context.TBankCash_PaymentDetails.Max(x => x.VoucherNo) + 1 : 1;
                            //get max id from fsytemgeneratedentrymasters
                            DJERefNo = _context.FSystemGeneratedEntryMasters.Any() ? _context.FSystemGeneratedEntryMasters.Max(x => x.DJEID) : 1;
                            int AccountID = getMaxAccountId;
                            int PaidTo = AccountID;
                            if (model.CashAccountNo > 0)
                            {
                                BankCashAccID = (int)model.CashAccountNo;
                                CashAmount = (double)model.CashAmount;
                                ContraAccID = Convert.ToInt32(model.CashAccountNo);
                            }
                            if (model.BankCashAccID > 0 && HttpContext.Current.Session["BranchCode"].ToString() == "HO")
                            {
                                BankCashAccID = (int)model.BankCashAccID;
                                BankAmount = (double)model.BankAmount;
                                ContraAccID = Convert.ToInt32(model.BankCashAccID);
                            }
                            Narration = "Payment made against New Gold Loan sanctioned"; //for debit only
                            DebitAmount = Convert.ToDouble(model.NetPayable);
                            DJEReferenceNo = DJERefType + "/" + Convert.ToString(DJERefNo);//1st entry
                            //Insert record in Bank Payment details
                            var count = _context.SP_InsertRecordInTBankCashPaymentDetails(BCPID, DJERefType, DJERefNo, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), VoucherNo,
                                 BankCashAccID, AccountID, DebitAmount, model.CheqNEFTDDNo, strChqDate, Narration, model.FinancialYearId);
                            //Entry in fledger master
                            //Debit Entry in FLedger(Main Ledger Entry)
                            //insert record in fledger master of debit entry of Net Payable of customer to cash/bank
                            bool datasaved = false;
                            LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccountID, DebitAmount, CreditAmount, ContraAccID, Narration, model.FinancialYearId);
                            if (LedgerID > 0)
                            {
                                datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccountID, DebitAmount, CreditAmount);
                            }
                            //debit end
                            //update tbank cash payment details of ledgerid  with bcpid
                            var updatepayment = _context.TBankCash_PaymentDetails.Where(x => x.BCPID == BCPID).FirstOrDefault();
                            updatepayment.LedgerID = LedgerID;
                            _context.SaveChanges();
                            //update fsystem generated entry masters of ledger id with djeid
                            var FSystemGenerated = _context.FSystemGeneratedEntryMasters.Where(x => x.DJEID == DJERefNo).FirstOrDefault();
                            FSystemGenerated.LedgerID = LedgerID;
                            _context.SaveChanges();
                            //update TGLSanctionDisburse_BasicDetails of bcpid 
                            var sanctiondetails = _context.TGLSanctionDisburse_BasicDetails.Where(x => x.SDID == value).FirstOrDefault();
                            sanctiondetails.BCPID = BCPID;
                            _context.SaveChanges();
                            //Ledger Account for Bank Entry
                            //credit start
                            int AccID = 0;
                            if (model.BankAmount > 0 && HttpContext.Current.Session["BranchCode"].ToString() == "HO")
                            {
                                DJERefNo = _context.FSystemGeneratedEntryMasters.Any() ? _context.FSystemGeneratedEntryMasters.Max(x => x.DJEID) + 1 : 1;
                                //insert record in FSystemGeneratedEntryMaster table
                                count = _context.SP_InsertRecordInFSystemGeneratedEntryMaster(DJERefNo, DJERefType, DJERefNo, DJEReferenceNo, model.LoanAccountNo, model.FinancialYearId);
                                if (model.PaymentMode == "Chq/DD/NEFT" || model.PaymentMode == "Both")
                                {
                                    AccID = Convert.ToInt32(model.BankCashAccID);
                                    ContraAccID = AccountID;
                                    if (model.BankAmount > 0)
                                    {
                                        BankAmount = Convert.ToDouble(model.BankAmount);
                                        Narration = "Payment made by Bank against New Gold Loan sanctioned";
                                    }
                                }
                                if (model.PaymentMode == "Chq/DD/NEFT" || model.PaymentMode == "Both")
                                {
                                    if (model.CheqNEFTDDDate.Trim() != "")
                                    {
                                        strChqDate = Convert.ToDateTime(model.CheqNEFTDDDate.Trim());
                                    }
                                }
                                else
                                {
                                    strChqDate = null; model.CheqNEFTDDNo = "";
                                }
                                CreditAmount = 0;
                                datasaved = false;
                                LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, CreditAmount, BankAmount, ContraAccID, Narration, model.FinancialYearId);
                                if (LedgerID > 0)
                                {
                                    datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, CreditAmount, BankAmount);
                                }
                                //update fsystem generated entry masters of ledger id with djeid
                                FSystemGenerated = _context.FSystemGeneratedEntryMasters.Where(x => x.DJEID == DJERefNo).FirstOrDefault();
                                FSystemGenerated.LedgerID = LedgerID;
                                _context.SaveChanges();
                            }
                            //Ledger Account for cash entry
                            if (model.CashAmount > 0 && HttpContext.Current.Session["BranchCode"].ToString() != "HO")
                            {
                                DJERefNo = _context.FSystemGeneratedEntryMasters.Any() ? _context.FSystemGeneratedEntryMasters.Max(x => x.DJEID) + 1 : 1;
                                //insert record in FSystemGeneratedEntryMaster table
                                count = _context.SP_InsertRecordInFSystemGeneratedEntryMaster(DJERefNo, DJERefType, DJERefNo, DJEReferenceNo, model.LoanAccountNo, model.FinancialYearId);
                                int CashAccID = Convert.ToInt32(model.CashAccountNo);
                                if (model.PaymentMode == "Cash" || model.PaymentMode == "Both")
                                {
                                    CashAmount = Convert.ToDouble(model.CashAmount);
                                    Narration = "Payment made by Cash against New Gold Loan sanctioned";
                                }
                                AccID = CashAccID;
                                ContraAccID = AccountID;
                                CreditAmount = 0;
                                datasaved = false;
                                LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, CreditAmount, CashAmount, ContraAccID, Narration, model.FinancialYearId);
                                if (LedgerID > 0)
                                {
                                    datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, CreditAmount, CashAmount);
                                }
                                //update fsystem generated entry masters of ledger id with djeid
                                FSystemGenerated = _context.FSystemGeneratedEntryMasters.Where(x => x.DJEID == DJERefNo).FirstOrDefault();
                                FSystemGenerated.LedgerID = LedgerID;
                                _context.SaveChanges();
                            }
                            //***************************** Accounting Entries for Charges started ***************************************
                            Narration = "Amount received against New Gold Loan charges";
                            if (model.ChargeDetailList != null && model.ChargeDetailList.Count > 0)
                            {
                                foreach (var chargeitem in model.ChargeDetailList)
                                {
                                    if (chargeitem.ChargeId != null && (chargeitem.ChargeId > 0 || chargeitem.GstId > 0))
                                    {
                                        int CreditID = 0;
                                        CGSTAmount = 0;
                                        SGSTAmount = 0;
                                        CreditID = Convert.ToInt32(chargeitem.AccountId);
                                        if (CreditID > 0)
                                        {
                                            AccID = AccountID;
                                            ContraAccID = CreditID;
                                            DebitAmount = Convert.ToDouble(chargeitem.Amount);
                                            CreditAmount = 0;
                                            //retrive max id from charge posting details
                                            var maxpostingid = _context.TGLSanctionDisburse_ChargesPostingDetails.Any() ? _context.TGLSanctionDisburse_ChargesPostingDetails.Max(x => x.ID) + 1 : 1;
                                            //insert record in charge posting details
                                            count = _context.SP_InsertRecordInChargePostingDetails(maxpostingid, value, model.LoanAccountNo, AccID, DebitAmount, CreditAmount, LedgerID, model.FinancialYearId);
                                        }
                                        // Contra Entry in FLedger (Ledger Entry for Charges)      
                                        AccID = AccountID;
                                        ContraAccID = Convert.ToInt32(chargeitem.AccountId);
                                        DebitAmount = 0;
                                        CreditAmount = Convert.ToDouble(chargeitem.Amount);
                                        LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), ContraAccID, DebitAmount, CreditAmount, AccID, Narration, model.FinancialYearId);
                                        if (LedgerID > 0)
                                        {
                                            datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, ContraAccID, DebitAmount, CreditAmount);
                                        }
                                        //debit side in customer account
                                        LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, CreditAmount, DebitAmount, ContraAccID, Narration, model.FinancialYearId);
                                        if (LedgerID > 0)
                                        {
                                            datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, CreditAmount, DebitAmount);
                                        }
                                        var maxchargepostingid = _context.TGLSanctionDisburse_ChargesPostingDetails.Any() ? _context.TGLSanctionDisburse_ChargesPostingDetails.Max(x => x.ID) + 1 : 1;
                                        //insert record in charge posting details
                                        count = _context.SP_InsertRecordInChargePostingDetails(maxchargepostingid, value, model.LoanAccountNo, ContraAccID, DebitAmount, CreditAmount, LedgerID, model.FinancialYearId);
                                    }
                                }
                            }
                            //***************************** Accounting Entries for Charges end ***************************************
                            //***************************** Accounting Entries for Other Charges Start *********************************
                            Narration = "Amount received against Gold Loan processing charges";
                            AccID = 62;
                            int ConAccID = AccountID;
                            double DebitAmt = 0;
                            double CreditAmt = 0;
                            CGSTAmount = 0;
                            SGSTAmount = 0;
                            DebitAmt = 0;
                            CreditAmt = 0;
                            if (model.SchemeProcessingType == "Percentage")
                            {
                                //CreditAmt = Convert.ToDouble(model.NetPayable) * Convert.ToDouble(model.SchemeProcessingCharge) / 100;
                                CreditAmt = Convert.ToDouble(model.SanctionLoanAmount) * Convert.ToDouble(model.SchemeProcessingCharge) / 100;
                                if (CreditAmt > Convert.ToDouble(model.SchemeProcessingLimit))
                                {
                                    CreditAmt = Convert.ToDouble(model.SchemeProcessingLimit);
                                }
                                CGSTAmount = CreditAmt * Convert.ToDouble(model.CGSTTax) / 100;
                                SGSTAmount = CreditAmt * Convert.ToDouble(model.SGSTTax) / 100;
                                CreditAmt = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(CreditAmt), 2));
                                CGSTAmount = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(CGSTAmount), 2));
                                SGSTAmount = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(SGSTAmount), 2));
                            }
                            else
                            {
                                CreditAmt = Convert.ToDouble(model.SchemeProcessingCharge);
                                if (CreditAmt > Convert.ToDouble(model.SchemeProcessingLimit))
                                {
                                    CreditAmt = Convert.ToDouble(model.SchemeProcessingLimit);
                                }
                                CGSTAmount = CreditAmt * Convert.ToDouble(model.CGSTTax) / 100;
                                SGSTAmount = CreditAmt * Convert.ToDouble(model.SGSTTax) / 100;
                                CreditAmt = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(CreditAmt), 2));
                                CGSTAmount = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(CGSTAmount), 2));
                                SGSTAmount = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(SGSTAmount), 2));
                            }
                            LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, DebitAmt, CreditAmt, ConAccID, Narration, model.FinancialYearId);
                            if (LedgerID > 0)
                            {
                                datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, DebitAmt, CreditAmt);
                            }
                            //debit side in customer account
                            LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), ConAccID, CreditAmt, DebitAmt, AccID, Narration, model.FinancialYearId);
                            if (LedgerID > 0)
                            {
                                datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, ConAccID, CreditAmt, DebitAmt);
                            }
                            Narration = "Amount received against GST Charges";
                            AccID = Convert.ToInt32(model.CGSTAccountId);
                            LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, DebitAmt, CGSTAmount, ConAccID, Narration, model.FinancialYearId);
                            if (LedgerID > 0)
                            {
                                datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, DebitAmt, CGSTAmount);
                            }
                            LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), ConAccID, CGSTAmount, DebitAmt, AccID, Narration, model.FinancialYearId);
                            if (LedgerID > 0)
                            {
                                datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, ConAccID, CGSTAmount, DebitAmt);
                            }
                            if (model.SGSTAmount > 0)
                            {
                                AccID = Convert.ToInt32(model.SGSTAccountId);
                                LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, DebitAmt, SGSTAmount, ConAccID, Narration, model.FinancialYearId);
                                if (LedgerID > 0)
                                {
                                    datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, DebitAmt, SGSTAmount);
                                }
                                LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), ConAccID, SGSTAmount, DebitAmt, AccID, Narration, model.FinancialYearId);
                                if (LedgerID > 0)
                                {
                                    datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, ConAccID, SGSTAmount, DebitAmt);
                                }
                            }
                            //***************************** Accounting Entries for Other Charges End *********************************
                            _context.SaveChanges();
                            transaction.Commit();
                            SendMessage(model.MobileNo, model.SanctionLoanAmount, model.LoanAccountNo);
                            SendEmail(model.EmailId, model.SanctionLoanAmount, model.LoanAccountNo);
                            #endregion
                        }
                        else
                        {
                            #region save method
                            //insert record in charge details table                            
                            foreach (var citem in model.ChargeDetailList)
                            {
                                if (citem != null)
                                {
                                    citem.ID = _context.TGLSanctionDisburse_ChargesDetails.Any() ? _context.TGLSanctionDisburse_ChargesDetails.Max(x => x.CHID) + 1 : 1;
                                    if (citem.ChargeId > 0)
                                    {
                                        var countt = _context.SP_InsertRecordInSanctionChargeDetails(citem.ID, value, citem.CDetailsID, Convert.ToDouble(citem.Charges), Convert.ToDouble(citem.Amount), citem.AccountId, citem.ChargeId, citem.GstId);
                                    }
                                    if (citem.GstId > 0)
                                    {
                                        var countt = _context.SP_InsertRecordInSanctionChargeDetails(citem.ID, value, citem.CDetailsID, Convert.ToDouble(citem.Charges), Convert.ToDouble(citem.Amount), citem.AccountId, citem.ChargeId, citem.GstId);
                                    }
                                }
                            }
                            int GPID = 67; //Group ID of Sundry Debtors
                            int LedgerID = 0;
                            int DJERefNo = 0;
                            string PrevDJEReferenceNo = "", DJERefType = "DJEGL";
                            int BCPID = 0;
                            int VoucherNo = 0;
                            int BankCashAccID = 0;
                            string DJEReferenceNo = string.Empty;
                            double Amount = 0, DebitAmount = 0, CreditAmount = 0, BankAmount = 0, CashAmount = 0;
                            string Narration = "";
                            int ContraAccID = 0;
                            //check record in FSystemGeneratedEntryMaster 
                            //get old gold loan no
                            var OldGoldLoanNo = _context.TGLSanctionDisburse_BasicDetails.Where(x => x.KYCID == model.KYCID).Select(x => x.GoldLoanNo).FirstOrDefault();
                            PrevDJEReferenceNo = _context.FSystemGeneratedEntryMasters.Where(x => x.LoginID == OldGoldLoanNo).Select(x => x.ReferenceNo).FirstOrDefault();

                            //insert record in account master and fsystemgenerated entry master
                            var accountcount = _context.SP_InsertRecordInAccountMaster(model.CustomerName, model.LoanAccountNo, GPID, model.PANNo, model.CustomerAddress,
                                model.AreaId, model.TelephoneNo, model.MobileNo, model.EmailId, model.FinancialYearId);
                            //get  max account id
                            var getMaxAccountId = _context.tblaccountmasters.Any() ? _context.tblaccountmasters.Max(x => x.AccountID) : 1;
                            //get max id from tbankcash_paymentdetails
                            BCPID = _context.TBankCash_PaymentDetails.Any() ? _context.TBankCash_PaymentDetails.Max(x => x.BCPID) + 1 : 1;
                            //get max voucher no from tbankcash_paymentdetails
                            VoucherNo = _context.TBankCash_PaymentDetails.Any() ? _context.TBankCash_PaymentDetails.Max(x => x.VoucherNo) + 1 : 1;
                            //get max id from fsytemgeneratedentrymasters
                            DJERefNo = _context.FSystemGeneratedEntryMasters.Any() ? _context.FSystemGeneratedEntryMasters.Max(x => x.DJEID) : 1;
                            var OldAccountId = _context.tblaccountmasters.Where(x => x.Alies == OldGoldLoanNo).Select(x => x.AccountID).FirstOrDefault();
                            int NewAccountID = getMaxAccountId;
                            int PaidTo = NewAccountID;
                            int AccID = 0;
                            //------------------------for interest-----------------------------------------
                            bool datasaved = false;
                            if (model.Interest > 0)
                            {
                                AccID = OldAccountId;
                                ContraAccID = 63;
                                DebitAmount = Convert.ToDouble(model.Interest);
                                CreditAmount = 0;
                                Narration = "Amount received against Gold Loan Interest";
                                LedgerID = CreateNormalLedgerEntries(DJERefType, PrevDJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, DebitAmount, CreditAmount, ContraAccID, Narration, model.FinancialYearId);
                                if (LedgerID > 0)
                                {
                                    datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, DebitAmount, CreditAmount);
                                }
                                AccID = 63;
                                ContraAccID = OldAccountId;
                                DebitAmount = 0;
                                CreditAmount = Convert.ToDouble(model.Interest);
                                LedgerID = CreateNormalLedgerEntries(DJERefType, PrevDJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, DebitAmount, CreditAmount, ContraAccID, Narration, model.FinancialYearId);
                                if (LedgerID > 0)
                                {
                                    datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, DebitAmount, CreditAmount);
                                }
                            }
                            //------------------------------end of interest------------------------------------

                            //------------------------for penal interest-----------------------------------------
                            if (model.PenalInterest > 0)
                            {
                                AccID = OldAccountId;
                                ContraAccID = 64;
                                DebitAmount = Convert.ToDouble(model.PenalInterest);
                                CreditAmount = 0;
                                Narration = "Amount received against Gold Loan penal Interest";
                                LedgerID = CreateNormalLedgerEntries(DJERefType, PrevDJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, DebitAmount, CreditAmount, ContraAccID, Narration, model.FinancialYearId);
                                if (LedgerID > 0)
                                {
                                    datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, DebitAmount, CreditAmount);
                                }
                                AccID = 64;
                                ContraAccID = OldAccountId;
                                DebitAmount = 0;
                                CreditAmount = Convert.ToDouble(model.PenalInterest);
                                LedgerID = CreateNormalLedgerEntries(DJERefType, PrevDJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, DebitAmount, CreditAmount, ContraAccID, Narration, model.FinancialYearId);
                                if (LedgerID > 0)
                                {
                                    datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, DebitAmount, CreditAmount);
                                }
                            }
                            //--------------------end of penal interest------------------------------------------------

                            //------------------------for charges-----------------------------------------
                            if (model.Charges > 0)
                            {
                                AccID = OldAccountId;
                                ContraAccID = 65;
                                DebitAmount = Convert.ToDouble(model.Charges);
                                CreditAmount = 0;
                                Narration = "Amount received against Gold Loan Charges";
                                LedgerID = CreateNormalLedgerEntries(DJERefType, PrevDJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, DebitAmount, CreditAmount, ContraAccID, Narration, model.FinancialYearId);
                                if (LedgerID > 0)
                                {
                                    datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, DebitAmount, CreditAmount);
                                }
                                AccID = 65;
                                ContraAccID = OldAccountId;
                                DebitAmount = 0;
                                CreditAmount = Convert.ToDouble(model.Charges);
                                LedgerID = CreateNormalLedgerEntries(DJERefType, PrevDJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, DebitAmount, CreditAmount, ContraAccID, Narration, model.FinancialYearId);
                                if (LedgerID > 0)
                                {
                                    datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, DebitAmount, CreditAmount);
                                }
                            }
                            //--------------------end of penal interest------------------------------------------------
                            //------------------------for Entry for total outstanding to Customer old account on credit side -----------------------------------------
                            if (model.Total > 0)
                            {
                                AccID = OldAccountId;
                                ContraAccID = NewAccountID;
                                DebitAmount = 0;
                                CreditAmount = Convert.ToDouble(model.Total);
                                Narration = "Amount received against Top Up Gold Loan outstanding";
                                LedgerID = CreateNormalLedgerEntries(DJERefType, PrevDJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, DebitAmount, CreditAmount, ContraAccID, Narration, model.FinancialYearId);
                                if (LedgerID > 0)
                                {
                                    datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, DebitAmount, CreditAmount);
                                }
                                AccID = NewAccountID;
                                ContraAccID = OldAccountId;
                                DebitAmount = Convert.ToDouble(model.Total);
                                CreditAmount = 0;
                                Narration = "Amount received against Top Up Gold Loan outstanding";
                                LedgerID = CreateNormalLedgerEntries(DJERefType, PrevDJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, DebitAmount, CreditAmount, ContraAccID, Narration, model.FinancialYearId);
                                if (LedgerID > 0)
                                {
                                    datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, DebitAmount, CreditAmount);
                                }
                            }
                            if (model.CashAccountNo > 0)
                            {
                                BankCashAccID = (int)model.CashAccountNo;
                                CashAmount = (double)model.CashAmount;
                                ContraAccID = Convert.ToInt32(model.CashAccountNo);
                            }
                            if (model.BankCashAccID > 0 && HttpContext.Current.Session["BranchCode"].ToString() == "HO")
                            {
                                BankCashAccID = (int)model.BankCashAccID;
                                BankAmount = (double)model.BankAmount;
                                ContraAccID = Convert.ToInt32(model.BankCashAccID);
                            }
                            Narration = "Payment made against Top up Gold Loan sanctioned"; //for debit only
                            Amount = Convert.ToDouble(model.NetPayable);
                            DJEReferenceNo = DJERefType + "/" + Convert.ToString(DJERefNo);//1st entry
                            //Insert record in Bank Payment details
                            //entry in bank cash payment table
                            var count = _context.SP_InsertRecordInTBankCashPaymentDetails(BCPID, DJERefType, DJERefNo, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), VoucherNo,
                                 BankCashAccID, NewAccountID, Amount, model.CheqNEFTDDNo, strChqDate, Narration, model.FinancialYearId);
                            //Entry in fledger master
                            //Debit Entry in FLedger(Main Ledger Entry)
                            AccID = NewAccountID;
                            CreditAmount = 0;
                            DebitAmount = Convert.ToDouble(model.NetPayable); //for entry in closing A/C
                            //insert record in fledger master of debit entry of sanction loan amount of customer to bank
                            LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, DebitAmount, CreditAmount, ContraAccID, Narration, model.FinancialYearId);
                            if (LedgerID > 0)
                            {
                                datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, DebitAmount, CreditAmount);
                            }
                            //debit end
                            //update tbank cash payment details of ledgerid  with bcpid
                            var updatepayment = _context.TBankCash_PaymentDetails.Where(x => x.BCPID == BCPID).FirstOrDefault();
                            updatepayment.LedgerID = LedgerID;
                            _context.SaveChanges();
                            //update fsystem generated entry masters of ledger id with djeid
                            var FSystemGenerated = _context.FSystemGeneratedEntryMasters.Where(x => x.DJEID == DJERefNo).FirstOrDefault();
                            FSystemGenerated.LedgerID = LedgerID;
                            _context.SaveChanges();
                            //update TGLSanctionDisburse_BasicDetails of bcpid 
                            var sanctiondetails = _context.TGLSanctionDisburse_BasicDetails.Where(x => x.SDID == value).FirstOrDefault();
                            sanctiondetails.BCPID = BCPID;
                            _context.SaveChanges();
                            //Ledger Account for Bank Entry
                            //credit start
                            if (model.BankAmount > 0 && HttpContext.Current.Session["BranchCode"].ToString() == "HO")
                            {
                                DJERefNo = _context.FSystemGeneratedEntryMasters.Any() ? _context.FSystemGeneratedEntryMasters.Max(x => x.DJEID) + 1 : 1;
                                //insert record in FSystemGeneratedEntryMaster table
                                count = _context.SP_InsertRecordInFSystemGeneratedEntryMaster(DJERefNo, DJERefType, DJERefNo, DJEReferenceNo, model.LoanAccountNo, model.FinancialYearId);
                                if (model.PaymentMode == "Chq/DD/NEFT" || model.PaymentMode == "Both")
                                {
                                    AccID = Convert.ToInt32(model.BankCashAccID);
                                    ContraAccID = NewAccountID;
                                    BankAmount = Convert.ToDouble(model.BankAmount);
                                    Narration = "Payment made by Bank against Top up Gold Loan sanctioned";
                                }
                                if (model.PaymentMode == "Chq/DD/NEFT" || model.PaymentMode == "Both")
                                {
                                    if (model.CheqNEFTDDDate.Trim() != "")
                                    {
                                        strChqDate = Convert.ToDateTime(model.CheqNEFTDDDate.Trim());
                                    }
                                }
                                else
                                {
                                    strChqDate = null; model.CheqNEFTDDNo = "";
                                }
                                CreditAmount = 0;
                                datasaved = false;
                                LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, CreditAmount, BankAmount, ContraAccID, Narration, model.FinancialYearId);
                                if (LedgerID > 0)
                                {
                                    datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, CreditAmount, BankAmount);
                                }
                                //update fsystem generated entry masters of ledger id with djeid
                                FSystemGenerated = _context.FSystemGeneratedEntryMasters.Where(x => x.DJEID == DJERefNo).FirstOrDefault();
                                FSystemGenerated.LedgerID = LedgerID;
                                _context.SaveChanges();
                            }
                            //Ledger Account for cash entry
                            if (model.CashAmount > 0 && HttpContext.Current.Session["BranchCode"].ToString() != "HO")
                            {
                                DJERefNo = _context.FSystemGeneratedEntryMasters.Any() ? _context.FSystemGeneratedEntryMasters.Max(x => x.DJEID) + 1 : 1;
                                //insert record in FSystemGeneratedEntryMaster table
                                count = _context.SP_InsertRecordInFSystemGeneratedEntryMaster(DJERefNo, DJERefType, DJERefNo, DJEReferenceNo, model.LoanAccountNo, model.FinancialYearId);
                                int CashAccID = Convert.ToInt32(model.CashAccountNo);
                                if (model.PaymentMode == "Cash" || model.PaymentMode == "Both")
                                {
                                    CashAmount = Convert.ToDouble(model.CashAmount);
                                    Narration = "Payment made by Cash against Top up Gold Loan sanctioned";
                                }
                                AccID = CashAccID;
                                ContraAccID = NewAccountID;
                                CreditAmount = 0;
                                datasaved = false;
                                LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, CreditAmount, CashAmount, ContraAccID, Narration, model.FinancialYearId);
                                if (LedgerID > 0)
                                {
                                    datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, CreditAmount, CashAmount);
                                }
                                //update fsystem generated entry masters of ledger id with djeid
                                FSystemGenerated = _context.FSystemGeneratedEntryMasters.Where(x => x.DJEID == DJERefNo).FirstOrDefault();
                                FSystemGenerated.LedgerID = LedgerID;
                                _context.SaveChanges();
                            }
                            //***************************** Accounting Entries for Charges started ***************************************
                            Narration = "Amount received against Gold Loan charges";
                            if (model.ChargeDetailList != null && model.ChargeDetailList.Count > 0)
                            {
                                foreach (var chargeitem in model.ChargeDetailList)
                                {
                                    if (chargeitem.ChargeId != null && (chargeitem.ChargeId > 0 || chargeitem.GstId > 0))
                                    {
                                        int CreditID = 0;
                                        CGSTAmount = 0;
                                        SGSTAmount = 0;
                                        CreditID = Convert.ToInt32(chargeitem.AccountId);
                                        AccID = NewAccountID;
                                        ContraAccID = CreditID;
                                        DebitAmount = Convert.ToDouble(chargeitem.Amount);
                                        CreditAmount = 0;
                                        //retrive max id from charge posting details
                                        var maxpostingid = _context.TGLSanctionDisburse_ChargesPostingDetails.Any() ? _context.TGLSanctionDisburse_ChargesPostingDetails.Max(x => x.ID) + 1 : 1;
                                        //insert record in charge posting details
                                        count = _context.SP_InsertRecordInChargePostingDetails(maxpostingid, value, model.LoanAccountNo, AccID, DebitAmount, CreditAmount, LedgerID, model.FinancialYearId);
                                        // Contra Entry in FLedger (Ledger Entry for Charges)      
                                        DebitAmount = 0;
                                        CreditAmount = Convert.ToDouble(chargeitem.Amount);
                                        LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), ContraAccID, DebitAmount, CreditAmount, AccID, Narration, model.FinancialYearId);
                                        if (LedgerID > 0)
                                        {
                                            datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, ContraAccID, DebitAmount, CreditAmount);
                                        }
                                        //debit side in customer account
                                        LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, CreditAmount, DebitAmount, ContraAccID, Narration, model.FinancialYearId);
                                        if (LedgerID > 0)
                                        {
                                            datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, CreditAmount, DebitAmount);
                                        }
                                        //insert record in TGLSanctionDisburse_ChargesPostingDetails
                                        var maxchargepostingid = _context.TGLSanctionDisburse_ChargesPostingDetails.Any() ? _context.TGLSanctionDisburse_ChargesPostingDetails.Max(x => x.ID) + 1 : 1;
                                        //insert record in charge posting details
                                        count = _context.SP_InsertRecordInChargePostingDetails(maxchargepostingid, value, model.LoanAccountNo, ContraAccID, DebitAmount, CreditAmount, LedgerID, model.FinancialYearId);
                                    }
                                }
                            }
                            //***************************** Accounting Entries for Charges end ***************************************
                            //***************************** Accounting Entries for Other Charges Start *********************************
                            Narration = "Amount received against Top up Gold Loan processing charges";
                            AccID = 62;
                            int ConAccID = NewAccountID;
                            double DebitAmt = 0;
                            double CreditAmt = 0;
                            CGSTAmount = 0;
                            SGSTAmount = 0;
                            if (model.SchemeProcessingType == "Percentage")
                            {
                                //CreditAmt = Convert.ToDouble(model.NetPayable) * Convert.ToDouble(model.SchemeProcessingCharge) / 100;
                                CreditAmt = Convert.ToDouble(model.SanctionLoanAmount) * Convert.ToDouble(model.SchemeProcessingCharge) / 100;
                                if (CreditAmt > Convert.ToDouble(model.SchemeProcessingLimit))
                                {
                                    CreditAmt = Convert.ToDouble(model.SchemeProcessingLimit);
                                }
                                CGSTAmount = CreditAmt * Convert.ToDouble(model.CGSTTax) / 100;
                                SGSTAmount = CreditAmt * Convert.ToDouble(model.SGSTTax) / 100;
                                CreditAmt = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(CreditAmt), 2));
                                CGSTAmount = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(CGSTAmount), 2));
                                SGSTAmount = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(SGSTAmount), 2));
                            }
                            else
                            {
                                CreditAmt = Convert.ToDouble(model.SchemeProcessingCharge);
                                if (CreditAmt > Convert.ToDouble(model.SchemeProcessingLimit))
                                {
                                    CreditAmt = Convert.ToDouble(model.SchemeProcessingLimit);
                                }
                                CGSTAmount = CreditAmt * Convert.ToDouble(model.CGSTTax) / 100;
                                SGSTAmount = CreditAmt * Convert.ToDouble(model.SGSTTax) / 100;
                                CreditAmt = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(CreditAmt), 2));
                                CGSTAmount = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(CGSTAmount), 2));
                                SGSTAmount = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(SGSTAmount), 2));
                            }
                            //insert record in Processing Charge Account
                            LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, DebitAmt, CreditAmt, ConAccID, Narration, model.FinancialYearId);
                            if (LedgerID > 0)
                            {
                                datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, DebitAmt, CreditAmt);
                            }
                            //debit side in customer account
                            LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), ConAccID, CreditAmt, DebitAmt, AccID, Narration, model.FinancialYearId);
                            if (LedgerID > 0)
                            {
                                datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, ConAccID, CreditAmt, DebitAmt);
                            }
                            Narration = "Amount received against Top up GST Charges";
                            AccID = Convert.ToInt32(model.CGSTAccountId);
                            LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, DebitAmt, CGSTAmount, ConAccID, Narration, model.FinancialYearId);
                            if (LedgerID > 0)
                            {
                                datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, DebitAmt, CGSTAmount);
                            }
                            LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), ConAccID, CGSTAmount, DebitAmt, AccID, Narration, model.FinancialYearId);
                            if (LedgerID > 0)
                            {
                                datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, ConAccID, CGSTAmount, DebitAmt);
                            }
                            if (model.SGSTAmount > 0)
                            {
                                AccID = Convert.ToInt32(model.SGSTAccountId);
                                LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, DebitAmt, SGSTAmount, ConAccID, Narration, model.FinancialYearId);
                                if (LedgerID > 0)
                                {
                                    datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, DebitAmt, SGSTAmount);
                                }
                                LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), ConAccID, SGSTAmount, DebitAmt, AccID, Narration, model.FinancialYearId);
                                if (LedgerID > 0)
                                {
                                    datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, ConAccID, SGSTAmount, DebitAmt);
                                }
                            }
                            //***************************** Accounting Entries for Other Charges End *********************************
                            _context.SaveChanges();
                            transaction.Commit();
                            #endregion
                        }
                    }
                    if (operation == "Update")
                    {
                        #region Update
                        if (model.BankCashAccID > 0 && HttpContext.Current.Session["BranchCode"].ToString() == "HO")
                        {

                        }
                        else
                        {
                            int AccID = 0;
                            int LedgerID = 0;
                            double DebitAmount = 0;
                            double CreditAmount = 0;
                            bool datasaved = false;
                            //first select AccID, Debit, Credit, LedgerID from TGLSanctionDisburse_ChargesPostingDetails with the help of sanction and disbursement id
                            #region ChargePosting
                            //delete data from TGLSanctionDisburse_ChargesPostingDetails using sdid
                            var deleteposting = _context.TGLSanctionDisburse_ChargesPostingDetails.Where(x => x.SDID == model.ID).ToList();
                            if (deleteposting != null && deleteposting.Count > 0)
                            {
                                foreach (var item1 in deleteposting)
                                {
                                    _context.TGLSanctionDisburse_ChargesPostingDetails.Remove(item1);
                                    _context.SaveChanges();
                                }
                            }
                            //delete data from TGLSanctionDisburse_ChargesDetails using sdid
                            var deletecharge = _context.TGLSanctionDisburse_ChargesDetails.Where(x => x.SDID == model.ID).ToList();
                            if (deletecharge != null && deletecharge.Count > 0)
                            {
                                foreach (var item1 in deletecharge)
                                {
                                    _context.TGLSanctionDisburse_ChargesDetails.Remove(item1);
                                    _context.SaveChanges();
                                }
                            }
                            #endregion
                            //insert record in charge details table
                            foreach (var citem in model.ChargeDetailList)
                            {
                                if (citem != null)
                                {
                                    citem.ID = _context.TGLSanctionDisburse_ChargesDetails.Any() ? _context.TGLSanctionDisburse_ChargesDetails.Max(x => x.CHID) + 1 : 1;
                                    if (citem.ChargeId > 0)
                                    {
                                        var countt = _context.SP_InsertRecordInSanctionChargeDetails(citem.ID, value, citem.CDetailsID, Convert.ToDouble(citem.Charges), Convert.ToDouble(citem.Amount), citem.AccountId, citem.ChargeId, citem.GstId);
                                    }
                                    if (citem.GstId > 0)
                                    {
                                        var countt = _context.SP_InsertRecordInSanctionChargeDetails(citem.ID, value, citem.CDetailsID, Convert.ToDouble(citem.Charges), Convert.ToDouble(citem.Amount), citem.AccountId, citem.ChargeId, citem.GstId);
                                    }
                                }
                            }
                            int BCPID = 0;
                            var bcpid = _context.TGLSanctionDisburse_BasicDetails.Where(x => x.SDID == model.ID).Select(x => x.BCPID).FirstOrDefault();
                            if (bcpid > 0)
                            {
                                BCPID = Convert.ToInt32(bcpid);
                            }
                            double Amount = 0;
                            double CashAmount = 0;
                            double BankAmount = 0;
                            int BankCashAccID = 0;
                            CGSTAmount = 0;
                            SGSTAmount = 0;
                            Amount = Convert.ToDouble(model.NetPayable);
                            if (model.CashAccountNo > 0)
                            {
                                BankCashAccID = (int)model.CashAccountNo;
                                CashAmount = (double)model.CashAmount;
                            }
                            if (model.BankCashAccID > 0 && HttpContext.Current.Session["BranchCode"].ToString() == "HO")
                            {
                                BankCashAccID = (int)model.BankCashAccID;
                                BankAmount = (double)model.BankAmount;
                            }
                            //Updating table TBankCash_PaymentDetails
                            var tblbankcashdetails = _context.TBankCash_PaymentDetails.Where(x => x.BCPID == BCPID).FirstOrDefault();
                            tblbankcashdetails.RefDate = Convert.ToDateTime(model.TransactionDate);
                            tblbankcashdetails.BankCashAccID = BankCashAccID;
                            tblbankcashdetails.Amount = Amount;
                            _context.SaveChanges();
                            //updation of Ledger and Company Wise Account Closing tables
                            int accountID = 0;
                            LedgerID = 0;
                            AccID = 0;
                            int ContraAccID = 0;
                            double debit, credit = 0;
                            DebitAmount = 0;
                            CreditAmount = 0;
                            DateTime dtRefDate;
                            string DJEReferenceNo = "";
                            var refno = _context.FSystemGeneratedEntryMasters.Where(x => x.LoginID == model.LoanAccountNo).Select(x => x.ReferenceNo).FirstOrDefault();
                            if (refno != null)
                            {
                                DJEReferenceNo = refno;
                            }
                            var tblledger = _context.FLedgerMasters.Where(x => x.ReferenceNo == DJEReferenceNo).ToList();
                            if (tblledger != null && tblledger.Count > 0)
                            {
                                foreach (var item2 in tblledger)
                                {
                                    accountID = Convert.ToInt32(item2.AccountID);
                                    debit = Convert.ToDouble(item2.Debit);
                                    credit = Convert.ToDouble(item2.Credit);
                                    dtRefDate = Convert.ToDateTime(item2.RefDate);
                                    LedgerID = Convert.ToInt32(item2.LedgerID);
                                    datasaved = CompanyWiseYearEndAccountClosingonDelete(model.FinancialYearId, model.CompanyId, model.BranchId, accountID, debit, credit);
                                }
                            }
                            //delete data from FLedgerMasters using sdidd
                            var deleteledger1 = _context.FLedgerMasters.Where(x => x.ReferenceNo == DJEReferenceNo).ToList();
                            if (deleteledger1 != null && deleteledger1.Count > 0)
                            {
                                foreach (var item4 in deleteledger1)
                                {
                                    _context.FLedgerMasters.Remove(item4);
                                    _context.SaveChanges();
                                }
                            }
                            //**************************** Accounting Entries for Charges ***************************************
                            string Narration = "Payment made against New Gold Loan sanctioned";
                            string DJERefType = "DJEGL";
                            var accid = _context.tblaccountmasters.Where(x => x.Alies == model.LoanAccountNo).Select(x => x.AccountID).FirstOrDefault();
                            if (accid > 0)
                            {
                                accountID = accid;
                            }
                            DebitAmount = 0;
                            CreditAmount = 0;
                            ContraAccID = BankCashAccID;
                            DebitAmount = Convert.ToDouble(model.NetPayable);
                            LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), accountID, DebitAmount, CreditAmount, ContraAccID, Narration, model.FinancialYearId);
                            if (datasaved)
                            {
                                datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, accountID, DebitAmount, CreditAmount);
                            }
                            if (model.BankAmount > 0 && HttpContext.Current.Session["BranchCode"].ToString() == "HO")
                            {
                                #region Bank Entry
                                ContraAccID = accountID;
                                AccID = Convert.ToInt32(model.BankCashAccID);
                                if (model.LoanType == "New")     //for New Loan
                                {
                                    if (model.PaymentMode == "Chq/DD/NEFT" || model.PaymentMode == "Both")
                                    {
                                        if (model.BankAmount != 0)
                                        {
                                            BankAmount = Convert.ToDouble(model.BankAmount);
                                            Narration = "Payment made by Bank against New Gold Loan sanctioned";
                                        }
                                    }
                                }
                                else
                                {
                                    Narration = "Payment made by Bank against Topup Gold Loan sanctioned";
                                }
                                if (model.PaymentMode == "Chq/DD/NEFT" || model.PaymentMode == "Both")
                                {
                                    if (model.CheqNEFTDDDate.Trim() != "")
                                    {
                                        strChqDate = Convert.ToDateTime(model.CheqNEFTDDDate.Trim());
                                    }
                                }
                                else
                                {
                                    strChqDate = null; model.CheqNEFTDDNo = "";
                                }
                                DebitAmount = BankAmount;
                                CreditAmount = 0;
                                datasaved = false;
                                LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, CreditAmount, DebitAmount, ContraAccID, Narration, model.FinancialYearId);
                                if (LedgerID > 0)
                                {
                                    datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, CreditAmount, DebitAmount);
                                }
                                #endregion
                            }
                            if (model.CashAmount > 0 && HttpContext.Current.Session["BranchCode"].ToString() != "HO")
                            {
                                #region Cash Entry
                                ContraAccID = accountID;
                                AccID = Convert.ToInt32(model.CashAccountNo);
                                CashAmount = Convert.ToDouble(model.CashAmount);
                                if (model.LoanType == "New")     //for New Loan
                                {
                                    if (model.PaymentMode == "Cash" || model.PaymentMode == "Both")
                                    {
                                        Narration = "Payment made by Cash against New Gold Loan sanctioned";
                                    }
                                }
                                else
                                {
                                    Narration = "Payment made by Cash against Topup Gold Loan sanctioned";
                                }
                                DebitAmount = CashAmount;
                                CreditAmount = 0;
                                datasaved = false;
                                LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, CreditAmount, DebitAmount, ContraAccID, Narration, model.FinancialYearId);
                                if (LedgerID > 0)
                                {
                                    datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, CreditAmount, DebitAmount);
                                }
                                #endregion
                            }
                            //RefType and ReferenceNo
                            var data = _context.FSystemGeneratedEntryMasters.Where(x => x.LoginID == model.LoanAccountNo).FirstOrDefault();
                            if (data != null)
                            {
                                DJERefType = data.RefType;
                                DJEReferenceNo = data.ReferenceNo;
                            }
                            Narration = "Amount received against Gold Loan charges";
                            if (model.ChargeDetailList != null && model.ChargeDetailList.Count > 0)
                            {
                                foreach (var chargeitem in model.ChargeDetailList)
                                {
                                    if (chargeitem.ChargeId != null && (chargeitem.ChargeId > 0 || chargeitem.GstId > 0))
                                    {
                                        int CreditID = 0;
                                        CGSTAmount = 0;
                                        SGSTAmount = 0;
                                        CreditID = Convert.ToInt32(chargeitem.AccountId);
                                        AccID = accountID;
                                        ContraAccID = CreditID;
                                        DebitAmount = Convert.ToDouble(chargeitem.Amount);
                                        CreditAmount = 0;
                                        //retrive max id from charge posting details
                                        var maxpostingid = _context.TGLSanctionDisburse_ChargesPostingDetails.Any() ? _context.TGLSanctionDisburse_ChargesPostingDetails.Max(x => x.ID) + 1 : 1;
                                        //insert record in charge posting details
                                        var count1 = _context.SP_InsertRecordInChargePostingDetails(maxpostingid, model.ID, model.LoanAccountNo, AccID, DebitAmount, CreditAmount, LedgerID, model.FinancialYearId);
                                        // Contra Entry in FLedger (Ledger Entry for Charges)      
                                        ContraAccID = Convert.ToInt32(chargeitem.AccountId);
                                        DebitAmount = 0;
                                        CreditAmount = Convert.ToDouble(chargeitem.Amount);
                                        LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), ContraAccID, DebitAmount, CreditAmount, AccID, Narration, model.FinancialYearId);
                                        if (LedgerID > 0)
                                        {
                                            datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, ContraAccID, DebitAmount, CreditAmount);
                                        }
                                        //debit side in customer account
                                        LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, CreditAmount, DebitAmount, ContraAccID, Narration, model.FinancialYearId);
                                        if (LedgerID > 0)
                                        {
                                            datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, CreditAmount, DebitAmount);
                                        }
                                        //insert record in TGLSanctionDisburse_ChargesPostingDetails
                                        var maxchargepostingid = _context.TGLSanctionDisburse_ChargesPostingDetails.Any() ? _context.TGLSanctionDisburse_ChargesPostingDetails.Max(x => x.ID) + 1 : 1;
                                        //insert record in charge posting details
                                        var count = _context.SP_InsertRecordInChargePostingDetails(maxchargepostingid, value, model.LoanAccountNo, ContraAccID, DebitAmount, CreditAmount, LedgerID, model.FinancialYearId);
                                    }
                                }
                            }
                            //***************************** Accounting Entries for Other Charges Start *********************************
                            Narration = "Amount received against Gold Loan processing charges";
                            AccID = 62;
                            int ConAccID = accountID;
                            double DebitAmt = 0;
                            double CreditAmt = 0;
                            CGSTAmount = 0;
                            SGSTAmount = 0;
                            DebitAmt = 0;
                            CreditAmt = 0;
                            if (model.SchemeProcessingType == "Percentage")
                            {
                                CreditAmt = Convert.ToDouble(model.SanctionLoanAmount) * Convert.ToDouble(model.SchemeProcessingCharge) / 100;
                                if (CreditAmt > Convert.ToDouble(model.SchemeProcessingLimit))
                                {
                                    CreditAmt = Convert.ToDouble(model.SchemeProcessingLimit);
                                }
                                CGSTAmount = CreditAmt * Convert.ToDouble(model.CGSTTax) / 100;
                                SGSTAmount = CreditAmt * Convert.ToDouble(model.SGSTTax) / 100;
                                CreditAmt = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(CreditAmt), 2));
                                CGSTAmount = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(CGSTAmount), 2));
                                SGSTAmount = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(SGSTAmount), 2));
                            }
                            else
                            {
                                CreditAmt = Convert.ToDouble(model.SchemeProcessingCharge);
                                if (CreditAmt > Convert.ToDouble(model.SchemeProcessingLimit))
                                {
                                    CreditAmt = Convert.ToDouble(model.SchemeProcessingLimit);
                                }
                                CGSTAmount = CreditAmt * Convert.ToDouble(model.CGSTTax) / 100;
                                SGSTAmount = CreditAmt * Convert.ToDouble(model.SGSTTax) / 100;
                                CreditAmt = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(CreditAmt), 2));
                                CGSTAmount = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(CGSTAmount), 2));
                                SGSTAmount = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(SGSTAmount), 2));
                            }
                            LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, DebitAmt, CreditAmt, ConAccID, Narration, model.FinancialYearId);
                            if (LedgerID > 0)
                            {
                                datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, DebitAmt, CreditAmt);
                            }
                            //debit side in customer account
                            LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), ConAccID, CreditAmt, DebitAmt, AccID, Narration, model.FinancialYearId);
                            if (LedgerID > 0)
                            {
                                datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, ContraAccID, CreditAmt, DebitAmt);
                            }
                            Narration = "Amount received against GST Charges";
                            AccID = Convert.ToInt32(model.CGSTAccountId);
                            LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, DebitAmt, CGSTAmount, ConAccID, Narration, model.FinancialYearId);
                            if (LedgerID > 0)
                            {
                                datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, DebitAmt, CGSTAmount);
                            }
                            LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), ConAccID, CGSTAmount, DebitAmt, AccID, Narration, model.FinancialYearId);
                            if (LedgerID > 0)
                            {
                                datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, ConAccID, CGSTAmount, DebitAmt);
                            }
                            if (model.SGSTAmount > 0)
                            {
                                AccID = Convert.ToInt32(model.SGSTAccountId);
                                LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, DebitAmt, SGSTAmount, ConAccID, Narration, model.FinancialYearId);
                                if (LedgerID > 0)
                                {
                                    datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, AccID, DebitAmt, SGSTAmount);
                                }
                                LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), ConAccID, SGSTAmount, DebitAmt, AccID, Narration, model.FinancialYearId);
                                if (LedgerID > 0)
                                {
                                    datasaved = CompanyWiseYearEndAccountClosingonSave(model.FinancialYearId, model.CompanyId, model.BranchId, ConAccID, SGSTAmount, DebitAmt);
                                }
                            }
                            _context.SaveChanges();
                            transaction.Commit();
                        }
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        #endregion SanctionDisbursment_PR

        #region DeleteSanctionRecord
        public void DeleteSanctionRecord(SanctionDisbursementVM model, DbContextTransaction transaction)
        {
            #region Delete
            int accountID = 0;
            int LedgerID = 0;
            double debit, credit = 0;
            DateTime dtRefDate;
            bool datasaved = false;
            var model1 = _context.TGLSanctionDisburse_BasicDetails.Where(x => x.SDID == model.ID).FirstOrDefault();
            model.LoanAccountNo = model1.GoldLoanNo;
            model.BranchId = Convert.ToInt32(model1.BranchID);
            model.FinancialYearId = Convert.ToInt32(model1.FYID);
            if (model1.SDID > 0)
            {
                var refno = _context.FSystemGeneratedEntryMasters.Where(x => x.LoginID == model.LoanAccountNo).Select(x => x.ReferenceNo).FirstOrDefault();
                var accid = _context.tblaccountmasters.Where(x => x.Alies == model.LoanAccountNo).Select(x => x.AccountID).FirstOrDefault();
                var tblledger = _context.FLedgerMasters.Where(x => x.ReferenceNo == refno).ToList();
                if (tblledger != null && tblledger.Count > 0)
                {
                    foreach (var item2 in tblledger)
                    {
                        accountID = Convert.ToInt32(item2.AccountID);
                        debit = Convert.ToDouble(item2.Debit);
                        credit = Convert.ToDouble(item2.Credit);
                        dtRefDate = Convert.ToDateTime(item2.RefDate);
                        LedgerID = Convert.ToInt32(item2.LedgerID);
                        datasaved = CompanyWiseYearEndAccountClosingonDelete(model.FinancialYearId, model.CompanyId, model.BranchId, accountID, debit, credit);
                    }
                }
                //Delete record from FLedgerMaster,TBankCash_PaymentDetails,FSystemGeneratedEntryMaster,tblaccountmaster,TGLSanctionDisburse_ChargesDetails,TGLSanctionDisburse_ChargesPostingDetails
                var count = _context.DeleteSanctionDisbursementData(accid, refno, model.LoanAccountNo, model.ID);
                //update sdid tbl_OrnamentEvaluationDetails
                var tblgolditem = _context.tbl_OrnamentEvaluationDetails.Where(x => x.SDID == model.ID).ToList();
                if (tblgolditem != null && tblgolditem.Count > 0)
                {
                    foreach (var item5 in tblgolditem)
                    {
                        item5.SDID = 0;
                        _context.SaveChanges();
                    }
                }
                _context.SaveChanges();
            }
            #endregion
        }
        #endregion

        #region CreateNormalLedgerEntries
        protected int CreateNormalLedgerEntries(string Reftype, string ReferenceNo, DateTime RefDate, int AccID, double DebitAmount, double CreditAmount, int ContraAccID, string Narration, int FinancialYearId)
        {
            int LedgerID = 0;
            try
            {
                LedgerID = _context.FLedgerMasters.Any() ? _context.FLedgerMasters.Max(x => x.LedgerID) + 1 : 1;
                FLedgerMaster tblFLedgerMaster = new FLedgerMaster();
                tblFLedgerMaster.LedgerID = LedgerID;
                tblFLedgerMaster.ReferenceNo = ReferenceNo;
                tblFLedgerMaster.RefType = Reftype;
                tblFLedgerMaster.RefDate = RefDate;
                tblFLedgerMaster.AccountID = AccID;
                tblFLedgerMaster.Debit = DebitAmount;
                tblFLedgerMaster.Credit = CreditAmount;
                tblFLedgerMaster.Narration = Narration;
                tblFLedgerMaster.ContraAccID = ContraAccID;
                tblFLedgerMaster.FinanceYear = FinancialYearId;
                _context.FLedgerMasters.Add(tblFLedgerMaster);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return LedgerID;
        }
        #endregion CreateNormalLedgerEntries

        #region CompanyWiseYearEndAccountClosingonSave
        public bool CompanyWiseYearEndAccountClosingonSave(int FYID, int compID, int branchID, int accountID, double debit, double credit)
        {
            bool datasaved = false;
            try
            {
                string DrCr = string.Empty;
                double openingBalanceDebit = 0;
                double openingBalanceCredit = 0;
                double prevDebit = 0;
                double prevCredit = 0;
                double currentDebit = 0;
                double currentCredit = 0;
                double closingBalanceDebit = 0;
                double closingBalanceCredit = 0;
                int ID = 0;
                //to check whether AccountID is present in " FCompanyYearEndClosing table " for the selected financial year.
                var FCompanyID = _context.FCompanyYearEndClosings.Where(x => x.FinancialyearID == FYID && x.AccountID == accountID).FirstOrDefault();
                if (FCompanyID != null && FCompanyID.ID > 0)
                {
                    ID = (int)FCompanyID.ID;
                }
                #region [if Account ID does not exist]
                //if Account ID does not exist
                if (ID == 0)
                {
                    openingBalanceDebit = 0;
                    openingBalanceCredit = 0;
                    closingBalanceDebit = 0;
                    closingBalanceCredit = 0;

                    currentDebit = debit;
                    currentCredit = credit;

                    if (currentDebit > 0)
                    {
                        closingBalanceDebit = ((openingBalanceDebit + currentDebit) - currentCredit);
                        //if Closing Balance Debit is negative it will go to Credit side
                        if (closingBalanceDebit < 0)
                        {
                            string strClosingBalanceDebit = Convert.ToString(closingBalanceDebit);
                            closingBalanceCredit = Convert.ToDouble(strClosingBalanceDebit.Replace("-", ""));
                            closingBalanceDebit = 0;
                        }
                    }
                    else if (currentCredit > 0)
                    {
                        closingBalanceCredit = ((openingBalanceCredit + currentCredit) - currentDebit);

                        //if Closing Balance Credit is negative it will go to Debit side
                        if (closingBalanceCredit < 0)
                        {
                            string strclosingBalanceCredit = Convert.ToString(closingBalanceCredit);
                            closingBalanceDebit = Convert.ToDouble(strclosingBalanceCredit.Replace("-", ""));
                            closingBalanceCredit = 0;
                        }
                    }
                    ID = 0;
                    //getting MAX ID
                    int? FID = _context.FCompanyYearEndClosings.Any() ? _context.FCompanyYearEndClosings.Max(x => x.ID) + 1 : 1;
                    if (FID != null && FID > 0)
                    {
                        ID = (int)FID;
                    }
                    var cc = _context.SP_InsertrecordinFCompanyYearEndClosing(ID, FYID, compID, accountID, openingBalanceDebit, openingBalanceCredit, currentDebit, currentCredit, closingBalanceDebit, closingBalanceCredit);
                }
                #endregion [if Account ID does not exist]

                #region [if Account ID exists]
                else    //if Account ID exists
                {
                    var item = _context.FCompanyYearEndClosings.Where(x => x.ID == ID).FirstOrDefault();
                    //retrieving OpeningBalanceDebit, OpeningBalanceCredit, CurrentDebit, CurrentCredit
                    if (item != null)
                    {
                        openingBalanceDebit = (double)item.OpeningBalanceDebit;
                        openingBalanceCredit = (double)item.OpeningBalanceCredit;
                        prevDebit = (double)item.CurrentDebit;
                        prevCredit = (double)item.CurrentCredit;
                    }
                    else
                    {
                        openingBalanceDebit = 0;
                        openingBalanceCredit = 0;
                        prevDebit = 0;
                        prevCredit = 0;
                    }
                    closingBalanceDebit = 0;
                    closingBalanceCredit = 0;
                    currentDebit = prevDebit + debit;
                    currentCredit = prevCredit + credit;
                    if (openingBalanceDebit > 0 && openingBalanceCredit == 0)
                    {
                        closingBalanceDebit = ((openingBalanceDebit + currentDebit) - currentCredit);
                    }
                    else if (openingBalanceCredit > 0 && openingBalanceDebit == 0)
                    {
                        closingBalanceCredit = ((openingBalanceCredit + currentCredit) - currentDebit);
                    }
                    else if (openingBalanceDebit == 0 && openingBalanceCredit == 0)
                    {
                        if (currentDebit > 0 && currentDebit > currentCredit)
                        {
                            closingBalanceDebit = ((openingBalanceDebit + currentDebit) - currentCredit);
                        }
                        else if (currentCredit > 0 && currentCredit > currentDebit)
                        {
                            closingBalanceCredit = ((openingBalanceCredit + currentCredit) - currentDebit);
                        }
                        else if ((currentDebit == 0 && currentCredit == 0) || (currentDebit == currentCredit))
                        {
                            closingBalanceDebit = ((openingBalanceDebit + currentDebit) - currentCredit);
                        }
                    }
                    //if Closing Balance Debit is negative it will go to Credit side
                    if (closingBalanceDebit < 0)
                    {
                        string strClosingBalanceDebit = Convert.ToString(closingBalanceDebit);
                        closingBalanceCredit = Convert.ToDouble(strClosingBalanceDebit.Replace("-", ""));
                        closingBalanceDebit = 0;
                    }
                    //if Closing Balance Credit is negative it will go to Debit side
                    if (closingBalanceCredit < 0)
                    {
                        string strclosingBalanceCredit = Convert.ToString(closingBalanceCredit);
                        closingBalanceDebit = Convert.ToDouble(strclosingBalanceCredit.Replace("-", ""));
                        closingBalanceCredit = 0;
                    }
                    //updating table FCompanyYearEndClosing
                    var updateQuery = _context.FCompanyYearEndClosings.Where(x => x.ID == ID).FirstOrDefault();
                    updateQuery.OpeningBalanceDebit = openingBalanceDebit;
                    updateQuery.OpeningBalanceCredit = openingBalanceCredit;
                    updateQuery.CurrentDebit = currentDebit;
                    updateQuery.CurrentCredit = currentCredit;
                    updateQuery.ClosingBalanceDebit = closingBalanceDebit;
                    updateQuery.ClosingBalanceCredit = closingBalanceCredit;
                    _context.SaveChanges();

                }
                #endregion [if Account ID exists]
                datasaved = true;
            }
            catch (Exception ex)
            {
                datasaved = false;
                throw ex;
            }
            finally
            {
            }
            return datasaved;
        }
        #endregion CompanyWiseYearEndAccountClosingonSave

        #region CompanyWiseYearEndAccountClosingonDelete
        public bool CompanyWiseYearEndAccountClosingonDelete(int FYID, int compID, int branchID, int accountID, double debit, double credit)
        {
            bool datasaved = false;
            try
            {
                string DrCr = string.Empty;
                double openingBalanceDebit = 0;
                double openingBalanceCredit = 0;
                double prevDebit = 0;
                double prevCredit = 0;
                double currentDebit = 0;
                double currentCredit = 0;
                double closingBalanceDebit = 0;
                double closingBalanceCredit = 0;
                int ID = 0;

                //to check whether AccountID is present in " FCompanyYearEndClosing table " for the selected financial year.
                var FCompanyID = _context.FCompanyYearEndClosings.Where(x => x.FinancialyearID == FYID && x.AccountID == accountID).FirstOrDefault();
                if (FCompanyID != null && FCompanyID.ID > 0)
                {
                    ID = (int)FCompanyID.ID;
                }
                #region [if Account ID does not exist]
                //if Account ID does not exist
                if (ID == 0)
                {
                    openingBalanceDebit = 0;
                    openingBalanceCredit = 0;
                    closingBalanceDebit = 0;
                    closingBalanceCredit = 0;

                    currentDebit = debit;
                    currentCredit = credit;

                    if (currentDebit > 0)
                    {
                        closingBalanceDebit = ((openingBalanceDebit + (-currentDebit)) - currentCredit);
                        //if Closing Balance Debit is negative it will go to Credit side
                        if (closingBalanceDebit < 0)
                        {
                            string strClosingBalanceDebit = Convert.ToString(closingBalanceDebit);
                            closingBalanceCredit = Convert.ToDouble(strClosingBalanceDebit.Replace("-", ""));
                            closingBalanceDebit = 0;
                        }
                    }
                    else if (currentCredit > 0)
                    {
                        closingBalanceCredit = ((openingBalanceCredit + currentCredit) - currentDebit);

                        //if Closing Balance Credit is negative it will go to Debit side
                        if (closingBalanceCredit < 0)
                        {
                            string strclosingBalanceCredit = Convert.ToString(closingBalanceCredit);
                            closingBalanceDebit = Convert.ToDouble(strclosingBalanceCredit.Replace("-", ""));
                            closingBalanceCredit = 0;
                        }
                    }
                    ID = 0;
                    //getting MAX ID
                    int? FID = _context.FCompanyYearEndClosings.Any() ? _context.FCompanyYearEndClosings.Max(x => x.ID) + 1 : 1;
                    if (FID != null && FID > 0)
                    {
                        ID = (int)FID;
                    }
                    var cc = _context.SP_InsertrecordinFCompanyYearEndClosing(ID, FYID, compID, accountID, openingBalanceDebit, openingBalanceCredit, currentDebit, currentCredit, closingBalanceDebit, closingBalanceCredit);
                }
                #endregion [if Account ID does not exist]

                #region [if Account ID exists]
                else    //if Account ID exists
                {
                    var item = _context.FCompanyYearEndClosings.Where(x => x.ID == ID).FirstOrDefault();
                    //retrieving OpeningBalanceDebit, OpeningBalanceCredit, CurrentDebit, CurrentCredit
                    if (item != null)
                    {
                        openingBalanceDebit = Math.Round((double)item.OpeningBalanceDebit, 2);
                        openingBalanceCredit = Math.Round((double)item.OpeningBalanceCredit, 2);
                        prevDebit = Math.Round((double)item.CurrentDebit, 2);
                        prevCredit = Math.Round((double)item.CurrentCredit, 2);
                    }
                    else
                    {
                        openingBalanceDebit = 0;
                        openingBalanceCredit = 0;
                        prevDebit = 0;
                        prevCredit = 0;
                    }
                    closingBalanceDebit = 0;
                    closingBalanceCredit = 0;
                    currentDebit = prevDebit - debit;
                    currentCredit = prevCredit - credit;
                    if (openingBalanceDebit > 0 && openingBalanceCredit == 0)
                    {
                        closingBalanceDebit = ((openingBalanceDebit + currentDebit) - currentCredit);
                    }
                    else if (openingBalanceCredit > 0 && openingBalanceDebit == 0)
                    {
                        closingBalanceCredit = ((openingBalanceCredit + currentCredit) - currentDebit);
                    }
                    else if (openingBalanceDebit == 0 && openingBalanceCredit == 0)
                    {
                        if (currentDebit > 0 && currentDebit > currentCredit)
                        {
                            closingBalanceDebit = ((openingBalanceDebit + currentDebit) - currentCredit);
                        }
                        else if (currentCredit > 0 && currentCredit > currentDebit)
                        {
                            closingBalanceCredit = ((openingBalanceCredit + currentCredit) - currentDebit);
                        }
                        else if ((currentDebit == 0 && currentCredit == 0) || (currentDebit == currentCredit))
                        {
                            closingBalanceDebit = ((openingBalanceDebit + currentDebit) - currentCredit);
                        }
                    }
                    //if Closing Balance Debit is negative it will go to Credit side
                    if (closingBalanceDebit < 0)
                    {
                        string strClosingBalanceDebit = Convert.ToString(closingBalanceDebit);
                        closingBalanceCredit = Convert.ToDouble(strClosingBalanceDebit.Replace("-", ""));
                        closingBalanceDebit = 0;
                    }
                    //if Closing Balance Credit is negative it will go to Debit side
                    if (closingBalanceCredit < 0)
                    {
                        string strclosingBalanceCredit = Convert.ToString(closingBalanceCredit);
                        closingBalanceDebit = Convert.ToDouble(strclosingBalanceCredit.Replace("-", ""));
                        closingBalanceCredit = 0;
                    }
                    //updating table FCompanyYearEndClosing
                    var updateQuery = _context.FCompanyYearEndClosings.Where(x => x.ID == ID).FirstOrDefault();
                    updateQuery.OpeningBalanceDebit = openingBalanceDebit;
                    updateQuery.OpeningBalanceCredit = openingBalanceCredit;
                    updateQuery.CurrentDebit = currentDebit;
                    updateQuery.CurrentCredit = currentCredit;
                    updateQuery.ClosingBalanceDebit = closingBalanceDebit;
                    updateQuery.ClosingBalanceCredit = closingBalanceCredit;
                    _context.SaveChanges();
                }
                #endregion [if Account ID exists]
                datasaved = true;
            }
            catch (Exception ex)
            {
                datasaved = false;
                throw ex;
            }
            finally
            {
            }
            return datasaved;
        }
        #endregion CompanyWiseYearEndAccountClosingonDelete        

        #region GetLoanNo
        public string GetLoanNo()
        {
            var result = _context.Gl_SanctionDisburse_GoldLoanNo_RTR(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))).FirstOrDefault();
            return result;
        }
        #endregion

        #region Fill List from database

        #region GetChargeList
        public List<tbl_GLChargeMaster_BasicInfo> GetChargeList()
        {
            return _context.tbl_GLChargeMaster_BasicInfo.Where(x => x.CID != 5 && x.Status == "Active").OrderBy(x => x.ChargeName).ToList();
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

        #region FillBankAccount
        public List<tblaccountmaster> FillBankAccount()
        {
            return _context.tblaccountmasters.Where(x => x.GPID == 71 || x.GPID == 11).OrderBy(x => x.Name).ToList();
        }
        #endregion

        #region FillCashAccount
        public List<tblaccountmaster> FillCashAccount()
        {
            return _context.tblaccountmasters.Where(x => x.GPID == 70).OrderBy(x => x.Name).ToList();
        }
        #endregion

        #region FillChargeAccount
        public List<tblaccountmaster> FillChargeAccount()
        {
            return _context.tblaccountmasters.Where(x => x.Suspended == "No").OrderBy(x => x.Name).ToList();
        }
        #endregion

        #region GetAccountName
        public string GetAccountName(int id)
        {
            return _context.tblaccountmasters.Where(x => x.AccountID == id).Select(x => x.Name).FirstOrDefault();
        }
        #endregion

        #region GetImageById
        public TGLSanctionDisburse_BasicDetails GetImageById(int id)
        {
            return _context.TGLSanctionDisburse_BasicDetails.Where(x => x.SDID == id).FirstOrDefault();
        }
        #endregion

        #endregion

        #region GetKycDetailsList
        public List<SanctionDisbursementVM> GetKycDetailsList()
        {
            var list = new List<SanctionDisbursementVM>();
            int fyid = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
            int branchid = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);
            var FyIdPar = new SqlParameter("@FYID", fyid);
            var BranchIdPar = new SqlParameter("@BranchId", branchid);
            var parameters = new List<SqlParameter>();
            parameters.Add(FyIdPar);
            parameters.Add(BranchIdPar);
            list = _context.Database.SqlQuery<SanctionDisbursementVM>("EXEC GL_SanctionDisburse_KYC_RTR @FYID=@FYID,@BranchId=@BranchId", parameters.ToArray()).ToList();
            return list;
        }
        #endregion

        #region GetKycListById
        public SanctionDisbursementVM GetKycListById(int PreSanctionId)
        {
            var model = new SanctionDisbursementVM();
            var topupmodel = new SanctionTopUpVM();
            var interestcalculation = new List<SanctionEMICalculatorVM>();
            int fyid = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
            int branchid = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);

            model = _context.Database.SqlQuery<SanctionDisbursementVM>("GL_SanctionDisburse_KYC_Details_RTR @PreSanctionId,@FYID,@BranchId",
                new SqlParameter("PreSanctionId", PreSanctionId),
                new SqlParameter("FYID", fyid),
                new SqlParameter("BranchId", branchid)).FirstOrDefault();

            if (model.LoanType == "Top Up")
            {
                #region Top Up
                int neworold = 0;
                int RowRoiID = 1;
                DateTime InterestFromDate;
                DateTime InterestToDate;
                decimal? RvcCLI = 0;
                DateTime AdvInterestFromDate;
                DateTime AdvInterestToDate;
                model.FinancialYearId = fyid;
                model.BranchId = branchid;
                model.TransactionDate = DateTime.Now.ToShortDateString();
                topupmodel = OutStanding(model);
                var OldGoldLoanNo = _context.TGLSanctionDisburse_BasicDetails.Where(x => x.KYCID == model.KYCID).OrderByDescending(x => x.SDID).Select(x => x.GoldLoanNo).FirstOrDefault();
                neworold = _context.TGlReceipt_BasicDetails.Where(x => x.GoldLoanNo == OldGoldLoanNo).Count();
                var RcptID = _context.TGlReceipt_BasicDetails.Where(x => x.GoldLoanNo == OldGoldLoanNo && x.isActive == "Y").Any() ? _context.TGlReceipt_BasicDetails.Where(x => x.GoldLoanNo == OldGoldLoanNo && x.isActive == "Y").Max(x => x.RcptId) : 0;
                if (topupmodel.OSIntAmt == 0 && topupmodel.AdvInterestAmount == 0)
                {
                    RowRoiID = _context.TSchemeMaster_EffectiveROI.Where(x => x.SID == topupmodel.SID).Select(x => x.ROIID).FirstOrDefault();
                }
                else if (topupmodel.OSIntAmt == 0 && topupmodel.AdvInterestAmount > 0)
                {
                    RowRoiID = _context.TSchemeMaster_EffectiveROI.Where(x => x.SID == topupmodel.SID).Select(x => x.ROIID).FirstOrDefault();
                }
                else if (topupmodel.OSIntAmt > 0 && topupmodel.AdvInterestAmount == 0)
                {
                    RowRoiID = _context.TGLInterest_Details.Where(x => x.ReceiptID == RcptID).Select(x => (int)x.ROIRowID).FirstOrDefault();
                }
                string lastrec, today, todayDateTime;
                lastrec = topupmodel.LastReceiveDate;
                todayDateTime = DateTime.Parse(lastrec).ToShortDateString();
                today = DateTime.Now.ToShortDateString();
                InterestFromDate = Convert.ToDateTime(DateTime.Today.ToShortDateString());
                InterestToDate = Convert.ToDateTime(DateTime.Today.ToShortDateString());
                AdvInterestFromDate = Convert.ToDateTime(DateTime.Today.ToShortDateString());
                AdvInterestToDate = Convert.ToDateTime(DateTime.Today.ToShortDateString());
                if (topupmodel.InterestFromDate != null && topupmodel.InterestFromDate != "")
                {
                    InterestFromDate = Convert.ToDateTime(topupmodel.InterestFromDate);
                }
                if (topupmodel.InterestToDate != null && topupmodel.InterestToDate != "")
                {
                    InterestToDate = Convert.ToDateTime(topupmodel.InterestToDate);
                }
                if (topupmodel.RecvInterest > 0)
                {
                    RvcCLI = topupmodel.RecvInterest;
                }
                if (topupmodel.AdvInterestFromDate != null && topupmodel.AdvInterestFromDate != "")
                {
                    AdvInterestFromDate = Convert.ToDateTime(topupmodel.AdvInterestFromDate);
                }
                if (topupmodel.AdvInterestToDate != null && topupmodel.AdvInterestToDate != "")
                {
                    AdvInterestToDate = Convert.ToDateTime(topupmodel.AdvInterestToDate);
                }
                decimal? AddPrvInt = Convert.ToDecimal(topupmodel.LoanAmout);
                if (topupmodel.OSIntAmt > 0)
                {
                    AddPrvInt = Convert.ToDecimal(topupmodel.LoanAmout + topupmodel.OSIntAmt);
                }
                interestcalculation = EMIInterestCalculator(topupmodel.CustLoanDate, AddPrvInt, topupmodel.SID, neworold, InterestFromDate, InterestToDate, RvcCLI,
                    topupmodel.OSIntAmt, AdvInterestFromDate, AdvInterestToDate, topupmodel.AdvInterestAmount, Convert.ToDateTime(topupmodel.LastReceiveDate), RowRoiID);
                double totalInt = 0;
                if (interestcalculation != null && interestcalculation.Count > 0)
                {
                    foreach (var item in interestcalculation)
                    {
                        totalInt = totalInt + Convert.ToDouble(item.InterestAmount);
                    }
                }
                model.Principal = topupmodel.LoanAmout;
                model.Interest = Convert.ToDecimal(totalInt);
                model.PenalInterest = topupmodel.CLPI;
                model.Charges = topupmodel.CLC;
                model.Total = topupmodel.LoanAmout + Convert.ToDecimal(totalInt) + topupmodel.BCLI;
                #endregion
            }
            else
            {
                #region Principle
                model.Principal = 0;
                model.Interest = 0;
                model.PenalInterest = 0;
                model.Charges = 0;
                model.Total = 0;
                #endregion
            }
            decimal Amount = Convert.ToDecimal(model.SchemeProcessingCharge);
            if (model.SchemeProcessingType == "Percentage")
            {
                Amount = Convert.ToDecimal(model.SanctionLoanAmount * model.SchemeProcessingCharge) / 100;
                if (Amount > model.SchemeProcessingLimit)
                {
                    Amount = model.SchemeProcessingLimit;
                }
            }

            #region ChargeList
            model.ChargeDetailList.Add(new ChargeSanctionVM()
            {
                ID = 0,
                CDetailsID = 0,
                ChargeName = "Processing Fees",
                Charges = Convert.ToDecimal(model.SchemeProcessingCharge),
                ChargeType = model.SchemeProcessingType,
                Amount = Amount,
                AccountId = 62,
                AccountName = _context.tblaccountmasters.Where(x => x.AccountID == 62).Select(x => x.Name).FirstOrDefault(),
                GstId = 0,
                ChargeId = 0
            });
            #endregion

            #region GST Calculation
            double CGSTTax = 0;
            double SGSTTax = 0;
            int CGSTAccountNo = 0;
            int? SGSTAccountNo = 0;
            int? GstId = 0;
            GetGSTRecord(model.StateID, ref CGSTAccountNo, ref SGSTAccountNo, ref CGSTTax, ref SGSTTax, ref GstId);

            model.ChargeDetailList.Add(new ChargeSanctionVM()
            {
                ID = 0,
                CDetailsID = 0,
                ChargeName = SGSTAccountNo > 0 ? "CGST" : "IGST",
                Charges = Convert.ToDecimal(CGSTTax),
                ChargeType = "Percentage",
                Amount = Amount * Convert.ToDecimal(CGSTTax) / 100,
                AccountId = CGSTAccountNo,
                AccountName = _context.tblaccountmasters.Where(x => x.AccountID == CGSTAccountNo).Select(x => x.Name).FirstOrDefault(),
                GstId = 0
            });

            if (SGSTAccountNo > 0)
            {
                model.ChargeDetailList.Add(new ChargeSanctionVM()
                {
                    ID = 0,
                    CDetailsID = 0,
                    ChargeName = "SGST",
                    Charges = Convert.ToDecimal(SGSTTax),
                    ChargeType = "Percentage",
                    Amount = Amount * Convert.ToDecimal(SGSTTax) / 100,
                    AccountId = SGSTAccountNo,
                    AccountName = _context.tblaccountmasters.Where(x => x.AccountID == SGSTAccountNo).Select(x => x.Name).FirstOrDefault(),
                    GstId = 0
                });
            }
            #endregion

            #region Gold Item Details
            var golddetails = (from a in _context.tbl_OrnamentEvaluationDetails
                               join b in _context.tblItemMasters on a.OrnamentId equals b.ItemID
                               join c in _context.Mst_PurityMaster on a.PurityId equals c.id
                               where a.KycId == model.KYCID
                               select new EligibleLoanAmountValuationDetailsVM()
                               {
                                   ID = a.Id,
                                   SDID = a.SDID,
                                   OrnamentId = a.OrnamentId,
                                   OrnamentName = b.ItemName,
                                   PurityNo = a.PurityId,
                                   PurityName = c.PurityName,
                                   Qty = a.Qty,
                                   NetWeight = a.NtWt,
                                   GrossWeight = a.GrossWt,
                                   RatePerGram = a.Rate,
                                   Value = a.Total,
                                   Deductions = a.Deduction,
                                   ImageName = a.ImageName
                               }).ToList();

            model.EligibleLoanAmountValuationDetailsVMList = golddetails;
            model.TotalQuantity = golddetails.Sum(x => x.Qty);
            model.TotalRatePerGram = golddetails.Sum(x => x.RatePerGram);
            model.TotalGrossWeight = golddetails.Sum(x => x.GrossWeight);
            model.TotalNetWeight = golddetails.Sum(x => x.NetWeight);
            model.TotalDeductions = golddetails.Sum(x => x.Deductions);
            model.TotalValue = golddetails.Sum(x => x.Value);
            model.DiscountAmount = Convert.ToDecimal(model.ChargeDetailList.Sum(x => x.Amount));
            model.CGSTTax = model.ChargeDetailList[1].Charges;
            model.CGSTAmount = model.ChargeDetailList[1].Amount;
            model.CGSTAccountId = model.ChargeDetailList[1].AccountId;
            model.CGSTAccountName = model.ChargeDetailList[1].AccountName;
            if (SGSTAccountNo > 0)
            {
                model.SGSTTax = model.ChargeDetailList[2].Charges;
                model.SGSTAmount = model.ChargeDetailList[2].Amount;
                model.SGSTAccountId = model.ChargeDetailList[2].AccountId;
                model.SGSTAccountName = model.ChargeDetailList[2].AccountName;
            }
            #endregion

            return model;
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

            var tablelist = _context.Database.SqlQuery<SanctionDisbursementVM>("EXEC GL_SanctionDisburse_RTR @FYID=@FYID,@BranchId=@BranchId", parameters.ToArray()).ToList();
            foreach (var item in tablelist)
            {
                var model = new SanctionDisbursementVM();
                model.ID = item.ID;
                model.KYCID = item.KYCID;
                model.CustomerID = item.CustomerID;
                model.LoanType = item.LoanType;
                model.LoanAccountNo = item.LoanAccountNo;
                model.TransactionDate = item.TransactionDate;
                model.CustomerName = item.CustomerName;
                model.PANNo = item.PANNo;
                model.MobileNo = item.MobileNo;
                list.Add(model);
            }
            return list;
        }
        #endregion        

        #region OutStanding
        public SanctionTopUpVM OutStanding(SanctionDisbursementVM model)
        {
            //var result = _context.GLReceipt_GoldLoanDetails_RTR_New(Convert.ToDateTime(model.TransactionDate), model.KYCID, model.FinancialYearId, model.BranchId);
            var SanctionTopUpVM = _context.Database.SqlQuery<SanctionTopUpVM>("GLReceipt_GoldLoanDetails_RTR_New @TransactionDate,@KYCID,@FinancialYearId,@BranchId",
                new SqlParameter("TransactionDate", model.TransactionDate),
                new SqlParameter("KYCID", model.KYCID),
                new SqlParameter("FinancialYearId", model.FinancialYearId),
                new SqlParameter("BranchId", model.BranchId)).
                FirstOrDefault();
            return SanctionTopUpVM;
        }
        #endregion

        #region EMIInterestCalculator
        public List<SanctionEMICalculatorVM> EMIInterestCalculator(DateTime LoanDate, decimal? LoanAmount, int SID, int NeworOld, DateTime FromDate, DateTime ToDate,
            decimal? PaidInt, decimal OSIntAmt, DateTime AdvInterestFromDate, DateTime AdvInterestToDate, decimal AdvInterestAmt, DateTime CalculateFromDate, int LastROIID)
        {
            DateTime CalculateToDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            //var result = _context.GL_EmiCalculator_RTR(LoanDate, LoanAmount, SID, NeworOld, FromDate, ToDate, PaidInt, FromDate,
            //    ToDate, OSIntAmt, AdvInterestFromDate, AdvInterestToDate, AdvInterestAmt, CalculateFromDate, CalculateToDate, LastROIID);
            var sanctionEMICalculatorVM = _context.Database.SqlQuery<SanctionEMICalculatorVM>("GL_EmiCalculator_RTR @LoanDate,@LoanAmount,@SID,@NeworOld,@FromDate,@ToDate," +
                "@PaidInt,@OSInterestFromDate,@OSInterestToDate,@OSIntAmt,@AdvInterestFromDate,@AdvInterestToDate,@AdvInterestAmt,@CalculateFromDate,@CalculateToDate,@LastROIID",
                new SqlParameter("LoanDate", LoanDate), new SqlParameter("LoanAmount", LoanAmount), new SqlParameter("SID", SID), new SqlParameter("NeworOld", NeworOld),
                new SqlParameter("FromDate", FromDate), new SqlParameter("ToDate", ToDate), new SqlParameter("PaidInt", PaidInt), new SqlParameter("OSInterestFromDate", FromDate),
                new SqlParameter("OSInterestToDate", ToDate), new SqlParameter("OSIntAmt", OSIntAmt), new SqlParameter("AdvInterestFromDate", AdvInterestFromDate),
                new SqlParameter("AdvInterestToDate", AdvInterestToDate), new SqlParameter("AdvInterestAmt", AdvInterestAmt), new SqlParameter("CalculateFromDate", CalculateFromDate),
                new SqlParameter("CalculateToDate", CalculateToDate), new SqlParameter("LastROIID", LastROIID)).ToList();
            return sanctionEMICalculatorVM;
        }
        #endregion

        #region GetSanctionDisbursementListById
        public SanctionDisbursementVM GetSanctionDisbursementListById(int SId)
        {
            var model = new SanctionDisbursementVM();
            model = _context.Database.SqlQuery<SanctionDisbursementVM>("GL_SanctionDisburseDetails_RTR @SDID", new SqlParameter("SDID", SId)).FirstOrDefault();

            var chargedetails = (from a in _context.TGLSanctionDisburse_ChargesDetails
                                 join b in _context.tbl_GLChargeMaster_BasicInfo on a.ChargeID equals b.CID into aa
                                 from b in aa.DefaultIfEmpty()
                                 join c in _context.tbl_GLChargeMaster_Details on a.ChargeDetailsID equals c.ID into bb
                                 from c in bb.DefaultIfEmpty()
                                 join d in _context.tblaccountmasters on a.AccountID equals d.AccountID
                                 //join e in _context.Mst_GstMaster on a.Gstid equals e.Gst_RefId into cc
                                 //from e in cc.DefaultIfEmpty()
                                 where a.SDID == SId
                                 select new ChargeSanctionVM()
                                 {
                                     ID = a.CHID,
                                     SantionId = a.SDID,
                                     CDetailsID = a.ChargeDetailsID,
                                     ChargeId = a.ChargeID,
                                     Charges = (decimal)a.Charges,
                                     Amount = (decimal)a.Amount,
                                     AccountId = a.AccountID,
                                     ChargeName = a.ChargeID == 0 ? "GST" : b.ChargeName,
                                     ChargeType = c.ChargeType == null ? "Percentage" : c.ChargeType,
                                     AccountName = d.Name,
                                     GstId = a.Gstid
                                 }).ToList();

            if (chargedetails != null && chargedetails.Count > 0)
            {
                model.ChargeDetailList = chargedetails;
                model.GSTId = model.ChargeDetailList[1].GstId;
            }

            model.ChargeDetailList.Add(new ChargeSanctionVM()
            {
                ID = 0,
                ChargeId = 0,
                CDetailsID = 0,
                ChargeName = "Processing Fees",
                Charges = model.SchemeProcessingCharge,
                ChargeType = model.SchemeProcessingType,
                Amount = model.SchemeProcessingCharge,
                AccountId = model.ProcessingFeeAccountId,
                AccountName = "Processing Fees"
            });

            #region GST Calculation           

            model.ChargeDetailList.Add(new ChargeSanctionVM()
            {
                ID = 0,
                ChargeId = 0,
                CDetailsID = 0,
                ChargeName = model.SGSTAccountId > 0 ? "CGST" : "IGST",
                Charges = model.CGSTTax,
                ChargeType = "Percentage",
                Amount = model.CGSTAmount,
                AccountId = model.CGSTAccountId,
                AccountName = _context.tblaccountmasters.Where(x => x.AccountID == model.CGSTAccountId).Select(x => x.Name).FirstOrDefault(),
            });
            if (model.SGSTAccountId > 0)
            {
                model.ChargeDetailList.Add(new ChargeSanctionVM()
                {
                    ID = 0,
                    ChargeId = 0,
                    CDetailsID = 0,
                    ChargeName = "SGST",
                    Charges = Convert.ToDecimal(model.SGSTTax),
                    ChargeType = "Percentage",
                    Amount = Convert.ToDecimal(model.SGSTAmount),
                    AccountId = model.SGSTAccountId,
                    AccountName = _context.tblaccountmasters.Where(x => x.AccountID == model.SGSTAccountId).Select(x => x.Name).FirstOrDefault(),
                });
            }
            #endregion

            var golddetails = (from a in _context.tbl_OrnamentEvaluationDetails
                               join b in _context.tblItemMasters on a.OrnamentId equals b.ItemID
                               join c in _context.Mst_PurityMaster on a.PurityId equals c.id
                               where a.KycId == model.KYCID
                               select new EligibleLoanAmountValuationDetailsVM()
                               {
                                   ID = a.Id,
                                   SDID = a.SDID,
                                   OrnamentId = a.OrnamentId,
                                   OrnamentName = b.ItemName,
                                   PurityNo = a.PurityId,
                                   PurityName = c.PurityName,
                                   Qty = a.Qty,
                                   NetWeight = a.NtWt,
                                   GrossWeight = a.GrossWt,
                                   RatePerGram = a.Rate,
                                   Value = a.Total,
                                   Deductions = a.Deduction,
                                   ImageName = a.ImageName
                               }).ToList();

            model.EligibleLoanAmountValuationDetailsVMList = golddetails;
            //if (chargedetails != null && chargedetails.Count > 0)
            //{
            //    model.ChargeDetailList = chargedetails;
            //}
            model.TotalQuantity = golddetails.Sum(x => x.Qty);
            model.TotalRatePerGram = golddetails.Sum(x => x.RatePerGram);
            model.TotalGrossWeight = golddetails.Sum(x => x.GrossWeight);
            model.TotalNetWeight = golddetails.Sum(x => x.NetWeight);
            model.TotalDeductions = golddetails.Sum(x => x.Deductions);
            model.TotalValue = golddetails.Sum(x => x.Value);
            model.DiscountAmount = Convert.ToDecimal(model.ChargeDetailList.Sum(x => x.Amount));
            model.CGSTAccountName = _context.tblaccountmasters.Where(x => x.AccountID == model.SGSTAccountId).Select(x => x.Name).FirstOrDefault();
            if (model.SGSTAccountId > 0)
            {
                model.SGSTAccountName = _context.tblaccountmasters.Where(x => x.AccountID == model.SGSTAccountId).Select(x => x.Name).FirstOrDefault();
            }
            return model;
        }
        #endregion

        #region GetGSTRecord
        public void GetGSTRecord(int pStateId, ref int CGSTAccountNo, ref int? SGSTAccountNo, ref double CGSTTax, ref double SGSTTax, ref int? GstId)
        {
            var datetime = DateTime.UtcNow;
            var stateid = (from a in _context.tblCompanyBranchMasters
                           join b in _context.Mst_PinCode on a.Pincode equals b.Pc_Id
                           join c in _context.tblCityMasters on b.Pc_CityId equals c.CityID
                           select new
                           {
                               c.StateID
                           }).FirstOrDefault();
            if (stateid != null)
            {
                if (stateid.StateID == pStateId)
                {
                    var getgst = _context.Mst_GstMaster.
                                 Where(x => x.Gst_CGST > 0 && x.Gst_SGST > 0 &&
                                 x.Gst_EffectiveFrom <= datetime.Date).
                                 OrderByDescending(x => x.Gst_EffectiveFrom).
                                 Take(1).FirstOrDefault();
                    if (getgst != null)
                    {
                        CGSTAccountNo = getgst.Gst_CgstAccountId;
                        SGSTAccountNo = getgst.Gst_SgstAccountId;
                        CGSTTax = Convert.ToDouble(getgst.Gst_CGST);
                        SGSTTax = Convert.ToDouble(getgst.Gst_SGST);
                        GstId = getgst.Gst_RefId;
                    }
                }
                else
                {
                    var getgst = _context.Mst_GstMaster.
                                  Where(x => x.Gst_IGST > 0 &&
                                  x.Gst_EffectiveFrom <= datetime.Date).
                                  OrderByDescending(x => x.Gst_EffectiveFrom).
                                  Take(1).FirstOrDefault();
                    if (getgst != null)
                    {
                        CGSTTax = Convert.ToDouble(getgst.Gst_IGST);
                        CGSTAccountNo = getgst.Gst_CgstAccountId;
                        GstId = getgst.Gst_RefId;
                    }
                }
            }
        }
        #endregion

        #region SendMessage
        public string SendMessage(string mobile, decimal pSanctionAmount, string LoanAccountNo)
        {
            //Send message
            string Username = "afpl";
            string APIKey = "afpl2014";
            string Sid = "ApheLN";
            string Message = "Thank you for choosing MCFL. Loan amount for Rs. " + pSanctionAmount + " has been successfully Disbursed to your account Number " + LoanAccountNo + "." +
                " For any queries please do call us on 1800.......... or email us on .............@mangalfincorp.com.";
            string URL = "http://smpp.keepintouch.co.in/vendorsms/pushsms.aspx/?user=" + Username + "&password=" + APIKey + "&msisdn=" + mobile + "&sid=" + Sid + "&msg=" + Message + "" + "&fl=0&gwid=2";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string results = sr.ReadToEnd();
            dynamic result = JsonConvert.DeserializeObject(results);
            sr.Close();
            string response = String.Empty;
            if (result.ErrorMessage == "Success")
            {
                response = "success";
            }
            else
            {
                response = "Please check if the mobile number entered is correct! or contact our customer care.";
            }
            return response;
        }
        #endregion

        #region SendEmail
        public void SendEmail(string EmailId, decimal pSanctionAmount, string LoanAccountNo)
        {
            //Send message
            string Message = "Thank you for choosing MCFL.Loan amount for Rs. " + pSanctionAmount + "  has been successfully disbursed to your loan account Number " + LoanAccountNo + "." +
                            " Please mention your Loan Account Number for all your queries.Call us on 1800..........or email us on.............@mangalfincorp.com." +
                            " For any queries please do call us on 1800.......... or email us on .............@mangalfincorp.com.";
            try
            {
                SmtpClient mailClient = new SmtpClient();
                //SmtpClient mailClient = new SmtpClient();
                //Create the mail message
                System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
                System.Net.Configuration.MailSettingsSectionGroup settings = (System.Net.Configuration.MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");

                mailClient.Credentials = new NetworkCredential(settings.Smtp.Network.UserName, settings.Smtp.Network.Password);
                //mailClient.UseDefaultCredentials = true;
                mailClient.Host = settings.Smtp.Network.Host;
                mailClient.Port = settings.Smtp.Network.Port;
                mailClient.DeliveryMethod = settings.Smtp.DeliveryMethod;
                mailClient.EnableSsl = settings.Smtp.Network.EnableSsl;
                MailMessage mailMessage = new MailMessage(settings.Smtp.Network.UserName, EmailId, "Email Confirmation", Message);
                mailMessage.From = new MailAddress(settings.Smtp.Network.UserName, "Mangal");
                mailMessage.Body = Message;
                mailMessage.IsBodyHtml = true;

                //string[] bccid = bcc.Split(',');

                //foreach (string bccEmailId in bccid)
                //{
                //    MailAddress SendBCC = new MailAddress(bccEmailId);
                //    mailMessage.Bcc.Add(new MailAddress(bccEmailId)); //Adding Multiple BCC email Id
                //}
                Thread threadSendMails;
                threadSendMails = new Thread(delegate ()
                {
                    mailClient.Send(mailMessage);
                });
                threadSendMails.IsBackground = true;
                threadSendMails.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
