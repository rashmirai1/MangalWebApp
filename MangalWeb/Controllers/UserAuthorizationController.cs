using MangalWeb.Model.Utilities;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class UserAuthorizationController : Controller
    {
        UserAuthorizationService _userAuthorizationService = new UserAuthorizationService();
        // GET: UserAuthorization
        public ActionResult UserAuthorization()
        {
            GetUserCategory();
            UserAuthorization userobj = new UserAuthorization();
            UserAuthorizationForms userobj1 = new UserAuthorizationForms();
            IList<UserAuthorizationForms> userauth = new List<UserAuthorizationForms>();
            ViewBag.user = new SelectList(userauth, "UserID", "User");
            ViewBag.menus = new SelectList(userauth, "FormID", "FormName");
            UserAuthorization authorization = new UserAuthorization();
            authorization.userauthorizationformsList.Insert(0, new UserAuthorizationForms());
            TempData["UTitle"] = "";
            TempData["UMessage"] = "";
            TempData["myModalDeleteMessageStyle"] = "";
            return View("UserAuthorization", authorization);
        }

        private void GetUserCategory()
        {
            var usercategory = _userAuthorizationService.GetUserCategory();
            ViewBag.usercategory = new SelectList(usercategory, "UserCategoryID", "UserCategoryName");
        }

        [HttpPost]
        public ActionResult GetUser(int UserCategoryId)
        {
            var user = _userAuthorizationService.GetUser(UserCategoryId);
            ViewBag.user = new SelectList(user, "UserID", "User");
            var forms = _userAuthorizationService.GetMenuList();
            ViewBag.menus = new SelectList(forms, "ID", "Name");
            var viewModel = user.Select(x => new
            {
                UserID = x.UserID,
                User = x.UserName,
            });
            var viewModel1 = forms.Select(x => new
            {
                FormID = x.ID,
                FormName = x.Name,
            });

            return Json(new { FirstList = viewModel, SecondList = viewModel1 }, JsonRequestBehavior.AllowGet);
        }

        #region GetForms
        //public virtual IList<UserAuthorizationForms> GetForms(int parentid, int UserId, int UserCategoryID)
        //{
            //Hashtable ht = new Hashtable();
            //ht.Add("ParentID", parentid);
            //ht.Add("UserID", UserId);
            //ht.Add("UserCategoryID", UserCategoryID);
            //DataSet ds = new DataSet();
            //ds = aquaCoolRepository.GetById("T_UserAuthorization_Forms", ht);

            //var list = ds.Tables[0].AsEnumerable().Select(dataRow => new UserAuthorizationForms
            //{
            //    ID = dataRow.Field<int>("ID"),
            //    FormID = dataRow.Field<int>("FormID"),
            //    FormName = dataRow.Field<string>("Name"),
            //    ParentForm = dataRow.Field<string>("ParentForm"),
            //    isEdit = dataRow.Field<bool>("isEdit"),
            //    isVisible = dataRow.Field<bool>("isVisible"),
            //    isSave = dataRow.Field<bool>("isSave"),
            //    isDelete = dataRow.Field<bool>("isDelete"),
            //    isView = dataRow.Field<bool>("isView"),
            //    ParentID = dataRow.Field<int>("ParentID"),
            //}).ToList();
          //  return list;
        //}
        #endregion
    }
}