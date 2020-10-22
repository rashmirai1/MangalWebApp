using MangalWeb.Model.Entity;
using MangalWeb.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

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

        public List<DocumentUploadViewModel> GetKYCList()
        {
            return _context.Database.SqlQuery<DocumentUploadViewModel>("GetKYCDetailsForDocument").ToList();
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

        public bool GetExpiryDate(int id)
        {
            return _context.tblDocumentMasters.Where(x => x.DocumentID == id).Select(x => x.ExpiryDateApplicable).FirstOrDefault();
        }

        public DocumentUploadViewModel GetCustomerById(int id)
        {
            //var tblkyc = _context.TGLKYC_BasicDetails.Where(x => x.KYCID == id).FirstOrDefault();
            var tblkyc = _context.Database.SqlQuery<DocumentUploadViewModel>("GetKYCDetailsForDocumentById @id",new SqlParameter("@id", id)).FirstOrDefault();
            var model = new DocumentUploadViewModel();
            model.CustomerId = tblkyc.CustomerId;
            model.ApplicationNo = tblkyc.ApplicationNo;
            model.LoanAccountNo = tblkyc.LoanAccountNo;
            model.DocumentUploadList = new List<DocumentUploadDetailsVM>();
            var transactionid = _context.Trn_DocumentUpload.Any() ? _context.Trn_DocumentUpload.Max(x => x.TransactionId) + 1 : 1;
            model.TransactionNumber = "D0000" + transactionid;
            model.DocDate = DateTime.Now.ToShortDateString();
            return model;
        }

        public void DeleteRecord(int id)
        {
            var deleterecord = _context.Trn_DocumentUpload.Where(x => x.DocId == id).FirstOrDefault();

            var ratetrndata = _context.Trn_DocUploadDetails.Where(x => x.KycId == deleterecord.KycId).ToList();
            //Delete the data from Installation Type Data
            if (ratetrndata != null)
            {
                foreach (var ratetrn in ratetrndata)
                {
                    _context.Trn_DocUploadDetails.Remove(ratetrn);
                }
                _context.SaveChanges();
            }
            
            if (deleterecord != null)
            {
                _context.Trn_DocumentUpload.Remove(deleterecord);
                _context.SaveChanges();
            }
        }

        public void SaveUpdateRecord(DocumentUploadViewModel DocUploadViewModel)
        {
            Trn_DocumentUpload tblDocUpload = new Trn_DocumentUpload();
            try
            {
                string output = Regex.Match(DocUploadViewModel.TransactionNumber, @"\d+").Value;
                int trnasid = Convert.ToInt32(output);
                DocUploadViewModel.TransactionId = trnasid;
                if (DocUploadViewModel.ID <= 0)
                {
                    //save the data in Document Upload Details table
                    tblDocUpload.TransactionId = DocUploadViewModel.TransactionId;
                    tblDocUpload.KycId = DocUploadViewModel.KycId;
                    tblDocUpload.TransactionNumber = DocUploadViewModel.TransactionNumber;
                    tblDocUpload.DocDate = Convert.ToDateTime(DocUploadViewModel.DocDate);
                    tblDocUpload.CustomerId = DocUploadViewModel.CustomerId;
                    tblDocUpload.ApplicationNo = DocUploadViewModel.ApplicationNo;
                    tblDocUpload.LoanAccountNo = DocUploadViewModel.LoanAccountNo;
                    tblDocUpload.Comments = DocUploadViewModel.Comments;
                    tblDocUpload.BranchId = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
                    tblDocUpload.CompId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    tblDocUpload.FinancialYearId = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);
                    tblDocUpload.RecordCreatedBy = DocUploadViewModel.CreatedBy;
                    tblDocUpload.RecordCreated = DateTime.Now;
                    tblDocUpload.RecordUpdatedBy = DocUploadViewModel.UpdatedBy;
                    tblDocUpload.RecordUpdated = DateTime.Now;
                    _context.Trn_DocumentUpload.Add(tblDocUpload);
                    _context.SaveChanges();

                    foreach (var p in DocUploadViewModel.DocumentUploadList)
                    {
                        var docuptrn = new Trn_DocUploadDetails
                        {
                            KycId = DocUploadViewModel.KycId,
                            DocumentTypeId = (int)p.DocumentTypeId,
                            DocumentId = (int)p.DocumentId,
                            ExpiryDate = p.ExpiryDate,
                            FileName = p.FileName,
                            ContentType = p.FileExtension,
                            UploadFile = p.UploadDocName,
                            Status = "Pending",
                            SpecifyOther=p.SpecifyOther,
                            NameonDocument=p.NameonDocument
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
                    tblDocUploadObj.KycId = DocUploadViewModel.KycId;
                    tblDocUploadObj.TransactionId = DocUploadViewModel.TransactionId;
                    tblDocUploadObj.TransactionNumber = DocUploadViewModel.TransactionNumber;
                    tblDocUploadObj.DocDate = Convert.ToDateTime(DocUploadViewModel.DocDate);
                    tblDocUploadObj.CustomerId = DocUploadViewModel.CustomerId;
                    tblDocUploadObj.ApplicationNo = DocUploadViewModel.ApplicationNo;
                    tblDocUploadObj.LoanAccountNo = DocUploadViewModel.LoanAccountNo;
                    tblDocUploadObj.Comments = DocUploadViewModel.Comments;
                    tblDocUploadObj.RecordUpdatedBy = DocUploadViewModel.UpdatedBy;
                    tblDocUploadObj.RecordUpdated = DateTime.Now;
                    _context.SaveChanges();

                    List<Trn_DocUploadDetails> NewDocUploadDetails = new List<Trn_DocUploadDetails>();

                    //update the data in Charge Details table
                    foreach (var p in DocUploadViewModel.DocumentUploadList)
                    {
                        var FindRateobject = _context.Trn_DocUploadDetails.Where(x => x.Id == p.ID && x.KycId == tblDocUploadObj.KycId).FirstOrDefault();
                        if (FindRateobject == null)
                        {
                            var ratetrnnew = new Trn_DocUploadDetails
                            {
                                KycId = DocUploadViewModel.KycId,
                                DocumentTypeId = (int)p.DocumentTypeId,
                                DocumentId = (int)p.DocumentId,
                                ExpiryDate = p.ExpiryDate,
                                FileName = p.FileName,
                                ContentType = p.FileExtension,
                                UploadFile = p.UploadDocName,
                                SpecifyOther=p.SpecifyOther,
                                NameonDocument=p.NameonDocument,
                                Status = "Pending"
                            };
                            _context.Trn_DocUploadDetails.Add(ratetrnnew);
                        }
                        else
                        {
                            FindRateobject.KycId = tblDocUploadObj.KycId;
                            FindRateobject.DocumentTypeId = (int)p.DocumentTypeId;
                            FindRateobject.DocumentId = (int)p.DocumentId;
                            FindRateobject.ExpiryDate = p.ExpiryDate;
                            FindRateobject.FileName = p.FileName;
                            FindRateobject.ContentType = p.FileExtension;
                            FindRateobject.UploadFile = p.UploadDocName;
                            FindRateobject.SpecifyOther = p.SpecifyOther;
                            FindRateobject.NameonDocument = p.NameonDocument;
                        }
                        NewDocUploadDetails.Add(FindRateobject);
                    }
                    #region product rate details remove
                    //take the loop of table and check from list if found in list then not remove else remove from table itself
                    var trnobjlist = _context.Trn_DocUploadDetails.Where(x => x.KycId ==DocUploadViewModel.KycId).ToList();
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

        public DocumentUploadViewModel SetRecordinEdit(int id)
        {
            DocumentUploadViewModel documentUploadViewModel = new DocumentUploadViewModel();
            //get document upload table
            var docupload = _context.Trn_DocumentUpload?.Where(x => x.DocId == id)?.FirstOrDefault() ?? new Trn_DocumentUpload();
            var docuploaddetails = _context.Trn_DocUploadDetails?.Where(x => x.KycId == docupload.KycId)?.ToList() ?? new List<Trn_DocUploadDetails>();
            documentUploadViewModel = ToViewModelDocUpload(docupload, docuploaddetails);
            return documentUploadViewModel;
        }

        #region ToViewModelDocUpload
        public DocumentUploadViewModel ToViewModelDocUpload(Trn_DocumentUpload docupload, List<Trn_DocUploadDetails> DocUploadTrnList)
        {
            var model = new DocumentUploadViewModel
            {
                KycId = docupload.KycId,
                TransactionNumber = docupload.TransactionNumber,
                DocDate = Convert.ToDateTime(docupload.DocDate).ToShortDateString(),
                CustomerId = docupload.CustomerId,
                ApplicationNo = docupload.ApplicationNo,
                LoanAccountNo = docupload.LoanAccountNo,
                TransactionId = docupload.TransactionId,
                ID = docupload.DocId,
                Comments=docupload.Comments
            };

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
                    SpecifyOther=c.SpecifyOther,
                    NameonDocument=c.NameonDocument,
                    KycId = c.KycId,
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
                model.KycId = item.KycId;
                model.ID = item.DocId;
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

        public Trn_DocUploadDetails GetUploadDocuments(int id)
        {
            return _context.Trn_DocUploadDetails.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
