var _inputBg = {};
jQuery(":text,.TextBoxInArea").live("focus", function () {
    _inputBg = { "backgroundColor": $(this).css("background-color"), "backgroundImage": $(this).css("background-image") };
    $(this).css({ "backgroundColor": "#fff280", "backgroundImage": "url()" });
});

jQuery(":text,.TextBoxInArea").live("blur", function () {
    $(this).css(_inputBg);
});