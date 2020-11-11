using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MangalWeb.Model.Transaction
{
    public class GLReceiptViewModel
    {
        public int glreceiptid { get; set; }
        public string KycType { get; set; }
        public DateTime? receiptDate { get; set; }
        public string transactionid { get; set; }
        public bool penaltybol { get; set; }
        
    }
}