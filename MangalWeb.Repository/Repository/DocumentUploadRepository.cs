using MangalWeb.Model.Entity;
using MangalWeb.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MangalWeb.Repository.Repository
{
    public class DocumentUploadRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public DocumentUploadViewModel GetAllDocumentUpload()
        {
            var model = new DocumentUploadViewModel();
            model.DocumentUploadList = new List<DocumentUploadDetailsVM>();
            var transactionid = _context.Trn_DocumentUpload.Any() ? _context.Trn_DocumentUpload.Max(x => x.TransactionId) + 1 : 1;
            model.TransactionNumber = "D0000" + transactionid;
            model.DocDate = DateTime.Now.ToShortDateString();
            return model;
        }

        public List<TGLKYC_BasicDetails> GetKYCList()
        {
            return _context.TGLKYC_BasicDetails.ToList();
        }

        public List<Mst_DocumentType> GetDocumentTypeList()
        {
            return _context.Mst_DocumentType.ToList();
        }

        public List<tblDocumentMaster> GetDocumentMasterList()
        {
            return _context.tblDocumentMasters.ToList();
        }

        public List<tblDocumentMaster> GetDocumentMasterById(int id)
        {
            return _context.tblDocumentMasters.Where(x => x.DocumentType == id).ToList();
        }

        public DocumentUploadViewModel GetCustomerById(int id)
        {
            var tblkyc = _context.TGLKYC_BasicDetails.Where(x => x.KYCID == id).FirstOrDefault();
            var model = new DocumentUploadViewModel();
            model.CustomerId = tblkyc.CustomerID;
            model.ApplicationNo = tblkyc.ApplicationNo;
            model.LoanAccountNo = "0";
            model.DocumentUploadList = new List<DocumentUploadDetailsVM>();
            var transactionid = _context.Trn_DocumentUpload.Any() ? _context.Trn_DocumentUpload.Max(x => x.TransactionId) + 1 : 1;
            model.TransactionNumber = "D0000" + transactionid;
            model.DocDate = DateTime.Now.ToShortDateString();
            return model;
        }


        public List<tblStateMaster> GetStateMasterList()
        {
            var statelist = _context.tblStateMasters.ToList();
            return statelist;
        }

        public void DeleteRecord(int id)
        {
            var trndata = _context.Trn_DocumentUpload.Where(x => x.TransactionId == id).ToList();
            //Delete the data from Installation Type Data
            if (trndata != null)
            {
                foreach (var doctrn in trndata)
                {
                    _context.Trn_DocumentUpload.Remove(doctrn);
                }
                _context.SaveChanges();
            }
        }
        public void SaveUpdateRecord(DocumentUploadViewModel DocUploadViewModel)
        {
            Trn_DocumentUpload tblDocUpload = new Trn_DocumentUpload();
            Trn_DocUploadDetails tbldocuploaddetails = new Trn_DocUploadDetails();
            try
            {
                if (DocUploadViewModel.ID <= 0)
                {
                    //save the data in Document Upload Details table
                    string output = Regex.Match(DocUploadViewModel.TransactionNumber, @"\d+").Value;
                    int trnasid = Convert.ToInt32(output);
                    tblDocUpload.TransactionId = trnasid;
                    tblDocUpload.TransactionNumber = DocUploadViewModel.TransactionNumber;
                    tblDocUpload.DocDate = Convert.ToDateTime(DocUploadViewModel.DocDate);
                    tblDocUpload.CustomerId = DocUploadViewModel.CustomerId;
                    tblDocUpload.ApplicationNo = DocUploadViewModel.ApplicationNo;
                    tblDocUpload.LoanAccountNo = DocUploadViewModel.LoanAccountNo;
                    tblDocUpload.Comments = DocUploadViewModel.Comments;
                    tblDocUpload.BranchId = 1;
                    tblDocUpload.FinancialYearId = 1;
                    tblDocUpload.CompId = 1;
                    tblDocUpload.Status = "P";
                    tblDocUpload.RecordCreatedBy = DocUploadViewModel.CreatedBy;
                    tblDocUpload.RecordCreated = DateTime.Now;
                    tblDocUpload.RecordUpdatedBy = DocUploadViewModel.UpdatedBy;
                    tblDocUpload.RecordUpdated = DateTime.Now;
                    _context.Trn_DocumentUpload.Add(tblDocUpload);
                    _context.SaveChanges();

                    int PID = _context.Trn_DocumentUpload.Max(x => x.DocId);


                    foreach (var p in DocUploadViewModel.DocumentUploadList)
                    {
                        var docuptrn = new Trn_DocUploadDetails
                        {
                            DocUploadId=PID,
                            DocumentTypeId = p.DocumentTypeId,
                            DocumentId = p.DocumentId,
                            ExpiryDate = p.ExpiryDate,
                            UploadFileBase64 = p.UploadDocName,
                        };
                        _context.Trn_DocUploadDetails.Add(docuptrn);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    //update the data in Charge Details table
                    var tblDocUploadObj = _context.Trn_DocumentUpload.Where(x => x.DocId == DocUploadViewModel.ID).FirstOrDefault();
                    //update the data in product rate table
                    tblDocUploadObj.TransactionId = DocUploadViewModel.TransactionId;
                    tblDocUploadObj.TransactionNumber = DocUploadViewModel.TransactionNumber;
                    tblDocUploadObj.DocDate = Convert.ToDateTime(DocUploadViewModel.DocDate);
                    tblDocUploadObj.CustomerId = DocUploadViewModel.CustomerId;
                    tblDocUploadObj.ApplicationNo = DocUploadViewModel.ApplicationNo;
                    tblDocUploadObj.LoanAccountNo = DocUploadViewModel.LoanAccountNo;
                    tblDocUploadObj.Comments = DocUploadViewModel.Comments;
                    tblDocUploadObj.Status = "P";
                    tblDocUploadObj.RecordUpdatedBy = DocUploadViewModel.UpdatedBy;
                    tblDocUploadObj.RecordUpdated = DateTime.Now;
                    _context.SaveChanges();

                    List<Trn_DocUploadDetails> NewDocUploadDetails = new List<Trn_DocUploadDetails>();

                    //update the data in Charge Details table
                    foreach (var p in DocUploadViewModel.DocumentUploadList)
                    {
                        var FindRateobject = _context.Trn_DocUploadDetails.Where(x => x.Id == p.ID && x.DocUploadId == DocUploadViewModel.ID).FirstOrDefault();
                        if (FindRateobject == null)
                        {
                            var ratetrnnew = new Trn_DocUploadDetails
                            {
                                DocUploadId = DocUploadViewModel.ID,
                                DocumentTypeId = p.DocumentTypeId,
                                DocumentId = p.DocumentId,
                                ExpiryDate = p.ExpiryDate,
                                FileName = p.FileName,
                                FileExtension = p.FileExtension,
                                UploadFileBase64 = p.UploadDocName
                            };
                            _context.Trn_DocUploadDetails.Add(ratetrnnew);
                        }
                        else
                        {
                            FindRateobject.DocumentTypeId = p.DocumentTypeId;
                            FindRateobject.DocumentId = p.DocumentId;
                            FindRateobject.ExpiryDate = p.ExpiryDate;
                            FindRateobject.FileName = p.FileName;
                            FindRateobject.FileExtension = p.FileExtension;
                            FindRateobject.UploadFileBase64 = p.UploadDocName;
                        }
                        NewDocUploadDetails.Add(FindRateobject);
                    }
                    #region product rate details remove
                    //take the loop of table and check from list if found in list then not remove else remove from table itself
                    var trnobjlist = _context.Trn_DocUploadDetails.Where(x => x.DocUploadId == DocUploadViewModel.ID).ToList();
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
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CheckCityNameExists(string Name)
        {
            var state = _context.tblCityMasters.Where(x => x.CityName == Name).Select(x => x.CityName).FirstOrDefault();
            return state;
        }

        public DocumentUploadViewModel SetRecordinEdit(int id)
        {
            DocumentUploadViewModel documentUploadViewModel = new DocumentUploadViewModel();
            //get document upload table
            var docupload = _context.Trn_DocumentUpload.Where(x => x.DocId == id).FirstOrDefault();
            var docuploaddetails = _context.Trn_DocUploadDetails.Where(x => x.DocUploadId == id).ToList();
            documentUploadViewModel = ToViewModelDocUpload(docupload, docuploaddetails);
            return documentUploadViewModel;
        }

        #region ToViewModelDocUpload
        public DocumentUploadViewModel ToViewModelDocUpload(Trn_DocumentUpload docupload, List<Trn_DocUploadDetails> DocUploadTrnList)
        {
            var model = new DocumentUploadViewModel
            {
                TransactionNumber =docupload.TransactionNumber,
                DocDate = Convert.ToDateTime(docupload.DocDate).ToShortDateString(),
                CustomerId = docupload.CustomerId,
                ApplicationNo = docupload.ApplicationNo,
                LoanAccountNo = docupload.LoanAccountNo,
                TransactionId = docupload.TransactionId,
                ID=docupload.DocId
            };

            List<DocumentUploadDetailsVM> DocTrnViewModelList = new List<DocumentUploadDetailsVM>();
            foreach (var c in DocUploadTrnList)
            {
                var TrnViewModel = new DocumentUploadDetailsVM
                {
                    DocumentTypeId = (int)c.DocumentTypeId,
                    DocumentTypeName = _context.Mst_DocumentType.Where(x => x.Id == c.DocumentTypeId).Select(x => x.Name).FirstOrDefault(),
                    DocumentName = _context.tblDocumentMasters.Where(x => x.DocumentID == c.DocumentId).Select(x => x.DocumentName).FirstOrDefault(),
                    DocumentId = (int)c.DocumentId,
                    ExpiryDate = c.ExpiryDate,
                    UploadDocName = c.UploadFileBase64,
                };
                DocTrnViewModelList.Add(TrnViewModel);
            }
            model.DocumentUploadList = DocTrnViewModelList;
            return model;
        }
        #endregion
        public List<DocumentUploadViewModel> SetModalList()
        {
            List<DocumentUploadViewModel> list = new List<DocumentUploadViewModel>();
            var model = new DocumentUploadViewModel();
            var tablelist = _context.Trn_DocumentUpload.ToList();
            foreach (var item in tablelist)
            {
                model = new DocumentUploadViewModel();
                model.ID = item.TransactionId;
                model.TransactionId = item.TransactionId;
                model.TransactionNumber = item.TransactionNumber;
                model.DocDate = Convert.ToDateTime(item.DocDate).ToShortDateString();
                model.ApplicationNo = item.ApplicationNo;
                model.CustomerId = item.CustomerId;
                model.LoanAccountNo = item.LoanAccountNo;
                list.Add(model);
            }
            return list;
        }
    }
}
