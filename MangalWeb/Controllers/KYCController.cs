using MangalWeb.Model.Transaction;
using MangalWeb.Service.Service;
using System;
using System.IO;
using System.Web;
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
            KYCBasicDetailsVM kycVM = new KYCBasicDetailsVM();
            Random rand = new Random(100);
            int cid = rand.Next(000000000, 999999999) + 1;
            kycVM.CustomerID = "C" + cid.ToString();
            kycVM.ApplicationNo = _kycService.GenerateApplicationNo();
            return View(kycVM);
        }

        public JsonResult CreateEdit(KYCBasicDetailsVM model, HttpPostedFileBase uploadFile)
        {
            try
            {
                if (uploadFile != null)
                {
                    Stream fs1 = uploadFile.InputStream;
                    BinaryReader br1 = new BinaryReader(fs1);
                    model.KycPhoto = br1.ReadBytes((Int32)fs1.Length);
                }
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