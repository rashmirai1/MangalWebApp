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
    public class ZoneService
    {
        ZoneRepository _zoneRepository = new ZoneRepository();

        public List<tblZonemaster> GetAllZoneMaster()
        {
            var zonelist = _zoneRepository.GetAllZoneMasters();
            return zonelist;
        }

        public tblZonemaster GetZoneById(int id)
        {
            var zone = _zoneRepository.GetZoneMasterById(id);
            return zone;
        }

        public int CheckPincodeExistsByZoneId(int id)
        {
            return _zoneRepository.CheckPincodeExistsByZoneId(id);
        }

        public string CheckZoneNameExists(string name,int id)
        {
            var statename = _zoneRepository.CheckZoneNameExists(name,id);
            return statename;
        }

        public ZoneViewModel SetDataOnEdit(tblZonemaster tblZone)
        {
            var item = _zoneRepository.SetRecordinEdit(tblZone);
            return item;
        }

        public void DeleteZoneRecord(int id)
        {
            _zoneRepository.DeleteRecord(id);
        }

        public void SaveUpdateRecord(ZoneViewModel model)
        {
            _zoneRepository.SaveUpdateRecord(model);
        }
    }
}
