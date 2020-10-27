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
            return _context.tblStateMasters.ToList();
        }

        public dynamic GetCKycStateist()
        {
            var ckyclist = _context.tbl_CKycState.Select(x => new
            {
                StateId = x.Id,
                StateName = x.StateCode + " - " + x.StateName + ""
            }).ToList();
            return ckyclist;
        }

        public List<tbl_CountryMaster> GetCountryMasterList()
        {
            return _context.tbl_CountryMaster.ToList();
        }

        public tblStateMaster GetStateMasterById(int id)
        {
            return _context.tblStateMasters.Where(x => x.StateID == id).FirstOrDefault();
        }

        public int CheckCityExistsByStateId(int id)
        {
            return _context.tblCityMasters.Where(x => x.StateID == id).Select(x => x.CityID).FirstOrDefault();
        }

        public void DeleteRecord(int id)
        {
            var deleterecord = _context.tblStateMasters.Where(x => x.StateID == id).FirstOrDefault();
            if (deleterecord != null)
            {
                _context.tblStateMasters.Remove(deleterecord);
                _context.SaveChanges();
            }
        }

        public void SaveUpdateRecord(StateViewModel model)
        {
            tblStateMaster tblstate = new tblStateMaster();
            if (model.ID <= 0)
            {
                model.ID = _context.tblStateMasters.Any() ? _context.tblStateMasters.Max(x => x.StateID) + 1 : 1;
                tblstate.StateID = model.ID;
                _context.tblStateMasters.Add(tblstate);
            }
            else
            {
                tblstate = _context.tblStateMasters.Where(x => x.StateID == model.ID).FirstOrDefault();
            }
            tblstate.StateCode = model.StateCode;
            tblstate.StateName = model.StateName;
            tblstate.countryID = model.CountryId;
            tblstate.CkycStateId = model.CkycStateId;
            _context.SaveChanges();
        }

        public string CheckStateNameExists(string Name,int id)
        {
            if (id > 0)
            {
                return _context.tblStateMasters.Where(x => x.StateName == Name && x.StateID!=id).Select(x => x.StateName).FirstOrDefault();
            }
            else
            {
                return _context.tblStateMasters.Where(x => x.StateName == Name).Select(x => x.StateName).FirstOrDefault();
            }
        }

        public StateViewModel SetRecordinEdit(tblStateMaster tblstate)
        {
            StateViewModel state = new StateViewModel();
            state.ID = tblstate.StateID;
            state.StateCode = tblstate.StateCode;
            state.StateName = tblstate.StateName;
            state.CountryId = tblstate.countryID;
            state.CkycStateId = (int)tblstate.CkycStateId;
            return state;
        }
    }
}
