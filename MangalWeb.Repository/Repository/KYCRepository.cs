using MangalWeb.Model.Entity;
using MangalWeb.Model.Transaction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MangalWeb.Repository.Repository
{
    public class KYCRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        /// <summary>
        /// Save kyc
        /// </summary>
        /// <param name="model"></param>
        public void SaveRecord(KYCBasicDetailsVM model, Boolean IsImageExist)
        {
            try
            {
                TGLKYC_BasicDetails tGLKYC_Basic = new TGLKYC_BasicDetails();
                if (model != null)
                {
                    int kycId = _context.TGLKYC_BasicDetails
                            .Where(x => x.CustomerID == model.CustomerID)
                            .OrderByDescending(x => x.AppliedDate)
                            .Select(x => x.KYCID).FirstOrDefault();
                    string refNo = Convert.ToString(kycId);
                    tGLKYC_Basic.AddressCategory = "02";
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
                    tGLKYC_Basic.NomineeAdharNo = model.NomineeAdharNo;
                    tGLKYC_Basic.PinCode = model.PinCode;
                    tGLKYC_Basic.Distance = model.Distance;
                    tGLKYC_Basic.Area = model.Area;
                    tGLKYC_Basic.ResidenceCode = model.ResidenceCode;
                    _context.TGLKYC_BasicDetails.Add(tGLKYC_Basic);
                    _context.SaveChanges();
                    HttpContext.Current.Session["KycId"] = tGLKYC_Basic.KYCID;

                    if (model.KycPhoto != null)
                    {
                        KycImageStore kycImageStore = new KycImageStore();
                        kycImageStore.KycPhoto = model.KycPhoto;
                        kycImageStore.Operation = "Save";
                        kycImageStore.Refno = Convert.ToString(tGLKYC_Basic.KYCID);
                        kycImageStore.ContentType = model.ContentType;
                        kycImageStore.ImageName = model.ImageName;
                        kycImageStore.CreatedDate = DateTime.Now;
                        _context.KycImageStores.Add(kycImageStore);
                        _context.SaveChanges();
                    }

                    else if (IsImageExist && model.KycPhoto == null)
                    {
                        //If Image already exist for the current customer and no new image is been uploaded add an existing image
                        KycImageStore kycImageStore = new KycImageStore();
                        var existingImage = _context.KycImageStores.Where(x => x.Refno == refNo).FirstOrDefault();
                        kycImageStore.KycPhoto = existingImage.KycPhoto;
                        kycImageStore.Operation = "Save";
                        kycImageStore.Refno = Convert.ToString(tGLKYC_Basic.KYCID);
                        kycImageStore.ContentType = existingImage.ContentType;
                        kycImageStore.ImageName = existingImage.ImageName;
                        kycImageStore.CreatedDate = DateTime.Now;
                        _context.KycImageStores.Add(kycImageStore);
                        _context.SaveChanges();
                        HttpContext.Current.Session["KycImageExist"] = null;
                    }

                    foreach (var item in model.Trans_KYCAddresses)
                    {
                        Trans_KYCAddresses trans_KYCAddresses = new Trans_KYCAddresses();
                        trans_KYCAddresses.AddressCategory = item.AddressCategory;
                        trans_KYCAddresses.Area = item.Area;
                        trans_KYCAddresses.BuildingHouseName = item.BuildingHouseName;
                        trans_KYCAddresses.BuildingPlotNo = item.BuildingPlotNo;
                        trans_KYCAddresses.CityID = item.CityID;
                        trans_KYCAddresses.CreatedDate = DateTime.Now;
                        trans_KYCAddresses.Distance_km = item.Distance_km;
                        trans_KYCAddresses.KYCID = tGLKYC_Basic.KYCID;
                        trans_KYCAddresses.NearestLandmark = item.NearestLandmark;
                        trans_KYCAddresses.PinCode = item.PinCode;
                        trans_KYCAddresses.ResidenceCode = item.ResidenceCode;
                        trans_KYCAddresses.Road = item.Road;
                        trans_KYCAddresses.RoomBlockNo = item.RoomBlockNo;
                        trans_KYCAddresses.StateID = item.StateID;
                        trans_KYCAddresses.ZoneId = item.ZoneId;
                        _context.Trans_KYCAddresses.Add(trans_KYCAddresses);
                        _context.SaveChanges();
                    }
                }


            }
            catch (Exception e)
            {
                throw e;
            }

        }
        /// <summary>
        /// get source of application to fill dopdown
        /// </summary>
        /// <returns></returns>
        public List<Mst_SourceofApplication> GetSourceOfApplicationList()
        {
            var list = _context.Mst_SourceofApplication.ToList();
            return list;
        }
        /// <summary>
        /// check if pan already exists
        /// </summary>
        /// <param name="Pan"></param>
        /// <returns></returns>
        public KYCBasicDetailsVM doesPanExist(string Pan)
        {
            var kyc = _context.TGLKYC_BasicDetails
                .Include("Trans_KYCAddresses")
                .Where(x => x.PANNo == Pan)
                .OrderByDescending(x => x.AppliedDate)
                .FirstOrDefault();

            KYCBasicDetailsVM kycVm = new KYCBasicDetailsVM();
            if (kyc != null)
            {
                kycVm.KYCID = kyc.KYCID;
                kycVm.isPanAdharExist = true;
                kycVm.Age = kyc.Age;
                kycVm.AppFName = kyc.AppFName;
                kycVm.AppliedDate = DateTime.Now;
                kycVm.AppLName = kyc.AppLName;
                kycVm.AppMName = kyc.AppMName;
                kycVm.AppPhoto = kyc.AppPhoto;
                kycVm.AppPhotoPath = kyc.AppPhotoPath;
                kycVm.AppSign = kyc.AppSign;
                kycVm.AppSignPath = kyc.AppSignPath;
                kycVm.AreaID = kyc.AreaID;
                kycVm.BirthDate = kyc.BirthDate;
                kycVm.BldgHouseName = kyc.BldgHouseName;
                kycVm.BldgPlotNo = kyc.BldgPlotNo;
                kycVm.BranchID = kyc.BranchID;
                kycVm.Children = kyc.Children;
                kycVm.CityID = kyc.CityID;
                kycVm.CmpID = kyc.CmpID;
                kycVm.CreatedBy = kyc.CreatedBy;
                kycVm.CreatedDate = kyc.CreatedDate;
                kycVm.CustomerID = kyc.CustomerID;
                kycVm.DealerID = kyc.DealerID;
                kycVm.DeletedBy = kyc.DeletedBy;
                kycVm.DeletedDate = kyc.DeletedDate;
                kycVm.Designation = kyc.Designation;
                kycVm.EmailID = kyc.EmailID;
                kycVm.EmploymentType = kyc.EmploymentType;
                kycVm.ExistingCustomerID = kyc.ExistingCustomerID;
                kycVm.ExistingPLCaseNo = kyc.ExistingPLCaseNo;
                kycVm.FYID = kyc.FYID;
                kycVm.Gender = kyc.Gender;
                kycVm.IndustriesType = kyc.IndustriesType;
                kycVm.isActive = true;
                kycVm.Landmark = kyc.Landmark;
                kycVm.LoanpurposeID = kyc.LoanpurposeID;
                kycVm.MaritalStatus = kyc.MaritalStatus;
                kycVm.MobileNo = kyc.MobileNo;
                kycVm.NomAddress = kyc.NomAddress;
                kycVm.NomFName = kyc.NomFName;
                kycVm.NomLName = kyc.NomLName;
                kycVm.NomMName = kyc.NomMName;
                kycVm.NomRelation = kyc.NomRelation;
                kycVm.Occupation = kyc.Occupation;
                kycVm.OfficeAddress = kyc.OfficeAddress;
                kycVm.OperatorID = kyc.OperatorID;
                kycVm.OrganizationName = kyc.OrganizationName;
                kycVm.PANNo = kyc.PANNo;
                kycVm.PresentIncome = kyc.PresentIncome;
                kycVm.Road = kyc.Road;
                kycVm.RoomBlockNo = kyc.RoomBlockNo;
                kycVm.SourceofApplication = kyc.SourceofApplication;
                kycVm.SourceofApplicationID = kyc.SourceofApplicationID;
                kycVm.SourceSpecification = kyc.SourceSpecification;
                kycVm.SpecifyEmployment = kyc.SpecifyEmployment;
                kycVm.SpecifyIndustries = kyc.SpecifyIndustries;
                kycVm.SpecifyLoanPurpose = kyc.SpecifyLoanPurpose;
                kycVm.Spouse = kyc.Spouse;
                kycVm.StateID = kyc.StateID;
                kycVm.TelephoneNo = kyc.TelephoneNo;
                kycVm.UpdatedBy = kyc.UpdatedBy;
                kycVm.UpdatedDate = DateTime.Now;
                kycVm.VerificationCode = kyc.VerificationCode;
                kycVm.ZoneID = kyc.ZoneID;
                kycVm.KYCDate = DateTime.Now;
                kycVm.AdhaarNo = kyc.AdhaarNo;
                kycVm.ApplicationNo = kyc.ApplicationNo;
                kycVm.SourceofApplicationID = kyc.SourceofApplicationID;
                kycVm.ApplicantPrefix = kyc.ApplicantPrefix;
                kycVm.MotherName = kyc.MotherName;
                kycVm.Father_Spouse = kyc.Father_Spouse;
                kycVm.CKYCNo = kyc.CKYCNo;
                kycVm.SourceType = kyc.SourceType;
                kycVm.OccupationOther = kyc.OccupationOther;
                kycVm.IndustryOther = kyc.IndustryOther;
                kycVm.NomineeMobileNo = kyc.NomineeMobileNo;
                kycVm.NomineePanNo = kyc.NomineePanNo;
                kycVm.NomineeAdharNo = kyc.NomineeAdharNo;
                kycVm.PinCode = kyc.PinCode;
                kycVm.Distance = kyc.Distance;
                kycVm.Area = kyc.Area;
                kycVm.Trans_KYCAddresses = kyc.Trans_KYCAddresses
                    .Select(
                    x => new KYCAddressesVM()
                    {
                        AddressCategory = x.AddressCategory,
                        Area = x.Area,
                        BuildingHouseName = x.BuildingHouseName,
                        BuildingPlotNo = x.BuildingPlotNo,
                        CityID = x.CityID,
                        CreatedDate = x.CreatedDate,
                        Distance_km = x.Distance_km,
                        ID = x.ID,
                        KYCID = x.KYCID,
                        NearestLandmark = x.NearestLandmark,
                        PinCode = x.PinCode,
                        ResidenceCode = x.ResidenceCode,
                        Road = x.Road,
                        RoomBlockNo = x.RoomBlockNo,
                        StateID = x.StateID,
                        ZoneId = x.ZoneId
                    }).ToList();

                string kycId = Convert.ToString(kyc.KYCID);
                kycVm.KycPhoto = _context.KycImageStores.Where(x => x.Refno == kycId)
                                          .OrderByDescending(x => x.CreatedDate)
                                          .Select(x => x.KycPhoto).FirstOrDefault();
                kycVm.ImageName = _context.KycImageStores.Where(x => x.Refno == kycId)
                                          .OrderByDescending(x => x.CreatedDate)
                                          .Select(x => x.ImageName).FirstOrDefault();
                kycVm.ContentType = _context.KycImageStores.Where(x => x.Refno == kycId)
                                         .OrderByDescending(x => x.CreatedDate)
                                         .Select(x => x.ContentType).FirstOrDefault();
            }
            else
            {
                kycVm.isPanAdharExist = false;
            }
            return kycVm;
        }
        /// <summary>
        /// check if adhar already exists
        /// </summary>
        /// <param name="AdharNo"></param>
        /// <returns></returns>
        public KYCBasicDetailsVM doesAdharExist(string AdharNo)
        {
            var kyc = _context.TGLKYC_BasicDetails
                .Include("Trans_KYCAddresses")
                .Where(x => x.AdhaarNo == AdharNo)
                .OrderByDescending(x => x.AppliedDate)
                .FirstOrDefault();
            KYCBasicDetailsVM kycVm = new KYCBasicDetailsVM();
            if (kyc != null)
            {
                kycVm.KYCID = kyc.KYCID;
                kycVm.isPanAdharExist = true;
                kycVm.Age = kyc.Age;
                kycVm.AppFName = kyc.AppFName;
                kycVm.AppliedDate = DateTime.Now;
                kycVm.AppLName = kyc.AppLName;
                kycVm.AppMName = kyc.AppMName;
                kycVm.AppPhoto = kyc.AppPhoto;
                kycVm.AppPhotoPath = kyc.AppPhotoPath;
                kycVm.AppSign = kyc.AppSign;
                kycVm.AppSignPath = kyc.AppSignPath;
                kycVm.AreaID = kyc.AreaID;
                kycVm.BirthDate = kyc.BirthDate;
                kycVm.BldgHouseName = kyc.BldgHouseName;
                kycVm.BldgPlotNo = kyc.BldgPlotNo;
                kycVm.BranchID = kyc.BranchID;
                kycVm.Children = kyc.Children;
                kycVm.CityID = kyc.CityID;
                kycVm.CmpID = kyc.CmpID;
                kycVm.CreatedBy = kyc.CreatedBy;
                kycVm.CreatedDate = kyc.CreatedDate;
                kycVm.CustomerID = kyc.CustomerID;
                kycVm.DealerID = kyc.DealerID;
                kycVm.DeletedBy = kyc.DeletedBy;
                kycVm.DeletedDate = kyc.DeletedDate;
                kycVm.Designation = kyc.Designation;
                kycVm.EmailID = kyc.EmailID;
                kycVm.EmploymentType = kyc.EmploymentType;
                kycVm.ExistingCustomerID = kyc.ExistingCustomerID;
                kycVm.ExistingPLCaseNo = kyc.ExistingPLCaseNo;
                kycVm.FYID = kyc.FYID;
                kycVm.Gender = kyc.Gender;
                kycVm.IndustriesType = kyc.IndustriesType;
                kycVm.isActive = true;
                kycVm.Landmark = kyc.Landmark;
                kycVm.LoanpurposeID = kyc.LoanpurposeID;
                kycVm.MaritalStatus = kyc.MaritalStatus;
                kycVm.MobileNo = kyc.MobileNo;
                kycVm.NomAddress = kyc.NomAddress;
                kycVm.NomFName = kyc.NomFName;
                kycVm.NomLName = kyc.NomLName;
                kycVm.NomMName = kyc.NomMName;
                kycVm.NomRelation = kyc.NomRelation;
                kycVm.Occupation = kyc.Occupation;
                kycVm.OfficeAddress = kyc.OfficeAddress;
                kycVm.OperatorID = kyc.OperatorID;
                kycVm.OrganizationName = kyc.OrganizationName;
                kycVm.PANNo = kyc.PANNo;
                kycVm.PresentIncome = kyc.PresentIncome;
                kycVm.Road = kyc.Road;
                kycVm.RoomBlockNo = kyc.RoomBlockNo;
                kycVm.SourceofApplication = kyc.SourceofApplication;
                kycVm.SourceofApplicationID = kyc.SourceofApplicationID;
                kycVm.SourceSpecification = kyc.SourceSpecification;
                kycVm.SpecifyEmployment = kyc.SpecifyEmployment;
                kycVm.SpecifyIndustries = kyc.SpecifyIndustries;
                kycVm.SpecifyLoanPurpose = kyc.SpecifyLoanPurpose;
                kycVm.Spouse = kyc.Spouse;
                kycVm.StateID = kyc.StateID;
                kycVm.TelephoneNo = kyc.TelephoneNo;
                kycVm.UpdatedBy = kyc.UpdatedBy;
                kycVm.UpdatedDate = DateTime.Now;
                kycVm.VerificationCode = kyc.VerificationCode;
                kycVm.ZoneID = kyc.ZoneID;
                kycVm.KYCDate = DateTime.Now;
                kycVm.AdhaarNo = kyc.AdhaarNo;
                kycVm.ApplicationNo = kyc.ApplicationNo;
                kycVm.SourceofApplicationID = kyc.SourceofApplicationID;
                kycVm.ApplicantPrefix = kyc.ApplicantPrefix;
                kycVm.MotherName = kyc.MotherName;
                kycVm.Father_Spouse = kyc.Father_Spouse;
                kycVm.CKYCNo = kyc.CKYCNo;
                kycVm.SourceType = kyc.SourceType;
                kycVm.OccupationOther = kyc.OccupationOther;
                kycVm.IndustryOther = kyc.IndustryOther;
                kycVm.NomineeMobileNo = kyc.NomineeMobileNo;
                kycVm.NomineePanNo = kyc.NomineePanNo;
                kycVm.NomineeAdharNo = kyc.NomineeAdharNo;
                kycVm.PinCode = kyc.PinCode;
                kycVm.Distance = kyc.Distance;
                kycVm.Area = kyc.Area;
                kycVm.Trans_KYCAddresses = kyc.Trans_KYCAddresses
                   .Select(
                   x => new KYCAddressesVM()
                   {
                       AddressCategory = x.AddressCategory,
                       Area = x.Area,
                       BuildingHouseName = x.BuildingHouseName,
                       BuildingPlotNo = x.BuildingPlotNo,
                       CityID = x.CityID,
                       CreatedDate = x.CreatedDate,
                       Distance_km = x.Distance_km,
                       ID = x.ID,
                       KYCID = x.KYCID,
                       NearestLandmark = x.NearestLandmark,
                       PinCode = x.PinCode,
                       ResidenceCode = x.ResidenceCode,
                       Road = x.Road,
                       RoomBlockNo = x.RoomBlockNo,
                       StateID = x.StateID,
                       ZoneId = x.ZoneId
                   }).ToList();
                string kycId = Convert.ToString(kyc.KYCID);
                kycVm.KycPhoto = _context.KycImageStores.Where(x => x.Refno == kycId)
                                          .OrderByDescending(x => x.CreatedDate)
                                          .Select(x => x.KycPhoto).FirstOrDefault();
                kycVm.ImageName = _context.KycImageStores.Where(x => x.Refno == kycId)
                                          .OrderByDescending(x => x.CreatedDate)
                                          .Select(x => x.ImageName).FirstOrDefault();
                kycVm.ContentType = _context.KycImageStores.Where(x => x.Refno == kycId)
                                         .OrderByDescending(x => x.CreatedDate)
                                         .Select(x => x.ContentType).FirstOrDefault();
            }
            else
            {
                kycVm.isPanAdharExist = false;
            }
            return kycVm;
        }
        /// <summary>
        /// generate application number
        /// </summary>
        /// <returns></returns>
        public int GenerateApplicationNo()
        {
            return _context.TGLKYC_BasicDetails.ToList().Count();
        }
        /// <summary>
        /// get kyc photo by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public KycImageStore GetImageById(int id)
        {
            string strId = Convert.ToString(id);
            return _context.KycImageStores.Where(x => x.Refno == strId).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
        }
        /// <summary>
        /// verify otp code
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="customerId"></param>
        /// <param name="otp"></param>
        /// <returns></returns>
        public string VerifyMobileNumber(string mobile, string customerId, string otp)
        {
            string response = String.Empty;
            var storedOTP = _context.tbl_KYCMobileOTP.Where(x => x.Mobile == mobile && x.CustomerId == customerId)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => x.OTP)
                .FirstOrDefault();
            if (otp == storedOTP)
            {
                response = "Mobile Verified Successfully!";
            }
            else
            {
                response = "Invalid OTP!";
            }
            return response;
        }
        /// <summary>
        /// store otp code in DB
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="customerId"></param>
        /// <param name="OTP"></param>
        public void StoreOtp(string mobile, string customerId, string OTP)
        {
            tbl_KYCMobileOTP tbl_KYCMobileOTP = new tbl_KYCMobileOTP();
            tbl_KYCMobileOTP.CreatedDate = DateTime.Now;
            tbl_KYCMobileOTP.CustomerId = customerId;
            tbl_KYCMobileOTP.Mobile = mobile;
            tbl_KYCMobileOTP.OTP = OTP;
            _context.tbl_KYCMobileOTP.Add(tbl_KYCMobileOTP);
            _context.SaveChanges();
        }
        /// <summary>
        /// get area, city, state, zone by pincode id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FillAddressByPinCode FillAddressByPinCode(int id)
        {
            var pincode = _context.Mst_PinCode.Where(x => x.Pc_Id == id).FirstOrDefault();
            var city = _context.tblCityMasters.Where(x => x.CityID == pincode.Pc_CityId).FirstOrDefault();
            var state = _context.tblStateMasters.Where(x => x.StateID == city.StateID).FirstOrDefault();
            var zone = _context.tblZonemasters.Where(x => x.ZoneID == pincode.Pc_ZoneId).FirstOrDefault();
            var areaName = pincode.Pc_AreaName;
            FillAddressByPinCode fillAddressByPinCode = new FillAddressByPinCode();
            fillAddressByPinCode.AreaName = areaName;
            fillAddressByPinCode.CityId = city.CityID;
            fillAddressByPinCode.CityName = city.CityName;
            fillAddressByPinCode.StateID = state.StateID;
            fillAddressByPinCode.StateName = state.StateName;
            fillAddressByPinCode.ZoneID = pincode.Pc_ZoneId;
            fillAddressByPinCode.ZoneName = zone.Zone;
            return fillAddressByPinCode;
        }
        /// <summary>
        /// get list of pincodes from db
        /// </summary>
        /// <returns></returns>
        public IList<Mst_PinCode> GetAllPincodes()
        {
            var pincode = _context.Mst_PinCode.ToList();

            return pincode;
        }
        /// <summary>
        /// Save Kyc Docs
        /// </summary>
        /// <param name="lstDocUploadTrn"></param>
        public void SaveDocument(List<KYCDocumentUpload> lstDocUploadTrn)
        {
            Trn_DocUploadDetails trn_DocUploadDetails = new Trn_DocUploadDetails();
            if (lstDocUploadTrn != null || lstDocUploadTrn.Count > 0)
            {
                foreach (var item in lstDocUploadTrn)
                {
                    trn_DocUploadDetails.DocumentId = item.DocumentId.Value;
                    trn_DocUploadDetails.UploadFile = item.UploadDocName;
                    trn_DocUploadDetails.ContentType = item.FileExtension;
                    trn_DocUploadDetails.FileName = item.FileName;
                    trn_DocUploadDetails.DocumentTypeId = item.DocumentTypeId.Value;
                    trn_DocUploadDetails.KycId = Convert.ToInt32(HttpContext.Current.Session["KycId"]);
                    trn_DocUploadDetails.ExpiryDate = item.ExpiryDate;
                    trn_DocUploadDetails.Status = "Pending";
                    _context.Trn_DocUploadDetails.Add(trn_DocUploadDetails);
                    _context.SaveChanges();
                }
            }
        }
    }
}
