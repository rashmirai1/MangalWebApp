using MangalWeb.Model.Masters;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class FinancialYearController : BaseController
    {
        FinancialYearService _financialYearService = new FinancialYearService();

        [HttpPost]
        [ValidateAntiForgeryToken]        
        public JsonResult CreateEdit()
        {
            try
            {
                _financialYearService.SaveUpdateRecord();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFinancialYearById(int ID)
        {
            string operation = Session["Operation"].ToString();
            ButtonVisiblity(operation);
            var tblyear = _financialYearService.GetFinancialYearById(ID);
            var year = new FinancialYearViewModel();
            if (tblyear!=null)
            {
                year = _financialYearService.SetDataOnEdit(tblyear);
            }
            year.operation = operation;
            return View("FinancialYear", year);
        }

        // GETDelete/5
        public ActionResult Delete(int id)
        {
            _financialYearService.DeleteRecord(id);
            return Json(JsonRequestBehavior.AllowGet);
        }

        public ActionResult FinancialYear()
        {
            ButtonVisiblity("Index");
            int ID = 1;
            var tblyear = _financialYearService.GetFinancialYearById(ID);
            var model = new FinancialYearViewModel();
            if (tblyear != null)
            {
                model = _financialYearService.SetDataOnEdit(tblyear);
            }
            return View(model);
        }

        public ActionResult GetFinancialYearTable(string Operation)
        {
            Session["Operation"] = Operation;
            //ButtonVisiblity(Operation);
            return PartialView("_FinancialYearList", _financialYearService.GetFinancialYearMasters());
        }

    }
}