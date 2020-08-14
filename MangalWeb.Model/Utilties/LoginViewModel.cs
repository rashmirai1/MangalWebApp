using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangalWeb.Model.Utilities
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Please Select Branch")]
        public int BranchId { get; set; }
        [Required(ErrorMessage ="Please Select Financial Year")]
        public int FinancialYearId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}