
using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Repository
{
    public class DocumentRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<tblDocumentMaster> GetAllDocumentMasters()
        {
            var list = _context.tblDocumentMasters.ToList();
            return list;
        }

        public tblDocumentMaster GetDocumentById(int id)
        {
            var source = _context.tblDocumentMasters.Where(x => x.DocumentID == id).FirstOrDefault();
            return source;
        }

        public List<Mst_DocumentType> GetDcoumentTypeList()
        {
            var list = _context.Mst_DocumentType.ToList();
            return list;
        }

        public string CheckDocumentNameExists(string Name)
        {
            var document = _context.tblDocumentMasters.Where(x => x.DocumentName == Name).Select(x => x.DocumentName).FirstOrDefault();
            return document;
        }

        public void DeleteRecord(int id)
        {
            var deleterecord = _context.tblDocumentMasters.Where(x => x.DocumentID == id).FirstOrDefault();
            if (deleterecord != null)
            {
                _context.tblDocumentMasters.Remove(deleterecord);
                _context.SaveChanges();
            }
        }

        public void SaveUpdateRecord(DocumentViewModel document)
        {
            tblDocumentMaster tblDocument = new tblDocumentMaster();
            if (document.ID <= 0)
            {
                _context.tblDocumentMasters.Add(tblDocument);
            }
            else
            {
                tblDocument = _context.tblDocumentMasters.Where(x => x.DocumentID == document.ID).FirstOrDefault();
            }
            tblDocument.DocumentName = document.DocumentName;
            tblDocument.DocumentType = document.DocumentType;
            tblDocument.ExpiryDateApplicable = false;
            if (document.ExpiryApplicableStr == "Yes")
                tblDocument.ExpiryDateApplicable = true;
            tblDocument.Status = document.DocumentStatus;
            _context.SaveChanges();
        }

        public DocumentViewModel SetRecordinEdit(tblDocumentMaster tblDocument)
        {
            DocumentViewModel document = new DocumentViewModel();
            document.ID = tblDocument.DocumentID;
            document.DocumentName = tblDocument.DocumentName;
            document.DocumentType = tblDocument.DocumentType;
            document.ExpiryDateApplicable = (bool)tblDocument.ExpiryDateApplicable;
            document.ExpiryApplicableStr = "No";
            if (document.ExpiryDateApplicable == true)
            {
                document.ExpiryApplicableStr = "Yes";
            }
            document.DocumentStatus = (short)tblDocument.Status;
            return document;
        }

        public List<DocumentViewModel> SetDataofModalList()
        {
            List<DocumentViewModel> list = new List<DocumentViewModel>();
            var model = new DocumentViewModel();
            var tablelist = _context.tblDocumentMasters.ToList();
            bool expiryapplicable = true;
            foreach (var item in tablelist)
            {
                model = new DocumentViewModel();
                model.ID = item.DocumentID;
                model.DocumentName = item.DocumentName;
                model.DocumentTypeStr = _context.Mst_DocumentType.Where(x => x.Id == item.DocumentType).Select(x => x.Name).FirstOrDefault();
                model.ExpiryApplicableStr = item.ExpiryDateApplicable == expiryapplicable ? "Yes" : "No";
                model.DocumentStatusStr = item.Status == 1 ? "Active" : "Inactive";
                list.Add(model);
            }
            return list;
        }

    }
}
