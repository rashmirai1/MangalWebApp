function cmnShowLoader() {

    $("#loaderModal").modal('show')
    $(".modal").unbind("click")
}
function cmnHideLoader() {
    $("#loaderModal").modal('hide');
    $("#loaderModal").removeClass("in");
    $(".modal-backdrop").remove();
    $("#loaderModal").hide();
}

function cmnConfirmation(msg, callbackResult) {
    bootbox.confirm({
        message: msg,
        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-success'
            },
            cancel: {
                label: 'No',
                className: 'btn-danger'
            }
        },
        callback: callbackResult,
    });
}

function getExtension(path) {
    var basename = path.split(/[\\/]/).pop(),  // extract file name from full path ...
        // (supports `\\` and `/` separators)
        pos = basename.lastIndexOf(".");       // get last position of `.`

    if (basename === "" || pos < 1)            // if file name is empty or ...
        return "";                             //  `.` not found (-1) or comes first (0)

    return "." + basename.slice(pos + 1);            // extract extension ignoring `.`
}

function UpdateMessageAction(messageActionUserId) {

    var url = "/MessageAction/UpdateMessageAction?messageActionUserId=" + messageActionUserId;

    $.ajax({
        type: "POST",
        url: url,
        //data: '{id: "' + scheme + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
           
        },
        error: function (xhr, status, error) {
            //alert("something went wrong!");
        }
    });
}