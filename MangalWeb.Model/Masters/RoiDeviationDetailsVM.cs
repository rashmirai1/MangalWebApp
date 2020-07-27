using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MangalWebProject.Models
{
    public class RoiDeviationDetailsVM
    {
        public int ID { get; set; }
        public decimal? RoiMinRange { get; set; }
        public decimal? RoiMaxRange { get; set; }
        public int? RoiUserNo { get; set; }
        public string RoiUserName { get; set; }
    }
}