﻿using MangalWeb.Model.Masters;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class ProductRateController : BaseController
    {
        ProductRateService _productrateService = new ProductRateService();

        [HttpPost]
        public ActionResult Insert(ProductRateViewModel objViewModel)
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
            return View("ProductRate", objViewModel);
        }

        #region Insert Data

        public bool InsertData(ProductRateViewModel productratevm)
        {
            bool retVal = false;
            productratevm.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
            productratevm.UpdatedBy = Convert.ToInt32(Session["UserLoginId"]);
            try
            {
                _productrateService.SaveRecord(productratevm);
                retVal = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        #endregion Insert Data

        public ActionResult GetProductRateById(int ID)
        {
            string operation = Session["Operation"].ToString();
            ButtonVisiblity(operation);
            var productRatevm = _productrateService.GetProductRateById(ID);
            ViewBag.PurityList = new SelectList(_productrateService.GetPurityById(productRatevm.Product), "Id", "PurityName");
            return View("ProductRate", productRatevm);
        }

        // GETDelete/5
        public ActionResult Delete(int id)
        {
            _productrateService.DeleteRecord(id);
            return Json(JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductRate()
        {
            ButtonVisiblity("Index");
            var model = new ProductRateViewModel();
            model.ProductRateList = new List<ProductRateDetailsVM>();
            ViewBag.PurityList = new SelectList(_productrateService.GetAllPurityMaster(), "Id", "PurityName");
            return View(model);
        }

        public JsonResult GetPurity(int id)
        {
            var data = new SelectList(_productrateService.GetPurityById(id), "Id", "PurityName");
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductRateTable(string Operation)
        {
            Session["Operation"] = Operation;
            var model = new ProductRateViewModel();
            var list = _productrateService.SetDataofModalList();
            return PartialView("_ProductRateList", list);
        }
    }
}


