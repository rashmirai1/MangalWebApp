using MangalWeb.Model.Entity;
using MangalWeb.Model.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Repository.Repository
{
   public class UserAuthorizationRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<UserAuthorizationForms> GetFormDetails_FormIdWise(int formid, int UserId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("FormId", formid);
            ht.Add("UserId", UserId);
            var lst = _context.Database.SqlQuery<UserAuthorizationForms>("T_GetFormDetails_FormIdWise @FormId,@UserId",
                new SqlParameter("FormId", formid),new SqlParameter("FormId",UserId)).ToList();
            return lst;
        }

        public List<tbl_UserCategory> GetUserCategory()
        {
            return _context.tbl_UserCategory.ToList();
        }
        #region GetUser
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserCategoryId"></param>
        /// <param name="BranchID"></param>
        /// <returns></returns>
        public List<UserDetail> GetUser(int UserCategoryId)
        {
            return _context.UserDetails.Where(x => x.UserTypeID == UserCategoryId).ToList();
        }
        #endregion

        #region GetMenuListUserCategoryWise
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserCategoryId"></param>
        /// <returns></returns>
        public List<Menu> GetMenuList()
        {

            return _context.Menus.Where(x => x.ParentId == 0).ToList();
        }
        #endregion
    }
}
