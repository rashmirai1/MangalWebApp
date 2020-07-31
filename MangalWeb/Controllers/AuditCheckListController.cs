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
        public ActionResult Insert(AuditCheckListViewModel objViewModel)
        {
            ModelState.Remove("Id");
            if (objViewModel.ID == 0)
            {
                if (InsertData(objViewModel))
                {
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(3, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                if (InsertData(objViewModel))
                {
                    return Json(2, JsonRequestBehavior.AllowGet);
                }
            }
            return View("Index", objViewModel);
        }

        #region Insert Data

        public bool InsertData(AuditCheckListViewModel audit)
        {
            bool retVal = false;
            audit.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
            audit.UpdatedBy = Convert.ToInt32(Session["UserLoginId"]);
            try
            {
                _auditService.SaveUpdateRecord(audit);
                retVal = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        #endregion Insert Data

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
            return View("AuditCheckList", audit);
        }

        // GETDelete/5
        public ActionResult Delete(int id)
        {
            _auditService.DeleteRecord(id);
            return Json(JsonRequestBehavior.AllowGet);
        }

        public ActionResult AuditCheckList()
        {
            ButtonVisiblity("Index");
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