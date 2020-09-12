using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MangalWeb.Model.Transaction
{
    public class KYCBasicDetailsVM
    {
        public KYCBasicDetailsVM()
        {
            DocumentUploadList = new List<KYCDocumentUpload>();
            Trans_KYCAddresses = new List<KYCAddressesVM>();
        }
        public Nullable<int> KYCID { get; set; }
        public string AdhaarNo { get; set; }
        public string ApplicationNo { get; set; }
        public string ApplicantPrefix { get; set; }
        public string CustomerID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> AppliedDate { get; set; }
        public Nullable<System.DateTime> KYCDate { get; set; }
        public int? OperatorID { get; set; }
        public string ExistingCustomerID { get; set; }
        public string ExistingPLCaseNo { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "This Field is Required.")]
        public string AppFName { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "This Field is Required.")]
        public string AppMName { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "This Field is Required.")]
        public string AppLName { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "This Field is Required.")]
        public string AppPhotoPath { get; set; }
        public string AppSignPath { get; set; }
        public byte[] AppPhoto { get; set; }
        public byte[] AppSign { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string MaritalStatus { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<int> Age { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string Spouse { get; set; }
        public string Children { get; set; }
        public string PANNo { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string MobileNo { get; set; }
        public string VerificationCode { get; set; }
        public string TelephoneNo { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "This Field is Required.")]
        public string EmailID { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string SourceofApplication { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string SourceSpecification { get; set; }
        public Nullable<int> DealerID { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string BldgHouseName { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string Road { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string BldgPlotNo { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string RoomBlockNo { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public Nullable<int> StateID { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public Nullable<int> CityID { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public Nullable<int> AreaID { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public Nullable<int> ZoneID { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string Landmark { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string Occupation { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string OrganizationName { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string PresentIncome { get; set; }
        public string OfficeAddress { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string EmploymentType { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string SpecifyEmployment { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string IndustriesType { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string SpecifyIndustries { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string Designation { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "This Field is Required.")]
        public string NomFName { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "This Field is Required.")]
        public string NomMName { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "This Field is Required.")]
        public string NomLName { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "This Field is Required.")]
        public string NomRelation { get; set; }
        public string NomAddress { get; set; }
        public Nullable<int> LoanpurposeID { get; set; }
        public string SpecifyLoanPurpose { get; set; }
        public Nullable<int> FYID { get; set; }
        public Nullable<int> CmpID { get; set; }
        public Nullable<int> BranchID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Boolean isActive { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public Nullable<int> SourceofApplicationID { get; set; }
        public string MotherName { get; set; }

        [Required(ErrorMessage = "This Field is Required.")]
        public string Father_Spouse { get; set; }
        public string CKYCNo { get; set; }

        [Required(ErrorMessage = "This Field is Required.")]
        public string SourceType { get; set; }

        public string OccupationOther { get; set; }
        public string IndustryOther { get; set; }

        [Required(ErrorMessage = "This Field is Required.")]
        public string NomineeMobileNo { get; set; }

        [Required(ErrorMessage = "This Field is Required.")]
        public string NomineePanNo { get; set; }
        public string NomineeAdharNo { get; set; }

        [Required(ErrorMessage = "This Field is Required.")]
        public string Distance { get; set; }

        [Required(ErrorMessage = "This Field is Required.")]
        public string PinCode { get; set; }
        public Boolean isPanAdharExist { get; set; }
        [Required(ErrorMessage ="Plese Select Upload Photo !")]
        public byte[] KycPhoto { get; set; }
        public string ContentType { get; set; }
        public string ImageName { get; set; }

        [Required(ErrorMessage = "This Field is Required.")]
        public string Area { get; set; }
        public KYCDocumentUpload DocumentUploadVM { get; set; }
        public List<KYCDocumentUpload> DocumentUploadList { get; set; }
        public virtual IList<KYCAddressesVM> Trans_KYCAddresses { get; set; }
        public string ResidenceCode { get; set; }

    }
}