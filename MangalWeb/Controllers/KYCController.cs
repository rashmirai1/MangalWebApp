﻿using MangalWeb.Model.Transaction;
using MangalWeb.Service.Service;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class KYCController : BaseController
    {
        KYCService _kycService = new KYCService();
        DocumentUploadService _documentUploadService = new DocumentUploadService();

        /// <summary>
        /// Index page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ButtonVisiblity("Index");
            ViewBag.SourceList = new SelectList(_kycService.GetSourceOfApplicationList(), "Soa_Name", "Soa_Name");
            ViewBag.PinCodeList = new SelectList(_kycService.GetAllPincodes(), "Pc_Id", "Pc_Desc");
            ViewBag.DocumentTypeList = new SelectList(_documentUploadService.GetDocumentTypeList(), "Id", "Name");
            ViewBag.DocumentList = new SelectList(_documentUploadService.GetDocumentMasterList(), "DocumentID", "DocumentName");
            KYCBasicDetailsVM kycVM = new KYCBasicDetailsVM();
            Random rand = new Random(100);
            int cid = rand.Next(000000000, 999999999) + 1;
            kycVM.CustomerID = "C" + cid.ToString();
            kycVM.ApplicationNo = _kycService.GenerateApplicationNo();
            return View(kycVM);
        }

        /// <summary>
        /// Save KYC
        /// </summary>
        /// <param name="model"></param>
        /// <param name="uploadFile"></param>
        /// <returns></returns>
        public JsonResult CreateEdit(KYCBasicDetailsVM model, HttpPostedFileBase uploadFile)
        {
            try
            {
                if (uploadFile != null)
                {
                    Stream fs1 = uploadFile.InputStream;
                    BinaryReader br1 = new BinaryReader(fs1);
                    model.KycPhoto = br1.ReadBytes((Int32)fs1.Length);
                    model.ContentType = uploadFile.ContentType;
                    model.ImageName = uploadFile.FileName;
                }
                if(model.CustomerID != null)
                {
                    _kycService.SaveRecord(model);
                }
                return Json(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Check if PAN already exist
        /// </summary>
        /// <param name="PanNo"></param>
        /// <returns></returns>
        public JsonResult doesPanExist(string PanNo)
        {
            try
            {
              var model= _kycService.doesPanExist(PanNo);
              return Json(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        /// <summary>
        /// check if adhar already exist
        /// </summary>
        /// <param name="AdharNo"></param>
        /// <returns></returns>
        public JsonResult doesAdharExist(string AdharNo)
        {
            try
            {
                var model = _kycService.doesAdharExist(AdharNo);
                return Json(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// download kyc image
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FileResult Download(int id)
        {
            var file = _kycService.GetImageById(id);
            return File(file.KycPhoto, file.ContentType);
        }
        /// <summary>
        /// verify otp code
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="customerId"></param>
        /// <param name="otp"></param>
        /// <returns></returns>
        public JsonResult VerifyOtp(string mobile, string customerId, string otp)
        {
            try
            {
                var response = _kycService.VerifyMobileNumber(mobile, customerId, otp);
                return Json(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// send otp to th mobile number
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public JsonResult SendOtp(string mobile, string customerId)
        {
            try
            {
                var response = _kycService.SendOtp(mobile, customerId);
                return Json(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// fill state, city, area, zone by pincode id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult FillAddressByPinCode(int id)
        {
            try
            {
                var response = _kycService.FillAddressByPinCode(id);
                return Json(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}