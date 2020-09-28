using MangalWeb.Model.Entity;
using MangalWeb.Model.Transaction;
using MangalWeb.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Service.Service
{
    public class PreSanctionService
    {
        DocumentVerificationRepository _documentVerificationRepository = new DocumentVerificationRepository();
        PreSanctionRepository _preSanctionRepository = new PreSanctionRepository();

        public DocumentUploadViewModel GetAllDocumentUpload(int id)
        {
            return _documentVerificationRepository.GetDoumentUploadById(id);
        }
        public void SaveUpdateRecord(PreSanctionDetailsVM model)
        {
            _preSanctionRepository.SaveUpdateRecord(model);
        }

        public List<KYCBasicDetailsVM> GetCustomerList()
        {
            return _preSanctionRepository.GetCustomerList();
        }

        public List<UserDetail> GetAllRMByBranch()
        {
            return _preSanctionRepository.GetAllRMByBranch();
        }

        public PreSanctionDetailsVM GetCustomerById(int id)
        {
            return _preSanctionRepository.GetCustomerById(id);
        }
    }
}
