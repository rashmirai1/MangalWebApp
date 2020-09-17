using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Repository.Repository
{
   public class PenaltySlabRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<Mst_PenaltySlab> GetPenaltySlabMasters()
        {
            return _context.Mst_PenaltySlab.ToList();
        }

        public List<tblaccountmaster> GetAccountHeadList()
        {
            return _context.tblaccountmasters.Where(x=>x.GPID==91).ToList();
        }

        public Mst_PenaltySlab GetPenaltySlabById(int id)
        {
            return _context.Mst_PenaltySlab.Where(x => x.Ps_Id == id).FirstOrDefault();
        }

        public int GetMaxPkNo()
        {
            return _context.Mst_PenaltySlab.Any() ? _context.Mst_PenaltySlab.Max(m => m.Ps_Id) + 1 : 1;
        }

        public void DeleteRecord(int id)
        {
            var deleterecord = _context.Mst_PenaltySlab.Where(x => x.Ps_Id == id).FirstOrDefault();
            if (deleterecord != null)
            {
                _context.Mst_PenaltySlab.Remove(deleterecord);
                _context.SaveChanges();
            }
        }

        public void SaveUpdateRecord(PenaltySlabViewModel penalty)
        {
            Mst_PenaltySlab tblPenaltySlab = new Mst_PenaltySlab();
            try
            {
                if (penalty.EditID <= 0)
                {
                    penalty.ID = _context.Mst_PenaltySlab.Any() ? _context.Mst_PenaltySlab.Max(m => m.Ps_Id) + 1 : 1;
                    tblPenaltySlab.Ps_Id = penalty.ID;
                    tblPenaltySlab.Ps_RecordCreated = DateTime.Now;
                    tblPenaltySlab.Ps_RecordCreatedBy = penalty.CreatedBy;
                    _context.Mst_PenaltySlab.Add(tblPenaltySlab);
                }
                else
                {
                    tblPenaltySlab = _context.Mst_PenaltySlab.Where(x => x.Ps_Id == penalty.ID).FirstOrDefault();
                }
                tblPenaltySlab.Ps_Datewef = Convert.ToDateTime(penalty.Datewef);
                tblPenaltySlab.Ps_Penalty = penalty.PenaltyAmount;
                tblPenaltySlab.Ps_Accounthead = penalty.AccountHead;
                tblPenaltySlab.Ps_RecordUpdated = DateTime.Now;
                tblPenaltySlab.Ps_RecordUpdatedBy = penalty.UpdatedBy;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PenaltySlabViewModel SetRecordinEdit(Mst_PenaltySlab tblPenalty)
        {
            PenaltySlabViewModel penalty = new PenaltySlabViewModel();
            penalty.ID = tblPenalty.Ps_Id;
            penalty.EditID = tblPenalty.Ps_Id;
            penalty.Datewef = tblPenalty.Ps_Datewef.ToString("dd/MM/yyyy");
            penalty.PenaltyAmount = tblPenalty.Ps_Penalty;
            penalty.AccountHead = tblPenalty.Ps_Accounthead;
            return penalty;
        }

        public List<PenaltySlabViewModel> SetDataofModalList()
        {
            List<PenaltySlabViewModel> list = new List<PenaltySlabViewModel>();
            var model = new PenaltySlabViewModel();
            var tablelist = _context.Mst_PenaltySlab.ToList();
            foreach (var item in tablelist)
            {
                model = new PenaltySlabViewModel();
                model.Datewef = item.Ps_Datewef.ToString("dd/MM/yyyy");
                model.EditID = item.Ps_Id;
                model.ID = item.Ps_Id;
                model.PenaltyAmount = item.Ps_Penalty;
                model.AccountHeadStr = _context.tblaccountmasters.Where(x => x.AccountID == item.Ps_Accounthead).Select(x => x.Name).FirstOrDefault();
                list.Add(model);
            }
            return list;
        }
    }
}
