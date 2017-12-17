// JScript 文件

function $163(str){
	return document.getElementById(str);
}
function _(str){
	return document.getElementsByTagName(str);
}
function msg(boxtitle,boxtype,msg,afterclickOK){
	$163("msg_div_main").style.left = (_("body")[0].clientWidth - 424) / 2;
	$163("msg_div_main").style.top  = (_("body")[0].clientHeight - 200) / 2;
	var msg_div_main_btnSysMsgOk = "<input class=\"syBtn\" id=\"btnSysMsgOk\" type=\"button\" value=\"确 定\" onclick=\"msg_close_tmp_biyuan();" + afterclickOK +"\" />";
	var msg_div_main_MsgCancel = "<input class=\"syBtn\" id=\"btnSysMsgCancel\" type=\"button\" value=\"取 消\" onclick=\"msg_close_tmp_biyuan();\" />";			
	switch(boxtype * 1){
		case 1:
			$163("msg_div_main_content").innerHTML = msg;
			$163("msg_div_main_title").innerHTML = boxtitle;
			$163("msg_div_main_btn").innerHTML = msg_div_main_btnSysMsgOk;
		break;
		case 2:
			$163("msg_div_main_content").innerHTML =  msg;
			$163("msg_div_main_title").innerHTML = boxtitle;
			$163("msg_div_main_btn").innerHTML = msg_div_main_MsgCancel + msg_div_main_btnSysMsgOk;
		break;
		case 3:
			$163("msg_div_main_content").innerHTML =  msg;
			$163("msg_div_main_title").innerHTML = boxtitle;
			$163("msg_div_main_btn").innerHTML = "";
		break;
		defualt:
			$163("msg_div_main_content").innerHTML =  msg;
			$163("msg_div_main_title").innerHTML = boxtitle;
			$163("msg_div_main_btn").innerHTML = msg_div_main_btnSysMsgOk;
		break;
	}
	$163("msg_div_main_title").innerHTML =  boxtitle;
	$163("msg_div_all").style.zIndex  = 100;
	$163("msg_div_main").style.zIndex = 200;
	$163("msg_div_all").style.display = "";
	$163("msg_div_main").style.display = "";
	$163("msg_div_all").style.height = "100%";
	$163("msg_div_all").oncontextmenu = function(){
		return false;
	}
	$163("msg_div_main").oncontextmenu = function(){
		return false;
	}
	document.body.scroll="no";
}
function msg_close_tmp_biyuan(){
	$163('msg_div_all').style.display='none';
	$163('msg_div_main').style.display='none';
	document.body.scroll="";
}
var msg_md = false,msg_mobj,msg_ox,msg_oy;
document.onmousedown = function(){
	if(typeof(event.srcElement.msg_canmove) == "undefined"){
		return;
	}
	if(event.srcElement.msg_canmove){
		msg_md = true;
		msg_mobj = $(event.srcElement.msg_forid);
		msg_ox = msg_mobj.offsetLeft - event.x;
		msg_oy = msg_mobj.offsetTop - event.y;
	}
}
document.onmouseup = function(){
	msg_md = false;
}
document.onmousemove = function(){
	if(msg_md){
		msg_mobj.style.left = event.x + msg_ox;
		msg_mobj.style.top  = event.y + msg_oy;
	}
}

document.writeln(""

	+ "<div id='msg_div_all' style='display:none;'></div>"
	+ "<div id='msg_div_main' style='display:none;'>"
	+ "        <div class=\"sysWin\" id=\"sysMsgWin\" style=\"z-index: 999; \">"
 	+ "           <h2 msg_canmove=\"true\" msg_forid=\"msg_div_main\">"
 	+ "               <div class=\"fLe\" >"
	+ "                </div>"
 	+ "               <b class=\"icoSw\" id=\"icoSw\" msg_canmove=\"true\" msg_forid=\"msg_div_main\"></b><span id=\"msg_div_main_title\" msg_canmove=\"true\" msg_forid=\"msg_div_main\">系统提示</span><div class=\"fRi\" >"
	+ "                </div>"
	+ "                <a class=\"clsWin\" id=\"btnSysInfoClose\" title=\"关闭\" href=\"javascript:msg_close_tmp_biyuan();\"></a>"
	+ "            </h2>"
	+ "            <div class=\"bdy\">"
	+ "                <div class=\"bdyCtn\">"
	+ "                    <table class=\"swTb\">"
	+ "                        <tbody>"
	+ "                            <tr>"
	+ "                                <th>"
	+ "                                    <b class=\"icoIfo\" id=\"icoIfo\"></b>"
	+ "                                </th>"
	+ "                                <td>"
	+ "                                    <span class=\"swTit\" id=\"msg_div_main_content\"></span></td>"
	+ "                            </tr>"
	+ "                        </tbody>"
	+ "                    </table>"
	+ "                    <div class=\"clear\">"
	+ "                    </div>"
	+ "                </div>"
	+ "            </div>"
	+ "            <div class=\"bot\">"
	+ "                <div class=\"fLe\">"
	+ "                </div><div id=\"msg_div_main_btn\"></div>"
	+ "                <div class=\"fRi\"></div>"
	+ "            </div>"
	+ "        </div></div>");