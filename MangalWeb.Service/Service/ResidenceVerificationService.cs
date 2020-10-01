﻿using MangalWeb.Model.Entity;
using MangalWeb.Model.Transaction;
using MangalWeb.Model.Utilities;
using MangalWeb.Repository.Repository;
using System.Collections.Generic;

namespace MangalWeb.Service.Service
{
    public class ResidenceVerificationService
    {
        DocumentVerificationRepository _documentVerificationRepository = new DocumentVerificationRepository();
        ResidenceVerificationRepository _residenceVerificationRepository = new ResidenceVerificationRepository();

        public DocumentUploadViewModel GetAllDocumentUpload(int id)
        {
            return _documentVerificationRepository.GetDoumentUploadById(id);
        }
        public void SaveUpdateRecord(ResidenceVerificationVM model)
        {
            _residenceVerificationRepository.SaveUpdateRecord(model);
        }

        public List<KYCBasicDetailsVM> GetCustomerList()
        {
            return _residenceVerificationRepository.GetCustomerList();
        }

        public List<UserDetail> GetAllRMByBranch()
        {
            return _residenceVerificationRepository.GetAllRMByBranch();
        }

        public ResidenceVerificationVM GetCustomerById(int id)
        {
            return _residenceVerificationRepository.GetCustomerById(id);
        }

        public int GetDocumentID(int id)
        {
            return _residenceVerificationRepository.GetDocumentID(id);
        }

        public UserViewModel FillEmployeeDetailsById(int id)
        {
            return _residenceVerificationRepository.FillEmployeeDetailsById(id);
        }
    }
}