using MangalWeb.Model.Masters;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class AuditCheckListController : BaseController
    {
        AuditChecklistService _auditService = new AuditChecklistService();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEdit(AuditCheckListViewModel audit)
        {
            audit.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
            audit.UpdatedBy = Convert.ToInt32(Session["UserLoginId"]);
            try
            {
                _auditService.SaveUpdateRecord(audit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(audit);
        }

        public ActionResult GetAuditCheckListById(int ID)
        {
            string operation = Session["Operation"].ToString();
            ButtonVisiblity(operation);
            var audit = new AuditCheckListViewModel();
            var tblAudit = _auditService.GetAuditCheckListById(ID);
            if (tblAudit != null)
            {
                audit = _auditService.SetDataOnEdit(tblAudit);
            }
            audit.operation = operation;
            ViewBag.AuditCategoryList = new SelectList(_auditService.GetAuditCategoryList(), "Id", "Name");
            return View("AuditCheckList", audit);
        }

        // GETDelete/5
        public ActionResult Delete(int id)
        {
            _auditService.DeleteRecord(id);
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckRecordonEditMode(int Id)
        {
            string data = "";
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AuditCheckList()
        {
            ButtonVisiblity("Index");
            ViewBag.AuditCategoryList = new SelectList(_auditService.GetAuditCategoryList(), "Id", "Name");
            return View();
        }

        public ActionResult GetAuditCheckListTable(string Operation)
        {
            Session["Operation"] = Operation;
            var list = _auditService.SetDataofModalList();
            return PartialView("_AuditCheckList", list);
        }
    }
}