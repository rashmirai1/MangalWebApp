using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangalWeb.Model.Masters
{
    public class OrnamentViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Ornament Name is required")]
        [StringLength(20, ErrorMessage = "Ornament Name can not be more than 20 characters")]
        public string OrnamentName { get; set; }

        [Required(ErrorMessage = "Please Select Product")]
        public int Product { get; set; }

        [Required(ErrorMessage = "Please Select Status")]
        public string Status { get; set; }

        public string ProductStr { get; set; }

        public string operation { get; set; }

    }
}