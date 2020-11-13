﻿using MangalWeb.Model.Entity;
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
            foreach (var item in kYCBasicDetailsVMs)
            {
                DateTime now = DateTime.Now;
            }
            return kYCBasicDetailsVMs;
        }

        public void SaveUpdateRecord(ResidenceVerificationVM model)
        {
            try
            {
                tblResidenceVerification residenceVerification = new tblResidenceVerification();
                residenceVerification.TransactionId = model.TransactionId;
                residenceVerification.TransactionDate = Convert.ToDateTime(model.AppliedDate);
                residenceVerification.KycId = model.KycId;
                residenceVerification.PreSanctionId = model.PreSanctionId;
                residenceVerification.DateofVisit = model.DateofVisit;
                residenceVerification.TimeofVisit = model.TimeofVisit;
                residenceVerification.PersonVisitedName = model.PersonVisitedName;
                residenceVerification.RelationWithCustomer = model.RelationWithCustomer;
                residenceVerification.FamilyMemberDetails = model.FamilyMemberDetails;
                residenceVerification.AddressCategory = "02";
                residenceVerification.ResidenceCode = model.ResidenceCode;
                residenceVerification.BldgHouseName = model.BldgHouseName;
                residenceVerification.BldgPlotNo = model.BldgPlotNo;
                residenceVerification.Road = model.Road;
                residenceVerification.RoomBlockNo = model.RoomBlockNo;
                residenceVerification.Landmark = model.Landmark;
                residenceVerification.Distance = model.Distance;
                residenceVerification.PinCode = model.PinCode;
                residenceVerification.ResidingAtThisAddress_Months = model.ResidingAtThisAddress_Months;
                residenceVerification.ResidingAtThisAddress_Years = model.ResidingAtThisAddress_Years;
                residenceVerification.UserId = model.UserId;
                residenceVerification.CreatedBy = model.CreatedBy;
                residenceVerification.CreatedDate = DateTime.Now;
                residenceVerification.Comments = model.Comments;
                residenceVerification.IsActive = true;
                _context.tblResidenceVerifications.Add(residenceVerification);
                _context.SaveChanges();

                foreach (var p in model.DocumentUploadList)
                {
                    var docuptrn = new Trn_DocUploadDetails
                    {
                        KycId = model.KycId,
                        DocumentTypeId = (int)p.DocumentTypeId,
                        DocumentId = (int)p.DocumentId,
                        ExpiryDate = p.ExpiryDate,
                        FileName = p.FileName,
                        ContentType = p.FileExtension,
                        UploadFile = p.UploadDocName,
                        Status = "Pending",
                        SpecifyOther = p.SpecifyOther,
                        NameonDocument = p.NameonDocument
                    };
                    _context.Trn_DocUploadDetails.Add(docuptrn);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResidenceVerificationVM GetCustomerById(int id)
        {
            ResidenceVerificationVM residenceVerificationVM = new ResidenceVerificationVM();
            int branchid = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
            int fyid = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);

            var kYCBasicDetails = _context.Database.SqlQuery<KYCBasicDetailsVM>("GetCustomerById @id,@BranchId,@FyId",
                new SqlParameter("@id", id),
                new SqlParameter("@BranchId", branchid),
                new SqlParameter("@FyId", fyid)).FirstOrDefault();
            residenceVerificationVM = ToViewModelResidenceVerification(kYCBasicDetails);
            return residenceVerificationVM;
        }

        #region ToViewModelPreSanction
        public ResidenceVerificationVM ToViewModelResidenceVerification(KYCBasicDetailsVM model)
        {
            ResidenceVerificationVM residenceVerificationVM = new ResidenceVerificationVM();

            var fillAddressByPinCode = (from aa in _context.Mst_PinCode
                                        join bb in _context.tblZonemasters on aa.Pc_ZoneId equals bb.ZoneID
                                        join cc in _context.tblCityMasters on aa.Pc_CityId equals cc.CityID
                                        join dd in _context.tblStateMasters on cc.StateID equals dd.StateID
                                        where aa.Pc_Id == model.PinCode
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

            residenceVerificationVM.ApplicationNo = model.ApplicationNo;
            residenceVerificationVM.AppliedDate = model.AppliedDate.ToShortDateString();
            residenceVerificationVM.CustomerId = model.CustomerID;
            residenceVerificationVM.PreSanctionId = model.PreSanctionId;
            residenceVerificationVM.AddressCategory = "02";
            residenceVerificationVM.BldgHouseName = model.BldgHouseName;
            residenceVerificationVM.BldgPlotNo = model.BldgPlotNo;
            residenceVerificationVM.Distance = model.Distance;
            residenceVerificationVM.Landmark = model.Landmark;
            residenceVerificationVM.PinCode = model.PinCode;
            residenceVerificationVM.ResidenceCode = model.ResidenceCode;
            residenceVerificationVM.Road = model.Road;
            residenceVerificationVM.RoomBlockNo = model.RoomBlockNo;
            residenceVerificationVM.city = fillAddressByPinCode.CityName;
            residenceVerificationVM.zone = fillAddressByPinCode.ZoneName;
            residenceVerificationVM.state =fillAddressByPinCode.StateName;
            residenceVerificationVM.Area = fillAddressByPinCode.AreaName;

            var docuploaddetails = (from a in _context.Trn_DocUploadDetails
                                    join b in _context.Mst_DocumentType on a.DocumentTypeId equals b.Id
                                    join c in _context.tblDocumentMasters on a.DocumentId equals c.DocumentID
                                    where a.KycId == model.KYCID && a.Status != "Rejected"
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

            residenceVerificationVM.DocumentUploadList = docuploaddetails;
            return residenceVerificationVM;
        }
        #endregion

        public ResidenceVerificationVM GetResidenceVerificationById(int id)
        {
            ResidenceVerificationVM residenceVerificationVM = new ResidenceVerificationVM();
            int branchid = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
            int fyid = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);

            var kYCBasicDetails = _context.Database.SqlQuery<KYCBasicDetailsVM>("GetResidenceVerificationById @id,@BranchId,@FyId",
                new SqlParameter("@id", id),
                new SqlParameter("@BranchId", branchid),
                new SqlParameter("@FyId", fyid)).FirstOrDefault();

            residenceVerificationVM = ToViewModelResidenceVerification(kYCBasicDetails);
            return residenceVerificationVM;
        }

        public List<UserDetail> GetAllRMByBranch()
        {
            return _context.UserDetails.ToList();
        }

        public int GetMaxId()
        {
            return 1;// _context.tbl_ResidenceVerification.Any() ? _context.tbl_ResidenceVerification.Max(x => x.Id) + 1 : 1;
        }

        public int GetDocumentID(int kycId)
        {
            int docId;
            docId = _context.Trn_DocumentUpload.Where(x => x.KycId == kycId)
                                          .OrderByDescending(x => x.DocId)
                                          .Select(x => x.DocId).FirstOrDefault();
            return docId;
        }

        public UserViewModel FillEmployeeDetailsById(int Id)
        {
            var result = _context.UserDetails.Include("tbl_UserCategory").Where(x => x.UserID == Id).FirstOrDefault();
            UserViewModel user = new UserViewModel();
            user.EmployeeCode = result.EmployeeCode;
            user.UserName = result.tbl_UserCategory.Name;
            return user;
        }
    }
}
