using MangalWeb.Model.Transaction;
using MangalWeb.Models.Security;
using MangalWeb.Service.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MangalWeb.Controllers
{
    public class PreSanctionController : BaseController
    {
        PreSanctionService _preSanctionService = new PreSanctionService();
        SchemeService _schemeService = new SchemeService();
        ProductRateService _productrateService = new ProductRateService();

        #region PreSanction
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
        #endregion

        #region Insert
        [HttpPost]
        public ActionResult SavePreSanction(TGLPreSanctionVM model)
        {
            model.CreatedBy = UserInfo.UserID;

            try
            {
                //    ViewBag.LoanPurpose = new SelectList(LoanPurposeListMethod(), "Value", "Text");
                //    ViewBag.Schemes = new SelectList(_schemeService.GetAllSchemeMasters(), "SID", "SchemeName");
                //    ViewBag.ProductList = new SelectList(_productrateService.GetProductList(), "Id", "Name");
                //    ModelState.Remove("Id");
                //    if (objViewModel.Id == 0)
                //    {
                //        InsertData(objViewModel);
                //    }
                var preSanction = _preSanctionService.SavePreSanction(model);
                return Json(preSanction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Insert

        #region Insert Data

        public bool InsertData(PreSanctionDetailsVM model)
        {
            bool retVal = false;
            model.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
            try
            {
                _preSanctionService.SaveUpdateRecord(model);
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

        public List<ListItem> LoanPurposeListMethod()
        {
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem { Text = "Marrige", Value = "Marrige" });
            list.Add(new ListItem { Text = "house renovation", Value = "house renovation" });
            list.Add(new ListItem { Text = "Consumer durable", Value = "Consumer durable" });
            list.Add(new ListItem { Text = "holiday", Value = "holiday" });
            list.Add(new ListItem { Text = "medical expenses", Value = "medical expenses" });
            list.Add(new ListItem { Text = "business", Value = "business" });
            list.Add(new ListItem { Text = "housing loan", Value = "housing loan" });
            list.Add(new ListItem { Text = "repayments", Value = "repayments" });
            list.Add(new ListItem { Text = "sister marrige", Value = "sister marrige" });
            list.Add(new ListItem { Text = "daughters marrige", Value = "daughters marrige" });
            list.Add(new ListItem { Text = "Purchase Property", Value = "Purchase Property" });
            list.Add(new ListItem { Text = "Others", Value = "Others" });
            return list;
        }


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
    }
}