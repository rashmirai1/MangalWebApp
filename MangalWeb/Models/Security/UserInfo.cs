using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MangalWeb.Models.Security
{
    public class UserInfo
    {
        public static int UserID { get { return Convert.ToInt32(System.Web.HttpContext.Current.Session["UserLoginId"]); } }
        public static string UserName { get { return Convert.ToString(System.Web.HttpContext.Current.Session["UserName"]); } }
        public static string UserCategory { get { return Convert.ToString(System.Web.HttpContext.Current.Session["UserCategory"]); } }
        public static int BranchId { get { return Convert.ToInt32(System.Web.HttpContext.Current.Session["BranchId"]); } }
        public static int FinancialYearId { get { return Convert.ToInt32(System.Web.HttpContext.Current.Session["BranchId"]); } }
        public static int CompanyId { get { return Convert.ToInt32(System.Web.HttpContext.Current.Session["CompanyId"]); } }
    }
}