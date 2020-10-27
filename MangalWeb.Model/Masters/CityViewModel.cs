using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangalWeb.Model.Masters
{
    public class CityViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "City Name is required")]
        [StringLength(20,ErrorMessage ="City Name can not be more than 20 charaters")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "Please Select State")]
        public int StateId { get; set; }

        public string CountryName { get; set; }

        public string operation { get; set; }

    }
}