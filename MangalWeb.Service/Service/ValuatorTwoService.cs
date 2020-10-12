using MangalWeb.Model.Entity;
using MangalWeb.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Service.Service
{
    public class ValuatorTwoService
    {
        ValuatorTwoRepository _valuatorTwoRepository = new ValuatorTwoRepository();

        public List<Mst_PurityMaster> GetAllPurityMaster()
        {
            return _valuatorTwoRepository.GetPurityMasterList();
        }

        public List<tblItemMaster> GetOrnamentList()
        {
            return _valuatorTwoRepository.GetOrnamentList();
        }
    }
}
