using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Utilities
{
    public class UserCategoryVM
    {
        public int refid { get; set; }
        public string Name { get; set; }
        public Nullable<int> Organhieraechyid { get; set; }
        public Nullable<bool> status1 { get; set; }
        
    }
}
