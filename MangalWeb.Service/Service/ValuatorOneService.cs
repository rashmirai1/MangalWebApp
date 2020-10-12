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
        public int GetMaxTransactionId()
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
    }
}
