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
        public virtual DbSet<tbl_GLChargeMaster_BasicInfo> tbl_GLChargeMaster_BasicInfo { get; set; }
        public virtual DbSet<tbl_GLChargeMaster_Details> tbl_GLChargeMaster_Details { get; set; }
        public virtual DbSet<tbl_GLDocumentMaster> tbl_GLDocumentMaster { get; set; }
        public virtual DbSet<tbl_SentJVCustHistory> tbl_SentJVCustHistory { get; set; }
        public virtual DbSet<tbl_UserLogin> tbl_UserLogin { get; set; }
        public virtual DbSet<tblCompanyBranchMaster> tblCompanyBranchMasters { get; set; }
        public virtual DbSet<tblCreateCompanyMaster> tblCreateCompanyMasters { get; set; }
        public virtual DbSet<tblFinancialyear> tblFinancialyears { get; set; }
        public virtual DbSet<tblItemMaster> tblItemMasters { get; set; }
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
        public virtual DbSet<TGLKYC_BasicDetails> TGLKYC_BasicDetails { get; set; }
        public virtual DbSet<TGLKYC_DocumentDetails> TGLKYC_DocumentDetails { get; set; }
        public virtual DbSet<TGLKYC_SourceOfApplication> TGLKYC_SourceOfApplication { get; set; }
        public virtual DbSet<TGLOutwardForm_BasicDetails> TGLOutwardForm_BasicDetails { get; set; }
        public virtual DbSet<TGLOutwardForm_DocDetails> TGLOutwardForm_DocDetails { get; set; }
        public virtual DbSet<TGLOutwardForm_GoldDetails> TGLOutwardForm_GoldDetails { get; set; }
        public virtual DbSet<TGLSmsHistory> TGLSmsHistories { get; set; }
        public virtual DbSet<TSchemeMaster_BasicDetails> TSchemeMaster_BasicDetails { get; set; }
        public virtual DbSet<TSchemeMaster_EffectiveROI> TSchemeMaster_EffectiveROI { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<UserFormAuthentication> UserFormAuthentications { get; set; }
        public virtual DbSet<UserFormAuthorisationDetail> UserFormAuthorisationDetails { get; set; }
        public virtual DbSet<UserFormDetail> UserFormDetails { get; set; }
        public virtual DbSet<UserMenuDetail> UserMenuDetails { get; set; }
        public virtual DbSet<UserModuleAuthentication> UserModuleAuthentications { get; set; }
        public virtual DbSet<UserModuleDetail> UserModuleDetails { get; set; }
        public virtual DbSet<UserTypeDetail> UserTypeDetails { get; set; }
        public virtual DbSet<tbl_CountryMaster> tbl_CountryMaster { get; set; }
        public virtual DbSet<tblDocumentMaster> tblDocumentMasters { get; set; }
        public virtual DbSet<tblINV_ItemMaster> tblINV_ItemMaster { get; set; }
        public virtual DbSet<tblPenaltySlabtMaster> tblPenaltySlabtMasters { get; set; }
        public virtual DbSet<tblSchemeMaster> tblSchemeMasters { get; set; }
        public virtual DbSet<tblSchemeTransMaster> tblSchemeTransMasters { get; set; }
        public virtual DbSet<tblStateMaster> tblStateMasters { get; set; }
        public virtual DbSet<tblZonemaster> tblZonemasters { get; set; }
        public virtual DbSet<tblSchemeTransMonth> tblSchemeTransMonths { get; set; }
        public virtual DbSet<tblCityMaster> tblCityMasters { get; set; }
        public virtual DbSet<Mst_PinCode> Mst_PinCode { get; set; }
        public virtual DbSet<Mst_SourceofApplication> Mst_SourceofApplication { get; set; }
    
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
    }
}
