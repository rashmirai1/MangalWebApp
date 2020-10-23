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
    public class KYCController : BaseController
    {
        KYCService _kycService = new KYCService();
        DocumentUploadService _documentUploadService = new DocumentUploadService();

        #region Index
        public ActionResult Index()
        {
            ButtonVisiblity("Index");
            ViewBag.SourceList = new SelectList(_kycService.GetSourceOfApplicationList(), "Soa_Id", "Soa_Name");
            ViewBag.PinCodeList = new SelectList(_kycService.GetAllPincodes(), "Pc_Id", "Pc_Desc");
            //ViewBag.PinCodes = _kycService.GetAllPincodes();
            ViewBag.DocumentTypeList = new SelectList(_documentUploadService.GetDocumentTypeList(), "Id", "Name");
            ViewBag.DocumentList = new SelectList(_documentUploadService.GetDocumentMasterList(), "DocumentID", "DocumentName");
            KYCBasicDetailsVM kycVM = new KYCBasicDetailsVM();
            KYCAddressesVM addressvm = new KYCAddressesVM();
            kycVM.Trans_KYCAddresses.Add(addressvm);
            kycVM.ApplicationNo = _kycService.GenerateApplicationNo();
            int appno = Convert.ToInt32(kycVM.ApplicationNo);
            Random rand = new Random(100);
            int cid = rand.Next(000000000, 999999999) + appno;
            kycVM.CustomerID = "C" + cid.ToString();
            kycVM.AppliedDate = DateTime.Now.ToShortDateString();
            Session["docsub"] = null;
            Session["ApplicantImage"]=null;
            Session["ApplicantImageName"] = null;
            Session["ApplicantImageContentType"] = null;
            return View(kycVM);
        }
        #endregion

        #region CreateEdit
        /// <summary>
        /// Save KYC
        /// </summary>
        /// <param name="model"></param>
        /// <param name="uploadFile"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CreateEdit(KYCBasicDetailsVM model)
        {
            try
            {
                if (Session["ApplicantImage"] != null)
                {
                    model.ApplicantPhoto = (byte[])Session["ApplicantImage"];
                    model.ImageName = Session["ApplicantImageName"].ToString();
                    model.ContentType = Session["ApplicantImageContentType"].ToString();
                }

                model.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
                model.UpdatedBy = Convert.ToInt32(Session["UserLoginId"]);
                model.FYID = Convert.ToInt32(Session["FinancialYearId"]);
                model.BranchID = Convert.ToInt32(Session["BranchId"]);
                model.CmpID = Convert.ToInt32(Session["CompanyId"]);

                model.DocumentUploadList = (List<DocumentUploadDetailsVM>)Session["docsub"];
                if (model.CustomerID != null)
                {
                    _kycService.SaveRecord(model);
                }
                return Json(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region doesPanExist
        /// <summary>
        /// Check if PAN already exist
        /// </summary>
        /// <param name="PanNo"></param>
        /// <returns></returns>
        public JsonResult doesPanExist(string PanNo)
        {
            try
            {
                var model = _kycService.doesPanExist(PanNo);
                var file = _kycService.GetApplicantImage(Convert.ToInt32(model.KYCID));
                Session["docsub"] = null;
                if (model.DocumentUploadList != null && model.DocumentUploadList.Count > 0)
                {
                    Session["docsub"] = model.DocumentUploadList;
                }
                if (model.ImageName != null)
                {
                    Session["ApplicantImage"] = file.AppPhoto;
                    Session["ApplicantImageName"] = model.ImageName;
                    Session["ApplicantImageContentType"] = file.ContentType;
                }
                return Json(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region doesAdharExist
        /// <summary>
        /// check if adhar already exist
        /// </summary>
        /// <param name="AdharNo"></param>
        /// <returns></returns>
        public JsonResult doesAdharExist(string AdharNo)
        {
            try
            {
                var model = _kycService.doesAdharExist(AdharNo);
                var file = _kycService.GetApplicantImage(Convert.ToInt32(model.KYCID));
                Session["docsub"] = null;
                if (model.DocumentUploadList != null && model.DocumentUploadList.Count > 0)
                {
                    Session["docsub"] = model.DocumentUploadList;
                }
                if (model.ImageName != null)
                {
                    Session["ApplicantImage"] = file.AppPhoto;
                    Session["ApplicantImageName"] = model.ImageName;
                    Session["ApplicantImageContentType"] = file.ContentType;
                }
                return Json(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region Download KYC Image
        /// <summary>
        /// download kyc image
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FileResult Download(int id)
        {
            var file = _kycService.GetImageById(id);
            return File(file.AppPhoto, file.ContentType);
        }
        #endregion

        #region SendOtp
        /// <summary>
        /// send otp to the mobile number
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public JsonResult SendOtp(string mobile, string customerId)
        {
            try
            {
                var response = _kycService.SendOtp(mobile, customerId);
                return Json(response);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region VerifyOtp
        /// <summary>
        /// verify otp code
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="customerId"></param>
        /// <param name="otp"></param>
        /// <returns></returns>
        public JsonResult VerifyOtp(string mobile, string customerId, string otp)
        {
            try
            {
                var response = _kycService.VerifyMobileNumber(mobile, customerId, otp);
                return Json(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region FillAddressByPinCode
        /// <summary>
        /// fill state, city, area, zone by pincode id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult FillAddressByPinCode(int id)
        {
            try
            {
                var response = _kycService.FillAddressByPinCode(id);
                return Json(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region Insert Document Data
        public bool InsertDocumentData(List<DocumentUploadDetailsVM> lstDocUploadTrn)
        {
            bool retVal = false;
            try
            {
                lstDocUploadTrn = (List<DocumentUploadDetailsVM>)Session["docsub"];
                //_kycService.SaveDocument(lstDocUploadTrn);
                retVal = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
        #endregion Insert Data

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
            var sessionlist = (List<DocumentUploadDetailsVM>)Session["docsub"];
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
            Session["docsub"] = sessionlist;
            return Json(docupload, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Remove
        public ActionResult Remove(int id)
        {
            var list = (List<DocumentUploadDetailsVM>)Session["docsub"];
            list.Remove(list.Where(x => x.ID == id).FirstOrDefault());
            Session["docsub"] = list;
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetSourceType
        public JsonResult GetSourceType(int id)
        {
            var str = _kycService.GetSourceType(id);
            return Json(str, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region UploadApplicantPhoto
        [HttpPost]
        public JsonResult UploadApplicantPhoto()
        {
            HttpFileCollectionBase files = Request.Files;
            HttpPostedFileBase postedFile = files[0];
            Stream fs = postedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bytes = br.ReadBytes(postedFile.ContentLength);
            Session["ApplicantImage"] = bytes;
            Session["ApplicantImageName"] = postedFile.FileName;
            Session["ApplicantImageContentType"] = postedFile.ContentType;
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}