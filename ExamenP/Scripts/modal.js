$(document).ready(function () {

    var oldJqTrigger = jQuery.fn.trigger;
    jQuery.fn.trigger = function () {
        if (arguments && arguments.length > 0) {
            if (typeof arguments[0] == "object") {
                if (typeof arguments[0].type == "string") {
                    if (arguments[0].type == "show.bs.modal") {
                        var ret = oldJqTrigger.apply(this, arguments);
                        if ($('.modal:visible').length) {
                            $('.modal-backdrop.in').first().css('z-index', parseInt($('.modal:visible').last().css('z-index')) + 10);
                            $(this).css('z-index', parseInt($('.modal-backdrop.in').first().css('z-index')) + 10);
                        }
                        return ret;
                    }
                }
            }
            else if (typeof arguments[0] == "string") {
                if (arguments[0] == "hidden.bs.modal") {
                    if ($('.modal:visible').length) {
                        $('.modal-backdrop').first().css('z-index', parseInt($('.modal:visible').last().css('z-index')) - 10);
                        $('body').addClass('modal-open');
                    }
                }
            }
        }
        return oldJqTrigger.apply(this, arguments);
    };
})