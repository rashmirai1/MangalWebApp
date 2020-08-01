using MangalWeb.Model.Masters;
using MangalWeb.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Service.Service
{
    public class ChargeService
    {
        ChargeRepository _chargeRepository = new ChargeRepository();

        public ChargeViewModel GetChargeById(int id)
        {
            var charge = _chargeRepository.SetRecordinEdit(id);
            return charge;
        }

        public string CheckChargeNameExists(string name)
        {
            var chargename = _chargeRepository.CheckChargeNameExists(name);
            return chargename;
        }

        public void DeleteRecord(int id)
        {
            _chargeRepository.DeleteRecord(id);
        }

        public void SaveRecord(ChargeViewModel model)
        {
            _chargeRepository.SaveRecord(model);
        }

        public void UpdateRecord(ChargeViewModel model)
        {
            _chargeRepository.UpdateRecord(model);
        }

        public List<ChargeViewModel> SetDataofModalList()
        {
         var list= _chargeRepository.SetDataofModalList();
            return list;
        }
    }
}
