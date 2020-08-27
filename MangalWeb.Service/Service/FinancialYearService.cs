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
    public class FinancialYearService
    {
        FinancialYearRepository _financialYearRepository = new FinancialYearRepository();

        public List<tblFinancialyear> GetFinancialYearMasters()
        {
            var list = _financialYearRepository.GetFinancialYearMasters();
            return list;
        }

        public tblFinancialyear GetFinancialYearById(int id)
        {
            var financialyear = _financialYearRepository.GetFinancialYearById(id);
            return financialyear;
        }

        public FinancialYearViewModel SetDataOnEdit(tblFinancialyear tblFYear)
        {
            var item = _financialYearRepository.SetRecordinEdit(tblFYear);
            return item;
        }

        public void DeleteRecord(int id)
        {
            _financialYearRepository.DeleteRecord(id);
        }

        public int SaveUpdateRecord()
        {
          return _financialYearRepository.SaveUpdateRecord();
        }
    }
}
