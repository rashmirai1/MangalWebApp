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

        public void SanctionDisbursment_PRI(string operation,SanctionDisbursementVM model)
        {
            _sanctionRepository.SanctionDisbursment_PRI(operation,model);
        }

        public string GetLoanDate()
        {
            return _sanctionRepository.GetLoanDate();
        }

        public string GetLoanNo()
        {
            return _sanctionRepository.GetLoanNo();
        }

        public List<UserDetail> GetCashOutwardList()
        {
            return _sanctionRepository.FillCashOutwardBy();
        }

        public List<UserDetail> GetGoldInwardList()
        {
            return _sanctionRepository.FillGoldInwardBy();
        }

        public List<tblaccountmaster> BankAccountList()
        {
            return _sanctionRepository.FillBankAccount();
        }

        public List<tblaccountmaster> CashAccountList()
        {
            return _sanctionRepository.FillCashAccount();
        }

        public List<tblaccountmaster> ChargeAccountList()
        {
            return _sanctionRepository.FillChargeAccount();
        }

        public List<tbl_GLChargeMaster_BasicInfo> FillChargeList()
        {
            return _sanctionRepository.GetChargeList();
        }

        public int GetMaxTransactionId()
        {
            return _sanctionRepository.GetMaxTransactionId();
        }

        public ChargeSanctionVM GetChargeDetails(int chargeid,decimal sanctionloanamt)
        {
            return _sanctionRepository.GetChargeDetails(chargeid,sanctionloanamt);
        }

        public List<SanctionDisbursementVM> GetSanctionDisbursementList()
        {
            return _sanctionRepository.GetSanctionDisbursementList();
        }

        public List<SanctionDisbursementVM> GetKycDetailsList()
        {
            return _sanctionRepository.GetKycDetailsList();
        }

        public SanctionDisbursementVM GetKYCListById(int Id)
        {
            return _sanctionRepository.GetKycListById(Id);
        }

        public SanctionDisbursementVM GetSanctionDisbursementListById(int Id)
        {
            return _sanctionRepository.GetSanctionDisbursementListById(Id);
        }

        public void SaveUpdateRecord(SanctionDisbursementVM model)
        {
            _sanctionRepository.SaveUpdateRecord(model);
        }

        public void DeleteRecord(int id)
        {
            _sanctionRepository.DeleteRecord(id);
        }

        public void GetGSTRecord(int pStateId,ref int CGSTAccountNo,ref int? SGSTAccountNo,ref double CGSTTax,ref double SGSTTax)
        {
            _sanctionRepository.GetGSTRecord(pStateId,ref CGSTAccountNo,ref SGSTAccountNo,ref CGSTTax,ref SGSTTax);
        }

        public string GetAccountName(int id)
        {
            return _sanctionRepository.GetAccountName(id);
        }
    }
}
