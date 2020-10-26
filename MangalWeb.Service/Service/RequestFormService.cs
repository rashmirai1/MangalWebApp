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

        public RequestFormViewModel GetPincodDetails(int id)
        {
            return _requestFormRepository.GetPincodDetails(id);
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
