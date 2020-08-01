using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using MangalWeb.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Repository.Repository
{
    public class KYCRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public void SaveRecord(KYCBasicDetailsVM model)
        {
            TGLKYC_BasicDetails tGLKYC_Basic = new TGLKYC_BasicDetails();
            if (model != null)
            {
                tGLKYC_Basic.Age = model.Age;
                tGLKYC_Basic.AppFName = model.AppFName;
                tGLKYC_Basic.AppliedDate = DateTime.Now;
                tGLKYC_Basic.AppLName = model.AppLName;
                tGLKYC_Basic.AppMName = model.AppMName;
                tGLKYC_Basic.AppPhoto = model.AppPhoto;
                tGLKYC_Basic.AppPhotoPath = model.AppPhotoPath;
                tGLKYC_Basic.AppSign = model.AppSign;
                tGLKYC_Basic.AppSignPath = model.AppSignPath;
                tGLKYC_Basic.AreaID = model.AreaID;
                tGLKYC_Basic.BirthDate = model.BirthDate;
                tGLKYC_Basic.BldgHouseName = model.BldgHouseName;
                tGLKYC_Basic.BldgPlotNo = model.BldgPlotNo;
                tGLKYC_Basic.BranchID = model.BranchID;
                tGLKYC_Basic.Children = model.Children;
                tGLKYC_Basic.CityID = model.CityID;
                tGLKYC_Basic.CmpID = model.CmpID;
                tGLKYC_Basic.CreatedBy = model.CreatedBy;
                tGLKYC_Basic.CreatedDate = model.CreatedDate;
                tGLKYC_Basic.CustomerID = model.CustomerID;
                tGLKYC_Basic.DealerID = model.DealerID;
                tGLKYC_Basic.DeletedBy = model.DeletedBy;
                tGLKYC_Basic.DeletedDate = model.DeletedDate;
                tGLKYC_Basic.Designation = model.Designation;
                tGLKYC_Basic.EmailID = model.EmailID;
                tGLKYC_Basic.EmploymentType = model.EmploymentType;
                tGLKYC_Basic.ExistingCustomerID = model.ExistingCustomerID;
                tGLKYC_Basic.ExistingPLCaseNo = model.ExistingPLCaseNo;
                tGLKYC_Basic.FYID = model.FYID;
                tGLKYC_Basic.Gender = model.Gender;
                tGLKYC_Basic.IndustriesType = model.IndustriesType;
                tGLKYC_Basic.isActive = true;
                tGLKYC_Basic.Landmark = model.Landmark;
                tGLKYC_Basic.LoanpurposeID = model.LoanpurposeID;
                tGLKYC_Basic.MaritalStatus = model.MaritalStatus;
                tGLKYC_Basic.MobileNo = model.MobileNo;
                tGLKYC_Basic.NomAddress = model.NomAddress;
                tGLKYC_Basic.NomFName = model.NomFName;
                tGLKYC_Basic.NomLName = model.NomLName;
                tGLKYC_Basic.NomMName = model.NomMName;
                tGLKYC_Basic.NomRelation = model.NomRelation;
                tGLKYC_Basic.Occupation = model.Occupation;
                tGLKYC_Basic.OfficeAddress = model.OfficeAddress;
                tGLKYC_Basic.OperatorID = model.OperatorID;
                tGLKYC_Basic.OrganizationName = model.OrganizationName;
                tGLKYC_Basic.PANNo = model.PANNo;
                tGLKYC_Basic.PresentIncome = model.PresentIncome;
                tGLKYC_Basic.Road = model.Road;
                tGLKYC_Basic.RoomBlockNo = model.RoomBlockNo;
                tGLKYC_Basic.SourceofApplication = model.SourceofApplication;
                tGLKYC_Basic.SourceofApplicationID = model.SourceofApplicationID;
                tGLKYC_Basic.SourceSpecification = model.SourceSpecification;
                tGLKYC_Basic.SpecifyEmployment = model.SpecifyEmployment;
                tGLKYC_Basic.SpecifyIndustries = model.SpecifyIndustries;
                tGLKYC_Basic.SpecifyLoanPurpose = model.SpecifyLoanPurpose;
                tGLKYC_Basic.Spouse = model.Spouse;
                tGLKYC_Basic.StateID = model.StateID;
                tGLKYC_Basic.TelephoneNo = model.TelephoneNo;
                tGLKYC_Basic.UpdatedBy = model.UpdatedBy;
                tGLKYC_Basic.UpdatedDate = DateTime.Now;
                tGLKYC_Basic.VerificationCode = model.VerificationCode;
                tGLKYC_Basic.ZoneID = model.ZoneID;
                tGLKYC_Basic.KYCDate = DateTime.Now;
                tGLKYC_Basic.AdhaarNo = model.AdhaarNo;
                tGLKYC_Basic.ApplicationNo = model.ApplicationNo;
                tGLKYC_Basic.SourceofApplicationID = model.SourceofApplicationID;
                tGLKYC_Basic.ApplicantPrefix = model.ApplicantPrefix;
                tGLKYC_Basic.MotherName = model.MotherName;
                tGLKYC_Basic.Father_Spouse = model.Father_Spouse;
                tGLKYC_Basic.CKYCNo = model.CKYCNo;
                tGLKYC_Basic.SourceType = model.SourceType;
                tGLKYC_Basic.OccupationOther = model.OccupationOther;
                tGLKYC_Basic.IndustryOther = model.IndustryOther;
                tGLKYC_Basic.NomineeMobileNo = model.NomineeMobileNo;
                tGLKYC_Basic.NomineePanNo = model.NomineePanNo;
                tGLKYC_Basic.AdhaarNo = model.AdhaarNo;
                _context.TGLKYC_BasicDetails.Add(tGLKYC_Basic);

                _context.SaveChanges();
            }


        }


    }
}
