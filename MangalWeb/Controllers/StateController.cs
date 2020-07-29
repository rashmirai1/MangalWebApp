using MangalWeb.Model.Masters;
using MangalWeb.Service.Service;
using MangalWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class StateController : BaseController
    {
        StateService _stateService = new StateService();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateEdit(StateViewModel state)
        {
            try
            {
                if (state.ID <= 0)
                {
                    var data = _stateService.CheckStateNameExists(state.StateName);
                    if (data != null)
                    {
                        ModelState.AddModelError("StateName", "State Name Already Exists");
                        return Json(state);
                    }
                    _stateService.SaveUpdateRecord(state);
                }
                else
                {
                    _stateService.SaveUpdateRecord(state);
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Json(state);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult GetStateById(int ID)
        {
            string operation = Session["Operation"].ToString();
            ButtonVisiblity(operation);
            var tblstate = _stateService.GetStateById(ID);
            var model = new StateViewModel();
            if (tblstate != null)
            {
                model = _stateService.SetDataOnEdit(tblstate);
            }
            model.operation = operation;
            ViewBag.CountryList = new SelectList(_stateService.GetCountryList(), "CountryID", "CountryName");
            return View("State", model);
        }

        // GETDelete/5
        public ActionResult Delete(int id)
        {
            string data = "";
            if (_stateService.CheckCityExistsByStateId(id) > 0)
            {
                data = "Record Cannot Be Deleted Already In Use!";

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                _stateService.DeleteStateRecord(id);
                return Json(JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult doesStateNameExist(string StateName)
        {
            var data = _stateService.CheckStateNameExists(StateName);
            var result = "";
            //Check if state name already exists
            if (data != null)
            {
                if (StateName.ToLower() == data.ToLower().ToString())
                {
                    result = "State Name Already Exists";
                }
                else
                {
                    result = "";
                }
            }
            return Json(result);
        }

        public ActionResult State()
        {
            ButtonVisiblity("Index");
            var model = new StateViewModel();
            ViewBag.CountryList = new SelectList(_stateService.GetCountryList(), "CountryID", "CountryName");
            return View(model);
        }

        public ActionResult GetStateTable(string Operation)
        {
            Session["Operation"] = Operation;
            //ButtonVisiblity(Operation);
            return PartialView("_StateList", _stateService.GetAllStateMaster());
        }
    }
}