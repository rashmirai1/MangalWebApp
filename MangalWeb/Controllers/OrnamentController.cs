using MangalWeb.Model.Masters;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class OrnamentController : BaseController
    {
        OrnamentService _ornamentService = new OrnamentService();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateEdit(OrnamentViewModel ornament)
        {
            try
            {
                if (ornament.ID <= 0)
                {
                    var data = _ornamentService.CheckOrnamentNameExists(ornament.OrnamentName);
                    if (data != null)
                    {
                        ModelState.AddModelError("OrnamentName", "Ornament Name Already Exists");
                        return Json(ornament);
                    }
                }
                _ornamentService.SaveUpdateRecord(ornament);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Json(ornament);
        }

        public ActionResult GetOrnamentById(int ID)
        {
            string operation = Session["Operation"].ToString();
            ButtonVisiblity(operation);
            var ornament = new OrnamentViewModel();
            var tblOrnament = _ornamentService.GetOrnamentById(ID);
            if (tblOrnament != null)
            {
                ornament = _ornamentService.SetDataOnEdit(tblOrnament);
            }
            ornament.operation = operation;
            ViewBag.ProductList = new SelectList(_ornamentService.GetProductList(), "Id", "Name");
            return View("Ornament", ornament);
        }

        // GETDelete/5
        public ActionResult Delete(int id)
        {
            _ornamentService.DeleteRecord(id);
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult doesOrnamentNameExist(string OrnamentName)
        {
            var data = _ornamentService.CheckOrnamentNameExists(OrnamentName);
            var result = "";
            //Check if document name already exists
            if (data != null)
            {
                if (OrnamentName.ToLower() == data.ToLower().ToString())
                {
                    result = "Ornament Name Already Exists";
                }
                else
                {
                    result = "";
                }
            }
            return Json(result);
        }

        public ActionResult Ornament()
        {
            ButtonVisiblity("Index");
            ViewBag.ProductList = new SelectList(_ornamentService.GetProductList(), "Id", "Name");
            return View();
        }

        public ActionResult GetOrnamentTable(string Operation)
        {
            Session["Operation"] = Operation;
            //ButtonVisiblity(Operation);
            var list = _ornamentService.SetDataofModalList();
            return PartialView("_OrnamentList", list);
        }
    }
}