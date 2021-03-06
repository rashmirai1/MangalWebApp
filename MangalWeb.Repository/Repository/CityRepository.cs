﻿using MangalWeb.Model.Entity;
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

        public int CheckPincodeExistsByCityId(int id)
        {
            return _context.Mst_PinCode.Where(x => x.Pc_CityId == id).Select(x => x.Pc_CityId).Count();
        }

        public List<tblCityMaster> GetAllCityMasters()
        {
            return _context.tblCityMasters.ToList();
        }

        public tblCityMaster GetCityMasterById(int id)
        {
            return _context.tblCityMasters.Where(x => x.CityID == id).FirstOrDefault();
        }

        public List<tbl_CountryMaster> GetCountryMasterList()
        {
            return _context.tbl_CountryMaster.ToList();
        }

        public List<tblStateMaster> GetStateMasterList()
        {
            return _context.tblStateMasters.ToList();
        }

        public string GetCountryName(int id)
        {
            var countryname = (from aa in _context.tblStateMasters
                               join bb in _context.tbl_CountryMaster on aa.countryID equals bb.CountryID
                               where aa.StateID==id
                               select new
                               {
                                   country=bb.CountryName
                               }).FirstOrDefault();
            return countryname.country;
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

        public string CheckCityNameExists(string Name, int id)
        {
            if (id > 0)
            {
                return _context.tblCityMasters.Where(x => x.CityName == Name && x.CityID != id).Select(x => x.CityName).FirstOrDefault();
            }
            else
            {
                return _context.tblCityMasters.Where(x => x.CityName == Name).Select(x => x.CityName).FirstOrDefault();
            }
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
