using MangalWeb.Model.Transaction;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Nitesh Tiwari 28.08.2020 4.00 pm
namespace MangalWeb.Controllers
{
    public class SanctionDisbursementController : BaseController
    {
        SanctionService _sanctionService = new SanctionService();

        public void BindList()
        {
            ViewBag.BankAccountList =new SelectList(_sanctionService.BankAccountList(), "AccountID", "Name");
            ViewBag.CashAccountList =new SelectList(_sanctionService.CashAccountList(), "AccountID", "Name");
            ViewBag.CashOutwardList =new SelectList(_sanctionService.GetCashOutwardList(),"UserID", "UserName"); 
            ViewBag.GoldInwardList =new SelectList(_sanctionService.GetGoldInwardList(),"UserID", "UserName"); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateEdit(SanctionDisbursementVM sanction)
        {
            try
            {
                _sanctionService.SaveUpdateRecord(sanction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Json(sanction);
        }
        // GET: SanctionDisbursement
        public ActionResult SanctionDisbursement()
        {
            ButtonVisiblity("Index");
            var model = new SanctionDisbursementVM();
            model.TransactionId = _sanctionService.GetMaxTransactionId();
            model.EmployeeName = Session["UserName"].ToString();
            model.TransactionDate = _sanctionService.GetLoanDate();
            model.LoanAccountNo = _sanctionService.GetLoanNo();
            BindList();
            return View(model);
        }

        public ActionResult GetSanctionTable(string Operation)
        {
            Session["Operation"] = Operation;
            //ButtonVisiblity(Operation);
            return PartialView("_CustomerDetails", _sanctionService.GetSanctionDisbursementList());
        }

        public ActionResult GetSanctionDisbursementDetails(int Id)
        {
            var model = _sanctionService.GetSanctionDisbursementListById(Id);
            model.TransactionId = _sanctionService.GetMaxTransactionId();
            model.EmployeeName = Session["UserName"].ToString();
            model.TransactionDate = _sanctionService.GetLoanDate();
            model.LoanAccountNo = _sanctionService.GetLoanNo();
            BindList();
            //return View("RequestForm", model);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}