using MangalWeb.Model.Transaction;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class ValuatorOneController : BaseController
    {
        ValuatorOneService _valuatorOneService = new ValuatorOneService();
        // GET: ValuatorOne 
        public ActionResult ValuatorOne()
        {
            ButtonVisiblity("Index");
            var model = new ValuatorOneViewModel();
            model.ValuatorOneDetailsList = new List<ValuatorOneDetailsViewModel>();
            ViewBag.PurityList = new SelectList(_valuatorOneService.GetAllPurityMaster(), "Id", "PurityName");
            ViewBag.ProductList = new SelectList(_valuatorOneService.GetOrnamentList(), "Id", "ItemName");
            return View(model);
        }
    }
}