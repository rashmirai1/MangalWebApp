﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MangalWeb.Model.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class MangalDBNewEntities : DbContext
    {
        public MangalDBNewEntities()
            : base("name=MangalDBNewEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblBankMaster> tblBankMasters { get; set; }
        public virtual DbSet<Bharat_tbl_SentJVCustHistory> Bharat_tbl_SentJVCustHistory { get; set; }
        public virtual DbSet<Bharat01012017tbl_SentJVCustHistory> Bharat01012017tbl_SentJVCustHistory { get; set; }
        public virtual DbSet<bharatTGLGoldInOutDetail> bharatTGLGoldInOutDetails { get; set; }
        public virtual DbSet<ImageKyc> ImageKycs { get; set; }
        public virtual DbSet<Imagestore> Imagestores { get; set; }
        public virtual DbSet<KycImageStore> KycImageStores { get; set; }
        public virtual DbSet<M_Words> M_Words { get; set; }
        public virtual DbSet<new1bharatTGLGoldStock> new1bharatTGLGoldStock { get; set; }
        public virtual DbSet<newauthorize> newauthorizes { get; set; }
        public virtual DbSet<Newbharat> Newbharats { get; set; }
        public virtual DbSet<newbharatTGLGoldInOutDetail> newbharatTGLGoldInOutDetails { get; set; }
        public virtual DbSet<newbharatTGLGoldStock> newbharatTGLGoldStocks { get; set; }
        public virtual DbSet<tbl_GLDocumentMaster> tbl_GLDocumentMaster { get; set; }
        public virtual DbSet<tbl_SentJVCustHistory> tbl_SentJVCustHistory { get; set; }
        public virtual DbSet<tblCreateCompanyMaster> tblCreateCompanyMasters { get; set; }
        public virtual DbSet<tblLogin> tblLogins { get; set; }
        public virtual DbSet<TestDB> TestDBs { get; set; }
        public virtual DbSet<TGL_DefaultOSPercentage> TGL_DefaultOSPercentage { get; set; }
        public virtual DbSet<TGL_FormAuthorizationDetails> TGL_FormAuthorizationDetails { get; set; }
        public virtual DbSet<TGL_FormDetails> TGL_FormDetails { get; set; }
        public virtual DbSet<TGLCash_Denomination_Details> TGLCash_Denomination_Details { get; set; }
        public virtual DbSet<TGLCashAuth_BasicDetails> TGLCashAuth_BasicDetails { get; set; }
        public virtual DbSet<TGLCashAuth_Denomination_Details> TGLCashAuth_Denomination_Details { get; set; }
        public virtual DbSet<TGLCashAuth_ReceiptDetails> TGLCashAuth_ReceiptDetails { get; set; }
        public virtual DbSet<TGLCashInOutDetail> TGLCashInOutDetails { get; set; }
        public virtual DbSet<TGLDefault_OSLevel> TGLDefault_OSLevel { get; set; }
        public virtual DbSet<TGLExcessAmount_Login> TGLExcessAmount_Login { get; set; }
        public virtual DbSet<TGLGoldInOutDetail> TGLGoldInOutDetails { get; set; }
        public virtual DbSet<TGLGoldStock> TGLGoldStocks { get; set; }
        public virtual DbSet<TGLInterest_Details> TGLInterest_Details { get; set; }
        public virtual DbSet<TGLInwardForm_BasicDetails> TGLInwardForm_BasicDetails { get; set; }
        public virtual DbSet<TGLInwardForm_DocDetails> TGLInwardForm_DocDetails { get; set; }
        public virtual DbSet<TGLInwardForm_GoldDetails> TGLInwardForm_GoldDetails { get; set; }
        public virtual DbSet<TGLKYC_DocumentDetails> TGLKYC_DocumentDetails { get; set; }
        public virtual DbSet<TGLKYC_SourceOfApplication> TGLKYC_SourceOfApplication { get; set; }
        public virtual DbSet<TGLOutwardForm_BasicDetails> TGLOutwardForm_BasicDetails { get; set; }
        public virtual DbSet<TGLOutwardForm_DocDetails> TGLOutwardForm_DocDetails { get; set; }
        public virtual DbSet<TGLOutwardForm_GoldDetails> TGLOutwardForm_GoldDetails { get; set; }
        public virtual DbSet<TGLSmsHistory> TGLSmsHistories { get; set; }
        public virtual DbSet<tbl_CountryMaster> tbl_CountryMaster { get; set; }
        public virtual DbSet<tblDocumentMaster> tblDocumentMasters { get; set; }
        public virtual DbSet<tblINV_ItemMaster> tblINV_ItemMaster { get; set; }
        public virtual DbSet<tblPenaltySlabtMaster> tblPenaltySlabtMasters { get; set; }
        public virtual DbSet<tblSchemeMaster> tblSchemeMasters { get; set; }
        public virtual DbSet<tblSchemeTransMaster> tblSchemeTransMasters { get; set; }
        public virtual DbSet<tblZonemaster> tblZonemasters { get; set; }
        public virtual DbSet<tblSchemeTransMonth> tblSchemeTransMonths { get; set; }
        public virtual DbSet<tblCityMaster> tblCityMasters { get; set; }
        public virtual DbSet<Mst_PinCode> Mst_PinCode { get; set; }
        public virtual DbSet<Mst_DocumentType> Mst_DocumentType { get; set; }
        public virtual DbSet<Mst_ChildDeviation> Mst_ChildDeviation { get; set; }
        public virtual DbSet<Mst_GstMaster> Mst_GstMaster { get; set; }
        public virtual DbSet<Mst_ParentDeviation> Mst_ParentDeviation { get; set; }
        public virtual DbSet<Mst_PurityMaster> Mst_PurityMaster { get; set; }
        public virtual DbSet<Mst_Reason> Mst_Reason { get; set; }
        public virtual DbSet<Mst_SchemePurity> Mst_SchemePurity { get; set; }
        public virtual DbSet<tblaccountmaster> tblaccountmasters { get; set; }
        public virtual DbSet<tblGroupMaster> tblGroupMasters { get; set; }
        public virtual DbSet<tblPrimaryGroup> tblPrimaryGroups { get; set; }
        public virtual DbSet<tbl_GLChargeMaster_BasicInfo> tbl_GLChargeMaster_BasicInfo { get; set; }
        public virtual DbSet<tbl_GLChargeMaster_Details> tbl_GLChargeMaster_Details { get; set; }
        public virtual DbSet<tblFinancialyear> tblFinancialyears { get; set; }
        public virtual DbSet<tblCompanyBranchMaster> tblCompanyBranchMasters { get; set; }
        public virtual DbSet<Mst_PenaltySlab> Mst_PenaltySlab { get; set; }
        public virtual DbSet<Mst_SourceofApplication> Mst_SourceofApplication { get; set; }
        public virtual DbSet<Mst_AuditCategory> Mst_AuditCategory { get; set; }
        public virtual DbSet<Mst_AuditCheckList> Mst_AuditCheckList { get; set; }
        public virtual DbSet<Mst_Product> Mst_Product { get; set; }
        public virtual DbSet<Mst_ProductRate> Mst_ProductRate { get; set; }
        public virtual DbSet<Mst_ProductRateDetails> Mst_ProductRateDetails { get; set; }
        public virtual DbSet<tblItemMaster> tblItemMasters { get; set; }
        public virtual DbSet<TSchemeMaster_BasicDetails> TSchemeMaster_BasicDetails { get; set; }
        public virtual DbSet<tbl_UserCategory> tbl_UserCategory { get; set; }
        public virtual DbSet<Trn_DocumentUpload> Trn_DocumentUpload { get; set; }
        public virtual DbSet<Mst_BranchType> Mst_BranchType { get; set; }
        public virtual DbSet<User_Category_Hierarchy> User_Category_Hierarchy { get; set; }
        public virtual DbSet<tbl_KYCMobileOTP> tbl_KYCMobileOTP { get; set; }
        public virtual DbSet<Trans_KYCAddresses> Trans_KYCAddresses { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<UserAuthorization> UserAuthorizations { get; set; }
        public virtual DbSet<Trn_DocUploadDetails> Trn_DocUploadDetails { get; set; }
        public virtual DbSet<TGLKYC_BasicDetails> TGLKYC_BasicDetails { get; set; }
        public virtual DbSet<Trn_RequestForm> Trn_RequestForm { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<tbl_CKycState> tbl_CKycState { get; set; }
        public virtual DbSet<tblStateMaster> tblStateMasters { get; set; }
        public virtual DbSet<TSchemeMaster_EffectiveROI> TSchemeMaster_EffectiveROI { get; set; }
        public virtual DbSet<tbl_PreSanctionDetails> tbl_PreSanctionDetails { get; set; }
        public virtual DbSet<tbl_ResidenceVerification> tbl_ResidenceVerification { get; set; }
        public virtual DbSet<tblStandardEndTime> tblStandardEndTimes { get; set; }
    
        [DbFunction("MangalDBNewEntities", "SplitValue")]
        public virtual IQueryable<SplitValue_Result> SplitValue(string @string, string delimiter)
        {
            var stringParameter = @string != null ?
                new ObjectParameter("String", @string) :
                new ObjectParameter("String", typeof(string));
    
            var delimiterParameter = delimiter != null ?
                new ObjectParameter("Delimiter", delimiter) :
                new ObjectParameter("Delimiter", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<SplitValue_Result>("[MangalDBNewEntities].[SplitValue](@String, @Delimiter)", stringParameter, delimiterParameter);
        }
    
        [DbFunction("MangalDBNewEntities", "SplitWords")]
        public virtual IQueryable<SplitWords_Result> SplitWords(string text)
        {
            var textParameter = text != null ?
                new ObjectParameter("text", text) :
                new ObjectParameter("text", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<SplitWords_Result>("[MangalDBNewEntities].[SplitWords](@text)", textParameter);
        }
    
        public virtual int generateFinancialYear(Nullable<int> financialYearId, ObjectParameter message)
        {
            var financialYearIdParameter = financialYearId.HasValue ?
                new ObjectParameter("FinancialYearId", financialYearId) :
                new ObjectParameter("FinancialYearId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("generateFinancialYear", financialYearIdParameter, message);
        }
    
        public virtual ObjectResult<GetKYCDetailsForDocument_Result> GetKYCDetailsForDocument()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetKYCDetailsForDocument_Result>("GetKYCDetailsForDocument");
        }
    
        public virtual ObjectResult<GetKYCDetailsForDocumentById_Result> GetKYCDetailsForDocumentById(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetKYCDetailsForDocumentById_Result>("GetKYCDetailsForDocumentById", idParameter);
        }
    
        public virtual ObjectResult<GetDocumentUpload_Result> GetDocumentUpload()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetDocumentUpload_Result>("GetDocumentUpload");
        }
    
        [DbFunction("MangalDBNewEntities", "SplitValue1")]
        public virtual IQueryable<string> SplitValue1(string @string, string delimiter)
        {
            var stringParameter = @string != null ?
                new ObjectParameter("String", @string) :
                new ObjectParameter("String", typeof(string));
    
            var delimiterParameter = delimiter != null ?
                new ObjectParameter("Delimiter", delimiter) :
                new ObjectParameter("Delimiter", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<string>("[MangalDBNewEntities].[SplitValue1](@String, @Delimiter)", stringParameter, delimiterParameter);
        }
    
        [DbFunction("MangalDBNewEntities", "SplitWords1")]
        public virtual IQueryable<SplitWords1_Result> SplitWords1(string text)
        {
            var textParameter = text != null ?
                new ObjectParameter("text", text) :
                new ObjectParameter("text", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<SplitWords1_Result>("[MangalDBNewEntities].[SplitWords1](@text)", textParameter);
        }
    
        public virtual ObjectResult<GetDocumentUploadById_Result> GetDocumentUploadById(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetDocumentUploadById_Result>("GetDocumentUploadById", idParameter);
        }
    
        public virtual ObjectResult<T_GetFormDetails_FormIdWise_Result> T_GetFormDetails_FormIdWise(Nullable<int> formId, Nullable<int> userId)
        {
            var formIdParameter = formId.HasValue ?
                new ObjectParameter("FormId", formId) :
                new ObjectParameter("FormId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<T_GetFormDetails_FormIdWise_Result>("T_GetFormDetails_FormIdWise", formIdParameter, userIdParameter);
        }
    
        public virtual ObjectResult<T_UserAuthorization_Forms_Result> T_UserAuthorization_Forms(Nullable<int> parentID, Nullable<int> userID, Nullable<int> userCategoryID)
        {
            var parentIDParameter = parentID.HasValue ?
                new ObjectParameter("ParentID", parentID) :
                new ObjectParameter("ParentID", typeof(int));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var userCategoryIDParameter = userCategoryID.HasValue ?
                new ObjectParameter("UserCategoryID", userCategoryID) :
                new ObjectParameter("UserCategoryID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<T_UserAuthorization_Forms_Result>("T_UserAuthorization_Forms", parentIDParameter, userIDParameter, userCategoryIDParameter);
        }
    
        public virtual int T_Update_UserAuthorization_DetailsUserWise(Nullable<int> index, Nullable<int> userID, Nullable<int> parentID, Nullable<int> formID, Nullable<bool> isVisible, Nullable<bool> isEdit, Nullable<bool> isView, Nullable<bool> isSave, Nullable<bool> isDelete)
        {
            var indexParameter = index.HasValue ?
                new ObjectParameter("index", index) :
                new ObjectParameter("index", typeof(int));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var parentIDParameter = parentID.HasValue ?
                new ObjectParameter("ParentID", parentID) :
                new ObjectParameter("ParentID", typeof(int));
    
            var formIDParameter = formID.HasValue ?
                new ObjectParameter("FormID", formID) :
                new ObjectParameter("FormID", typeof(int));
    
            var isVisibleParameter = isVisible.HasValue ?
                new ObjectParameter("isVisible", isVisible) :
                new ObjectParameter("isVisible", typeof(bool));
    
            var isEditParameter = isEdit.HasValue ?
                new ObjectParameter("isEdit", isEdit) :
                new ObjectParameter("isEdit", typeof(bool));
    
            var isViewParameter = isView.HasValue ?
                new ObjectParameter("isView", isView) :
                new ObjectParameter("isView", typeof(bool));
    
            var isSaveParameter = isSave.HasValue ?
                new ObjectParameter("isSave", isSave) :
                new ObjectParameter("isSave", typeof(bool));
    
            var isDeleteParameter = isDelete.HasValue ?
                new ObjectParameter("isDelete", isDelete) :
                new ObjectParameter("isDelete", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("T_Update_UserAuthorization_DetailsUserWise", indexParameter, userIDParameter, parentIDParameter, formIDParameter, isVisibleParameter, isEditParameter, isViewParameter, isSaveParameter, isDeleteParameter);
        }
    
        public virtual ObjectResult<T_Forms_UserAuthorization_ParentPage_Result> T_Forms_UserAuthorization_ParentPage(Nullable<int> userId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<T_Forms_UserAuthorization_ParentPage_Result>("T_Forms_UserAuthorization_ParentPage", userIdParameter);
        }
    
        public virtual ObjectResult<T_GetAuthorizeSubPagesList_PrentidWise_Result> T_GetAuthorizeSubPagesList_PrentidWise(Nullable<int> userID, Nullable<int> parentID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var parentIDParameter = parentID.HasValue ?
                new ObjectParameter("ParentID", parentID) :
                new ObjectParameter("ParentID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<T_GetAuthorizeSubPagesList_PrentidWise_Result>("T_GetAuthorizeSubPagesList_PrentidWise", userIDParameter, parentIDParameter);
        }
    
        public virtual ObjectResult<GetKYCDetailsForRequestForm_Result> GetKYCDetailsForRequestForm(Nullable<int> kycId)
        {
            var kycIdParameter = kycId.HasValue ?
                new ObjectParameter("KycId", kycId) :
                new ObjectParameter("KycId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetKYCDetailsForRequestForm_Result>("GetKYCDetailsForRequestForm", kycIdParameter);
        }
    }
}
