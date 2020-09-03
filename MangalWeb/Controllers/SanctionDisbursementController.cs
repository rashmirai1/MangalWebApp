using MangalWeb.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Nitesh Tiwari 28.08.2020 4.00 pm
namespace MangalWeb.Controllers
{
    public class SanctionDisbursementController : BaseController
    {
        // GET: SanctionDisbursement
        public ActionResult SanctionDisbursement()
        {
            ButtonVisiblity("Index");
            var model = new SanctionDisbursementVM();
            return View(model);
        }
    }
}