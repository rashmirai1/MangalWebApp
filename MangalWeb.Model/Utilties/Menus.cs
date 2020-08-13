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

        #region ForeignKey Properties     
        public virtual Menus MyParent { get; set; }
        #endregion

        #region ForeignKey Properties     
        public virtual ICollection<Menus> MenuCollection { get; set; }
        public virtual ICollection<UserAuthorization> MyUserAuthorizationList { get; set; }


        [NotMapped]
        public bool isVisible { get; set; }

        [NotMapped]
        public bool isEdit { get; set; }
        [NotMapped]
        public bool isSave { get; set; }
        [NotMapped]
        public bool isDelete { get; set; }
        [NotMapped]
        public bool isView { get; set; }
        [NotMapped]
        public bool isSearch { get; set; }
        [NotMapped]
        public int UserID { get; set; }

        #endregion
    }
}
