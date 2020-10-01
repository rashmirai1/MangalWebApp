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
            GetBranch();
            IList<UserAuthorizationForms> userauth = new List<UserAuthorizationForms>();
            ViewBag.user = new SelectList(userauth, "UserID", "User");
            ViewBag.menus = new SelectList(userauth, "FormID", "FormName");
            //authorization.userauthorizationformsList.Insert(0, new UserAuthorizationForms());
            TempData["UTitle"] = "";
            TempData["UMessage"] = "";
            TempData["myModalDeleteMessageStyle"] = "";
            return View("UserAuthorization");
        }

        private void GetUserCategory()
        {
            var usercategory = _userAuthorizationService.GetUserCategory();
            ViewBag.usercategory = new SelectList(usercategory, "refid", "Name");
        }

        private void GetBranch()
        {
            var branch = _userAuthorizationService.GetBranch();
            ViewBag.userbranch = new SelectList(branch, "BID", "BranchName");
        }
        
        [HttpPost]
        public ActionResult GetUser(int UserCategoryId)
        {
            var user = _userAuthorizationService.GetUser(UserCategoryId);
            ViewBag.user = new SelectList(user, "UserID", "UserName");
            var forms = _userAuthorizationService.GetMenuList();
            ViewBag.menus = new SelectList(forms, "ID", "Name");
            var branch = _userAuthorizationService.GetBranch();
            ViewBag.userbranch = new SelectList(branch, "BID", "BranchName");
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
            var viewModel2 = branch.Select(x => new
            {
                BranchID = x.BID,
                BranchName = x.BranchName,
            });

            return Json(new { FirstList = viewModel, SecondList = viewModel1, ThirdList = viewModel2 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBackDatedVoucher(int parentid, int UserId, int BranchId)
        {
            var voucher = _userAuthorizationService.GetBackDatedVoucher(parentid, UserId, BranchId);
            var jsonresult = Json(voucher, JsonRequestBehavior.AllowGet);
            return jsonresult;
        }

        #region GetForms
        public ActionResult GetForms(int parentid, int UserId, int UserCategoryID, int BranchId)
        {
            var d = _userAuthorizationService.GetForms(parentid, UserId, UserCategoryID, BranchId);
            var jsonResult = Json(d, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion
        public ActionResult SaveUserAuthorization(int index, int UserID, int formid, int parentformid, int branchid, string backdatedvoucher, bool visible, bool edit, bool save, bool delete_id, bool view)
        {
            UserAuthorizationForms rm = new UserAuthorizationForms();
            rm.index = index;
            rm.FormID = formid;
            rm.UserID = UserID;
            rm.ParentID = parentformid;
            rm.BranchID = branchid;
            rm.BackDatedVoucher = backdatedvoucher;
            rm.isVisible = visible;
            rm.isDelete = delete_id;
            rm.isEdit = edit;
            rm.isSave = save;
            rm.isView = view;
            rm.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
            formid=_userAuthorizationService.InsertUserAuthorization_DetailsUserWise(rm);
            var jsonResult = Json(formid, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
    }
}