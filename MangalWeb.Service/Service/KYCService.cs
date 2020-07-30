using MangalWeb.Model.Transaction;
using MangalWeb.Repository.Repository;

namespace MangalWeb.Service.Service
{
    public class KYCService
    {
        KYCRepository _kycRepository = new KYCRepository();

       
        public void SaveRecord(KYCBasicDetailsVM model)
        {
            _kycRepository.SaveRecord(model);
        }
    }
}
