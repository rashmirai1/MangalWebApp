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

        public List<GstViewModel> GetAllGSTMasters()
        {
            List<GstViewModel> list = new List<GstViewModel>();
            var tbllist=_context.Mst_GstMaster.ToList();
            foreach (var item in tbllist)
            {
                GstViewModel gstvm = new GstViewModel();
                gstvm.ID = item.Gst_RefId;
                gstvm.EditID = item.Gst_RefId;
                gstvm.EffectiveFrom = item.Gst_EffectiveFrom.ToShortDateString();
                gstvm.CGST = item.Gst_CGST;
                gstvm.SGST = item.Gst_SGST;
                gstvm.IGST = item.Gst_IGST;
                gstvm.AccountNo = item.Gst_AccountId;
                gstvm.AccountName = _context.tblaccountmasters.Where(x => x.AccountID == item.Gst_AccountId).Select(x => x.Name).FirstOrDefault();
                list.Add(gstvm);
            }
            return list;
        }

        #region FillGSTAccount
        public List<tblaccountmaster> FillGSTAccount()
        {
            return _context.tblaccountmasters.Where(x => x.GPID == 20 || x.GPID==21 || x.GPID==61 || x.GPID==100).OrderBy(x => x.Name).ToList();
        }
        #endregion

        public Mst_GstMaster GetGSTById(int id)
        {
            return _context.Mst_GstMaster.Where(x => x.Gst_RefId == id).FirstOrDefault();
        }

        public int GetMaxId()
        {
            return _context.Mst_GstMaster.Any() ? _context.Mst_GstMaster.Max(m => m.Gst_RefId) + 1 : 1;
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
            tblGst.Gst_EffectiveFrom =Convert.ToDateTime(gstvm.EffectiveFrom);
            tblGst.Gst_CGST = gstvm.CGST;
            tblGst.Gst_SGST = gstvm.SGST;
            tblGst.Gst_IGST = gstvm.IGST;
            tblGst.Gst_AccountId = gstvm.AccountNo;
            tblGst.Gst_RecordUpdated = DateTime.Now;
            tblGst.Gst_RecordUpdatedBy = gstvm.UpdatedBy;
            _context.SaveChanges();
        }

        public GstViewModel SetRecordinEdit(Mst_GstMaster tblGst)
        {
            GstViewModel gstvm = new GstViewModel();
            gstvm.ID = tblGst.Gst_RefId;
            gstvm.EditID = tblGst.Gst_RefId;
            gstvm.EffectiveFrom = tblGst.Gst_EffectiveFrom.ToShortDateString();
            gstvm.CGST = tblGst.Gst_CGST;
            gstvm.SGST = tblGst.Gst_SGST;
            gstvm.IGST = tblGst.Gst_IGST;
            gstvm.AccountNo = tblGst.Gst_AccountId;
            return gstvm;
        }
    }
}
