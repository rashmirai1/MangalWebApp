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
    public class ResidenceVerificationController : BaseController
    {
        #region Constructor
        ResidenceVerificationService _residenceVerificationService = new ResidenceVerificationService();
        DocumentUploadService _documentUploadService = new DocumentUploadService();
        SchemeService _schemeService = new SchemeService();
        KYCService _kycService = new KYCService();
        #endregion

        #region ResidenceVerification
        // GET: ResidenceVerification
        public ActionResult Index()
        {
            ButtonVisiblity("Index");
            var model = new ResidenceVerificationVM();
            ViewBag.RMList = new SelectList(_residenceVerificationService.GetAllRMByBranch(), "UserName", "UserName");
            ViewBag.PinCodeList = new SelectList(_kycService.GetAllPincodes(), "Pc_Id", "Pc_Desc");
            int id = _residenceVerificationService.GenerateApplicationNo();
            Random rand = new Random(100);
            int cid = rand.Next(000000000, 999999999) + id;
            model.TransactionId = "RV" + cid.ToString();
            model.AppliedDate = DateTime.Now.ToShortDateString();
            ViewBag.DocumentTypeList = new SelectList(_documentUploadService.GetDocumentTypeList(), "Id", "Name");
            ViewBag.DocumentList = new SelectList(_documentUploadService.GetDocumentMasterList(), "DocumentID", "DocumentName");
            Session["resdocupload"] = null;
            return View(model);
        }
        #endregion

        #region Insert
        [HttpPost]
        public ActionResult Insert(ResidenceVerificationVM objViewModel)
        {
            try
            {
                ModelState.Remove("Id");
                bool check = false;
                if (objViewModel.Id == 0)
                {
                    check=InsertData(objViewModel);
                }
                ViewBag.DocumentTypeList = new SelectList(_documentUploadService.GetDocumentTypeList(), "Id", "Name");
                ViewBag.DocumentList = new SelectList(_documentUploadService.GetDocumentMasterList(), "DocumentID", "DocumentName");
                ViewBag.RMList = new SelectList(_residenceVerificationService.GetAllRMByBranch(), "UserName", "UserName");
                ViewBag.PinCodeList = new SelectList(_kycService.GetAllPincodes(), "Pc_Id", "Pc_Desc");
                ViewBag.PinCodes = _kycService.GetAllPincodes();

                return Json(check);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Insert

        #region Insert Data

        public bool InsertData(ResidenceVerificationVM model)
        {
            bool retVal = false;
            model.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
            try
            {
                model.DocumentUploadList = (List<DocumentUploadDetailsVM>)Session["resdocupload"];
                _residenceVerificationService.SaveUpdateRecord(model);
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
            return PartialView("_CustomerDetails", _residenceVerificationService.GetCustomerList());
        }
        #endregion GetCustomerDetails

        #region GetCustomerById
        public ActionResult GetCustomerById(int Id)
        {
            ButtonVisiblity("Edit");
            ViewBag.RMList = new SelectList(_residenceVerificationService.GetAllRMByBranch(), "UserID", "UserName");
            ViewBag.PinCodeList = new SelectList(_kycService.GetAllPincodes(), "Pc_Id", "Pc_Desc");
            ViewBag.PinCodes = _kycService.GetAllPincodes();
            var model = _residenceVerificationService.GetCustomerById(Id);
            int id = _residenceVerificationService.GenerateApplicationNo();
            Random rand = new Random(100);
            int cid = rand.Next(000000000, 999999999) + id;
            model.TransactionId = "RV" + cid.ToString();
            model.AppliedDate = DateTime.Now.ToShortDateString();
            ViewBag.DocumentTypeList = new SelectList(_documentUploadService.GetDocumentTypeList(), "Id", "Name");
            ViewBag.DocumentList = new SelectList(_documentUploadService.GetDocumentMasterList(), "DocumentID", "DocumentName");
            Session["resdocupload"] = model.DocumentUploadList;
            return View("Index", model);
        }
        #endregion GetCustomerById

        #region GetDocumentID
        /// <summary>
        /// Get Document Id by KYCID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetDocumentID(int id)
        {
            try
            {
                var result = _residenceVerificationService.GetDocumentID(id);
               
                return Json(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region FillEmployeeDetailsById
        public JsonResult FillEmployeeDetailsById(int id)
        {
            try
            {
                var result = _residenceVerificationService.FillEmployeeDetailsById(id);
                return Json(result);
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
            HttpFileCollectionBase files = Request.Files;
            string pFileName = "";
            string pFileExtension = "";
            HttpPostedFileBase postedFile = files[0];
            Stream fs = postedFile.InputStream;
            pFileName = postedFile.FileName;
            pFileExtension = postedFile.ContentType;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bytes = br.ReadBytes(postedFile.ContentLength);
            //base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            DocumentUploadDetailsVM docupload = null;
            var sessionlist = (List<DocumentUploadDetailsVM>)Session["resdocupload"];
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
            Session["resdocupload"] = sessionlist;
            return Json(docupload, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Remove
        public ActionResult Remove(int id)
        {
            var list = (List<DocumentUploadDetailsVM>)Session["resdocupload"];
            list.Remove(list.Where(x => x.ID == id).FirstOrDefault());
            Session["resdocupload"] = list;
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Download
        public FileResult Download(int id)
        {
            var file = _documentUploadService.GetUploadDocuments(id);
            return File(file.UploadFile, file.ContentType);
        }
        #endregion
    }
}