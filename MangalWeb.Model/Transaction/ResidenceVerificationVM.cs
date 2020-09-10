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
        public int Id { get; set; }
        public int PreSanctionId { get; set; }
        public string TransactionId { get; set; }
        public string ApplicationNo { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> DateofVisit { get; set; }
        [Required]
        public Nullable<System.TimeSpan> TimeofVisit { get; set; }
        public string CustomerId { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> AppliedDate { get; set; }
        [Required]
        public string PersonVisitedName { get; set; }
        [Required]
        public string RelationWithCustomer { get; set; }
        [Required]
        public string FamilyMemberDetails { get; set; }
        [Required]
        public Nullable<int> ResidingAtThisAddress_Years { get; set; }
        [Required]
        public Nullable<int> ResidingAtThisAddress_Months { get; set; }
        [Required]
        public Nullable<int> UserId { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        public string EmployeeCode { get; set; }
        [Required]
        public string Comments { get; set; }
        [Required]
        public string BldgHouseName { get; set; }
        [Required]
        public string Road { get; set; }
        [Required]
        public string BldgPlotNo { get; set; }
        [Required]
        public string RoomBlockNo { get; set; }
        [Required]
        public Nullable<int> StateID { get; set; }
        [Required]
        public Nullable<int> CityID { get; set; }
        [Required]
        public Nullable<int> AreaID { get; set; }
        [Required]
        public Nullable<int> ZoneID { get; set; }
        [Required]
        public string Landmark { get; set; }
        [Required]
        public string Distance { get; set; }
        [Required]
        public string PinCode { get; set; }
        [Required]
        public string Area { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zone { get; set; }
        [Required]
        public string ResidenceCode { get; set; }
        [Required]
        public string AddressCategory { get; set; }
        public string IsApproved { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual PreSanctionDetailsVM tbl_PreSanctionDetails { get; set; }
    }
}
