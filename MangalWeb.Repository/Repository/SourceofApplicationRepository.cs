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
            var list = _context.Mst_SourceofApplication.ToList();
            return list;
        }

        public Mst_SourceofApplication GetSourceofApplicationById(int id)
        {
            var source = _context.Mst_SourceofApplication.Where(x => x.Soa_Id == id && x.Soa_Status == 1).FirstOrDefault();
            return source;
        }

        public int GetMaxSourceId()
        {
            int id = _context.Mst_SourceofApplication.Any() ? _context.Mst_SourceofApplication.Max(m => m.Soa_Id) + 1 : 1;
            return id;
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

        public string CheckSourceNameExists(string Name)
        {
            var source = _context.Mst_SourceofApplication.Where(x => x.Soa_Name == Name).Select(x => x.Soa_Name).FirstOrDefault();
            return source;
        }


        public SourceofApplicationViewModel SetRecordinEdit(Mst_SourceofApplication tblsource)
        {
            SourceofApplicationViewModel source = new SourceofApplicationViewModel();
            source.ID = tblsource.Soa_Id;
            source.EditID = tblsource.Soa_Id;
            source.SourceName = tblsource.Soa_Name;
            source.SourceCategory = (short)tblsource.Soa_Category;
            source.SourceStatus = (short)tblsource.Soa_Status;
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
                 model.SourceCategirystr = item.Soa_Category == 1 ? "Online" : "Offline";
                 model.SourceStatusstr = item.Soa_Status == 1 ? "Active" : "Inactive";
                 list.Add(model);
            }
            return list;
        }
    }
}
