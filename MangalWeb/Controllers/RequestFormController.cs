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
        RequestFormService _requestFormService = new RequestFormService();
        DocumentUploadService _documentUploadService = new DocumentUploadService();


        public void BindList()
        {
            ViewBag.DocumentTypeList = new SelectList(_documentUploadService.GetDocumentTypeList(), "Id", "Name");
            ViewBag.DocumentList = new SelectList(_documentUploadService.GetDocumentMasterList(), "DocumentID", "DocumentName");
            ViewBag.PinCodeList = new SelectList(_requestFormService.GetAllPincodes(), "Pc_Id", "Pc_Desc");
            ViewBag.PinCodes = _requestFormService.GetAllPincodes();
        }

        // GET: RequestForm
        public ActionResult RequestForm()
        {
            ButtonVisiblity("Index");
            BindList();
            Session["sub"] = null;
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
                    model.DocumentUploadList = (List<DocumentUploadDetailsVM>)Session["sub"];
                    _requestFormService.SaveRecord(model);
                }
                return Json(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


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
            var sessionlist = (List<DocumentUploadDetailsVM>)Session["sub"];
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
            Session["sub"] = sessionlist;
            return Json(docupload, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Remove(int id)
        {
            var list = (List<DocumentUploadDetailsVM>)Session["sub"];
            list.Remove(list.Where(x => x.ID == id).FirstOrDefault());
            Session["sub"] = list;
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        #region GetCustomerDetails
        public ActionResult GetCustomerDetails()
        {
            return PartialView("_CustomerDetails", _requestFormService.GetKYCList());
        }
        #endregion GetCustomerDetails

        public JsonResult GetPincodeDetails(int id)
        {
            var branch = _requestFormService.GetPincodDetails(id);
            return Json(branch, JsonRequestBehavior.AllowGet);
        }

        #region GetRequestFormById

        //[HttpPost]
        public ActionResult GetRequestFormById(int Id)
        {
            ButtonVisiblity("Edit");
            BindList();
            var model = _requestFormService.GetRequestFormById(Id);
            model.TransactionId = _requestFormService.GetMaxTransactionId();
            //return View("RequestForm", model);
            Session["sub"] = model.DocumentUploadList;
            return Json(model,JsonRequestBehavior.AllowGet);
        }
        #endregion GetRequestFormById
    }
}