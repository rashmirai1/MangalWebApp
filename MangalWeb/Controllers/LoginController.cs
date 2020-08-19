using MangalWeb.Model.Entity;
using MangalWeb.Model.Utilities;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MangalWeb.Controllers
{
    public class LoginController : Controller
    {
        BranchService _branchService = new BranchService();
        FinancialYearService _financialYearService = new FinancialYearService();
        MangalDBNewEntities _context = new MangalDBNewEntities();

        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.BranchList = new SelectList(_branchService.GetAllBranchMasters(), "BID", "BranchName");
            ViewBag.FinancialYearList = new SelectList(_financialYearService.GetFinancialYearMasters(), "FinancialyearID", "Financialyear");
            return View();
        }

        [AllowAnonymous]
        [HttpPost, ValidateInput(false)]
        public ActionResult Login(LoginViewModel login)
        {
            var user = _context.UserDetails.Where(x => x.UserName.ToLower() == login.UserName.ToLower() &&
                                                    x.Password.ToLower() == login.Password.ToLower() &&
                                                    x.BranchId == login.BranchId &&
                                                    x.FinancialYearId == login.FinancialYearId
                                                    ).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    FormsAuthentication.RedirectFromLoginPage(login.UserName, false);
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, login.UserName, DateTime.Now, DateTime.Now.AddMinutes(1051897), true, string.Empty, FormsAuthentication.FormsCookiePath);
                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                    if (ticket.IsPersistent)
                    {
                        cookie.Expires = ticket.Expiration;
                    }
                    Response.Cookies.Add(cookie);
                    //get User Id on Login so that we will update this id for every page on insert ,update and delete entry
                    Session["UserLoginId"] = user.UserID;
                    Session["UserName"] = user.UserName;
                    Session["UserCategory"] = user.UserTypeID;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Please enter proper data.");
                }
            }
            ViewBag.BranchList = new SelectList(_branchService.GetAllBranchMasters(), "BID", "BranchName");
            ViewBag.FinancialYearList = new SelectList(_financialYearService.GetFinancialYearMasters(), "FinancialyearID", "Financialyear");
            return View(login);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}