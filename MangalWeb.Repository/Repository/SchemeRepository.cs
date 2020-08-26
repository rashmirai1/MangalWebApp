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
            var list = _context.TSchemeMaster_BasicDetails.ToList();
            return list;
        }

        public TSchemeMaster_BasicDetails GetSchemeMasterById(int id)
        {
            return _context.TSchemeMaster_BasicDetails.Where(x => x.SID == id && x.isActive == "Active").FirstOrDefault();
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
                    tblSchemeMaster.CreatedDate = DateTime.Now;
                    tblSchemeMaster.CreatedBy = scheme.CreatedBy;
                    _context.TSchemeMaster_BasicDetails.Add(tblSchemeMaster);
                }
                else
                {
                    tblSchemeMaster = _context.TSchemeMaster_BasicDetails.Where(x => x.SID == scheme.SchemeId).FirstOrDefault();
                }
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
                tblSchemeMaster.EffectiveRoiPerc = scheme.EffectiveROIPerc;
                tblSchemeMaster.LockInPeriod = scheme.LockInPeriod;
                tblSchemeMaster.ProChargeType = scheme.ProcessingFeeType;
                tblSchemeMaster.ProCharge = scheme.ProcessingCharges;
                tblSchemeMaster.AmtLmtTo = scheme.MaxProcessingCharge;
                tblSchemeMaster.isActive = scheme.Status;
                tblSchemeMaster.UpdatedDate = DateTime.Now;
                tblSchemeMaster.UpdatedBy = scheme.UpdatedBy;
                tblSchemeMaster.BranchId = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
                tblSchemeMaster.CMPId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]); ;
                tblSchemeMaster.FYId = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]); ;
                _context.SaveChanges();

                int schemeid = _context.TSchemeMaster_BasicDetails.Max(x => x.SID);
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
                    tblSchemePurity.SchemeId = schemeid;
                    tblSchemePurity.PurityId = item;
                    _context.Mst_SchemePurity.Add(tblSchemePurity);
                }
                _context.SaveChanges();
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
            scheme.EffectiveROIPerc = tblScheme.EffectiveRoiPerc;
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
            var tablelist = _context.TSchemeMaster_BasicDetails.ToList();
            foreach (var item in tablelist)
            {
                model = new SchemeViewModel();
                model.SchemeName = item.SchemeName;
                model.ProductStr = _context.Mst_Product.Where(x => x.Id == item.Product).Select(x => x.Name).FirstOrDefault();
                model.SchemeId = item.SID;
                model.EditID = item.SID;
                model.SchemeType = item.SchemeType;
                model.Frequency = item.CalMethod;
                model.Status = item.isActive;
                list.Add(model);
            }
            return list;
        }
    }
}
