using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Repository.Repository
{
    public class ProductRateRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<Mst_PurityMaster> GetPurityMasterList()
        {
            return _context.Mst_PurityMaster.ToList();
        }

        public List<Mst_PurityMaster> GetPurityMasterById(int id)
        {
            return _context.Mst_PurityMaster.Where(x => x.PurityType == id).ToList();
        }

        public void DeleteRecord(int id)
        {
            var ratetrndata = _context.Mst_ProductRateDetails.Where(x => x.Prd_FkId == id).ToList();
            //Delete the data from Installation Type Data
            if (ratetrndata != null)
            {
                foreach (var ratetrn in ratetrndata)
                {
                    _context.Mst_ProductRateDetails.Remove(ratetrn);
                }
                _context.SaveChanges();
            }
            var deleterecord = _context.Mst_ProductRate.Where(x => x.Pr_Id == id).FirstOrDefault();
            if (deleterecord != null)
            {
                _context.Mst_ProductRate.Remove(deleterecord);
                _context.SaveChanges();
            }
        }

        public void SaveRecord(ProductRateViewModel productratevm)
        {
            Mst_ProductRate tblProductRate = new Mst_ProductRate();
            Mst_ProductRateDetails tblProductRateDetails = new Mst_ProductRateDetails();
            if (productratevm.ID <= 0)
            {
                tblProductRate.Pr_Date = Convert.ToDateTime(productratevm.ProductRateDate);
                tblProductRate.Pr_Product = productratevm.Product;
                tblProductRate.Pr_RecordCreated = DateTime.Now;
                tblProductRate.Pr_RecordCreatedBy = productratevm.CreatedBy;
                tblProductRate.Pr_RecordUpdated = DateTime.Now;
                tblProductRate.Pr_RecordUpdatedBy = productratevm.UpdatedBy;
                _context.Mst_ProductRate.Add(tblProductRate);
                _context.SaveChanges();

                int PID = _context.Mst_ProductRate.Max(x => x.Pr_Id);

                foreach (var p in productratevm.ProductRateList)
                {
                    var prdrate = new Mst_ProductRateDetails
                    {
                        Prd_FkId = PID,
                        Prd_Purity = p.Purity,
                        Prd_GrossRate = p.GrossRate,
                        Prd_Deductions = p.DeductionsType,
                        Prd_DeductionsAmount = p.DeductionAmount,
                        Prd_NetRate = p.NetRate,
                        Prd_RecordCreatedBy = productratevm.CreatedBy,
                        Prd_RecordCreated = DateTime.Now,
                        Prd_RecordUpdatedBy = productratevm.UpdatedBy,
                        Prd_RecordUpdated = DateTime.Now,
                    };
                    _context.Mst_ProductRateDetails.Add(prdrate);
                    _context.SaveChanges();
                }
            }
            else
            {
                //update the data in Charge Details table
                var productObj = _context.Mst_ProductRate.Where(x => x.Pr_Id == productratevm.ID).FirstOrDefault();
                //update the data in product rate table
                productObj.Pr_Date = Convert.ToDateTime(productratevm.ProductRateDate);
                productObj.Pr_Product = productratevm.Product;
                productObj.Pr_RecordUpdatedBy = productratevm.UpdatedBy;
                productObj.Pr_RecordUpdated = DateTime.Now;

                List<Mst_ProductRateDetails> NewProductRateList = new List<Mst_ProductRateDetails>();

                //update the data in Charge Details table
                foreach (var p in productratevm.ProductRateList)
                {
                    var FindRateobject = _context.Mst_ProductRateDetails.Where(x => x.Prd_Id == p.ID && x.Prd_FkId == productratevm.ID).FirstOrDefault();
                    if (FindRateobject == null)
                    {
                        var ratetrnnew = new Mst_ProductRateDetails
                        {
                            Prd_FkId = productratevm.ID,
                            Prd_Purity = p.Purity,
                            Prd_GrossRate = p.GrossRate,
                            Prd_Deductions = p.DeductionsType,
                            Prd_DeductionsAmount = p.DeductionAmount,
                            Prd_NetRate = p.NetRate,
                            Prd_RecordCreated = DateTime.Now,
                            Prd_RecordCreatedBy = productratevm.CreatedBy,
                            Prd_RecordUpdatedBy = productratevm.UpdatedBy,
                            Prd_RecordUpdated = DateTime.Now
                        };
                        _context.Mst_ProductRateDetails.Add(ratetrnnew);
                    }
                    else
                    {
                        FindRateobject.Prd_Purity = p.Purity;
                        FindRateobject.Prd_GrossRate = p.GrossRate;
                        FindRateobject.Prd_Deductions = p.DeductionsType;
                        FindRateobject.Prd_DeductionsAmount = p.DeductionAmount;
                        FindRateobject.Prd_NetRate = p.NetRate;
                        FindRateobject.Prd_RecordUpdatedBy = productratevm.UpdatedBy;
                        FindRateobject.Prd_RecordUpdated = DateTime.Now;
                    }
                    NewProductRateList.Add(FindRateobject);
                }
                #region product rate details remove
                //take the loop of table and check from list if found in list then not remove else remove from table itself
                var trnobjlist = _context.Mst_ProductRateDetails.Where(x => x.Prd_FkId == productratevm.ID).ToList();
                if (trnobjlist != null)
                {
                    foreach (Mst_ProductRateDetails item in trnobjlist)
                    {
                        if (NewProductRateList.Contains(item))
                        {
                            continue;
                        }
                        else
                        {
                            _context.Mst_ProductRateDetails.Remove(item);
                        }
                    }
                    _context.SaveChanges();
                }
                #endregion product trn remove
            }
        }

        public List<ProductRateViewModel> SetDataofModalList()
        {
            List<ProductRateViewModel> list = new List<ProductRateViewModel>();
            var model = new ProductRateViewModel();
            var tablelist = _context.Mst_ProductRate.ToList();
            foreach (var item in tablelist)
            {
                model = new ProductRateViewModel();
                model.ID = item.Pr_Id;
                model.ProductRateDate = item.Pr_Date.ToShortDateString();
                model.ProductStr = item.Pr_Product == 1 ? "Gold" : "Diamond";
                list.Add(model);
            }
            return list;
        }

        public ProductRateViewModel SetRecordinEdit(int ID)
        {
            var productRatevm = new ProductRateViewModel();
            var Productrate = _context.Mst_ProductRate.Where(x => x.Pr_Id == ID).FirstOrDefault();
            productRatevm.ProductRateDate = Productrate.Pr_Date.ToShortDateString();
            var Producttrndatalist = _context.Mst_ProductRateDetails.Where(x => x.Prd_FkId == ID).ToList();
            productRatevm = ToViewModelProductRate(Productrate, Producttrndatalist);
            return productRatevm;
        }

        public ProductRateViewModel ToViewModelProductRate(Mst_ProductRate product, ICollection<Mst_ProductRateDetails> ProductTrnList)
        {
            var rateviewmodel = new ProductRateViewModel
            {
                Product = product.Pr_Product,
                ProductRateDate = product.Pr_Date.ToShortDateString(),
                ID = product.Pr_Id,
            };
            List<ProductRateDetailsVM> ProductTrnViewModelList = new List<ProductRateDetailsVM>();
            foreach (var c in ProductTrnList)
            {
                var rateTrnViewModel = new ProductRateDetailsVM();
                rateTrnViewModel.ID = c.Prd_Id;
                rateTrnViewModel.Purity = c.Prd_Purity;
                var puritystr = _context.Mst_PurityMaster.Where(x => x.id == c.Prd_Purity).Select(x => x.PurityName).FirstOrDefault();
                rateTrnViewModel.PurityStr = puritystr;
                rateTrnViewModel.GrossRate = c.Prd_GrossRate;
                rateTrnViewModel.DeductionsType = c.Prd_Deductions;
                rateTrnViewModel.DeductionTypeStr = c.Prd_Deductions == 1 ? "Amount" : "Diamond";
                rateTrnViewModel.DeductionAmount = c.Prd_DeductionsAmount;
                rateTrnViewModel.NetRate = c.Prd_NetRate;
                ProductTrnViewModelList.Add(rateTrnViewModel);
            }
            rateviewmodel.ProductRateList = ProductTrnViewModelList;
            return rateviewmodel;
        }
    }
}

