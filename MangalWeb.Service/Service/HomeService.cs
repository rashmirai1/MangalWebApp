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
        private readonly HomeRepository _homeRepository;
        public HomeService(HomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }
        public List<tblBankMaster> GetAlltblBankMasters()
        {
            var list = _homeRepository.GetAlltblBankMasters();
            return list;
        }
    }
}
