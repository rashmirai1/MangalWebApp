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
    public class DocumentUploadController : BaseController
    {
        DocumentUploadService _documentUploadService = new DocumentUploadService();

        #region DocumentUpload
        // GET: DocumentUpload
        public ActionResult DocumentUpload()
        {
            ButtonVisiblity("Index");
            Session["sub"] = null;
            var model = _documentUploadService.GetAllDocumentUpload();
            ViewBag.DocumentTypeList = new SelectList(_documentUploadService.GetDocumentTypeList(), "Id", "Name");
            ViewBag.DocumentList = new SelectList(_documentUploadService.GetDocumentMasterList(), "DocumentID", "DocumentName");
            return View(model);
        }
        #endregion

        #region GetCustomerDetails

        public ActionResult GetCustomerDetails()
        {
            return PartialView("_CustomerDetails", _documentUploadService.GetKYCList());
        }

        #endregion GetCustomerDetails

        #region GetCustomerById

        public ActionResult GetCustomerById(int KycId)
        {
            try
            {
                ButtonVisiblity("Index");
                ViewBag.DocumentTypeList = new SelectList(_documentUploadService.GetDocumentTypeList(), "Id", "Name");
                ViewBag.DocumentList = new SelectList(_documentUploadService.GetDocumentMasterList(), "DocumentID", "DocumentName");
                var model = _documentUploadService.GetCustomerById(KycId);
                return View("DocumentUpload", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion GetCustomerById

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
                else
                {
                    if (InsertData(objViewModel))
                    {
                        return Json(2, JsonRequestBehavior.AllowGet);
                    }
                }
                objViewModel.DocumentUploadList = new List<DocumentUploadDetailsVM>();
                ViewBag.DocumentTypeList = new SelectList(_documentUploadService.GetDocumentTypeList(), "Id", "Name");
                ViewBag.DocumentList = new SelectList(_documentUploadService.GetDocumentMasterList(), "DocumentID", "DocumentName");
                return View("DocumentUpload", objViewModel);
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
                DocUploadViewModel.DocumentUploadList = (List<DocumentUploadDetailsVM>)Session["sub"];
                _documentUploadService.SaveRecord(DocUploadViewModel);
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
            int Id =Convert.ToInt32(Request.Form["Id"]);
            int DocumentTypeId =Convert.ToInt32(Request.Form["DocumentTypeId"]);
            int DocumentId =Convert.ToInt32(Request.Form["DocumentId"]);
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
            var sessionlist =(List<DocumentUploadDetailsVM>)Session["sub"];
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
        #endregion

        #region Remove
        public ActionResult Remove(int id)
        {
            var list = (List<DocumentUploadDetailsVM>)Session["sub"];
            list.Remove(list.Where(x => x.ID == id).FirstOrDefault());
            Session["sub"] = list;
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

        #region GetDcoument
        public JsonResult GetDcoument(int id)
        {
            var data = new SelectList(_documentUploadService.GetDocumentMasterById(id), "DocumentID", "DocumentName");
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetExpiryDate
        public JsonResult GetExpiryDate(int id)
        {
            bool data = _documentUploadService.GetExpiryDate(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetDocUploadTable
        public ActionResult GetDocUploadTable(string Operation)
        {
            Session["Operation"] = Operation;
            ButtonVisiblity(Operation);
            var list = _documentUploadService.SetModalList();
            return PartialView("_DocumentUploadList", list);
        }
        #endregion

        #region GetDocUploadDetailsById

        public ActionResult GetDocUploadDetailsById(int ID)
        {
            try
            {
                string operation = String.Empty;
                if (Session["Operation"] != null)
                {
                    operation = Session["Operation"].ToString();
                }
                else
                {
                    operation = "Edit";
                }
                //get document upload table
                var documentUploadViewModel = _documentUploadService.GetUploadDocumentById(ID);
                documentUploadViewModel.operation = operation;
                Session["sub"] = documentUploadViewModel.DocumentUploadList;
                ViewBag.DocumentTypeList = new SelectList(_documentUploadService.GetDocumentTypeList(), "Id", "Name");
                ViewBag.DocumentList = new SelectList(_documentUploadService.GetDocumentMasterList(), "DocumentID", "DocumentName");
                return View("DocumentUpload", documentUploadViewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion GetDocUploadDetailsById

        #region Delete

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            try
            {
                _documentUploadService.DeleteRecord(id);
                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Delete
    }
}