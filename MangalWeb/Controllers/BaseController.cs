using MangalWeb.Model.Utilities;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class BaseController : Controller
    {
        UserAuthorizationService _userAuthorizationService = new UserAuthorizationService();
        public ActionResult Menu()
        {
            return PartialView("Menu");
        }

        public ActionResult Header()
        {
            return PartialView("Header");
        }

        public ActionResult UserAuthorizationForm(int MenuId, int UserId)
        {
            Session["MenuId"] = MenuId;
            Session["UserLoginID"] = UserId;
            IList<UserAuthorizationForms> user = _userAuthorizationService.GetFormDetails_FormIdWise(MenuId, UserId);
            var jsonResult = Json(user, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }

        public void ButtonVisiblity(string action)
        {
            switch (action)
            {
                case "Index":
                    TempData["btn_editclass"] = "mb -xs mt-xs mr-xs btn btn-primary";
                    TempData["btn_saveclass"] = "mb -xs mt-xs mr-xs btn btn-success";
                    TempData["btn_deleteclass"] = "mb -xs mt-xs mr-xs btn btn-danger disabled";
                    TempData["btn_viewclass"] = "mb-xs mt-xs mr-xs btn btn-warning";

                    break;
                case "Edit":
                    TempData["btn_editclass"] = "mb -xs mt-xs mr-xs btn btn-primary disabled";
                    TempData["btn_saveclass"] = "mb -xs mt-xs mr-xs btn btn-success";
                    TempData["btn_deleteclass"] = "mb -xs mt-xs mr-xs btn btn-danger";
                    TempData["btn_viewclass"] = "mb-xs mt-xs mr-xs btn btn-warning disabled";
                    break;
                case "View":
                    TempData["btn_editclass"] = "mb -xs mt-xs mr-xs btn btn-primary disabled";
                    TempData["btn_saveclass"] = "mb -xs mt-xs mr-xs btn btn-success disabled";
                    TempData["btn_deleteclass"] = "mb -xs mt-xs mr-xs btn btn-danger disabled";
                    TempData["btn_viewclass"] = "mb-xs mt-xs mr-xs btn btn-warning";
                    break;
            }
        }
    }
}