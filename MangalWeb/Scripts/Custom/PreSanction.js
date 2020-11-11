$(document).ready(function () {
    //$('#AppliedDate').datepicker({
    //    dateFormat: "dd/mm/yy",
    //    changeMonth: true,
    //    changeYear: true,
    //    yearRange: "-60:+0"
    //});

    $("#SchemeID").on('change', function () {
        var scheme = $(this).val();
        FillSchemeDetailsById(scheme);
    });
});

function FillSchemeDetailsById(scheme) {
    var url ="/PreSanction/FillSchemeDetailsById"

    $.ajax({
        type: "POST",
        url: url,
        data: '{id: "' + scheme + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $("#ReqLoanAmount").val(result.MaxLoanAmt);
            $("#Tenure").val(result.Tenure);
            $("#ROI").val(result.ROI);
            $("#LTV").val(result.Ltv);
        },
        error: function (xhr, status, error) {
            alert("something went wrong!");
        }
    });
}
function ShowClientTableList() {
    $('#divviewfilldata').empty();
    var url = '/PreSanction/GetCustomerDetails';
    $.ajax({
        type: "POST",
        //url: '@Url.Action("GetCustomerDetails", "PreSanction")',
        url: url,
        success: function (data) {
            $('#diveditfilldata').empty();
            $('#diveditfilldata').html(data);
            $('#CustomerDetailsTable').dataTable();
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}


var urlRedirect = '/PreSanction/Index';
function OnBegin(e) {
    cmnShowLoader();
}
function OnSuccess(response) {
    cmnHideLoader();
    if (response.PreSanctionID > 0) {
        bootbox.alert("Record saved successfully", function () {
            window.location.href = urlRedirect;
        });
    }
    
}

function OnFailure(err) {
    cmnHideLoader();
    var message = "Error occured.<br/>";

    var errors = null;
    try {
        errors = err.responseJSON;
    }
    catch (ex) {
    }

    if (errors.PreSanctionError) {
        message += errors.PreSanctionError + "<br/>";
    }

    if (errors.NoRecordFound) {
        message += errors.NoRecordFound + "<br/>";
    }

    if (errors.InUse) {
        message += errors.InUse + "<br/>";
    }

    bootbox.alert(message);
}

function GetPreSanctionList() {
    $('#divviewfilldata').empty();
    var url = "/PreSanction/GetPreSanctions";
    $.ajax({
        type: "POST",
        url: url,
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
function DeletePreSanction(Id) {

    cmnConfirmation("Are you sure! You want to delete this record?", function (result) {
        if (result) {
            var url = "/PreSanction/DeletePreSanction?Id=" + Id;

            $.ajax({
                type: "POST",
                url: url,
                //data: '{id: "' + scheme + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result) {
                        window.location.href = urlRedirect;
                    }
                },
                error: function (xhr, status, error) {
                    bootbox.alert("Problem with deleting record.");
                }
            });
        }
    }
    );

}

function parseJsonDate(jsonDateString) {
    var date = new Date(parseInt(jsonDateString.replace('/Date(', '')));
    return date.getDate() + "/" + date.getMonth() + "/" + date.getFullYear();
}