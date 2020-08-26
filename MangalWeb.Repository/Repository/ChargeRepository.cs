using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MangalWeb.Repository.Repository
{
    public class ChargeRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public void DeleteRecord(int id)
        {
            var chargetrndata = _context.tbl_GLChargeMaster_Details.Where(x => x.CID == id).ToList();
            //Delete the data from tbl_GLChargeMaster_Details
            if (chargetrndata != null)
            {
                foreach (var chargetrn in chargetrndata)
                {
                    _context.tbl_GLChargeMaster_Details.Remove(chargetrn);
                }
                _context.SaveChanges();
            }
            var chargedata = _context.tbl_GLChargeMaster_BasicInfo.Find(id);
            _context.tbl_GLChargeMaster_BasicInfo.Remove(chargedata);
            _context.SaveChanges();
        }

        public void SaveRecord(ChargeViewModel chargeViewModel)
        {
            tbl_GLChargeMaster_BasicInfo tblCharge = new tbl_GLChargeMaster_BasicInfo();
            tbl_GLChargeMaster_Details tblChargeDetails = new tbl_GLChargeMaster_Details();

            tblCharge.ChargeName = chargeViewModel.ChargeName;
            tblCharge.ReferenceDate = chargeViewModel.ReferenceDate;
            tblCharge.Status = chargeViewModel.Status;
            tblCharge.BranchID = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
            tblCharge.CMPId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            tblCharge.FYID = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);
            tblCharge.CreatedDate = DateTime.Now;
            tblCharge.CreatedBy = chargeViewModel.CreatedBy;
            tblCharge.UpdatedDate = DateTime.Now;
            tblCharge.UpdatedBy = chargeViewModel.UpdatedBy;
            _context.tbl_GLChargeMaster_BasicInfo.Add(tblCharge);
            _context.SaveChanges();

            int CID = _context.tbl_GLChargeMaster_BasicInfo.Max(x => x.CID);

            //save the data in Charge Details table
            foreach (var p in chargeViewModel.chargeDetailsCollection)
            {
                var chargetrn = new tbl_GLChargeMaster_Details
                {
                    CID = CID,
                    LoanAmtFrom = p.LoanAmountGreaterthan,
                    LoanAmtTo = p.LoanAmountLessthan,
                    Charges = p.ChargeAmount,
                    ChargeType = p.ChargeType,
                };
                _context.tbl_GLChargeMaster_Details.Add(chargetrn);
                _context.SaveChanges();
            }
        }

        public void UpdateRecord(ChargeViewModel chargeViewModel)
        {
            var chargeObj = _context.tbl_GLChargeMaster_BasicInfo.Where(x => x.CID == chargeViewModel.ID).FirstOrDefault();

            //update the data in charge table
            chargeObj.ChargeName = chargeViewModel.ChargeName;
            chargeObj.ReferenceDate = chargeViewModel.ReferenceDate;
            chargeObj.Status = chargeViewModel.Status;
            chargeObj.UpdatedBy = chargeViewModel.UpdatedBy;
            chargeObj.UpdatedDate = DateTime.Now;
            chargeObj.BranchID = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
            chargeObj.CMPId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            chargeObj.FYID = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);

            List<tbl_GLChargeMaster_Details> NewChargeDetailsList = new List<tbl_GLChargeMaster_Details>();

            //update the data in Charge Details table
            foreach (var p in chargeViewModel.chargeDetailsCollection)
            {
                var FindChargebject = _context.tbl_GLChargeMaster_Details.Where(x => x.ID == p.ID && x.CID == chargeViewModel.ID).FirstOrDefault();
                if (FindChargebject == null)
                {
                    var chargetrnnew = new tbl_GLChargeMaster_Details
                    {
                        CID = chargeViewModel.ID,
                        LoanAmtFrom = p.LoanAmountGreaterthan,
                        LoanAmtTo = p.LoanAmountLessthan,
                        Charges = p.ChargeAmount,
                        ChargeType = p.ChargeType,
                    };
                    _context.tbl_GLChargeMaster_Details.Add(chargetrnnew);
                }
                else
                {
                    FindChargebject.LoanAmtFrom = p.LoanAmountGreaterthan;
                    FindChargebject.LoanAmtTo = p.LoanAmountLessthan;
                    FindChargebject.Charges = p.ChargeAmount;
                    FindChargebject.ChargeType = p.ChargeType;
                }
                NewChargeDetailsList.Add(FindChargebject);
            }
            #region charge details remove
            //take the loop of table and check from list if found in list then not remove else remove from table itself
            var chargetrnobjlist = _context.tbl_GLChargeMaster_Details.Where(x => x.CID == chargeViewModel.ID).ToList();
            if (chargetrnobjlist != null)
            {
                foreach (tbl_GLChargeMaster_Details item in chargetrnobjlist)
                {
                    if (NewChargeDetailsList.Contains(item))
                    {
                        continue;
                    }
                    else
                    {
                        _context.tbl_GLChargeMaster_Details.Remove(item);
                    }
                }
                _context.SaveChanges();
            }
            #endregion

        }

        public string CheckChargeNameExists(string Name, int id)
        {
            if (id > 0)
            {
                return _context.tbl_GLChargeMaster_BasicInfo.Where(x => x.ChargeName == Name && x.CID != id).Select(x => x.ChargeName).FirstOrDefault();
            }
            else
            {
                return _context.tbl_GLChargeMaster_BasicInfo.Where(x => x.ChargeName == Name).Select(x => x.ChargeName).FirstOrDefault();
            }
        }

        public List<ChargeViewModel> SetDataofModalList()
        {
            List<ChargeViewModel> list = new List<ChargeViewModel>();
            var model = new ChargeViewModel();
            var tablelist = _context.tbl_GLChargeMaster_BasicInfo.ToList();
            foreach (var item in tablelist)
            {
                model = new ChargeViewModel();
                model.ID = item.CID;
                model.ChargeName = item.ChargeName;
                model.ReferenceDate = item.ReferenceDate;
                model.Status = item.Status;
                list.Add(model);
            }
            return list;
        }


        public ChargeViewModel SetRecordinEdit(int ID)
        {
            ChargeViewModel chargeViewModel = new ChargeViewModel();
            var chargeobj = _context.tbl_GLChargeMaster_BasicInfo.Where(x => x.CID == ID).FirstOrDefault();
            //get charge trn table data
            var purchasetrndatalist = _context.tbl_GLChargeMaster_Details.Where(x => x.CID == ID).ToList();
            chargeViewModel = ToViewModelCharge(chargeobj, purchasetrndatalist);
            return chargeViewModel;
        }

        public static ChargeViewModel ToViewModelCharge(tbl_GLChargeMaster_BasicInfo charge, ICollection<tbl_GLChargeMaster_Details> ChargeTrnList)
        {
            var purchaseviewmodel = new ChargeViewModel
            {
                ChargeName = charge.ChargeName,
                ReferenceDate = charge.ReferenceDate,
                Status = charge.Status,
                ID = charge.CID,
            };

            IList<ChargeDetailsViewModel> ChargeTrnViewModelList = new List<ChargeDetailsViewModel>();
            foreach (var c in ChargeTrnList)
            {
                var ChargeTrnViewModel = new ChargeDetailsViewModel
                {
                    ID = c.ID,
                    LoanAmountGreaterthan = c.LoanAmtFrom,
                    LoanAmountLessthan = c.LoanAmtTo,
                    ChargeAmount = c.Charges,
                    ChargeType = c.ChargeType,
                };
                ChargeTrnViewModelList.Add(ChargeTrnViewModel);
            }
            purchaseviewmodel.chargeDetailsCollection = ChargeTrnViewModelList;
            return purchaseviewmodel;
        }
    }
}
