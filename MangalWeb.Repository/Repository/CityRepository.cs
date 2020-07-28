using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Repository.Repository
{
    public class CityRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<tblCityMaster> GetAllCityMasters()
        {
            var list = _context.tblCityMasters.ToList();
            return list;
        }

        public tblCityMaster GetCityMasterById(int id)
        {
            var city = _context.tblCityMasters.Where(x => x.CityID == id).FirstOrDefault();
            return city;
        }

        public List<tbl_CountryMaster> GetCountryMasterList()
        {
            var list = _context.tbl_CountryMaster.ToList();
            return list;
        }

        public List<tblStateMaster> GetStateMasterList()
        {
            var statelist = _context.tblStateMasters.ToList();
            return statelist;
        }

        public string GetCountryName(int id)
        {
            var countryid = _context.tblStateMasters.Where(x => x.StateID == id).Select(x => x.countryID).FirstOrDefault();
            var country = _context.tbl_CountryMaster.Where(x => x.CountryID == countryid).Select(x => x.CountryName).FirstOrDefault();
            return country;
        }

        public void DeleteRecord(int id)
        {
            var deleterecord = _context.tblCityMasters.Where(x => x.CityID == id).FirstOrDefault();
            if (deleterecord != null)
            {
                _context.tblCityMasters.Remove(deleterecord);
                _context.SaveChanges();
            }
        }

        public void SaveUpdateRecord(CityViewModel model)
        {
            tblCityMaster tblCity = new tblCityMaster();
            if (model.ID <= 0)
            {
                model.ID = _context.tblCityMasters.Any() ? _context.tblCityMasters.Max(x => x.CityID) + 1 : 1;
                tblCity.CityID = model.ID;
                _context.tblCityMasters.Add(tblCity);
            }
            else
            {
                tblCity = _context.tblCityMasters.Where(x => x.CityID == model.ID).FirstOrDefault();
            }
            tblCity.CityName = model.CityName;
            tblCity.StateID = model.StateId;
            _context.SaveChanges();
        }

        public string CheckCityNameExists(string Name)
        {
            var state = _context.tblCityMasters.Where(x => x.CityName == Name).Select(x => x.CityName).FirstOrDefault();
            return state;
        }

        public CityViewModel SetRecordinEdit(tblCityMaster tblCity)
        {
            CityViewModel city = new CityViewModel();
            city.ID = tblCity.StateID;
            city.CityName = tblCity.CityName;
            city.StateId = tblCity.StateID;
            return city;
        }

    }
}
