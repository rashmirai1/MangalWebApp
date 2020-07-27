//$(function () {
//    $.validator.methods.date = function (value, element) {
//        if ($.browser.webkit) {
//            var d = new Date();
//            return this.optional(element) || !/Invalid|NaN/.test(new Date(d.toLocaleDateString(value)));
//        }
//        else {
//            return this.optional(element) || !/Invalid|NaN/.test(new Date(value));
//        }
//    };
//});

$(function () {
    $.validator.addMethod('date',
    function (value, element) {
        if (this.optional(element)) {
            return true;
        }
        var ok = true;
        try {
            $.datepicker.parseDate('dd/mm/yy', value);
        }
        catch (err) {
            ok = false;
        }
        return ok;
    });
    $(".datefield").datepicker({ dateFormat: 'dd/mm/yy', changeYear: true });
});