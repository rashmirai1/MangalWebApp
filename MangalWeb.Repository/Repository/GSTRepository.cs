using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Repository.Repository
{
    public class GSTRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public List<Mst_GstMaster> GetAllGSTMasters()
        {
            var list = _context.Mst_GstMaster.ToList();
            return list;
        }

        public Mst_GstMaster GetGSTById(int id)
        {
            var gst = _context.Mst_GstMaster.Where(x => x.Gst_RefId == id).FirstOrDefault();
            return gst;
        }

        public int GetMaxId()
        {
            var id = _context.Mst_GstMaster.Any() ? _context.Mst_GstMaster.Max(m => m.Gst_RefId) + 1 : 1;
            return id;
        }

        public void DeleteRecord(int id)
        {
            var deleterecord = _context.Mst_GstMaster.Where(x => x.Gst_RefId == id).FirstOrDefault();
            if (deleterecord != null)
            {
                _context.Mst_GstMaster.Remove(deleterecord);
                _context.SaveChanges();
            }
        }

        public void SaveUpdateRecord(GstViewModel gstvm)
        {
            Mst_GstMaster tblGst = new Mst_GstMaster();
            if (gstvm.EditID <= 0)
            {
                gstvm.ID = _context.Mst_GstMaster.Any() ? _context.Mst_GstMaster.Max(m => m.Gst_RefId) + 1 : 1;
                tblGst.Gst_RefId = gstvm.ID;
                tblGst.Gst_RecordCreated = DateTime.Now;
                tblGst.Gst_RecordCreatedBy = gstvm.CreatedBy;
                _context.Mst_GstMaster.Add(tblGst);
            }
            else
            {
                tblGst = _context.Mst_GstMaster.Where(x => x.Gst_RefId == gstvm.ID).FirstOrDefault();
            }
            tblGst.Gst_EffectiveFrom = gstvm.EffectiveFrom;
            tblGst.Gst_CGST = gstvm.CGST;
            tblGst.Gst_SGST = gstvm.SGST;
            tblGst.Gst_IGST = gstvm.IGST;
            tblGst.Gst_RecordUpdated = DateTime.Now;
            tblGst.Gst_RecordUpdatedBy = gstvm.UpdatedBy;
            _context.SaveChanges();
        }

        public GstViewModel SetRecordinEdit(Mst_GstMaster tblGst)
        {
            GstViewModel gstvm = new GstViewModel();
            gstvm.ID = tblGst.Gst_RefId;
            gstvm.EditID = tblGst.Gst_RefId;
            gstvm.EffectiveFrom = tblGst.Gst_EffectiveFrom;
            gstvm.CGST = tblGst.Gst_CGST;
            gstvm.SGST = tblGst.Gst_SGST;
            gstvm.IGST = tblGst.Gst_IGST;
            return gstvm;
        }

    }
}
