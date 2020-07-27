using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MangalWebProject.Models
{
    public class TenureDeviationVM
    {
        public int ID { get; set; }
        public decimal? TenureMinRange { get; set; }
        public decimal? TenureMaxRange { get; set; }
        public int? TenureUserNo { get; set; }
        public string TenureUserName { get; set; }
    }
}