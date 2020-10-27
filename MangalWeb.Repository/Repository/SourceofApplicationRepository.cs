using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Repository.Repository
{
    public class SourceofApplicationRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<Mst_SourceofApplication> GetAllSourceofApplicationMasters()
        {
            return _context.Mst_SourceofApplication.ToList();
        }

        public Mst_SourceofApplication GetSourceofApplicationById(int id)
        {
            return _context.Mst_SourceofApplication.Where(x => x.Soa_Id == id).FirstOrDefault();
        }

        public int GetMaxSourceId()
        {
            return _context.Mst_SourceofApplication.Any() ? _context.Mst_SourceofApplication.Max(m => m.Soa_Id) + 1 : 1;
        }

        public void DeleteRecord(int id)
        {
            var deleterecord = _context.Mst_SourceofApplication.Where(x => x.Soa_Id == id).FirstOrDefault();
            if (deleterecord != null)
            {
                _context.Mst_SourceofApplication.Remove(deleterecord);
                _context.SaveChanges();
            }
        }

        public void SaveUpdateRecord(SourceofApplicationViewModel source)
        {
            Mst_SourceofApplication tblSource = new Mst_SourceofApplication();
            if (source.EditID <= 0)
            {
                source.ID = _context.Mst_SourceofApplication.Any() ? _context.Mst_SourceofApplication.Max(m => m.Soa_Id) + 1 : 1;
                tblSource.Soa_Id = source.ID;
                tblSource.Soa_RecordCreated = DateTime.Now;
                tblSource.Soa_RecordCreatedBy = source.CreatedBy;
                _context.Mst_SourceofApplication.Add(tblSource);
            }
            else
            {
                tblSource = _context.Mst_SourceofApplication.Where(x => x.Soa_Id == source.ID).FirstOrDefault();
            }
            tblSource.Soa_Name = source.SourceName;
            tblSource.Soa_Category = source.SourceCategory;
            tblSource.Soa_Status = source.SourceStatus;
            tblSource.Soa_RecordUpdated = DateTime.Now;
            tblSource.Soa_RecordUpdatedBy = source.UpdatedBy;
            _context.SaveChanges();
        }

        public string CheckSourceNameExists(string Name, int id)
        {
            if (id > 0)
            {
                return _context.Mst_SourceofApplication.Where(x => x.Soa_Name == Name && x.Soa_Id != id).Select(x => x.Soa_Name).FirstOrDefault();
            }
            else
            {
                return _context.Mst_SourceofApplication.Where(x => x.Soa_Name == Name).Select(x => x.Soa_Name).FirstOrDefault();
            }
        }

        public SourceofApplicationViewModel SetRecordinEdit(Mst_SourceofApplication tblsource)
        {
            SourceofApplicationViewModel source = new SourceofApplicationViewModel();
            source.ID = tblsource.Soa_Id;
            source.EditID = tblsource.Soa_Id;
            source.SourceName = tblsource.Soa_Name;
            source.SourceCategory = tblsource.Soa_Category;
            source.SourceStatus = tblsource.Soa_Status;
            return source;
        }

        public List<SourceofApplicationViewModel> SetDataofModalList()
        {
            var tablelist = _context.Mst_SourceofApplication.ToList();
            List<SourceofApplicationViewModel> list = new List<SourceofApplicationViewModel>();
            foreach (var item in tablelist)
            {
                var model = new SourceofApplicationViewModel();
                model.SourceName = item.Soa_Name;
                model.EditID = item.Soa_Id;
                model.ID = item.Soa_Id;
                model.SourceCategory = item.Soa_Category;
                model.SourceStatus = item.Soa_Status;
                list.Add(model);
            }
            return list;
        }
    }
}
