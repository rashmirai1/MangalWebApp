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
    public class PincodeService
    {
        PincodeRepository _pincodeRepository = new PincodeRepository();

        public List<Mst_PinCode> GetAllPincodeMaster()
        {
            return _pincodeRepository.GetAllPincodeMasters();
        }

        public int CheckBranchExistsByPincodeId(int id)
        {
            return _pincodeRepository.CheckBranchExistsByPincodeId(id);
        }

        public Mst_PinCode GetPincodeById(int id)
        {
            return _pincodeRepository.GetPincodeById(id);
        }

        public string CheckPinAreaExists(string area,int id)
        {
            return _pincodeRepository.CheckPinAreaExists(area,id);
        }

        public PincodeViewModel SetDataOnEdit(Mst_PinCode tblPincode)
        {
            return _pincodeRepository.SetRecordinEdit(tblPincode);
        }

        public List<tblCityMaster> GetCityMasterList()
        {
            return _pincodeRepository.GetCityMasterList();
        }

        public List<tblZonemaster> GetZoneMasterList()
        {
            var list = _pincodeRepository.GetZoneMasterList();
            return list;
        }

        public string GetStateName(int id)
        {
            var list = _pincodeRepository.GetStateName(id);
            return list;
        }


        public void DeleteRecord(int id)
        {
            _pincodeRepository.DeleteRecord(id);
        }

        public void SaveUpdateRecord(PincodeViewModel model)
        {
            _pincodeRepository.SaveUpdateRecord(model);
        }
    }
}
