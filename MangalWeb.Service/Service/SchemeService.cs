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
   public class SchemeService
    {
        SchemeRepository _schemeRepository = new SchemeRepository();

        public List<TSchemeMaster_BasicDetails> GetAllSchemeMasters()
        {
            return _schemeRepository.GetAllSchemeMasters();
        }

        public TSchemeMaster_BasicDetails GetSchemeMasterById(int id)
        {
            return _schemeRepository.GetSchemeMasterById(id);
        }

        public string CheckSchemeNameExists(string name,int id)
        {
            return _schemeRepository.CheckSchemeNameExists(name,id);
        }

        public SchemeViewModel SetDataOnEdit(TSchemeMaster_BasicDetails tblScheme)
        {
            return _schemeRepository.SetRecordinEdit(tblScheme);
        }

        public List<SchemeViewModel> SetModalSchemeList()
        {
            return _schemeRepository.SetModalSchemeList();
        }

        public int GetMaxPkNo()
        {
            return _schemeRepository.GetMaxPkNo();
        }

        public List<Mst_PurityMaster> GetPurityMasterList()
        {
            return _schemeRepository.GetPurityMasterList();
        }

        public List<Mst_Product> GetProductList()
        {
            return _schemeRepository.GetProductList();
        }

        public List<Mst_PurityMaster> GetPurityById(int id)
        {
            return _schemeRepository.GetPurityById(id);
        }

        public void DeleteRecord(int id)
        {
            _schemeRepository.DeleteRecord(id);
        }

        public void SaveUpdateRecord(SchemeViewModel model)
        {
            _schemeRepository.SaveUpdateRecord(model);
        }
    }
}
