using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangalWeb.Model.Masters
{
    public class ZoneViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Zone Name is Required")]
        [StringLength(20)]
        public string ZoneName { get; set; }

        public string operation { get; set; }

    }
}