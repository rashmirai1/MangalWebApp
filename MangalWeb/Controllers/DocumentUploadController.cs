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

        [HttpPost]
        public JsonResult AddDocument()
        {
            string Id = Request.Form["Id"];
            string DocumentTypeId = Request.Form["DocumentTypeId"];
            string DocumentId = Request.Form["DocumentId"];
            string ExpiryDate = Request.Form["ExpiryDate"];
            string base64String = "";
            HttpFileCollectionBase files = Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                Stream fs = file.InputStream;
                BinaryReader br = new BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            }
            DocumentUploadDetailsVM docupload = null;
            var sessionlist = (List<DocumentUploadDetailsVM>)Session["sub"];
            if (sessionlist == null)
            {
                sessionlist = new List<DocumentUploadDetailsVM>();
            }
            docupload = new DocumentUploadDetailsVM();
            sessionlist.Add(docupload);
            docupload.ID = Convert.ToInt32(Id);
            docupload.DocumentTypeId = Convert.ToInt32(DocumentTypeId);
            docupload.DocumentId = Convert.ToInt32(DocumentId);
            docupload.ExpiryDate =Convert.ToDateTime(ExpiryDate);
            docupload.UploadDocName = base64String;
            Session["sub"] = sessionlist;
            return Json(1, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Remove(int id)
        {
            var list = (List<DocumentUploadDetailsVM>)Session["sub"];
            list.Remove(list.Where(x => x.ID == id).FirstOrDefault());
            Session["sub"] = list;
            return Json(1, JsonRequestBehavior.AllowGet);
        }

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

        #region GetDcoument
        public JsonResult GetDcoument(int id)
        {
            var data = new SelectList(_documentUploadService.GetDocumentMasterById(id),"DocumentID", "DocumentName");
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult GetDocUploadTable(string Operation)
        {
            Session["Operation"] = Operation;
            ButtonVisiblity(Operation);
            var list = _documentUploadService.SetModalList();
            return PartialView("_DocumentUploadList", list);
        }

        #region GetDocUploadDetailsById

        public ActionResult GetDocUploadDetailsById(int ID)
        {
            try
            {
                DocumentUploadViewModel documentUploadViewModel = new DocumentUploadViewModel();
                string operation = Session["Operation"].ToString();
                //get document upload table
                var productRatevm = _documentUploadService.GetUploadDocumentById(ID);
                documentUploadViewModel.operation = operation;
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