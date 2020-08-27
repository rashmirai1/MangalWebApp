using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Masters
{
    public class SchemeEffectiveROIVM
    {
        public int ID { get; set; }
        public int SchemeId { get; set; }
        public int NoofDefaultMonths { get; set; }
        public decimal? EffectiveROIPerc { get; set; }
    }
}
