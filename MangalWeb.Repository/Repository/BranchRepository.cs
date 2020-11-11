using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;

namespace MangalWeb.Repository.Repository
{
    public class BranchRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<tblCompanyBranchMaster> GetAllBranchMasters()
        {
            return _context.tblCompanyBranchMasters.Where(x => x.Status == 1).ToList();
        }

        public tblCompanyBranchMaster GetBranchMasterById(int id)
        {
            return _context.tblCompanyBranchMasters.Where(x => x.BID == id).FirstOrDefault();
        }

        public List<BranchViewModel> SetDataofModalList()
        {
            var tablelist = _context.tblCompanyBranchMasters.Where(x => x.Status == 1).ToList();
            List<BranchViewModel> list = new List<BranchViewModel>();
            var model = new BranchViewModel();
            foreach (var item in tablelist)
            {
                model = new BranchViewModel();
                model.ID = item.BID;
                model.BranchName = item.BranchName;
                model.BranchCode = item.BranchCode;
                model.DateInception = item.InceptionDate.ToShortDateString();
                model.DateWEF = Convert.ToDateTime(item.DateWEF).ToShortDateString();
                model.PincodeStr = item.Mst_PinCode.Pc_Desc;
                model.StatusStr = item.Status == 1 ? "Active" : "Inactive";
                list.Add(model);
            }
            return list;
        }

        public dynamic GetPincodeMasterList()
        {
            var pincodelist = _context.Mst_PinCode.Select(x => new
            {
                PcId = x.Pc_Id,
                PincodeWithArea = x.Pc_Desc + "(" + x.Pc_AreaName + ")"
            }).ToList();
            return pincodelist;
        }

        public List<Mst_BranchType> GetBranchTypeList()
        {
            return _context.Mst_BranchType.ToList();
        }

        public void DeleteRecord(int id)
        {
            var deleterecord = _context.tblCompanyBranchMasters.Where(x => x.BID == id).FirstOrDefault();
            if (deleterecord != null)
            {
                _context.tblCompanyBranchMasters.Remove(deleterecord);
                _context.SaveChanges();
            }
        }

        public void SaveUpdateRecord(BranchViewModel branch)
        {
            tblCompanyBranchMaster tblBranch = new tblCompanyBranchMaster();
            try
            {
                if (branch.ID <= 0)
                {
                    tblBranch.BranchName = branch.BranchName;
                    tblBranch.BranchCode = branch.BranchCode;
                    tblBranch.BranchType = branch.BranchType;
                    tblBranch.InceptionDate = Convert.ToDateTime(branch.DateInception);
                    if (tblBranch.BranchType == 2)
                    {
                        tblBranch.RentPeriodAgreed = null;
                    }
                    else
                    {
                        tblBranch.RentPeriodAgreed = Convert.ToDateTime(branch.RentPeriodAgreed);
                    }
                    tblBranch.DateWEF = Convert.ToDateTime(branch.DateWEF);
                    tblBranch.Address = branch.Address;
                    tblBranch.Pincode = branch.Pincode;
                    tblBranch.ContactPerson = branch.ContactPerson;
                    tblBranch.MobileNo = branch.MobileNo;
                    tblBranch.InTime = branch.InTime;
                    tblBranch.OutTime = branch.OutTime;
                    tblBranch.Status = branch.Status;
                    tblBranch.RecordCreated = DateTime.Now;
                    tblBranch.RecordCreatedBy = branch.CreatedBy;
                    tblBranch.RecordUpdated = DateTime.Now;
                    tblBranch.RecordUpdatedBy = branch.UpdatedBy;
                    tblBranch.CompID = 1;
                    _context.tblCompanyBranchMasters.Add(tblBranch);
                }
                else
                {
                    tblBranch = _context.tblCompanyBranchMasters.Where(x => x.BID == branch.ID && x.Status == 1).FirstOrDefault();
                    // first inactive record then insert active record
                    if (tblBranch.InceptionDate != Convert.ToDateTime(branch.DateInception))
                    {
                        tblBranch.Status = 2;
                        _context.SaveChanges();
                        tblBranch = new tblCompanyBranchMaster();
                        tblBranch.BranchName = branch.BranchName;
                        tblBranch.BranchCode = branch.BranchCode;
                        tblBranch.BranchType = branch.BranchType;
                        tblBranch.InceptionDate = Convert.ToDateTime(branch.DateInception);
                        if (tblBranch.BranchType == 2)
                        {
                            tblBranch.RentPeriodAgreed = null;
                        }
                        else
                        {
                            tblBranch.RentPeriodAgreed = Convert.ToDateTime(branch.RentPeriodAgreed);
                        }
                        tblBranch.DateWEF = Convert.ToDateTime(branch.DateWEF);
                        tblBranch.Address = branch.Address;
                        tblBranch.Pincode = branch.Pincode;
                        tblBranch.ContactPerson = branch.ContactPerson;
                        tblBranch.MobileNo = branch.MobileNo;
                        tblBranch.InTime = branch.InTime;
                        tblBranch.OutTime = branch.OutTime;
                        tblBranch.Status = branch.Status;
                        tblBranch.RecordUpdated = DateTime.Now;
                        tblBranch.RecordUpdatedBy = branch.UpdatedBy;
                        tblBranch.RecordCreated = DateTime.Now;
                        tblBranch.RecordCreatedBy = branch.CreatedBy;
                        tblBranch.CompID = 1;
                        _context.tblCompanyBranchMasters.Add(tblBranch);
                    }
                    else
                    {
                        tblBranch.BranchName = branch.BranchName;
                        tblBranch.BranchCode = branch.BranchCode;
                        tblBranch.BranchType = branch.BranchType;
                        tblBranch.InceptionDate = Convert.ToDateTime(branch.DateInception);
                        if (tblBranch.BranchType == 2)
                        {
                            tblBranch.RentPeriodAgreed = null;
                        }
                        else
                        {
                            tblBranch.RentPeriodAgreed = Convert.ToDateTime(branch.RentPeriodAgreed);
                        }
                        //tblBranch.DateWEF = Convert.ToDateTime(branch.DateWEF);
                        tblBranch.Address = branch.Address;
                        tblBranch.Pincode = branch.Pincode;
                        tblBranch.ContactPerson = branch.ContactPerson;
                        tblBranch.MobileNo = branch.MobileNo;
                        tblBranch.InTime = branch.InTime;
                        tblBranch.OutTime = branch.OutTime;
                        tblBranch.Status = branch.Status;
                        tblBranch.RecordUpdated = DateTime.Now;
                        tblBranch.RecordUpdatedBy = branch.UpdatedBy;
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string CheckBranchNameExists(string Name, int id)
        {
            if (id > 0)
            {
                return _context.tblCompanyBranchMasters.Where(x => x.BranchName == Name && x.BID != id).Select(x => x.BranchName).FirstOrDefault();
            }
            else
            {
                return _context.tblCompanyBranchMasters.Where(x => x.BranchName == Name).Select(x => x.BranchName).FirstOrDefault();
            }
        }

        public BranchViewModel SetRecordinEdit(tblCompanyBranchMaster tblBranch)
        {
            BranchViewModel branch = new BranchViewModel();
            branch.BranchName = tblBranch.BranchName;
            branch.BranchCode = tblBranch.BranchCode;
            branch.BranchType = tblBranch.BranchType;
            branch.DateInception = tblBranch.InceptionDate.ToShortDateString();
            branch.RentPeriodAgreed = Convert.ToDateTime(tblBranch.RentPeriodAgreed).ToShortDateString();
            branch.DateWEF = Convert.ToDateTime(tblBranch.DateWEF).ToShortDateString();
            branch.Address = tblBranch.Address;
            branch.Pincode = tblBranch.Pincode;
            branch.ContactPerson = tblBranch.ContactPerson;
            branch.MobileNo = tblBranch.MobileNo;
            branch.InTime = tblBranch.InTime;
            branch.OutTime = tblBranch.OutTime;
            branch.Status = (short)tblBranch.Status;
            //ViewBag.PincodeList = new SelectList(dd._context.Mst_PinCode.ToList(), "Pc_Id", "Pc_Desc");
            var pincodelist = _context.Mst_PinCode.Select(x => new
            {
                PcId = x.Pc_Id,
                PincodeWithArea = x.Pc_Desc + "(" + x.Pc_AreaName + ")"
            }).ToList();
            //ViewBag.PincodeList = new SelectList(pincodelist, "PcId", "PincodeWithArea");
            var pincodemodel = _context.Mst_PinCode.Where(x => x.Pc_Id == branch.Pincode).Select(x => new PincodeViewModel { CityId = x.Pc_CityId, ZoneId = x.Pc_ZoneId, AreaName = x.Pc_AreaName }).FirstOrDefault();
            var ZoneName = _context.tblZonemasters.Where(x => x.ZoneID == pincodemodel.ZoneId).Select(x => x.Zone).FirstOrDefault();
            var cityname = _context.tblCityMasters.Where(x => x.CityID == pincodemodel.CityId).Select(x => new CityViewModel { CityName = x.CityName, StateId = (int)x.StateID }).FirstOrDefault();
            var statename = _context.tblStateMasters.Where(x => x.StateID == cityname.StateId).Select(x => x.StateName).FirstOrDefault();
            branch.AreaName = pincodemodel.AreaName;
            branch.ZoneName = ZoneName;
            branch.CityName = cityname.CityName;
            branch.StateName = statename;
            return branch;
        }


        public BranchViewModel GetPincodDetails(int id)
        {
            var branch = new BranchViewModel();
            var pincodemodel = _context.Mst_PinCode.Where(x => x.Pc_Id == id).Select(x => new PincodeViewModel { CityId = x.Pc_CityId, ZoneId = x.Pc_ZoneId, AreaName = x.Pc_AreaName }).FirstOrDefault();
            var ZoneName = _context.tblZonemasters.Where(x => x.ZoneID == pincodemodel.ZoneId).Select(x => x.Zone).FirstOrDefault();
            var cityname = _context.tblCityMasters.Where(x => x.CityID == pincodemodel.CityId).Select(x => new CityViewModel { CityName = x.CityName, StateId = (int)x.StateID }).FirstOrDefault();
            var statename = _context.tblStateMasters.Where(x => x.StateID == cityname.StateId).Select(x => x.StateName).FirstOrDefault();
            branch.AreaName = pincodemodel.AreaName;
            branch.ZoneName = ZoneName;
            branch.CityName = cityname.CityName;
            branch.StateName = statename;
            return branch;
        }
    }
}
