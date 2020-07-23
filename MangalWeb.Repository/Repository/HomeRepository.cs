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
        private readonly MangalDBNewEntities _context;

        public HomeRepository(MangalDBNewEntities context)
        {
            _context = context;
        }
        public List<tblBankMaster> GetAlltblBankMasters()
        {
            var list = _context.tblBankMasters.ToList();
            return list;
        }
    }
}
