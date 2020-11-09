using MangalWeb.Model.Masters;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class CityController : BaseController
    {
        CityService _cityService = new CityService();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateEdit(CityViewModel city)
        {
            try
            {
                    _cityService.SaveUpdateRecord(city);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Json(city);
        }

        public ActionResult GetCityById(int ID)
        {
            string operation = Session["Operation"].ToString();
            ButtonVisiblity(operation);
            var tblcity = _cityService.GetCityById(ID);
            var model = new CityViewModel();
            if (tblcity != null)
            {
                model = _cityService.SetDataOnEdit(tblcity);
            }
            model.operation = operation;
            ViewBag.StateList = new SelectList(_cityService.GetStateList(), "StateID", "StateName");
            var country = _cityService.GetCountryName(model.StateId);
            model.CountryName = country;
            return View("City", model);
        }

        // GETDelete/5
        public ActionResult Delete(int id)
        {
            string data = "";
            if (_cityService.CheckPincodeExistsByCityId(id) > 0)
            {
                data = "Record Cannot Be Deleted Already In Use!";
            }
            else
            {
                _cityService.DeleteCityRecord(id);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckRecordonEditMode(int Id)
        {
            string data = "";
            if (_cityService.CheckPincodeExistsByCityId(Id) > 0)
            {
                data = "Record Cannot Be Edit Already In Use!";
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult doesCityNameExist(string CityName, int Id)
        {
            var result = "";
            var data = _cityService.CheckCityNameExists(CityName, Id);
            //Check if city name already exists
            if (data != null)
            {
                if (CityName.ToLower() == data.ToLower().ToString())
                {
                    result = "City Name Already Exists";
                }
                else
                {
                    result = "";
                }
            }
            return Json(result);
        }

        public ActionResult City()
        {
            ButtonVisiblity("Index");
            var model = new CityViewModel();
            ViewBag.StateList = new SelectList(_cityService.GetStateList(), "StateID", "StateName");
            return View(model);
        }

        public ActionResult GetCityTable(string Operation)
        {
            Session["Operation"] = Operation;
            //ButtonVisiblity(Operation);
            return PartialView("_CityList", _cityService.GetAllCityMaster());
        }

        public JsonResult GetCountry(int id)
        {
            var country = _cityService.GetCountryName(id);
            return Json(country, JsonRequestBehavior.AllowGet);
        }
    }
}