using MangalWeb.Model.Entity;
using MangalWeb.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Repository.Repository
{
    public class RequestFormRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<Mst_SourceofApplication> GetSourceOfApplicationList()
        {
            return _context.Mst_SourceofApplication.ToList();
        }

        public List<DocumentUploadViewModel> GetKYCList()
        {
            return _context.Database.SqlQuery<DocumentUploadViewModel>("GetKYCDetailsForDocument").ToList();
        }

        public int GetMaxTransactionId()
        {
            return _context.Trn_RequestForm.Any() ? _context.Trn_RequestForm.Max(x => x.Id) : 1;
        }

        public IList<Mst_PinCode> GetAllPincodes()
        {
            return _context.Mst_PinCode.ToList();
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
                TrnViewModel.CityName = _context.tblCityMasters.Where(x => x.CityID == c.CityID).Select(x => x.CityName).FirstOrDefault();
                TrnViewModel.StateName = _context.tblStateMasters.Where(x => x.StateID == c.StateID).Select(x => x.StateName).FirstOrDefault();
                TrnViewModel.ZoneName = _context.tblZonemasters.Where(x => x.ZoneID == Convert.ToInt32(c.ZoneId)).Select(x => x.Zone).FirstOrDefault();
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
                    UploadDocName = c.UploadFile,
                    FileName = c.FileName,
                    FileExtension = c.ContentType,
                    KycId = c.KycId,
                    Status = c.Status,
                    VerifiedBy = c.VerifiedBy,
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
                if (model != null)
                {
                    tGLKYC_Basic.BldgHouseName = model.BldgHouseName;
                    tGLKYC_Basic.BldgPlotNo = model.BldgPlotNo;
                    tGLKYC_Basic.BranchID = model.BranchID;
                    tGLKYC_Basic.CityID = model.CityID;
                    tGLKYC_Basic.CmpID = model.CmpID;
                    tGLKYC_Basic.CustomerID = model.CustomerID;
                    tGLKYC_Basic.EmailID = model.EmailID;
                    tGLKYC_Basic.FYID = model.FYID;
                    tGLKYC_Basic.Landmark = model.Landmark;
                    tGLKYC_Basic.MobileNo = model.MobileNo;
                    tGLKYC_Basic.OfficeAddress = model.OfficeAddress;
                    tGLKYC_Basic.Road = model.Road;
                    tGLKYC_Basic.RoomBlockNo = model.RoomBlockNo;
                    tGLKYC_Basic.StateID = model.StateID;
                    tGLKYC_Basic.TelephoneNo = model.TelephoneNo;
                    tGLKYC_Basic.ZoneID = model.ZoneID;
                    tGLKYC_Basic.KYCDate = model.KYCDate;
                    tGLKYC_Basic.AdhaarNo = model.AdhaarNo;
                    tGLKYC_Basic.ApplicationNo = model.ApplicationNo;
                    tGLKYC_Basic.PinCode = model.PinCode;
                    tGLKYC_Basic.Distance = model.Distance;
                    tGLKYC_Basic.Area = model.Area;
                    tGLKYC_Basic.UpdatedDate = DateTime.Now;
                    _context.SaveChanges();
                    foreach (var item in model.Trans_KYCAddresses)
                    {
                        Trans_KYCAddresses trans_KYCAddresses = new Trans_KYCAddresses();
                        trans_KYCAddresses = _context.Trans_KYCAddresses.Where(x => x.KYCID == model.KycId && x.ID == item.ID).FirstOrDefault();
                        if (trans_KYCAddresses != null)
                        {
                            trans_KYCAddresses.AddressCategory = item.AddressCategory;
                            trans_KYCAddresses.Area = item.Area;
                            trans_KYCAddresses.BuildingHouseName = item.BuildingHouseName;
                            trans_KYCAddresses.BuildingPlotNo = item.BuildingPlotNo;
                            trans_KYCAddresses.CityID = item.CityID;
                            trans_KYCAddresses.CreatedDate = DateTime.Now;
                            trans_KYCAddresses.Distance_km = item.Distance_km;
                            trans_KYCAddresses.KYCID = tGLKYC_Basic.KYCID;
                            trans_KYCAddresses.NearestLandmark = item.NearestLandmark;
                            trans_KYCAddresses.PinCode = item.PinCode;
                            trans_KYCAddresses.ResidenceCode = item.ResidenceCode;
                            trans_KYCAddresses.Road = item.Road;
                            trans_KYCAddresses.RoomBlockNo = item.RoomBlockNo;
                            trans_KYCAddresses.StateID = item.StateID;
                            trans_KYCAddresses.ZoneId = item.ZoneId;

                            _context.SaveChanges();
                        }
                    }
                    var requestform = new Trn_RequestForm
                    {
                        Id = model.TransactionId,
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
