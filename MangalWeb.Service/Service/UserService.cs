﻿using MangalWeb.Model.Entity;
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

        public string CheckEmployeeCodeExists(string empcode,int id)
        {
            return _userRepository.CheckEmployeeCodeExists(empcode,id);
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

        public void SetUserFlag(string email,int flag)
        {
            _userRepository.SetUserFlag(email,flag);
        }

        public void UpdatePassword(int userid, string password)
        {
            _userRepository.UpdatePassword(userid, password);
        }

        public void AppSettings(out string UserId, out string Password, out string SMTPPort, out string Host)
        {
            _userRepository.AppSettings(out UserId,out Password,out SMTPPort,out Host);
        }

        public void SendEmail(string From, string Subject, string Body, string To, string UserID, string Password, string SMTPPort, string Host)
        {
            _userRepository.SendEmail(From, Subject, Body, To, UserID, Password, SMTPPort, Host);
        }
    }
}
