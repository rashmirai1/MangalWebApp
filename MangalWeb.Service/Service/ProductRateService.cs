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
            var product = _productRateRepository.SetRecordinEdit(id);
            return product;
        }

        public List<Mst_PurityMaster> GetPurityById(int id)
        {
            var purity = _productRateRepository.GetPurityMasterById(id);
            return purity;
        }

        public List<Mst_PurityMaster> GetAllPurityMaster()
        {
            var purity = _productRateRepository.GetPurityMasterList();
            return purity;
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
            var list = _productRateRepository.SetDataofModalList();
            return list;
        }
    }
}
