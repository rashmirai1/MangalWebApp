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
            var list = _cityRepository.GetAllCityMasters();

            return list;
        }

        public tblCityMaster GetCityById(int id)
        {
            var city = _cityRepository.GetCityMasterById(id);
            return city;
        }

        public string CheckCityNameExists(string name,int id)
        {
            var cityname = _cityRepository.CheckCityNameExists(name,id);
            return cityname;
        }

        public CityViewModel SetDataOnEdit(tblCityMaster tblCity)
        {
            var item = _cityRepository.SetRecordinEdit(tblCity);
            return item;
        }

        public List<tbl_CountryMaster> GetCountryList()
        {
            var list = _cityRepository.GetCountryMasterList();
            return list;
        }

        public List<tblStateMaster> GetStateList()
        {
            var list = _cityRepository.GetStateMasterList();
            return list;
        }

        public string GetCountryName(int id)
        {
            var city = _cityRepository.GetCountryName(id);
            return city;
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
