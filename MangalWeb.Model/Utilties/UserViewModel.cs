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
        public UserViewModel()
        {
            tbl_UserCategory = new UserCategoryVM();
        }
        public int ID { get; set; }
        public int EditId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

        [Required]
        public string EmployeeName { get; set; }

        [Required]
        public string EmployeeCode { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(10)]
        [MinLength(10)]
        public string MobileNo { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [Required]
        public int UserCategoryId { get; set; }

        [Required]
        [StringLength(30)]
        public string UserName { get; set; }

        [Required]
        [StringLength(30)]
        [DataType(DataType.Password)]
        [MinLength(4)]
        public string Password { get; set; }

        public string operation { get; set; }
        public UserCategoryVM tbl_UserCategory { get; set; }
    }
}
