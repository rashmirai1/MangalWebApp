using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Utilities
{
    public class UserCategory 
    {
        public string Category { get; set; }
        public string Description { get; set; }

        #region Collection Properties
        public virtual ICollection<User> MyUserList { get; set; }
        #endregion
    }
}
