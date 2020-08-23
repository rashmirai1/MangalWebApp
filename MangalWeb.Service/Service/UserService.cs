using MangalWeb.Model.Entity;
using MangalWeb.Model.Utilities;
using MangalWeb.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Service.Service
{
    public class UserService
    {
        UserRepository _userRepository = new UserRepository();

        public List<UserDetail> GetAllUserDetails()
        {
            return _userRepository.GetAllUserDetails();
        }

        public int GetMaxUserMasterId()
        {
            return _userRepository.GetMaxUserMasterId();
        }

        public UserDetail GetUserMasterById(int id)
        {
            return _userRepository.GetUserMasterById(id);
        }

        public string CheckEmployeeCodeExists(string empcode)
        {
            return _userRepository.CheckEmployeeCodeExists(empcode);
        }

        public UserViewModel SetDataOnEdit(UserDetail tblUser)
        {
            return _userRepository.SetRecordinEdit(tblUser);
        }

        public void DeleteCityRecord(int id)
        {
            _userRepository.DeleteRecord(id);
        }

        public void SaveUpdateRecord(UserViewModel model)
        {
            _userRepository.SaveUpdateRecord(model);
        }
    }
}
