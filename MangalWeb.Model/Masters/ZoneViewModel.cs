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

        [Required(ErrorMessage = "Zone Name is required")]
        [StringLength(20, ErrorMessage = "Zone Name can not be more than 20 characters")]
        public string ZoneName { get; set; }

        public string operation { get; set; }

    }
}