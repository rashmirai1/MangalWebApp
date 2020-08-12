using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Utilities
{
    public class UserAuthorizationForms
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please select User Category")]
        public int UserCategoryID { get; set; }
        public string UserCategoryName { get; set; }
        [Required(ErrorMessage = "Please select User")]
        public int UserID { get; set; }
        public string User { get; set; }
        [Required(ErrorMessage = "Please select Menu")]
        public int FormID { get; set; }
        public string FormName { get; set; }

        public int index { get; set; }
        public int ParentID { get; set; }
        public string flag { get; set; }
        public string ParentForm { get; set; }

        [Required(ErrorMessage = "Please select Branch")]
        public int BranchID { get; set; }

        /// <summary>
        /// Gets or sets the UserCategory status
        /// </summary>
        public bool isVisible { get; set; }
        public string VisibleName { get; set; }
   
        public bool isEdit { get; set; }
        public bool isSave { get; set; }
        public bool isDelete { get; set; }
        public bool isView { get; set; }

        public bool isSearch { get; set; }

    }
}
