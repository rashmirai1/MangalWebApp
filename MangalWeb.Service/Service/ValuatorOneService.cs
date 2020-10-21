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
    public class ValuatorOneService
    {
        ValuatorOneRepository _valuatorOneRepository = new ValuatorOneRepository();

        #region GetAllPurityMaster
        public List<Mst_PurityMaster> GetAllPurityMaster()
        {
            return _valuatorOneRepository.GetPurityMasterList();
        }
        #endregion

        #region GetOrnamentList
        public List<tblItemMaster> GetOrnamentList()
        {
            return _valuatorOneRepository.GetOrnamentList();
        }
        #endregion

        #region GetPreSanctionList
        public List<ValuatorOneViewModel> GetPreSanctionList()
        {
            return _valuatorOneRepository.GetPreSanctionList();
        }
        #endregion

        #region GetMaxTransactionId
        public ValuatorOneViewModel GetMaxTransactionId()
        {
            return _valuatorOneRepository.GetMaxTransactionId();
        }
        #endregion

        #region SaveRecord
        public void SaveRecord(ValuatorOneViewModel model)
        {
            _valuatorOneRepository.SaveUpdateRecord(model);
        }
        #endregion

        #region GetValuatorOneList
        public List<ValuatorOneViewModel> GetValuatorOneList()
        {
            return _valuatorOneRepository.GetValuatorOneList();
        }
        #endregion

        #region GetValuatorOneDetailsById
        public ValuatorOneViewModel GetValuatorOneDetailsById(int Id)
        {
            return _valuatorOneRepository.GetValuatorOneDetailsById(Id);
        }
        #endregion

        #region DeleteRecord
        public void DeleteRecord(int id)
        {
            _valuatorOneRepository.DeleteRecord(id);
        }
        #endregion

        #region GetConsolidatedImage
        public Tran_ValuationOneDetails GetConsolidatedImage(int id)
        {
            return _valuatorOneRepository.GetConsolidatedImage(id);
        }
        #endregion

        #region GetValuationImage
        public tbl_OrnamentValuationOneDetails GetValuationImage(int id)
        {
            return _valuatorOneRepository.GetValuationImage(id);
        }
        #endregion

        #region Goldrates
        public double Goldrates()
        {
            return _valuatorOneRepository.Goldrates();
        }
        #endregion

        #region GetOrnamentProductWise
        public List<tblItemMaster> GetOrnamentProductWise(int id)
        {
            return _valuatorOneRepository.GetOrnamentProductWise(id);
        }
        #endregion

        #region GetRateFromProductRate
        public double GetRateFromProductRate(int pProductId,int pPurityId)
        {
            return _valuatorOneRepository.GetRateFromProductRate(pProductId,pPurityId);
        }
        #endregion
    }
}
