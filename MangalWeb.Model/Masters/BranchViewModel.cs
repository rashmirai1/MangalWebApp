using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MangalWeb.Model.Masters
{
    public class BranchViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Branch Name is required")]
        [StringLength(20,ErrorMessage ="Branch Name can not be greater than 20 characters")]
        public string BranchName { get; set; }

        [Required(ErrorMessage = "Branch Code is required")]
        [StringLength(20,ErrorMessage ="Branch Code can not be greater than 20 characters")]
        public string BranchCode { get; set; }

        [Required(ErrorMessage = "Please Select Branch Type")]
        public int BranchType { get; set; }

        [Required(ErrorMessage = "Date Inception is required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public string DateInception { get; set; }

        [Required(ErrorMessage = "Rent Period Agreed is required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public string RentPeriodAgreed { get; set; }

        [Required(ErrorMessage = "Date WEF Agreed is required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public string DateWEF { get; set; }

        [Required(ErrorMessage = "Please Enter Address")]
        [StringLength(100,ErrorMessage ="Address can not be greater than 100 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please Select Pincode")]
        public int Pincode { get; set; }
        public string PincodeStr { get; set; }

        public string AreaName { get; set; }
        public string ZoneName { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }

        [Required(ErrorMessage = "Please Enter Contact Person")]
        [StringLength(50, ErrorMessage = "Contact Person can not be greater than 50 characters")]
        public string ContactPerson { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile No")]
        [StringLength(10, ErrorMessage = "Mobile No can not be greater than 10 characters")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Please Enter In Time")]
        public string InTime { get; set; }

        [Required(ErrorMessage = "Please Enter Out Time")]
        public string OutTime { get; set; }

        [Required(ErrorMessage = "Please Select Status")]
        public short Status { get; set; }
        public string StatusStr { get; set; }

        public string operation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}