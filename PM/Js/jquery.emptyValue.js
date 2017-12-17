/**
* emptyValue plugin 默认关键字效果
* @version 1.3
* @authod 雨打浮萍
* Copyright (c) 2012 雨打浮萍 (http://www.rainleaves.com/)
* For more docs and examples visit:
* http://www.rainleaves.com/html/1357.html
*/
(function ($) {
    $.fn.val2 = $.fn.val;
    $.fn.emptyValue = function (arg) {
        this.each(function () {
            var input = $(this);
            var options = arg;
            if (typeof options == "string") {
                options = { empty: options }
            }
            options = jQuery.extend({
                empty: input.attr("data-empty") || "",
                className: "gray"
            }, options);
            input.attr("data-empty", options.empty);
            return input.focus(function () {
                $(this).removeClass(options.className);
                if ($(this).val2() == options.empty) {
                    $(this).val2("");
                }
            }).blur(function () {
                if ($(this).val2() == "") {
                    $(this).val2(options.empty);
                }
                $(this).addClass(options.className);
            }).blur();
        });
    };
    //重写jquery val方法，增加data-empty过滤
    $.fn.val = function () {
        var value = $(this).val2.apply(this, arguments);
        var empty = $(this).attr("data-empty");
        if (typeof empty != "undefined" && empty == value) {
            value = "";
        }
        return value;
    };
})(jQuery);