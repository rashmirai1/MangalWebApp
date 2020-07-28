using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MangalWebProject.Models
{
    public class SanctionDeviationVM
    {
        public int ID { get; set; }
        public decimal? SanctionMinRange { get; set; }
        public decimal? SanctionMaxRange { get; set; }
        public int? SanctionedUserNo { get; set; }
        public string SanctionedUserName { get; set; }
    }
}