using MangalWeb.Model.Entity;
using MangalWeb.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace MangalWeb.Repository.Repository
{
    public class ValuatorOneRepository
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
        public List<ValuatorOneViewModel> GetPreSanctionList()
        {
            int branchid = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
            int fyid = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);

            var list = _context.Database.SqlQuery<ValuatorOneViewModel>("GetPreSanctionListFromValuatorOne @BranchId,@FYID",
                new SqlParameter("@BranchId", branchid),
                new SqlParameter("@FYID", fyid)).ToList();
            return list;
        }
        #endregion

        #region GetValuatorOneList
        public List<ValuatorOneViewModel> GetValuatorOneList()
        {
            int branchid = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
            int fyid = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);

            var list = _context.Database.SqlQuery<ValuatorOneViewModel>("SP_GetValuatorOneRecord @BranchId,@FYID",
                new SqlParameter("@BranchId", branchid),
                new SqlParameter("@FYID", fyid)).ToList();

            return list;
        }
        #endregion

        #region GetMaxTransactionId
        public ValuatorOneViewModel GetMaxTransactionId()
        {
            var model = new ValuatorOneViewModel();
            var transactionid = _context.tbl_ValuatorOne.Any() ? _context.tbl_ValuatorOne.Max(x => x.Id) + 1 : 1;
            model.TransactionId = "VO0000" + transactionid;
            return model;
        }
        #endregion

        #region GetValuatorOneDetailsById
        public ValuatorOneViewModel GetValuatorOneDetailsById(int Id)
        {
            var model = new ValuatorOneViewModel();
            model = (from a in _context.tbl_ValuatorOne
                     join b in _context.tbl_PreSanctionDetails on a.PreSanctionId equals b.Id
                     where a.Id == Id
                     select new ValuatorOneViewModel()
                     {
                         ID = a.Id,
                         KycId = a.KYCId,
                         TransactionId = a.TransactionId,
                         CustomerId = a.CustomerID,
                         ApplicationNo = a.ApplicationNo,
                         Comments = a.Comments,
                         ImageName = a.ImageName ?? "",
                         ProductId = b.Product,
                         PreSanctionId = (int)a.PreSanctionId
                     }).FirstOrDefault();

            var valuatoronedetails = (from a in _context.tbl_ValuatorOne
                                      join b in _context.tbl_ValuatorOneDetails on a.Id equals b.ValuationOneID
                                      join c in _context.tblItemMasters on b.OrnamentId equals c.ItemID
                                      join d in _context.Mst_PurityMaster on b.PurityId equals d.id
                                      where b.ValuationOneID == Id
                                      select new ValuatorOneDetailsViewModel()
                                      {
                                          ID = b.Id,
                                          ValuatorOneId = b.ValuationOneID,
                                          OrnamentId = b.OrnamentId,
                                          OrnamentName = c.ItemName,
                                          ImageName = b.ImageName ?? "",
                                          Qty = (int)b.Qty,
                                          PurityId = b.PurityId,
                                          PurityName = d.PurityName,
                                          GrossWeight = (decimal)b.GrossWt,
                                          Deductions = (decimal)b.Deduction,
                                          NetWeight = (decimal)b.NtWt,
                                          Rate = (decimal)b.Rate,
                                          Total = (decimal)b.Total
                                      }).ToList();
            model.ValuatorOneDetailsList = valuatoronedetails;
            return model;
        }
        #endregion

        #region SaveUpdateRecord
        public void SaveUpdateRecord(ValuatorOneViewModel model)
        {
            tbl_ValuatorOne tblValOne = new tbl_ValuatorOne();
            try
            {
                if (model.ID <= 0)
                {
                    //save the data in Document Upload Details table
                    tblValOne.TransactionId = model.TransactionId;
                    tblValOne.KYCId = model.KycId;
                    tblValOne.PreSanctionId = model.PreSanctionId;
                    tblValOne.CustomerID = model.CustomerId;
                    tblValOne.ApplicationNo = model.ApplicationNo;
                    tblValOne.Comments = model.Comments;
                    tblValOne.BranchId = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
                    tblValOne.CompId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    tblValOne.FinancialYearId = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);
                    tblValOne.CreatedBy = model.CreatedBy;
                    tblValOne.CreatedDate = DateTime.Now;
                    tblValOne.UpdatedBy = model.UpdatedBy;
                    tblValOne.UpdatedDate = DateTime.Now;
                    tblValOne.ConsolidatedImage = model.ConsolidatedImageFile;
                    tblValOne.ImageName = model.ImageName;
                    tblValOne.ContentType = model.ContentType;
                    _context.tbl_ValuatorOne.Add(tblValOne);
                    _context.SaveChanges();

                    int maxid = _context.tbl_ValuatorOne.Any() ? _context.tbl_ValuatorOne.Max(x => x.Id) : 1;
                    foreach (var p in model.ValuatorOneDetailsList)
                    {
                        var trn = new tbl_ValuatorOneDetails
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
                        };
                        _context.tbl_ValuatorOneDetails.Add(trn);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    //update the data in Charge Details table
                    var tblObj = _context.tbl_ValuatorOne.Where(x => x.Id == model.ID).FirstOrDefault();
                    tblObj.PreSanctionId = model.PreSanctionId;
                    tblObj.KYCId = model.KycId;
                    tblObj.TransactionId = model.TransactionId.ToString();
                    tblObj.CustomerID = model.CustomerId;
                    tblObj.ApplicationNo = model.ApplicationNo;
                    tblObj.Comments = model.Comments;
                    tblObj.UpdatedBy = model.CreatedBy;
                    tblObj.UpdatedDate = DateTime.Now;
                    tblObj.BranchId = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
                    tblObj.CompId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    tblObj.FinancialYearId = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);
                    tblObj.ConsolidatedImage = model.ConsolidatedImageFile;
                    tblObj.ImageName = model.ImageName;
                    tblObj.ContentType = model.ContentType;
                    _context.SaveChanges();

                    List<tbl_ValuatorOneDetails> NewtblDetails = new List<tbl_ValuatorOneDetails>();

                    //update the data in Charge Details table
                    foreach (var p in model.ValuatorOneDetailsList)
                    {
                        var Findobject = _context.tbl_ValuatorOneDetails.Where(x => x.ValuationOneID == model.ID && x.Id == p.ID).FirstOrDefault();
                        if (Findobject == null)
                        {
                            var trn = new tbl_ValuatorOneDetails
                            {
                                ValuationOneID = model.ID,
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
                                Total = p.Total
                            };
                            _context.tbl_ValuatorOneDetails.Add(trn);
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
                        }
                        NewtblDetails.Add(Findobject);
                    }
                    #region product rate details remove
                    //take the loop of table and check from list if found in list then not remove else remove from table itself
                    var trnobjlist = _context.tbl_ValuatorOneDetails.Where(x => x.ValuationOneID == model.ID).ToList();
                    if (trnobjlist != null)
                    {
                        foreach (tbl_ValuatorOneDetails item in trnobjlist)
                        {
                            if (NewtblDetails.Contains(item))
                            {
                                continue;
                            }
                            else
                            {
                                _context.tbl_ValuatorOneDetails.Remove(item);
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
            var trndata = _context.tbl_ValuatorOneDetails.Where(x => x.ValuationOneID == id).ToList();
            if (trndata != null)
            {
                foreach (var trn in trndata)
                {
                    _context.tbl_ValuatorOneDetails.Remove(trn);
                }
                _context.SaveChanges();
            }
            var data = _context.tbl_ValuatorOne.Find(id);
            _context.tbl_ValuatorOne.Remove(data);
            _context.SaveChanges();
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

        #region GetOrnamentProductWise
        public List<tblItemMaster> GetOrnamentProductWise(int id)
        {
            return _context.tblItemMasters.Where(x => x.Product == id).ToList();
        }
        #endregion

        #region GetOrnamentProductWise
        public string CheckOrnamentStatus(int id)
        {
            return _context.tblItemMasters.Where(x => x.ItemID == id).Select(x => x.Status).FirstOrDefault();
        }
        #endregion

        #region GetGoldrates
        public double GetGoldrates()
        {
            double finalrate = 0;
            string temp = string.Empty;
            string rate = string.Empty;
            try
            {
                string urlAddress = "https://www.goldpriceindia.com/wmshare-wlifop-002.php";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;
                    if (response.CharacterSet == null)
                        readStream = new StreamReader(receiveStream);
                    else
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    string data = readStream.ReadToEnd();
                    response.Close();
                    readStream.Close();

                    //temp = data.Substring(data.LastIndexOf("10 gram"), 30);
                    // string ch = "Gold price slips marginally to Rs ";
                    //string abb = data.Substring(data.LastIndexOf("Gold price slips marginally to Rs"), 40);

                    string abb = data.Substring(data.LastIndexOf("pad-15"), 14);
                    rate = abb.Substring(8, 6);
                    //*******************************************************************
                    temp = rate.Replace(",", "");

                    finalrate = Convert.ToDouble(temp);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return finalrate;
        }
        #endregion

        #region GetRateFromProductRate
        public double GetRateFromProductRate(int pProductId, int pPurityId)
        {
            double FinalRate = 0;
            double pFinalRate = 0;
            string purityname = "";
            DateTime ratedate;
            int data = 0;
            if (pProductId == 1)
            {
                pFinalRate = GetGoldrates();
                if (pFinalRate > 0)
                {
                    purityname = _context.Mst_PurityMaster.Where(x => x.id == pPurityId).Select(x => x.PurityName).FirstOrDefault();
                    data = Convert.ToInt32(Regex.Match(purityname, @"\d+").Value);
                    FinalRate = pFinalRate / 24;
                    FinalRate = FinalRate * data;
                }
                else
                {
                    var rate = (from a in _context.Mst_ProductRate
                                join b in _context.Mst_ProductRateDetails on a.Pr_Id equals b.Prd_FkId
                                where a.Pr_Product == pProductId && b.Prd_Purity == 7
                                select new
                                {
                                    FinalRate = b.Prd_NetRate,
                                    ratedate = a.Pr_Date
                                }).OrderByDescending(x => x.ratedate).FirstOrDefault();
                    if (rate != null && rate.FinalRate > 0)
                    {
                        pFinalRate = Convert.ToDouble(rate.FinalRate);
                        FinalRate = pFinalRate / 24;
                        FinalRate = FinalRate * data;
                    }
                }
            }
            else
            {
                var rate = (from a in _context.Mst_ProductRate
                            join b in _context.Mst_ProductRateDetails on a.Pr_Id equals b.Prd_FkId
                            where a.Pr_Product == pProductId && b.Prd_Purity == pProductId
                            select new
                            {
                                FinalRate = b.Prd_NetRate,
                                ratedate = a.Pr_Date
                            }).OrderByDescending(x => x.ratedate).FirstOrDefault();
                if (rate != null && rate.FinalRate > 0)
                {
                    FinalRate = Convert.ToDouble(rate.FinalRate);
                }
            }
            return FinalRate;
        }
        #endregion

        #region CheckRecordExist
        public int CheckRecordExist(int id)
        {
            return _context.tbl_ValuatorTwo.Where(x => x.ValuatorOneId == id).Select(x => x.Id).Count();
        }
        #endregion
    }
}
