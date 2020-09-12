using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using MangalWeb.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Service.Service
{
    public class StateService
    {
        StateRepository _stateRepository = new StateRepository();

        public List<tblStateMaster> GetAllStateMaster()
        {
            var statelist = _stateRepository.GetAllStateMasters();

            return statelist;
        }

        public tblStateMaster GetStateById(int id)
        {
            var state = _stateRepository.GetStateMasterById(id);
            return state;
        }

        public string CheckStateNameExists(string name,int id)
        {
            var statename = _stateRepository.CheckStateNameExists(name,id);
            return statename;
        }

        public StateViewModel SetDataOnEdit(tblStateMaster tblstate)
        {
            var item = _stateRepository.SetRecordinEdit(tblstate);
            return item;
        }

        public List<tbl_CountryMaster> GetCountryList()
        {
            var list = _stateRepository.GetCountryMasterList();
            return list;
        }

        public dynamic GetCKycStateist()
        {
            return _stateRepository.GetCKycStateist();
        }

        public int CheckCityExistsByStateId(int id)
        {
            var city = _stateRepository.CheckCityExistsByStateId(id);
            return city;
        }        

        public void DeleteStateRecord(int id)
        {
            _stateRepository.DeleteRecord(id);
        }

        public void SaveUpdateRecord(StateViewModel model)
        {
            _stateRepository.SaveUpdateRecord(model);
        }
    }
}
