using MangalWeb.Model.Entity;
using MangalWeb.Model.Transaction;
using MangalWeb.Repository.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace MangalWeb.Service.Service
{
    public class KYCService
    {
        KYCRepository _kycRepository = new KYCRepository();

        /// <summary>
        /// Save KYC
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void SaveRecord(KYCBasicDetailsVM model)
        {
            _kycRepository.SaveRecord(model);
        }

        /// <summary>
        /// get source of application list to fill dropdown
        /// </summary>
        /// <returns></returns>
        public List<Mst_SourceofApplication> GetSourceOfApplicationList()
        {
            var list = _kycRepository.GetSourceOfApplicationList();
            return list;
        }
        /// <summary>
        /// get city, state, area, zone by pincode id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FillAddressByPinCode FillAddressByPinCode(int id)
        {
            return _kycRepository.FillAddressByPinCode(id);
        }
        /// <summary>
        /// get all pincodes from pincode master table
        /// </summary>
        /// <returns></returns>
        public IList<Mst_PinCode> GetAllPincodes()
        {
            return _kycRepository.GetAllPincodes();
        }

        public dynamic GetPincodeWithArea()
        {
            return _kycRepository.GetPincodeMasterList();
        }

        #region GetConsolidatedImage
        public TGLKYC_BasicDetails GetApplicantImage(int id)
        {
            return _kycRepository.GetApplicantImage(id);
        }
        #endregion

        /// <summary>
        /// check if pan already exists
        /// </summary>
        /// <param name="Pan"></param>
        /// <returns></returns>
        public KYCBasicDetailsVM doesPanExist(string Pan)
        {
            return _kycRepository.doesPanExist(Pan);
        }
        /// <summary>
        /// get kyc photo by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TGLKYC_BasicDetails GetImageById(int id)
        {
            return _kycRepository.GetImageById(id);
        }
        /// <summary>
        /// send otp to the mobile number
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public string SendOtp(string mobile, string customerId)
        {
            Random r = new Random();
            string OTP = r.Next(1000, 9999).ToString();

            //Send message
            string Username = "afpl";
            string APIKey = "afpl2014";
            string Sid = "ApheLN";
            string Message = "Dear Customer, your verification ID is " + OTP + ". Kindly get it verified with our staff to get the registration completed.";
            string URL = "http://smpp.keepintouch.co.in/vendorsms/pushsms.aspx/?user=" + Username + "&password=" + APIKey + "&msisdn=" + mobile + "&sid=" + Sid + "&msg=" + Message + "" + "&fl=0&gwid=2";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string results = sr.ReadToEnd();
            dynamic result = JsonConvert.DeserializeObject(results);
            sr.Close();
            string response = String.Empty;
            if (result.ErrorMessage == "Success")
            {
                response = "OTP successfully sent!";
                _kycRepository.StoreOtp(mobile, customerId, OTP);
            }
            else
            {
                response = "Trouble sending OTP, please check if the mobile number entered is correct! or contact our customer care.";
            }
            return response;
        }
        /// <summary>
        /// verify otp code
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="customerId"></param>
        /// <param name="otp"></param>
        /// <returns></returns>
        public string VerifyMobileNumber(string mobile, string customerId, string otp)
        {
            return _kycRepository.VerifyMobileNumber(mobile, customerId, otp);
        }
        /// <summary>
        /// check if adhar alreday exists
        /// </summary>
        /// <param name="AdharNo"></param>
        /// <returns></returns>
        public KYCBasicDetailsVM doesAdharExist(string AdharNo)
        {
            var kycVm = _kycRepository.doesAdharExist(AdharNo);
            return kycVm;
        }
        /// <summary>
        /// generate application number by id
        /// </summary>
        /// <returns></returns>
        public string GenerateApplicationNo()
        {
            int count = _kycRepository.GenerateApplicationNo();
            string AppNo = string.Empty;
            if (count > 0)
            {
                AppNo = (count + 1).ToString();
            }
            else
            {
                AppNo = "1";
            }
            return AppNo;
        }

        public void SaveDocument(List<DocumentUploadDetailsVM> lstDocUploadTrn)
        {
            _kycRepository.SaveDocument(lstDocUploadTrn);
        }

        public string GetSourceType(int id)
        {
            return _kycRepository.GetSourceType(id);
        }
    }
}
