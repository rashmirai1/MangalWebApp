using MangalWeb.Model;
using MangalWeb.Model.Entity;
using MangalWeb.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MangalWeb.Repository.Repository
{
    public class SanctionRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        #region GetChargeDetails
        public ChargeSanctionVM GetChargeDetails(int chargeid, decimal sanctionloanamt)
        {
            var model = new ChargeSanctionVM();
            //_context.GL_SanctionDisburse_PRV("Charges", 0, "", 0, 0, chargeid,"", 0, sanctionloanamt);
            var data = _context.GL_SanctionDisburse_Charges_RTR(chargeid, sanctionloanamt).FirstOrDefault();
            if (data != null)
            {
                ModelReflection.MapObjects(data, model);
            }
            return model;
        }
        #endregion

        #region [SanctionDisbursment_PRI]
        public void SanctionDisbursment_PRI(string operation, SanctionDisbursementVM model)
        {
            using (DbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    int value = model.TransactionId;
                    if (operation != "Delete")
                    {
                        #region Save
                        if (operation == "Save")
                        {
                        }                       
                        DateTime? strChqDate = null;
                        DateTime? strBankPaymentDate = null;
                        if (model.ProofOfOwnerShipFile != null)
                        {
                            //Stream fs = model.ProofOfOwnerShipFile.InputStream;
                            //BinaryReader br = new BinaryReader(fs);
                            //Byte[] bytes = br.ReadBytes(model.ProofOfOwnerShipFile.ContentLength);
                            //base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                           // model.ProofOfOwnerShipImageFile = bytes;
                        }
                        else
                        {
                            model.ProofOfOwnerShipImageFile = null;
                        }
                        if (model.PaymentMode == "Chq/DD/NEFT" || model.PaymentMode == "Both")
                        {
                            if (model.ChqDDNEFTDate.Trim() != "" && model.BankPaymentDate.Trim() != "")
                            {
                                strChqDate = Convert.ToDateTime(model.ChqDDNEFTDate);
                                strBankPaymentDate = Convert.ToDateTime(model.BankPaymentDate);
                            }
                            else
                            {
                                strChqDate = null; model.ChqDDNEFTNo = ""; strBankPaymentDate = null;
                            }
                        }
                        DateTime? GoldInwardDate = null;
                        if (model.GoldInwardDate != null)
                        {
                            GoldInwardDate = Convert.ToDateTime(model.GoldInwardDate);
                        }
                        //insert record in sanction disbursement details table
                        var count = _context.SP_SanctionDisburse_PRI(operation, "GOLDITEM", value, model.LoanType, Convert.ToDateTime(model.TransactionDate),
                            model.LoanAccountNo, model.KYCID, model.EligibleLoanAmount, model.SanctionLoanAmount, 0, model.NetPayable, model.ChqDDNEFT, model.ChqDDNEFTNo,
                           strChqDate, model.TotalGrossWeight, model.TotalNetWeight, model.TotalQuantity, model.TotalValue, model.TotalRatePerGram,
                           model.SchemeId, Convert.ToDateTime(model.InterestRepaymentDate), model.ProofOfOwnerShipImageFile,
                            "", 0, model.CashOutwardbyNo, model.GoldInwardByNo, model.CreatedBy, model.FinancialYearId, model.BranchId, model.CompanyId,
                            model.CashAccountNo, model.CashAmount, model.BankAccountNo, model.BankAmount, model.PaymentMode, 0, strBankPaymentDate,model.LockerNo,
                            model.PacketWeight,model.RackNo,model.Remark,GoldInwardDate);

                        //update gold item details
                        var tblgolditem = _context.TGLSanctionDisburse_GoldItemDetails.Where(x => x.KycId == model.KYCID).ToList();
                        foreach (var golditem in tblgolditem)
                        {
                            golditem.SDID = Convert.ToInt32(value);
                            _context.SaveChanges();
                        }
                        //insert record in charge details table
                        foreach(var citem in model.ChargeDetailList)
                        {
                            citem.ID = _context.TGLSanctionDisburse_ChargesDetails.Any() ? _context.TGLSanctionDisburse_ChargesDetails.Max(x => x.CHID) + 1 : 1;
                            count = _context.SP_InsertRecordInSanctionChargeDetails(citem.ID, value, citem.CDetailsID, citem.Charges, citem.Amount, citem.AccountId, citem.ChargeId);
                        }
                        int GPID = 67; //Group ID of Sundry Debtors
                        int LedgerID = 0;
                        int DJERefNo = 0;
                        string DJERefType = "DJEGL";
                        string DJEReferenceNo = string.Empty;
                        //insert record in account master and fsystemgenerated entry master
                        var accountcount = _context.SP_InsertRecordInAccountMaster(model.CustomerName, model.LoanAccountNo, GPID, model.PANNo, model.CustomerAddress,
                            model.AreaId, model.TelephoneNo, model.MobileNo, model.EmailId, model.FinancialYearId);
                        int BCPID = 0;
                        int VoucherNo = 0;
                        int BankCashAccID = 0;
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
                        double Amount = 0, BankAmount = 0, CashAmount = 0;
                        string Narration = "", BankNarration = "";
                        if (model.CashAccountNo > 0)
                        {
                            BankCashAccID = (int)model.CashAccountNo;
                            CashAmount = (double)model.CashAmount;
                        }
                        if (model.BankAccountNo > 0)
                        {
                            BankCashAccID = (int)model.BankAccountNo;
                            BankAmount = (double)model.BankAmount;
                        }
                        Narration = "Payment made against New Gold Loan sanctioned"; //for debit only
                        if (model.LoanType == "New")     //for New Loan
                        {
                            Amount = Convert.ToDouble(model.SanctionLoanAmount);
                        }
                        DJEReferenceNo = DJERefType + "/" + Convert.ToString(DJERefNo);//1st entry
                        //Insert record in Bank Payment details
                        int ContraAccID = 0;
                        //entry in bank cash payment table
                        count = _context.SP_InsertRecordInTBankCashPaymentDetails(BCPID, DJERefType, DJERefNo, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), VoucherNo,
                            BankCashAccID, AccountID, Amount, model.ChqDDNEFTNo, strChqDate, Narration, model.FinancialYearId);
                        //Entry in fledger master
                        //Debit Entry in FLedger(Main Ledger Entry)
                        int AccID = AccountID;
                        if (model.BankAmount > 0)
                        {
                            ContraAccID = Convert.ToInt32(model.BankAccountNo);
                        }
                        else
                        {
                            ContraAccID = Convert.ToInt32(model.CashAccountNo);
                        }
                        double DebitAmount = 0, DebitBankAmount = 0;
                        if (model.LoanType == "New")     //for New Loan
                        {
                            DebitAmount = Convert.ToDouble(model.SanctionLoanAmount); //for entry in closing A/C
                        }
                        //insert record in fledger master of debit entry of sanction loan amount of customer to bank
                        double CreditAmount = 0;
                        bool datasaved = false;
                        LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, DebitAmount, CreditAmount, ContraAccID, BankNarration, model.FinancialYearId);
                        if (LedgerID > 0)
                        {
                            datasaved = CompanyWiseYearEndAccountClosingonSave(Convert.ToInt32(model.FinancialYearId), Convert.ToInt32(model.CompanyId), Convert.ToInt32(model.BranchId), AccID, DebitAmount, CreditAmount);
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
                        if (model.BankAmount > 0)
                        {
                            DJERefNo = _context.FSystemGeneratedEntryMasters.Any() ? _context.FSystemGeneratedEntryMasters.Max(x => x.DJEID) + 1 : 1;
                            //insert record in FSystemGeneratedEntryMaster table
                            count = _context.SP_InsertRecordInFSystemGeneratedEntryMaster(DJERefNo, DJERefType, DJERefNo, DJEReferenceNo, model.LoanAccountNo, model.FinancialYearId);
                            if (model.LoanType == "New")     //for New Loan
                            {
                                if (model.PaymentMode == "Chq/DD/NEFT" || model.PaymentMode == "Both")
                                {
                                    AccID = Convert.ToInt32(model.BankAccountNo);
                                    ContraAccID = AccountID;
                                    if (model.BankAmount != 0)
                                    {
                                        DebitBankAmount = Convert.ToDouble(model.BankAmount);
                                        BankNarration = "Payment made by Bank against New Gold Loan sanctioned";
                                    }
                                }
                            }
                            if (model.PaymentMode == "Chq/DD/NEFT" || model.PaymentMode == "Both")
                            {
                                if (model.ChqDDNEFTDate.Trim() != "")
                                {
                                    strChqDate = Convert.ToDateTime(model.ChqDDNEFTDate.Trim());
                                }
                            }
                            else
                            {
                                strChqDate = null; model.ChqDDNEFTNo = "";
                            }
                            CreditAmount = 0;
                            datasaved = false;
                            LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, CreditAmount, DebitBankAmount, ContraAccID, BankNarration, model.FinancialYearId);
                            if (LedgerID > 0)
                            {
                                datasaved = CompanyWiseYearEndAccountClosingonSave(Convert.ToInt32(model.FinancialYearId), Convert.ToInt32(model.CompanyId), Convert.ToInt32(model.BranchId), AccID, CreditAmount, DebitBankAmount);
                            }
                            //update fsystem generated entry masters of ledger id with djeid
                            FSystemGenerated = _context.FSystemGeneratedEntryMasters.Where(x => x.DJEID == DJERefNo).FirstOrDefault();
                            FSystemGenerated.LedgerID = LedgerID;
                            _context.SaveChanges();
                        }
                        //Ledger Account for cash entry
                        if (model.CashAmount > 0)
                        {
                            DJERefNo = _context.FSystemGeneratedEntryMasters.Any() ? _context.FSystemGeneratedEntryMasters.Max(x => x.DJEID) + 1 : 1;
                            //insert record in FSystemGeneratedEntryMaster table
                            count = _context.SP_InsertRecordInFSystemGeneratedEntryMaster(DJERefNo, DJERefType, DJERefNo, DJEReferenceNo, model.LoanAccountNo, model.FinancialYearId);
                            int CashAccID = Convert.ToInt32(model.CashAccountNo);
                            string CashNarration = "";
                            if (model.LoanType == "New")     //for New Loan
                            {
                                if (model.PaymentMode == "Cash" || model.PaymentMode == "Both")
                                {
                                    CashAmount = Convert.ToDouble(model.CashAmount);
                                    CashNarration = "Payment made by Cash against New Gold Loan sanctioned";
                                }
                            }
                            AccID = CashAccID;
                            ContraAccID = AccountID;
                            CreditAmount = 0;
                            datasaved = false;
                            LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, CreditAmount, CashAmount, ContraAccID, CashNarration, model.FinancialYearId);
                            if (LedgerID > 0)
                            {
                                datasaved = CompanyWiseYearEndAccountClosingonSave(Convert.ToInt32(model.FinancialYearId), Convert.ToInt32(model.CompanyId), Convert.ToInt32(model.BranchId), AccID, CreditAmount, CashAmount);
                            }
                            //update fsystem generated entry masters of ledger id with djeid
                            FSystemGenerated = _context.FSystemGeneratedEntryMasters.Where(x => x.DJEID == DJERefNo).FirstOrDefault();
                            FSystemGenerated.LedgerID = LedgerID;
                            _context.SaveChanges();
                        }
                        //***************************** Accounting Entries for Charges stared ***************************************
                        Narration = "Amount received against Gold Loan charges";
                        if (model.ChargeDetailList != null && model.ChargeDetailList.Count > 0)
                        {
                            foreach (var chargeitem in model.ChargeDetailList)
                            {
                                if (chargeitem.ChargeId != null && chargeitem.ChargeId > 0)
                                {
                                    int CreditID = 0;
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
                                        datasaved = CompanyWiseYearEndAccountClosingonSave(Convert.ToInt32(model.FinancialYearId), Convert.ToInt32(model.CompanyId), Convert.ToInt32(model.BranchId), ContraAccID, DebitAmount, CreditAmount);
                                    }
                                    //update fsystem generated entry masters of ledger id with djeid
                                    FSystemGenerated = _context.FSystemGeneratedEntryMasters.Where(x => x.DJEID == DJERefNo).FirstOrDefault();
                                    FSystemGenerated.LedgerID = LedgerID;
                                    _context.SaveChanges();
                                    //insert record in TGLSanctionDisburse_ChargesPostingDetails
                                    var maxchargepostingid = _context.TGLSanctionDisburse_ChargesPostingDetails.Any() ? _context.TGLSanctionDisburse_ChargesPostingDetails.Max(x => x.ID) + 1 : 1;
                                    //insert record in charge posting details
                                    count = _context.SP_InsertRecordInChargePostingDetails(maxchargepostingid, value, model.LoanAccountNo, ContraAccID, DebitAmount, CreditAmount, LedgerID, model.FinancialYearId);
                                }
                            }
                        }
                        //***************************** Accounting Entries for Charges end ***************************************
                        //***************************** Accounting Entries for Other Charges Start *********************************
                        int GetBranchPinCode = _context.tblCompanyBranchMasters.Where(x => x.BID == model.BranchId).Select(x => x.Pincode).FirstOrDefault();
                        int GetCityId = _context.Mst_PinCode.Where(x => x.Pc_Id == GetBranchPinCode).Select(x => x.Pc_CityId).FirstOrDefault();
                        int GetStateId = _context.tblCityMasters.Where(x => x.CityID == GetCityId).Select(x => x.StateID).FirstOrDefault();
                        //if state same of customer present address state and selected branch state then CGST or SGST ,if not match then IGST
                        double CGSTTax = 0;
                        double SGSTTax = 0;
                        int CGSTAccountNo = 0;
                        int? SGSTAccountNo = 0;
                        if (GetStateId == model.StateID)
                        {
                            var getgst = _context.Mst_GstMaster.Where(x => x.Gst_CGST != null && x.Gst_SGST != null)
                                .OrderByDescending(x => x.Gst_RefId).Take(1).FirstOrDefault();
                            CGSTAccountNo = getgst.Gst_CgstAccountId;
                            SGSTAccountNo = getgst.Gst_SgstAccountId;
                            CGSTTax = Convert.ToDouble(getgst.Gst_CGST);
                            SGSTTax = Convert.ToDouble(getgst.Gst_SGST);
                        }
                        else
                        {
                            var getgst = _context.Mst_GstMaster.Where(x => x.Gst_IGST != "")
                                                           .OrderByDescending(x => x.Gst_RefId).Take(1).FirstOrDefault();
                            CGSTTax = Convert.ToDouble(getgst.Gst_IGST);
                            CGSTAccountNo = getgst.Gst_CgstAccountId;
                        }
                        if (model.LoanType == "New")
                        {
                            Narration = "Amount received against Gold Loan processing charges";
                            AccID = 6828;
                            int ConAccID = AccountID;
                            double DebitAmt = 0;
                            double CreditAmt = 0;
                            ConAccID = AccountID;
                            DebitAmt = 0;
                            CreditAmt = 0;
                            if (model.SchemeProcessingType == "Percentage")
                            {
                                CreditAmt = Convert.ToDouble(model.SanctionLoanAmount) * Convert.ToDouble(model.SchemeProcessingCharge) / 100;
                                if (CreditAmt > Convert.ToDouble(model.SchemeProcessingLimit))
                                {
                                    CreditAmt = Convert.ToDouble(model.SchemeProcessingLimit);
                                }
                                CreditAmt = (CreditAmt + CreditAmt * Convert.ToDouble(CGSTTax) / 100);
                                CreditAmt = (CreditAmt + CreditAmt * Convert.ToDouble(SGSTTax) / 100);
                                CreditAmt = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(CreditAmt), 2));
                            }
                            else
                            {
                                CreditAmt = Convert.ToDouble(model.SchemeProcessingCharge);
                                if (CreditAmt > Convert.ToDouble(model.SchemeProcessingLimit))
                                {
                                    CreditAmt = Convert.ToDouble(model.SchemeProcessingLimit);
                                }
                                CreditAmt = (CreditAmt + CreditAmt * Convert.ToDouble(CGSTTax) / 100);
                                CreditAmt = (CreditAmt + CreditAmt * Convert.ToDouble(SGSTTax) / 100);
                                CreditAmt = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(CreditAmt), 2));
                            }
                            LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, DebitAmt, CreditAmt, ConAccID, Narration, model.FinancialYearId);
                            if (LedgerID > 0)
                            {
                                datasaved = CompanyWiseYearEndAccountClosingonSave(Convert.ToInt32(model.FinancialYearId), Convert.ToInt32(model.CompanyId), Convert.ToInt32(model.BranchId), ConAccID, DebitAmt, CreditAmt);
                            }
                        }
                        //***************************** Accounting Entries for Other Charges End *********************************

                        //***************************** Accounting Entries for GST Charges Start *********************************
                        AccID = CGSTAccountNo;
                        double DebitGstAmt = 0;
                        double CreditGstAmt = 0;
                        ContraAccID = AccountID;
                        DebitGstAmt = 0;
                        CreditGstAmt = 0;
                        CreditGstAmt = CGSTTax;
                        LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, DebitGstAmt, CreditGstAmt, ContraAccID, Narration, model.FinancialYearId);
                        if (LedgerID > 0)
                        {
                            datasaved = CompanyWiseYearEndAccountClosingonSave(Convert.ToInt32(model.FinancialYearId), Convert.ToInt32(model.CompanyId), Convert.ToInt32(model.BranchId), ContraAccID, DebitGstAmt, CreditGstAmt);
                        }
                        AccID =Convert.ToInt32(SGSTAccountNo);
                        DebitGstAmt = 0;
                        CreditGstAmt = 0;
                        ContraAccID = AccountID;
                        DebitGstAmt = 0;
                        CreditGstAmt = 0;
                        CreditGstAmt = SGSTTax;
                        LedgerID = CreateNormalLedgerEntries(DJERefType, DJEReferenceNo, Convert.ToDateTime(model.TransactionDate), AccID, DebitGstAmt, CreditGstAmt, ContraAccID, Narration, model.FinancialYearId);
                        if (LedgerID > 0)
                        {
                            datasaved = CompanyWiseYearEndAccountClosingonSave(Convert.ToInt32(model.FinancialYearId), Convert.ToInt32(model.CompanyId), Convert.ToInt32(model.BranchId), ContraAccID, DebitGstAmt, CreditGstAmt);
                        }
                        //***************************** Accounting Entries for GST Charges End *********************************
                        //credit end
                        _context.SaveChanges();
                        transaction.Commit();
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
        #endregion [SanctionDisbursment_PRI]

        #region [CreateNormalLedgerEntries]
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
            }
            return LedgerID;
        }
        #endregion [CreateNormalLedgerEntries]

        #region [CompanyWiseYearEndAccountClosingonSave]
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
                var FCompanyID = _context.FCompanyYearEndClosings.Where(x => x.FinancialyearID == FYID && x.AccountID == accountID).Select(x => x.ID).FirstOrDefault();
                if (FCompanyID != null && FCompanyID > 0)
                {
                    ID = (int)FCompanyID;
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
                    FCompanyYearEndClosing fCompanyYearEndClosing = new FCompanyYearEndClosing();
                    fCompanyYearEndClosing.ID = ID;
                    fCompanyYearEndClosing.FinancialyearID = FYID;
                    fCompanyYearEndClosing.CompID = compID;
                    fCompanyYearEndClosing.AccountID = accountID;
                    fCompanyYearEndClosing.OpeningBalanceDebit = openingBalanceDebit;
                    fCompanyYearEndClosing.OpeningBalanceCredit = openingBalanceCredit;
                    fCompanyYearEndClosing.CurrentDebit = currentDebit;
                    fCompanyYearEndClosing.CurrentCredit = currentCredit;
                    fCompanyYearEndClosing.ClosingBalanceDebit = closingBalanceDebit;
                    fCompanyYearEndClosing.ClosingBalanceCredit = closingBalanceCredit;
                    _context.FCompanyYearEndClosings.Add(fCompanyYearEndClosing);
                    _context.SaveChanges();
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
                    datasaved = true;
                }
                #endregion [if Account ID exists]
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
        #endregion [CompanyWiseYearEndAccountClosingonSave]

        #region GetLoanDate
        public string GetLoanDate()
        {
            return DateTime.Now.ToShortDateString();
        }
        #endregion

        #region GetMaxTransactionId
        public int GetMaxTransactionId()
        {
            return _context.TGLSanctionDisburse_BasicDetails.Any() ? _context.TGLSanctionDisburse_BasicDetails.Max(x => (int)x.SDID) + 1 : 1;
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
            return _context.tblaccountmasters.Where(x => x.GPID == 71 || x.GPID == 11).OrderBy(x=>x.Name).ToList();
        }
        #endregion

        #region FillCashAccount
        public List<tblaccountmaster> FillCashAccount()
        {
            return _context.tblaccountmasters.Where(x => x.GPID == 70).OrderBy(x=>x.Name).ToList();
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
            //var KycIdPar = new SqlParameter("@KYCID", KycId);
            //var FyIdPar = new SqlParameter("@FYID", fyid);
            //var BranchIdPar = new SqlParameter("@BranchId", branchid);
            //var parameters = new List<SqlParameter>();
            //parameters.Add(KycIdPar);
            //parameters.Add(FyIdPar);
            //parameters.Add(BranchIdPar);
            //model = _context.Database.SqlQuery<SanctionDisbursementVM>("EXEC GL_SanctionDisburse_KYC_Details_RTR @KYCID=@KYCID,@FYID=@FYID,@BranchId=@BranchId", parameters.ToArray()).FirstOrDefault();
            var result = _context.GL_SanctionDisburse_KYC_Details_RTR(KycId, fyid, branchid).FirstOrDefault();
            if (result != null)
            {
                ModelReflection.MapObjects(result, model);
            }
            model.EligibleLoanAmountValuationDetailsVMList = new List<EligibleLoanAmountValuationDetailsVM>();
            var valuationlist = _context.TGLSanctionDisburse_GoldItemDetails.ToList();
            int? TotalQuantity = 0;
            decimal? TotalRatePerGram = 0;
            decimal? TotalGrossWeight = 0;
            decimal? TotalDeductions = 0;
            decimal? TotalNetWeight = 0;
            decimal? TotalNetValue = 0;
            foreach (var item in valuationlist)
            {
                var gold = new EligibleLoanAmountValuationDetailsVM();
                gold.ID = item.GID;
                gold.SDID = item.SDID;
                gold.OrnamentId = item.ItemID;
                gold.OrnamentName = _context.tblItemMasters.Where(x => x.ItemID == item.ItemID).Select(x => x.ItemName).FirstOrDefault();
                gold.PurityNo = item.Purity;
                gold.PurityName = _context.Mst_PurityMaster.Where(x => x.id == item.Purity).Select(x => x.PurityName).FirstOrDefault();
                gold.GrossWeight = item.GrossWeight;
                gold.Qty = item.Quantity;
                gold.NetWeight = item.NetWeight;
                gold.RatePerGram = item.RateperGram;
                gold.Value = item.Value;
                gold.Deductions = item.Deduction;
                TotalQuantity = TotalQuantity + gold.Qty;
                TotalRatePerGram = TotalRatePerGram + gold.RatePerGram;
                TotalGrossWeight = TotalGrossWeight + gold.GrossWeight;
                TotalNetWeight = TotalNetWeight + gold.NetWeight;
                TotalDeductions = TotalDeductions + gold.Deductions;
                TotalNetValue = TotalNetValue + gold.Value;
                model.EligibleLoanAmountValuationDetailsVMList.Add(gold);
            }
            model.TotalQuantity = TotalQuantity;
            model.TotalRatePerGram = TotalRatePerGram;
            model.TotalGrossWeight = TotalGrossWeight;
            model.TotalNetWeight = TotalNetWeight;
            model.TotalDeductions = TotalDeductions;
            model.TotalValue = TotalNetValue;
            return model;
        }
        #endregion
    }
}
