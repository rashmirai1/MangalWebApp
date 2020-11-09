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


var urlRedirect = '@Html.Raw(@Url.Action("Index", "PreSanction"))';

function OnSuccess(response) {
    $("#saveModal").modal('show');
    $(document).click(function () {
        window.location.href = urlRedirect;
    });
}

function OnFailure(response) {
    alert('Error Occured');
}