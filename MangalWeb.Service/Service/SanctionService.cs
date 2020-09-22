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

        public void SanctionDisbursment_PRI(string operation,string value,SanctionDisbursementVM model)
        {
            _sanctionRepository.SanctionDisbursment_PRI(operation,value,model);
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
            return _sanctionRepository.FillChargeList();
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

        public SanctionDisbursementVM GetSanctionDisbursementListById(int Id)
        {
            return _sanctionRepository.GetSanctionDisbursementListById(Id);
        }

        public void SaveUpdateRecord(SanctionDisbursementVM model)
        {
            _sanctionRepository.SaveUpdateRecord(model);
        }
    }
}
