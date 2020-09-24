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
    public class GSTService
    {
        GSTRepository _gstRepository = new GSTRepository();

        public List<GstViewModel> GetAllGSTMasters()
        {
            return _gstRepository.GetAllGSTMasters();
        }

        public List<tblaccountmaster> GSTAccountList()
        {
            return _gstRepository.FillGSTAccount();
        }

        public int GetMaxId()
        {
            return _gstRepository.GetMaxId();
        }

        public Mst_GstMaster GetGSTById(int id)
        {
            return _gstRepository.GetGSTById(id);
        }

        public GstViewModel SetDataOnEdit(Mst_GstMaster tblgst)
        {
            return _gstRepository.SetRecordinEdit(tblgst);
        }

        public void DeleteRecord(int id)
        {
            _gstRepository.DeleteRecord(id);
        }

        public void SaveUpdateRecord(GstViewModel model)
        {
            _gstRepository.SaveUpdateRecord(model);
        }

    }
}
