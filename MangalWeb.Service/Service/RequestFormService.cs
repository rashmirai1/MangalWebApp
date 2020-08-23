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
    public class RequestFormService
    {
        RequestFormRepository _requestFormRepository = new RequestFormRepository();

        /// <summary>
        /// get source of application list to fill dropdown
        /// </summary>
        /// <returns></returns>
        public List<Mst_SourceofApplication> GetSourceOfApplicationList()
        {
            return _requestFormRepository.GetSourceOfApplicationList();
        }

        public RequestFormViewModel GetPincodDetails(int id)
        {
            var pincodedetails = _requestFormRepository.GetPincodDetails(id);
            return pincodedetails;
        }

        public IList<Mst_PinCode> GetAllPincodes()
        {
            return _requestFormRepository.GetAllPincodes();
        }

        public void SaveRecord(RequestFormViewModel model)
        {
            _requestFormRepository.SaveRecord(model);
        }

        public List<RequestFormViewModel> GetKYCList()
        {
            return _requestFormRepository.GetKYCList();
        }

        public RequestFormViewModel GetRequestFormById(int id)
        {
            return _requestFormRepository.GetRequestFormById(id);
        }

        public int GetMaxTransactionId()
        {
            return _requestFormRepository.GetMaxTransactionId();
        }
    }
}
