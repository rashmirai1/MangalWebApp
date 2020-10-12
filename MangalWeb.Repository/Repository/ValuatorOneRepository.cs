using MangalWeb.Model.Entity;
using MangalWeb.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var list = _context.Database.SqlQuery<ValuatorOneViewModel>("GetPreSanctionListFromValuatorOne").ToList();
            return list;
        }
        #endregion

        #region GetMaxTransactionId
        public int GetMaxTransactionId()
        {
            return _context.Tran_ValuationOneDetails.Any() ? _context.Tran_ValuationOneDetails.Max(x => x.Id) + 1 : 1;
        }
        #endregion

        #region SaveUpdateRecord
        public void SaveUpdateRecord(ValuatorOneViewModel model)
        {
            Tran_ValuationOneDetails tblValOne = new Tran_ValuationOneDetails();
            try
            {
                if (model.ID <= 0)
                {
                    //save the data in Document Upload Details table
                    tblValOne.TransactionId = model.TransactionId.ToString();
                    tblValOne.PreSanctionId = model.PreSanctionId;
                    tblValOne.CustomerID = model.CustomerId;
                    tblValOne.ApplicationNo = model.ApplicationNo;
                    tblValOne.Comments = model.Comments;
                    //tblDocUpload.BranchId = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
                    //tblDocUpload.CompId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    //tblDocUpload.FinancialYearId = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);
                    tblValOne.CreatedBy = model.CreatedBy;
                    tblValOne.CreatedDate = DateTime.Now;
                    _context.Tran_ValuationOneDetails.Add(tblValOne);
                    _context.SaveChanges();

                    int maxid = _context.Tran_ValuationOneDetails.Any() ? _context.Tran_ValuationOneDetails.Max(x => x.Id) : 1;

                    foreach (var p in model.ValuatorOneDetailsList)
                    {
                        var trn = new tbl_OrnamentValuationOneDetails
                        {
                            ValuationOneID = maxid,
                            OrnamentId = p.OrnamentId,
                            OrnamentImage = null,
                            ContentType = null,
                            ImageName = null,
                            Qty = p.Qty,
                            PurityId = p.PurityId,
                            GrossWt = p.GrossWeight,
                            Deduction = p.Deductions,
                            NtWt = p.NetWeight,
                            Rate = p.Rate,
                            Total = p.Total,
                            TotalValuation = 0,
                        };
                        _context.tbl_OrnamentValuationOneDetails.Add(trn);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    //update the data in Charge Details table
                    var tblObj = _context.Tran_ValuationOneDetails.Where(x => x.Id == model.ID).FirstOrDefault();
                    tblObj.PreSanctionId = model.PreSanctionId;
                    tblObj.TransactionId = model.TransactionId.ToString();
                    tblObj.CustomerID = model.CustomerId;
                    tblObj.ApplicationNo = model.ApplicationNo;
                    tblObj.Comments = model.Comments;
                    tblObj.CreatedBy = model.CreatedBy;
                    tblObj.CreatedDate = DateTime.Now;
                    _context.SaveChanges();

                    List<tbl_OrnamentValuationOneDetails> NewtblDetails = new List<tbl_OrnamentValuationOneDetails>();

                    //update the data in Charge Details table
                    foreach (var p in model.ValuatorOneDetailsList)
                    {
                        var Findobject = _context.tbl_OrnamentValuationOneDetails.Where(x => x.ValuationOneID == model.ID && x.Id==p.ID).FirstOrDefault();
                        if (Findobject == null)
                        {
                            var trn = new tbl_OrnamentValuationOneDetails
                            {
                                ValuationOneID = p.ValuatorOneId,
                                OrnamentId = p.OrnamentId,
                                OrnamentImage = null,
                                ContentType = null,
                                ImageName = null,
                                Qty = p.Qty,
                                PurityId = p.PurityId,
                                GrossWt = p.GrossWeight,
                                Deduction = p.Deductions,
                                NtWt = p.NetWeight,
                                Rate = p.Rate,
                                Total = p.Total,
                                TotalValuation = 0,
                            };
                            _context.tbl_OrnamentValuationOneDetails.Add(trn);
                            _context.SaveChanges();
                        }
                        else
                        {
                            Findobject.ValuationOneID = tblObj.Id;
                            Findobject.OrnamentId = p.OrnamentId;
                            Findobject.OrnamentImage = null;
                            Findobject.ContentType = null;
                            Findobject.ImageName = null;
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
    }
}
