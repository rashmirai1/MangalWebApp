using MangalWeb.Model.Transaction;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MangalWeb.Controllers
{
    public class RequestFormController : BaseController
    {
        #region constryctor
        RequestFormService _requestFormService = new RequestFormService();
        DocumentUploadService _documentUploadService = new DocumentUploadService();
        #endregion

        #region BindList
        public void BindList()
        {
            ViewBag.DocumentTypeList = new SelectList(_documentUploadService.GetDocumentTypeList(), "Id", "Name");
            ViewBag.DocumentList = new SelectList(_documentUploadService.GetDocumentMasterList(), "DocumentID", "DocumentName");
            ViewBag.PinCodeList = new SelectList(_requestFormService.GetAllPincodes(), "Pc_Id", "Pc_Desc");
        }
        #endregion

        #region RequestForm
        // GET: RequestForm
        public ActionResult RequestForm()
        {
            ButtonVisiblity("Index");
            BindList();
            Session["documentsub"] = null;
            RequestFormViewModel kycVM = new RequestFormViewModel();
            KYCAddressesVM addressvm = new KYCAddressesVM();
            kycVM.Trans_KYCAddresses.Add(addressvm);
            kycVM.TransactionId = _requestFormService.GetMaxTransactionId();
            kycVM.KYCDate = DateTime.Now.ToShortDateString();
            return View(kycVM);
        }
        #endregion

        #region CreateEdit
        public JsonResult CreateEdit(RequestFormViewModel model)
        {
            try
            {
                if (model.CustomerID != null)
                {
                    model.DocumentUploadList = (List<DocumentUploadDetailsVM>)Session["documentsub"];
                    _requestFormService.SaveRecord(model);
                }
                return Json(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region AddDocument
        [HttpPost]
        public JsonResult AddDocument()
        {
            int Id = Convert.ToInt32(Request.Form["Id"]);
            int DocumentTypeId = Convert.ToInt32(Request.Form["DocumentTypeId"]);
            int DocumentId = Convert.ToInt32(Request.Form["DocumentId"]);
            DateTime? ExpiryDate = null;
            if (Request.Form["ExpiryDate"].ToString() != "")
            {
                ExpiryDate = Convert.ToDateTime(Request.Form["ExpiryDate"]);
            }
            string pFileName = "";
            string pFileExtension = "";
            HttpPostedFileBase postedFile =Request.Files[0];
            Stream fs = postedFile.InputStream;
            pFileName = postedFile.FileName;
            pFileExtension = postedFile.ContentType;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bytes = br.ReadBytes(postedFile.ContentLength);
            //base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            DocumentUploadDetailsVM docupload = null;
            var sessionlist = (List<DocumentUploadDetailsVM>)Session["documentsub"];
            if (sessionlist == null)
            {
                sessionlist = new List<DocumentUploadDetailsVM>();
            }
            docupload = new DocumentUploadDetailsVM();
            docupload.ID = Id;
            docupload.DocumentTypeId = DocumentTypeId;
            docupload.DocumentId = DocumentId;
            docupload.ExpiryDate = ExpiryDate;
            docupload.UploadDocName = bytes;
            docupload.FileName = pFileName;
            docupload.FileExtension = pFileExtension;
            docupload.SpecifyOther = Request.Form["SpecifyOther"];
            docupload.NameonDocument = Request.Form["NameonDocument"];
            sessionlist.Add(docupload);
            Session["documentsub"] = sessionlist;
            return Json(docupload, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Remove
        public ActionResult Remove(int id)
        {
            var list = (List<DocumentUploadDetailsVM>)Session["documentsub"];
            list.Remove(list.Where(x => x.ID == id).FirstOrDefault());
            Session["documentsub"] = list;
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region SetDocumentSession
        public JsonResult SetDocumentSession()
        {
            Session["documentsub"] = null;
            return Json(JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetCustomerDetails
        public ActionResult GetCustomerDetails()
        {
            return PartialView("_CustomerDetails", _requestFormService.GetKYCList());
        }
        #endregion GetCustomerDetails

        #region GetPincodeDetails
        public JsonResult GetPincodeDetails(int id)
        {
            var branch = _requestFormService.GetPincodDetails(id);
            return Json(branch, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetRequestFormById

        //[HttpPost]
        public ActionResult GetRequestFormById(int Id)
        {
            ButtonVisiblity("Edit");
            BindList();
            var model = _requestFormService.GetRequestFormById(Id);
            model.TransactionId = _requestFormService.GetMaxTransactionId();
            //return View("RequestForm", model);
            Session["documentsub"] = model.DocumentUploadList;
            return Json(model,JsonRequestBehavior.AllowGet);
        }
        #endregion GetRequestFormById
    }
}