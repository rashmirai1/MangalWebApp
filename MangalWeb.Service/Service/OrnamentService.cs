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
   public class OrnamentService
    {
        OrnamentRepository _ornamentRepository = new OrnamentRepository();

        public List<tblItemMaster> GetAllReasonMaster()
        {
            var list = _ornamentRepository.GetAllOrnamentMasters();
            return list;
        }

        public List<Mst_Product> GetProductList()
        {
            var list = _ornamentRepository.GetProductList();
            return list;
        }

        public tblItemMaster GetOrnamentById(int id)
        {
            var ornament = _ornamentRepository.GetOrnamentById(id);
            return ornament;
        }

        public string CheckOrnamentNameExists(string name)
        {
            var reasonname = _ornamentRepository.CheckOrnamentNameExists(name);
            return reasonname;
        }

        public OrnamentViewModel SetDataOnEdit(tblItemMaster tblOrnament)
        {
            var item = _ornamentRepository.SetRecordinEdit(tblOrnament);
            return item;
        }

        public void DeleteRecord(int id)
        {
            _ornamentRepository.DeleteRecord(id);
        }

        public void SaveUpdateRecord(OrnamentViewModel model)
        {
            _ornamentRepository.SaveUpdateRecord(model);
        }

        public List<OrnamentViewModel> SetDataofModalList()
        {
            var list = _ornamentRepository.SetDataofModalList();
            return list;
        }
    }
}
