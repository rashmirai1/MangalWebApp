﻿using MangalWeb.Model.Masters;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class GSTController : BaseController
    {
        GSTService _gstService = new GSTService();



        [HttpPost]
        public ActionResult Insert(GstViewModel objViewModel)
        {
            ModelState.Remove("Id");
            if (objViewModel.EditID == 0)
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
            ViewBag.GSTAccountList = new SelectList(_gstService.GSTAccountList(), "AccountID", "Name");
            ViewBag.IGSTAccountList = new SelectList(_gstService.GSTAccountList(), "AccountID", "Name");
            return View("Index", objViewModel);
        }

        #region Insert Data

        public bool InsertData(GstViewModel gstvm)
        {
            bool retVal = false;
            gstvm.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
            gstvm.UpdatedBy = Convert.ToInt32(Session["UserLoginId"]);
            try
            {
                _gstService.SaveUpdateRecord(gstvm);
                retVal = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        #endregion Insert Data

        public ActionResult GetGSTById(int ID)
        {
            string operation = Session["Operation"].ToString();
            ButtonVisiblity(operation);
            var tblgst = _gstService.GetGSTById(ID);
            var gstvm = new GstViewModel();
            if (tblgst != null)
            {
                gstvm = _gstService.SetDataOnEdit(tblgst);
            }
            gstvm.operation = operation;
            ViewBag.GSTAccountList = new SelectList(_gstService.GSTAccountList(), "AccountID", "Name");
            ViewBag.IGSTAccountList = new SelectList(_gstService.GSTAccountList(), "AccountID", "Name");
            return View("GST", gstvm);
        }

        // GETDelete/5
        public ActionResult Delete(int id)
        {
            _gstService.DeleteRecord(id);
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckRecordonEditMode(int Id)
        {
            string data = "";
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GST()
        {
            ButtonVisiblity("Index");
            var model = new GstViewModel();
            model.ID = _gstService.GetMaxId();
            ViewBag.GSTAccountList = new SelectList(_gstService.GSTAccountList(), "AccountID", "Name");
            return View(model);
        }

        public ActionResult GetGSTTable(string Operation)
        {
            Session["Operation"] = Operation;
            var tablelist = _gstService.GetAllGSTMasters();
            return PartialView("_GSTList", tablelist);
        }
    }
}