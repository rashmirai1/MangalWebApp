﻿function cmnShowLoader() {

    $(".bd-loader-modal-lg").modal('show')
    $(".modal").unbind("click")
}
function cmnHideLoader() {
    $(".bd-loader-modal-lg").modal('hide')
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