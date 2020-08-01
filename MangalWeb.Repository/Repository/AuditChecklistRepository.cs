using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Repository.Repository
{
   public class AuditChecklistRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<Mst_AuditCheckList> GetAllAuditChecklistMasters()
        {
            var list = _context.Mst_AuditCheckList.ToList();
            return list;
        }

        public List<Mst_AuditCategory> GetAuditCategoryList()
        {
            var list = _context.Mst_AuditCategory.ToList();
            return list;
        }

        public Mst_AuditCheckList GetAuditCheckListById(int id)
        {
            var audit = _context.Mst_AuditCheckList.Where(x => x.Acl_Id == id).FirstOrDefault();
            return audit;
        }

        public void DeleteRecord(int id)
        {
            var deleterecord = _context.Mst_AuditCheckList.Where(x => x.Acl_Id == id).FirstOrDefault();
            if (deleterecord != null)
            {
                _context.Mst_AuditCheckList.Remove(deleterecord);
                _context.SaveChanges();
            }
        }

        public void SaveUpdateRecord(AuditCheckListViewModel audit)
        {
            Mst_AuditCheckList tblAudit = new Mst_AuditCheckList();
            if (audit.ID <= 0)
            {
                tblAudit.Acl_RecordCreated = DateTime.Now;
                tblAudit.Acl_RecordCreatedBy = audit.CreatedBy;
                _context.Mst_AuditCheckList.Add(tblAudit);
            }
            else
            {
                tblAudit = _context.Mst_AuditCheckList.Where(x => x.Acl_Id == audit.ID).FirstOrDefault();
            }
            tblAudit.Acl_EffectiveDate = audit.EffectiveDate;
            tblAudit.Acl_Categoryofaudit = audit.CategoryAudit;
            tblAudit.Acl_CheckPoint = audit.AuditCheckPoint;
            tblAudit.Acl_Status = audit.Status;
            tblAudit.Acl_RecordUpdated = DateTime.Now;
            tblAudit.Acl_RecordUpdatedBy = audit.UpdatedBy;
            _context.SaveChanges();
        }

        public string CheckReasonNameExists(string Name)
        {
            var source = _context.Mst_Reason.Where(x => x.Re_Desc == Name).Select(x => x.Re_Desc).FirstOrDefault();
            return source;
        }

        public AuditCheckListViewModel SetRecordinEdit(Mst_AuditCheckList tblAudit)
        {
            var audit = new AuditCheckListViewModel();
            audit.ID = tblAudit.Acl_Id;
            audit.EffectiveDate = tblAudit.Acl_EffectiveDate;
            audit.CategoryAudit = tblAudit.Acl_Categoryofaudit;
            audit.AuditCheckPoint = tblAudit.Acl_CheckPoint;
            audit.Status = tblAudit.Acl_Status;
            return audit;
        }

        public List<AuditCheckListViewModel> SetDataofModalList()
        {
            List<AuditCheckListViewModel> list = new List<AuditCheckListViewModel>();
            var model = new AuditCheckListViewModel();
            var tablelist = _context.Mst_AuditCheckList.ToList();
            foreach (var item in tablelist)
            {
                model = new AuditCheckListViewModel();
                model.ID = item.Acl_Id;
                model.EffectiveDate = item.Acl_EffectiveDate;
                model.CategoryAuditStr = _context.Mst_AuditCategory.Where(x => x.Id == item.Acl_Categoryofaudit).Select(x=>x.Name).FirstOrDefault();
                model.AuditCheckPoint = item.Acl_CheckPoint;
                model.Status = item.Acl_Status;
                list.Add(model);
            }
            return list;
        }
    }
}
