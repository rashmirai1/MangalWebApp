using MangalWeb.Model.Masters;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class ReasonController : BaseController
    {
        ReasonService _reasonService = new ReasonService();

        [HttpPost]
        public JsonResult CreateEdit(ReasonViewModel reason)
        {
            reason.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
            reason.UpdatedBy = Convert.ToInt32(Session["UserLoginId"]);
            try
            {
                _reasonService.SaveUpdateRecord(reason);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Json(reason);
        }

        public ActionResult GetReasonById(int ID)
        {
            string operation = Session["Operation"].ToString();
            ButtonVisiblity(operation);
            var tblReason = _reasonService.GetReasonById(ID);
            var model = new ReasonViewModel();
            if (tblReason != null)
            {
                model = _reasonService.SetDataOnEdit(tblReason);
            }
            model.operation = operation;
            return View("Reason", model);
        }

        // GETDelete/5
        public ActionResult Delete(int id)
        {
            _reasonService.DeleteRecord(id);
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult doesReasonExist(string Reason)
        {
            var data = _reasonService.CheckReasonNameExists(Reason);
            var result = "";
            //Check if record already exists
            if (data != null)
            {
                if (Reason.ToLower() == data.ToLower().ToString())
                {
                    result = "Reason Already Exists";
                }
                else
                {
                    result = "";
                }
            }
            return Json(result);
        }

        public ActionResult Reason()
        {
            ButtonVisiblity("Index");
            return View();
        }

        public ActionResult GetReasonTable(string Operation)
        {
            Session["Operation"] = Operation;
            //ButtonVisiblity(Operation);
            var list = _reasonService.SetDataofModalList();
            return PartialView("_ReasonList", list);
        }

    }
}