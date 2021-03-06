﻿using MangalWeb.Model.Entity;
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
        public string GetMaxTransactionId()
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

        #region GetValuatorOneData
        public ValuatorTwoDetailsViewModel GetValuatorOneData(int Id)
        {
            return _valuatorTwoRepository.GetValuatorOneData(Id);
        }
        #endregion

        #region GetValuatorTwoDetailsById
        public ValuatorTwoViewModel GetValuatorTwoDetailsById(int Id)
        {
            return _valuatorTwoRepository.GetValuatorTwoDetailsById(Id);
        }
        #endregion

        #region DeleteRecord
        public void DeleteRecord(int id)
        {
            _valuatorTwoRepository.DeleteRecord(id);
        }
        #endregion

        #region GetConsolidatedImage
        public tbl_ValuatorOne GetConsolidatedImage(int id)
        {
            return _valuatorTwoRepository.GetConsolidatedImage(id);
        }
        #endregion

        #region GetValuationImage
        public tbl_ValuatorOneDetails GetValuationImage(int id)
        {
            return _valuatorTwoRepository.GetValuationImage(id);
        }
        #endregion

        #region GetConsolidatedTwoImage
        public tbl_ValuatorTwo GetConsolidatedTwoImage(int id)
        {
            return _valuatorTwoRepository.GetConsolidatedTwoImage(id);
        }
        #endregion

        #region GetValuationTwoImage
        public tbl_ValuatorTwoDetails GetValuationTwoImage(int id)
        {
            return _valuatorTwoRepository.GetValuationTwoImage(id);
        }
        #endregion

        #region CheckRecordExist
        public int CheckRecordExist(int id)
        {
            return _valuatorTwoRepository.CheckRecordExist(id);
        }
        #endregion
    }
}
