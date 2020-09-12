using MangalWeb.Model.Masters;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class DocumentController : BaseController
    {
        DocumentService _documentService = new DocumentService();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateEdit(DocumentViewModel document)
        {
            try
            {
                _documentService.SaveUpdateRecord(document);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Json(document);
        }

        public ActionResult GetDocumentById(int ID)
        {
            string operation = Session["Operation"].ToString();
            ButtonVisiblity(operation);
            var document = new DocumentViewModel();
            var tbldocument = _documentService.GetDocumentById(ID);
            if(tbldocument!=null)
            {
                document = _documentService.SetDataOnEdit(tbldocument);
            }
            document.operation = operation;
            ViewBag.DocumentTypeList = new SelectList(_documentService.GetDcoumentTypeList(), "Id", "Name");
            return View("Document", document);
        }

        // GETDelete/5
        public ActionResult Delete(int id)
        {
            _documentService.DeleteRecord(id);
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckRecordonEditMode(int Id)
        {
            string data = "";
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult doesDocumentNameExist(string DocumentName,int Id)
        {
            var data = _documentService.CheckDocumentNameExists(DocumentName,Id);
            var result = "";
            //Check if document name already exists
            if (data != null)
            {
                if (DocumentName.ToLower() == data.ToLower().ToString())
                {
                    result = "Document Name Already Exists";
                }
                else
                {
                    result = "";
                }
            }
            return Json(result);
        }

        public ActionResult Document()
        {
            ButtonVisiblity("Index");
            var model = new DocumentViewModel();
            ViewBag.DocumentTypeList = new SelectList(_documentService.GetDcoumentTypeList(), "Id", "Name");
            return View(model);
        }

        public ActionResult GetDocumentTable(string Operation)
        {
            Session["Operation"] = Operation;
            var list = _documentService.SetDataofModalList();
            return PartialView("_DocumentList", list);
        }
    }
}