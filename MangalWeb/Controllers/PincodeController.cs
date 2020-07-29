using MangalWeb.Model.Masters;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class PincodeController : BaseController
    {
        PincodeService _pincodeService = new PincodeService();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateEdit(PincodeViewModel pincodeViewModel)
        {
            pincodeViewModel.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
            pincodeViewModel.UpdatedBy = Convert.ToInt32(Session["UserLoginId"]);
            try
            {
                if (pincodeViewModel.ID <= 0)
                {
                    var data = _pincodeService.CheckPinAreaExists(pincodeViewModel.AreaName);
                    if (data != null)
                    {
                        ModelState.AddModelError("AreaName", "Area Name Already Exists");
                        return Json(pincodeViewModel);
                    }
                }
                _pincodeService.SaveUpdateRecord(pincodeViewModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Json(pincodeViewModel);
        }

        public ActionResult GetPincodeById(int ID)
        {
            string operation = Session["Operation"].ToString();
            ButtonVisiblity(operation);
            var tblpincode = _pincodeService.GetPincodeById(ID);
            var model = new PincodeViewModel();
            if (tblpincode != null)
            {
                model = _pincodeService.SetDataOnEdit(tblpincode);
            }
            model.operation = operation;
            ViewBag.CityList = new SelectList(_pincodeService.GetCityMasterList(), "CityId", "CityName");
            ViewBag.ZoneList = new SelectList(_pincodeService.GetZoneMasterList(), "ZoneId", "Zone");
            var statename = _pincodeService.GetStateName(model.CityId);
            model.StateName = statename;
            return View("Pincode", model);
        }

        // GETDelete/5
        public ActionResult Delete(int id)
        {
            string data = "";
            _pincodeService.DeleteRecord(id);
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult doesAreaNameExist(string AreaName)
        {
            var data = _pincodeService.CheckPinAreaExists(AreaName);
            var result = "";
            //Check if city name already exists
            if (data != null)
            {
                if (AreaName.ToLower() == data.ToLower().ToString())
                {
                    result = "Area Already Exists";
                }
                else
                {
                    result = "";
                }
            }
            return Json(result);
        }

        public ActionResult Pincode()
        {
            ButtonVisiblity("Index");
            var model = new PincodeViewModel();
            ViewBag.CityList = new SelectList(_pincodeService.GetCityMasterList(), "CityId", "CityName");
            ViewBag.ZoneList = new SelectList(_pincodeService.GetZoneMasterList(), "ZoneId", "Zone");
            return View(model);
        }

        public ActionResult GetPincodeTable(string Operation)
        {
            Session["Operation"] = Operation;
            //ButtonVisiblity(Operation);

            return PartialView("_PincodeList", _pincodeService.GetAllPincodeMaster());
        }

        public JsonResult GetState(int id)
        {
            var statename = _pincodeService.GetStateName(id);
            return Json(statename, JsonRequestBehavior.AllowGet);
        }

    }
}