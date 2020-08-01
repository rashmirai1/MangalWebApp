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
        public ActionResult Insert(PenaltySlabViewModel objViewModel)
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

        public bool InsertData(PenaltySlabViewModel penalty)
        {
            bool retVal = false;
            penalty.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
            penalty.UpdatedBy = Convert.ToInt32(Session["UserLoginId"]);
            _penaltySlabService.SaveUpdateRecord(penalty);
            retVal = true;
            return retVal;
        }

        #endregion Insert Data

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