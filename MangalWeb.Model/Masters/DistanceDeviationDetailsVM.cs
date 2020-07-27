using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MangalWebProject.Models
{
    public class DistanceDeviationDetailsVM
    {
        public int ID { get; set; }
        public decimal? DistanceMinRange { get; set; }
        public decimal? DistanceMaxRange { get; set; }
        public int? DistanceUserNo { get; set; }
        public string DistanceUserNanme { get; set; }

    }
}