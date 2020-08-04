using MangalWeb.Model.Masters;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class SchemeController : BaseController
    {
        SchemeService _schemeService = new SchemeService();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateEdit(SchemeViewModel scheme)
        {
            try
            {
                scheme.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
                scheme.UpdatedBy = Convert.ToInt32(Session["UserLoginId"]);

                if (scheme.EditID <= 0)
                {
                    var data = _schemeService.CheckSchemeNameExists(scheme.SchemeName);
                    if (data != null)
                    {
                        ModelState.AddModelError("SchemeName", "Scheme Name Already Exists");
                        return Json(scheme);
                    }
                }
                _schemeService.SaveUpdateRecord(scheme);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Json(scheme);
        }

        public ActionResult GetSchemeById(int ID)
        {
            string operation = Session["Operation"].ToString();
            ButtonVisiblity(operation);
            var tblscheme = _schemeService.GetSchemeMasterById(ID);
            var model = new SchemeViewModel();
            if (tblscheme != null)
            {
                model = _schemeService.SetDataOnEdit(tblscheme);
            }
            model.operation = operation;
            ViewBag.PurityList = new SelectList(_schemeService.GetPurityById(model.Product).ToList(), "Id", "PurityName");
            ViewBag.ProductList = new SelectList(_schemeService.GetProductList(), "Id", "Name");
            return View("Scheme", model);
        }

        // GETDelete/5
        public ActionResult Delete(int id)
        {
            _schemeService.DeleteRecord(id);
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult doesSchemeNameExist(string SchemeName)
        {
            var result = "";
            var data = _schemeService.CheckSchemeNameExists(SchemeName);
            //Check if city name already exists
            if (data != null)
            {
                if (SchemeName.ToLower() == data.ToLower().ToString())
                {
                    result = "Scheme Name Already Exists";
                }
                else
                {
                    result = "";
                }
            }
            return Json(result);
        }

        public ActionResult Scheme()
        {
            ButtonVisiblity("Index");
            var model = new SchemeViewModel();
            ViewBag.PurityList = new SelectList(_schemeService.GetPurityMasterList(), "Id", "PurityName");
            ViewBag.ProductList = new SelectList(_schemeService.GetProductList(), "Id", "Name");
            model.SchemeId = _schemeService.GetMaxPkNo();
            return View(model);
        }

        public JsonResult GetPurity(int id)
        {
            var data = new SelectList(_schemeService.GetPurityById(id), "Id", "PurityName");
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSchemeTable(string Operation)
        {
            Session["Operation"] = Operation;
            //ButtonVisiblity(Operation);
            return PartialView("_SchemeList", _schemeService.SetModalSchemeList());
        }
    }
}