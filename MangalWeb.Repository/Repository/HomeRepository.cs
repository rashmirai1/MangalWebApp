using MangalWeb.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Repository.Home
{
    public class HomeRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities(); 

        public List<tblBankMaster> GetAlltblBankMasters()
        {
            var list = _context.tblBankMasters.ToList();
            return list;
        }
    }
}
