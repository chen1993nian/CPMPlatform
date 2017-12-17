var APP_ITEM_HEIGHT = 28;
var MIN_PNAEL_HEIGHT = 11 * APP_ITEM_HEIGHT;
var SCROLL_HEIGHT = 4 * APP_ITEM_HEIGHT;
var SCREEN_MAX_APP_NUM = 32;

var default_icon = 'default';
var s_default_icon = 'oa';
var rowAppNum = 8;

window.onactive = function(){
   jQuery(window).triggerHandler('resize');
   window.onactive = null;
};

//过滤重复js元素 return array;
function unique(d){
   var o = {};
   jQuery.each(d, function(i, e) { 
      o[e] = i; 
   }); 
   var a = []; 
   jQuery.each(o, function(i, e) { 
      a.push(d[e]); 
   });
   return a;
}
//过滤重复js元素 返回 boolean
Array.prototype.S=String.fromCharCode(2);
Array.prototype.in_array=function(e)
{
    var r=new RegExp(this.S+e+this.S);
    return (r.test(this.S+this.join(this.S)+this.S));
}

function isTouchDevice(){
    try{
        document.createEvent("TouchEvent");
        return true;
    }catch(e){
        return false;
    }
}

//添加桌面应用 e {"func_id": ,"id": ,"name":} index 为要添加应用的屏幕索引
function addApp(e, index) { 
   var s = slideBox.getScreen(index); 
   if (s) { 
      var ul = s.find("ul"); 
      if (!ul.length) { 
         ul = jQuery("<ul></ul>");
         s.append(ul); 
          ul.sortable({
            revert: true,
            //delay: 200,
            //distance: 10,               //延迟拖拽事件(鼠标移动十像素),便于操作性
            tolerance: 'pointer',       //通过鼠标的位置计算拖动的位置*重要属性*
            connectWith: ".screen ul",
            scroll: false,
            stop: function(e, ui) {
              setTimeout(function() {
                    jQuery(".block.remove").remove();
                    jQuery("#trash").hide();
                    ui.item.click(openUrl);
                    serializeSlide();
              }, 0);
            },
            start: function(e, ui) {
               jQuery("#trash").show();
               ui.item.unbind("click");
            }
         });
      }
      addModule(e, s.find("ul")); 
   } 
}

function getAppMargin(){
      var clientSize = jQuery(document.body).outerWidth(true);
      var appsize = 120 * rowAppNum;
      if(clientSize > appsize){
         var _margin = Math.floor((clientSize - appsize - 70*2)/16);     
      }else{
         var _margin = 0;    
      }
      return _margin; 
}
   
function refixAppPos(){
      var _margin = getAppMargin() + "px";
      jQuery("#container .screen li.block").css({"margin-left": _margin, "margin-right":_margin})   
}

function delModule(el){
   var pObj = jQuery("#container .screen ul li.block");
   pObj.each(function(){
      var index = jQuery(this).attr("index");
      if(el == index){
         jQuery(this).remove();
         var flag = serializeSlide();
      }
   });
}

//lp 获取当前屏幕应用的个数
function getAppNums(index){
   var index = (index == "" || typeof(index) == "undefined") ? slideBox.getCursor() : index;  
   var num =  jQuery("#container .screen:eq("+index+") ul li.block").size();
   return num;          
}

function initMenus()
{	
	var modules = [];
	var screen_count = 0;
	var screen_array = funcIdStr.split("|");
	for(var i=0; i<screen_array.length; i++)
	{
        var idStr = screen_array[i];
        if(idStr == "")
	        continue;
	   
        var items = [];
        var item_count = 0;
        var item_array = idStr.split(",");
        for(var j=0; j<item_array.length; j++)
   	    {
   	       var func_id = item_array[j];
   	       if(func_id == "")
   	          continue;
   	       items[item_count++] = {id:func_id, name:funcarray[func_id][0], func_id:func_id, count:0};
   	       moduleIdStr += func_id + ",";
   	    }
   	    modules[screen_count++] = {title:"桌面", id:"1", items:items};
	}
	return modules;
}

function initModules(modules) {
   
   window.slideBox = jQuery("#container").slideBox({
      count: modules.length,
      cancel: isTouchDevice() ? "" : ".block", 
      obstacle: "200",
      speed: "slow",
      touchDevice: isTouchDevice(),
      control: "#control .control-c",
      listeners: {
         afterScroll: function(i) {
          },
          beforeScroll: function(i) {
             //图片延迟加载
             jQuery("img[_src]", slideBox.getScreen(i)).each(function(){
                this.src = this.getAttribute("_src");
                this.removeAttribute("_src");
             });
          }
       }
    });

   var count = 0;
   jQuery.each(modules || [] , function(i, e) {
      var ul = jQuery("<ul></ul>");
      slideBox.getScreen(i).append(ul)
      jQuery.each(e.items || [], function(j, e) {
         addModule(e, ul, (i!=0));
      });
      i++;
   });
}

function addModule(e, el, bImgDelay) {
   el = jQuery(el);
   bImgDelay = typeof(bImgDelay) != "undefined" ? bImgDelay : false;
   var _id = e.id;
   img_src = funcarray[_id][2];
   img_src = "theme/"+ostheme+"/images/app_icons/"+ (img_src==""?"default.png" : img_src);
   var li = jQuery("<li class=\"block " + (IsTextIcon ? "txtLi" : "") + "\"></li>");
   var img = jQuery("<div class='img " + (IsTextIcon ? "txtImg" : "") + "'><p class='" + (IsTextIcon ? "uhide" : "") + "'><img " + (bImgDelay ? ('src="/images/transparent.gif" _src="' + img_src + '"') : ('src="' + img_src + '"')) + "/></p></div>");
   

   var divT = jQuery("<div class=\"count\"></div>");
   li.attr("id", "block_" + e.func_id);
   li.attr("title", e.name);
   li.attr("index", e.func_id);
   var _margin = getAppMargin() + "px";
   li.css({"margin-left": _margin, "margin-right":_margin});
   divT.attr("id", "count_" + e.func_id);
   if(e.count > 0){
      divT.addClass("count" + e.count);   
   }
   var a = jQuery("<a class=\"icon-text " + (IsTextIcon ? "txtBox" : "") + "\" href=\"javascript: void(0)\"></a>");
   var span = "<span class='" + (IsTextIcon ? "txtTitle" : "") + "'>" + e.name + "</span>";
   li.append(img.append(divT)).append(a.append(span)); 
   el.append(li);
}

jQuery.noConflict();
(function($){
   function resizeContainer()
   {
      var wWidth = Math.floor(parseInt((window.innerWidth || (window.document.documentElement.clientWidth || window.document.body.clientWidth))*0.9));
      var blockWidth = $('#container > .block:first').outerWidth();
      if(blockWidth <= 0)
         return;
      
      var count = Math.min(4, Math.max(3, Math.floor(wWidth/blockWidth)));
      $('#container').width(blockWidth*count);
   }
         
   function openUrl(){
    var id = this.id.substr(6);
    try{
        //window.open(funcarray[id][1],"_self");
        var openType = funcarray[id][3];
        if(openType == "2")
        {
            window.open(funcarray[id][1],"_blank");
        }
        else if(openType == "3")
        {
            window.open(funcarray[id][1],"_top");
        }
        else{
            window.top._MP(id,funcarray[id][1],funcarray[id][0]);
        }
    }
    catch(e){
        window.open(funcarray[id][1],"_self");
    }
  }
   
   function initBlock()
   {
      $('#container .screen ul li.block').live("click",openUrl);
   }
    var maxtest = 0;
   
   function GetCounts(moduleIdStr)
   {
       //alert(moduleIdStr);return;
       var ret =_curClass.GetCounts(moduleIdStr);

       if (ret.error || ret.value==null) {
            if (maxtest >= 3) {
                //alert("会话超时，请重新登录");
                //window.location = "default.aspx?logout=1";
            }
            else {
                maxtest++;
                window.setTimeout(GetCounts, monInterval.reminder * 60 * 1000, moduleIdStr);
            }

        }
        else {
            var array = Text2Object(ret.value);
            if(typeof(array) == "object")
            {
               var counts = 0;
               for(var id in array)
               {
                   var count = Math.min(10, array[id]);
                  var className = count > 0 ? ('count count' + count) : 'count';
                  if(func_array[id]){
                      $('#count_' + id).attr('class', className);
                      $('#count_' + id).attr('title', array[id]);
                  }
                  counts += count;
               }
               
               if(counts > 0 && parent && typeof(parent.BlinkTabs) == 'function')
                  parent.BlinkTabs('p0');
            }
            window.setTimeout(GetCounts, monInterval.reminder * 60 * 1000, moduleIdStr);
        }
   }
  
         
   function CreateDialog(id, title, parent)
   {
      var html = '<div id="dialog_' + id + '" index="' + id + '" class="dialogContainer">';
      html += '<table class="dialog" align="center">';
      html += '   <tr class="head">';
      html += '      <td class="left"></td>';
      html += '      <td class="center">';
      html += '         <div class="title">' + title + '</div>';
      html += '         <a class="close" href="javascript:;"></a>';
      html += '      </td>';
      html += '      <td class="right"></td>';
      html += '   </tr>';
      html += '   <tr class="body">';
      html += '      <td class="left"></td>';
      html += '      <td class="center">';
      html += '         <div id="dialog_content_' + id + '" class="msg-content"></div>';
      html += '      </td>';
      html += '      <td class="right"></td>';
      html += '   </tr>';
      html += '   <tr class="foot">';
      html += '      <td class="left"></td>';
      html += '      <td class="center"></td>';
      html += '      <td class="right"></td>';
      html += '   </tr>';
      html += '</table>';
      html += '</div>';
      $(parent).append(html);
      $("#dialog_"+id).draggable({handle: 'tr.head',containment: 'window' , scroll: false});
   }
   
   function initTrash() {
         $("#trash").droppable({
            over: function() {
               $("#trash").addClass("hover");
            },
            out: function() {
               $("#trash").removeClass("hover");
            },
            drop: function(event, ui) {
               ui.draggable.addClass("remove").hide();
               delModule && delModule(ui.draggable.attr("index"));
               $(".ui-sortable-placeholder").animate({
                  width: "0"
               }, "normal", function() {
               });
               $("#trash").removeClass("hover");
            }
         });   
   }
   
   //扩展对话框
   $.extend({
      tExtDialog: function (options) {
         var defaults = {
            width: 600,
            height: 400,
            parent: $("body"),
            title: ''
         };
         
         var options = $.extend(true, defaults, options);
                 
         var width = options.width;
         var height = options.height;
         var id = options.id;
         var title = options.title;
         var parent = options.parent;
         var src = options.src;
         var icon = options.icon;
         var content = options.content;
        
         if(!$('#dialog_' + id).length)
         {         
            CreateDialog(id, title, parent);
            $('#dialog_' + id).draggable("destroy");
            $('#dialog_' + id).addClass('extDialog');
            $('#dialog_' + id + ' .dialog tr.head').css("cursor","");
            $('#dialog_' + id).css({"width" : width +"px","height" : height +"px"});
            $('#dialog_' + id + ' > .dialog').css({"width":"100%"});
            $("div.msg-content", $('#dialog_' + id)).css({"height":(height - 48) + "px"})
            if(icon){
               $('#dialog_' + id + ' .dialog .head .center .title').prepend("<img src = '"+icon+"' style='margin-right:5px' width='16' height='16' />");
            }
            if(src){
               $("#dialog_content_"+id).html("<iframe name='iframe' src='" + src +"' width='100%' height='100%' border='0' frameborder='0' marginwidth='0' marginheight='0'></iframe>");
            }else{
               $("#dialog_content_"+id).html(content);   
            }
         }
         
         function display()
         {
            var wWidth = (window.innerWidth || (window.document.documentElement.clientWidth || window.document.body.clientWidth));
            var hHeight = (window.innerHeight || (window.document.documentElement.clientHeight || window.document.body.clientHeight));
            
            var top = left = 0;
            var bst = document.body.scrollTop || document.documentElement.scrollTop;
            top = Math.round((hHeight - height)/2 + bst) + "px";
            mleft = "-" + Math.round(width/2) + "px";
            top = top < 0 ? top = 0 : top;

            $('#dialog_' + id).css({"top":top,"left":"50%","margin-left":mleft});
            $('#dialog_' + id).show();
            $('#overlay').height(window.document.documentElement.scrollHeight);
            $('#overlay').show();
         }
         return{
            display: display   
         }
      }
   });
   
   //构造屏幕设置html结构 return str;
   function returnScreen(){
      var html = '';
      var _len = slideBox.getCount();
      for(var i=0; i< _len; i++)
      {
         html += '<li class="minscreenceil" index='+i+'>' + (i+1) +'</li>';
      }
      return html;
   }
   
   //选中桌面已有的app，@para srceenid 屏幕自然索引    
   function getScreenAppIds(srceenid){
      var idstr = sep = '';
      if(srceenid){
         obj = $("#container .screen").eq(srceenid).find("li.block")
      }else{
         obj = $("#container .screen li.block");
      }
      obj.each(function(){
         var appid = $(this).attr("index");
         idstr += sep + appid;
         sep = ',';
      });
      return idstr;
   }
        
   //显示消息 @para msg 要显示的提示文字
   function portalMessage(msg){
      if(!msg) return;
      msgObj = $("#portalSettingMsg");
      msgObj.html(msg).show();
      setTimeout(function(){msgObj.empty().hide()},5000);
   }
   
   //修正点击按钮出现屏幕小按钮width为0的现象
   function refixminScreenbtn(){
      $('#control').width(window.document.documentElement.clientWidth);   
   }
   
   //refixDialogPos
   function refixDialogPos(){
      var dialog = $('div.extDialog:visible', document.body).first();
      height = dialog.height();
      width = dialog.width();
      var wWidth = (window.innerWidth || (window.document.documentElement.clientWidth || window.document.body.clientWidth));
      var hHeight = (window.innerHeight || (window.document.documentElement.clientHeight || window.document.body.clientHeight));
      var top = left = 0;
      var bst = document.body.scrollTop || document.documentElement.scrollTop;
      top = Math.round((hHeight - height)/2 + bst) + "px";
      mleft = "-" + Math.round(width/2) + "px";
      top = top < 0 ? top = 0 : top;
      dialog.css({"top":top,"left":"50%","margin-left":mleft});
   }
   
   $(window).resize(function(){
      
      return ;
      refixAppPos();
            
      $('#overlay').height(window.document.documentElement.scrollHeight);
      
      refixminScreenbtn();
      
      refixDialogPos();
      
   });
   
   function reSortMinScreen(){
      $("#screenPageDom #screen_list ul li.minscreenceil").each(function(i){
         $(this).text(i+1);
         $(this).attr("index",i);      
      });      
   }
   
   $(function($){
      
      $("body").focus();
      $('#overlay').height(window.document.documentElement.scrollHeight);
      //初始化显示列数
      resizeContainer();
      //初始化图标
      initModules(initMenus());
      //初始化图标间距
      refixAppPos();
      //模块点击事件
      initBlock();

      if(cur_pwbs != "")      
        GetCounts(moduleIdStr);
      
      initTrash();
      //初始化屏幕
      if(cur_pwbs == "")
      $(".screen ul").sortable({
            revert: true,
            //delay: 200,
            //distance: 10,               //延迟拖拽事件(鼠标移动十像素),便于操作性
            tolerance: 'pointer',       //通过鼠标的位置计算拖动的位置*重要属性*
            connectWith: ".screen ul",
            scroll: false,
            stop: function(e, ui) {
              setTimeout(function() {
                    $(".block.remove").remove();
                    //$("#trash").hide();
                    ui.item.click(openUrl);
                     serializeSlide();
              }, 0);

            },
            start: function(e, ui) {
               //$("#trash").show();
               refixminScreenbtn();
               ui.item.unbind("click");
            }
      });/**/
      
      CheckBkImg('div,a,ul,li,span');
   });
      
})(jQuery);

function CheckBkImg(selector)
{
   jQuery(selector).each(function(){
      jQuery(this).css('background-image');
   });
}

var __sto = setTimeout;
window.setTimeout = function(callback,timeout,param)
{
   var args = Array.prototype.slice.call(arguments,2);
   var _cb = function()
   {
      callback.apply(null,args);
   }
   return __sto(_cb,timeout);
};

//序列化桌面上的图标,并且更新
function serializeSlide() {
   var s = "";
   jQuery("#container .screen").each(function(i, e) {
      jQuery(this).find("li.block").each(function(j, el) {
         if(!jQuery(el).attr("index")) return true;
         s += jQuery(el).attr("index");
         s += ",";
      });
      s += "|";
   });
   if (s.length) {
      s = s.replace(/\|$/, "");   
   }
   
   var flag = true;
   var ret = _curClass.SaveOrder(s);
   return flag;
}

function Text2Object(data)
{
   try{
      var func = new Function("return " + data);
      return func();
   }
   catch(ex){
      return '<b>' + ex.description + '</b><br /><br />';
   }
}

function openURL(id, name, code)
{
   if(code.indexOf('http://') == 0 || code.indexOf('https://') == 0 || code.indexOf('ftp://') == 0)
   {
      window.open(code);
      return;
   }

   if(url.indexOf(".") < 0 && url.indexOf("?") < 0  && url.indexOf("#") < 0 && url.substring(url.length-1) != "/")
      url += "/";
      
   window.open(url);
}