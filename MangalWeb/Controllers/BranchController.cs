using MangalWeb.Model.Masters;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class BranchController : BaseController
    {
        BranchService _branchService = new BranchService();


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult CreateEdit(BranchViewModel branch)
        {
            branch.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
            branch.UpdatedBy = Convert.ToInt32(Session["UserLoginId"]);
            try
            {
                if (branch.ID <= 0)
                {
                    var data = _branchService.CheckBranchNameExists(branch.BranchName);
                    if (data != null)
                    {
                        ModelState.AddModelError("BranchName", "Branch Name Already Exists");
                        return Json(branch);
                    }
                }
                _branchService.SaveUpdateRecord(branch);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Json(branch);
        }

        public ActionResult GetBranchById(int ID)
        {
            string operation = Session["Operation"].ToString();
            ButtonVisiblity(operation);
            var tblBranch = _branchService.GetBranchMasterById(ID);
            var branch = new BranchViewModel();
            if(tblBranch!=null)
            {
                branch = _branchService.SetRecordinEdit(tblBranch);
            }
            var pincodelist = _branchService.GetPincodeMasterList();
            ViewBag.PincodeList = new SelectList(pincodelist, "PcId", "PincodeWithArea");

            return View("Branch", branch);
        }

        // GETDelete/5
        public ActionResult Delete(int id)
        {
            _branchService.DeleteRecord(id);
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult doesBranchNameExist(string BranchName)
        {
            var data = _branchService.CheckBranchNameExists(BranchName);
            var result = "";
            //Check if branch name already exists
            if (data != null)
            {
                if (BranchName.ToLower() == data.ToLower().ToString())
                {
                    result = "Branch Name Already Exists";
                }
                else
                {
                    result = "";
                }
            }
            return Json(result);
        }

        public ActionResult Branch()
        {
            ButtonVisiblity("Index");
            var model = new BranchViewModel();
            var pincodelist = _branchService.GetPincodeMasterList();
            ViewBag.PincodeList = new SelectList(pincodelist, "PcId", "PincodeWithArea");
            return View(model);
        }

        public ActionResult GetBranchTable(string Operation)
        {
            Session["Operation"] = Operation;
            //ButtonVisiblity(Operation);
            var list = _branchService.SetDataofModalList();
            return PartialView("_BranchList", list);
        }

        public JsonResult GetPincodeDetails(int id)
        {
            var branch = _branchService.GetPincodDetails(id);
            return Json(branch, JsonRequestBehavior.AllowGet);
        }
    }
}