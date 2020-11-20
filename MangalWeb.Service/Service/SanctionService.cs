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
   public class SanctionService
    {
        SanctionRepository _sanctionRepository = new SanctionRepository();

        #region SanctionDisbursment_PRI
        public void SanctionDisbursment_PRI(string operation,SanctionDisbursementVM model)
        {
            _sanctionRepository.SanctionDisbursment_PRI(operation,model);
        }
        #endregion

        #region GetLoanNo
        public string GetLoanNo()
        {
            return _sanctionRepository.GetLoanNo();
        }
        #endregion

        #region GetCashOutwardList
        public List<UserDetail> GetCashOutwardList()
        {
            return _sanctionRepository.FillCashOutwardBy();
        }
        #endregion

        #region GetGoldInwardList
        public List<UserDetail> GetGoldInwardList()
        {
            return _sanctionRepository.FillGoldInwardBy();
        }
        #endregion

        #region BankAccountList
        public List<tblaccountmaster> BankAccountList()
        {
            return _sanctionRepository.FillBankAccount();
        }
        #endregion

        #region CashAccountList
        public List<tblaccountmaster> CashAccountList()
        {
            return _sanctionRepository.FillCashAccount();
        }
        #endregion

        #region ChargeAccountList
        public List<tblaccountmaster> ChargeAccountList()
        {
            return _sanctionRepository.FillChargeAccount();
        }
        #endregion

        #region FillChargeList
        public List<tbl_GLChargeMaster_BasicInfo> FillChargeList()
        {
            return _sanctionRepository.GetChargeList();
        }
        #endregion

        #region GetMaxTransactionId
        public int GetMaxTransactionId()
        {
            return _sanctionRepository.GetMaxTransactionId();
        }
        #endregion

        #region GetChargeDetails
        public ChargeSanctionVM GetChargeDetails(int chargeid,decimal sanctionloanamt,string schemeproctype,decimal schemepcharge)
        {
            return _sanctionRepository.GetChargeDetails(chargeid,sanctionloanamt,schemeproctype,schemepcharge);
        }
        #endregion

        #region GetSanctionDisbursementList
        public List<SanctionDisbursementVM> GetSanctionDisbursementList()
        {
            return _sanctionRepository.GetSanctionDisbursementList();
        }
        #endregion

        #region GetKycDetailsList
        public List<SanctionDisbursementVM> GetKycDetailsList()
        {
            return _sanctionRepository.GetKycDetailsList();
        }
        #endregion

        #region GetKYCListById
        public SanctionDisbursementVM GetKYCListById(int Id)
        {
            return _sanctionRepository.GetKycListById(Id);
        }
        #endregion

        #region GetSanctionDisbursementListById
        public SanctionDisbursementVM GetSanctionDisbursementListById(int Id)
        {
            return _sanctionRepository.GetSanctionDisbursementListById(Id);
        }
        #endregion

        #region DeleteRecord
        public void DeleteRecord(int id)
        {
            _sanctionRepository.DeleteRecord(id);
        }
        #endregion

        #region GetGSTRecord
        public void GetGSTRecord(int pStateId,ref int CGSTAccountNo,ref int? SGSTAccountNo,ref double CGSTTax,ref double SGSTTax,ref int? GstId)
        {
            _sanctionRepository.GetGSTRecord(pStateId,ref CGSTAccountNo,ref SGSTAccountNo,ref CGSTTax,ref SGSTTax,ref GstId);
        }
        #endregion

        #region GetAccountName
        public string GetAccountName(int id)
        {
            return _sanctionRepository.GetAccountName(id);
        }
        #endregion

        #region GetImageById
        public TGLSanctionDisburse_BasicDetails GetImageById(int id)
        {
            return _sanctionRepository.GetImageById(id);
        }
        #endregion
    }
}
