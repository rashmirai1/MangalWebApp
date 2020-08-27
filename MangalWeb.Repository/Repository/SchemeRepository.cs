using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MangalWeb.Repository.Repository
{
    public class SchemeRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();
        static List<int> Purity;

        public List<TSchemeMaster_BasicDetails> GetAllSchemeMasters()
        {
            return _context.TSchemeMaster_BasicDetails.OrderBy(x => x.SID).ToList();
        }

        public TSchemeMaster_BasicDetails GetSchemeMasterById(int id)
        {
            return _context.TSchemeMaster_BasicDetails.Where(x => x.SID == id).FirstOrDefault();
        }

        public void DeleteRecord(int id)
        {
            var getrecord = _context.Mst_SchemePurity.Where(x => x.SchemeId == id).ToList();
            if (getrecord != null)
            {
                foreach (var item1 in getrecord)
                {
                    _context.Mst_SchemePurity.Remove(item1);
                    _context.SaveChanges();
                }
            }

            var effectiveroi = _context.TSchemeMaster_EffectiveROI.Where(x => x.SID == id).ToList();
            // Delete the effective roi 
            if (effectiveroi != null)
            {
                foreach (var roitrn in effectiveroi)
                {
                    _context.TSchemeMaster_EffectiveROI.Remove(roitrn);
                }
                _context.SaveChanges();
            }

            var deleterecord = _context.TSchemeMaster_BasicDetails.Where(x => x.SID == id).FirstOrDefault();
            if (deleterecord != null)
            {
                _context.TSchemeMaster_BasicDetails.Remove(deleterecord);
                _context.SaveChanges();
            }
        }

        public void SaveUpdateRecord(SchemeViewModel scheme)
        {
            TSchemeMaster_BasicDetails tblSchemeMaster = new TSchemeMaster_BasicDetails();
            try
            {
                if (scheme.EditID <= 0)
                {
                    tblSchemeMaster.Product = scheme.Product;
                    tblSchemeMaster.SchemeName = scheme.SchemeName;
                    tblSchemeMaster.SchemeType = scheme.SchemeType;
                    tblSchemeMaster.CalMethod = scheme.Frequency;
                    tblSchemeMaster.Tenure = scheme.MinTenure;
                    tblSchemeMaster.MaxTenure = scheme.MaxTenure;
                    tblSchemeMaster.MinLoanAmt = scheme.MinLoanAmount;
                    tblSchemeMaster.MaxLoanAmt = scheme.MaxLoanAmount;
                    tblSchemeMaster.Ltv = scheme.MinLTVPerc;
                    tblSchemeMaster.MaxLtv = scheme.MaxLTVPerc;
                    tblSchemeMaster.ROI = scheme.MinROIPerc;
                    tblSchemeMaster.MaxRoi = scheme.MaxROIPerc;
                    tblSchemeMaster.GracePeriod = scheme.GracePeriod;
                    tblSchemeMaster.LockInPeriod = scheme.LockInPeriod;
                    tblSchemeMaster.ProChargeType = scheme.ProcessingFeeType;
                    tblSchemeMaster.ProCharge = scheme.ProcessingCharges;
                    tblSchemeMaster.AmtLmtTo = scheme.MaxProcessingCharge;
                    tblSchemeMaster.isActive = scheme.Status;
                    tblSchemeMaster.UpdatedDate = DateTime.Now;
                    tblSchemeMaster.UpdatedBy = scheme.UpdatedBy;
                    tblSchemeMaster.BranchId = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
                    tblSchemeMaster.CMPId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    tblSchemeMaster.FYId = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);
                    tblSchemeMaster.CreatedDate = DateTime.Now;
                    tblSchemeMaster.CreatedBy = scheme.CreatedBy;
                    _context.TSchemeMaster_BasicDetails.Add(tblSchemeMaster);
                    _context.SaveChanges();

                    //**********************************************************************************************
                    int SchemeId = _context.TSchemeMaster_BasicDetails.Max(x => x.SID);

                    foreach (var p in scheme.SchemeEffectiveROIList)
                    {
                        var effectiverow = new TSchemeMaster_EffectiveROI
                        {
                            SID = SchemeId,
                            NoofDefaultMonths = p.NoofDefaultMonths,
                            EffROI = (decimal)p.EffectiveROIPerc,
                        };
                        _context.TSchemeMaster_EffectiveROI.Add(effectiverow);
                        _context.SaveChanges();
                    }
                    //**********************************************************************************************

                    if (scheme.Purity == null)
                    {
                        scheme.Purity = Purity;
                    }

                    var getrecord = _context.Mst_SchemePurity.Where(x => x.SchemeId == scheme.SchemeId).ToList();
                    if (getrecord != null)
                    {
                        foreach (var item1 in getrecord)
                        {
                            _context.Mst_SchemePurity.Remove(item1);
                            _context.SaveChanges();
                        }
                    }
                    foreach (var item in scheme.Purity)
                    {
                        Mst_SchemePurity tblSchemePurity = new Mst_SchemePurity();
                        tblSchemePurity.SchemeId = SchemeId;
                        tblSchemePurity.PurityId = item;
                        _context.Mst_SchemePurity.Add(tblSchemePurity);
                    }
                    _context.SaveChanges();
                }
                else
                {
                    tblSchemeMaster = _context.TSchemeMaster_BasicDetails.Where(x => x.SID == scheme.SchemeId).FirstOrDefault();
                    tblSchemeMaster.Product = scheme.Product;
                    tblSchemeMaster.SchemeName = scheme.SchemeName;
                    tblSchemeMaster.SchemeType = scheme.SchemeType;
                    tblSchemeMaster.CalMethod = scheme.Frequency;
                    tblSchemeMaster.Tenure = scheme.MinTenure;
                    tblSchemeMaster.MaxTenure = scheme.MaxTenure;
                    tblSchemeMaster.MinLoanAmt = scheme.MinLoanAmount;
                    tblSchemeMaster.MaxLoanAmt = scheme.MaxLoanAmount;
                    tblSchemeMaster.Ltv = scheme.MinLTVPerc;
                    tblSchemeMaster.MaxLtv = scheme.MaxLTVPerc;
                    tblSchemeMaster.ROI = scheme.MinROIPerc;
                    tblSchemeMaster.MaxRoi = scheme.MaxROIPerc;
                    tblSchemeMaster.GracePeriod = scheme.GracePeriod;
                    tblSchemeMaster.LockInPeriod = scheme.LockInPeriod;
                    tblSchemeMaster.ProChargeType = scheme.ProcessingFeeType;
                    tblSchemeMaster.ProCharge = scheme.ProcessingCharges;
                    tblSchemeMaster.AmtLmtTo = scheme.MaxProcessingCharge;
                    tblSchemeMaster.isActive = scheme.Status;
                    tblSchemeMaster.UpdatedDate = DateTime.Now;
                    tblSchemeMaster.UpdatedBy = scheme.UpdatedBy;
                    tblSchemeMaster.BranchId = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
                    tblSchemeMaster.CMPId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    tblSchemeMaster.FYId = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);
                    _context.SaveChanges();

                    #region Effective ROI
                    List<TSchemeMaster_EffectiveROI> NewEffectiveROIList = new List<TSchemeMaster_EffectiveROI>();
                    foreach (var p in scheme.SchemeEffectiveROIList)
                    {
                        var Findobject = _context.TSchemeMaster_EffectiveROI.Where(x => x.ROIID == p.ID && x.SID == scheme.SchemeId).FirstOrDefault();
                        if (Findobject == null)
                        {
                            var ratetrnnew = new TSchemeMaster_EffectiveROI
                            {
                                SID = scheme.SchemeId,
                                NoofDefaultMonths = p.NoofDefaultMonths,
                                EffROI = (decimal)p.EffectiveROIPerc,
                            };
                            _context.TSchemeMaster_EffectiveROI.Add(ratetrnnew);
                        }
                        else
                        {
                            Findobject.NoofDefaultMonths = p.NoofDefaultMonths;
                            Findobject.EffROI = (decimal)p.EffectiveROIPerc;
                        }
                        NewEffectiveROIList.Add(Findobject);
                    }
                    #endregion

                    #region Effective ROI details remove
                    //take the loop of table and check from list if found in list then not remove else remove from table itself
                    var trnobjlist = _context.TSchemeMaster_EffectiveROI.Where(x => x.SID == scheme.SchemeId).ToList();
                    if (trnobjlist != null)
                    {
                        foreach (TSchemeMaster_EffectiveROI item in trnobjlist)
                        {
                            if (NewEffectiveROIList.Contains(item))
                            {
                                continue;
                            }
                            else
                            {
                                _context.TSchemeMaster_EffectiveROI.Remove(item);
                            }
                        }
                        _context.SaveChanges();
                    }
                    #endregion Effective ROI details remove

                    //*************************************************************************************************************
                    if (scheme.Purity == null)
                    {
                        scheme.Purity = Purity;
                    }
                    //var getrecord = _context.Mst_SchemePurity.Where(x => x.SchemeId == scheme.SchemeId).ToList();
                    //if (getrecord != null)
                    //{
                    //    foreach (var item1 in getrecord)
                    //    {
                    //        _context.Mst_SchemePurity.Remove(item1);
                    //        _context.SaveChanges();
                    //    }
                    //}
                    //foreach (var item in scheme.Purity)
                    //{
                    //    Mst_SchemePurity tblSchemePurity = new Mst_SchemePurity();
                    //    tblSchemePurity.SchemeId = scheme.SchemeId;
                    //    tblSchemePurity.PurityId = item;
                    //    _context.Mst_SchemePurity.Add(tblSchemePurity);
                    //}

                    List<Mst_SchemePurity> NewSchemePurityList = new List<Mst_SchemePurity>();
                    foreach (var p in scheme.Purity)
                    {
                        var Findobject = _context.Mst_SchemePurity.Where(x => x.SchemeId == scheme.SchemeId && x.PurityId == p).FirstOrDefault();
                        if (Findobject == null)
                        {
                            var schemepurity = new Mst_SchemePurity
                            {
                                PurityId = p,
                                SchemeId = scheme.SchemeId,
                            };
                            _context.Mst_SchemePurity.Add(schemepurity);
                        }
                        else
                        {
                            Findobject.SchemeId = scheme.SchemeId;
                            Findobject.PurityId = p;
                        }
                        NewSchemePurityList.Add(Findobject);
                    }

                    #region Effective ROI details remove
                    //take the loop of table and check from list if found in list then not remove else remove from table itself
                    var purityobjlist = _context.Mst_SchemePurity.Where(x => x.SchemeId == scheme.SchemeId).ToList();
                    if (purityobjlist != null)
                    {
                        foreach (Mst_SchemePurity item in purityobjlist)
                        {
                            if (NewSchemePurityList.Contains(item))
                            {
                                continue;
                            }
                            else
                            {
                                _context.Mst_SchemePurity.Remove(item);
                            }
                        }
                        _context.SaveChanges();
                    }
                    #endregion Effective ROI details remove

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public List<Mst_PurityMaster> GetPurityById(int id)
        {
            return _context.Mst_PurityMaster.Where(x => x.PurityType == id).ToList();
        }

        public string CheckSchemeNameExists(string Name, int id)
        {
            if (id > 0)
            {
                return _context.TSchemeMaster_BasicDetails.Where(x => x.SchemeName == Name && x.SID != id).Select(x => x.SchemeName).FirstOrDefault();
            }
            else
            {
                return _context.TSchemeMaster_BasicDetails.Where(x => x.SchemeName == Name).Select(x => x.SchemeName).FirstOrDefault();
            }
        }

        public SchemeViewModel SetRecordinEdit(TSchemeMaster_BasicDetails tblScheme)
        {
            SchemeViewModel scheme = new SchemeViewModel();
            scheme.SchemeId = tblScheme.SID;
            scheme.EditID = tblScheme.SID;
            scheme.Product = tblScheme.Product;
            scheme.SchemeName = tblScheme.SchemeName;
            scheme.SchemeType = tblScheme.SchemeType;
            scheme.Frequency = tblScheme.CalMethod;
            scheme.MinTenure = (int)tblScheme.Tenure;
            scheme.MaxTenure = (int)tblScheme.MaxTenure;
            scheme.MinLoanAmount = tblScheme.MinLoanAmt;
            scheme.MaxLoanAmount = tblScheme.MaxLoanAmt;
            scheme.MinLTVPerc = tblScheme.Ltv;
            scheme.MaxLTVPerc = tblScheme.MaxLtv;
            scheme.MinROIPerc = tblScheme.ROI;
            scheme.MaxROIPerc = tblScheme.MaxRoi;
            scheme.GracePeriod = tblScheme.GracePeriod;
            scheme.LockInPeriod = tblScheme.LockInPeriod;
            scheme.ProcessingFeeType = tblScheme.ProChargeType;
            scheme.ProcessingCharges = tblScheme.ProCharge;
            scheme.MaxProcessingCharge = tblScheme.AmtLmtTo;
            scheme.Status = tblScheme.isActive;
            List<Mst_SchemePurity> getPuritylist = _context.Mst_SchemePurity.Where(x => x.SchemeId == tblScheme.SID).ToList();
            List<int> puritytlist = new List<int>();
            foreach (var item in getPuritylist)
            {
                puritytlist.Add(item.PurityId);
            }
            scheme.Purity = puritytlist;
            Purity = puritytlist;

            var tblEffectiveRoiList = _context.TSchemeMaster_EffectiveROI.Where(x => x.SID == scheme.SchemeId).ToList();

            List<SchemeEffectiveROIVM> EffectiveROIList = new List<SchemeEffectiveROIVM>();
            foreach (var c in tblEffectiveRoiList)
            {
                var effectiveROIViewModel = new SchemeEffectiveROIVM();
                effectiveROIViewModel.ID = c.ROIID;
                effectiveROIViewModel.SchemeId = (int)c.SID;
                effectiveROIViewModel.NoofDefaultMonths = (int)c.NoofDefaultMonths;
                effectiveROIViewModel.EffectiveROIPerc = c.EffROI;
                EffectiveROIList.Add(effectiveROIViewModel);
            }
            scheme.SchemeEffectiveROIList = EffectiveROIList;
            return scheme;
        }

        public List<Mst_PurityMaster> GetPurityMasterList()
        {
            return _context.Mst_PurityMaster.ToList();
        }

        public List<Mst_Product> GetProductList()
        {
            return _context.Mst_Product.ToList();
        }

        public int GetMaxPkNo()
        {
            return _context.TSchemeMaster_BasicDetails.Any() ? _context.TSchemeMaster_BasicDetails.Max(x => x.SID) + 1 : 1;
        }

        public List<SchemeViewModel> SetModalSchemeList()
        {
            List<SchemeViewModel> list = new List<SchemeViewModel>();
            var model = new SchemeViewModel();
            var tablelist = _context.TSchemeMaster_BasicDetails.OrderBy(x=>x.SID).ToList();
            foreach (var item in tablelist)
            {
                model = new SchemeViewModel();
                model.SchemeName = item.SchemeName;
                model.ProductStr = _context.Mst_Product.Where(x => x.Id == item.Product).Select(x => x.Name).FirstOrDefault();
                model.SchemeId = item.SID;
                model.EditID = item.SID;
                model.MinLoanAmount = item.MinLoanAmt;
                model.MaxLoanAmount = item.MaxLoanAmt;
                model.MinTenure = (int)item.Tenure;
                model.MaxTenure = item.MaxTenure;
                model.SchemeType = item.SchemeType;
                model.Frequency = item.CalMethod;
                model.ProcessingFeeType = item.ProChargeType;
                model.ProcessingCharges = item.ProCharge;
                model.Status = item.isActive;
                list.Add(model);
            }
            return list;
        }
    }
}
