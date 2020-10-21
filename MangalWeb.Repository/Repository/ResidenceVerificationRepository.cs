using MangalWeb.Model.Entity;
using MangalWeb.Model.Transaction;
using MangalWeb.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MangalWeb.Repository.Repository
{
    public class ResidenceVerificationRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<KYCBasicDetailsVM> GetCustomerList()
        {
            List<KYCBasicDetailsVM> kYCBasicDetailsVMs = new List<KYCBasicDetailsVM>();
            kYCBasicDetailsVMs = _context.Database.SqlQuery<KYCBasicDetailsVM>("GetResidenceVerificationCustomerList").ToList();
            foreach(var item in kYCBasicDetailsVMs)
            {
                DateTime now = DateTime.Now;
            }
            return kYCBasicDetailsVMs;
        }

        public void SaveUpdateRecord(ResidenceVerificationVM model)
        {
            try
            {
                tbl_ResidenceVerification residenceVerification = new tbl_ResidenceVerification();
                residenceVerification.CreatedBy = model.CreatedBy;
                residenceVerification.CreatedDate = DateTime.Now;
                residenceVerification.ApplicationNo = model.ApplicationNo;
                residenceVerification.AppliedDate =Convert.ToDateTime(model.AppliedDate);
                residenceVerification.Comments = model.Comments;
                residenceVerification.CustomerId = model.CustomerId;
                residenceVerification.IsActive = true;
                residenceVerification.AddressCategory = model.AddressCategory;
                residenceVerification.Area = model.Area;
                residenceVerification.AreaID = model.AreaID;
                residenceVerification.BldgHouseName = model.BldgHouseName;
                residenceVerification.BldgPlotNo = model.BldgPlotNo;
                residenceVerification.CityID = model.CityID;
                residenceVerification.DateofVisit = model.DateofVisit;
                residenceVerification.Designation = model.Designation;
                residenceVerification.Distance = model.Distance;
                residenceVerification.EmployeeCode = model.EmployeeCode;
                residenceVerification.FamilyMemberDetails = model.FamilyMemberDetails;
                residenceVerification.Landmark = model.Landmark;
                residenceVerification.PersonVisitedName = model.PersonVisitedName;
                residenceVerification.PinCode = model.PinCode;
                residenceVerification.PreSanctionId = model.PreSanctionId;
                residenceVerification.RelationWithCustomer = model.RelationWithCustomer;
                residenceVerification.ResidenceCode = model.ResidenceCode;
                residenceVerification.ResidingAtThisAddress_Months = model.ResidingAtThisAddress_Months;
                residenceVerification.ResidingAtThisAddress_Years = model.ResidingAtThisAddress_Years;
                residenceVerification.Road = model.Road;
                residenceVerification.RoomBlockNo = model.RoomBlockNo;
                residenceVerification.StateID = model.StateID;
                residenceVerification.TimeofVisit = model.TimeofVisit;
                residenceVerification.TransactionId = model.TransactionId;
                residenceVerification.UserId = model.UserId;
                residenceVerification.ZoneID = model.ZoneID;
                _context.tbl_ResidenceVerification.Add(residenceVerification);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResidenceVerificationVM GetCustomerById(int id)
        {
            ResidenceVerificationVM residenceVerificationVM = new ResidenceVerificationVM();
            var kYCBasicDetails = _context.Database.SqlQuery<KYCBasicDetailsVM>("GetCustomerById @id", new SqlParameter("@id", id)).FirstOrDefault();
            residenceVerificationVM = ToViewModelResidenceVerification(kYCBasicDetails);
            return residenceVerificationVM;
        }

        #region ToViewModelPreSanction
        public ResidenceVerificationVM ToViewModelResidenceVerification(KYCBasicDetailsVM model)
        {
            ResidenceVerificationVM residenceVerificationVM = new ResidenceVerificationVM();
            Random rand = new Random(100);
            int cid = rand.Next(000000000, 999999999) + 1;
            int pin = Convert.ToInt32(model.PinCode);
            var pincode = _context.Mst_PinCode.Where(x => x.Pc_Id == pin).FirstOrDefault();
            var city = _context.tblCityMasters.Where(x => x.CityID == pincode.Pc_CityId).FirstOrDefault();
            var state = _context.tblStateMasters.Where(x => x.StateID == city.StateID).FirstOrDefault();
            var zone = _context.tblZonemasters.Where(x => x.ZoneID == pincode.Pc_ZoneId).FirstOrDefault();

            residenceVerificationVM.TransactionId = "T" + cid.ToString();
            residenceVerificationVM.ApplicationNo = model.ApplicationNo;
            residenceVerificationVM.AppliedDate = model.AppliedDate;
            residenceVerificationVM.CustomerId = model.CustomerID;
            residenceVerificationVM.PreSanctionId = _context.tbl_PreSanctionDetails.Where(x => x.KycId == model.KYCID)
                                          .OrderByDescending(x => x.CreatedDate)
                                          .Select(x => x.Id).FirstOrDefault();
            residenceVerificationVM.AddressCategory = "02";
            residenceVerificationVM.Area = model.Area;
            residenceVerificationVM.AreaID = model.AreaID;
            residenceVerificationVM.BldgHouseName = model.BldgHouseName;
            residenceVerificationVM.BldgPlotNo = model.BldgPlotNo;
            residenceVerificationVM.CityID = model.CityID;
            residenceVerificationVM.Distance = model.Distance;
            residenceVerificationVM.Landmark = model.Landmark;
            residenceVerificationVM.PinCode = model.PinCode;
            residenceVerificationVM.ResidenceCode = model.ResidenceCode;
            residenceVerificationVM.Road = model.Road;
            residenceVerificationVM.RoomBlockNo = model.RoomBlockNo;
            residenceVerificationVM.StateID = model.StateID;
            residenceVerificationVM.ZoneID = model.ZoneID;
            residenceVerificationVM.city = city.CityName;
            residenceVerificationVM.zone = zone.Zone;
            residenceVerificationVM.state = state.StateName;
            return residenceVerificationVM;
        }
        #endregion

        public List<UserDetail> GetAllRMByBranch()
        {
            return _context.UserDetails.ToList();
        }

        public int GetDocumentID(int kycId)
        {
            int docId;
            docId =  _context.Trn_DocumentUpload.Where(x => x.KycId == kycId)
                                          .OrderByDescending(x => x.DocId)
                                          .Select(x => x.DocId).FirstOrDefault();
            return docId;
        }

        public UserViewModel FillEmployeeDetailsById(int Id)
        {
           var result =  _context.UserDetails.Include("tbl_UserCategory").Where(x => x.UserID == Id).FirstOrDefault();
            UserViewModel user = new UserViewModel();
            user.EmployeeCode = result.EmployeeCode;
            user.tbl_UserCategory.Name = result.tbl_UserCategory.Name;
            return user;
        }
    }
}
