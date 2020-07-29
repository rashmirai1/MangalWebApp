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

        [Required(ErrorMessage = "Ornament Name is Required")]
        [StringLength(20)]
        public string OrnamentName { get; set; }

        [Required(ErrorMessage = "Please Select Product")]
        public short Product { get; set; }

        [Required(ErrorMessage = "Please Select Status")]
        public short Status { get; set; }

        public string ProductStr { get; set; }
        public string StatusStr { get; set; }

        public string operation { get; set; }

    }
}