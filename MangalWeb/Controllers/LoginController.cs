﻿using MangalWeb.Model.Entity;
using MangalWeb.Model.Security;
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
            if (ModelState.IsValid)
            {
                login.Password = PasswordEncryptionDecryption.Encrypt(login.Password);

                ViewBag.BranchList = new SelectList(_branchService.GetAllBranchMasters(), "BID", "BranchName");
                ViewBag.FinancialYearList = new SelectList(_financialYearService.GetFinancialYearMasters(), "FinancialyearID", "Financialyear");

                var branch = _context.Mst_UserBranch.Where(b => b.BranchID == login.BranchId && b.UserDetail.UserName.ToLower() == login.UserName.ToLower()).FirstOrDefault();

                if(branch==null)
                {
                    ModelState.AddModelError("", "Selected branch is not assigned to you.");

                    return View(login);
                }

                var user = _context.UserDetails.Where(x => x.UserID== branch.UserID &&
                                                        x.Password == login.Password
                                                        ).FirstOrDefault();
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
                    Session["BranchId"] = login.BranchId;
                    Session["BranchCode"] = _context.tblCompanyBranchMasters.Where(x => x.BID == login.BranchId && x.Status == 1).Select(x => x.BranchCode).FirstOrDefault();
                    Session["FinancialYearId"] = login.FinancialYearId;
                    Session["CompanyId"] = 1;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Please check user name and password.");
                }
            }
            
            return View(login);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}