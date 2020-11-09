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
    public class CityService
    {
        CityRepository _cityRepository = new CityRepository();

        public List<tblCityMaster> GetAllCityMaster()
        {
            return _cityRepository.GetAllCityMasters();
        }

        public tblCityMaster GetCityById(int id)
        {
            return _cityRepository.GetCityMasterById(id);
        }

        public string CheckCityNameExists(string name,int id)
        {
            return _cityRepository.CheckCityNameExists(name,id);
        }

        public CityViewModel SetDataOnEdit(tblCityMaster tblCity)
        {
            return _cityRepository.SetRecordinEdit(tblCity);
        }

        public List<tbl_CountryMaster> GetCountryList()
        {
            return _cityRepository.GetCountryMasterList();
        }

        public List<tblStateMaster> GetStateList()
        {
            return _cityRepository.GetStateMasterList();
        }

        public string GetCountryName(int id)
        {
            return _cityRepository.GetCountryName(id);
        }

        public void DeleteCityRecord(int id)
        {
            _cityRepository.DeleteRecord(id);
        }

        public void SaveUpdateRecord(CityViewModel model)
        {
            _cityRepository.SaveUpdateRecord(model);
        }

        public int CheckPincodeExistsByCityId(int id)
        {
            return _cityRepository.CheckPincodeExistsByCityId(id);
        }
    }
}
