using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using MangalWeb.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Service
{
    public class DeviationService
    {
        DeviationRepository _deviationRepository = new DeviationRepository();

        public DeviationViewModel GetAllDeviation()
        {
            var model = _deviationRepository.GetAllDeviation();
            return model;
        }

        public List<tbl_UserCategory> GetUserCategoryList()
        {
            return _deviationRepository.GetUserCategoryList();
        }

        public void DeleteRecord(int id)
        {
            _deviationRepository.DeleteRecord(id);
        }

        public void SaveRecord(DeviationViewModel model)
        {
            _deviationRepository.SaveRecord(model);
        }

        public void UpdateRecord(DeviationViewModel model)
        {
            _deviationRepository.UpdateRecord(model);
        }
    }
}
