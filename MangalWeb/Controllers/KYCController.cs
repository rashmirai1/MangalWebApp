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
            ViewBag.SourceList = new SelectList(_kycService.GetSourceOfApplicationList(), "Soa_Name", "Soa_Name");
            return View();
        }

        public JsonResult CreateEdit(KYCBasicDetailsVM model)
        {
            try
            {
                _kycService.SaveRecord(model);
                return Json(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public JsonResult doesPanExist(string PanNo)
        {
            try
            {
              var model= _kycService.doesPanExist(PanNo);
              return Json(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public JsonResult doesAdharExist(string AdharNo)
        {
            try
            {
                var model = _kycService.doesAdharExist(AdharNo);
                return Json(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}