using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Repository.Repository
{
   public class StateRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<tblStateMaster> GetAllStateMasters()
        {
            var list = _context.tblStateMasters.ToList();
            return list;
        }

        public List<tbl_CountryMaster> GetCountryMasterList()
        {
            var country = _context.tbl_CountryMaster.ToList();
            return country;
        }

        public tblStateMaster GetStateMasterById(int id)
        {
            var state = _context.tblStateMasters.Where(x=>x.StateID==id).FirstOrDefault();
            return state;
        }

        public string CheckStateNameExists(string Name)
        {
            var state = _context.tblStateMasters.Where(x => x.StateName == Name).Select(x=>x.StateName).FirstOrDefault();
            return state;
        }

        public StateViewModel SetRecordinEdit(tblStateMaster tblstate)
        {
            StateViewModel state = new StateViewModel();
            state.ID = tblstate.StateID;
            state.StateCode = tblstate.StateCode;
            state.StateName = tblstate.StateName;
            state.CountryId = tblstate.countryID;
            return state;
        }
    }
}
