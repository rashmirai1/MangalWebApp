using MangalWeb.Model.Transaction;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MangalWeb.Controllers
{
    public class RequestFormController : BaseController
    {
        RequestFormService _requestFormService = new RequestFormService();
        DocumentUploadService _documentUploadService = new DocumentUploadService();

        // GET: RequestForm
        public ActionResult RequestForm()
        {
            ButtonVisiblity("Index");
            ViewBag.SourceList = new SelectList(_requestFormService.GetSourceOfApplicationList(), "Soa_Name", "Soa_Name");
            ViewBag.PinCodeList = new SelectList(_requestFormService.GetAllPincodes(), "Pc_Id", "Pc_Desc");
            ViewBag.DocumentTypeList = new SelectList(_documentUploadService.GetDocumentTypeList(), "Id", "Name");
            ViewBag.DocumentList = new SelectList(_documentUploadService.GetDocumentMasterList(), "DocumentID", "DocumentName");
            RequestFormViewModel kycVM = new RequestFormViewModel();
            kycVM.TransactionId = _requestFormService.GetMaxTransactionId();
            return View(kycVM);
        }

        public JsonResult CreateEdit(RequestFormViewModel model)
        {
            try
            {
                if (model.CustomerID != null)
                {
                    _requestFormService.SaveRecord(model);
                }
                return Json(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region GetCustomerDetails
        public ActionResult GetCustomerDetails()
        {
            return PartialView("_CustomerDetails", _requestFormService.GetKYCList());
        }
        #endregion GetCustomerDetails

        #region GetDoumentUploadById

        //[HttpPost]
        public ActionResult GetDoumentUploadById(int Id)
        {
            ButtonVisiblity("Edit");
            var model = _requestFormService.GetDoumentUploadById(Id);
            return View("RequestForm", model);
        }
        #endregion GetDoumentUploadById
    }
}