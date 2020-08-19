using System;
using System.Collections.Generic;
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
        public string ResidenceCode { get; set; }
        public string BuildingHouseName { get; set; }
        public string Road { get; set; }
        public string BuildingPlotNo { get; set; }
        public string RoomBlockNo { get; set; }
        public string NearestLandmark { get; set; }
        public string Distance_km { get; set; }
        public string PinCode { get; set; }
        public int StateID { get; set; }
        public int CityID { get; set; }
        public string Area { get; set; }
        public string ZoneId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string ZoneName { get; set; }

        public virtual KYCBasicDetailsVM KYCBasicDetailsVm { get; set; }
    }
}
