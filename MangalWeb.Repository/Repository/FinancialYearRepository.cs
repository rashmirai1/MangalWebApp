using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Repository.Repository
{
    public class FinancialYearRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<tblFinancialyear> GetFinancialYearMasters()
        {
            var list = _context.tblFinancialyears.ToList();
            return list;
        }

        public tblFinancialyear GetFinancialYearById(int id)
        {
            var fyear = _context.tblFinancialyears.Where(x => x.FinancialyearID == id).FirstOrDefault();
            return fyear;
        }

        public void DeleteRecord(int id)
        {
            var deleterecord = _context.tblFinancialyears.Where(x => x.FinancialyearID == id).FirstOrDefault();
            if (deleterecord != null)
            {
                _context.tblFinancialyears.Remove(deleterecord);
                _context.SaveChanges();
            }
        }

        public void SaveUpdateRecord()
        {
            tblFinancialyear tblfinancial = new tblFinancialyear();
            var financial = new FinancialYearViewModel();
            tblfinancial = _context.tblFinancialyears.Where(x => x.FinancialyearID == financial.ID).FirstOrDefault();
            if (tblfinancial != null)
            {
                var financialyear = _context.Database.SqlQuery<FinancialYearViewModel>("generatefinancialyear").ToList();
            }
            else
            {
                tblfinancial = new tblFinancialyear();
                DateTime thisDate = DateTime.Now;
                string currentyear = DateTime.Now.ToString("yyyy");
                DateTime nextyear = thisDate.AddYears(1);
                financial.FinancialYearFrom = "01/04/" + currentyear;
                financial.FinancialYearTo = "31/03/" + nextyear.Year;

                tblfinancial.Financialyearfrom = Convert.ToDateTime(financial.FinancialYearFrom);
                tblfinancial.Financialyearto = Convert.ToDateTime(financial.FinancialYearTo);
                tblfinancial.StartDate = Convert.ToDateTime(financial.FinancialYearFrom);
                tblfinancial.EndDate = Convert.ToDateTime(financial.FinancialYearTo);
                tblfinancial.CompID = 1;
                tblfinancial.Financialyear = "April" + tblfinancial.Financialyearfrom.Year + "-" + "March" + tblfinancial.Financialyearto.Year;
                _context.tblFinancialyears.Add(tblfinancial);
                _context.SaveChanges();
            }
            financial.ID = _context.tblFinancialyears.Max(x => x.FinancialyearID); ;
        }

        public FinancialYearViewModel SetRecordinEdit(tblFinancialyear tblyear)
        {
            FinancialYearViewModel year = new FinancialYearViewModel();
            year.ID = tblyear.FinancialyearID;
            year.FinancialYearFrom = tblyear.Financialyearfrom.ToShortDateString();
            year.FinancialYearTo = tblyear.Financialyearto.ToShortDateString();
            return year;
        }
    }
}
