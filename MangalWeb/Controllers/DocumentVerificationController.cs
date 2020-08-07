using MangalWeb.Model.Transaction;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MangalWeb.Controllers
{
    public class DocumentVerificationController : BaseController
    {
        DocumentVerificationService _documentVerificationService = new DocumentVerificationService();

        #region DocumentVerification
        // GET: DocumentVerification
        public ActionResult DocumentVerification()
        {
            ButtonVisiblity("Index");
            var model = new DocumentUploadViewModel();
            model.DocumentUploadList = new List<DocumentUploadDetailsVM>();
            ViewBag.StatusList = new SelectList(StatusListMethod(), "Value", "Text");
            return View(model);
        }
        #endregion

        #region Insert
        [HttpPost]
        public ActionResult Insert(DocumentUploadViewModel objViewModel)
        {
            try
            {
                ModelState.Remove("Id");
                if (objViewModel.ID == 0)
                {
                    if (InsertData(objViewModel))
                    {
                        return Json(1, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(3, JsonRequestBehavior.AllowGet);
                    }
                }
                objViewModel.DocumentUploadList = new List<DocumentUploadDetailsVM>();
                ViewBag.StatusList = new SelectList(StatusListMethod(), "Value", "Text");
                return View("DocumentVerification", objViewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Insert

        #region Insert Data

        public bool InsertData(DocumentUploadViewModel DocUploadViewModel)
        {
            bool retVal = false;
            DocUploadViewModel.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
            DocUploadViewModel.UpdatedBy = Convert.ToInt32(Session["UserLoginId"]);
            try
            {
                _documentVerificationService.SaveUpdateRecord(DocUploadViewModel);
                retVal = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        #endregion Insert Data

        #region GetCustomerDetails
        public ActionResult GetCustomerDetails()
        {
            return PartialView("_CustomerDetails", _documentVerificationService.GetDocumentUploadList());
        }
        #endregion GetCustomerDetails

        #region GetDoumentUploadById
        public ActionResult GetDoumentUploadById(int Id)
        {
            ButtonVisiblity("Edit");
            var model = _documentVerificationService.GetDoumentUploadById(Id);
            ViewBag.StatusList = new SelectList(StatusListMethod(), "Value", "Text");
            return View("DocumentVerification",model);
        }
        #endregion GetDoumentUploadById

        public List<ListItem> StatusListMethod()
        {
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem { Text = "Pending", Value = "Pending" });
            list.Add(new ListItem { Text = "Verified", Value = "Verified" });
            list.Add(new ListItem { Text = "Rejected", Value = "Rejected" });
            return list;
        }
    }
}