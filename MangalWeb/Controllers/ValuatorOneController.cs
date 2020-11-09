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
            model = _valuatorOneService.GetMaxTransactionId();
            ViewBag.PurityList = new SelectList(_valuatorOneService.GetAllPurityMaster(), "Id", "PurityName");
            ViewBag.OrnamentList = new SelectList(_valuatorOneService.GetOrnamentList(), "ItemId", "ItemName");
            Session["ValuationImageList"] = null;
            Session["ConsolidatedImage"] = null;
            Session["ConsolidatedImageName"] = null;
            Session["ConsolidatedImageContentType"] = null;
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
                            model.ValuatorOneDetailsList[i].ValuationImageFile = list[i].ValuationImageFile;
                            model.ValuatorOneDetailsList[i].ImageName = list[i].ImageName;
                            model.ValuatorOneDetailsList[i].ContentType = list[i].ContentType;
                        }
                    }
                }
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

        #region AddValuationImage
        [HttpPost]
        public JsonResult AddValuationImage()
        {
            var sessionlist = (List<ValuatorOneDetailsViewModel>)Session["ValuationImageList"];
            HttpFileCollectionBase files = Request.Files;
            HttpPostedFileBase postedFile = files[0];
            int id = Convert.ToInt32(Request.Form["ID"]);
            Stream fs = postedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bytes = br.ReadBytes(postedFile.ContentLength);
            ValuatorOneDetailsViewModel docupload = new ValuatorOneDetailsViewModel();
            if (sessionlist != null)
            {
                docupload = sessionlist.Where(x => x.ID == id).FirstOrDefault();
                if (docupload != null)
                {
                    docupload.ValuationImageFile = bytes;
                    docupload.ImageName = postedFile.FileName;
                    docupload.ContentType = postedFile.ContentType;
                }
                else
                {
                    docupload = new ValuatorOneDetailsViewModel();
                    int tempid = sessionlist.Max(x => x.ID);
                    tempid++;
                    docupload.ID = id == 0 ? tempid : id;
                    docupload.ValuationImageFile = bytes;
                    docupload.ImageName = postedFile.FileName;
                    docupload.ContentType = postedFile.ContentType;
                    sessionlist.Add(docupload);
                }
            }
            else
            {
                sessionlist = new List<ValuatorOneDetailsViewModel>();
                docupload = new ValuatorOneDetailsViewModel();
                docupload.ID = id == 0 ? 1 : 1;
                docupload.ValuationImageFile = bytes;
                docupload.ImageName = postedFile.FileName;
                docupload.ContentType = postedFile.ContentType;
                sessionlist.Add(docupload);
            }
            Session["ValuationImageList"] = sessionlist;
            return Json(docupload, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region SetRateOfSelectedPurity
        public JsonResult SetRateOfSelectedPurity(int ProductId, int PurityId)
        {
            double rate = _valuatorOneService.GetRateFromProductRate(ProductId, PurityId);
            return Json(rate, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetCustomerDetails

        public ActionResult GetCustomerDetails()
        {
            return PartialView("_CustomerDetails", _valuatorOneService.GetPreSanctionList());
        }

        #endregion GetCustomerDetails

        #region GetValuatorOneDetails

        public ActionResult GetValuatorOneDetails(string Operation)
        {
            Session["Operation"] = Operation;
            return PartialView("_ValuatorOneDetails", _valuatorOneService.GetValuatorOneList());
        }

        #endregion GetValuatorOneDetails

        #region GetValuatorOneDetailsById
        public ActionResult GetValuatorOneDetailsById(int Id)
        {
            var model = _valuatorOneService.GetValuatorOneDetailsById(Id);
            var file = _valuatorOneService.GetConsolidatedImage(model.ID);
            if (file.ConsolidatedImage != null)
            {
                Session["ConsolidatedImage"] = file.ConsolidatedImage;
                Session["ConsolidatedImageName"] = model.ImageName;
                Session["ConsolidatedImageContentType"] = file.ContentType;
            }

            var sessionlist = (List<ValuatorOneDetailsViewModel>)Session["ValuationImageList"];
            if (sessionlist == null)
            {
                sessionlist = new List<ValuatorOneDetailsViewModel>();
            }
            foreach (var item in model.ValuatorOneDetailsList)
            {
                var docupload = new ValuatorOneDetailsViewModel();
                docupload.ID = item.ID;
                var file1 = _valuatorOneService.GetValuationImage(item.ID);
                docupload.ValuationImageFile = file1.OrnamentImage;
                docupload.ImageName = file1.ImageName;
                docupload.ContentType = file1.ContentType;
                sessionlist.Add(docupload);
                Session["ValuationImageList"] = sessionlist;
            }

            string operation = String.Empty;
            if (Session["Operation"] != null)
            {
                operation = Session["Operation"].ToString();
            }

            model.operation = operation;

            model.RecordExist = false;

            if (_valuatorOneService.CheckRecordExist(Id) > 0)
            {
                model.RecordExist = true;
            }
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
                string data = "";
                if (_valuatorOneService.CheckRecordExist(id) > 0)
                {
                    data = "Record Cannot Be Deleted Already In Use!";
                }
                else
                {
                    _valuatorOneService.DeleteRecord(id);
                }
                return Json(data,JsonRequestBehavior.AllowGet);
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
            var file = _valuatorOneService.GetConsolidatedImage(id);
            return File(file.ConsolidatedImage, file.ContentType);
        }
        #endregion

        #region DownLoadValuationImage
        public FileResult DownLoadValuationImage(int id)
        {
            var file = _valuatorOneService.GetValuationImage(id);
            return File(file.OrnamentImage, file.ContentType);
        }
        #endregion

        #region GetOrnamentProductWise
        public JsonResult GetOrnamentProductWise(int id)
        {
            var data = new SelectList(_valuatorOneService.GetOrnamentProductWise(id), "ItemID", "ItemName");
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CheckOrnamentStatus
        public JsonResult CheckOrnamentStatus(int OrnamentId)
        {
            string status = _valuatorOneService.CheckOrnamentStatus(OrnamentId);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Remove
        public ActionResult Remove(int id)
        {
            var list = (List<ValuatorOneDetailsViewModel>)Session["ValuationImageList"];
            list.Remove(list.Where(x => x.ID == id).FirstOrDefault());
            Session["ValuationImageList"] = list;
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}