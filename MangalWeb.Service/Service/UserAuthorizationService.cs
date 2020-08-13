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

        public List<UserDetail> GetUser(int catid)
        {
            return _userAuthorizationRepository.GetUser(catid);
        }

        public List<Menu> GetMenuList()
        {
            return _userAuthorizationRepository.GetMenuList();
        }
    }
}
