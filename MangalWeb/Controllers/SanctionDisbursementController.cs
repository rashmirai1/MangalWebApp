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

        #region BindList
        public void BindList()
        {
            ViewBag.BankAccountList = new SelectList(_sanctionService.BankAccountList(), "AccountID", "Name");
            ViewBag.CashAccountList = new SelectList(_sanctionService.CashAccountList(), "AccountID", "Name");
            ViewBag.CashOutwardList = new SelectList(_sanctionService.GetCashOutwardList(), "UserID", "UserName");
            ViewBag.GoldInwardList = new SelectList(_sanctionService.GetGoldInwardList(), "UserID", "UserName");
            ViewBag.ChargeMasterList = new SelectList(_sanctionService.FillChargeList(), "CID", "ChargeName");
            ViewBag.ChargeAccountList = new SelectList(_sanctionService.ChargeAccountList(), "AccountID", "Name");
        }
        #endregion

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
            sanctionViewModel.FinancialYearId = Convert.ToInt32(Session["FinancialYearId"]);
            sanctionViewModel.BranchId = Convert.ToInt32(Session["BranchId"]);
            sanctionViewModel.CompanyId = Convert.ToInt32(Session["CompanyId"]);
            if (Session["Proofofownership"] != null)
            {
                // sanctionViewModel.ProofOfOwnerShipFile = (HttpPostedFileBase)Session["Proofofownership"];
                sanctionViewModel.ProofOfOwnerShipImageFile = (byte[])Session["Proofofownership"];
            }
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
            sanctionViewModel.FinancialYearId = Convert.ToInt32(Session["FinancialYearId"]);
            sanctionViewModel.BranchId = Convert.ToInt32(Session["BranchId"]);
            sanctionViewModel.CompanyId = Convert.ToInt32(Session["CompanyId"]);
            if (Session["Proofofownership"] != null)
            {
                // sanctionViewModel.ProofOfOwnerShipFile = (HttpPostedFileBase)Session["Proofofownership"];
                sanctionViewModel.ProofOfOwnerShipImageFile = (byte[])Session["Proofofownership"];
            }
            _sanctionService.SanctionDisbursment_PRI("Update", sanctionViewModel);
            retVal = true;
            return retVal;
        }
        #endregion Update Data

        #region UploadProofOfOwnershipImage
        [HttpPost]
        public JsonResult UploadProofOfOwnershipImage()
        {
            HttpFileCollectionBase files = Request.Files;
            HttpPostedFileBase postedFile = files[0];
            Stream fs = postedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bytes = br.ReadBytes(postedFile.ContentLength);
            ////base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            //var docupload = new SanctionDisbursementVM();
            //docupload.ProofOfOwnerShipImageFile = bytes;
            Session["Proofofownership"] = bytes;
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Remove
        public ActionResult Remove(int id)
        {
            //var list = (List<DocumentUploadDetailsVM>)Session["sub"];
            //list.Remove(list.Where(x => x.ID == id).FirstOrDefault());
            //Session["sub"] = list;
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        #endregion

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
            Session["ChargeDetails"] = null;
            return View(model);
        }
        #endregion

        #region GetChargeDetails
        public JsonResult GetChargeDetails(int ChargeId, decimal SanctionLoanAmount,string SchemeProcessingType,double SchemeProcessingCharge)
        {
            var data = _sanctionService.GetChargeDetails(ChargeId, SanctionLoanAmount,SchemeProcessingType,SchemeProcessingCharge);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetCustomerTable
        public ActionResult GetCustomerTable(string Operation)
        {
            Session["Operation"] = Operation;
            //ButtonVisiblity(Operation);
            return PartialView("_CustomerDetails", _sanctionService.GetKycDetailsList());
        }
        #endregion

        #region GetSanctionTable
        public ActionResult GetSanctionTable(string Operation)
        {
            Session["Operation"] = Operation;
            //ButtonVisiblity(Operation);
            return PartialView("_SanctionDetails", _sanctionService.GetSanctionDisbursementList());
        }
        #endregion

        #region GetKycDetails
        public ActionResult GetKycDetails(int Id)
        {
            var model = _sanctionService.GetKYCListById(Id);
            model.TransactionId = _sanctionService.GetMaxTransactionId();
            model.InterestRepaymentDate = DateTime.Now.AddMonths(1).ToShortDateString();
            model.EmployeeName = Session["UserName"].ToString();
            model.TransactionDate = _sanctionService.GetLoanDate();
            model.LoanAccountNo = _sanctionService.GetLoanNo();
            BindList();
            string operation = Session["Operation"].ToString();
            model.operation = operation;
            ButtonVisiblity(operation);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetSanctionDisbursementDetails
        public ActionResult GetSanctionDisbursementDetails(int Id)
        {
            var model = _sanctionService.GetSanctionDisbursementListById(Id);
            BindList();
            string operation = Session["Operation"].ToString();
            ButtonVisiblity(operation);
            model.operation = operation;
            Session["ChargeDetails"] = model.ChargeDetailList;
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Delete
        // GETDelete/5
        public ActionResult Delete(int id)
        {
            string data = "";
            _sanctionService.DeleteRecord(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region AddChargeDetails
        [HttpPost]
        public JsonResult AddChargeDetails()
        {
            SanctionDisbursementVM model = new SanctionDisbursementVM();
            var chargeSanctionVM = new ChargeSanctionVM();
            chargeSanctionVM.ID = Convert.ToInt32(Request.Form["Id"]);
            chargeSanctionVM.ChargeId = Convert.ToInt32(Request.Form["ChargeId"]);
            chargeSanctionVM.CDetailsID = Convert.ToInt32(Request.Form["CDetailsID"]);
            chargeSanctionVM.AccountId = Convert.ToInt32(Request.Form["AccountId"]);
            chargeSanctionVM.AccountName = Request.Form["AccountName"];
            chargeSanctionVM.ChargeName = Request.Form["ChargeName"];
            chargeSanctionVM.Charges = Convert.ToDouble(Request.Form["Charges"]);
            chargeSanctionVM.Amount = Convert.ToDouble(Request.Form["Amount"]);
            chargeSanctionVM.ChargeType = Request.Form["ChargeType"];
            int StateID = Convert.ToInt32(Request.Form["StateID"]);
            model.ChargeDetailList.Add(chargeSanctionVM);
            double CGSTTax = 0;
            double SGSTTax = 0;
            int CGSTAccountNo = 0;
            int? SGSTAccountNo = 0;
            _sanctionService.GetGSTRecord(StateID, ref CGSTAccountNo, ref SGSTAccountNo, ref CGSTTax, ref SGSTTax);
            model.ChargeDetailList.Add(new ChargeSanctionVM()
            {
                ID = chargeSanctionVM.ID,
                ChargeId = chargeSanctionVM.ID,
                CDetailsID = 0,
                ChargeName = SGSTAccountNo > 0 ? "CGST" : "IGST",
                Charges = Convert.ToDouble(CGSTTax),
                ChargeType = "Percentage",
                Amount = chargeSanctionVM.Amount * Convert.ToDouble(CGSTTax) / 100,
                AccountId = CGSTAccountNo,
                AccountName = _sanctionService.GetAccountName(CGSTAccountNo)
            });
            if (SGSTAccountNo > 0)
            {
                int sgstaccid = Convert.ToInt32(SGSTAccountNo);
                model.ChargeDetailList.Add(new ChargeSanctionVM()
                {
                    ID = chargeSanctionVM.ID,
                    ChargeId = chargeSanctionVM.ID,
                    CDetailsID = 0,
                    ChargeName = "SGST",
                    Charges = Convert.ToDouble(SGSTTax),
                    ChargeType = "Percentage",
                    Amount = chargeSanctionVM.Amount * Convert.ToDouble(SGSTTax) / 100,
                    AccountId = SGSTAccountNo,
                    AccountName = _sanctionService.GetAccountName(sgstaccid)
                });
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region RemoveCharge
        public ActionResult RemoveCharge(int id)
        {
            var list = (List<ChargeSanctionVM>)Session["ChargeList"];
            list.Remove(list.Where(x => x.ID == id).FirstOrDefault());
            Session["ChargeList"] = list;
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Download
        public FileResult Download(int id)
        {
            var file = _sanctionService.GetImageById(id);
            return File(file, "image/jpeg,image/png");
        }
        #endregion
    }
}