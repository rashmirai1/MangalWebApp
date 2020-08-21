using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Utilities
{
    public class UserViewModel
    {
        public int ID { get; set; }

        [Required]
        public string EmployeeName { get; set; }

        [Required]
        public string EmployeeCode { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(10)]
        public string MobileNo { get; set; }

        [Required]
        [StringLength(30)]
        public string EmailId { get; set; }

        [Required]
        public int UserCategoryId { get; set; }

        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        [StringLength(20)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string operation { get; set; }
    }
}
