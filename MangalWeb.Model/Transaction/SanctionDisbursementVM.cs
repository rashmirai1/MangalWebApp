﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model.Transaction
{
    public class SanctionDisbursementVM
    {
        public int ID { get; set; }

        public int TransactionId { get; set; }
        public int KycId { get; set; }
        public string TransactionNumber { get; set; }
        public string LoanType { get; set; }
        [Required(ErrorMessage = "Please Select Customer")]
        public string CustomerId { get; set; }
        public string ApplicationNo { get; set; }
        public string LoanAccountNo { get; set; }

        public string EmployeeName { get; set; }
        public string CustomerName { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string PANNo { get; set; }
        public string MaritalStatus { get; set; }
        public int Age { get; set; }
        public string CustomerAddress { get; set; }
        public string NomineeName { get; set; }
        public string NomineeRelation { get; set; }
        public decimal GoldItemDetails { get; set; }
        public string SchemeName { get; set; }
        public int Tenure { get; set; }
        public decimal MaximumLoanAmount { get; set; }
        //Outstanding Details
        public decimal Principal { get; set; }
        public decimal Interest { get; set; }
        public decimal PenalInterest { get; set; }
        public decimal Charges { get; set; }
        public decimal Total { get; set; }
        public decimal EligibleLoanAmount { get; set; }
        public decimal SanctionLoanAmount { get; set; }
        //Charge Details
        public string ChargeName { get; set; }
        public decimal ChargeAmount { get; set; }
        public decimal NetPayable { get; set; }
        public string InterestRepaymentDate { get; set; }
        public string GoldItemImage { get; set; }
        public decimal PacketWeight { get; set; }
        public string LockerNo { get; set; }
        //accounting part
        public string PaymentMode { get; set; }
        public int CashAccountNo { get; set; }
        public decimal Amount { get; set; }
        public int OutwardbyNo { get; set; }
        public string VoucherEntryDate { get; set; }
        public string TransactionDate { get; set; }
        public decimal LoanAmount { get; set; }
        public string BankPaymentDate { get; set; }
        public int BankAccountNo { get; set; }
        public string CheckDD { get; set; }
        public string CheckDDstring { get; set; }
        public string CheckDDNEFTDate { get; set; }
        public string Remark { get; set; }
        public int GoldInwardByNo { get; set; }
        public string RackNo { get; set; }
        public string GoldInwardDate { get; set; }

        public int BranchId { get; set; }

        public int FinancialYearId { get; set; }
        public int CompanyId { get; set; }

        public string operation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}