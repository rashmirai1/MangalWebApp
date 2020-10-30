using MangalWeb.Model.Entity;
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
    public class DocumentVerificationRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<DocumentUploadViewModel> DocumentUploadList()
        {
            int branchid = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
            int fyid = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);

            return _context.Database.SqlQuery<DocumentUploadViewModel>("GetDocumentUpload @BranchId,@FYID",
                new SqlParameter("@BranchId", branchid),
                new SqlParameter("@FYID", fyid)).ToList();
        }

        public void SaveUpdateRecord(DocumentUploadViewModel DocUploadViewModel)
        {
            Trn_DocUploadDetails tbldocuploaddetails = new Trn_DocUploadDetails();
            try
            {
                //update the data in Charge Details table
                foreach (var p in DocUploadViewModel.DocumentUploadList)
                {
                    var Findobject = _context.Trn_DocUploadDetails.Where(x => x.Id == p.ID && x.KycId == DocUploadViewModel.KycId).FirstOrDefault();
                    if (Findobject != null)
                    {
                        Findobject.VerifiedBy = Convert.ToInt32(HttpContext.Current.Session["UserLoginId"]);
                        Findobject.Status = p.Status;
                        Findobject.ReasonForRejection = p.ReasonForRejection;
                    }
                    //if (p.Status == "Rejected")
                    //{
                    //    _context.Trn_DocUploadDetails.Remove(Findobject);
                    //}
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DocumentUploadViewModel GetDoumentUploadById(int id)
        {
            DocumentUploadViewModel documentUploadViewModel = new DocumentUploadViewModel();

            int branchid = Convert.ToInt32(HttpContext.Current.Session["BranchId"]);
            int fyid = Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);

            //get document upload table
            var docupload = _context.Database.SqlQuery<DocumentUploadViewModel>("GetDocumentUploadById @id,@BranchId,@FYID",
                new SqlParameter("@id", id),
                new SqlParameter("@BranchId", branchid),
                new SqlParameter("@FYID", fyid)
                ).FirstOrDefault();
            documentUploadViewModel = ToViewModelDocUpload(docupload);
            return documentUploadViewModel;
        }

        #region ToViewModelDocUpload
        public DocumentUploadViewModel ToViewModelDocUpload(DocumentUploadViewModel model)
        {
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
    }
}
