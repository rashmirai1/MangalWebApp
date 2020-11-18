using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Transaction
{
    public class ResidenceVerificationVM
    {
        public ResidenceVerificationVM()
        {
            DocumentUploadVM = new DocumentUploadDetailsVM();
            DocumentUploadList = new List<DocumentUploadDetailsVM>();
        }

        public int Id { get; set; }
        public int PreSanctionId { get; set; }
        public string TransactionId { get; set; }
        public int KycId { get; set; }
        public string ApplicationNo { get; set; }
        [Required(ErrorMessage = "Date of Visit is required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public string DateofVisit { get; set; }
        [Required(ErrorMessage ="Time of Visit is required")]
        public string TimeofVisit { get; set; }
        [Required(ErrorMessage ="Please Select Customer")]
        public string CustomerId { get; set; }
        [Required(ErrorMessage ="Transaction Date is required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public string AppliedDate { get; set; }
        [Required(ErrorMessage ="Person Visited Name is required")]
        public string PersonVisitedName { get; set; }
        [Required(ErrorMessage ="Relation with customer is required")]
        public string RelationWithCustomer { get; set; }
        [Required(ErrorMessage ="Please Select Family Member Details")]
        public string FamilyMemberDetails { get; set; }
        [Required(ErrorMessage = "Residing At This Address(Years) is required")]
        public int ResidingAtThisAddress_Years { get; set; }
        [Required(ErrorMessage = "Residing At This Address(Months) is required")]
        public int ResidingAtThisAddress_Months { get; set; }
        [Required(ErrorMessage ="Please Select Employee")]
        public int UserId { get; set; }
        public string Designation { get; set; }
        public string EmployeeCode { get; set; }
        public string Comments { get; set; }
        [Required(ErrorMessage = "Bldg House Name is required")]
        public string BldgHouseName { get; set; }
        [Required(ErrorMessage = "Road is required")]
        public string Road { get; set; }
        [Required(ErrorMessage = "Bldg Plot No is required")]
        public string BldgPlotNo { get; set; }
        [Required(ErrorMessage = "Room Block No is required")]
        public string RoomBlockNo { get; set; }
        public int? StateID { get; set; }
        public int? CityID { get; set; }
        public int? AreaID { get; set; }
        public int? ZoneID { get; set; }
        [Required(ErrorMessage ="Landmark is required")]
        public string Landmark { get; set; }
        [Required(ErrorMessage ="Distance is required")]
        public string Distance { get; set; }
        [Required(ErrorMessage ="Please Select Pincode")]
        public int PinCode { get; set; }
        public string Area { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zone { get; set; }
        [Required]
        public string ResidenceCode { get; set; }
        [Required(ErrorMessage ="Please Select Address Category")]
        public string AddressCategory { get; set; }

        public string IsApproved { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }

        public DocumentUploadDetailsVM DocumentUploadVM { get; set; }

        public List<DocumentUploadDetailsVM> DocumentUploadList { get; set; }
    }
}
