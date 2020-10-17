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
    public class ValuatorTwoService
    {
        ValuatorTwoRepository _valuatorTwoRepository = new ValuatorTwoRepository();

        #region GetAllPurityMaster
        public List<Mst_PurityMaster> GetAllPurityMaster()
        {
            return _valuatorTwoRepository.GetPurityMasterList();
        }
        #endregion

        #region GetOrnamentList
        public List<tblItemMaster> GetOrnamentList()
        {
            return _valuatorTwoRepository.GetOrnamentList();
        }
        #endregion


        #region GetMaxTransactionId
        public ValuatorTwoViewModel GetMaxTransactionId()
        {
            return _valuatorTwoRepository.GetMaxTransactionId();
        }
        #endregion

        #region SaveRecord
        public void SaveRecord(ValuatorTwoViewModel model)
        {
            _valuatorTwoRepository.SaveUpdateRecord(model);
        }
        #endregion

        #region GetValuatorOneList
        public List<ValuatorTwoViewModel> GetValuatorOneList()
        {
            return _valuatorTwoRepository.GetValuatorOneList();
        }
        #endregion

        #region GetValuatorOneList
        public List<ValuatorTwoViewModel> GetValuatorTwoList()
        {
            return _valuatorTwoRepository.GetValuatorTwoList();
        }
        #endregion

        #region GetValuatorOneDetailsById
        public ValuatorTwoViewModel GetValuatorOneDetailsById(int Id)
        {
            return _valuatorTwoRepository.GetValuatorOneDetailsById(Id);
        }
        #endregion

        #region DeleteRecord
        public void DeleteRecord(int id)
        {
            _valuatorTwoRepository.DeleteRecord(id);
        }
        #endregion

        #region GetConsolidatedImage
        public Tran_ValuationOneDetails GetConsolidatedImage(int id)
        {
            return _valuatorTwoRepository.GetConsolidatedImage(id);
        }
        #endregion

        #region GetValuationImage
        public tbl_OrnamentValuationOneDetails GetValuationImage(int id)
        {
            return _valuatorTwoRepository.GetValuationImage(id);
        }
        #endregion
    }
}
