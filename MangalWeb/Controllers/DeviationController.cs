using MangalWeb.Model.Masters;
using MangalWeb.Service;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class DeviationController : BaseController
    {
        DeviationService _deviationService = new DeviationService();

        [HttpPost]
        public JsonResult Insert(DeviationViewModel model)
        {
            model.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
            model.UpdatedBy = Convert.ToInt32(Session["UserLoginId"]);
            try
            {
                if (model.ID == 0)
                {
                    if (InsertData(model))
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
                    if (UpdateData(model))
                    {
                        return Json(2, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Json(model);
        }

        public bool InsertData(DeviationViewModel deviationViewModel)
        {
            bool retVal = false;
            _deviationService.SaveRecord(deviationViewModel);
            retVal = true;
            return retVal;
        }

        public bool UpdateData(DeviationViewModel deviationViewModel)
        {
            bool retVal = false;
            _deviationService.UpdateRecord(deviationViewModel);
            retVal = true;
            return retVal;
        }

        public ActionResult Deviation()
        {
            ButtonVisiblity("Edit");
            var model = new DeviationViewModel();
            ViewBag.RoiUserList = new SelectList(_deviationService.GetUserCategoryList(), "refId", "Name");
            ViewBag.DistanceUserList = new SelectList(_deviationService.GetUserCategoryList(), "refId", "Name");
            ViewBag.OrnamentUserList = new SelectList(_deviationService.GetUserCategoryList(), "refId", "Name");
            ViewBag.SanctionedUserList = new SelectList(_deviationService.GetUserCategoryList(), "refId", "Name");
            ViewBag.TenureUserList = new SelectList(_deviationService.GetUserCategoryList(), "refId", "Name");
            ViewBag.LTVUserList = new SelectList(_deviationService.GetUserCategoryList(), "refId", "Name");
            model = _deviationService.GetAllDeviation();
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            _deviationService.DeleteRecord(id);
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}