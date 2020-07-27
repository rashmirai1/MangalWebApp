using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MangalWebProject.Models
{
    public class DeviationViewModel
    {
        public int ID { get; set; }
        public RoiDeviationDetailsVM roiDeviationDetailsVM { get; set; }
        public DistanceDeviationDetailsVM distanceDeviationDetailsVM { get; set; }

        public int? OrnamentUserNo { get; set; }

        public SanctionDeviationVM sanctionDeviationVM { get; set; }
        public TenureDeviationVM tenureDeviationVM { get; set; }
        public LTVDeviationDetailsVM lTVDeviationDetailsVM { get; set; }

        public List<RoiDeviationDetailsVM> roiDeviationDetailsList { get; set; }
        public List<DistanceDeviationDetailsVM> distanceDeviationDetailsList { get; set; }

        public List<SanctionDeviationVM> sanctionDeviationList { get; set; }
        public List<TenureDeviationVM> tenureDeviationList { get; set; }
        public List<LTVDeviationDetailsVM> lTVDeviationDetailsList { get; set; }

        public decimal? ThresholdLimit { get; set; }
        public decimal? ApproveDistanceLimit { get; set; }

        public string operation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}