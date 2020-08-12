using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Utilities
{
  public class UserAuthorization
    {
        public int FormID { get; set; }
        public int UserID { get; set; }
        public bool isVisible { get; set; }
        public bool isEdit { get; set; }
        public bool isSave { get; set; }
        public bool isDelete { get; set; }
        public bool isView { get; set; }
        public bool isDefault { get; set; }
        public int ParentID { get; set; }
        public UserAuthorizationForms UserAuthorizationForms { get; set; }//First tab

        public List<UserAuthorizationForms> userauthorizationformsList { get; set; }

        public virtual Menu MyForm { get; set; }
        public virtual User MyUser { get; set; }

        public UserAuthorization()
        {
            UserAuthorizationForms = new UserAuthorizationForms();
            userauthorizationformsList = new List<UserAuthorizationForms>();
        }
    }
}
