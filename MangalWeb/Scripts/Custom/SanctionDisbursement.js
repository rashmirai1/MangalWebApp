$(document).ready(function () {

    if ($("#ItemID").val() == 0) {
        $("#CashAccountNo").attr('disabled', 'disabled');
        $("#CashAmount").attr('disabled', 'disabled');
        $("#BankCashAccID").attr('disabled', 'disabled');
        $("#BankPaymentDate").attr('disabled', 'disabled');
        $("#CheqNEFTDD").attr('disabled', 'disabled');
        $("#CheqNEFTDDNo").attr('disabled', 'disabled');
        $("#CheqNEFTDDDate").attr('disabled', 'disabled');
        $("#BankAmount").attr('disabled', 'disabled');
    }

    $("#CashAmount").on('keyup', function () {
        var netPayable = $("#NetPayable").val();
        var CashAmount = $("#CashAmount").val();
        if (parseFloat(CashAmount) > parseFloat(netPayable)) {
            alert('Cash Amount can not be greater than Net Payable');
            $("#CashAmount").val(0);
            $("#CashAmount").focus()
            return;
        }
    });

    $("#BankAmount").on('keyup', function () {
        var netPayable = $("#NetPayable").val();
        var BankAmount = $("#BankAmount").val();
        if (parseFloat(BankAmount) > parseFloat(netPayable)) {
            alert('Bank Amount can not be greater than Net Payable');
            $("#BankAmount").val(0);
            $("#BankAmount").focus()
            return;
        }
    });

    //$("#divGoldItemDetails").hide();
    //if (Model.EligibleLoanAmountValuationDetailsVMList.Count() > 0) {
    //    $("#divGoldItemDetails").show();
    //}

    //$("#divChargeDetails").hide();
    //if (Model.ChargeDetailList.Count() > 0) {
    //    $("#divChargeDetails").show();
    //}

    $("#TransactionDate").datepicker({ dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true, maxDate: new Date() });
    $("#BankPaymentDate").datepicker({ dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true, maxDate: new Date() });
    $("#CheqNEFTDDDate").datepicker({ dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true });
    $("#GoldInwardDate").datepicker({ dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true });
});

$("#SanctionLoanAmount").on('keyup', function () {
    var sanctionamount = $("#SanctionLoanAmount").val();
    var EligibleLoanAmount = $("#EligibleLoanAmount").val();
    $("#SanctionErrorMessage").text("");
    if (parseFloat(sanctionamount) > parseFloat(EligibleLoanAmount)) {
        alert('Sanction Loan Amount can not be greater than Eligible Loan Amount');
        $("#SanctionLoanAmount").val(0);
        $("#NetPayable").val(0);
        $("#SanctionLoanAmount").focus()
        return;
    }
    if (sanctionamount == 0) {
        $("#NetPayable").val(0);
    }
    else {
        var discountamount = $("#DiscountAmount").val();
        var chargetype = $("#SchemeProcessingType").val();
        if (chargetype == "Amount") {
            $("#NetPayable").val(parseFloat(sanctionamount).toFixed(0) - parseFloat(discountamount).toFixed(0));
        }
        else {
            $("#NetPayable").val(parseFloat(sanctionamount).toFixed(0) - parseFloat(discountamount).toFixed(0));
        }
    }
});

$("#btn_save").click(function () {
    cmnShowLoader();
    SaveSanctionDisbursement();
    cmnHideLoader();
});

function SaveSanctionDisbursement() {
    debugger;
    var SanctionViewModel = {};
    var check = true;
    SanctionViewModel.TransactionId = $("#TransactionId").val();
    SanctionViewModel.CustomerId = $("#CustomerID").val();
    SanctionViewModel.KYCID = $("#KYCID").val();
    SanctionViewModel.AreaId = $("#AreaId").val();
    SanctionViewModel.MobileNo = $("#MobileNo").val();
    SanctionViewModel.TelephoneNo = $("#TelephoneNo").val();
    SanctionViewModel.EmailId = $("#EmailId").val();
    SanctionViewModel.TransactionDate = $("#TransactionDate").val();
    SanctionViewModel.LoanType = $("#LoanType").val();
    SanctionViewModel.LoanAccountNo = $("#LoanAccountNo").val();
    SanctionViewModel.CustomerName = $("#CustomerName").val();
    SanctionViewModel.PANNo = $("#PANNo").val();
    SanctionViewModel.CustomerAddress = $("#CustomerAddress").val();
    SanctionViewModel.EligibleLoanAmount = $("#EligibleLoanAmount").val();
    SanctionViewModel.SanctionLoanAmount = $("#SanctionLoanAmount").val();
    SanctionViewModel.NetPayable = $("#NetPayable").val();
    SanctionViewModel.InterestRepaymentDate = $("#InterestRepaymentDate").val();
    SanctionViewModel.PacketWeight = $("#PacketWeight").val();
    SanctionViewModel.LockerNo = $("#LockerNo").val();
    SanctionViewModel.SchemeId = $("#SchemeId").val();
    SanctionViewModel.PaymentMode = $("#PaymentMode").val();
    SanctionViewModel.BankCashAccID = $("#BankCashAccID").val();
    SanctionViewModel.BankPaymentDate = $("#BankPaymentDate").val();
    SanctionViewModel.CheqNEFTDD = $("#CheqNEFTDD").val();
    SanctionViewModel.CheqNEFTDDNo = $("#CheqNEFTDDNo").val();
    SanctionViewModel.CheqNEFTDDDate = $("#CheqNEFTDDDate").val();
    SanctionViewModel.BankAmount = $("#BankAmount").val();
    SanctionViewModel.CashAccountNo = $("#CashAccountNo").val();
    SanctionViewModel.CashAmount = $("#CashAmount").val();
    SanctionViewModel.CashOutwardbyNo = $("#CashOutwardbyNo").val();
    SanctionViewModel.GoldInwardByNo = $("#GoldInwardByNo").val();
    SanctionViewModel.RackNo = $("#RackNo").val();
    SanctionViewModel.Remark = $("#Remark").val();
    SanctionViewModel.GoldInwardDate = $("#GoldInwardDate").val();
    SanctionViewModel.StateID = $("#StateID").val();
    SanctionViewModel.SchemeProcessingType = $("#SchemeProcessingType").val();
    SanctionViewModel.SchemeProcessingCharge = $("#SchemeProcessingCharge").val();
    SanctionViewModel.SchemeProcessingLimit = $("#SchemeProcessingLimit").val();
    SanctionViewModel.CGSTAmount = $("#CGSTAmount").val();
    SanctionViewModel.SGSTAmount = $("#SGSTAmount").val();
    SanctionViewModel.CGSTAccountId = $("#CGSTAccountId").val();
    SanctionViewModel.SGSTAccountId = $("#SGSTAccountId").val();
    SanctionViewModel.CGSTTax = $("#CGSTTax").val();
    SanctionViewModel.SGSTTax = $("#SGSTTax").val();
    SanctionViewModel.PreSanctionId = $("#PreSanctionId").val();
    SanctionViewModel.Interest = $("#Interest").text();
    SanctionViewModel.PenalInterest = $("#PenalInterest").text();
    SanctionViewModel.Charges = $("#Charges").text();
    SanctionViewModel.Total = $("#Total").text();
    debugger;
    if (SanctionViewModel.PaymentMode == "Cash") {
        if (SanctionViewModel.CashAmount < SanctionViewModel.NetPayable || SanctionViewModel.NetPayable < SanctionViewModel.CashAmount) {
            alert("Cash Amount and Net Payable must be equal");
            $("#CashAmount").focus();
            check = false;
            return;
        }
    }

    if (SanctionViewModel.CustomerId == 0) {
        $("#CustomErr").text("Please Select Customer");
        $("#CustomerID").focus();
        check = false;
        return;
    }
    $("#CustomErr").text("");
    if (SanctionViewModel.SanctionLoanAmount == 0) {
        $("#SanctionErrorMessage").text("Please Enter Sanction Loan Amount");
        $("#SanctionLoanAmount").focus();
        check = false;
        return;
    }
    debugger;
    if (SanctionViewModel.PaymentMode == "") {
        $("#PaymentModeErrorMessage").text("Please Select Payment Mode");
        $("#PaymentMode").focus();
        check = false;
        return;
    }
    $("#PaymentModeErrorMessage").text("");
    if (SanctionViewModel.PaymentMode == "Chq/DD/NEFT" || SanctionViewModel.PaymentMode == "Both") {
        if (SanctionViewModel.BankAccountNo == "") {
            $("#BankAccountNo").css('border-color', 'red');
            $("#BankAccountNo").focus();
            check = false;
            return;
        }
        $("#BankAccountNo").css('border-color', '');
        if (SanctionViewModel.BankPaymentDate == "") {
            $("#BankPaymentDate").css('border-color', 'red');
            $("#BankPaymentDate").focus();
            check = false;
            return;
        }
        $("#BankPaymentDate").css('border-color', '');
        if (SanctionViewModel.ChqDDNEFT == "") {
            $("#ChqDDNEFT").css('border-color', 'red');
            $("#ChqDDNEFT").focus();
            check = false;
            return;
        }
        $("#ChqDDNEFT").css('border-color', '');
        if (SanctionViewModel.ChqDDNEFT == "Cheque") {
            if (SanctionViewModel.ChqDDNEFTNo == "") {
                $("#ChqDDNEFTNo").css('border-color', 'red');
                $("#ChqDDNEFTNo").focus();
                check = false;
                return;
            }
            $("#ChqDDNEFTNo").css('border-color', '');
            if (SanctionViewModel.CheqNEFTDDDate == "") {
                $("#CheqNEFTDDDate").css('border-color', 'red');
                $("#CheqNEFTDDDate").focus();
                check = false;
                return;
            }
            $("#CheqNEFTDDDate").css('border-color', '');
        }
        if (SanctionViewModel.BankAmount == "0.00" || SanctionViewModel.BankAmount == "0") {
            $("#BankAmount").css('border-color', 'red');
            $("#BankAmount").focus();
            check = false;
            return;
        }
        $("#BankAmount").css('border-color', '');
    }

    if (SanctionViewModel.PaymentMode == "Cash") {
        if (SanctionViewModel.CashAccountNo == "") {
            $("#CashAccountNo").css('border-color', 'red');
            $("#CashAccountNo").focus();
            check = false;
            return;
        }
        $("#CashAccountNo").css('border-color', '');
        if (SanctionViewModel.CashAmount == "0.00" || SanctionViewModel.CashAmount == "0") {
            $("#CashAmount").css('border-color', 'red');
            $("#CashAmount").focus();
            check = false;
            return;
        }
        $("#CashAmount").css('border-color', '');
        if (SanctionViewModel.CashOutwardbyNo == "") {
            $("#CashOutwardbyNo").css('border-color', 'red');
            $("#CashOutwardbyNo").focus();
            check = false;
            return;
        }
        $("#CashOutwardbyNo").css('border-color', '');
    }
    if (SanctionViewModel.GoldInwardByNo == "") {
        $("#GoldInwardByNo").css('border-color', 'red');
        $("#GoldInwardByNo").focus();
        check = false;
        return;
    }
    $("#GoldInwardByNo").css('border-color', '');
    if (SanctionViewModel.RackNo == "") {
        $("#RackNo").css('border-color', 'red');
        $("#RackNo").focus();
        check = false;
        return;
    }
    $("#RackNo").css('border-color', '');
    if (SanctionViewModel.GoldInwardDate == "") {
        $("#GoldInwardDate").css('border-color', 'red');
        $("#GoldInwardDate").focus();
        check = false;
        return;
    }
    $("#GoldInwardDate").css('border-color', '');
    //var inp = document.getElementById('ProofOfOwnerShipFile');
    //if (inp.files.length === 0) {
    //    $("#CustomErr").text("Upload Proof Of OwnerShip Image!");
    //    inp.focus();
    //    return false;
    //}

    //if ($('#tblChargeDetails tbody tr').length == 0) {
    //    var ChargeId= $("#ChargeVM_ChargeId").val();
    //    if (ChargeId == 0) {
    //        $("#CustomErr").text("Please Select Charge");
    //        $("#ChargeVM_ChargeId").focus();
    //        check=false;
    //        return;
    //    }
    //    var AccountId= $("#ChargeVM_AccountId").val();
    //    if (AccountId == 0) {
    //        $("#CustomErr").text("Please Select Account");
    //        $("#ChargeVM_AccountId").focus();
    //        check=false;
    //        return;
    //    }
    //}
    //var length=$('#tblChargeDetails tbody tr').length;
    //if(length==0)
    //{
    //    $("#CustomErr").text("Please Click Add button then save record");
    //    $("#tblChargeDetails").focus();
    //    check=false;
    //    return check;
    //}

    //$("#btn_save").prop("disabled", true);
    var lstChargeTrn = [];
    var lstGoldItemTrn = [];

    $("#tblChargeDetails TBODY TR").each(function () {
        var row = $(this);
        var rowChargeTrn = {};
        debugger;
        rowChargeTrn.ID = row.attr('id');
        rowChargeTrn.ChargeId = row.find("td").eq(1).attr('id');
        rowChargeTrn.CDetailsID = row.find("td").eq(2).attr('id');
        rowChargeTrn.Charges = row.find("TD").eq(2).html();
        rowChargeTrn.ChargeType = row.find("TD").eq(3).html();
        rowChargeTrn.GstId = row.find("td").eq(3).attr('id');
        rowChargeTrn.Amount = row.find("TD").eq(4).html();
        rowChargeTrn.AccountId = row.find("td").eq(5).attr('id');
        lstChargeTrn.push(rowChargeTrn);
    });

    $("#tblGoldItemDetails TBODY TR").each(function () {
        var row = $(this);
        var rowGoldItemTrn = {};
        debugger;
        rowGoldItemTrn.ID = row.attr('id');
        rowGoldItemTrn.OrnamentId = row.find("td").eq(1).attr('id');
        rowGoldItemTrn.Qty = row.find("TD").eq(3).html();
        rowGoldItemTrn.PurityNo = row.find("td").eq(4).attr('id');
        rowGoldItemTrn.GrossWeight = row.find("TD").eq(5).html();
        rowGoldItemTrn.Deductions = row.find("TD").eq(6).html();
        rowGoldItemTrn.NetWeight = row.find("TD").eq(7).html();
        rowGoldItemTrn.RatePerGram = row.find("TD").eq(8).html();
        rowGoldItemTrn.Value = row.find("TD").eq(9).html();
        lstGoldItemTrn.push(rowGoldItemTrn);
    });

    $("#tblGoldItemDetails tfoot TR").each(function () {
        var row = $(this);
        var rowGoldItemTrn = {};
        SanctionViewModel.TotalQuantity = row.find("TD").eq(3).text();
        SanctionViewModel.TotalGrossWeight = row.find("TD").eq(5).text();
        SanctionViewModel.TotalDeductions = row.find("TD").eq(6).text();
        SanctionViewModel.TotalNetWeight = row.find("TD").eq(7).text();
        SanctionViewModel.TotalRatePerGram = row.find("TD").eq(8).text();
        SanctionViewModel.TotalValue = row.find("TD").eq(9).text();
    });
    debugger;
    SanctionViewModel.ChargeDetailList = lstChargeTrn;
    SanctionViewModel.EligibleLoanAmountValuationDetailsVMList = lstGoldItemTrn;

    SanctionViewModel.ID = $('#ItemID').val();
    if (check == true) {
        $.ajax({
            type: "POST",
            url: "/SanctionDisbursement/Insert",
            data: JSON.stringify(SanctionViewModel),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                debugger;
                if (r == 1) {
                    $("#saveModal").modal('show');
                    $(document).click(function () {
                        window.location.href = urlRedirect;
                    });
                }
                else if (r == 2) {
                    $("#saveModal").modal('show');
                    $(document).click(function () {
                        window.location.href = urlRedirect;
                    });
                }
                else {
                    $("#CustomErr").text(r);
                    $("#btn_save").prop("disabled", false);
                    return false;
                }
            }
        });
    }
}
var urlRedirect = '/SanctionDisbursement/SanctionDisbursement';
function OnSuccess(response) {

    $("#saveModal").modal('show');
    $(document).click(function () {
        window.location.href = urlRedirect;
    });
}

function OnFailure(response) {
    alert("Error occured.");
}

function ShowEditTableList() {
    $('#divviewfilldata').empty();
    $.ajax({
        type: "POST",
        url: '/SanctionDisbursement/GetSanctionTable',
        dataType: "html",
        data: { Operation: "Edit" },
        success: function (data) {
            $('#divsanctionfilldata').empty();
            $('#divsanctionfilldata').html(data);
            $('#example').dataTable();
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}

function ShowViewTableList() {
    $('#diveditfilldata').empty();
    $.ajax({
        type: "POST",
        url: '/SanctionDisbursement/GetSanctionTable',
        dataType: "html",
        data: { Operation: "View" },
        success: function (data) {
            $('#divsanctionfilldata').empty();
            $('#divsanctionfilldata').html(data);
            $('#example').dataTable();
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}

function ShowClientTableList() {
    $('#divviewfilldata').empty();
    $.ajax({
        type: "POST",
        url: '/SanctionDisbursement/GetCustomerTable',
        dataType: "html",
        data: { Operation: "Edit" },
        success: function (data) {
            $('#diveditfilldata').empty();
            $('#diveditfilldata').html(data);
            $('#example').dataTable();
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}

$("#uploadFile").change(function () {

    var fileUpload = $("#uploadFile").get(0);
    var files = fileUpload.files;
    // Create FormData object
    fileData = new FormData();
    // Looping over all files and add it to FormData object
    for (var i = 0; i < files.length; i++) {
        fileData.append("uploadFile", files[i]);
    }
    $.ajax({
        type: 'POST',
        url: '/SanctionDisbursement/UploadProofOfOwnershipImage', // we are calling json method
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        data: fileData,
        success: function (data) {
        },
        error: function (ex) {
            alert('Failed to retrieve data.' + ex);
        }
    });
    return false;
})

//Dropdownlist Selectedchange event of payment mode
$("#PaymentMode").change(function () {
    var paymentmode = $("#PaymentMode").val();
    $("#CashAccountNo").attr('disabled', 'disabled');
    $("#CashAmount").attr('disabled', 'disabled');
    $("#BankCashAccID").attr('disabled', 'disabled');
    $("#BankPaymentDate").attr('disabled', 'disabled');
    $("#CheqNEFTDD").attr('disabled', 'disabled');
    $("#CheqNEFTDDNo").attr('disabled', 'disabled');
    $("#CheqNEFTDDDate").attr('disabled', 'disabled');
    $("#BankAmount").attr('disabled', 'disabled');

    if (paymentmode == "Chq/DD/NEFT") {
        $("#BankCashAccID").removeAttr('disabled');
        $("#BankPaymentDate").removeAttr('disabled');
        $("#CheqNEFTDD").removeAttr('disabled');
        $("#CheqNEFTDDNo").removeAttr('disabled');
        $("#CheqNEFTDDDate").removeAttr('disabled');
        $("#BankAmount").removeAttr('disabled');
    }
    else if (paymentmode == "Both") {
        $("#BankCashAccID").removeAttr('disabled');
        $("#BankPaymentDate").removeAttr('disabled');
        $("#CheqNEFTDD").removeAttr('disabled');
        $("#CheqNEFTDDNo").removeAttr('disabled');
        $("#CheqNEFTDDDate").removeAttr('disabled');
        $("#BankAmount").removeAttr('disabled');
        $("#CashAccountNo").removeAttr('disabled');
        $("#CashAmount").removeAttr('disabled');
    }
    else {
        $("#CashAccountNo").removeAttr('disabled');
        $("#CashAmount").removeAttr('disabled');
    }
});

//Dropdownlist Selectedchange event
$("#ChargeVM_ChargeId").change(function () {
    var sanctionamount = $("#SanctionLoanAmount").val();
    if (sanctionamount == 0) {
        alert('Please Enter Sanction Loan Amount')
        $("#SanctionLoanAmount").focus()
        $("#ChargeVM_ChargeId").val("");
        return;
    }
    if ($("#ChargeVM_ChargeId").val() > 0) {
        $.ajax({
            type: 'POST',
            url: '/SanctionDisbursement/GetChargeDetails', // we are calling json method
            dataType: 'json',
            data: { ChargeId: $("#ChargeVM_ChargeId").val(), SanctionLoanAmount: sanctionamount, SchemeProcessingType: $("#SchemeProcessingType").val(), SchemeProcessingCharge: $("#SchemeProcessingCharge").val() },
            //as inputto json method GetStatesfr
            success: function (data) {
                if (data.ID > 0) {
                    $("#ChargeVM_CDetailsID").val(data.CDetailsID);
                    $("#ChargeVM_Charges").val(data.Charges);
                    $("#ChargeVM_ChargeType").val(data.ChargeType);
                    $("#ChargeVM_Amount").val(data.Amount);
                }
                else {
                    alert('Can not apply charge for selected Sanction Loan Amount');
                    return;
                }
            },
            error: function (ex) {
                alert('Failed to retrieve data.' + ex);
            }
        });
        return false;
    }
    else {
        alert('Please Select Proper Charge')
        $("#ChargeVM_ChargeId").focus();
        return;
    }
})

var sr_no = 0;
var trid = 0;
currentRow = null;
$("#btnAddChargeDetails").click(function () {
    $("#CustomErrr").text("");
    $('[data-toggle="tooltip"]').tooltip();
    var actions = "<a class=\"edit\"><i class=\"fa fa-edit\" title='Edit'></i></a>&nbsp;&nbsp;&nbsp;&nbsp;<a class=\"delete\"><i class=\"fa fa-trash\" title='Delete'></i></a>"
    var index = $("#tblChargeDetails tbody tr:last").index();
    var ChargeId = $("#ChargeVM_ChargeId").val();
    var ChargeDetailsId = $("#ChargeVM_CDetailsID").val();
    var ChargeName = $("#ChargeVM_ChargeId option:selected").text();
    var Charges = $("#ChargeVM_Charges").val();
    var ChargeType = $("#ChargeVM_ChargeType").val();
    var Amount = $("#ChargeVM_Amount").val();
    var AccountId = $("#ChargeVM_AccountId").val();
    var AccountName = $("#ChargeVM_AccountId option:selected").text();

    if (ChargeId == 0) {
        alert('Please Select Charge')
        $("#ChargeVM_ChargeId").focus();
        return;
    }

    if (AccountId == 0) {
        alert('Please Select Account')
        $("#ChargeVM_AccountId").focus();
        return;
    }

    var srno = 0;
    var newtrno = trid;
    if (sr_no == 0) {
        srno = index + 2;
        newtrno = 0;
    }
    else {
        srno = sr_no;
    }
    sr_no = 0;
    trid = 0;
    var row = '<tr id=' + srno + '>' +
        '<td width="10%">' + srno + '</td>' +
        '<td width="30%" id="' + ChargeId + '">' + ChargeName + '</td>' +
        '<td width="10%" id="' + ChargeDetailsId + '">' + Charges + '</td>' +
        '<td width="10%">' + ChargeType + '</td>' +
        '<td width="10%">' + Amount + '</td>' +
        '<td width="30%" id="' + AccountId + '">' + AccountName + '</td>' +
        '<td ' + actions + '</td>' +
        '</tr>';
    var isExist = false;
    if (ChargeId != "" && AccountId != "") {
        $("#tblChargeDetails TBODY tr").each(function () {
            // get row
            var row = $(this);
            // get first and second td
            if (currentRow == null) {
                var first = row.find('td:nth-child(2)').attr('id');
                // if exists, remove the tr
                if (first == ChargeId) {
                    isExist = true;
                }
            }
        });
        if (isExist) {
            alert("The same Charge already Exist");
            return false;
        }
        else {
            if (currentRow) {
                $("#tblChargeDetails tbody").find($(currentRow)).replaceWith(row);
                debugger;
                //ID which want to compare
                var id = currentRow.attr('id');
                var first = 0;
                var amount = 0;
                $('#tblChargeDetails tbody>tr').each(function () {
                    var row1 = $(this).closest("tr");
                    if (id == $(this).attr('id')) {
                        if (first == 0) {
                            amount = row1.find("td:eq(4)").text();
                            var oldchgamt = currentRow.find("td:eq(4)").text();
                            var netpayable = parseFloat($("#NetPayable").val());
                            $("#NetPayable").val(netpayable + parseFloat(oldchgamt));
                        }
                        else {
                            var percentValue = row1.find("td:eq(2)").text();
                            var per = (amount * percentValue) / 100;
                            row1.find("td:eq(4)").text(per);
                            $("#CGSTAmount").val(per);
                            $("#SGSTAmount").val(per);
                        }
                        first = first + 1;
                    }
                });
                debugger;
                var totamt = 0;
                $('#tblChargeDetails tbody>tr').each(function () {
                    var row = $(this).closest("tr");
                    totamt = totamt + parseFloat(row.find('td:eq(4)').html());
                });
                var SanctionLoanAmount = parseFloat($("#SanctionLoanAmount").val());
                var netpayable = parseFloat($("#NetPayable").val());
                $("#NetPayable").val(parseFloat(SanctionLoanAmount) - (parseFloat(totamt)).toFixed(2));

                $("#ChargeVM_ChargeId").val("");
                $("#ChargeVM_Charges").val("");
                $("#ChargeVM_ChargeType").val("");
                $("#ChargeVM_Amount").val("");
                $("#ChargeVM_AccountId").val("");
                currentRow = null;
                $("#tblChargeDetails TBODY TR").on("click", ".edit", function () {
                    debugger;
                    var tr = $(this).closest('tr');
                    currentRow = $(this).parents('tr');
                    sr_no = tr.find('td:eq(0)').html();
                    trid = tr.attr('id'); // table row ID
                    var ChargeId = tr.find("td").eq(1).attr('id');
                    var ChargeName = tr.find('td:eq(1)').html();
                    var Charges = tr.find('td:eq(2)').html();
                    var ChargeType = tr.find('td:eq(3)').html();
                    var Amount = tr.find('td:eq(4)').html();
                    var AccountId = tr.find("td").eq(5).attr('id');
                    var AccountName = tr.find('td:eq(5)').html();

                    $("#ChargeVM_ChargeId").val(ChargeId);
                    $("#ChargeVM_Charges").val(Charges);
                    $("#ChargeVM_ChargeType").val(ChargeType);
                    $("#ChargeVM_Amount").val(Amount);
                    $("#ChargeVM_AccountId").val(AccountId);
                })
                $("#tblChargeDetails tbody tr").on("click", ".delete", function () {
                    if (confirm('Are you sure to remove?')) {
                        debugger;
                        var row1 = $(this).closest("tr");
                        $('#tblChargeDetails tbody>tr').each(function () {
                            if (row1.attr('id') == $(this).attr('id')) {
                                $(this).closest('tr').remove();
                            }
                        });
                        var totamt = 0;
                        $('#tblChargeDetails tbody>tr').each(function () {
                            var row = $(this).closest("tr");
                            totamt = totamt + parseFloat(row.find('td:eq(4)').html());
                        });
                        var SanctionLoanAmount = parseFloat($("#SanctionLoanAmount").val());
                        $("#NetPayable").val(parseFloat(SanctionLoanAmount) - (parseFloat(totamt)));
                        $("#ChargeVM_ChargeId").val("");
                        $("#ChargeVM_Charges").val("");
                        $("#ChargeVM_ChargeType").val("");
                        $("#ChargeVM_Amount").val("");
                        $("#ChargeVM_AccountId").val("");
                        return false;
                    }
                })
            }
            else {
                fileData = new FormData();
                fileData.append('Id', srno);
                fileData.append('CDetailsID', ChargeDetailsId);
                fileData.append('ChargeId', ChargeId);
                fileData.append('ChargeName', ChargeName);
                fileData.append('Charges', Charges);
                fileData.append('Amount', Amount);
                fileData.append('AccountId', AccountId);
                fileData.append('AccountName', AccountName);
                fileData.append('ChargeType', ChargeType);
                fileData.append('StateID', $("#StateID").val());
                $.ajax({
                    url: '/SanctionDisbursement/AddChargeDetails',
                    type: "POST",
                    contentType: false, // Not to set any content header
                    processData: false, // Not to process data
                    data: fileData,
                    success: function (result) {
                        debugger;
                        $.each(result.ChargeDetailList, function (key, val) {
                            if (val.CDetailsID == 0) {
                                actions = '';
                                $("#CGSTAmount").val(val.Amount);
                                $("#SGSTAmount").val(val.Amount);
                            }
                            var rowCount = $('#tblChargeDetails >tbody >tr').length + 1;
                            $('#tblChargeDetails tbody').append('<tr id=' + val.ID + '><td width="10%">' + rowCount +
                             '<td width="20%" id="' + val.ChargeId + '">' + val.ChargeName + '</td>' +
                             '<td width="10%" id="' + val.CDetailsID + '">' + val.Charges + '</td>' +
                             '<td width="10%" id="' + val.GstId + '">' + val.ChargeType + '</td>' +
                             '<td width="10%">' + val.Amount + '</td>' +
                             '<td width="10%" id="' + val.AccountId + '">' + val.AccountName + '</td>' +
                             '<td  ' + actions + '</td>' +
                             '</tr>');
                        });
                        var totamt = 0;
                        $('#tblChargeDetails tbody>tr').each(function () {
                            var row = $(this).closest("tr");
                            totamt = totamt + parseFloat(row.find('td:eq(4)').html());
                        });
                        //var gstamount=parseFloat($("#CGSTAmount").val())+parseFloat($("#SGSTAmount").val());
                        var SanctionLoanAmount = parseFloat($("#SanctionLoanAmount").val());
                        var totalamt = parseFloat(totamt.toFixed(2));
                        //Amount=parseFloat(Amount);
                        //$("#NetPayable").val(netpayable + (Amount+gstamount));
                        $("#NetPayable").val(parseFloat(SanctionLoanAmount - totalamt).toFixed(2));
                        $("#tblChargeDetails TBODY TR").on("click", ".edit", function () {
                            debugger;
                            var tr = $(this).closest('tr');
                            currentRow = $(this).parents('tr');
                            sr_no = tr.find('td:eq(0)').html();
                            trid = tr.attr('id'); // table row ID
                            var ChargeId = tr.find("td").eq(1).attr('id');
                            var ChargeName = tr.find('td:eq(1)').html();
                            var Charges = tr.find('td:eq(2)').html();
                            var ChargeType = tr.find('td:eq(3)').html();
                            var Amount = tr.find('td:eq(4)').html();
                            var AccountId = tr.find("td").eq(5).attr('id');
                            var AccountName = tr.find('td:eq(5)').html();

                            $("#ChargeVM_ChargeId").val(ChargeId);
                            $("#ChargeVM_Charges").val(Charges);
                            $("#ChargeVM_ChargeType").val(ChargeType);
                            $("#ChargeVM_Amount").val(Amount);
                            $("#ChargeVM_AccountId").val(AccountId);
                        })
                        $("#tblChargeDetails tbody tr").on("click", ".delete", function () {
                            if (confirm('Are you sure to remove?')) {
                                debugger;
                                var row = $(this).closest("tr");
                                $('#tblChargeDetails tbody>tr').each(function () {
                                    var row1 = $(this).closest("tr");
                                    if (row.attr('id') == $(this).attr('id')) {
                                        $(this).closest('tr').remove();
                                    }
                                });
                                var totamt = 0;
                                $('#tblChargeDetails tbody>tr').each(function () {
                                    var row = $(this).closest("tr");
                                    totamt = totamt + parseFloat(row.find('td:eq(4)').html());
                                });
                                var totalamt = parseFloat(totamt.toFixed(2));
                                var SanctionLoanAmount = parseFloat($("#SanctionLoanAmount").val());
                                $("#NetPayable").val(parseFloat(parseFloat(SanctionLoanAmount) - totalamt).toFixed(2));
                                $("#ChargeVM_ChargeId").val("");
                                $("#ChargeVM_Charges").val("");
                                $("#ChargeVM_ChargeType").val("");
                                $("#ChargeVM_Amount").val("");
                                $("#ChargeVM_AccountId").val("");
                                return false;
                            }
                        })
                    },
                    error: function (err) {
                        //alert(err.statusText);
                    }
                });
            }
            //get sanction loan amount
            //net payable = sanction-(charge+GST)
            debugger;
            $("#ChargeVM_ChargeId").val("");
            $("#ChargeVM_Charges").val("");
            $("#ChargeVM_ChargeType").val("");
            $("#ChargeVM_Amount").val("");
            $("#ChargeVM_AccountId").val("");
            $("#divChargeDetails").show();
        }
    }
});

$("#diveditfilldata").on("click", "tr", function () {
    debugger;
    var tr = $(this).closest('tr');
    var PresanctionId = tr.find("td").eq(1).attr('id');
    $("#editModal").hide();
    SetKycDetails(PresanctionId);
})

function SetKycDetails(PresanctionId) {
    debugger;
    $.ajax({
        type: "POST",
        url: '/SanctionDisbursement/GetKycDetails',
        data: "{Id: " + PresanctionId + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            debugger;
            $("#CustomErr").text("");
            $("#tblGoldItemDetails tbody tr").empty();
            $("#tblGoldItemDetails tfoot tr").empty();
            $("#tblChargeDetails tbody tr").empty();
            $("#divChargeDetails").show();
            $.each(result, function (key, value) {
                if (key == "AppliedDate") {
                    value = new Date(parseInt(value.replace('/Date(', '')));
                    var day = value.getDate();
                    var month = value.getMonth() + 1;
                    var year = value.getFullYear();
                    value = day + "/" + month + "/" + year;
                }
                $("#" + key + "").val(value);
            });
            debugger;
            $("#Principal").text(result.Principal);
            $("#Interest").text(result.Interest);
            $("#PenalInterest").text(result.PenalInterest);
            $("#Charges").text(result.Charges);
            $("#Total").text(result.Total);
            $("#SanctionLoanAmount").val($("#EligibleLoanAmount").val());
            $("#NetPayable").val($("#EligibleLoanAmount").val() - $("#DiscountAmount").val());
            $("#GoldItemImage").empty();

            if (result.GoldItemImage != null) {
                $("#GoldItemImage").append('<td><a href="/ValuatorOne/Download/' + result.ValuatorOneId + '" target=_blank>' + result.GoldItemImage + '</a></td>');
            }

            $.each(result.ChargeDetailList, function (key, val) {
                var rowCount = $('#tblChargeDetails >tbody >tr').length + 1;
                var value = "";
                $('#tblChargeDetails tbody').append('<tr id=' + val.ID + '><td width="10%">' + rowCount +
                 '<td width="20%" id="' + val.ChargeId + '">' + val.ChargeName + '</td>' +
                 '<td width="10%" id="' + val.CDetailsID + '">' + val.Charges + '</td>' +
                   '<td width="10%" id="' + val.GstId + '">' + val.ChargeType + '</td>' +
                   '<td width="10%">' + val.Amount + '</td>' +
                   '<td width="10%" id="' + val.AccountId + '">' + val.AccountName + '</td>' +
                   '<td id=' + val.ID + '' + + '</td>' +
                   '</tr>');
            });

            $.each(result.EligibleLoanAmountValuationDetailsVMList, function (key, val) {
                var rowCount = $('#tblGoldItemDetails >tbody >tr').length + 1;
                var value = "";
                $('#tblGoldItemDetails tbody').append('<tr id=' + val.ID + '><td width="10%">' + rowCount +
                 '<td width="20%" id="' + val.OrnamentId + '">' + val.OrnamentName + '</td>' +
                 '<td width="20%"><a href="/ValuatorTwo/DownLoadValuationImage/' + val.ID + '" target=_blank>' + val.ImageName + '</a></td>' +
                 '<td width="10%">' + val.Qty + '</td>' +
                   '<td width="10%" id="' + val.PurityNo + '">' + val.PurityName + '</td>' +
                   '<td width="10%">' + val.GrossWeight + '</td>' +
                   '<td width="10%">' + val.Deductions + '</td>' +
                   '<td width="10%">' + val.NetWeight + '</td>' +
                   '<td width="10%">' + val.RatePerGram + '</td>' +
                   '<td width="10%">' + val.Value + '</td>' +
                   '</tr>');
            });
            var foot = $("#tblGoldItemDetails").find('tfoot');
            if (!foot.length) foot = $('<tfoot>').appendTo("#tblGoldItemDetails");
            foot.append($('<tr><td><b>Total</b></td><td></td><td></td><td><b>' + result.TotalQuantity + '</b></td><td></td>' +
                                                    '<td><b>' + result.TotalGrossWeight + '</b></td>' +
                                                    '<td><b>' + result.TotalDeductions + '</b></td>' +
                                                    '<td><b>' + result.TotalNetWeight + '</b></td>' +
                                                    '<td><b>' + result.TotalRatePerGram + '</b></td>' +
                                                    '<td><b>' + result.TotalValue + '</b></td></tr>'));
        }
    });
}

$("#divsanctionfilldata").on("click", "tr", function () {
    var tr = $(this).closest('tr');
    var Id = tr.find("td").eq(0).attr('id');
    $("#editSanctionModal").hide();
    SetSanctionDisbursementDetails(Id);
})

function SetSanctionDisbursementDetails(id) {
    debugger;
    $.ajax({
        type: "POST",
        url: '/SanctionDisbursement/GetSanctionDisbursementDetails',
        data: "{Id: " + id + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            debugger;
            $("#divChargeDetails").show();
            $("#tblChargeDetails tbody tr").empty();
            $("#tblGoldItemDetails tbody tr").empty();
            $("#tblGoldItemDetails tfoot tr").empty();
            $.each(result, function (key, value) {
                $("#" + key + "").val(value);
            });
            $("#Principal").text(result.Principal);
            $("#Interest").text(result.Interest);
            $("#PenalInterest").text(result.PenalInterest);
            $("#Charges").text(result.Charges);
            $("#Total").text(result.Total);
            $("#ItemID").val(result.ID);

            var coperation = $("#operation").val();
            if (coperation == "Edit") {
                $("#btnDelete").removeClass('disabled')
            }
            else {
                $("#btnDelete").addClass('disabled')
                $("#btn_save").addClass('disabled')
            }

            $("#ProofPhoto").empty();

            if (result.ProofOfOwnerShipImageFile != null) {
                $("#ProofPhoto").append('<td><a href="/SanctionDisbursement/Download/' + result.ID + '" target=_blank>' + result.FileName + '</a></td>');
            }
            $("#GoldItemImage").empty();

            if (result.GoldItemImage != null) {
                $("#GoldItemImage").append('<td><a href="/ValuatorOne/Download/' + result.ValuatorOneId + '" target=_blank>' + result.GoldItemImage + '</a></td>');
            }

            if ($("#ItemID").val() > 0) {
                $("#CashAccountNo").removeAttr('disabled');
                $("#CashAmount").removeAttr('disabled');
                $("#BankCashAccID").removeAttr('disabled');
                $("#BankPaymentDate").removeAttr('disabled');
                $("#CheqNEFTDD").removeAttr('disabled');
                $("#CheqNEFTDDNo").removeAttr('disabled');
                $("#CheqNEFTDDDate").removeAttr('disabled');
                $("#BankAmount").removeAttr('disabled');
            }
            var actions = "<a class=\"edit\"><i class=\"fa fa-edit\" title='Edit'></i></a>&nbsp;&nbsp;&nbsp;&nbsp;<a class=\"delete\"><i class=\"fa fa-trash\" title='Delete'></i></a>"

            $.each(result.ChargeDetailList, function (key, val) {
                var rowCount = $('#tblChargeDetails >tbody >tr').length + 1;
                if (val.ChargeId == 0) {
                    actions = '';
                    $("#CGSTAmount").val(val.Amount);
                    $("#SGSTAmount").val(val.Amount);
                }
                $('#tblChargeDetails tbody').append('<tr id=' + val.ID + '><td width="10%">' + rowCount +
                   '<td width="20%" id="' + val.ChargeId + '">' + val.ChargeName + '</td>' +
                   '<td width="10%" id="' + val.CDetailsID + '">' + val.Charges + '</td>' +
                   '<td width="10%" id="' + val.GstId + '">' + val.ChargeType + '</td>' +
                   '<td width="10%">' + val.Amount + '</td>' +
                   '<td width="10%" id="' + val.AccountId + '">' + val.AccountName + '</td>' +
                   '<td ' + actions + '</td>' +
                   '</tr>');
            });

            $("#tblChargeDetails TBODY TR").on("click", ".edit", function () {
                debugger;
                var tr = $(this).closest('tr');
                currentRow = $(this).parents('tr');
                sr_no = tr.find('td:eq(0)').html();
                trid = tr.attr('id'); // table row ID
                var ChargeId = tr.find("td").eq(1).attr('id');
                var ChargeName = tr.find('td:eq(1)').html();
                var Charges = tr.find('td:eq(2)').html();
                var ChargeType = tr.find('td:eq(3)').html();
                var Amount = tr.find('td:eq(4)').html();
                var AccountId = tr.find("td").eq(5).attr('id');
                var AccountName = tr.find('td:eq(5)').html();

                $("#ChargeVM_ChargeId").val(ChargeId);
                $("#ChargeVM_Charges").val(Charges);
                $("#ChargeVM_ChargeType").val(ChargeType);
                $("#ChargeVM_Amount").val(Amount);
                $("#ChargeVM_AccountId").val(AccountId);
            })

            $("#tblChargeDetails tbody tr").on("click", ".delete", function () {
                if (confirm('Are you sure to remove?')) {
                    debugger;
                    var row = $(this).closest("tr");
                    $('#tblChargeDetails tbody>tr').each(function () {
                        //var row1 = $(this).closest("tr");
                        if (row.attr('id') == $(this).attr('id')) {
                            $(this).closest('tr').remove();
                        }
                    });

                    var totamt = 0;
                    $('#tblChargeDetails tbody>tr').each(function () {
                        var row = $(this).closest("tr");
                        totamt = totamt + parseFloat(row.find('td:eq(4)').html());
                    });
                    var SanctionLoanAmount = parseFloat($("#SanctionLoanAmount").val());
                    var netpayable = parseFloat($("#NetPayable").val());
                    $("#NetPayable").val(parseFloat(SanctionLoanAmount) - (parseFloat(totamt)));
                    $("#ChargeVM_ChargeId").val("");
                    $("#ChargeVM_Charges").val("");
                    $("#ChargeVM_ChargeType").val("");
                    $("#ChargeVM_Amount").val("");
                    $("#ChargeVM_AccountId").val("");
                    return false;
                }
            })

            $.each(result.EligibleLoanAmountValuationDetailsVMList, function (key, val) {
                var rowCount = $('#tblGoldItemDetails >tbody >tr').length + 1;
                var value = "";
                $('#tblGoldItemDetails tbody').append('<tr id=' + val.ID + '><td width="10%">' + rowCount +
                  '<td width="20%" id="' + val.OrnamentId + '">' + val.OrnamentName + '</td>' +
                  '<td width="20%"><a href="/ValuatorTwo/DownLoadValuationImage/' + val.ID + '" target=_blank>' + val.ImageName + '</a></td>' +
                  '<td width="10%">' + val.Qty + '</td>' +
                    '<td width="10%" id="' + val.PurityNo + '">' + val.PurityName + '</td>' +
                    '<td width="10%">' + val.GrossWeight + '</td>' +
                    '<td width="10%">' + val.Deductions + '</td>' +
                    '<td width="10%">' + val.NetWeight + '</td>' +
                    '<td width="10%">' + val.RatePerGram + '</td>' +
                    '<td width="10%">' + val.Value + '</td>' +
                    '</tr>');
            });
            var foot = $("#tblGoldItemDetails").find('tfoot');
            if (!foot.length) foot = $('<tfoot>').appendTo("#tblGoldItemDetails");
            foot.append($('<tr><td><b>Total</b></td><td></td><td></td><td><b>' + result.TotalQuantity + '</b></td><td></td>' +
                                                    '<td><b>' + result.TotalGrossWeight + '</b></td>' +
                                                    '<td><b>' + result.TotalDeductions + '</b></td>' +
                                                    '<td><b>' + result.TotalNetWeight + '</b></td>' +
                                                    '<td><b>' + result.TotalRatePerGram + '</b></td>' +
                                                    '<td><b>' + result.TotalValue + '</b></td></tr>'));
        }
    });
}

$(function () {
    var url = '/SanctionDisbursement/Delete';

    $("#btnDelete").click(function () {
        $("#deleteModal").modal('show');
    });
    $("#btnDeleteConfirm").click(function () {
        $.ajax({
            type: "POST",
            url: url,
            data: '{id: "' + $("#ItemID").val() + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    $("#CustomErr").text(data);
                    return false;
                }
                else {
                    $("#CustomErr").empty();
                    $("#DeleteSuccessModal").modal('show');

                    $(document).click(function () {
                        window.location.href = urlRedirect;
                    });
                }
            },
            error: function (xhr, status, error) {
                alert(error);
            }
        });
    });
});
