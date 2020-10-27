
using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Repository
{
    public class ReasonRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<Mst_Reason> GetAllReasonMasters()
        {
            return _context.Mst_Reason.ToList();
        }

        public Mst_Reason GetReasonById(int id)
        {
            return _context.Mst_Reason.Where(x => x.Re_No == id).FirstOrDefault();
        }

        public void DeleteRecord(int id)
        {
            var deleterecord = _context.Mst_Reason.Where(x => x.Re_No == id).FirstOrDefault();
            if (deleterecord != null)
            {
                _context.Mst_Reason.Remove(deleterecord);
                _context.SaveChanges();
            }
        }

        public void SaveUpdateRecord(ReasonViewModel reason)
        {
            Mst_Reason tblReason = new Mst_Reason();
            if (reason.ID <= 0)
            {
                tblReason.Re_RecordCreated = DateTime.Now;
                tblReason.Re_RecordCreatedBy = reason.CreatedBy;
                _context.Mst_Reason.Add(tblReason);
            }
            else
            {
                tblReason = _context.Mst_Reason.Where(x => x.Re_No == reason.ID).FirstOrDefault();
            }
            tblReason.Re_Desc = reason.ReasonName;
            tblReason.Re_Status = reason.Status;
            tblReason.Re_RecordUpdated = DateTime.Now;
            tblReason.Re_RecordUpdatedBy = reason.UpdatedBy;
            _context.SaveChanges();
        }

        public string CheckReasonNameExists(string Name, int id)
        {
            if (id > 0)
            {
                return _context.Mst_Reason.Where(x => x.Re_Desc == Name && x.Re_No != id).Select(x => x.Re_Desc).FirstOrDefault();
            }
            else
            {
                return _context.Mst_Reason.Where(x => x.Re_Desc == Name).Select(x => x.Re_Desc).FirstOrDefault();
            }
        }


        public ReasonViewModel SetRecordinEdit(Mst_Reason tblReason)
        {
            ReasonViewModel reason = new ReasonViewModel();
            reason.ID = tblReason.Re_No;
            reason.ReasonName = tblReason.Re_Desc;
            reason.Status = (short)tblReason.Re_Status;
            return reason;
        }

        public List<ReasonViewModel> SetDataofModalList()
        {
            List<ReasonViewModel> list = new List<ReasonViewModel>();
            var model = new ReasonViewModel();
            var tablelist = _context.Mst_Reason.ToList();
            foreach (var item in tablelist)
            {
                model = new ReasonViewModel();
                model.ID = item.Re_No;
                model.ReasonName = item.Re_Desc;
                model.StatusStr = item.Re_Status == 1 ? "Active" : "Inactive";
                list.Add(model);
            }
            return list;
        }
    }
}
