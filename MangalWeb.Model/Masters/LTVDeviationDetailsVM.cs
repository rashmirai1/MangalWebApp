using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MangalWeb.Model.Masters
{
    public class LTVDeviationDetailsVM
    {
        public int ID { get; set; }
        public decimal? LTVMinRange { get; set; }
        public decimal? LTVMaxRange { get; set; }
        public int? LTVUserNo { get; set; }
        public string LTVUserName { get; set; }
    }
}