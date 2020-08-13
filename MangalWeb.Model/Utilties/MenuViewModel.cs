using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MangalWeb.Model.Utilities
{
    public class MenuViewModel
    {
        public int? ParentId { get; set; }
        public string Sequence { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string ToolTip { get; set; }
        public string IconPath { get; set; }
        public string isActive { get; set; }

        public int FormID { get; set; }
        public int UserID { get; set; }
        public bool isVisible { get; set; }
        public bool isEdit { get; set; }
        public bool isSave { get; set; }
        public bool isDelete { get; set; }
        public bool isView { get; set; }
        public bool isDefault { get; set; }
    }
}