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
                if (city.ID <= 0)
                {
                    var data = _cityService.CheckCityNameExists(city.CityName);
                    if (data != null)
                    {
                        ModelState.AddModelError("CityName", "City Name Already Exists");
                        return Json(city);
                    }
                    _cityService.SaveUpdateRecord(city);
                }
                else
                {
                    _cityService.SaveUpdateRecord(city);
                }
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
            var country = _cityService.GetCountryName(ID);
            model.CountryName = country;
            return View("City", model);
        }

        // GETDelete/5
        public ActionResult Delete(int id)
        {

            string data = "";
            //if (dd._context.Mst_PinCode.Any(o => o.Pc_CityId == id))
            //{
            //    data = "Record Cannot Be Deleted Already In Use!";

            //    return Json(data, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            _cityService.DeleteCityRecord(id);
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult doesCityNameExist(string CityName)
        {
            var result = "";
            var data = _cityService.CheckCityNameExists(CityName);
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