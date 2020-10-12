using MangalWeb.Model.Transaction;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class ValuatorTwoController : BaseController
    {
        ValuatorTwoService _valuatorTwoService = new ValuatorTwoService();
        // GET: ValuatorOne 
        public ActionResult ValuatorTwo()
        {
            ButtonVisiblity("Index");
            var model = new ValuatorTwoViewModel();
            model.ValuatorTwoDetailsList = new List<ValuatorTwoDetailsViewModel>();
            ViewBag.PurityList = new SelectList(_valuatorTwoService.GetAllPurityMaster(), "Id", "PurityName");
            ViewBag.OrnamentList = new SelectList(_valuatorTwoService.GetOrnamentList(), "ItemId", "ItemName");
            return View(model);
        }
    }
}