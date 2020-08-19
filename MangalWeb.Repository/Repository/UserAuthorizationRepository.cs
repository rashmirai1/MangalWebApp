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


        public UserAuthorizationRepository()
        {
            _context = new MangalDBNewEntities();
        }

        public List<UserAuthorizationForms> GetFormDetails_FormIdWise(int formid, int UserId)
        {
            var lst = _context.Database.SqlQuery<UserAuthorizationForms>("T_GetFormDetails_FormIdWise @FormId,@UserId",
                new SqlParameter("FormId", formid), new SqlParameter("UserId", UserId)).ToList();
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

        public List<UserAuthorizationForms> GetForms(int parentid, int UserId, int UserCategoryID)
        {
            var list = _context.Database.SqlQuery<UserAuthorizationForms>("T_UserAuthorization_Forms @ParentID,@UserID,@UserCategoryID",
                 new SqlParameter("@ParentID", parentid),
                 new SqlParameter("@UserID", UserId),
                 new SqlParameter("@UserCategoryID", UserCategoryID)).ToList();
            return list;
        }

        #region InsertUserAuthorization_DetailsUserWise
        /// <summary>
        /// 
        /// </summary>
        /// <param name="InsertUserAuthorization_DetailsUserWise"></param>
        /// <returns></returns>
        public int InsertUserAuthorization_DetailsUserWise(UserAuthorizationForms city)
        {
            try
            {
                if (city.isVisible == false)
                {
                    city.isEdit = false;
                    city.isSave = false;
                    city.isDelete = false;
                    city.isSearch = false;
                }
                
                //var state = _context.Database.SqlQuery<UserAuthorizationForms>("T_Update_UserAuthorization_DetailsUserWise @index,@UserID,@ParentID,@FormID,@isVisible,@isEdit,@isView,isSave,@isDelete",
                //new SqlParameter("index", city.index),
                //new SqlParameter("UserID", city.UserID),
                //new SqlParameter("ParentID", city.ParentID),
                //new SqlParameter("FormID", city.FormID),
                //new SqlParameter("isVisible", city.isVisible),
                //new SqlParameter("isEdit", city.isEdit),
                //new SqlParameter("isView", city.isView),
                //new SqlParameter("isSave", city.isSave),
                //new SqlParameter("isDelete", city.isDelete)).FirstOrDefault();

                UserAuthorization tblUserAuthorization = new UserAuthorization();
                tblUserAuthorization = _context.UserAuthorizations.Where(x => x.UserID == city.UserID && x.FormID == city.FormID && x.isActive == "Y").FirstOrDefault();
                if (tblUserAuthorization == null)
                {
                    tblUserAuthorization = new UserAuthorization();
                    tblUserAuthorization.FormID = city.FormID;
                    tblUserAuthorization.UserID = city.UserID;
                    tblUserAuthorization.ParentID = city.ParentID;
                    tblUserAuthorization.isVisible = city.isVisible;
                    tblUserAuthorization.isEdit = city.isEdit;
                    tblUserAuthorization.isSave = city.isSave;
                    tblUserAuthorization.isDelete = city.isDelete;
                    tblUserAuthorization.isView = city.isView;
                    tblUserAuthorization.isDefault = false;
                    tblUserAuthorization.CreatedBy = city.CreatedBy;
                    tblUserAuthorization.CreatedDate = DateTime.Now;
                    tblUserAuthorization.isActive = "Y";
                    _context.UserAuthorizations.Add(tblUserAuthorization);
                    _context.SaveChanges();
                }
                else
                {
                    tblUserAuthorization.FormID = city.FormID;
                    tblUserAuthorization.UserID = city.UserID;
                    tblUserAuthorization.ParentID = city.ParentID;
                    tblUserAuthorization.isVisible = city.isVisible;
                    tblUserAuthorization.isEdit = city.isEdit;
                    tblUserAuthorization.isSave = city.isSave;
                    tblUserAuthorization.isDelete = city.isDelete;
                    tblUserAuthorization.isView = city.isView;
                    tblUserAuthorization.isDefault = false;
                    tblUserAuthorization.CreatedBy = city.CreatedBy;
                    tblUserAuthorization.CreatedDate = DateTime.Now;
                    tblUserAuthorization.isActive = "Y";
                    _context.SaveChanges();
                }
                return 1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        public List<MenusViewModel> UserAuthorization_ParentPage(int Userid)
        {
            try
            {
                var list = _context.Database.SqlQuery<MenusViewModel>("T_Forms_UserAuthorization_ParentPage @UserId",
                 new SqlParameter("UserID", Userid)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<MenusViewModel> GetAuthorizeSubPagesList_PrentidWise(int Userid, int ParentId)
        {
            try
            {
                var list = _context.Database.SqlQuery<MenusViewModel>("T_GetAuthorizeSubPagesList_PrentidWise @UserId,@ParentId",
                           new SqlParameter("UserID", Userid), new SqlParameter("ParentId", ParentId)).ToList();
                return list;
            }

            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
