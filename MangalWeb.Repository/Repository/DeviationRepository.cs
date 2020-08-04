using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Repository
{
    public class DeviationRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<tbl_UserCategory> GetUserCategoryList()
        {
            return _context.tbl_UserCategory.ToList();
        }

        public DeviationViewModel GetAllDeviation()
        {
            var model = new DeviationViewModel();

            model.roiDeviationDetailsList = new List<RoiDeviationDetailsVM>();
            model.distanceDeviationDetailsList = new List<DistanceDeviationDetailsVM>();
            model.sanctionDeviationList = new List<SanctionDeviationVM>();
            model.tenureDeviationList = new List<TenureDeviationVM>();
            model.lTVDeviationDetailsList = new List<LTVDeviationDetailsVM>();

            var getdeviation = _context.Mst_ChildDeviation.ToList();
            var roimodellist = new List<RoiDeviationDetailsVM>();
            var distancemodellist = new List<DistanceDeviationDetailsVM>();
            var sanctionmodellist = new List<SanctionDeviationVM>();
            var tenuremodellist = new List<TenureDeviationVM>();
            var ltvmodellist = new List<LTVDeviationDetailsVM>();

            foreach (var item in getdeviation)
            {
                if (item.ParentId == 1)
                {
                    var roimodel = new RoiDeviationDetailsVM();
                    roimodel.ID = item.Id;
                    roimodel.RoiMinRange = item.MinRange;
                    roimodel.RoiMaxRange = item.MaxRange;
                    roimodel.RoiUserNo = item.UserCategoryId;
                    roimodel.RoiUserName = _context.tbl_UserCategory.Where(x => x.refid == roimodel.RoiUserNo).Select(x => x.Name).FirstOrDefault();
                    roimodellist.Add(roimodel);
                }
                if (item.ParentId == 2)
                {
                    var distancemodel = new DistanceDeviationDetailsVM();
                    distancemodel.ID = item.Id;
                    model.ApproveDistanceLimit = item.ApproveDistanceLimit;
                    distancemodel.DistanceMinRange = item.MinRange;
                    distancemodel.DistanceMaxRange = item.MaxRange;
                    distancemodel.DistanceUserNo = item.UserCategoryId;
                    distancemodel.DistanceUserNanme = _context.tbl_UserCategory.Where(x => x.refid == distancemodel.DistanceUserNo).Select(x => x.Name).FirstOrDefault();
                    distancemodellist.Add(distancemodel);
                }
                if (item.ParentId == 3)
                {
                    model.OrnamentUserNo = item.UserCategoryId;
                }
                if (item.ParentId == 4)
                {
                    var sanctionmodel = new SanctionDeviationVM();
                    sanctionmodel.ID = item.Id;
                    sanctionmodel.SanctionMinRange = item.MinRange;
                    sanctionmodel.SanctionMaxRange = item.MaxRange;
                    sanctionmodel.SanctionedUserNo = item.UserCategoryId;
                    sanctionmodel.SanctionedUserName = _context.tbl_UserCategory.Where(x => x.refid == sanctionmodel.SanctionedUserNo).Select(x => x.Name).FirstOrDefault();
                    sanctionmodellist.Add(sanctionmodel);
                }
                if (item.ParentId == 5)
                {
                    var tenuremodel = new TenureDeviationVM();
                    tenuremodel.ID = item.Id;
                    tenuremodel.TenureMinRange = item.MinRange;
                    tenuremodel.TenureMaxRange = item.MaxRange;
                    tenuremodel.TenureUserNo = item.UserCategoryId;
                    tenuremodel.TenureUserName = _context.tbl_UserCategory.Where(x => x.refid == tenuremodel.TenureUserNo).Select(x => x.Name).FirstOrDefault();
                    tenuremodellist.Add(tenuremodel);
                }
                if (item.ParentId == 6)
                {
                    model.ThresholdLimit = item.ThreasoldLimit;
                }
                if (item.ParentId == 7)
                {
                    var ltvmodel = new LTVDeviationDetailsVM();
                    ltvmodel.ID = item.Id;
                    ltvmodel.LTVMinRange = item.MinRange;
                    ltvmodel.LTVMaxRange = item.MaxRange;
                    ltvmodel.LTVUserNo = item.UserCategoryId;
                    ltvmodel.LTVUserName = _context.tbl_UserCategory.Where(x => x.refid == ltvmodel.LTVUserNo).Select(x => x.Name).FirstOrDefault();
                    ltvmodellist.Add(ltvmodel);
                }
            }
            model.roiDeviationDetailsList = roimodellist;
            model.distanceDeviationDetailsList = distancemodellist;
            model.sanctionDeviationList = sanctionmodellist;
            model.tenureDeviationList = tenuremodellist;
            model.lTVDeviationDetailsList = ltvmodellist;
            model.ID = getdeviation.Select(x => x.Id).FirstOrDefault();
            return model;
        }

        public void DeleteRecord(int id)
        {
            var deviationtable = _context.Mst_ChildDeviation.ToList();
            if (deviationtable != null)
            {
                foreach (var deviationtrn in deviationtable)
                {
                    _context.Mst_ChildDeviation.Remove(deviationtrn);
                }
                _context.SaveChanges();
            }
        }

        public void SaveRecord(DeviationViewModel deviationViewModel)
        {
            using (var context = new MangalDBNewEntities())
            {
                using (DbContextTransaction dbTran = context.Database.BeginTransaction())
                {
                    try
                    {
                        //save  ROI
                        foreach (var p in deviationViewModel.roiDeviationDetailsList)
                        {
                            var roitrn = new Mst_ChildDeviation
                            {
                                ParentId = 1,
                                ApproveDistanceLimit = 0,
                                MinRange = p.RoiMinRange,
                                MaxRange = p.RoiMaxRange,
                                UserCategoryId = p.RoiUserNo,
                                ThreasoldLimit = 0,
                                RecordCreatedBy = deviationViewModel.CreatedBy,
                                RecordCreated = DateTime.Now,
                                RecordUpdatedBy = deviationViewModel.UpdatedBy,
                                RecordUpdated = DateTime.Now,
                            };
                            context.Mst_ChildDeviation.Add(roitrn);
                        }

                        //Save Distance
                        foreach (var p in deviationViewModel.distanceDeviationDetailsList)
                        {
                            var distancetrn = new Mst_ChildDeviation
                            {
                                ParentId = 2,
                                ApproveDistanceLimit = deviationViewModel.ApproveDistanceLimit,
                                MinRange = p.DistanceMinRange,
                                MaxRange = p.DistanceMaxRange,
                                UserCategoryId = p.DistanceUserNo,
                                ThreasoldLimit = 0,
                                RecordCreatedBy = deviationViewModel.CreatedBy,
                                RecordCreated = DateTime.Now,
                                RecordUpdatedBy = deviationViewModel.UpdatedBy,
                                RecordUpdated = DateTime.Now,
                            };
                            context.Mst_ChildDeviation.Add(distancetrn);
                        }

                        //Save Distance
                        var ornamenttrn = new Mst_ChildDeviation
                        {
                            ParentId = 3,
                            ApproveDistanceLimit = 0,
                            MinRange = 0,
                            MaxRange = 0,
                            UserCategoryId = deviationViewModel.OrnamentUserNo,
                            ThreasoldLimit = 0,
                            RecordCreatedBy = deviationViewModel.CreatedBy,
                            RecordCreated = DateTime.Now,
                            RecordUpdatedBy = deviationViewModel.UpdatedBy,
                            RecordUpdated = DateTime.Now,
                        };
                        context.Mst_ChildDeviation.Add(ornamenttrn);

                        //Save Sanction loan amount
                        foreach (var p in deviationViewModel.sanctionDeviationList)
                        {
                            var sanctiontrn = new Mst_ChildDeviation
                            {
                                ParentId = 4,
                                ApproveDistanceLimit = 0,
                                MinRange = p.SanctionMinRange,
                                MaxRange = p.SanctionMaxRange,
                                UserCategoryId = p.SanctionedUserNo,
                                ThreasoldLimit = 0,
                                RecordCreatedBy = deviationViewModel.CreatedBy,
                                RecordCreated = DateTime.Now,
                                RecordUpdatedBy = deviationViewModel.UpdatedBy,
                                RecordUpdated = DateTime.Now,
                            };
                            context.Mst_ChildDeviation.Add(sanctiontrn);
                        }

                        //Save Sanction loan amount
                        foreach (var p in deviationViewModel.tenureDeviationList)
                        {
                            var sanctiontrn = new Mst_ChildDeviation
                            {
                                ParentId = 5,
                                ApproveDistanceLimit = 0,
                                MinRange = p.TenureMinRange,
                                MaxRange = p.TenureMaxRange,
                                UserCategoryId = p.TenureUserNo,
                                ThreasoldLimit = 0,
                                RecordCreatedBy = deviationViewModel.CreatedBy,
                                RecordCreated = DateTime.Now,
                                RecordUpdatedBy = deviationViewModel.UpdatedBy,
                                RecordUpdated = DateTime.Now,
                            };
                            context.Mst_ChildDeviation.Add(sanctiontrn);
                        }

                        //save thresold amount
                        var thrsholdntrn = new Mst_ChildDeviation
                        {
                            ParentId = 6,
                            ApproveDistanceLimit = 0,
                            MinRange = 0,
                            MaxRange = 0,
                            UserCategoryId = 0,
                            ThreasoldLimit = deviationViewModel.ThresholdLimit,
                            RecordCreatedBy = deviationViewModel.CreatedBy,
                            RecordCreated = DateTime.Now,
                            RecordUpdatedBy = deviationViewModel.UpdatedBy,
                            RecordUpdated = DateTime.Now,
                        };
                        context.Mst_ChildDeviation.Add(thrsholdntrn);

                        //Save LTV %
                        foreach (var p in deviationViewModel.lTVDeviationDetailsList)
                        {
                            var sanctiontrn = new Mst_ChildDeviation
                            {
                                ParentId = 7,
                                ApproveDistanceLimit = 0,
                                MinRange = p.LTVMinRange,
                                MaxRange = p.LTVMaxRange,
                                UserCategoryId = p.LTVUserNo,
                                ThreasoldLimit = 0,
                                RecordCreatedBy = deviationViewModel.CreatedBy,
                                RecordCreated = DateTime.Now,
                                RecordUpdatedBy = deviationViewModel.UpdatedBy,
                                RecordUpdated = DateTime.Now,
                            };
                            context.Mst_ChildDeviation.Add(sanctiontrn);
                        }
                        context.SaveChanges();
                        dbTran.Commit();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        dbTran.Rollback();
                        throw ex;
                    }
                }

            }
        }

        public void UpdateRecord(DeviationViewModel deviationViewModel)
        {
            try
            {
                List<Mst_ChildDeviation> NewRoiDeviationList = new List<Mst_ChildDeviation>();
                List<Mst_ChildDeviation> NewDistanceDeviationList = new List<Mst_ChildDeviation>();
                List<Mst_ChildDeviation> NewSanctionDeviationList = new List<Mst_ChildDeviation>();
                List<Mst_ChildDeviation> NewTenureDeviationList = new List<Mst_ChildDeviation>();
                List<Mst_ChildDeviation> NewLtvDeviationList = new List<Mst_ChildDeviation>();

                #region update record in roi details of deviation
                //update the data in roi Details table
                foreach (var p in deviationViewModel.roiDeviationDetailsList)
                {
                    var FindRoibject = _context.Mst_ChildDeviation.Where(x => x.Id == p.ID && x.ParentId == 1).FirstOrDefault();
                    if (FindRoibject == null)
                    {
                        var roitrnnew = new Mst_ChildDeviation
                        {
                            ParentId = 1,
                            ApproveDistanceLimit = 0,
                            MinRange = p.RoiMinRange,
                            MaxRange = p.RoiMaxRange,
                            UserCategoryId = p.RoiUserNo,
                            ThreasoldLimit = 0,
                            RecordCreatedBy = deviationViewModel.CreatedBy,
                            RecordCreated = DateTime.Now,
                            RecordUpdatedBy = deviationViewModel.UpdatedBy,
                            RecordUpdated = DateTime.Now,
                        };
                        _context.Mst_ChildDeviation.Add(roitrnnew);
                    }
                    else
                    {
                        FindRoibject.MinRange = p.RoiMinRange;
                        FindRoibject.MaxRange = p.RoiMaxRange;
                        FindRoibject.UserCategoryId = p.RoiUserNo;
                        FindRoibject.RecordUpdatedBy = deviationViewModel.UpdatedBy;
                        FindRoibject.RecordUpdated = DateTime.Now;
                    }
                    NewRoiDeviationList.Add(FindRoibject);
                }
                #endregion

                #region roi details remove
                //take the loop of table and check from list if found in list then not remove else remove from table itself
                var roitrnobjlist = _context.Mst_ChildDeviation.Where(x => x.ParentId == 1).ToList();
                if (roitrnobjlist != null)
                {
                    foreach (Mst_ChildDeviation item in roitrnobjlist)
                    {
                        if (NewRoiDeviationList.Contains(item))
                        {
                            continue;
                        }
                        else
                        {
                            _context.Mst_ChildDeviation.Remove(item);
                        }
                    }
                    _context.SaveChanges();
                }
                #endregion roi trn remove

                #region update record in distance details of deviation
                //update the data in roi Details table
                foreach (var p in deviationViewModel.distanceDeviationDetailsList)
                {
                    var FindDistancebject = _context.Mst_ChildDeviation.Where(x => x.Id == p.ID && x.ParentId == 2).FirstOrDefault();
                    if (FindDistancebject == null)
                    {
                        var distancetrnnew = new Mst_ChildDeviation
                        {
                            ParentId = 2,
                            ApproveDistanceLimit = deviationViewModel.ApproveDistanceLimit,
                            MinRange = p.DistanceMinRange,
                            MaxRange = p.DistanceMaxRange,
                            UserCategoryId = p.DistanceUserNo,
                            ThreasoldLimit = 0,
                            RecordCreatedBy = deviationViewModel.CreatedBy,
                            RecordCreated = DateTime.Now,
                            RecordUpdatedBy = deviationViewModel.UpdatedBy,
                            RecordUpdated = DateTime.Now,
                        };
                        _context.Mst_ChildDeviation.Add(distancetrnnew);
                    }
                    else
                    {
                        FindDistancebject.MinRange = p.DistanceMinRange;
                        FindDistancebject.MaxRange = p.DistanceMaxRange;
                        FindDistancebject.UserCategoryId = p.DistanceUserNo;
                        FindDistancebject.RecordUpdatedBy = deviationViewModel.UpdatedBy;
                        FindDistancebject.RecordUpdated = DateTime.Now;
                    }
                    NewDistanceDeviationList.Add(FindDistancebject);
                }
                #endregion

                #region distance details remove
                //take the loop of table and check from list if found in list then not remove else remove from table itself
                var distancetrnobjlist = _context.Mst_ChildDeviation.Where(x => x.ParentId == 2).ToList();
                if (distancetrnobjlist != null)
                {
                    foreach (Mst_ChildDeviation item in distancetrnobjlist)
                    {
                        if (NewDistanceDeviationList.Contains(item))
                        {
                            continue;
                        }
                        else
                        {
                            _context.Mst_ChildDeviation.Remove(item);
                        }
                    }
                    _context.SaveChanges();
                }
                #endregion distance trn remove

                #region update ornament user
                var ornamentuser = _context.Mst_ChildDeviation.Where(x => x.ParentId == 3).FirstOrDefault();
                if (ornamentuser != null)
                {
                    ornamentuser.UserCategoryId = deviationViewModel.OrnamentUserNo;
                    _context.SaveChanges();
                }
                #endregion

                #region update record in sanction details of deviation
                //update the data in roi Details table
                foreach (var p in deviationViewModel.sanctionDeviationList)
                {
                    var FindSanctionObject = _context.Mst_ChildDeviation.Where(x => x.Id == p.ID && x.ParentId == 4).FirstOrDefault();
                    if (FindSanctionObject == null)
                    {
                        var sanctiontrnnew = new Mst_ChildDeviation
                        {
                            ParentId = 4,
                            ApproveDistanceLimit = 0,
                            MinRange = p.SanctionMinRange,
                            MaxRange = p.SanctionMaxRange,
                            UserCategoryId = p.SanctionedUserNo,
                            ThreasoldLimit = 0,
                            RecordCreatedBy = deviationViewModel.CreatedBy,
                            RecordCreated = DateTime.Now,
                            RecordUpdatedBy = deviationViewModel.UpdatedBy,
                            RecordUpdated = DateTime.Now,
                        };
                        _context.Mst_ChildDeviation.Add(sanctiontrnnew);
                    }
                    else
                    {
                        FindSanctionObject.MinRange = p.SanctionMinRange;
                        FindSanctionObject.MaxRange = p.SanctionMaxRange;
                        FindSanctionObject.UserCategoryId = p.SanctionedUserNo;
                        FindSanctionObject.RecordUpdatedBy = deviationViewModel.UpdatedBy;
                        FindSanctionObject.RecordUpdated = DateTime.Now;
                    }
                    NewSanctionDeviationList.Add(FindSanctionObject);
                }
                #endregion

                #region sanction details remove
                //take the loop of table and check from list if found in list then not remove else remove from table itself
                var sanctiontrnobjlist = _context.Mst_ChildDeviation.Where(x => x.ParentId == 4).ToList();
                if (sanctiontrnobjlist != null)
                {
                    foreach (Mst_ChildDeviation item in sanctiontrnobjlist)
                    {
                        if (NewSanctionDeviationList.Contains(item))
                        {
                            continue;
                        }
                        else
                        {
                            _context.Mst_ChildDeviation.Remove(item);
                        }
                    }
                    _context.SaveChanges();
                }
                #endregion sanction trn remove

                #region update record in tenure details of deviation
                //update the data in tenure Details table
                foreach (var p in deviationViewModel.tenureDeviationList)
                {
                    var FindTenureObject = _context.Mst_ChildDeviation.Where(x => x.Id == p.ID && x.ParentId == 5).FirstOrDefault();
                    if (FindTenureObject == null)
                    {
                        var tenuretrnnew = new Mst_ChildDeviation
                        {
                            ParentId = 5,
                            ApproveDistanceLimit = 0,
                            MinRange = p.TenureMinRange,
                            MaxRange = p.TenureMaxRange,
                            UserCategoryId = p.TenureUserNo,
                            ThreasoldLimit = 0,
                            RecordCreatedBy = deviationViewModel.CreatedBy,
                            RecordCreated = DateTime.Now,
                            RecordUpdatedBy = deviationViewModel.UpdatedBy,
                            RecordUpdated = DateTime.Now,
                        };
                        _context.Mst_ChildDeviation.Add(tenuretrnnew);
                    }
                    else
                    {
                        FindTenureObject.MinRange = p.TenureMinRange;
                        FindTenureObject.MaxRange = p.TenureMaxRange;
                        FindTenureObject.UserCategoryId = p.TenureUserNo;
                        FindTenureObject.RecordUpdatedBy = deviationViewModel.UpdatedBy;
                        FindTenureObject.RecordUpdated = DateTime.Now;
                    }
                    NewTenureDeviationList.Add(FindTenureObject);
                }
                #endregion

                #region tenure details remove
                //take the loop of table and check from list if found in list then not remove else remove from table itself
                var tenuretrnobjlist = _context.Mst_ChildDeviation.Where(x => x.ParentId == 5).ToList();
                if (tenuretrnobjlist != null)
                {
                    foreach (Mst_ChildDeviation item in tenuretrnobjlist)
                    {
                        if (NewTenureDeviationList.Contains(item))
                        {
                            continue;
                        }
                        else
                        {
                            _context.Mst_ChildDeviation.Remove(item);
                        }
                    }
                    _context.SaveChanges();
                }
                #endregion tenure trn remove

                #region update exposure of loan
                var exposure = _context.Mst_ChildDeviation.Where(x => x.ParentId == 6).FirstOrDefault();
                if (exposure != null)
                {
                    exposure.ThreasoldLimit = deviationViewModel.ThresholdLimit;
                    _context.SaveChanges();
                }
                #endregion

                #region update record in LTV details of deviation
                //update the data in LTV Details table
                foreach (var p in deviationViewModel.lTVDeviationDetailsList)
                {
                    var FindLtvObject = _context.Mst_ChildDeviation.Where(x => x.Id == p.ID && x.ParentId == 7).FirstOrDefault();
                    if (FindLtvObject == null)
                    {
                        var ltvtrnnew = new Mst_ChildDeviation
                        {
                            ParentId = 7,
                            ApproveDistanceLimit = 0,
                            MinRange = p.LTVMinRange,
                            MaxRange = p.LTVMaxRange,
                            UserCategoryId = p.LTVUserNo,
                            ThreasoldLimit = 0,
                            RecordCreatedBy = deviationViewModel.CreatedBy,
                            RecordCreated = DateTime.Now,
                            RecordUpdatedBy = deviationViewModel.UpdatedBy,
                            RecordUpdated = DateTime.Now,
                        };
                        _context.Mst_ChildDeviation.Add(ltvtrnnew);
                    }
                    else
                    {
                        FindLtvObject.MinRange = p.LTVMinRange;
                        FindLtvObject.MaxRange = p.LTVMaxRange;
                        FindLtvObject.UserCategoryId = p.LTVUserNo;
                        FindLtvObject.RecordUpdatedBy = deviationViewModel.UpdatedBy;
                        FindLtvObject.RecordUpdated = DateTime.Now;
                    }
                    NewLtvDeviationList.Add(FindLtvObject);
                }
                #endregion

                #region tenure details remove
                //take the loop of table and check from list if found in list then not remove else remove from table itself
                var ltvtrnobjlist = _context.Mst_ChildDeviation.Where(x => x.ParentId == 7).ToList();
                if (ltvtrnobjlist != null)
                {
                    foreach (Mst_ChildDeviation item in ltvtrnobjlist)
                    {
                        if (NewLtvDeviationList.Contains(item))
                        {
                            continue;
                        }
                        else
                        {
                            _context.Mst_ChildDeviation.Remove(item);
                        }
                    }
                    _context.SaveChanges();
                }
                #endregion ltv trn remove
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

