using MangalWeb.Model.Masters;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class SourceofApplicationController : BaseController
    {
        SourcefApplicationService _sourceofApplicationService = new SourcefApplicationService();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateEdit(SourceofApplicationViewModel source)
        {
            source.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
            source.UpdatedBy = Convert.ToInt32(Session["UserLoginId"]);
            try
            {
                if (source.EditID <= 0)
                {
                    var data = _sourceofApplicationService.CheckSourceNameExists(source.SourceName);
                    if (data != null)
                    {
                        ModelState.AddModelError("SourceName", "Source Name Already Exists");
                        return Json(source);
                    }
                }
                _sourceofApplicationService.SaveUpdateRecord(source);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Json(source);
        }

        public ActionResult GetSourceApplicationById(int ID)
        {
            string operation = Session["Operation"].ToString();
            ButtonVisiblity(operation);
            var tblSource = _sourceofApplicationService.GetSourceApplicationById(ID);
            var model = new SourceofApplicationViewModel();
            if (tblSource!=null)
            {
                model = _sourceofApplicationService.SetDataOnEdit(tblSource);
            }
            model.operation = operation;
            return View("SourceApplication", model);
        }

        // GETDelete/5
        public ActionResult Delete(int id)
        {
            string data = "";
            _sourceofApplicationService.DeleteRecord(id);
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult doesSourceNameExist(string SourceName)
        {
            var result = "";
            var data = _sourceofApplicationService.CheckSourceNameExists(SourceName);
            //Check if city name already exists
            if (data != null)
            {
                if (SourceName.ToLower() == data.ToLower().ToString())
                {
                    result = "Source Name Already Exists";
                }
                else
                {
                    result = "";
                }
            }
            return Json(result);
        }

        public ActionResult SourceApplication()
        {
            ButtonVisiblity("Index");
            var model = new SourceofApplicationViewModel();
            model.ID = _sourceofApplicationService.GetMaxSourceId();
            return View(model);
        }

        public ActionResult GetSourceApplicationTable(string Operation)
        {
            Session["Operation"] = Operation;
            //ButtonVisiblity(Operation);
            List<SourceofApplicationViewModel> list = new List<SourceofApplicationViewModel>();
            var model = new SourceofApplicationViewModel();
            //var tablelist = dd._context.Mst_SourceofApplication.ToList();
            int onlinecategory = 1;
            int status = 1;
            //foreach (var item in tablelist)
            //{
            //    model = new SourceofApplicationViewModel();
            //    model.SourceName = item.Soa_Name;
            //    model.EditID = item.Soa_Id;
            //    model.ID = item.Soa_Id;
            //    model.SourceCategirystr = item.Soa_Category == onlinecategory ? "Online" : "Offline";
            //    model.SourceStatusstr = item.Soa_Status == status ? "Active" : "Inactive";
            //    list.Add(model);
            //}
            return PartialView("_SourceApplicationList", list);
        }
    }
}