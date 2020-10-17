using MangalWeb.Model.Entity;
using MangalWeb.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MangalWeb.Repository.Repository
{
    public class ValuatorTwoRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        #region GetPurityMasterList
        public List<Mst_PurityMaster> GetPurityMasterList()
        {
            return _context.Mst_PurityMaster.ToList();
        }
        #endregion

        #region GetOrnamentList
        public List<tblItemMaster> GetOrnamentList()
        {
            return _context.tblItemMasters.ToList();
        }
        #endregion

        #region GetPreSanctionList
        public List<ValuatorTwoViewModel> GetValuatorOneList()
        {
            var list = _context.Database.SqlQuery<ValuatorTwoViewModel>("GetValuatorOneList").ToList();
            return list;
        }
        #endregion

        #region GetValuatorOneList
        public List<ValuatorTwoViewModel> GetValuatorTwoList()
        {
            var list = _context.Database.SqlQuery<ValuatorTwoViewModel>("SP_GetValuatorOneRecord").ToList();
            return list;
        }
        #endregion

        #region GetMaxTransactionId
        public ValuatorTwoViewModel GetMaxTransactionId()
        {
            var model = new ValuatorTwoViewModel();
            var transactionid = _context.Tran_ValuationOneDetails.Any() ? _context.Tran_ValuationOneDetails.Max(x => x.Id) + 1 : 1;
            model.TransactionId = "VT0000" + transactionid;
            return model;
        }
        #endregion

        #region GetValuatorOneDetailsById
        public ValuatorTwoViewModel GetValuatorOneDetailsById(int Id)
        {
            var model = new ValuatorTwoViewModel();
            model = (from a in _context.Tran_ValuationOneDetails
                     join b in _context.tbl_PreSanctionDetails on a.PreSanctionId equals b.Id
                     join c in _context.TSchemeMaster_BasicDetails on b.Scheme equals c.SID
                     where a.Id == Id
                     select new ValuatorTwoViewModel()
                     {
                         ID = a.Id,
                         TransactionId = a.TransactionId,
                         CustomerId = a.CustomerID,
                         ApplicationNo = a.ApplicationNo,
                         Comments = a.Comments,
                         ImageName = a.ImageName,
                         MaxLtv = (decimal)c.MaxLtv
                     }).FirstOrDefault();

            var valuatoronedetails = (from a in _context.Tran_ValuationOneDetails
                                      join b in _context.tbl_OrnamentValuationOneDetails on a.Id equals b.ValuationOneID
                                      join c in _context.tblItemMasters on b.OrnamentId equals c.ItemID
                                      join d in _context.Mst_PurityMaster on b.PurityId equals d.id
                                      where a.Id == Id
                                      select new ValuatorTwoDetailsViewModel()
                                      {
                                          ID = b.Id,
                                          //ValuatorOneId = b.ValuationOneID,
                                          OrnamentId = b.OrnamentId,
                                          OrnamentName = c.ItemName,
                                          ImageName = b.ImageName,
                                          Qty = (int)b.Qty,
                                          PurityId = b.PurityId,
                                          PurityName = d.PurityName,
                                          GrossWeight = (decimal)b.GrossWt,
                                          Deductions = (decimal)b.Deduction,
                                          NetWeight = (decimal)b.NtWt,
                                          Rate = (decimal)b.Rate,
                                          Total = (decimal)b.Total
                                      }).ToList();
            model.ValuatorTwoDetailsList = valuatoronedetails;
            return model;
        }
        #endregion

        #region SaveUpdateRecord
        public void SaveUpdateRecord(ValuatorTwoViewModel model)
        {
            Tran_ValuationOneDetails tblValOne = new Tran_ValuationOneDetails();
            try
            {
                if (model.ID <= 0)
                {
                    //save the data in Document Upload Details table
                    tblValOne.TransactionId = model.TransactionId;
                    //tblValOne.PreSanctionId = model.PreSanctionId;
                    tblValOne.CustomerID = model.CustomerId;
                    tblValOne.ApplicationNo = model.ApplicationNo;
                    tblValOne.Comments = model.Comments;
                    tblValOne.BranchId = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
                    tblValOne.CompId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    tblValOne.FinancialYearId = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);
                    tblValOne.CreatedBy = model.CreatedBy;
                    tblValOne.CreatedDate = DateTime.Now;
                    tblValOne.ConsolidatedImage = model.ConsolidatedImageFile;
                    tblValOne.ImageName = model.ImageName;
                    tblValOne.ContentType = model.ContentType;
                    _context.Tran_ValuationOneDetails.Add(tblValOne);
                    _context.SaveChanges();

                    int maxid = _context.Tran_ValuationOneDetails.Any() ? _context.Tran_ValuationOneDetails.Max(x => x.Id) : 1;
                    foreach (var p in model.ValuatorTwoDetailsList)
                    {
                        var trn = new tbl_OrnamentValuationOneDetails
                        {
                            ValuationOneID = maxid,
                            OrnamentId = p.OrnamentId,
                            OrnamentImage = p.ValuationImageFile,
                            ImageName = p.ImageName,
                            ContentType = p.ContentType,
                            Qty = p.Qty,
                            PurityId = p.PurityId,
                            GrossWt = p.GrossWeight,
                            Deduction = p.Deductions,
                            NtWt = p.NetWeight,
                            Rate = p.Rate,
                            Total = p.Total,
                            TotalValuation = p.Total
                        };
                        _context.tbl_OrnamentValuationOneDetails.Add(trn);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    //update the data in Charge Details table
                    var tblObj = _context.Tran_ValuationOneDetails.Where(x => x.Id == model.ID).FirstOrDefault();
                    //tblObj.PreSanctionId = model.PreSanctionId;
                    tblObj.TransactionId = model.TransactionId.ToString();
                    tblObj.CustomerID = model.CustomerId;
                    tblObj.ApplicationNo = model.ApplicationNo;
                    tblObj.Comments = model.Comments;
                    tblObj.CreatedBy = model.CreatedBy;
                    tblObj.CreatedDate = DateTime.Now;
                    tblObj.BranchId = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
                    tblObj.CompId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    tblObj.FinancialYearId = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);
                    //tblObj.ConsolidatedImage = model.ConsolidatedImageFile;
                    //tblObj.ImageName = model.ImageName;
                    //tblObj.ContentType = model.ContentType;
                    _context.SaveChanges();

                    List<tbl_OrnamentValuationOneDetails> NewtblDetails = new List<tbl_OrnamentValuationOneDetails>();

                    //update the data in Charge Details table
                    foreach (var p in model.ValuatorTwoDetailsList)
                    {
                        var Findobject = _context.tbl_OrnamentValuationOneDetails.Where(x => x.ValuationOneID == model.ID && x.Id == p.ID).FirstOrDefault();
                        if (Findobject == null)
                        {
                            var trn = new tbl_OrnamentValuationOneDetails
                            {
                                //ValuationOneID = p.ValuatorOneId,
                                OrnamentId = p.OrnamentId,
                                OrnamentImage = p.ValuationImageFile,
                                ImageName = p.ImageName,
                                ContentType = p.ContentType,
                                Qty = p.Qty,
                                PurityId = p.PurityId,
                                GrossWt = p.GrossWeight,
                                Deduction = p.Deductions,
                                NtWt = p.NetWeight,
                                Rate = p.Rate,
                                Total = p.Total,
                                TotalValuation = p.Total
                            };
                            _context.tbl_OrnamentValuationOneDetails.Add(trn);
                            _context.SaveChanges();
                        }
                        else
                        {
                            Findobject.ValuationOneID = tblObj.Id;
                            Findobject.OrnamentId = p.OrnamentId;
                            Findobject.OrnamentImage = p.ValuationImageFile;
                            Findobject.ContentType = p.ContentType;
                            Findobject.ImageName = p.ImageName;
                            Findobject.Qty = p.Qty;
                            Findobject.PurityId = p.PurityId;
                            Findobject.GrossWt = p.GrossWeight;
                            Findobject.Deduction = p.Deductions;
                            Findobject.NtWt = p.NetWeight;
                            Findobject.Rate = p.Rate;
                            Findobject.Total = p.Total;
                            Findobject.TotalValuation = p.TotalValuation;
                        }
                        NewtblDetails.Add(Findobject);
                    }
                    #region product rate details remove
                    //take the loop of table and check from list if found in list then not remove else remove from table itself
                    var trnobjlist = _context.tbl_OrnamentValuationOneDetails.Where(x => x.ValuationOneID == model.ID).ToList();
                    if (trnobjlist != null)
                    {
                        foreach (tbl_OrnamentValuationOneDetails item in trnobjlist)
                        {
                            if (NewtblDetails.Contains(item))
                            {
                                continue;
                            }
                            else
                            {
                                _context.tbl_OrnamentValuationOneDetails.Remove(item);
                            }
                        }
                        _context.SaveChanges();
                    }
                    #endregion  trn remove
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DeleteRecord
        public void DeleteRecord(int id)
        {
            var trndata = _context.tbl_OrnamentValuationOneDetails.Where(x => x.ValuationOneID == id).ToList();
            //Delete the data from tbl_GLChargeMaster_Details
            if (trndata != null)
            {
                foreach (var trn in trndata)
                {
                    _context.tbl_OrnamentValuationOneDetails.Remove(trn);
                }
                _context.SaveChanges();
            }
            var data = _context.Tran_ValuationOneDetails.Find(id);
            _context.Tran_ValuationOneDetails.Remove(data);
            _context.SaveChanges();
        }
        #endregion

        #region GetConsolidatedImage
        public Tran_ValuationOneDetails GetConsolidatedImage(int id)
        {
            return _context.Tran_ValuationOneDetails.Where(x => x.Id == id).FirstOrDefault();
        }
        #endregion

        #region GetValuationImage
        public tbl_OrnamentValuationOneDetails GetValuationImage(int id)
        {
            return _context.tbl_OrnamentValuationOneDetails.Where(x => x.Id == id).FirstOrDefault();
        }
        #endregion
    }
}
