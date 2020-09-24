using MangalWeb.Model.Transaction;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Nitesh Tiwari 28.08.2020 4.00 pm
namespace MangalWeb.Controllers
{
    public class SanctionDisbursementController : BaseController
    {
        SanctionService _sanctionService = new SanctionService();

        public void BindList()
        {
            ViewBag.BankAccountList =new SelectList(_sanctionService.BankAccountList(), "AccountID", "Name");
            ViewBag.CashAccountList =new SelectList(_sanctionService.CashAccountList(), "AccountID", "Name");
            ViewBag.CashOutwardList =new SelectList(_sanctionService.GetCashOutwardList(),"UserID", "UserName"); 
            ViewBag.GoldInwardList =new SelectList(_sanctionService.GetGoldInwardList(),"UserID", "UserName");
            ViewBag.ChargeMasterList = new SelectList(_sanctionService.FillChargeList(), "CID", "ChargeName");
            ViewBag.ChargeAccountList = new SelectList(_sanctionService.ChargeAccountList(), "AccountID", "Name");
        }

        #region Insert

        [HttpPost]
        public ActionResult Insert(SanctionDisbursementVM objViewModel)
        {
            try
            {
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
                    if (UpdateData(objViewModel))
                    {
                        return Json(2, JsonRequestBehavior.AllowGet);
                    }
                }
                objViewModel.ChargeDetailList = new List<ChargeSanctionVM>();
                BindList();
                return View("SanctionDisbursement", objViewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Insert

        #region Insert Data

        public bool InsertData(SanctionDisbursementVM sanctionViewModel)
        {
            bool retVal = false;
            sanctionViewModel.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
            sanctionViewModel.UpdatedBy = Convert.ToInt32(Session["UserLoginId"]);
            sanctionViewModel.FinancialYearId= Convert.ToInt32(Session["FinancialYearId"]);
            sanctionViewModel.BranchId = Convert.ToInt32(Session["BranchId"]);
            sanctionViewModel.CompanyId = Convert.ToInt32(Session["CompanyId"]);
            sanctionViewModel.ProofOfOwnerShipFile =(HttpPostedFileBase)Session["Proofofownership"];
            _sanctionService.SanctionDisbursment_PRI("Save", sanctionViewModel);
            retVal = true;
            return retVal;
        }

        #endregion Insert Data

        #region Update Data

        public bool UpdateData(SanctionDisbursementVM sanctionViewModel)
        {
            bool retVal = false;
            sanctionViewModel.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
            sanctionViewModel.UpdatedBy = Convert.ToInt32(Session["UserLoginId"]);
            _sanctionService.SanctionDisbursment_PRI("Save", sanctionViewModel);
            retVal = true;
            return retVal;
        }
        #endregion Update Data

        [HttpPost]
        public JsonResult UploadProofOfOwnershipImage()
        {
            HttpFileCollectionBase files = Request.Files;
            HttpPostedFileBase postedFile = files[0];
            //Stream fs = postedFile.InputStream;
            //BinaryReader br = new BinaryReader(fs);
            //Byte[] bytes = br.ReadBytes(postedFile.ContentLength);
            ////base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            //SanctionDisbursementVM docupload = null;
            //docupload = new SanctionDisbursementVM();
            //docupload.ProofOfOwnerShipImageFile = bytes;
            Session["Proofofownership"] = postedFile;
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Remove(int id)
        {
            //var list = (List<DocumentUploadDetailsVM>)Session["sub"];
            //list.Remove(list.Where(x => x.ID == id).FirstOrDefault());
            //Session["sub"] = list;
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        #region SanctionDisbursement
        // GET: SanctionDisbursement
        public ActionResult SanctionDisbursement()
        {
            ButtonVisiblity("Index");
            var model = new SanctionDisbursementVM();
            model.TransactionId = _sanctionService.GetMaxTransactionId();
            model.InterestRepaymentDate = DateTime.Now.AddMonths(1).ToShortDateString();
            model.EmployeeName = Session["UserName"].ToString();
            model.TransactionDate = _sanctionService.GetLoanDate();
            model.LoanAccountNo = _sanctionService.GetLoanNo();
            BindList();
            Session["Proofofownership"] = null;
            return View(model);
        }
        #endregion

        #region GetChargeDetails
        public JsonResult GetChargeDetails(int ChargeId,decimal SanctionLoanAmount)
        {
            var data = _sanctionService.GetChargeDetails(ChargeId,SanctionLoanAmount);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetSanctionTable
        public ActionResult GetSanctionTable(string Operation)
        {
            Session["Operation"] = Operation;
            //ButtonVisiblity(Operation);
            return PartialView("_CustomerDetails", _sanctionService.GetSanctionDisbursementList());
        }
        #endregion

        #region GetSanctionDisbursementDetails
        public ActionResult GetSanctionDisbursementDetails(int Id)
        {
            var model = _sanctionService.GetSanctionDisbursementListById(Id);
            model.TransactionId = _sanctionService.GetMaxTransactionId();
            model.InterestRepaymentDate = DateTime.Now.AddMonths(1).ToShortDateString();
            model.EmployeeName = Session["UserName"].ToString();
            model.TransactionDate = _sanctionService.GetLoanDate();
            model.LoanAccountNo = _sanctionService.GetLoanNo();
            BindList();
            //return View("RequestForm", model);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}