using MangalWeb.Model.Masters;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class PenaltySlabController : BaseController
    {
        PenaltySlabService _penaltySlabService = new PenaltySlabService();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateEdit(PenaltySlabViewModel penalty)
        {
            penalty.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
            penalty.UpdatedBy = Convert.ToInt32(Session["UserLoginId"]);
            try
            {
                _penaltySlabService.SaveUpdateRecord(penalty);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Json(penalty);
        }

        public ActionResult GetPenaltySlabById(int ID)
        {
            string operation = Session["Operation"].ToString();
            ButtonVisiblity(operation);
            ViewBag.AccountHeadList = new SelectList(_penaltySlabService.GetAccountHeadList(), "AccountID", "Name");
            var penalty = new PenaltySlabViewModel();
            var tblPenalaty = _penaltySlabService.GetPenaltySlabById(ID);
            if(tblPenalaty!=null)
            {
                penalty = _penaltySlabService.SetDataOnEdit(tblPenalaty);
            }
            return View("PenaltySlab", penalty);
        }

        // GETDelete/5
        public ActionResult Delete(int id)
        {
            _penaltySlabService.DeleteRecord(id);
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckRecordonEditMode(int Id)
        {
            string data = "";
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PenaltySlab()
        {
            ButtonVisiblity("Index");
            var model = new PenaltySlabViewModel();
            model.ID = _penaltySlabService.GetMaxPkNo();
            ViewBag.AccountHeadList = new SelectList(_penaltySlabService.GetAccountHeadList(), "AccountID", "Name");
            return View(model);
        }

        public ActionResult GetPenaltySlabTable(string Operation)
        {
            Session["Operation"] = Operation;
            var list = _penaltySlabService.SetDataofModalList();
            return PartialView("_PenaltySlablist", list);
        }
    }
}