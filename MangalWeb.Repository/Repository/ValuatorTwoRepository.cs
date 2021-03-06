﻿using MangalWeb.Model.Entity;
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
        public string GetMaxTransactionId()
        {
            string TransactionId = "";
            var transactionid = _context.tbl_ValuatorTwo.Any() ? _context.tbl_ValuatorTwo.Max(x => x.Id) + 1 : 1;
            TransactionId = "VT0000" + transactionid;
            return TransactionId;
        }
        #endregion

        #region GetValuatorOneDetailsById
        public ValuatorTwoViewModel GetValuatorOneDetailsById(int Id)
        {
            var model = new ValuatorTwoViewModel();
            model = (from a in _context.tbl_ValuatorOne
                     join b in _context.TGLPreSanctions on a.PreSanctionId equals b.PreSanctionID
                     join c in _context.TSchemeMaster_BasicDetails on b.SchemeID equals c.SID
                     where a.Id == Id
                     select new ValuatorTwoViewModel()
                     {
                         ValuatorOneId = a.Id,
                         KycId = (int)a.KYCId,
                         CustomerId = a.CustomerID,
                         ApplicationNo = a.ApplicationNo,
                         Comments = a.Comments,
                         ImageName = a.ImageName,
                         MaxLtv = (decimal)c.MaxLtv,
                         ProductId = b.ProductID
                     }).FirstOrDefault();

            var valuatoronedetails = (from a in _context.tbl_ValuatorOne
                                      join b in _context.tbl_ValuatorOneDetails on a.Id equals b.ValuationOneID
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
                                          ValOneTotal = (decimal)b.Total
                                      }).ToList();

            model.ValuatorTwoDetailsList = valuatoronedetails;

            return model;
        }
        #endregion

        #region GetValuatorTwoDetailsById
        public ValuatorTwoViewModel GetValuatorTwoDetailsById(int Id)
        {
            var model = new ValuatorTwoViewModel();
            model = (from a in _context.tbl_ValuatorTwo
                     join b in _context.tbl_ValuatorTwoDetails on a.Id equals b.ValuationTwoID
                     join c in _context.tbl_ValuatorOne on a.ValuatorOneId equals c.Id
                     join d in _context.TGLPreSanctions on c.PreSanctionId equals d.PreSanctionID
                     where a.Id == Id
                     select new ValuatorTwoViewModel()
                     {
                         ID = a.Id,
                         KycId = (int)a.KYCID,
                         ValuatorOneId = a.ValuatorOneId,
                         TransactionId = a.TransactionId,
                         CustomerId = a.CustomerID,
                         ApplicationNo = a.ApplicationNo,
                         Comments = a.Comments,
                         ImageName = a.ImageName,
                         LTVPerc = (decimal)a.LTVPerc,
                         MaxLtv = (decimal)a.LTVPerc,
                         EligibleLoanAmount = (decimal)a.EligibleLoanAmount,
                         SanctionLoanAmount = (decimal)a.SanctionLoanAmount,
                         ProductId = d.ProductID
                     }).FirstOrDefault();

            var valuatortwodetails = (from a in _context.tbl_ValuatorTwo
                                      join b in _context.tbl_ValuatorTwoDetails on a.Id equals b.ValuationTwoID into ps
                                      from b in ps.DefaultIfEmpty()
                                      join c in _context.tblItemMasters on b.OrnamentId equals c.ItemID
                                      join d in _context.Mst_PurityMaster on b.PurityId equals d.id
                                      where a.Id == Id
                                      select new ValuatorTwoDetailsViewModel()
                                      {
                                          ID = b.Id,
                                          ValOneDetailsId = b.ValOneId,
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
                                          ValOneTotal = (decimal)b.ValOneTotal,
                                          ValTwoTotal = (decimal)b.ValTwoTotal
                                      }).ToList();

            model.ValuatorTwoDetailsList = valuatortwodetails;

            var valuatordetails = (from a in _context.tbl_ValuatorTwo
                                   join b in _context.tbl_OrnamentEvaluationDetails on a.Id equals b.ValTwoId
                                   join c in _context.tblItemMasters on b.OrnamentId equals c.ItemID
                                   join d in _context.Mst_PurityMaster on b.PurityId equals d.id
                                   where a.Id == Id
                                   select new ValuatorTwoDetailsViewModel()
                                   {
                                       ID = b.Id,
                                       ValuatorTwoId = b.ValTwoId,
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
                                       ValTwoTotal = (decimal)b.Total
                                   }).ToList();

            model.ValuationDetailsList = valuatordetails;
            return model;
        }
        #endregion

        #region GetValuatorOneData
        public ValuatorTwoDetailsViewModel GetValuatorOneData(int Id)
        {
            var model = (from a in _context.tbl_ValuatorOne
                         join b in _context.tbl_ValuatorOneDetails on a.Id equals b.ValuationOneID
                         join c in _context.tblItemMasters on b.OrnamentId equals c.ItemID
                         join d in _context.Mst_PurityMaster on b.PurityId equals d.id
                         where b.Id == Id
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
                             ValOneTotal = (decimal)b.Total
                         }).FirstOrDefault();
            return model;
        }
        #endregion

        #region SaveUpdateRecord
        public void SaveUpdateRecord(ValuatorTwoViewModel model)
        {
            tbl_ValuatorTwo tblValtwo = new tbl_ValuatorTwo();
            try
            {
                if (model.ID <= 0)
                {
                    tblValtwo.TransactionId = model.TransactionId;
                    tblValtwo.ValuatorOneId = model.ValuatorOneId;
                    tblValtwo.KYCID = model.KycId;
                    tblValtwo.CustomerID = model.CustomerId;
                    tblValtwo.ApplicationNo = model.ApplicationNo;
                    tblValtwo.Comments = model.Comments;
                    tblValtwo.BranchId = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
                    tblValtwo.CompId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    tblValtwo.FinancialYearId = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);
                    tblValtwo.ConsolidatedImage = model.ConsolidatedImageFile;
                    tblValtwo.ImageName = model.ImageName;
                    tblValtwo.ContentType = model.ContentType;
                    tblValtwo.LTVPerc = model.LTVPerc;
                    tblValtwo.EligibleLoanAmount = model.EligibleLoanAmount;
                    tblValtwo.SanctionLoanAmount = model.SanctionLoanAmount;
                    tblValtwo.Comments = model.Comments;
                    tblValtwo.CreatedBy = model.CreatedBy;
                    tblValtwo.CreatedDate = DateTime.Now;
                    tblValtwo.UpdatedBy = model.UpdatedBy;
                    tblValtwo.UpdatedDate = DateTime.Now;
                    _context.tbl_ValuatorTwo.Add(tblValtwo);
                    _context.SaveChanges();

                    int maxid = _context.tbl_ValuatorTwo.Any() ? _context.tbl_ValuatorTwo.Max(x => x.Id) : 1;

                    foreach (var p in model.ValuatorTwoDetailsList)
                    {
                        var trn = new tbl_ValuatorTwoDetails
                        {
                            ValuationTwoID = maxid,
                            ValOneId = p.ValOneDetailsId,
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
                            ValOneTotal = p.ValOneTotal,
                            ValTwoTotal = p.ValTwoTotal
                        };
                        _context.tbl_ValuatorTwoDetails.Add(trn);
                        _context.SaveChanges();
                    }

                    foreach (var p in model.ValuationDetailsList)
                    {
                        var val = new tbl_OrnamentEvaluationDetails
                        {
                            ValTwoId = maxid,
                            SDID = 0,
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
                            Total = p.ValTwoTotal,
                            KycId = model.KycId
                        };
                        _context.tbl_OrnamentEvaluationDetails.Add(val);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    //update the data in Charge Details table
                    var tblObj = _context.tbl_ValuatorTwo.Where(x => x.Id == model.ID).FirstOrDefault();
                    tblObj.CustomerID = model.CustomerId;
                    tblObj.ValuatorOneId = model.ValuatorOneId;
                    tblObj.TransactionId = model.TransactionId;
                    tblObj.KYCID = model.KycId;
                    tblObj.ApplicationNo = model.ApplicationNo;
                    tblObj.Comments = model.Comments;
                    tblObj.UpdatedBy = model.UpdatedBy;
                    tblObj.CreatedDate = DateTime.Now;
                    tblObj.BranchId = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
                    tblObj.CompId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    tblObj.FinancialYearId = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);
                    tblObj.ConsolidatedImage = model.ConsolidatedImageFile;
                    tblObj.ImageName = model.ImageName;
                    tblObj.ContentType = model.ContentType;
                    tblObj.LTVPerc = model.LTVPerc;
                    tblObj.EligibleLoanAmount = model.EligibleLoanAmount;
                    tblObj.SanctionLoanAmount = model.SanctionLoanAmount;
                    tblObj.Comments = model.Comments;
                    _context.SaveChanges();

                    foreach (var p in model.ValuatorTwoDetailsList)
                    {
                        var Findobject = _context.tbl_ValuatorTwoDetails.Where(x => x.ValuationTwoID == model.ID && x.Id == p.ID).FirstOrDefault();
                        if (Findobject != null)
                        {
                            Findobject.ValOneId = p.ValOneDetailsId;
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
                            Findobject.ValOneTotal = p.ValOneTotal;
                            Findobject.ValTwoTotal = p.ValTwoTotal;
                            _context.SaveChanges();
                        }
                    }

                    foreach (var p in model.ValuationDetailsList)
                    {
                        var Findobject = _context.tbl_OrnamentEvaluationDetails.Where(x => x.ValTwoId == model.ID && x.Id == p.ID).FirstOrDefault();
                        if (Findobject != null)
                        {
                            Findobject.ValTwoId = tblObj.Id;
                            Findobject.SDID = 0;
                            Findobject.KycId = model.KycId;
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
                            Findobject.Total = p.ValTwoTotal;
                            _context.SaveChanges();
                        }
                    }
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
            //delete record from valuator two table
            var trndata = _context.tbl_ValuatorTwoDetails.Where(x => x.ValuationTwoID == id).ToList();
            if (trndata != null)
            {
                foreach (var trn in trndata)
                {
                    _context.tbl_ValuatorTwoDetails.Remove(trn);
                }
                _context.SaveChanges();
            }
            // delete record from ornament evaluation table
            var valdata = _context.tbl_OrnamentEvaluationDetails.Where(x => x.ValTwoId == id).ToList();
            if (valdata != null)
            {
                foreach (var val in valdata)
                {
                    _context.tbl_OrnamentEvaluationDetails.Remove(val);
                }
                _context.SaveChanges();
            }

            var data = _context.tbl_ValuatorTwo.Find(id);
            _context.tbl_ValuatorTwo.Remove(data);
            _context.SaveChanges();
        }
        #endregion

        #region GetConsolidatedTwoImage
        public tbl_ValuatorTwo GetConsolidatedTwoImage(int id)
        {
            return _context.tbl_ValuatorTwo.Where(x => x.Id == id).FirstOrDefault();
        }
        #endregion

        #region GetValuationTwoImage
        public tbl_ValuatorTwoDetails GetValuationTwoImage(int id)
        {
            return _context.tbl_ValuatorTwoDetails.Where(x => x.Id == id).FirstOrDefault();
        }
        #endregion

        #region GetConsolidatedImage
        public tbl_ValuatorOne GetConsolidatedImage(int id)
        {
            return _context.tbl_ValuatorOne.Where(x => x.Id == id).FirstOrDefault();
        }
        #endregion

        #region GetValuationImage
        public tbl_ValuatorOneDetails GetValuationImage(int id)
        {
            return _context.tbl_ValuatorOneDetails.Where(x => x.Id == id).FirstOrDefault();
        }
        #endregion

        #region CheckRecordExist
        public int CheckRecordExist(int id)
        {
            int count = 0;
            var sdid = (from a in _context.tbl_OrnamentEvaluationDetails
                        join b in _context.TGLSanctionDisburse_BasicDetails on a.KycId equals b.KYCID into kyc
                        from b in kyc.DefaultIfEmpty()
                        select a).FirstOrDefault();

            if (sdid != null && sdid.SDID > 0)
            {
                count = 1;
            }
            else
            {
                count = 0;
            }
            return count;
        }
        #endregion
    }
}
