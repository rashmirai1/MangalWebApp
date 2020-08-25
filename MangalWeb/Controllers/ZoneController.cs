using MangalWeb.Model.Masters;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class ZoneController : BaseController
    {
        ZoneService _zoneService = new ZoneService();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateEdit(ZoneViewModel zone)
        {
            try
            {
                _zoneService.SaveUpdateRecord(zone);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Json(zone);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult GetZoneById(int ID)
        {
            string operation = Session["Operation"].ToString();
            ButtonVisiblity(operation);
            var tblzone = _zoneService.GetZoneById(ID);
            var model = new ZoneViewModel();
            if (tblzone != null)
            {
                model = _zoneService.SetDataOnEdit(tblzone);
            }
            model.operation = operation;
            return View("Zone", model);
        }

        // GETDelete/5
        public ActionResult Delete(int id)
        {
            string data = "";
            if (_zoneService.CheckPincodeExistsByZoneId(id) > 0)
            {
                data = "Record Cannot Be Deleted Already In Use!";
            }
            else
            {
                _zoneService.DeleteZoneRecord(id);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckRecordonEditMode(int Id)
        {
            string data = "";
            if (_zoneService.CheckPincodeExistsByZoneId(Id) > 0)
            {
                data = "Record Cannot Be Edit Already In Use!";
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public JsonResult doesZoneNameExist(string ZoneName,int Id)
        {
            var data = _zoneService.CheckZoneNameExists(ZoneName,Id);
            var result = "";
            //Check if state name already exists
            if (data != null)
            {
                if (ZoneName.ToLower() == data.ToLower().ToString())
                {
                    result = "Zone Name Already Exists";
                }
                else
                {
                    result = "";
                }
            }
            return Json(result);
        }

        public ActionResult Zone()
        {
            ButtonVisiblity("Index");
            var model = new ZoneViewModel();
            return View(model);
        }

        public ActionResult GetZoneTable(string Operation)
        {
            Session["Operation"] = Operation;
            //ButtonVisiblity(Operation);
            return PartialView("_ZoneList", _zoneService.GetAllZoneMaster());
        }
    }
}