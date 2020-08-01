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
   public class PenaltySlabService
    {
        PenaltySlabRepository _penaltyslabRepository = new PenaltySlabRepository();

        public List<Mst_PenaltySlab> GetPenaltySlabMasters()
        {
            var list = _penaltyslabRepository.GetPenaltySlabMasters();

            return list;
        }

        public List<tblaccountmaster> GetAccountHeadList()
        {
            var list = _penaltyslabRepository.GetAccountHeadList();
            return list;
        }

        public Mst_PenaltySlab GetPenaltySlabById(int id)
        {
            var city = _penaltyslabRepository.GetPenaltySlabById(id);
            return city;
        }

        public PenaltySlabViewModel SetDataOnEdit(Mst_PenaltySlab tblPenalty)
        {
            var item = _penaltyslabRepository.SetRecordinEdit(tblPenalty);
            return item;
        }

        public int GetMaxPkNo()
        {
            var list = _penaltyslabRepository.GetMaxPkNo();
            return list;
        }

        public void DeleteRecord(int id)
        {
            _penaltyslabRepository.DeleteRecord(id);
        }

        public List<PenaltySlabViewModel> SetDataofModalList()
        {
            var list = _penaltyslabRepository.SetDataofModalList();
            return list;

        }
        public void SaveUpdateRecord(PenaltySlabViewModel model)
        {
            _penaltyslabRepository.SaveUpdateRecord(model);
        }
    }
}
