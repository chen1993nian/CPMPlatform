
var _objAppHelp = new Object();
_objAppHelp.app_showHelp = (function (urlStr, titleStr) {
    if ((titleStr == null) || (titleStr == undefined) || (titleStr == "")) titleStr = "帮助";
    var win = new $.dialog({
        id: 'HelpWin'
        , cover: true
        , maxBtn: true
        , minBtn: true
        , btnBar: true
        , lockScroll: false
        , title: titleStr
        , autoSize: false
        , width: 1150
        , height: 600
        , resize: true
        , bgcolor: 'black'
        , iconTitle: false
        , page: urlStr
    });
    win.ShowDialog();
});
_objAppHelp.boundClickEvent = (function () {
    $(".winHelpLinkBtn").click(function () {
        var url = $(this).attr("data-hlpUrl");
        var title = $(this).attr("data-hlpTitle");
        _objAppHelp.app_showHelp(url, title);
    });
});

$(document).ready(function () {
    _objAppHelp.boundClickEvent();
});
