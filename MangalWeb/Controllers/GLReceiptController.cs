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
    public class GLReceiptController : Controller
    {
        DocumentUploadService _documentUploadService = new DocumentUploadService();
        // GET: GLReceipt
        public ActionResult Index()
        {
            ViewBag.narration = new SelectList(_documentUploadService.GetDocumentTypeList(), "Id", "Name"); 
            return View();
        }
    }
}