using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MangalWeb.Repository.Repository
{
    public class FinancialYearRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<tblFinancialyear> GetFinancialYearMasters()
        {
            return _context.tblFinancialyears.ToList();
        }

        public tblFinancialyear GetFinancialYearById(int id)
        {
            return _context.tblFinancialyears.Where(x => x.FinancialyearID == id).FirstOrDefault();
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

        public int SaveUpdateRecord()
        {
            int Status;
            tblFinancialyear tblfinancial = new tblFinancialyear();
            var financial = new FinancialYearViewModel();
            financial.ID =Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);
            tblfinancial = _context.tblFinancialyears.Where(x => x.FinancialyearID == financial.ID).FirstOrDefault();
            if (tblfinancial != null)
            {
                var localPar = new SqlParameter("@FinancialYearId", financial.ID);
                var Paramater2 = new SqlParameter("@Message", "");
                Paramater2.Direction = ParameterDirection.Output;
                //Paramater2.Size = 50;
                Paramater2.DbType = DbType.Int32;

                var parameters = new List<SqlParameter>();
                parameters.Add(localPar);
                parameters.Add(Paramater2);

                var result = _context.Database.SqlQuery<object>("EXEC generatefinancialyear @FinancialYearId=@FinancialYearId,@Message=@Message OUTPUT", parameters.ToArray());
                var x = result.FirstOrDefault();
                Status =Convert.ToInt32(Paramater2.Value);
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
                Status = 1;
            }
            financial.ID = _context.tblFinancialyears.Max(x => x.FinancialyearID);
            return Status;
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
