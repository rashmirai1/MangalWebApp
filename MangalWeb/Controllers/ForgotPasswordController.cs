using MangalWeb.Model.Entity;
using MangalWeb.Model.Security;
using MangalWeb.Model.Utilities;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class ForgotPasswordController : Controller
    {
        // GET: ForgotPassword
        UserService _userService = new UserService();
        private MangalDBNewEntities _context;

        public ForgotPasswordController()
        {
            _context = new MangalDBNewEntities();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewModel objViewModel)
        {
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");
            if (ModelState.IsValid)
            {
                string emailid = objViewModel.EmailId;
                string To = emailid, UserID, Password, SMTPPort, Host;
                string UserName = _context.UserDetails.Where(x => x.EmailId == objViewModel.EmailId).Select(x => x.UserName).FirstOrDefault();
                int? idofuser = _context.UserDetails.Where(x => x.EmailId == objViewModel.EmailId).Select(x => x.UserID).FirstOrDefault();
                if (idofuser != null && idofuser>0)
                {
                    var linkHref = Url.Action("ResetPassword", "ForgotPassword", new { email = emailid }, "http");

                    string subject = "Your changed password";

                    //string body = "Hello"+" "+UserName.ToUpper()+ ",@\n Received request for reset password from you. Please click on below button to reset password." + linkHref;

                    string body = "Hello" + " " + UserName.ToUpper() + ",<br/><br/>Received request for reset password from you. Please click on below button to reset password." +
                        "<br/><br/><a href="+linkHref +">Reset Password</a>";

                    _userService.AppSettings(out UserID, out Password, out SMTPPort, out Host);

                    _userService.SendEmail(UserID, subject, body, To, UserID, Password, SMTPPort, Host);
                }
                else
                {
                    ModelState.AddModelError("", "Email Id is not registered.");
                    return View(objViewModel);
                }

                ModelState.Remove("EmailId");
                int isverified = 0;
                _userService.SetUserFlag(emailid, isverified);

                return View("SendLinkSuccess", objViewModel);
            }
            return View(objViewModel);
        }

        [HttpGet]
        public ActionResult ResetPassword(string email)
        {
            int isverified = _context.UserDetails.Where(x => x.EmailId == email).Select(x => x.IsVerified??0).FirstOrDefault();
            //var emailid = _context.UserDetails.Where(x => x.EmpAddress.Contains(email)).FirstOrDefault();
            if (isverified != 0)
                return HttpNotFound();
            else
            {
                var data = new ForgotPasswordViewModel()
                {
                    EmailId = email
                };
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult ResetPassword(ForgotPasswordViewModel objViewModel)
        {
            if (ModelState.IsValid)
            {
                int isverified = 1;
                _userService.SetUserFlag(objViewModel.EmailId, isverified);

                int userid = _context.UserDetails.Where(x => x.EmailId == objViewModel.EmailId).Select(x => x.UserID).SingleOrDefault();
                string password = PasswordEncryptionDecryption.Encrypt(objViewModel.Password);
                _userService.UpdatePassword(userid, password);
                return View("ResetPasswordSuccess");
            }
            return View();
        }
    }
}