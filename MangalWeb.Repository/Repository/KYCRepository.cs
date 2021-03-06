﻿using MangalWeb.Model.Entity;
using MangalWeb.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public void SaveRecord(KYCBasicDetailsVM model)
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
                    if (kycId == 0)
                    {
                        _context.TGLKYC_BasicDetails.Add(tGLKYC_Basic);
                        tGLKYC_Basic.CreatedBy = model.CreatedBy;
                        tGLKYC_Basic.CreatedDate = DateTime.Now;
                        model.KYCID = kycId;
                    }
                    else
                    {
                        tGLKYC_Basic = _context.TGLKYC_BasicDetails.Where(x => x.KYCID == kycId && x.isActive == true).FirstOrDefault();
                        model.KYCID = tGLKYC_Basic.KYCID;
                        int count = _context.SP_SaveKYCHistory(model.KYCID);
                    }
                    tGLKYC_Basic.AddressCategory = "02";
                    tGLKYC_Basic.KycType = model.KycType;
                    tGLKYC_Basic.Age = Convert.ToInt32(model.Age);
                    tGLKYC_Basic.AppliedDate = Convert.ToDateTime(model.AppliedDate);
                    tGLKYC_Basic.AppFName = model.AppFName;
                    tGLKYC_Basic.AppMName = model.AppMName;
                    tGLKYC_Basic.AppLName = model.AppLName;
                    tGLKYC_Basic.AppPhoto = model.ApplicantPhoto;
                    tGLKYC_Basic.ImageName = model.ImageName;
                    tGLKYC_Basic.ContentType = model.ContentType;
                    tGLKYC_Basic.BirthDate = Convert.ToDateTime(model.BirthDate);
                    tGLKYC_Basic.BldgHouseName = model.BldgHouseName;
                    tGLKYC_Basic.BldgPlotNo = model.BldgPlotNo;
                    tGLKYC_Basic.BranchID = model.BranchID;
                    tGLKYC_Basic.CmpID = model.CmpID;
                    tGLKYC_Basic.CustomerID = model.CustomerID;
                    tGLKYC_Basic.Designation = model.Designation;
                    tGLKYC_Basic.EmailID = model.EmailID;
                    tGLKYC_Basic.ExistingPLCaseNo = model.ExistingPLCaseNo;
                    tGLKYC_Basic.FYID = model.FYID;
                    tGLKYC_Basic.Gender = model.Gender;
                    tGLKYC_Basic.IndustriesType = model.IndustriesType;
                    tGLKYC_Basic.isActive = true;
                    tGLKYC_Basic.Landmark = model.Landmark;
                    tGLKYC_Basic.MaritalStatus = model.MaritalStatus;
                    tGLKYC_Basic.MobileNo = model.MobileNo;
                    tGLKYC_Basic.NomAddress = model.NomAddress;
                    tGLKYC_Basic.NomFName = model.NomFName;
                    tGLKYC_Basic.NomLName = model.NomLName;
                    tGLKYC_Basic.NomMName = model.NomMName;
                    tGLKYC_Basic.NomRelation = model.NomRelation;
                    tGLKYC_Basic.Occupation = model.Occupation;
                    tGLKYC_Basic.OperatorID = model.CreatedBy;
                    tGLKYC_Basic.OrganizationName = model.OrganizationName;
                    tGLKYC_Basic.PANNo = model.PANNo;
                    tGLKYC_Basic.PresentIncome = model.PresentIncome;
                    tGLKYC_Basic.Road = model.Road;
                    tGLKYC_Basic.RoomBlockNo = model.RoomBlockNo;
                    tGLKYC_Basic.SourceofApplicationID = Convert.ToInt32(model.SourceofApplicationID);
                    tGLKYC_Basic.Spouse = model.Spouse;
                    tGLKYC_Basic.TelephoneNo = model.TelephoneNo;
                    tGLKYC_Basic.UpdatedBy = model.UpdatedBy;
                    tGLKYC_Basic.UpdatedDate = DateTime.Now;
                    tGLKYC_Basic.KYCDate = DateTime.Now;
                    tGLKYC_Basic.AdhaarNo = model.AdhaarNo;
                    tGLKYC_Basic.ApplicationNo = model.ApplicationNo;
                    tGLKYC_Basic.ApplicantPrefix = model.ApplicantPrefix;
                    tGLKYC_Basic.MotherName = model.MotherName;
                    tGLKYC_Basic.Father_Spouse = model.Father_Spouse;
                    tGLKYC_Basic.CKYCNo = model.CKYCNo;
                    tGLKYC_Basic.OccupationOther = model.OccupationOther;
                    tGLKYC_Basic.IndustryOther = model.IndustryOther;
                    tGLKYC_Basic.NomineeMobileNo = model.NomineeMobileNo;
                    tGLKYC_Basic.NomineePanNo = model.NomineePanNo;
                    tGLKYC_Basic.NomineeAdharNo = model.NomineeAdharNo;
                    tGLKYC_Basic.PinCode = model.PinCode;
                    tGLKYC_Basic.Distance = model.Distance;
                    tGLKYC_Basic.ResidenceCode = model.ResidenceCode;
                    tGLKYC_Basic.FatherPrefix = model.FatherPrefix;
                    tGLKYC_Basic.CibilGender = model.Gender == 1 ? "F" : "M";
                    _context.SaveChanges();
                    int kycid = _context.TGLKYC_BasicDetails.Max(x => x.KYCID);
                    if (model.KYCID == 0)
                    {
                        model.KYCID = kycid;
                    }

                    foreach (var item in model.Trans_KYCAddresses)
                    {
                        Trans_KYCAddresses trans_KYCAddresses = new Trans_KYCAddresses();
                        trans_KYCAddresses = _context.Trans_KYCAddresses.Where(x => x.ID == item.ID).FirstOrDefault();
                        if (trans_KYCAddresses == null)
                        {
                            trans_KYCAddresses = new Trans_KYCAddresses();
                            _context.Trans_KYCAddresses.Add(trans_KYCAddresses);
                        }
                        trans_KYCAddresses.AddressCategory = item.AddressCategory;
                        trans_KYCAddresses.BuildingHouseName = item.BuildingHouseName;
                        trans_KYCAddresses.BuildingPlotNo = item.BuildingPlotNo;
                        trans_KYCAddresses.CreatedDate = DateTime.Now;
                        trans_KYCAddresses.Distance_km = item.Distance_km;
                        trans_KYCAddresses.KYCID = (int)model.KYCID;
                        trans_KYCAddresses.NearestLandmark = item.NearestLandmark;
                        trans_KYCAddresses.PinCode = item.PinCode;
                        trans_KYCAddresses.ResidenceCode = item.ResidenceCode;
                        trans_KYCAddresses.Road = item.Road;
                        trans_KYCAddresses.RoomBlockNo = item.RoomBlockNo;

                        _context.SaveChanges();
                    }

                    List<Trn_DocUploadDetails> NewDocUploadDetails = new List<Trn_DocUploadDetails>();
                    foreach (var p in model.DocumentUploadList)
                    {
                        var Findobject = _context.Trn_DocUploadDetails.Where(x => x.Id == p.ID && x.KycId == model.KYCID).FirstOrDefault();
                        if (Findobject == null)
                        {
                            var trnnew = new Trn_DocUploadDetails
                            {
                                KycId = (int)model.KYCID,
                                DocumentTypeId = (int)p.DocumentTypeId,
                                DocumentId = (int)p.DocumentId,
                                ExpiryDate = p.ExpiryDate,
                                FileName = p.FileName,
                                ContentType = p.FileExtension,
                                UploadFile = p.UploadDocName,
                                SpecifyOther = p.SpecifyOther,
                                NameonDocument = p.NameonDocument,
                                Status = "Pending"
                            };
                            _context.Trn_DocUploadDetails.Add(trnnew);
                        }
                        else
                        {
                            Findobject.KycId = (int)model.KYCID;
                            Findobject.DocumentTypeId = (int)p.DocumentTypeId;
                            Findobject.DocumentId = (int)p.DocumentId;
                            Findobject.ExpiryDate = p.ExpiryDate;
                            Findobject.FileName = p.FileName;
                            Findobject.ContentType = p.FileExtension;
                            Findobject.UploadFile = p.UploadDocName;
                            Findobject.SpecifyOther = p.SpecifyOther;
                            Findobject.NameonDocument = p.NameonDocument;
                        }
                        NewDocUploadDetails.Add(Findobject);
                    }
                    #region document details remove
                    //take the loop of table and check from list if found in list then not remove else remove from table itself
                    var trnobjlist = _context.Trn_DocUploadDetails.Where(x => x.KycId == model.KYCID).ToList();
                    if (trnobjlist != null)
                    {
                        foreach (Trn_DocUploadDetails item in trnobjlist)
                        {
                            if (NewDocUploadDetails.Contains(item))
                            {
                                continue;
                            }
                            else
                            {
                                _context.Trn_DocUploadDetails.Remove(item);
                            }
                        }
                        _context.SaveChanges();
                    }
                    #endregion document trn remove
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region GetConsolidatedImage
        public TGLKYC_BasicDetails GetApplicantImage(int id)
        {
            return _context.TGLKYC_BasicDetails.Where(x => x.KYCID == id).FirstOrDefault();
        }
        #endregion

        /// <summary>
        /// get source of application to fill dopdown
        /// </summary>
        /// <returns></returns>
        public List<Mst_SourceofApplication> GetSourceOfApplicationList()
        {
            return _context.Mst_SourceofApplication.ToList();
        }
        /// <summary>
        /// check if pan already exists
        /// </summary>
        /// <param name="Pan"></param>
        /// <returns></returns>
        public KYCBasicDetailsVM doesPanExist(string Pan)
        {
            int branchid = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
            var fyid = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);

            var kyc = _context.TGLKYC_BasicDetails
                .Include("Trans_KYCAddresses")
                .Where(x => x.PANNo == Pan &&
                x.BranchID == branchid &&
                x.FYID == fyid)
                .OrderByDescending(x => x.AppliedDate)
                .FirstOrDefault();
            KYCBasicDetailsVM kycVm = new KYCBasicDetailsVM();
            if (kyc != null)
            {
                kycVm.KYCID = kyc.KYCID;
                kycVm.KycType = kyc.KycType;
                kycVm.isPanAdharExist = true;
                kycVm.Age = kyc.Age;
                kycVm.AppFName = kyc.AppFName;
                kycVm.ApplicantPrefix = kyc.ApplicantPrefix.Trim();
                kycVm.FatherPrefix = kyc.FatherPrefix.Trim();
                kycVm.ResidenceCode = kyc.ResidenceCode;
                kycVm.AppliedDate = kyc.AppliedDate;
                kycVm.AppLName = kyc.AppLName;
                kycVm.AppMName = kyc.AppMName;
                kycVm.ImageName = kyc.ImageName;
                kycVm.BirthDate = kyc.BirthDate;
                kycVm.BldgHouseName = kyc.BldgHouseName;
                kycVm.BldgPlotNo = kyc.BldgPlotNo;
                kycVm.BranchID = kyc.BranchID;
                kycVm.CmpID = kyc.CmpID;
                kycVm.CreatedBy = kyc.CreatedBy;
                kycVm.CreatedDate = kyc.CreatedDate;
                kycVm.CustomerID = kyc.CustomerID;
                kycVm.Designation = kyc.Designation;
                kycVm.EmailID = kyc.EmailID;
                kycVm.ExistingPLCaseNo = kyc.ExistingPLCaseNo;
                kycVm.FYID = kyc.FYID;
                kycVm.Gender = kyc.Gender;
                kycVm.IndustriesType = kyc.IndustriesType;
                kycVm.isActive = true;
                kycVm.Landmark = kyc.Landmark;
                kycVm.MaritalStatus = kyc.MaritalStatus;
                kycVm.MobileNo = kyc.MobileNo;
                kycVm.NomAddress = kyc.NomAddress;
                kycVm.NomFName = kyc.NomFName;
                kycVm.NomLName = kyc.NomLName;
                kycVm.NomMName = kyc.NomMName;
                kycVm.NomRelation = kyc.NomRelation;
                kycVm.Occupation = kyc.Occupation;
                kycVm.OperatorID = kyc.OperatorID;
                kycVm.OrganizationName = kyc.OrganizationName;
                kycVm.PANNo = kyc.PANNo;
                kycVm.PresentIncome = kyc.PresentIncome;
                kycVm.Road = kyc.Road;
                kycVm.RoomBlockNo = kyc.RoomBlockNo;
                kycVm.SourceofApplicationID = kyc.SourceofApplicationID;
                kycVm.SourceType = _context.Mst_SourceofApplication.Where(x => x.Soa_Id == kycVm.SourceofApplicationID).Select(x => x.Soa_Category).FirstOrDefault();
                kycVm.Spouse = kyc.Spouse;
                kycVm.TelephoneNo = kyc.TelephoneNo;
                kycVm.UpdatedBy = kyc.UpdatedBy;
                kycVm.UpdatedDate = DateTime.Now;
                kycVm.KYCDate = DateTime.Now;
                kycVm.AdhaarNo = kyc.AdhaarNo;
                kycVm.ApplicationNo = kyc.ApplicationNo;
                kycVm.MotherName = kyc.MotherName;
                kycVm.Father_Spouse = kyc.Father_Spouse;
                kycVm.CKYCNo = kyc.CKYCNo;
                kycVm.OccupationOther = kyc.OccupationOther;
                kycVm.IndustryOther = kyc.IndustryOther;
                kycVm.NomineeMobileNo = kyc.NomineeMobileNo;
                kycVm.NomineePanNo = kyc.NomineePanNo;
                kycVm.NomineeAdharNo = kyc.NomineeAdharNo;
                kycVm.PinCode = kyc.PinCode;
                kycVm.Distance = kyc.Distance;
                kycVm.Trans_KYCAddresses = kyc.Trans_KYCAddresses
                    .Select(
                    x => new KYCAddressesVM()
                    {
                        AddressCategory = x.AddressCategory,
                        BuildingHouseName = x.BuildingHouseName,
                        BuildingPlotNo = x.BuildingPlotNo,
                        CreatedDate = x.CreatedDate,
                        Distance_km = x.Distance_km,
                        ID = x.ID,
                        KYCID = x.KYCID,
                        NearestLandmark = x.NearestLandmark,
                        PinCode = (int)x.PinCode,
                        ResidenceCode = x.ResidenceCode,
                        Road = x.Road,
                        RoomBlockNo = x.RoomBlockNo,
                    }).ToList();

                var docuploaddetails = (from a in _context.Trn_DocUploadDetails
                                        join b in _context.Mst_DocumentType on a.DocumentTypeId equals b.Id
                                        join c in _context.tblDocumentMasters on a.DocumentId equals c.DocumentID
                                        where a.KycId == kycVm.KYCID && a.Status != "Rejected"
                                        select new DocumentUploadDetailsVM()
                                        {
                                            ID = a.Id,
                                            DocumentTypeId = a.DocumentTypeId,
                                            DocumentTypeName = b.Name,
                                            DocumentName = c.DocumentName,
                                            DocumentId = a.DocumentId,
                                            ExpiryDate = a.ExpiryDate,
                                            FileName = a.FileName,
                                            FileExtension = a.ContentType,
                                            KycId = a.KycId,
                                            Status = a.Status,
                                            VerifiedBy = a.VerifiedBy,
                                            SpecifyOther = a.SpecifyOther,
                                            NameonDocument = a.NameonDocument,
                                            ReasonForRejection = a.ReasonForRejection
                                        }).ToList();

                kycVm.DocumentUploadList = docuploaddetails;
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
            int branchid = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
            var fyid = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);

            var kyc = _context.TGLKYC_BasicDetails
                .Include("Trans_KYCAddresses")
                .Where(x => x.AdhaarNo == AdharNo &&
                x.BranchID == branchid &&
                x.FYID == fyid)
                .OrderByDescending(x => x.AppliedDate)
                .FirstOrDefault();
            KYCBasicDetailsVM kycVm = new KYCBasicDetailsVM();
            if (kyc != null)
            {
                kycVm.KYCID = kyc.KYCID;
                kycVm.KycType = kyc.KycType;
                kycVm.isPanAdharExist = true;
                kycVm.Age = kyc.Age;
                kycVm.AppFName = kyc.AppFName;
                kycVm.AppliedDate = kyc.AppliedDate;
                kycVm.ApplicantPrefix = kyc.ApplicantPrefix.Trim();
                kycVm.FatherPrefix = kyc.FatherPrefix.Trim();
                kycVm.ResidenceCode = kyc.ResidenceCode;
                kycVm.AppLName = kyc.AppLName;
                kycVm.AppMName = kyc.AppMName;
                kycVm.ImageName = kyc.ImageName;
                kycVm.BirthDate = kyc.BirthDate;
                kycVm.BldgHouseName = kyc.BldgHouseName;
                kycVm.BldgPlotNo = kyc.BldgPlotNo;
                kycVm.BranchID = kyc.BranchID;
                kycVm.CmpID = kyc.CmpID;
                kycVm.CreatedBy = kyc.CreatedBy;
                kycVm.CreatedDate = kyc.CreatedDate;
                kycVm.CustomerID = kyc.CustomerID;
                kycVm.Designation = kyc.Designation;
                kycVm.EmailID = kyc.EmailID;
                kycVm.ExistingPLCaseNo = kyc.ExistingPLCaseNo;
                kycVm.FYID = kyc.FYID;
                kycVm.Gender = kyc.Gender;
                kycVm.IndustriesType = kyc.IndustriesType;
                kycVm.isActive = true;
                kycVm.Landmark = kyc.Landmark;
                kycVm.MaritalStatus = kyc.MaritalStatus;
                kycVm.MobileNo = kyc.MobileNo;
                kycVm.NomAddress = kyc.NomAddress;
                kycVm.NomFName = kyc.NomFName;
                kycVm.NomLName = kyc.NomLName;
                kycVm.NomMName = kyc.NomMName;
                kycVm.NomRelation = kyc.NomRelation;
                kycVm.Occupation = kyc.Occupation;
                kycVm.OperatorID = kyc.OperatorID;
                kycVm.OrganizationName = kyc.OrganizationName;
                kycVm.PANNo = kyc.PANNo;
                kycVm.PresentIncome = kyc.PresentIncome;
                kycVm.Road = kyc.Road;
                kycVm.RoomBlockNo = kyc.RoomBlockNo;
                kycVm.Spouse = kyc.Spouse;
                kycVm.TelephoneNo = kyc.TelephoneNo;
                kycVm.UpdatedBy = kyc.UpdatedBy;
                kycVm.UpdatedDate = DateTime.Now;
                kycVm.KYCDate = DateTime.Now;
                kycVm.AdhaarNo = kyc.AdhaarNo;
                kycVm.ApplicationNo = kyc.ApplicationNo;
                kycVm.SourceofApplicationID = kyc.SourceofApplicationID;
                kycVm.SourceType = _context.Mst_SourceofApplication.Where(x => x.Soa_Id == kycVm.SourceofApplicationID).Select(x => x.Soa_Category).FirstOrDefault();
                kycVm.MotherName = kyc.MotherName;
                kycVm.Father_Spouse = kyc.Father_Spouse;
                kycVm.CKYCNo = kyc.CKYCNo;
                kycVm.OccupationOther = kyc.OccupationOther;
                kycVm.IndustryOther = kyc.IndustryOther;
                kycVm.NomineeMobileNo = kyc.NomineeMobileNo;
                kycVm.NomineePanNo = kyc.NomineePanNo;
                kycVm.NomineeAdharNo = kyc.NomineeAdharNo;
                kycVm.PinCode = kyc.PinCode;
                kycVm.Distance = kyc.Distance;
                kycVm.FatherPrefix = kyc.FatherPrefix;
                kycVm.Trans_KYCAddresses = kyc.Trans_KYCAddresses
                   .Select(
                   x => new KYCAddressesVM()
                   {
                       AddressCategory = x.AddressCategory,
                       BuildingHouseName = x.BuildingHouseName,
                       BuildingPlotNo = x.BuildingPlotNo,
                       CreatedDate = x.CreatedDate,
                       Distance_km = x.Distance_km,
                       ID = x.ID,
                       KYCID = x.KYCID,
                       NearestLandmark = x.NearestLandmark,
                       PinCode = (int)x.PinCode,
                       ResidenceCode = x.ResidenceCode,
                       Road = x.Road,
                       RoomBlockNo = x.RoomBlockNo,
                   }).ToList();

                var docuploaddetails = (from a in _context.Trn_DocUploadDetails
                                        join b in _context.Mst_DocumentType on a.DocumentTypeId equals b.Id
                                        join c in _context.tblDocumentMasters on a.DocumentId equals c.DocumentID
                                        where a.KycId == kycVm.KYCID
                                        select new DocumentUploadDetailsVM()
                                        {
                                            ID = a.Id,
                                            DocumentTypeId = a.DocumentTypeId,
                                            DocumentTypeName = b.Name,
                                            DocumentName = c.DocumentName,
                                            DocumentId = a.DocumentId,
                                            ExpiryDate = a.ExpiryDate,
                                            FileName = a.FileName,
                                            FileExtension = a.ContentType,
                                            KycId = a.KycId,
                                            Status = a.Status,
                                            VerifiedBy = a.VerifiedBy,
                                            SpecifyOther = a.SpecifyOther,
                                            NameonDocument = a.NameonDocument,
                                            ReasonForRejection = a.ReasonForRejection
                                        }).ToList();

                kycVm.DocumentUploadList = docuploaddetails;
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
        public TGLKYC_BasicDetails GetImageById(int id)
        {
            int branchid = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
            int fyid = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);

            return _context.TGLKYC_BasicDetails.Where(x => x.KYCID == id &&
            x.BranchID == branchid &&
            x.FYID == fyid &&
            x.isActive == true).FirstOrDefault();
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
            //&& (DateTime.Now - x.CreatedDate.Value).TotalMinutes <= 5
            string response = String.Empty;
            var storedOTP = _context.tbl_KYCMobileOTP.
                            Where(x => x.Mobile == mobile && x.CustomerId == customerId)
                            //&& DbFunctions.DiffMinutes(x.CreatedDate, DateTime.Now) <= 15)
                            .OrderByDescending(x => x.CreatedDate)
                            //.Select(x => x.OTP)
                            .FirstOrDefault();

            if (storedOTP != null)
            {
                if (otp == storedOTP.OTP)
                {
                    response = "Mobile Verified Successfully!";
                }
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
            var fillAddressByPinCode = (from aa in _context.Mst_PinCode
                                        join bb in _context.tblZonemasters on aa.Pc_ZoneId equals bb.ZoneID
                                        join cc in _context.tblCityMasters on aa.Pc_CityId equals cc.CityID
                                        join dd in _context.tblStateMasters on cc.StateID equals dd.StateID
                                        where aa.Pc_Id == id
                                        select new FillAddressByPinCode()
                                        {
                                            AreaName = aa.Pc_AreaName,
                                            ZoneName = bb.Zone,
                                            ZoneID = bb.ZoneID,
                                            CityId = cc.CityID,
                                            CityName = cc.CityName,
                                            StateID = dd.StateID,
                                            StateName = dd.StateName
                                        }).FirstOrDefault();

            return fillAddressByPinCode;
        }
        /// <summary>
        /// get list of pincodes from db
        /// </summary>
        /// <returns></returns>
        public dynamic GetPincodeMasterList()
        {
            var pincodelist = _context.Mst_PinCode.Select(x => new
            {
                PcId = x.Pc_Id,
                PincodeWithArea = x.Pc_Desc + "(" + x.Pc_AreaName + ")"
            }).ToList();
            return pincodelist;
        }

        public IList<Mst_PinCode> GetAllPincodes()
        {
            return _context.Mst_PinCode.ToList();
        }

        public string GetSourceType(int id)
        {
            return _context.Mst_SourceofApplication.Where(x => x.Soa_Id == id).Select(x => x.Soa_Category).FirstOrDefault();
        }
    }
}
