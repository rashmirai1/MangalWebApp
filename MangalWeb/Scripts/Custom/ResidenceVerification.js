function StartValidation() {

    var length = $('#tblDocumentDetails tbody tr').length;
    if (length == 0) {
        bootbox.alert("Please Add Document Details");
        $("#tblDocumentDetails").focus();
        return false;
    }
}

$(document).ready(function () {
    $('input[type=datetime]').datepicker({
        dateFormat: "dd/mm/yy",
        changeMonth: true,
        changeYear: true,
        yearRange: "-60:+0"
    });
    $('#TimeofVisit').timepicker(
        {
            template: 'dropdown',
            minuteStep: 1,
            secondStep: 1,
            showMeridian: true
        });
    $("#PinCode").on('change', function () {
        var pin = $(this).val();
        FillAddressByPinCode(pin);
    });
    $("#UserId").on('change', function () {
        var user = $(this).val();
        FillEmployeeDetailsById(user);
    });
});


$("#tblDocumentDetails TBODY TR").on("click", ".delete", function () {
    if (confirm('Are you sure to remove?')) {
        var tr = $(this).closest('tr');
        trid = tr.find('td:eq(0)').html();
        tr.remove();
        $.ajax({
            url: '/ResidenceVerification/Remove?id=' + trid,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                //$(this).parents('tr').remove();
            },
            error: function (err) {
                alert(err.statusText);
            }
        });
    }
})

var sr_no = 0;
var trid = 0;
currentRow = null;

$("#btnAddDocumentDetails").click(function () {
    $("#CustomErrr").text("");
    $('[data-toggle="tooltip"]').tooltip();
    //var actions = "<a class=\"edit\"><i class=\"fa fa-edit\" title='Edit'></i></a>&nbsp;&nbsp;&nbsp;&nbsp;<a class=\"delete\"><i class=\"fa fa-trash\" title='Delete'></i></a>"
    var actions = "<a class=\"delete\"><i class=\"fa fa-trash\" title='Delete'></i></a>"
    var index = $("#tblDocumentDetails tbody tr:last").index();
    debugger;
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

    var DocumentTypeId = $("#DocumentUploadVM_DocumentTypeId").val();
    var DocumentTypeName = $("#DocumentUploadVM_DocumentTypeId option:selected").text();
    var DocumentId = $("#DocumentUploadVM_DocumentId").val();
    var DocumentName = $("#DocumentUploadVM_DocumentId option:selected").text();
    var ExpiryDate = $("#DocumentUploadVM_ExpiryDate").val();
    //var filename=$("#UploadedFile").val();
    var filename = $("#targetImg").val();
    var SpecifyOther = $("#DocumentUploadVM_SpecifyOther").val();
    var NameonDocument = $("#DocumentUploadVM_NameonDocument").val();
    debugger;

    var isExist = false;
    $("#tblDocumentDetails TBODY tr").each(function () {
        // get row
        debugger;
        var row = $(this);
        // get first and second td
        var first = row.find('td:nth-child(2)').attr('id');
        var second = row.find('td:nth-child(3)').attr('id');
        // if exists, remove the tr
        if (first == DocumentTypeId && second == DocumentId) {
            isExist = true;
        }
    });
    if (isExist) {
        alert("The same Document Type and Document Name already Exist");
        return false;
    }
    else {
        if (currentRow) {
            $("#tblDocumentDetails tbody").find($(currentRow)).replaceWith(row);
            $("#DocumentUploadVM_DocumentTypeId").val("");
            $("#DocumentUploadVM_DocumentId").val("");
            $("#DocumentUploadVM_SpecifyOther").val("");
            $("#DocumentUploadVM_NameonDocument").val("");
            $("#DocumentUploadVM_ExpiryDate").val("");
            $("#UploadedFile").val("");
            currentRow = null;
        }
        else {
            if (DocumentTypeId != "" && DocumentId != "") {

                var fileUpload = $("#UploadedFile").get(0);
                var files = fileUpload.files;
                // Create FormData object
                fileData = new FormData();
                // Looping over all files and add it to FormData object
                for (var i = 0; i < files.length; i++) {
                    fileData.append("UploadedFile", files[i]);
                }
                var row = '<tr id=' + newtrno + '>' +
              '<td width="10%">' + srno + '</td>' +
              '<td width="10%" id="' + DocumentTypeId + '">' + DocumentTypeName + '</td>' +
              '<td width="10%" id="' + DocumentId + '">' + DocumentName + '</td>' +
              '<td width="20%">' + SpecifyOther + '</td>' +
              '<td width="20%">' + NameonDocument + '</td>' +
              '<td width="20%">' + ExpiryDate + '</td>' +
              '<td width="20%"><a href="#">' + files[0].name + '</a></td>"' +
              '<td id=' + srno + '' + actions + '</td>' +
              '</tr>';
                $("#tblDocumentDetails tbody").append(row);

                // Adding one more key to FormData object
                fileData.append('Id', srno);
                fileData.append('DocumentTypeId', DocumentTypeId);
                fileData.append('DocumentId', DocumentId);
                fileData.append('ExpiryDate', ExpiryDate);
                fileData.append('SpecifyOther', SpecifyOther);
                fileData.append('NameonDocument', NameonDocument);
                $.ajax({
                    url: '/ResidenceVerification/AddDocument',
                    type: "POST",
                    contentType: false, // Not to set any content header
                    processData: false, // Not to process data
                    data: fileData,
                    success: function (result) {
                    },
                    error: function (err) {
                        //alert(err.statusText);
                    }
                });

                $("#DocumentUploadVM_DocumentTypeId").val("");
                $("#DocumentUploadVM_DocumentId").val("");
                $("#DocumentUploadVM_ExpiryDate").val("");
                $("#UploadedFile").val("");
                $("#DocumentUploadVM_SpecifyOther").val("");
                $("#DocumentUploadVM_NameonDocument").val("");
                $("#divDocumentDetails").show();
            }
        }
    }

    $("#tblDocumentDetails TBODY TR").on("click", ".delete", function () {
        if (confirm('Are you sure to remove?')) {
            var tr = $(this).closest('tr');
            trid = tr.find('td:eq(0)').html();
            tr.remove();
            $.ajax({
                url: '/ResidenceVerification/Remove?id=' + trid,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    //$(this).parents('tr').remove();
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        }
    })
})

$("#DocumentUploadVM_DocumentTypeId").change(function () {
    $("#DocumentUploadVM_DocumentId").empty();
    $.ajax({
        type: 'POST',
        url: '/DocumentUpload/GetDcoument',
        dataType: 'json',
        data: { id: $("#DocumentUploadVM_DocumentTypeId").val() },
        // here we are get value of selected country and passing same value
        //as inputto json method GetStates.
        success: function (data) {
            var doc = "<select id='DocumentUploadVM_DocumentId'>";
            doc = doc + '<option value="">--Select Document--</option>';
            for (var i = 0; i < data.length; i++) {
                doc = doc + '<option value=' + data[i].Value + '>' + data[i].Text + '</option>';
            }
            doc = doc + '</select>';
            $('#DocumentUploadVM_DocumentId').html(doc);
            $("#DocumentUploadVM_DocumentId").removeAttr('disabled')
        },
        error: function (ex) {
            alert('Failed to retrieve data.' + ex);
        }
    });
    return false;
})

$("#DocumentUploadVM_DocumentId").change(function () {
    $.ajax({
        type: 'POST',
        url: '/DocumentUpload/GetExpiryDate',
        dataType: 'json',
        data: { id: $("#DocumentUploadVM_DocumentId").val() },
        // here we are get value of selected country and passing same value
        //as inputto json method GetStates.
        success: function (data) {
            debugger;
            $("#DocumentUploadVM_ExpiryDate").attr('disabled', 'disabled')
            if (data == true) {
                $("#DocumentUploadVM_ExpiryDate").removeAttr('disabled')
            }
        },
        error: function (ex) {
            alert('Failed to retrieve data.' + ex);
        }
    });
    return false;
})


function FillEmployeeDetailsById(id) {
    $.ajax({
        type: "POST",
        url: '/ResidenceVerification/FillEmployeeDetailsById',
        data: '{id: "' + id + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $("#Designation").val(result.UserName);
            $("#EmployeeCode").val(result.EmployeeCode);
        },
        error: function (xhr, status, error) {
            alert("something went wrong!");
        }
    });
}
function FillAddressByPinCode(pin) {
    $.ajax({
        type: "POST",
        url: '/KYC/FillAddressByPinCode',
        data: '{id: "' + pin + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $("#city").val(result.CityName);
            $("#state").val(result.StateName);
            $("#area").val(result.AreaName);
            $("#zone").val(result.ZoneName);
            $("#StateID").val(result.StateID);
            $("#CityID").val(result.CityId);
            $("#Area").val(result.AreaName);
            $("#ZoneID").val(result.ZoneID);

        },
        error: function (xhr, status, error) {
            alert("something went wrong!");
        }
    });
}

function getDocumentURL() {
    var kycid = $("#kycid").val();
    $.ajax({
        type: "POST",
        url: '/ResidenceVerification/GetDocumentID',
        data: '{id: "' + kycid + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result != 0) {
                window.open("/DocumentUpload/GetDocUploadDetailsById?ID=" + result + "", '_blank');
            }
            else {
                window.open("/DocumentUpload/GetCustomerById?KycId=" + kycid + "", '_blank');
            }

        },
        error: function (xhr, status, error) {
            alert("Please select customer!");
        }
    });
}

function ShowClientTableList() {
    $('#divviewfilldata').empty();
    $.ajax({
        type: "POST",
        url: '/ResidenceVerification/GetCustomerDetails',
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

var urlRedirect = '/ResidenceVerification/Index';

function OnBegin(e) {
    cmnShowLoader();
}

function OnSuccess(response) {
    bootbox.alert('Record saved successfully')
    $(document).click(function () {
        window.location.href = urlRedirect;
    });
}

function OnFailure(response) {
    cmnHideLoader();
    bootbox.alert('Error Occured');
}

