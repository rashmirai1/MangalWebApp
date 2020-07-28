
using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Repository.Repository
{
    public class PincodeRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<Mst_PinCode> GetAllPincodeMasters()
        {
            var list = _context.Mst_PinCode.ToList();
            return list;
        }

        public List<tblCityMaster> GetCityMasterList()
        {
            var list = _context.tblCityMasters.ToList();
            return list;
        }

        public List<tblZonemaster> GetZoneMasterList()
        {
            var list = _context.tblZonemasters.ToList();
            return list;
        }

        public string GetStateName(int id)
        {
            var stateid =_context.tblCityMasters.Where(x => x.CityID ==id).Select(x => x.StateID).FirstOrDefault();
            var statename = _context.tblStateMasters.Where(x => x.StateID == stateid).Select(x => x.StateName).FirstOrDefault();
            return statename;
        }

        public Mst_PinCode GetPincodeById(int id)
        {
            var pincode = _context.Mst_PinCode.Where(x => x.Pc_Id == id).FirstOrDefault();
            return pincode;
        }

        public void DeleteRecord(int id)
        {
            var deleterecord = _context.Mst_PinCode.Where(x => x.Pc_Id == id).FirstOrDefault();
            if (deleterecord != null)
            {
                _context.Mst_PinCode.Remove(deleterecord);
                _context.SaveChanges();
            }
        }

        public void SaveUpdateRecord(PincodeViewModel model)
        {
            Mst_PinCode tblpincode = new Mst_PinCode();
            if (model.ID <= 0)
            {
                tblpincode.Pc_RecordCreated = DateTime.Now;
                tblpincode.Pc_RecordCreatedBy = model.CreatedBy;
                _context.Mst_PinCode.Add(tblpincode); ;
            }
            else
            {
                tblpincode = _context.Mst_PinCode.Where(x => x.Pc_Id == model.ID).FirstOrDefault();
            }
            tblpincode.Pc_Desc = model.Pincode;
            tblpincode.Pc_AreaName = model.AreaName;
            tblpincode.Pc_CityId = model.CityId;
            tblpincode.Pc_ZoneId = model.ZoneId;
            tblpincode.Pc_RecordUpdated = DateTime.Now;
            tblpincode.Pc_RecordUpdatedBy = model.UpdatedBy;
            _context.SaveChanges();
        }

        public string CheckPincodeExists(string Name)
        {
            var pincode = _context.Mst_PinCode.Where(x => x.Pc_AreaName == Name).Select(x => x.Pc_AreaName).FirstOrDefault();
            return pincode;
        }

        public PincodeViewModel SetRecordinEdit(Mst_PinCode tblpincode)
        {
            PincodeViewModel pincode = new PincodeViewModel();
            pincode.ID = tblpincode.Pc_Id;
            pincode.Pincode = tblpincode.Pc_Desc;
            pincode.AreaName = tblpincode.Pc_AreaName;
            pincode.CityId = tblpincode.Pc_CityId;
            pincode.ZoneId = tblpincode.Pc_ZoneId;
            return pincode;
        }
    }
}
