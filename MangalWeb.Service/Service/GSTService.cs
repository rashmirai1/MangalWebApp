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

        public List<Mst_GstMaster> GetAllGSTMasters()
        {
            var list = _gstRepository.GetAllGSTMasters();
            return list;
        }

        public int GetMaxId()
        {
            var id = _gstRepository.GetMaxId();
            return id;
        }

        public Mst_GstMaster GetGSTById(int id)
        {
            var gst = _gstRepository.GetGSTById(id);
            return gst;
        }

        public GstViewModel SetDataOnEdit(Mst_GstMaster tblgst)
        {
            var item = _gstRepository.SetRecordinEdit(tblgst);
            return item;
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
