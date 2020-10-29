using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
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
    public class RequestFormRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<RequestFormViewModel> GetKYCList()
        {
            return _context.Database.SqlQuery<RequestFormViewModel>("GetKYCDetailsRequestForm").ToList();
        }

        public int GetMaxTransactionId()
        {
            return _context.Trn_RequestForm.Any() ? _context.Trn_RequestForm.Max(x => x.Id) + 1 : 1;
        }

        public IList<Mst_PinCode> GetAllPincodes()
        {
            return _context.Mst_PinCode.ToList();
        }

        public RequestFormViewModel GetPincodDetails(int id)
        {
            var branch = (from aa in _context.Mst_PinCode
                          join bb in _context.tblZonemasters on aa.Pc_ZoneId equals bb.ZoneID
                          join cc in _context.tblCityMasters on aa.Pc_CityId equals cc.CityID
                          join dd in _context.tblStateMasters on cc.StateID equals dd.StateID
                          where aa.Pc_Id == id 
                          select new RequestFormViewModel()
                          {
                              Area = aa.Pc_AreaName,
                              ZoneName = bb.Zone,
                              ZoneID = bb.ZoneID,
                              CityID = cc.CityID,
                              CityName = cc.CityName,
                              StateID = dd.StateID,
                              StateName = dd.StateName
                          }).FirstOrDefault();
            return branch;
        }

        public RequestFormViewModel GetRequestFormById(int id)
        {
            var documentUploadViewModel = new RequestFormViewModel();
            var kycdetails = _context.Database.SqlQuery<RequestFormViewModel>("GetKYCDetailsForRequestForm @KycId", new SqlParameter("KycId", id)).FirstOrDefault();
            var KycAddressDetailsList = _context.Trans_KYCAddresses.Where(x => x.KYCID == kycdetails.KycId).ToList();
            documentUploadViewModel = ToViewModelDocUpload(kycdetails, KycAddressDetailsList);
            return documentUploadViewModel;
        }

        #region ToViewModelDocUpload
        public RequestFormViewModel ToViewModelDocUpload(RequestFormViewModel model, List<Trans_KYCAddresses> KycAddressDetailsList)
        {
            List<KYCAddressesVM> AddressDetailsList = new List<KYCAddressesVM>();
            foreach (var c in KycAddressDetailsList)
            {
                var TrnViewModel = new KYCAddressesVM();
                TrnViewModel.ID = c.ID;
                TrnViewModel.KYCID = c.KYCID;
                TrnViewModel.AddressCategory = c.AddressCategory;
                TrnViewModel.ResidenceCode = c.ResidenceCode;
                TrnViewModel.BuildingHouseName = c.BuildingHouseName;
                TrnViewModel.Road = c.Road;
                TrnViewModel.BuildingPlotNo = c.BuildingPlotNo;
                TrnViewModel.RoomBlockNo = c.RoomBlockNo;
                TrnViewModel.NearestLandmark = c.NearestLandmark;
                TrnViewModel.Distance_km = c.Distance_km;
                TrnViewModel.PinCode = (int)c.PinCode;
                TrnViewModel.ResidenceCode = c.ResidenceCode;
                AddressDetailsList.Add(TrnViewModel);
            }

            model.Trans_KYCAddresses = AddressDetailsList;

            var docuploaddetails = (from a in _context.Trn_DocUploadDetails
                                    join b in _context.Mst_DocumentType on a.DocumentTypeId equals b.Id
                                    join c in _context.tblDocumentMasters on a.DocumentId equals c.DocumentID
                                    where a.KycId == model.KycId && a.Status != "Rejected"
                                    select new DocumentUploadDetailsVM()
                                    {
                                        ID = a.Id,
                                        DocumentTypeId = a.DocumentTypeId,
                                        DocumentTypeName = b.Name,
                                        DocumentName = c.DocumentName,
                                        DocumentId = a.DocumentId,
                                        ExpiryDate = a.ExpiryDate,
                                        FileName = a.FileName,
                                        FileExtension = a.ContentType,
                                        KycId = a.KycId,
                                        Status = a.Status,
                                        VerifiedBy = a.VerifiedBy,
                                        SpecifyOther = a.SpecifyOther,
                                        NameonDocument = a.NameonDocument,
                                        ReasonForRejection = a.ReasonForRejection
                                    }).ToList();

            model.DocumentUploadList = docuploaddetails;

            return model;
        }
        #endregion

        public void SaveRecord(RequestFormViewModel model)
        {
            try
            {
                TGLKYC_BasicDetails tGLKYC_Basic = _context.TGLKYC_BasicDetails.Where(x => x.KYCID == model.KycId).FirstOrDefault();
                if (tGLKYC_Basic != null)
                {
                    tGLKYC_Basic.KYCDate = Convert.ToDateTime(model.KYCDate);
                    tGLKYC_Basic.ApplicationNo = model.ApplicationNo;
                    tGLKYC_Basic.CustomerID = model.CustomerID;
                    tGLKYC_Basic.ResidenceCode = model.ResidenceCode;
                    tGLKYC_Basic.BldgHouseName = model.BldgHouseName;
                    tGLKYC_Basic.Road = model.Road;
                    tGLKYC_Basic.BldgPlotNo = model.BldgPlotNo;
                    tGLKYC_Basic.RoomBlockNo = model.RoomBlockNo;
                    tGLKYC_Basic.Landmark = model.Landmark;
                    tGLKYC_Basic.Distance = model.Distance;
                    tGLKYC_Basic.PinCode = model.PinCode;
                    tGLKYC_Basic.MobileNo = model.MobileNo;
                    tGLKYC_Basic.EmailID = model.EmailID;
                    tGLKYC_Basic.BranchID = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
                    tGLKYC_Basic.CmpID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    tGLKYC_Basic.FYID = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);
                    tGLKYC_Basic.UpdatedDate = DateTime.Now;
                    tGLKYC_Basic.UpdatedBy = Convert.ToInt32(HttpContext.Current.Session["UserLoginId"]);
                    tGLKYC_Basic.AddressCategory = "02";
                    _context.SaveChanges();

                    foreach (var item in model.Trans_KYCAddresses)
                    {
                        Trans_KYCAddresses trans_KYCAddresses = new Trans_KYCAddresses();
                        trans_KYCAddresses = _context.Trans_KYCAddresses.Where(x => x.KYCID == model.KycId && x.ID == item.ID).FirstOrDefault();
                        if (trans_KYCAddresses != null)
                        {
                            trans_KYCAddresses.AddressCategory = item.AddressCategory;
                            trans_KYCAddresses.ResidenceCode = item.ResidenceCode;
                            trans_KYCAddresses.BuildingHouseName = item.BuildingHouseName;
                            trans_KYCAddresses.Road = item.Road;
                            trans_KYCAddresses.BuildingPlotNo = item.BuildingPlotNo;
                            trans_KYCAddresses.RoomBlockNo = item.RoomBlockNo;
                            trans_KYCAddresses.NearestLandmark = item.NearestLandmark;
                            trans_KYCAddresses.Distance_km = item.Distance_km;
                            trans_KYCAddresses.PinCode = item.PinCode;
                            trans_KYCAddresses.CreatedDate = DateTime.Now;
                            trans_KYCAddresses.KYCID = tGLKYC_Basic.KYCID;
                            _context.SaveChanges();
                        }
                    }

                    List<Trn_DocUploadDetails> NewDocUploadDetails = new List<Trn_DocUploadDetails>();
                    //update the data in Charge Details table
                    foreach (var p in model.DocumentUploadList)
                    {
                        var FindRateobject = _context.Trn_DocUploadDetails.Where(x => x.Id == p.ID && x.KycId == model.KycId).FirstOrDefault();
                        if (FindRateobject == null)
                        {
                            var ratetrnnew = new Trn_DocUploadDetails
                            {
                                KycId = model.KycId,
                                DocumentTypeId = (int)p.DocumentTypeId,
                                DocumentId = (int)p.DocumentId,
                                ExpiryDate = p.ExpiryDate,
                                FileName = p.FileName,
                                ContentType = p.FileExtension,
                                UploadFile = p.UploadDocName,
                                SpecifyOther = p.SpecifyOther,
                                NameonDocument = p.NameonDocument,
                                Status = "Pending"
                            };
                            _context.Trn_DocUploadDetails.Add(ratetrnnew);
                        }
                        else
                        {
                            var uploadfile = _context.Trn_DocUploadDetails.Where(x => x.Id == FindRateobject.Id).Select(x => x.UploadFile).FirstOrDefault();
                            FindRateobject.KycId = model.KycId;
                            FindRateobject.DocumentTypeId = (int)p.DocumentTypeId;
                            FindRateobject.DocumentId = (int)p.DocumentId;
                            FindRateobject.ExpiryDate = p.ExpiryDate;
                            FindRateobject.FileName = p.FileName;
                            FindRateobject.ContentType = p.FileExtension;
                            FindRateobject.UploadFile = uploadfile;
                            FindRateobject.SpecifyOther = p.SpecifyOther;
                            FindRateobject.NameonDocument = p.NameonDocument;
                        }
                        NewDocUploadDetails.Add(FindRateobject);
                    }

                    #region doc details remove
                    //take the loop of table and check from list if found in list then not remove else remove from table itself
                    var trnobjlist = _context.Trn_DocUploadDetails.Where(x => x.KycId == model.KycId).ToList();
                    if (trnobjlist != null)
                    {
                        foreach (Trn_DocUploadDetails item in trnobjlist)
                        {
                            if (NewDocUploadDetails.Contains(item))
                            {
                                continue;
                            }
                            else
                            {
                                _context.Trn_DocUploadDetails.Remove(item);
                            }
                        }
                        _context.SaveChanges();
                    }
                    #endregion product trn remove

                    var requestform = new Trn_RequestForm
                    {
                        KycId = model.KycId,
                        SanctionId = model.SanctionId,
                        creationdate = DateTime.Now
                    };
                    _context.Trn_RequestForm.Add(requestform);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
