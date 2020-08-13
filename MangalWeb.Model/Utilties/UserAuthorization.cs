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
        public int ParentID { get; set; }//Nitesh Tiwari 30/03/2020 4:00 pm add parent id for checking user rights
        public UserAuthorizationForms UserAuthorizationForms { get; set; }//First tab

        public List<UserAuthorizationForms> userauthorizationformsList { get; set; }

        //#region ForeignKey Properties
        public virtual Menus MyForm { get; set; }
        public virtual User MyUser { get; set; }
        //#endregion

        public UserAuthorization()
        {
            UserAuthorizationForms = new UserAuthorizationForms();
            userauthorizationformsList = new List<UserAuthorizationForms>();
        }
    }
}
