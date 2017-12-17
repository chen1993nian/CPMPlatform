/// <reference path="JSintellisense/jquery-1.2.6-intellisense.js" />
(function($) {
    $.fn.noSelect = function(p) { //no select plugin by me :-)
        if (p == null)
            prevent = true;
        else
            prevent = p;

        if (prevent) {

            return this.each(function() {
                if ($.browser.msie || $.browser.safari) $(this).bind('selectstart', function() { return false; });
                else if ($.browser.mozilla) {
                    $(this).css('MozUserSelect', 'none');
                    $('body').trigger('focus');
                }
                else if ($.browser.opera) $(this).bind('mousedown', function() { return false; });
                else $(this).attr('unselectable', 'on');
            });

        } else {


            return this.each(function() {
                if ($.browser.msie || $.browser.safari) $(this).unbind('selectstart');
                else if ($.browser.mozilla) $(this).css('MozUserSelect', 'inherit');
                else if ($.browser.opera) $(this).unbind('mousedown');
                else $(this).removeAttr('unselectable', 'on');
            });

        }

    }; //end noSelect
})(jQuery);

//全局变量定义
var CANVAS_WIDTH = 284+2; //画布的宽
var CANVAS_HEIGHT = 266+2; //画布的高
var ICON_WIDTH = 120;
var ICON_HEIGHT = 120
var LEFT_EDGE = (CANVAS_WIDTH - ICON_WIDTH) / 2; //72
var TOP_EDGE = (CANVAS_HEIGHT - ICON_HEIGHT) / 2; //83

var RIGHT_EDGE = LEFT_EDGE + ICON_WIDTH;
var BOTTOM_EDGE = TOP_EDGE + ICON_HEIGHT;

var CANVAS_LEFT_MARGIN = 4;

var CROP_LEFT = LEFT_EDGE;
var CROP_TOP = -BOTTOM_EDGE;

//用document. ready事件中在上传后第一次转向时无法获取到图片的打开，应该是没有被下载来的缘故
$(window).load(function() {

    $("#Crop").noSelect();
    var $iconElement = $("#ImageIcon");
    //    var $imagedrag = $("#ImageDrag");

    //获取上传图片的实际高度，宽度
    var image = new Image();
    image.src = $iconElement.attr("src");
    var realWidth = image.width;
    var realHeight = image.height;
    image = null;

    //计算缩放比例开始
    var minFactor = Math.max(ICON_WIDTH / realWidth, ICON_HEIGHT / realHeight)
    if (ICON_WIDTH > realWidth && ICON_HEIGHT > realHeight) {
        minFactor = 1;
    }
    var factor = minFactor > 0.25 ? 8.0 : 4.0 / Math.sqrt(minFactor);

    //图片缩放比例
    var scaleFactor = 1;
    if (realWidth > CANVAS_WIDTH && realHeight > CANVAS_HEIGHT) {
        if (realWidth / CANVAS_WIDTH > realHeight / CANVAS_HEIGHT) {
            scaleFactor = CANVAS_HEIGHT / realHeight;
        }
        else {
            scaleFactor = CANVAS_WIDTH / realWidth;
        }
    }
    //计算缩放比例结束

    //设置滑动条的位置，设置滑动条的值的变化来改变缩放率是一个非线性的变化，而是幂函数类型 即类似y=factor（X）--此处x为幂指数
    //此处100 * (Math.log(scaleFactor * factor) / Math.log(factor)) 为知道y 求解x 的算法
    $("#barchild").css("left", 100 * (Math.log(scaleFactor * factor) / Math.log(factor)) + "px");

    //图片实际尺寸，并近似到整数
    var currentWidth = Math.round(scaleFactor * realWidth);
    var currentHeight = Math.round(scaleFactor * realHeight);


    //图片相对CANVAS的初始位置，图片相对画布居中
    var originLeft = Math.round((CANVAS_WIDTH - currentWidth) / 2);
    var originTop = Math.round((CANVAS_HEIGHT - currentHeight) / 2 - CANVAS_HEIGHT);

    //计算截取框中的图片的位置
    var dragleft = originLeft - LEFT_EDGE;
    var dragtop = originTop - TOP_EDGE + CANVAS_HEIGHT;
    //设置图片当前尺寸和位置
    $iconElement.css({ width: currentWidth + "px", height: currentHeight + "px", left: originLeft + "px", top: originTop + "px" });


    //初始化默认值
    $("#txt_width").val(currentWidth);
    $("#txt_height").val(currentHeight);
    $("#txt_top").val(0 - dragtop);
    $("#txt_left").val(0 - dragleft);
    $("#txt_Zoom").val(scaleFactor);

    var oldWidth = currentWidth;
    var oldHeight = currentHeight;

    //设置图片可拖拽
    $iconElement.easydrag(false).setHandler("Crop").ondrag(function(e, target) {
        var pos = $(target).position();

        $("#txt_left").val(0 - (parseInt(pos.left) - LEFT_EDGE));
        $("#txt_top").val(0 - (parseInt(pos.top) - TOP_EDGE + CANVAS_HEIGHT));
    }
    );
    $("#barchild").easydrag(false, "x").ondrag(function(e, target) {        
        var me = $(target);
        var left = parseInt(me.css("left"));
        if (left < 0) {
            left = 0;
            me.css("left", 0);
        }
        if (left > 200) {
            me.css("left", 200);
        }

        //前面讲过了y=factor（x），此处是知道x求y的值，即知道滑动条的位置，获取缩放率
        scaleFactor = Math.pow(factor, (left / 100 - 1));
        if (scaleFactor < minFactor) {
            scaleFactor = minFactor;
        }
        if (scaleFactor > factor) {
            scaleFactor = factor;
        }
        //以下代码同初始化过程，因为用到局部变量所以没有
        var iconElement = $("#ImageIcon");


        var image = new Image();
        image.src = iconElement.attr("src");
        var realWidth = image.width;
        var realHeight = image.height;
        image = null;

        //图片实际尺寸
        var currentWidth = Math.round(scaleFactor * realWidth);
        var currentHeight = Math.round(scaleFactor * realHeight);

        //图片相对CANVAS的初始位置
        var originLeft = parseInt(iconElement.css("left"));
        var originTop = parseInt(iconElement.css("top"));

        if (currentWidth > oldWidth) {
            originLeft -= Math.round((currentWidth - oldWidth) / 2);
            originTop -= Math.round((currentHeight - oldHeight) / 2);
        }
        else {
            originLeft += Math.round((oldWidth - currentWidth) / 2);
            originTop += Math.round((oldHeight - currentHeight) / 2);
        }

        dragleft = originLeft - LEFT_EDGE;
        dragtop = originTop - TOP_EDGE + CANVAS_HEIGHT;

        //图片当前尺寸和位置
        iconElement.css({ width: currentWidth + "px", height: currentHeight + "px", left: originLeft + "px", top: originTop + "px" });

        $("#txt_Zoom").val(scaleFactor);
        $("#txt_left").val(0 - dragleft);
        $("#txt_top").val(0 - dragtop);
        $("#txt_width").val(currentWidth);
        $("#txt_height").val(currentHeight);
        oldWidth = currentWidth;
        oldHeight = currentHeight;

    });
    var SilderSetValue = function(i) {
        var left = parseInt($(".child").css("left"));
        left += i;

        if (left < 0) {
            left = 0;
        }
        if (left > 200) {
            left = 200;
        }

        scaleFactor = Math.pow(factor, (left / 100 - 1));
        if (scaleFactor < minFactor) {
            scaleFactor = minFactor;
        }
        if (scaleFactor > factor) {
            scaleFactor = factor;
        }
        var iconElement = $("#ImageIcon");
        var imagedrag = $("#ImageDrag");

        var image = new Image();
        image.src = iconElement.attr("src");
        var realWidth = image.width;
        var realHeight = image.height;
        image = null;

        //图片实际尺寸
        var currentWidth = Math.round(scaleFactor * realWidth);
        var currentHeight = Math.round(scaleFactor * realHeight);

        //图片相对CANVAS的初始位置
        var originLeft = parseInt(iconElement.css("left"));
        var originTop = parseInt(iconElement.css("top"));

        if (currentWidth > oldWidth) {
            originLeft -= Math.round((currentWidth - oldWidth) / 2);
            originTop -= Math.round((currentHeight - oldHeight) / 2);
        }
        else {
            originLeft += Math.round((oldWidth - currentWidth) / 2);
            originTop += Math.round((oldHeight - currentHeight) / 2);
        }
        dragleft = originLeft - LEFT_EDGE;
        dragtop = originTop - TOP_EDGE + CANVAS_HEIGHT;

        //图片当前尺寸和位置
        $("#barchild").css("left", left + "px");
        iconElement.css({ width: currentWidth + "px", height: currentHeight + "px", left: originLeft + "px", top: originTop + "px" });


        $("#txt_Zoom").val(scaleFactor);
        $("#txt_left").val(0 - dragleft);
        $("#txt_top").val(0 - dragtop);
        $("#txt_width").val(currentWidth);
        $("#txt_height").val(currentHeight);

        oldWidth = currentWidth;
        oldHeight = currentHeight;
    }
    //点击加减号
    $("#moresmall").click(function() {
        SilderSetValue(-20);
    });
    $("#morebig").click(function() {
        SilderSetValue(20);
    });

});