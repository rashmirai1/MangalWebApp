using MangalWeb.Model.Transaction;
using MangalWeb.Service.Service;
using System;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class KYCController : BaseController
    {
        KYCService _kycService = new KYCService();
        // GET: KYC
        public ActionResult Index()
        {
            ButtonVisiblity("Index");
            // var model = new CityViewModel();
            //  ViewBag.StateList = new SelectList(_cityService.GetStateList(), "StateID", "StateName");
            return View();
        }

        public JsonResult CreateEdit(KYCBasicDetailsVM model)
        {
            try
            {
                _kycService.SaveRecord(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Json(model);
        }
    }
}