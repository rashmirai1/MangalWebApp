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
    public class OrnamentRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<tblItemMaster> GetAllOrnamentMasters()
        {
            var list = _context.tblItemMasters.ToList();
            return list;
        }

        public tblItemMaster GetOrnamentById(int id)
        {
            var ornament = _context.tblItemMasters.Where(x => x.ItemID == id).FirstOrDefault();
            return ornament;
        }

        public void DeleteRecord(int id)
        {
            var deleterecord = _context.tblItemMasters.Where(x => x.ItemID == id).FirstOrDefault();
            if (deleterecord != null)
            {
                _context.tblItemMasters.Remove(deleterecord);
                _context.SaveChanges();
            }
        }

        public void SaveUpdateRecord(OrnamentViewModel ornament)
        {
            tblItemMaster tblOrnament = new tblItemMaster();
            if (ornament.ID <= 0)
            {
                _context.tblItemMasters.Add(tblOrnament);
            }
            else
            {
                tblOrnament = _context.tblItemMasters.Where(x => x.ItemID == ornament.ID).FirstOrDefault();
            }
            tblOrnament.ItemName = ornament.OrnamentName;
            tblOrnament.Product = ornament.Product;
            tblOrnament.Status = ornament.Status;
            _context.SaveChanges();
        }

        public string CheckOrnamentNameExists(string Name)
        {
            var item = _context.tblItemMasters.Where(x => x.ItemName == Name).Select(x => x.ItemName).FirstOrDefault();
            return item;
        }


        public OrnamentViewModel SetRecordinEdit(tblItemMaster tblOrnament)
        {
            OrnamentViewModel ornament = new OrnamentViewModel();
            ornament.ID = tblOrnament.ItemID;
            ornament.OrnamentName = tblOrnament.ItemName;
            ornament.Product = (short)tblOrnament.Product;
            ornament.Status = (short)tblOrnament.Status;
            return ornament;
        }

        public List<OrnamentViewModel> SetDataofModalList()
        {
            List<OrnamentViewModel> list = new List<OrnamentViewModel>();
            var model = new OrnamentViewModel();
            var tablelist = _context.tblItemMasters.ToList();
            foreach (var item in tablelist)
            {
                model = new OrnamentViewModel();
                model.ID = item.ItemID;
                model.OrnamentName = item.ItemName;
                model.ProductStr = item.Product == 1 ? "Gold Loan" : "Diamond Loan";
                model.StatusStr = GetStatus((short)item.Status);
                list.Add(model);
            }
            return list;
        }

        public string GetStatus(short statusid)
        {
            string statusstr = "";
            switch (statusid)
            {
                case 1:
                    statusstr = "Allowed";
                    break;
                case 2:
                    statusstr = "Not allowed";
                    break;
                case 3:
                    statusstr = "Prohibited";
                    break;
                default:
                    break;
            }
            return statusstr;
        }
    }
}
