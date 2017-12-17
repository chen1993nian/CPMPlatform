
function CreateHelpCatalog() {
    var html = '';
    $(".hlpContentItemTitle").each(function (index, elem) {
        var text = $(elem).text();
        var order = index + 1;
        var proStr = " data-index='" + index + "' ";
        var strLi = "<li title='" + text + "'" + proStr + ">" + order + "、" + text + "</li>";
        html = html + strLi;
    });
    var abox = document.createElement("ul");
    $(abox).html(html);
    $(".hlpCatalogList").append(abox);

    $(".hlpCatalogList li").click(function () {
        var arrHelp = $(".hlpContentItemTitle");
        var index = parseInt($(this).attr("data-index"));
        //$(arrHelp[index]).show();
        $('html, body').animate({
            scrollTop: $(arrHelp[index]).offset().top
        }, 800);
        setTimeout(function(){
            $(".hlpCatalogList li").removeClass("Active");
            $(this).addClass("Active");
        }, 1800);
    });

}

function scrollHelpCatalog() {
    var arrHelp = $(".hlpContentItemTitle");
    var helpIndex = -1;
    var arrHelpMenu = $(".hlpCatalogList li");
    var st = $(window).scrollTop();
    sth = st + $(window).height();
    var posb = 0, post = 0;
    if (0 < st) {
        //向下滚动
        for (var i = (arrHelp.length - 1) ; i >= 0; i--) {
            post = $(arrHelp[i]).offset().top;
            posb = post + $(arrHelp[i]).height();
            if ((post > st && post < sth) || (posb > st && posb < sth)) {
                $(".hlpCatalogList li").removeClass("Active");;
                $(arrHelpMenu[i]).addClass("Active");
                helpIndex = i;
                break;
            }
        }
    }
    else {
        //向上滚动
        for (var i =0  ; i <arrHelp.length; i++) {
            post = $(arrHelp[i]).offset().top;
            posb = post + $(arrHelp[i]).height();
            if ((post > st && post < sth) || (posb > st && posb < sth)) {
                $(".hlpCatalogList li").removeClass("Active");;
                $(arrHelpMenu[i]).addClass("Active");
                helpIndex = i;
                break;
            }
        }
    }
    st = st + 20;
    $(".hlpCatalogList").css("top", st);
    var mpost = $(arrHelpMenu[helpIndex]).offset().top;
    if (mpost > sth) {
        st = st - (mpost - sth) - 40;
        $(".hlpCatalogList").css("top", st);
    }
}


$(document).ready(function () {
    CreateHelpCatalog();
    $(window).bind("scroll", scrollHelpCatalog);
});
