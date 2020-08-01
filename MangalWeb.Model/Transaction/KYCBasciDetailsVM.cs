using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Transaction
{
    public class KYCBasicDetailsVM
    {
        public Nullable<int> KYCID { get; set; }
        public string AdhaarNo { get; set; }
        public string ApplicationNo { get; set; }
        public string ApplicantPrefix { get; set; }
        public string CustomerID { get; set; }
        public Nullable<System.DateTime> AppliedDate { get; set; }
        public int OperatorID { get; set; }
        public string ExistingCustomerID { get; set; }
        public string ExistingPLCaseNo { get; set; }
        public string AppFName { get; set; }
        public string AppMName { get; set; }
        public string AppLName { get; set; }
        public string AppPhotoPath { get; set; }
        public string AppSignPath { get; set; }
        public byte[] AppPhoto { get; set; }
        public byte[] AppSign { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<int> Age { get; set; }
        public string Spouse { get; set; }
        public string Children { get; set; }
        public string PANNo { get; set; }
        public string MobileNo { get; set; }
        public string VerificationCode { get; set; }
        public string TelephoneNo { get; set; }
        public string EmailID { get; set; }
        public string SourceofApplication { get; set; }
        public string SourceSpecification { get; set; }
        public Nullable<int> DealerID { get; set; }
        public string BldgHouseName { get; set; }
        public string Road { get; set; }
        public string BldgPlotNo { get; set; }
        public string RoomBlockNo { get; set; }
        public Nullable<int> StateID { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<int> AreaID { get; set; }
        public Nullable<int> ZoneID { get; set; }
        public string Landmark { get; set; }
        public string Occupation { get; set; }
        public string OrganizationName { get; set; }
        public string PresentIncome { get; set; }
        public string OfficeAddress { get; set; }
        public string EmploymentType { get; set; }
        public string SpecifyEmployment { get; set; }
        public string IndustriesType { get; set; }
        public string SpecifyIndustries { get; set; }
        public string Designation { get; set; }
        public string NomFName { get; set; }
        public string NomMName { get; set; }
        public string NomLName { get; set; }
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
        public string isActive { get; set; }
        public Nullable<int> SourceofApplicationID { get; set; }
        public string MotherName { get; set; }
        public string Father_Spouse { get; set; }
        public string CKYCNo { get; set; }
        public string SourceType { get; set; }

        public string OccupationOther { get; set; }
        public string IndustryOther { get; set; }
        public string NomineeMobileNo { get; set; }
        public string NomineePanNo { get; set; }
        public string NomineeAdharNo { get; set; }
    }
}
