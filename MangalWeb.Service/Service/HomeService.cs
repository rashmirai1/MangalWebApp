using MangalWeb.Model.Entity;
using MangalWeb.Repository.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Service
{

    public class HomeService
    {
        HomeRepository _homeRepository = new HomeRepository();
       
        public List<tblBankMaster> GetAlltblBankMasters()
        {
            var list = _homeRepository.GetAlltblBankMasters();
            return list;
        }
    }
}
