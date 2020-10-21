using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Transaction
{
    public class KYCAddressesVM
    {
        public int ID { get; set; }
        public int KYCID { get; set; }
        public string AddressCategory { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string ResidenceCode { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string BuildingHouseName { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string Road { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string BuildingPlotNo { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string RoomBlockNo { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string NearestLandmark { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string Distance_km { get; set; }
        public int? PinCode { get; set; }
        public int StateID { get; set; }
        public int CityID { get; set; }
        public string Area { get; set; }
        public int ZoneId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string ZoneName { get; set; }
        public virtual KYCBasicDetailsVM KYCBasicDetailsVm { get; set; }
    }
}
