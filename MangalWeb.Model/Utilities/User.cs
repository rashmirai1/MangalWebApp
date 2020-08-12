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
        public int UserCategoryID { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public string UserCategory { get; set; }

        public string operation { get; set; }

        public virtual UserCategory MyUserCategory { get; set; }

        public virtual ICollection<UserAuthorization> MyUserAuthorizationList { get; set; }
    }
}
