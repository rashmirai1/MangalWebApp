
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
            return _context.Mst_PinCode.ToList();
        }

        public int CheckBranchExistsByPincodeId(int id)
        {
            return _context.tblCompanyBranchMasters.Where(x => x.Pincode == id).Select(x => x.Pincode).Count();
        }

        public List<tblCityMaster> GetCityMasterList()
        {
            return _context.tblCityMasters.ToList();
        }

        public List<tblZonemaster> GetZoneMasterList()
        {
            return _context.tblZonemasters.ToList();
        }

        public string GetStateName(int id)
        {
            var stateid = _context.tblCityMasters.Where(x => x.CityID == id).Select(x => x.StateID).FirstOrDefault();
            var statename = _context.tblStateMasters.Where(x => x.StateID == stateid).Select(x => x.StateName).FirstOrDefault();
            return statename;
        }

        public Mst_PinCode GetPincodeById(int id)
        {
            return _context.Mst_PinCode.Where(x => x.Pc_Id == id).FirstOrDefault();
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

        public string CheckPincodeExists(string pincode, int id)
        {
            if (id > 0)
            {
                return _context.Mst_PinCode.Where(x => x.Pc_Desc == pincode && x.Pc_Id != id).Select(x => x.Pc_AreaName).FirstOrDefault();
            }
            else
            {
                return _context.Mst_PinCode.Where(x => x.Pc_Desc == pincode).Select(x => x.Pc_AreaName).FirstOrDefault();
            }
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
