using MangalWeb.Model.Transaction;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class ValuatorOneController : BaseController
    {
        ValuatorOneService _valuatorOneService = new ValuatorOneService();
        
        #region ValuatorOne
        public ActionResult ValuatorOne()
        {
            ButtonVisiblity("Index");
            var model = new ValuatorOneViewModel();
            model.TransactionId = _valuatorOneService.GetMaxTransactionId();
            model.ValuatorOneDetailsList = new List<ValuatorOneDetailsViewModel>();
            ViewBag.PurityList = new SelectList(_valuatorOneService.GetAllPurityMaster(), "Id", "PurityName");
            ViewBag.OrnamentList = new SelectList(_valuatorOneService.GetOrnamentList(), "ItemId", "ItemName");
            return View(model);
        }
        #endregion

        #region Insert

        [HttpPost]
        public ActionResult Insert(ValuatorOneViewModel objViewModel)
        {
            try
            {
                ModelState.Remove("Id");
                if (objViewModel.ID == 0)
                {
                    if (InsertData(objViewModel))
                    {
                        return Json(1, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(3, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    if (InsertData(objViewModel))
                    {
                        return Json(2, JsonRequestBehavior.AllowGet);
                    }
                }
                objViewModel.ValuatorOneDetailsList = new List<ValuatorOneDetailsViewModel>();
                ViewBag.PurityList = new SelectList(_valuatorOneService.GetAllPurityMaster(), "Id", "PurityName");
                ViewBag.OrnamentList = new SelectList(_valuatorOneService.GetOrnamentList(), "ItemId", "ItemName");
                return View("ValuatorOne", objViewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Insert

        #region Insert Data

        public bool InsertData(ValuatorOneViewModel model)
        {
            bool retVal = false;
            model.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
            model.UpdatedBy = Convert.ToInt32(Session["UserLoginId"]);
            try
            {
                //model.ValuatorOneDetailsList = (List<ValuatorOneDetailsViewModel>)Session["sub"];
                _valuatorOneService.SaveRecord(model);
                retVal = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        #endregion Insert Data

        #region AddDocument
        [HttpPost]
        public JsonResult AddDocument()
        {
            int Id = Convert.ToInt32(Request.Form["Id"]);
            int DocumentTypeId = Convert.ToInt32(Request.Form["DocumentTypeId"]);
            int DocumentId = Convert.ToInt32(Request.Form["DocumentId"]);
            DateTime? ExpiryDate = null;
            if (Request.Form["ExpiryDate"].ToString() != "")
            {
                ExpiryDate = Convert.ToDateTime(Request.Form["ExpiryDate"]);
            }
            HttpFileCollectionBase files = Request.Files;
            string pFileName = "";
            string pFileExtension = "";
            HttpPostedFileBase postedFile = files[0];
            Stream fs = postedFile.InputStream;
            pFileName = postedFile.FileName;
            pFileExtension = postedFile.ContentType;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bytes = br.ReadBytes(postedFile.ContentLength);
            //base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            DocumentUploadDetailsVM docupload = null;
            var sessionlist = (List<DocumentUploadDetailsVM>)Session["sub"];
            if (sessionlist == null)
            {
                sessionlist = new List<DocumentUploadDetailsVM>();
            }
            docupload = new DocumentUploadDetailsVM();
            docupload.ID = Id;
            docupload.DocumentTypeId = DocumentTypeId;
            docupload.DocumentId = DocumentId;
            docupload.ExpiryDate = ExpiryDate;
            docupload.UploadDocName = bytes;
            docupload.FileName = pFileName;
            docupload.FileExtension = pFileExtension;
            docupload.SpecifyOther = Request.Form["SpecifyOther"];
            docupload.NameonDocument = Request.Form["NameonDocument"];
            sessionlist.Add(docupload);
            Session["sub"] = sessionlist;
            return Json(docupload, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetCustomerDetails

        public ActionResult GetCustomerDetails()
        {
            return PartialView("_CustomerDetails", _valuatorOneService.GetPreSanctionList());
        }

        #endregion GetCustomerDetails
    }
}