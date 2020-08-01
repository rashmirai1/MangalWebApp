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
    public class AuditChecklistService
    {
        AuditChecklistRepository _auditRepository = new AuditChecklistRepository();

        public List<Mst_AuditCheckList> GetAllAuditChecklistMasters()
        {
            var list = _auditRepository.GetAllAuditChecklistMasters();
            return list;
        }

        public Mst_AuditCheckList GetAuditCheckListById(int id)
        {
            var audit = _auditRepository.GetAuditCheckListById(id);
            return audit;
        }

        public List<Mst_AuditCategory> GetAuditCategoryList()
        {
            var list = _auditRepository.GetAuditCategoryList();
            return list;
        }

        public AuditCheckListViewModel SetDataOnEdit(Mst_AuditCheckList tblAudit)
        {
            var item = _auditRepository.SetRecordinEdit(tblAudit);
            return item;
        }

        public void DeleteRecord(int id)
        {
            _auditRepository.DeleteRecord(id);
        }

        public List<AuditCheckListViewModel> SetDataofModalList()
        {
            var list = _auditRepository.SetDataofModalList();
            return list;
        }

        public void SaveUpdateRecord(AuditCheckListViewModel model)
        {
            _auditRepository.SaveUpdateRecord(model);
        }
    }
}
