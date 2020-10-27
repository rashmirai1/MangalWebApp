using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MangalWeb.Model.Transaction
{
    public class KYCBasicDetailsVM
    {
        public KYCBasicDetailsVM()
        {
            DocumentUploadVM = new DocumentUploadDetailsVM();
            DocumentUploadList = new List<DocumentUploadDetailsVM>();
            Trans_KYCAddresses = new List<KYCAddressesVM>();
        }
        public int KYCID { get; set; }
        public string KycType { get; set; }
        public string AdhaarNo { get; set; }
        public string ApplicationNo { get; set; }
        [Required(ErrorMessage = "Prefix is required.")]
        public string ApplicantPrefix { get; set; }

        public string CustomerID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime AppliedDate { get; set; }

        //[Required(ErrorMessage = "Please Upload Photo !")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        public HttpPostedFileBase uploadFile { get; set; }

        public DateTime KYCDate { get; set; }
        public int OperatorID { get; set; }
        public byte[] ApplicantPhoto { get; set; }
        public string PhotoName { get; set; }
        public string ContentType { get; set; }
        public string ExistingPLCaseNo { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string AppFName { get; set; }

        [Required(ErrorMessage = "Middle Name is required")]
        public string AppMName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string AppLName { get; set; }

        [Required(ErrorMessage = "Please Select Gender")]
        public short Gender { get; set; }

        [Required(ErrorMessage = "Please Select Marital Status")]
        public string MaritalStatus { get; set; }

        [Required(ErrorMessage = "Birth Date is required.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }

        public int? Age { get; set; }

        [Required(ErrorMessage = "Father/Spouse is required")]
        public string Spouse { get; set; }

        [RegularExpression("^([A-Za-z]){5}([0-9]){4}([A-Za-z]){1}$", ErrorMessage = "Invalid PAN Number")]
        public string PANNo { get; set; }

        [Required(ErrorMessage = "Mobile No is required")]
        public string MobileNo { get; set; }

        public string VerificationCode { get; set; }
        public string TelephoneNo { get; set; }
        [EmailAddress]
        //[Required(ErrorMessage = "This Field is Required.")]
        public string EmailID { get; set; }

        //[Required(ErrorMessage = "This Field is Required.")]
        public int? SourceofApplicationID { get; set; }

        public string SourceSpecification { get; set; }

        [Required(ErrorMessage = "Bldg House Name is required")]
        public string BldgHouseName { get; set; }

        [Required(ErrorMessage = "Road is required")]
        public string Road { get; set; }

        [Required(ErrorMessage = "Bldg PlotNo is required")]
        public string BldgPlotNo { get; set; }

        [Required(ErrorMessage = "Room Block No is required")]
        public string RoomBlockNo { get; set; }

        public int StateID { get; set; }
        public int CityID { get; set; }
        public int AreaID { get; set; }
        public int ZoneID { get; set; }
        public string Area { get; set; }

        [Required(ErrorMessage = "Landmark is required")]
        public string Landmark { get; set; }

        [Required(ErrorMessage = "Please Select Occupation Type")]
        public string Occupation { get; set; }

        //[Required(ErrorMessage = "This Field is Required.")]
        public string OrganizationName { get; set; }

        [Required(ErrorMessage = "Present annual Income is required")]
        public decimal PresentIncome { get; set; }

        //[Required(ErrorMessage = "This Field is Required.")]
        public string IndustriesType { get; set; }

        //[Required(ErrorMessage = "This Field is Required.")]
        public string SpecifyIndustries { get; set; }

        //[Required(ErrorMessage = "This Field is Required.")]
        public string Designation { get; set; }

        //[Required(ErrorMessage = "This Field is Required.")]
        public string NomFName { get; set; }
        //[Required(ErrorMessage = "This Field is Required.")]
        public string NomMName { get; set; }
        //[Required(ErrorMessage = "This Field is Required.")]
        public string NomLName { get; set; }
        //[Required(ErrorMessage = "This Field is Required.")]
        public string NomRelation { get; set; }
        public string NomAddress { get; set; }
        public int FYID { get; set; }
        public int CmpID { get; set; }
        public int BranchID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }

        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Boolean isActive { get; set; }

        [Required(ErrorMessage = "Mother Name is required")]
        public string MotherName { get; set; }

        [Required(ErrorMessage = "Please Select Father/Spouse")]
        public string Father_Spouse { get; set; }
        public string CKYCNo { get; set; }
        [Required(ErrorMessage = "Prefix is required.")]
        public string FatherPrefix { get; set;  }

        public string SourceType { get; set; }

        public string OccupationOther { get; set; }
        public string IndustryOther { get; set; }

        //[Required(ErrorMessage = "This Field is Required.")]
        public string NomineeMobileNo { get; set; }

        //[Required(ErrorMessage = "This Field is Required.")]
        [RegularExpression("^([A-Za-z]){5}([0-9]){4}([A-Za-z]){1}$", ErrorMessage = "Invalid PAN Number")]
        public string NomineePanNo { get; set; }
        public string NomineeAdharNo { get; set; }

        [Required(ErrorMessage = "Distance is required")]
        public string Distance { get; set; }

        [Required(ErrorMessage = "Please Select PinCode")]
        public int PinCode { get; set; }
        public Boolean isPanAdharExist { get; set; }
        public string ImageName { get; set; }


        [Required(ErrorMessage = "Please Select Residence Code")]
        public string ResidenceCode { get; set; }

        public string Status { get; set; }
        public string AddressCategory { get; set; }

        public DocumentUploadDetailsVM DocumentUploadVM { get; set; }
        public List<DocumentUploadDetailsVM> DocumentUploadList { get; set; }
        public virtual IList<KYCAddressesVM> Trans_KYCAddresses { get; set; }

    }
}