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
            model.TransactionId = _valuatorTwoService.GetMaxTransactionId();
            model.ValuatorTwoDetailsList = new List<ValuatorTwoDetailsViewModel>();
            ViewBag.PurityList = new SelectList(_valuatorTwoService.GetAllPurityMaster(), "Id", "PurityName");
            ViewBag.OrnamentList = new SelectList(_valuatorTwoService.GetOrnamentList(), "ItemId", "ItemName");
            Session["ValuationImageTwoList"] = null;
            Session["ConsolidatedImageTwo"] = null;
            Session["ConsolidatedImageNameTwo"] = null;
            Session["ConsolidatedImageContentTypeTwo"] = null;
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
                return View("ValuatoTwo", objViewModel);
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
                if (Session["ConsolidatedImageTwo"] != null)
                {
                    model.ConsolidatedImageFile = (byte[])Session["ConsolidatedImageTwo"];
                    model.ImageName = Session["ConsolidatedImageNameTwo"].ToString();
                    model.ContentType = Session["ConsolidatedImageContentTypeTwo"].ToString();
                }
                if (Session["ValuationImageTwoList"] != null)
                {
                    var list = (List<ValuatorOneDetailsViewModel>)Session["ValuationImageTwoList"];
                    if (list != null)
                    {
                        for (int i = 0; i <= list.Count - 1; i++)
                        {
                            // for valuator two
                            model.ValuatorTwoDetailsList[i].ValuationImageFile = list[i].ValuationImageFile;
                            model.ValuatorTwoDetailsList[i].ImageName = list[i].ImageName;
                            model.ValuatorTwoDetailsList[i].ContentType = list[i].ContentType;
                            // for valuation table 
                            model.ValuationDetailsList[i].ValuationImageFile = list[i].ValuationImageFile;
                            model.ValuationDetailsList[i].ImageName = list[i].ImageName;
                            model.ValuationDetailsList[i].ContentType = list[i].ContentType;
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

        #region GetValuatorOneList

        public ActionResult GetValuatorOneList()
        {
            return PartialView("_ValuatorOneDetails", _valuatorTwoService.GetValuatorOneList());
        }

        #endregion GetValuatorOneList        

        #region GetValuatorOneDetailsById
        public ActionResult GetValuatorOneDetailsById(int Id)
        {
            var model = _valuatorTwoService.GetValuatorOneDetailsById(Id);
            model.TransactionId = _valuatorTwoService.GetMaxTransactionId();
            var file = _valuatorTwoService.GetConsolidatedImage(model.ValuatorOneId);
            Session["ConsolidatedImageTwo"] = file.ConsolidatedImage;
            Session["ConsolidatedImageNameTwo"] = model.ImageName;
            Session["ConsolidatedImageContentTypeTwo"] = file.ContentType;

            var sessionlist = (List<ValuatorOneDetailsViewModel>)Session["ValuationImageTwoList"];
            if (sessionlist == null)
            {
                sessionlist = new List<ValuatorOneDetailsViewModel>();
            }
            foreach (var item in model.ValuatorTwoDetailsList)
            {
                var docupload = new ValuatorOneDetailsViewModel();
                docupload.ID = item.ID;
                var file1 = _valuatorTwoService.GetValuationImage(item.ID);
                docupload.ValuationImageFile = file1.OrnamentImage;
                docupload.ImageName = file1.ImageName;
                docupload.ContentType = file1.ContentType;
                sessionlist.Add(docupload);
                Session["ValuationImageTwoList"] = sessionlist;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetValuatorOneDetails

        public ActionResult GetValuatorOneDetails(string Operation)
        {
            Session["Operation"] = Operation;
            return PartialView("_ValuatorTwoDetails", _valuatorTwoService.GetValuatorTwoList());
        }

        #endregion GetValuatorOneDetails

        #region GetValuatorTwoDetailsById
        public ActionResult GetValuatorTwoDetailsById(int Id)
        {
            var model = _valuatorTwoService.GetValuatorTwoDetailsById(Id);
            var file = _valuatorTwoService.GetConsolidatedTwoImage(model.ID);
            Session["ConsolidatedImageTwo"] = file.ConsolidatedImage;
            Session["ConsolidatedImageNameTwo"] = model.ImageName;
            Session["ConsolidatedImageContentTypeTwo"] = file.ContentType;

            var sessionlist = (List<ValuatorOneDetailsViewModel>)Session["ValuationImageTwoList"];
            if (sessionlist == null)
            {
                sessionlist = new List<ValuatorOneDetailsViewModel>();
            }
            foreach (var item in model.ValuatorTwoDetailsList)
            {
                var docupload = new ValuatorOneDetailsViewModel();
                docupload.ID = item.ID;
                var file1 = _valuatorTwoService.GetValuationTwoImage(item.ID);
                docupload.ValuationImageFile = file1.OrnamentImage;
                docupload.ImageName = file1.ImageName;
                docupload.ContentType = file1.ContentType;
                sessionlist.Add(docupload);
                Session["ValuationImageTwoList"] = sessionlist;
            }
            string operation = String.Empty;
            if (Session["Operation"] != null)
            {
                operation = Session["Operation"].ToString();
            }
            model.operation = operation;
            model.RecordExist = false;
            if (_valuatorTwoService.CheckRecordExist(Id) > 0)
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
                if (_valuatorTwoService.CheckRecordExist(id) > 0)
                {
                    data = "Record Cannot Be Deleted Already In Use!";
                }
                else
                {
                    _valuatorTwoService.DeleteRecord(id);
                }
                return Json(data,JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Delete

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

        #region GetValuatorOneData

        public JsonResult GetValuatorOneData(int id)
        {
            var model = _valuatorTwoService.GetValuatorOneData(id);
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        #endregion GetValuatorOneDetails
    }
}