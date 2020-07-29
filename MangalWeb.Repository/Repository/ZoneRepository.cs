using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using MangalWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Repository.Repository
{
    public class ZoneRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<tblZonemaster> GetAllZoneMasters()
        {
            var list = _context.tblZonemasters.ToList();
            return list;
        }

        public tblZonemaster GetZoneMasterById(int id)
        {
            var zone = _context.tblZonemasters.Where(x => x.ZoneID == id).FirstOrDefault();
            return zone;
        }

        public void DeleteRecord(int id)
        {
            var deleterecord = _context.tblZonemasters.Where(x => x.ZoneID == id).FirstOrDefault();
            if (deleterecord != null)
            {
                _context.tblZonemasters.Remove(deleterecord);
                _context.SaveChanges();
            }
        }

        public void SaveUpdateRecord(ZoneViewModel model)
        {
            tblZonemaster tblZone = new tblZonemaster();
            if (model.ID <= 0)
            {
                model.ID = _context.tblZonemasters.Any() ? _context.tblZonemasters.Max(x => x.ZoneID) + 1 : 1;
                tblZone.ZoneID = model.ID;
                _context.tblZonemasters.Add(tblZone);
            }
            else
            {
                tblZone = _context.tblZonemasters.Where(x => x.ZoneID == model.ID).FirstOrDefault();
            }
            tblZone.Zone = model.ZoneName;
            _context.SaveChanges();
        }

        public string CheckZoneNameExists(string Name)
        {
            var zone = _context.tblZonemasters.Where(x => x.Zone == Name).Select(x => x.Zone).FirstOrDefault();
            return zone;
        }

        public ZoneViewModel SetRecordinEdit(tblZonemaster tblZone)
        {
            ZoneViewModel zone = new ZoneViewModel();
            zone.ID = tblZone.ZoneID;
            zone.ZoneName = tblZone.Zone;
            return zone;
        }
    }
}
