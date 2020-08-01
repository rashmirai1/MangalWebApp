using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using MangalWeb.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Service.Service
{
    public class DocumentService
    {
        DocumentRepository _documentRepository = new DocumentRepository();

        public List<tblDocumentMaster> GetAllDocumentMaster()
        {
            var list = _documentRepository.GetAllDocumentMasters();
            return list;
        }

        public List<Mst_DocumentType> GetDcoumentTypeList()
        {
            var list = _documentRepository.GetDcoumentTypeList();
            return list;
        }

        public tblDocumentMaster GetDocumentById(int id)
        {
            var source = _documentRepository.GetDocumentById(id);
            return source;
        }

        public string CheckDocumentNameExists(string name)
        {
            var docname = _documentRepository.CheckDocumentNameExists(name);
            return docname;
        }

        public DocumentViewModel SetDataOnEdit(tblDocumentMaster tbldoc)
        {
            var item = _documentRepository.SetRecordinEdit(tbldoc);
            return item;
        }

        public void DeleteRecord(int id)
        {
            _documentRepository.DeleteRecord(id);
        }

        public void SaveUpdateRecord(DocumentViewModel model)
        {
            _documentRepository.SaveUpdateRecord(model);
        }

        public List<DocumentViewModel> SetDataofModalList()
        {
            var sourcelist = _documentRepository.SetDataofModalList();
            return sourcelist;
        }
    }
}
