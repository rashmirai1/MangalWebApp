using MangalWeb.Model.Entity;
using MangalWeb.Model.Utilities;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class StatdardEndTimeController : BaseController
    {
        // GET: StatdardEndTime
        StandardEndTimeService _standardEndTimeService = new StandardEndTimeService();

        public ActionResult Index()
        {
            ButtonVisiblity("Index");
            var time = _standardEndTimeService.GetTime();

            var data = new StandardEndTime()
            {
                StandadrDateTime = time
            };
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Createtime(StandardEndTime standardEndTime)
        {
            try
            {
                string time = standardEndTime.StandadrDateTime;
                _standardEndTimeService.SaveUpdateRecord(standardEndTime);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Json(standardEndTime);
        }
    }
}