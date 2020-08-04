using MangalWeb.Model.Entity;
using MangalWeb.Model.Transaction;
using MangalWeb.Repository.Repository;
using System.Collections.Generic;
using System.Web;

namespace MangalWeb.Service.Service
{
    public class KYCService
    {
        KYCRepository _kycRepository = new KYCRepository();


        public void SaveRecord(KYCBasicDetailsVM model)
        {
            _kycRepository.SaveRecord(model);
        }
        public List<Mst_SourceofApplication> GetSourceOfApplicationList()
        {
            var list = _kycRepository.GetSourceOfApplicationList();
            return list;
        }

        public KYCBasicDetailsVM doesPanExist(string Pan)
        {
            var kycVm = _kycRepository.doesPanExist(Pan);
            return kycVm;
        }

        public KYCBasicDetailsVM doesAdharExist(string AdharNo)
        {
            var kycVm = _kycRepository.doesAdharExist(AdharNo);
            return kycVm;
        }

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
    }
}
