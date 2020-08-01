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

        public List<Mst_Product> GetProductList()
        {
            var list = _context.Mst_Product.ToList();
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
            ornament.Product = tblOrnament.Product;
            ornament.Status =tblOrnament.Status;
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
                model.ProductStr = _context.Mst_Product.Where(x => x.Id == item.Product).Select(x => x.Name).FirstOrDefault();
                model.Status = item.Status;
                list.Add(model);
            }
            return list;
        }

    }
}
