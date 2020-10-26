using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Transaction
{
    public class RequestFormViewModel
    {
        public RequestFormViewModel()
        {
            DocumentUploadVM = new DocumentUploadDetailsVM();
            DocumentUploadList = new List<DocumentUploadDetailsVM>();
            Trans_KYCAddresses = new List<KYCAddressesVM>();
        }
        public int ID { get; set; }
        public int TransactionId { get; set; }
        public int SanctionId { get; set; }
        public int KycId { get; set; }
        public string AdhaarNo { get; set; }
        public string ApplicationNo { get; set; }
        public string LoanAccountNo { get; set; }

        [Required(ErrorMessage ="Please Select Customer ")]
        public string CustomerID { get; set; }

        public string DisburseDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public string KYCDate { get; set; }

        [Required(ErrorMessage = "Mobile No is required")]
        public string MobileNo { get; set; }

        public string TelephoneNo { get; set; }

        public string EmailID { get; set; }

        public string AddressCategory { get; set; }

        [Required(ErrorMessage = "Please Select Residence Code")]
        public string ResidenceCode { get; set; }

        [Required(ErrorMessage = "Bldg House Name is required")]
        public string BldgHouseName { get; set; }

        [Required(ErrorMessage = "Road is required")]
        public string Road { get; set; }

        [Required(ErrorMessage = "Bldg PlotNo is required")]
        public string BldgPlotNo { get; set; }

        [Required(ErrorMessage = "Room Block No is required")]
        public string RoomBlockNo { get; set; }

        [Required(ErrorMessage = "Landmark is required")]
        public string Landmark { get; set; }

        public int StateID { get; set; }
        public int CityID { get; set; }
        public int AreaID { get; set; }
        public int ZoneID { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string ZoneName { get; set; }
        public string Area { get; set; }

        [Required(ErrorMessage = "Please Select PinCode")]
        public int PinCode { get; set; }

        [Required(ErrorMessage = "Distance is required")]
        public string Distance { get; set; }

        public int FYID { get; set; }
        public int CmpID { get; set; }
        public int BranchID { get; set; }
        public DateTime CreationDate { get; set; }

        public DocumentUploadDetailsVM DocumentUploadVM { get; set; }
        public List<DocumentUploadDetailsVM> DocumentUploadList { get; set; }
        public List<KYCAddressesVM> Trans_KYCAddresses { get; set; }
    }
}
