using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Utilities
{
    public class ForgotPasswordViewModel
    {
        //[Required]
        //[Display(Name = "User Name")]
        //public string UserName { get; set; }

        [Required(ErrorMessage = "Email Address required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string EmailId { get; set; }

        [Required(ErrorMessage ="Password required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage ="Confirm Password required")]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage ="Password doesn't match")]
        public string ConfirmPassword { get; set; }
    }
}
