using MangalWeb.Model.Transaction;
using MangalWeb.Models.Security;
using MangalWeb.Service.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MangalWeb.Controllers
{
    public class PreSanctionController : BaseController
    {
        PreSanctionService _preSanctionService = new PreSanctionService();
        SchemeService _schemeService = new SchemeService();
        ProductRateService _productrateService = new ProductRateService();

       
        // GET: PreSanction
        public ActionResult Index()
        {
            ButtonVisiblity("Index");
            var model = new TGLPreSanctionVM();
            model.RMList = new SelectList(_preSanctionService.GetAllRMByBranch(), "UserID", "UserName");
            model.LoanPurposes = new SelectList(_preSanctionService.GetLoanPurposes(), "LoanPuposeID", "LoanPupose");
            model.Schemes = new SelectList(_schemeService.GetAllSchemeMasters(), "SID", "SchemeName");
            model.Products = new SelectList(_productrateService.GetProductList(), "Id", "Name");
            return View(model);
        }
      
        [HttpPost]
        public ActionResult SavePreSanction(TGLPreSanctionVM model)
        {
           

            try
            {
                model.CreatedBy = UserInfo.UserID;
                model.FYID = UserInfo.FinancialYearId;
                model.CMPID = UserInfo.CompanyId;
                model.BranchID = UserInfo.BranchId;

                Dictionary<string, string> errors;

                var preSanction = _preSanctionService.SavePreSanction(model, out errors);
                if (errors != null && errors.Count > 0)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(errors);
                }
                return Json(preSanction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        #region GetCustomerDetails
        public ActionResult GetCustomerDetails()
        {
            return PartialView("_CustomerDetails", _preSanctionService.GetCustomerList());
        }
        #endregion GetCustomerDetails

        #region GetCustomerById
        public ActionResult GetCustomerById(int Id)
        {
            ButtonVisiblity("Edit");
          
            var model = _preSanctionService.GetCustomerById(Id);
            model.RMList = new SelectList(_preSanctionService.GetAllRMByBranch(), "UserID", "UserName");
            model.LoanPurposes = new SelectList(_preSanctionService.GetLoanPurposes(), "LoanPuposeID", "LoanPupose");
            model.Schemes = new SelectList(_schemeService.GetAllSchemeMasters(), "SID", "SchemeName");
            model.Products = new SelectList(_productrateService.GetProductList(), "Id", "Name");
            return View("Index", model);
        }
        #endregion GetCustomerById
        

        /// <summary>
        /// fill scheme details 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult FillSchemeDetailsById(int id)
        {
            try
            {
                var result = _schemeService.GetSchemeMasterById(id);
                SchemeDetailsVM schemeDetailsVM = new SchemeDetailsVM();
                schemeDetailsVM.Ltv = result.Ltv;
                schemeDetailsVM.MaxLoanAmt = result.MaxLoanAmt;
                schemeDetailsVM.Tenure = result.Tenure;
                schemeDetailsVM.ROI = result.ROI;
                return Json(schemeDetailsVM);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public ActionResult GetPreSanctions()
        {
            var preSanctionList = _preSanctionService.GetPreSanctions();
            return PartialView("_PreSanctionList", preSanctionList);
        }
        public ActionResult GetPreSanction(int ID)
        {
            ButtonVisiblity("Edit");
            var model = _preSanctionService.GetPreSanction(ID);
            model.RMList = new SelectList(_preSanctionService.GetAllRMByBranch(), "UserID", "UserName");
            model.LoanPurposes = new SelectList(_preSanctionService.GetLoanPurposes(), "LoanPuposeID", "LoanPupose");
            model.Schemes = new SelectList(_schemeService.GetAllSchemeMasters(), "SID", "SchemeName");
            model.Products = new SelectList(_productrateService.GetProductList(), "Id", "Name");
            return View("Index", model);
        }

        public ActionResult DeletePreSanction(int Id)
        {
            Dictionary<string, string> errors;
            var success = false;

            success = _preSanctionService.DeletePreSanction(Id, out errors);
            return Json(success);
        }
    }
}