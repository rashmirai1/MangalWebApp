using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Utilities
{
    public class UserAuthorizations 
    {
        public UserAuthorizationForms UserAuthorizationForms { get; set; }

        public UserAuthorizations()
        {
            UserAuthorizationForms = new UserAuthorizationForms();
        }
    }
}
