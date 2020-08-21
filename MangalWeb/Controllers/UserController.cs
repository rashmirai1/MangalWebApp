using MangalWeb.Model.Utilities;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class UserController : BaseController
    {
        UserAuthorizationService _userAuthorizationService = new UserAuthorizationService();
        UserService _userService = new UserService();

        // GET: User
        public ActionResult UserMaster()
        {
            ButtonVisiblity("Index");
            ViewBag.UserCategoryList = new SelectList(_userAuthorizationService.GetUserCategory(), "refId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateEdit(UserViewModel user)
        {
            try
            {
                if (user.ID <= 0)
                {
                    var data = _userService.CheckEmployeeCodeExists(user.UserName);
                    if (data != null)
                    {
                        ModelState.AddModelError("UserName", "User Name Already Exists");
                        return Json(user);
                    }
                }
                _userService.SaveUpdateRecord(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Json(user);
        }

        public ActionResult GetUserMasterById(int ID)
        {
            string operation = Session["Operation"].ToString();
            ButtonVisiblity(operation);
            var tbluser = _userService.GetUserMasterById(ID);
            var model = new UserViewModel();
            if (tbluser != null)
            {
                model = _userService.SetDataOnEdit(tbluser);
            }
            model.operation = operation;
            return View("UserMaster", model);
        }

        // GETDelete/5
        public ActionResult Delete(int id)
        {
            _userService.DeleteCityRecord(id);
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult doesEmployeeCodeExist(string EmployeeCode)
        {
            var result = "";
            var data = _userService.CheckEmployeeCodeExists(EmployeeCode);
            //Check if empluee code already exists
            if (data != null)
            {
                if (EmployeeCode.ToLower() == data.ToLower().ToString())
                {
                    result = "Employee Code Already Exists";
                }
                else
                {
                    result = "";
                }
            }
            return Json(result);
        }

        public ActionResult GetUserMasterTable(string Operation)
        {
            Session["Operation"] = Operation;
            return PartialView("_UserMasterList", _userService.GetAllUserDetails());
        }
    }
}