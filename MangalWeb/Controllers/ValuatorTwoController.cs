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
    public class ValuatorTwoController : BaseController
    {
        ValuatorTwoService _valuatorTwoService = new ValuatorTwoService();

        #region ValuatorTwo
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ValuatorTwo()
        {
            ButtonVisiblity("Index");
            var model = new ValuatorTwoViewModel();
            model.ValuatorTwoDetailsList = new List<ValuatorTwoDetailsViewModel>();
            ViewBag.PurityList = new SelectList(_valuatorTwoService.GetAllPurityMaster(), "Id", "PurityName");
            ViewBag.OrnamentList = new SelectList(_valuatorTwoService.GetOrnamentList(), "ItemId", "ItemName");
            return View(model);
        }
        #endregion

        #region Insert

        [HttpPost]
        public ActionResult Insert(ValuatorTwoViewModel objViewModel)
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
                objViewModel.ValuatorTwoDetailsList = new List<ValuatorTwoDetailsViewModel>();
                ViewBag.PurityList = new SelectList(_valuatorTwoService.GetAllPurityMaster(), "Id", "PurityName");
                ViewBag.OrnamentList = new SelectList(_valuatorTwoService.GetOrnamentList(), "ItemId", "ItemName");
                return View("ValuatorOne", objViewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Insert

        #region Insert Data

        public bool InsertData(ValuatorTwoViewModel model)
        {
            bool retVal = false;
            model.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
            model.UpdatedBy = Convert.ToInt32(Session["UserLoginId"]);
            try
            {
                if (Session["ConsolidatedImage"] != null)
                {
                    model.ConsolidatedImageFile = (byte[])Session["ConsolidatedImage"];
                    model.ImageName = Session["ConsolidatedImageName"].ToString();
                    model.ContentType = Session["ConsolidatedImageContentType"].ToString();
                }
                if (Session["ValuationImageList"] != null)
                {
                    var list = (List<ValuatorOneDetailsViewModel>)Session["ValuationImageList"];
                    if (list != null)
                    {
                        for (int i = 0; i <= list.Count - 1; i++)
                        {
                            model.ValuatorTwoDetailsList[i].ValuationImageFile = list[i].ValuationImageFile;
                            model.ValuatorTwoDetailsList[i].ImageName = list[i].ImageName;
                            model.ValuatorTwoDetailsList[i].ContentType = list[i].ContentType;
                        }
                    }
                }
                _valuatorTwoService.SaveRecord(model);
                retVal = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        #endregion Insert Data

        #region AddValuationImage
        [HttpPost]
        public JsonResult AddValuationImage()
        {
            HttpFileCollectionBase files = Request.Files;
            HttpPostedFileBase postedFile = files[0];
            Stream fs = postedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bytes = br.ReadBytes(postedFile.ContentLength);
            //base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            ValuatorOneDetailsViewModel docupload = null;
            var sessionlist = (List<ValuatorOneDetailsViewModel>)Session["ValuationImageList"];
            docupload = new ValuatorOneDetailsViewModel();
            if (sessionlist == null)
            {
                sessionlist = new List<ValuatorOneDetailsViewModel>();
            }
            docupload.ID = Convert.ToInt32(Request.Form["ID"]);
            docupload.ValuationImageFile = bytes;
            docupload.ImageName = postedFile.FileName;
            docupload.ContentType = postedFile.ContentType;
            sessionlist.Add(docupload);
            Session["ValuationImageList"] = sessionlist;
            return Json(docupload, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetCustomerDetails

        public ActionResult GetCustomerDetails()
        {
            return PartialView("_CustomerDetails", _valuatorTwoService.GetPreSanctionList());
        }

        #endregion GetCustomerDetails

        #region GetValuatorOneDetails

        public ActionResult GetValuatorOneDetails()
        {
            Session["Operation"] = "Edit";
            return PartialView("_ValuatorOneDetails", _valuatorTwoService.GetValuatorOneList());
        }

        #endregion GetValuatorOneDetails

        #region GetValuatorOneDetailsById
        public ActionResult GetValuatorOneDetailsById(int Id)
        {
            var model = _valuatorTwoService.GetValuatorOneDetailsById(Id);
            var file = _valuatorTwoService.GetConsolidatedImage(model.ID);
            Session["ConsolidatedImage"] = file.ConsolidatedImage;
            Session["ConsolidatedImageName"] = model.ImageName;
            Session["ConsolidatedImageContentType"] = file.ContentType;
            string operation = Session["Operation"].ToString();

            var sessionlist = (List<ValuatorOneDetailsViewModel>)Session["ValuationImageList"];
            var docupload = new ValuatorOneDetailsViewModel();
            if (sessionlist == null)
            {
                sessionlist = new List<ValuatorOneDetailsViewModel>();
            }
            foreach (var item in model.ValuatorTwoDetailsList)
            {
                docupload.ID = item.ID;
                var file1 = _valuatorTwoService.GetValuationImage(item.ID);
                docupload.ValuationImageFile = file1.OrnamentImage;
                docupload.ImageName = file1.ImageName;
                docupload.ContentType = file1.ContentType;
                sessionlist.Add(docupload);
                Session["ValuationImageList"] = sessionlist;
            }
            model.operation = operation;
            ButtonVisiblity(operation);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Delete

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            try
            {
                _valuatorTwoService.DeleteRecord(id);
                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Delete

        #region UploadConsolidatedImage
        [HttpPost]
        public JsonResult UploadConsolidatedImage()
        {
            HttpFileCollectionBase files = Request.Files;
            HttpPostedFileBase postedFile = files[0];
            Stream fs = postedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bytes = br.ReadBytes(postedFile.ContentLength);
            Session["ConsolidatedImage"] = bytes;
            Session["ConsolidatedImageName"] = postedFile.FileName;
            Session["ConsolidatedImageContentType"] = postedFile.ContentType;
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Download
        public FileResult Download(int id)
        {
            var file = _valuatorTwoService.GetConsolidatedImage(id);
            return File(file.ConsolidatedImage, file.ContentType);
        }
        #endregion

        #region DownLoadValuationImage
        public FileResult DownLoadValuationImage(int id)
        {
            var file = _valuatorTwoService.GetValuationImage(id);
            return File(file.OrnamentImage, file.ContentType);
        }
        #endregion
    }
}