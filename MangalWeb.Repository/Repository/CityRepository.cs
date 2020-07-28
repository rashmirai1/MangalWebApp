using MangalWeb.Model.Entity;
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
    }
}
