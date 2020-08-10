using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Transaction
{
    public class FillAddressByPinCode
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public int StateID { get; set; }
        public string AreaName { get; set; }
        public string ZoneName { get; set; }
        public int ZoneID { get; set; }
    }
}
