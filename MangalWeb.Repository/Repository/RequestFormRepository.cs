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

        public List<Mst_SourceofApplication> GetSourceOfApplicationList()
        {
            return _context.Mst_SourceofApplication.ToList();
        }

        public List<RequestFormViewModel> GetKYCList()
        {
            return _context.Database.SqlQuery<RequestFormViewModel>("GetKYCDetailsForDocument").ToList();
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
            var branch = new RequestFormViewModel();
            var pincodemodel = _context.Mst_PinCode.Where(x => x.Pc_Id == id).Select(x => new PincodeViewModel { CityId = x.Pc_CityId, ZoneId = x.Pc_ZoneId, AreaName = x.Pc_AreaName }).FirstOrDefault();
            var ZoneName = _context.tblZonemasters.Where(x => x.ZoneID == pincodemodel.ZoneId).Select(x => x.Zone).FirstOrDefault();
            var cityname = _context.tblCityMasters.Where(x => x.CityID == pincodemodel.CityId).Select(x => new CityViewModel { CityName = x.CityName, StateId = (int)x.StateID }).FirstOrDefault();
            var statename = _context.tblStateMasters.Where(x => x.StateID == cityname.StateId).Select(x => x.StateName).FirstOrDefault();
            branch.Area = pincodemodel.AreaName;
            branch.ZoneName = ZoneName;
            branch.CityName = cityname.CityName;
            branch.StateName = statename;
            branch.StateID = cityname.StateId;
            branch.CityID = pincodemodel.CityId;
            branch.ZoneID = pincodemodel.ZoneId;
            return branch;
        }

        public RequestFormViewModel GetRequestFormById(int id)
        {
            RequestFormViewModel documentUploadViewModel = new RequestFormViewModel();
            //get document upload table
            //var kycdetails = _context.TGLKYC_BasicDetails.Where(x => x.KYCID == id).FirstOrDefault();
            var kycdetails = _context.Database.SqlQuery<RequestFormViewModel>("GetKYCDetailsForRequestForm @KycId", new SqlParameter("KycId", id)).FirstOrDefault();
            var docuploaddetails = _context.Trn_DocUploadDetails.Where(x => x.KycId == kycdetails.KycId).ToList();
            var KycAddressDetailsList = _context.Trans_KYCAddresses.Where(x => x.KYCID == kycdetails.KycId).ToList();
            documentUploadViewModel = ToViewModelDocUpload(kycdetails, docuploaddetails, KycAddressDetailsList);
            return documentUploadViewModel;
        }

        #region ToViewModelDocUpload
        public RequestFormViewModel ToViewModelDocUpload(RequestFormViewModel model, List<Trn_DocUploadDetails> DocUploadTrnList, List<Trans_KYCAddresses> KycAddressDetailsList)
        {
            //var model = new RequestFormViewModel
            //{
            //    KycId = kycdetails.KycId,
            //    CreationDate = kycdetails.KYCDate,
            //    CustomerID = kycdetails.CustomerID,
            //    ApplicationNo = kycdetails.ApplicationNo,
            //    LoanAccountNo = kycdetails.LoanAccountNo,
            //    ID = kycdetails.KycId,
            //    BldgHouseName=kycdetails.BldgHouseName,
            //    Road=kycdetails.Road,
            //    BldgPlotNo=kycdetails.BldgPlotNo,
            //    RoomBlockNo=kycdetails.RoomBlockNo,
            //    Landmark=kycdetails.Landmark,
            //    Distance=kycdetails.Distance,
            //    PinCode=kycdetails.PinCode,
            //    StateID=kycdetails.StateID,
            //    CityID=kycdetails.CityID,
            //    Area=kycdetails.Area,
            //    ZoneID=kycdetails.ZoneID
            //};

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
                TrnViewModel.PinCode = c.PinCode;
                TrnViewModel.StateID = c.StateID;
                TrnViewModel.CityID = c.CityID;
                TrnViewModel.Area = c.Area;
                TrnViewModel.ZoneId = c.ZoneId;
                int zoneid = Convert.ToInt32(TrnViewModel.ZoneId);
                TrnViewModel.CityName = _context.tblCityMasters.Where(x => x.CityID == c.CityID).Select(x => x.CityName).FirstOrDefault();
                TrnViewModel.StateName = _context.tblStateMasters.Where(x => x.StateID == c.StateID).Select(x => x.StateName).FirstOrDefault();
                TrnViewModel.ZoneName = _context.tblZonemasters.Where(x => x.ZoneID == zoneid).Select(x => x.Zone).FirstOrDefault();
                TrnViewModel.ResidenceCode = c.ResidenceCode;
                AddressDetailsList.Add(TrnViewModel);
            }
            model.Trans_KYCAddresses = AddressDetailsList;

            List<DocumentUploadDetailsVM> DocTrnViewModelList = new List<DocumentUploadDetailsVM>();
            foreach (var c in DocUploadTrnList)
            {
                var TrnViewModel = new DocumentUploadDetailsVM
                {
                    ID = c.Id,
                    DocumentTypeId = (int)c.DocumentTypeId,
                    DocumentTypeName = _context.Mst_DocumentType.Where(x => x.Id == c.DocumentTypeId).Select(x => x.Name).FirstOrDefault(),
                    DocumentName = _context.tblDocumentMasters.Where(x => x.DocumentID == c.DocumentId).Select(x => x.DocumentName).FirstOrDefault(),
                    DocumentId = (int)c.DocumentId,
                    ExpiryDate = c.ExpiryDate,
                    //UploadDocName = c.UploadFile,
                    FileName = c.FileName,
                    FileExtension = c.ContentType,
                    KycId = c.KycId,
                    Status = c.Status,
                    VerifiedBy = c.VerifiedBy,
                    SpecifyOther = c.SpecifyOther,
                    NameonDocument = c.NameonDocument,
                    ReasonForRejection = c.ReasonForRejection
                };
                DocTrnViewModelList.Add(TrnViewModel);
            }

            model.DocumentUploadList = DocTrnViewModelList;
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
                    tGLKYC_Basic.StateID = model.StateID;
                    tGLKYC_Basic.CityID = model.CityID;
                    tGLKYC_Basic.Area = model.Area;
                    tGLKYC_Basic.ZoneID = model.ZoneID;
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
                            trans_KYCAddresses.StateID = item.StateID;
                            trans_KYCAddresses.CityID = item.CityID;
                            trans_KYCAddresses.Area = item.Area;
                            trans_KYCAddresses.ZoneId = item.ZoneId;
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
                            var uploadfile= _context.Trn_DocUploadDetails.Where(x => x.Id == FindRateobject.Id).Select(x=>x.UploadFile).FirstOrDefault();
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
