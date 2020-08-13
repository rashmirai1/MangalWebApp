﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Transaction
{
    public class RequestFormViewModel
    {
        public RequestFormViewModel()
        {
            DocumentUploadList = new List<DocumentUploadDetailsVM>();
        }
        public int TransactionId { get; set; }
        public int SanctionId { get; set; }
        public int KYCID { get; set; }
        public string AdhaarNo { get; set; }
        public string ApplicationNo { get; set; }
        public string LoanAccountNo { get; set; }
        public string CustomerID { get; set; }
        public Nullable<System.DateTime> DisburseDate { get; set; }
        public Nullable<System.DateTime> KYCDate { get; set; }
        public string MobileNo { get; set; }
        public string TelephoneNo { get; set; }
        public string EmailID { get; set; }
        public string BldgHouseName { get; set; }
        public string Road { get; set; }
        public string BldgPlotNo { get; set; }
        public string RoomBlockNo { get; set; }
        public Nullable<int> StateID { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<int> AreaID { get; set; }
        public Nullable<int> ZoneID { get; set; }
        public string Landmark { get; set; }
        public string OfficeAddress { get; set; }
        public string PinCode { get; set; }
        public string Distance { get; set; }
        public Nullable<int> FYID { get; set; }
        public Nullable<int> CmpID { get; set; }
        public Nullable<int> BranchID { get; set; }

        public string Area { get; set; }
        public DocumentUploadDetailsVM DocumentUploadVM { get; set; }
        public List<DocumentUploadDetailsVM> DocumentUploadList { get; set; }
    }
}