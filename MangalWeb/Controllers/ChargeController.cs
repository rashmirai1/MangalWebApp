using MangalWeb.Model.Masters;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class ChargeController : BaseController
    {
        ChargeService _chargeService = new ChargeService();

        #region Charge

        public ActionResult Charge()
        {
            try
            {
                ButtonVisiblity("Index");
                var chargeviewmodel = new ChargeViewModel();
                var chargetrn = new ChargeDetailsViewModel();
                chargeviewmodel.chargeDetailsCollection = new List<ChargeDetailsViewModel>();
                chargeviewmodel.ReferenceDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                return View(chargeviewmodel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Purchase

        #region Insert

        [HttpPost]
        public ActionResult Insert(ChargeViewModel objViewModel)
        {
            try
            {
                ModelState.Remove("Id");
                if (ModelState.IsValid)
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
                }
                objViewModel.chargeDetailsCollection = new List<ChargeDetailsViewModel>();
                return View("Charge", objViewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Insert

        #region Insert Data

        public bool InsertData(ChargeViewModel chargeViewModel)
        {
            bool retVal = false;
            chargeViewModel.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
            chargeViewModel.UpdatedBy = Convert.ToInt32(Session["UserLoginId"]);
            _chargeService.SaveRecord(chargeViewModel);
            retVal = true;
            return retVal;
        }

        #endregion Insert Data

        #region Update Data

        public bool UpdateData(ChargeViewModel chargeViewModel)
        {
            bool retVal = false;
            chargeViewModel.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
            chargeViewModel.UpdatedBy = Convert.ToInt32(Session["UserLoginId"]);
            _chargeService.UpdateRecord(chargeViewModel);
            retVal = true;
            return retVal;
        }

        #endregion Update Data

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
                _chargeService.DeleteRecord(id);
                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Delete

        public JsonResult CheckRecordonEditMode(int Id)
        {
            string data = "";
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #region GetChargeTable

        public ActionResult GetChargeTable(string Operation)
        {
            Session["Operation"] = Operation;
            ButtonVisiblity(Operation);
            var list = _chargeService.SetDataofModalList();
            return PartialView("_ChargePartialTable", list);
        }

        #endregion GetChargeTable

        #region GetChargeDetailsById

        public ActionResult GetChargeDetailsById(int ID)
        {
            try
            {
                string operation = Session["Operation"].ToString();
                ButtonVisiblity(operation);
                var chargeViewModel = _chargeService.GetChargeById(ID);
                return View("Charge", chargeViewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion GetChargeDetailsById

        public JsonResult doesChargeNameExist(string ChargeName, int Id)
        {
            var result = "";
            var data = _chargeService.CheckChargeNameExists(ChargeName, Id);
            //Check if city name already exists
            if (data != null)
            {
                if (ChargeName.ToLower() == data.ToLower().ToString())
                {
                    result = "Charge Name Already Exists";
                }
                else
                {
                    result = "";
                }
            }
            return Json(result);
        }
    }
}