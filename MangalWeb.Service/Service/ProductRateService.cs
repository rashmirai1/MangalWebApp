using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using MangalWeb.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Service.Service
{
    public class ProductRateService
    {
        ProductRateRepository _productRateRepository = new ProductRateRepository();

        public ProductRateViewModel GetProductRateById(int id)
        {
            return _productRateRepository.SetRecordinEdit(id);
        }

        public List<Mst_Product> GetProductList()
        {
            return _productRateRepository.GetProductList();
        }

        public List<Mst_PurityMaster> GetPurityById(int id)
        {
            return _productRateRepository.GetPurityMasterById(id);
        }

        public List<Mst_PurityMaster> GetAllPurityMaster()
        {
            return _productRateRepository.GetPurityMasterList();
        }

        public void DeleteRecord(int id)
        {
            _productRateRepository.DeleteRecord(id);
        }

        public void SaveRecord(ProductRateViewModel model)
        {
            _productRateRepository.SaveRecord(model);
        }

        public List<ProductRateViewModel> SetDataofModalList()
        {
            return _productRateRepository.SetDataofModalList();
        }
    }
}
