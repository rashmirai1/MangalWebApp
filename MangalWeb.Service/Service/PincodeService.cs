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
            var list = _pincodeRepository.GetAllPincodeMasters();
            return list;
        }

        public Mst_PinCode GetPincodeById(int id)
        {
            var pincode = _pincodeRepository.GetPincodeById(id);
            return pincode;
        }

        public string CheckPinAreaExists(string name)
        {
            var statename = _pincodeRepository.CheckPincodeExists(name);
            return statename;
        }

        public PincodeViewModel SetDataOnEdit(Mst_PinCode tblPincode)
        {
            var item = _pincodeRepository.SetRecordinEdit(tblPincode);
            return item;
        }

        public List<tblCityMaster> GetCityMasterList()
        {
            var list = _pincodeRepository.GetCityMasterList();
            return list;
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
