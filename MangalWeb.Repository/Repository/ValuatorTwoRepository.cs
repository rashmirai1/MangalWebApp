using MangalWeb.Model.Entity;
using MangalWeb.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        #region GetValuatorOneList
        public List<ValuatorTwoViewModel> GetValuatorOneList()
        {
            int branchid = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
            int fyid = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);

            var list = _context.Database.SqlQuery<ValuatorTwoViewModel>("GetValuatorOneList @BranchId,@FYID",
                new SqlParameter("@BranchId", branchid),
                new SqlParameter("@FYID", fyid)).ToList();
            return list;
        }
        #endregion

        #region GetValuatorTwoList
        public List<ValuatorTwoViewModel> GetValuatorTwoList()
        {
            int branchid = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
            int fyid = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);

            var list = _context.Database.SqlQuery<ValuatorTwoViewModel>("SP_GetValuatorTwoRecord @BranchId,@FYID",
                new SqlParameter("@BranchId", branchid),
                new SqlParameter("@FYID", fyid)).ToList();
            return list;
        }

        #endregion

        #region GetMaxTransactionId
        public ValuatorTwoViewModel GetMaxTransactionId()
        {
            var model = new ValuatorTwoViewModel();
            var transactionid = _context.Tbl_ValuationTwo.Any() ? _context.Tbl_ValuationTwo.Max(x => x.Id) + 1 : 1;
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
            Tbl_ValuationTwo tblValtwo = new Tbl_ValuationTwo();
            try
            {
                if (model.ID <= 0)
                {
                    //save the data in Document Upload Details table
                    tblValtwo.TransactionId = model.TransactionId;
                    tblValtwo.ValuatorOneId = model.ValuatorOneId;
                    tblValtwo.KYCID = model.KycId;
                    tblValtwo.CustomerID = model.CustomerId;
                    tblValtwo.ApplicationNo = model.ApplicationNo;
                    tblValtwo.Comments = model.Comments;
                    tblValtwo.BranchId = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
                    tblValtwo.CompId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    tblValtwo.FinancialYearId = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);
                    tblValtwo.CreatedBy = model.CreatedBy;
                    tblValtwo.CreatedDate = DateTime.Now;
                    tblValtwo.ConsolidatedImage = model.ConsolidatedImageFile;
                    tblValtwo.ImageName = model.ImageName;
                    tblValtwo.ContentType = model.ContentType;
                    _context.Tbl_ValuationTwo.Add(tblValtwo);
                    _context.SaveChanges();

                    int maxid = _context.Tbl_ValuationTwo.Any() ? _context.Tbl_ValuationTwo.Max(x => x.Id) : 1;
                    foreach (var p in model.ValuatorTwoDetailsList)
                    {
                        var trn = new tbl_ValuationTwoDetails
                        {
                            ValuationTwoID = maxid,
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
                        _context.tbl_ValuationTwoDetails.Add(trn);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    //update the data in Charge Details table
                    var tblObj = _context.Tbl_ValuationTwo.Where(x => x.Id == model.ID).FirstOrDefault();
                    tblObj.TransactionId = model.TransactionId.ToString();
                    tblObj.CustomerID = model.CustomerId;
                    tblObj.ApplicationNo = model.ApplicationNo;
                    tblObj.Comments = model.Comments;
                    tblObj.CreatedBy = model.CreatedBy;
                    tblObj.CreatedDate = DateTime.Now;
                    tblObj.BranchId = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
                    tblObj.CompId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    tblObj.FinancialYearId = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);
                    tblObj.ConsolidatedImage = model.ConsolidatedImageFile;
                    tblObj.ImageName = model.ImageName;
                    tblObj.ContentType = model.ContentType;
                    _context.SaveChanges();

                    List<tbl_ValuationTwoDetails> NewtblDetails = new List<tbl_ValuationTwoDetails>();

                    //update the data in Charge Details table
                    foreach (var p in model.ValuatorTwoDetailsList)
                    {
                        var Findobject = _context.tbl_ValuationTwoDetails.Where(x => x.ValuationTwoID == model.ID && x.Id == p.ID).FirstOrDefault();
                        if (Findobject == null)
                        {
                            var trn = new tbl_ValuationTwoDetails
                            {
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
                            _context.tbl_ValuationTwoDetails.Add(trn);
                            _context.SaveChanges();
                        }
                        else
                        {
                            Findobject.ValuationTwoID = tblObj.Id;
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
                    var trnobjlist = _context.tbl_ValuationTwoDetails.Where(x => x.ValuationTwoID == model.ID).ToList();
                    if (trnobjlist != null)
                    {
                        foreach (tbl_ValuationTwoDetails item in trnobjlist)
                        {
                            if (NewtblDetails.Contains(item))
                            {
                                continue;
                            }
                            else
                            {
                                _context.tbl_ValuationTwoDetails.Remove(item);
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
            var trndata = _context.tbl_ValuationTwoDetails.Where(x => x.ValuationTwoID == id).ToList();
            if (trndata != null)
            {
                foreach (var trn in trndata)
                {
                    _context.tbl_ValuationTwoDetails.Remove(trn);
                }
                _context.SaveChanges();
            }
            var data = _context.Tbl_ValuationTwo.Find(id);
            _context.Tbl_ValuationTwo.Remove(data);
            _context.SaveChanges();
        }
        #endregion

        #region GetConsolidatedTwoImage
        public Tbl_ValuationTwo GetConsolidatedTwoImage(int id)
        {
            return _context.Tbl_ValuationTwo.Where(x => x.Id == id).FirstOrDefault();
        }
        #endregion

        #region GetValuationTwoImage
        public tbl_ValuationTwoDetails GetValuationTwoImage(int id)
        {
            return _context.tbl_ValuationTwoDetails.Where(x => x.Id == id).FirstOrDefault();
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
