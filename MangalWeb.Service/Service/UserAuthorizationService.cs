using MangalWeb.Model.Entity;
using MangalWeb.Model.Utilities;
using MangalWeb.Repository.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Service.Service
{
    public class UserAuthorizationService
    {
        UserAuthorizationRepository _userAuthorizationRepository = new UserAuthorizationRepository();

        public List<UserAuthorizationForms> GetFormDetails_FormIdWise(int formid, int UserId)
        {
            return _userAuthorizationRepository.GetFormDetails_FormIdWise(formid, UserId);
        }

        public List<tbl_UserCategory> GetUserCategory()
        {
            return _userAuthorizationRepository.GetUserCategory();
        }

        public List<tblCompanyBranchMaster> GetBranch()
        {
            return _userAuthorizationRepository.GetBranch();
        }

        public List<UserDetail> GetUser(int catid)
        {
            return _userAuthorizationRepository.GetUser(catid);
        }

        public List<Menu> GetMenuList()
        {
            return _userAuthorizationRepository.GetMenuList();
        }

        public int GetBackDatedVoucher(int parentid, int UserId, int BranchId)
        {
            return _userAuthorizationRepository.GetBackDatedVoucher(parentid, UserId, BranchId);
        }
        public List<UserAuthorizationForms> GetForms(int parentid, int userid, int usercategoryid, int branchid)
        {
            return _userAuthorizationRepository.GetForms(parentid, userid, usercategoryid, branchid);
        }

        public int InsertUserAuthorization_DetailsUserWise(UserAuthorizationForms uaf)
        {
            return _userAuthorizationRepository.InsertUserAuthorization_DetailsUserWise(uaf);
        }

        public List<MenusViewModel> UserAuthorization_ParentPage(int Userid)
        {
            return _userAuthorizationRepository.UserAuthorization_ParentPage(Userid);
        }

        public List<MenusViewModel> GetAuthorizeSubPagesList_PrentidWise(int Userid, int ParentId, int BranchId)
        {
            return _userAuthorizationRepository.GetAuthorizeSubPagesList_PrentidWise(Userid, ParentId, BranchId);
        }
    }
}
