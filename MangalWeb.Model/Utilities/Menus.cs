using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Utilities
{
    public class Menus 
    {
        public int? ParentId { get; set; }
        public string Sequence { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string ToolTip { get; set; }
        public string IconPath { get; set; }

        public virtual Menus MyParent { get; set; }

        public virtual ICollection<Menus> MenuCollection { get; set; }
        public virtual ICollection<UserAuthorization> MyUserAuthorizationList { get; set; }


        public bool isVisible { get; set; }
        public bool isEdit { get; set; }
        public bool isSave { get; set; }
        public bool isDelete { get; set; }
        public bool isView { get; set; }
        public bool isSearch { get; set; }
        public int UserID { get; set; }
    }
}
