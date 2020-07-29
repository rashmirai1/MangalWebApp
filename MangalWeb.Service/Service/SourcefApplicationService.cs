using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using MangalWeb.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Service.Service
{
   public class SourcefApplicationService
    {
        SourceofApplicationRepository _sourceRepository = new SourceofApplicationRepository();

        public List<Mst_SourceofApplication> GetAllPincodeMaster()
        {
            var list = _sourceRepository.GetAllSourceofApplicationMasters();
            return list;
        }

        public Mst_SourceofApplication GetSourceApplicationById(int id)
        {
            var source = _sourceRepository.GetSourceofApplicationById(id);
            return source;
        }

        public int GetMaxSourceId()
        {
            int Sourceid = _sourceRepository.GetMaxSourceId();
            return Sourceid;
        }

        public string CheckSourceNameExists(string name)
        {
            var sourcename = _sourceRepository.CheckSourceNameExists(name);
            return sourcename;
        }

        public SourceofApplicationViewModel SetDataOnEdit(Mst_SourceofApplication tblsource)
        {
            var item = _sourceRepository.SetRecordinEdit(tblsource);
            return item;
        }

        public void DeleteRecord(int id)
        {
            _sourceRepository.DeleteRecord(id);
        }

        public void SaveUpdateRecord(SourceofApplicationViewModel model)
        {
            _sourceRepository.SaveUpdateRecord(model);
        }
    }
}
