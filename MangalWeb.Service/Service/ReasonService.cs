using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using MangalWeb.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Service.Service
{
   public class ReasonService
    {
        ReasonRepository _reasonRepository = new ReasonRepository();

        public List<Mst_Reason> GetAllReasonMaster()
        {
            var list = _reasonRepository.GetAllReasonMasters();
            return list;
        }

        public Mst_Reason GetReasonById(int id)
        {
            var source =_reasonRepository.GetReasonById(id);
            return source;
        }

        public string CheckReasonNameExists(string name,int id)
        {
            var reasonname = _reasonRepository.CheckReasonNameExists(name,id);
            return reasonname;
        }

        public ReasonViewModel SetDataOnEdit(Mst_Reason tblReason)
        {
            var item = _reasonRepository.SetRecordinEdit(tblReason);
            return item;
        }

        public void DeleteRecord(int id)
        {
            _reasonRepository.DeleteRecord(id);
        }

        public void SaveUpdateRecord(ReasonViewModel model)
        {
            _reasonRepository.SaveUpdateRecord(model);
        }

        public List<ReasonViewModel> SetDataofModalList()
        {
            var list = _reasonRepository.SetDataofModalList();
            return list;
        }
    }
}
