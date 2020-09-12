using MangalWeb.Model.Entity;
using MangalWeb.Model.Security;
using MangalWeb.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Repository.Repository
{
    public class UserRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<UserDetail> GetAllUserDetails()
        {
            return _context.UserDetails.ToList();
        }

        public UserDetail GetUserMasterById(int id)
        {
            return _context.UserDetails.Where(x => x.UserID == id).FirstOrDefault();
        }

        public int GetMaxUserMasterId()
        {
            return _context.UserDetails.Any() ? _context.UserDetails.Max(x => x.UserID) + 1 : 1;
        }

        public void DeleteRecord(int id)
        {
            var deleterecord = _context.UserDetails.Where(x => x.UserID == id).FirstOrDefault();
            if (deleterecord != null)
            {
                _context.UserDetails.Remove(deleterecord);
                _context.SaveChanges();
            }
        }

        public void SaveUpdateRecord(UserViewModel model)
        {
            UserDetail tblUserMaster = new UserDetail();
            if (model.EditId <= 0)
            {
                tblUserMaster.UserID = model.ID;
                _context.UserDetails.Add(tblUserMaster);
                tblUserMaster.createdby = model.CreatedBy;
                tblUserMaster.createddate = DateTime.Now;
            }
            else
            {
                tblUserMaster = _context.UserDetails.Where(x => x.UserID == model.ID).FirstOrDefault();
            }
            tblUserMaster.EmployeeName = model.EmployeeName;
            tblUserMaster.EmployeeCode = model.EmployeeCode;
            tblUserMaster.EmpAddress = model.Address;
            tblUserMaster.MobileNo = model.MobileNo;
            tblUserMaster.EmailId = model.EmailId;
            tblUserMaster.UserTypeID = model.UserCategoryId;
            tblUserMaster.UserName = model.UserName;
            tblUserMaster.Password = PasswordEncryptionDecryption.Encrypt(model.Password);
            tblUserMaster.updatedby = model.UpdatedBy;
            tblUserMaster.updateddate = DateTime.Now;
            _context.SaveChanges();
        }

        public string CheckEmployeeCodeExists(string EmpCode, int id)
        {
            if (id > 0)
            {
                return _context.UserDetails.Where(x => x.EmployeeCode == EmpCode && x.UserID != id).Select(x => x.EmployeeCode).FirstOrDefault();
            }
            else
            {
                return _context.UserDetails.Where(x => x.EmployeeCode == EmpCode).Select(x => x.EmployeeCode).FirstOrDefault();
            }
        }

        public UserViewModel SetRecordinEdit(UserDetail tblUser)
        {
            UserViewModel user = new UserViewModel();
            user.ID = tblUser.UserID;
            user.EditId = tblUser.UserID;
            user.EmployeeName = tblUser.EmployeeName;
            user.EmployeeCode = tblUser.EmployeeCode;
            user.Address = tblUser.EmpAddress;
            user.MobileNo = tblUser.MobileNo;
            user.EmailId = tblUser.EmailId;
            user.UserCategoryId = tblUser.UserTypeID;
            user.UserName = tblUser.UserName;
            user.Password = PasswordEncryptionDecryption.Decrypt(tblUser.Password);
            return user;
        }

        public void SetUserFlag(string emailid,int flag)
        {
            var user = _context.UserDetails.SingleOrDefault(x => x.EmailId == emailid);
            user.IsVerified = flag;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdatePassword(int userid, string password)
        {
            var user = _context.UserDetails.SingleOrDefault(x => x.UserID == userid);
            user.Password = password;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void AppSettings(out string UserId, out string Password, out string SMTPPort, out string Host)
        {
            UserId = ConfigurationManager.AppSettings.Get("UserId");
            Password = ConfigurationManager.AppSettings.Get("Password");
            SMTPPort = ConfigurationManager.AppSettings.Get("SMTPPort");
            Host = ConfigurationManager.AppSettings.Get("Host");
        }

        public void SendEmail(string From, string Subject, string Body, string To, string UserID, string Password, string SMTPPort, string Host)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(To);
            mail.From = new MailAddress(From);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = Host;
            smtp.Port = Convert.ToInt16(SMTPPort);
            smtp.Credentials = new NetworkCredential(UserID, Password);
            smtp.EnableSsl = false;
            smtp.Send(mail);
        }
    }
}
