using MangalWeb.Model.Transaction;
using MangalWeb.Service.Service;
using System;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class ResidenceVerificationController : BaseController
    {
        ResidenceVerificationService _residenceVerificationService = new ResidenceVerificationService();
        SchemeService _schemeService = new SchemeService();
        KYCService _kycService = new KYCService();

        #region ResidenceVerification
        // GET: ResidenceVerification
        public ActionResult Index()
        {
            ButtonVisiblity("Index");
            var model = new ResidenceVerificationVM();
            ViewBag.RMList = new SelectList(_residenceVerificationService.GetAllRMByBranch(), "UserName", "UserName");
            ViewBag.PinCodeList = new SelectList(_kycService.GetAllPincodes(), "Pc_Id", "Pc_Desc");
            ViewBag.PinCodes = _kycService.GetAllPincodes();
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
                if (objViewModel.Id == 0)
                {
                    InsertData(objViewModel);
                }
                return Json(objViewModel);
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
            return View("Index", model);
        }
        #endregion GetCustomerById

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
    }
}