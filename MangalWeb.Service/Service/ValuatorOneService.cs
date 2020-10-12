using MangalWeb.Model.Entity;
using MangalWeb.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Service.Service
{
    public class ValuatorOneService
    {
        ValuatorOneRepository _valuatorOneRepository = new ValuatorOneRepository();

        public List<Mst_PurityMaster> GetAllPurityMaster()
        {
            return _valuatorOneRepository.GetPurityMasterList();
        }

        public List<tblItemMaster> GetOrnamentList()
        {
            return _valuatorOneRepository.GetOrnamentList();
        }
    }
}
