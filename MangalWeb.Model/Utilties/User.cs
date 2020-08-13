using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MangalWeb.Model.Utilities
{
    public class User
    {
        
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
        public int EmployeeID { get; set; }
        public int UserCategoryID { get; set; }
        public string UserCategory { get; set; }

        [NotMapped]
        public string operation { get; set; }


        #region ForeignKey Properties

        public virtual UserCategory MyUserCategory { get; set; }
        #endregion

        #region Collection Properties
        public virtual ICollection<UserAuthorization> MyUserAuthorizationList { get; set; }
        #endregion
    }
}
