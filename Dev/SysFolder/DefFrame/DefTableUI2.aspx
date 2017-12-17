<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefTableUI2.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefTableUI2" ValidateRequest="false"  %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>模板编辑-编辑界面</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="../../css/DefStyle.css"/>
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <script type="text/javascript" src="../../js/jquery.cookie.js"></script>
    <script type="text/javascript" src="../../js/lhgdialog.min.js"></script>
    <script type="text/javascript" src="../tiny_mce/tiny_mce.js"></script>

    <style type="text/css">
    #fieldlist{
	    list-style-type: none; 
	    float: left; 
	    background: aliceblue; 
	    width: 95%; 
	    height:580px;
	    border:1px dashed teal;
	    text-align:left;
	    overflow:auto;
	    
	}
	.li-field , .fieldattr{
	    text-align:left; 
	    color:teal;
	    font-family:Tahoma,Helvetica,Arial,sans-serif;
	    margin:1px; 
	    padding: 1px 0px 0px 1px;
	    display:inline-block;
	    font-size:9pt;
        cursor:hand;
	}
	.titleZone{
	    background:url(../../img/toolbar/bg.gif) repeat-x;
	    display:block;
        height:26px;
        line-height:26px;
        padding-left:5px;
        margin:1px;
        border:1px solid lightblue;
        cursor:pointer;
	    }
	.titleZone a{line-height:24px;padding:1px 0px 1px 20px;
	             background:transparent url(../../img/common/site.png) no-repeat}
	.titleZone a.linkRelation{
	             background:transparent url(../../img/common/add_small.png) no-repeat}
	html,body {
        margin:0;
        padding:0;
        border:medium none;
        overflow:hidden;
        height:100%;
    }
    #form1{height:100%;}
    .textbox
    {
        width:95%;
    }
    #fieldtd
    {
    	padding:5px;
    	}
    #txtStyleName
    {
    	vertical-align:middle;
    	float:none;
    }
    .mceEditor{display:block;width:900px;}
    .hidden{display:none;}
	.middlebar{
        text-align:left;
        background-color:rgb(229, 241, 246);
        border:3px solid #eee;
        margin:5px 0px;
        padding:2px 3px;
        width:900px;
        }
    .middlebar a.linkbtn{padding:0px 5px;}
    .middlebar a.linkbtn:hover{
        background:#3e88c7;
        color:White;
        border-radius:2px;
        padding:0px 5px;
        text-decoration:none;
        } 
    a{text-decoration:none;color:blue;}
    a:hover{text-decoration:none;color:Red;}
    .curstyle{color:red;}
	</style>
	<script type="text/javascript">
	    var editor = null;
	    var _dragData = "";
	    var _curClass = null;
	    $(function () {
	        var t = parseInt("<%=t %>") - 1;
	        $(".styleLink:eq(" + t + ")").addClass("curstyle");

	        _curClass = EIS.Studio.SysFolder.DefFrame.DefTableUI2;

	        function SaveData(showMsg) {
	            var ed = tinyMCE.getInstanceById("TextBox1");
	            var ret = _curClass.SaveData("<%=tblName %>", "<%=styleIndex %>", "<%=t %>", editor.getContent());
	            if (ret.error) {
	                alert("保存出错：" + ret.error.Message);
	            }
	            else {
	                if (showMsg)
	                    $.noticeAdd({ text: '保存成功！', stay: false, stayTime: 200 });
	            }
            }

	        $("#LinkButton1").click(function () {
	            SaveData(true);
	            return false;
	        });

	        //界面预览
	        $("#btnPreview").click(function () {
	            SaveData(false);
	            var sindex = "<%=styleIndex %>";
	            var paraStr = "tblName=<%=tblName %>&sindex=" + (sindex == "0" ? "" : sindex);
	            var url = "../AppFrame/AppInput.aspx?para=" + _curClass.CryptPara(paraStr).value;
	            var dlg = new $.dialog({
	                title: '界面预览', page: url
                    , btnBar: true, cover: true, lockScroll: true, width: 900, height: 600, bgcolor: 'gray', cancelBtnTxt: '关闭',
	                onCancel: function () {
	                }
	            });
	            dlg.ShowDialog();
	        });
	        $("#DropDownList1").change(function () {
	            var i = $(this).val();
	            var paraStr = "tblName=<%=tblName %>&styleindex=" + i + "&t=<%=t%>";
	            var url = "DefTableUI2.aspx?para=" + _curClass.CryptPara(paraStr).value;
	            window.open(url, "_self");
	            return false;
	        });
	        $("#ddlTblName").change(function () {
	            var tbl = $(this).val();
	            var paraStr = "tblName=" + tbl + "&styleindex=<%=styleIndex %>&t=<%=t%>";
	            var url = "DefTableUI2.aspx?para=" + _curClass.CryptPara(paraStr).value;
	            window.open(url, "_self");
	            return false;
	        });
	        $(".li-field").live("click", function () {
	            editor.execCommand('mceInsertContent', false, $(this).attr("ref")); return false;
	        }).bind("drag", function (e) {
	            var ref = $(e.srcElement).attr("ref");
	            _dragData = ref;
	        });

	        $("#linkbtn4").click(function () {
	            var t4 = [];
	            t4.push("<table class='normaltbl' border='1'><caption><%=tblNameCn %></caption>");
	            t4.push("<tbody>");
	            t4.push("<tr><td width='20%'>&nbsp;</td><td width='30%'></td><td width='20%'>&nbsp;</td><td></td></tr>");
	            t4.push("<tr><td>&nbsp;</td><td></td><td>&nbsp;</td><td></td></tr>");
	            t4.push("<tr><td>&nbsp;</td><td></td><td>&nbsp;</td><td></td></tr>");
	            t4.push("</tbody>");
	            t4.push("</table>");

	            editor.execCommand('mceInsertContent', false, t4.join("")); return false;
	        });

	        $("#linkbtn6").click(function () {
	            var t4 = [];
	            t4.push("<table class='normaltbl' border='1'><caption><%=tblNameCn %></caption>");
	            t4.push("<tbody>");
	            t4.push("<tr><td width='13%'>&nbsp;</td><td width='20%'></td><td width='13%'>&nbsp;</td><td width='20%'></td><td width='13%'>&nbsp;</td><td></td></tr>");
	            t4.push("<tr><td>&nbsp;</td><td></td><td>&nbsp;</td><td></td><td>&nbsp;</td><td></td></tr>");
	            t4.push("<tr><td>&nbsp;</td><td></td><td>&nbsp;</td><td></td><td>&nbsp;</td><td></td></tr>");
	            t4.push("</tbody>");
	            t4.push("</table>");

	            editor.execCommand('mceInsertContent', false, t4.join("")); return false;
	        });

	        $("#linkedit,#linkcopy,#linkdel").click(function () {
	            editor.execCommand('mceInsertContent', false, $(this).attr("command")); return false;
	        });
	        $("#linkBtn1").click(function () {
	            editor.execCommand('mceInsertContent', false, $(this).attr("command")); return false;
	        });
	        $(".titleZone2").attr("title", "展开/收缩").click(function () {
	            $(this).next().toggle();
	        });

	        $(".li-table").click(function (e) {
	            editor.execCommand('mceInsertContent', false, joinHtml(this));
	        }).bind("drag", function (e) {
	            joinHtml(this);
	        });

	        $("#btnField").click(function () {
	            $("#fieldtd").toggle();
	        });

	        tinyMCE.init({
	            language: 'cn',
	            mode: "exact",
	            elements: "TextBox1",
	            theme: "advanced",
	            verify_html: false,
	            plugins: "autolink,lists,pagebreak,style,table,save,advhr,advimage,advlink,inlinepopups,preview,searchreplace,contextmenu,paste,directionality,fullscreen,noneditable,nonbreaking,xhtmlxtras,template,advlist",
	            // Theme options
	            theme_advanced_buttons1: "code,undo,redo,|,cut,copy,paste,pastetext,pasteword,|,forecolor,backcolor,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,styleselect,formatselect,fontselect,fontsizeselect",
	            theme_advanced_buttons2: "tablecontrols,|search,replace,|,bullist,numlist,|,link,unlink,image,|,removeformat,visualaid,|,sub,sup,|,charmap,|,fullscreen,attribs,template",
	            theme_advanced_toolbar_location: "top",
	            theme_advanced_toolbar_align: "left",
	            theme_advanced_statusbar_location: "bottom",
	            theme_advanced_resizing: true,
	            theme_advanced_path: false,
	            theme_advanced_fonts: "宋体," +
                    "Arial=arial,helvetica,sans-serif;" +
                    "Arial Black=arial black,avant garde;" +
                    "Courier New=courier new,courier;",

	            content_css: "../../css/editorStyle.css",
	            remove_linebreaks: true,
	            cleanup: true,

	            extended_valid_elements: "pre[name|class],style",
	            valid_children: "+body[style],+body[link]",

	            width: "900px",
	            height: "540px",

	            external_link_list_url: "lists/link_list.js",
	            external_image_list_url: "lists/image_list.js",
	            media_external_list_url: "lists/media_list.js",
	            template_external_list_url: "tmpl/template_list.js",
	            template_replace_values: { username: "Some User", staffid: "991234" },
	            oninit: function () { editor = tinyMCE.getInstanceById("TextBox1"); },
	            execcommand_callback: "myExecCommandHandler",
	            setupcontent_callback: function (editor_id, b, doc) {
	                b.ondrop = function () {
	                    if (_dragData == "")
	                        return true;
	                    var ed = tinyMCE.getInstanceById(editor_id);
	                    ed.execCommand('mceInsertContent', false, _dragData);
	                    _dragData = "";
	                    return false;
	                }

	            }
	        });


	        //插入关系表
	        $(".linkRelation").click(function () {
	            var dlg = new $.dialog({
	                title: '创建表关系', page: 'DefTableRelation.aspx?tblName=<%=tblName %>'
                    , btnBar: false, cover: true, lockScroll: true, width: 600, height: 300, bgcolor: 'gray',
	                onCancel: function () { }
	            });
	            dlg.ShowDialog();
	        });

	    });

        function myExecCommandHandler(editor_id, elm, command, user_interface, value) {
            var linkElm, inst;
            switch (command) {
                case "mceInsertContent":
                    if (elm.nodeName == "TD" && value.indexOf("{") == 0 && (elm.innerHTML == "&nbsp;" || elm.innerHTML == "&nbsp;&nbsp;" || elm.innerHTML == "&nbsp;&nbsp;&nbsp;")) {
                        elm.innerHTML = value;
                        return true;
                    }
                    break;
            }
            return false;
        }
        function loadFields(tblName) {
            var listStr = _curClass.GetFields(tblName).value;
            var list = listStr.split("|");
            var arr = [];
            arr.push("<div class='titleZone'><a title='点击或拖动插入子表' href='#' class='li-table' ref='", tblName, "'>", tblName, "</a></div><div class='fieldZone'>");
            for (var i = 0; i < list.length; i++) {
                var st = list[i].split(',');
                arr.push("<div class='fieldZone' ref='", st[0], "|", st[1], "'>", "<a href='#' class='fieldattr' ref=''>[*]</a>&nbsp;"
                , "<a href='#' class='li-field' ref='{", st[0], "}'>", st[0], "</a>&nbsp;"
                , "<a href='#' class='li-field' ref='", st[1], "'>", st[1], "</a></div>");
            }
            arr.push("</div>");
            var newList = $(arr.join(""));
            $(".titleRel").before(newList);

            $(".li-table", newList).click(function (e) {
                editor.execCommand('mceInsertContent', false, joinHtml(this));
            }).bind("drag", function (e) {
                joinHtml(this);
            });
        }
        function joinHtml(obj) {
            var titleRef = [];
            var fieldRef = [];
            var subTbl = $(obj).attr("ref");

            titleRef.push("<table id=\"" + subTbl + "\" class=\"subtbl\" border=\"1\"><caption>明细表标题</caption>");
            titleRef.push("<thead><tr>");
            fieldRef.push("<tbody><tr>");
            var fldLen = $(obj).parent().next().children().length;
            $(obj).parent().next().children().each(function () {
                var arr = $(this).attr("ref").split("|");
                titleRef.push("<td>&nbsp;", arr[1], "</td>");
                fieldRef.push("<td>{", arr[0], "}</td>");
            });
            titleRef.push("<td class='ADDBTN'>[#ADD#]</td>");
            fieldRef.push("<td class='DELBTN'>[#DEL#]</td>");

            titleRef.push("</tr></thead>");
            fieldRef.push("</tr></tbody>");
            fieldRef.push("<tfoot>");
            fieldRef.push("<tr>");
            fieldRef.push("<td colspan='", fldLen + 1, "'><a href='javascript:' class='linkAdd'>添加行</a></td>");
            fieldRef.push("</tr>");
            fieldRef.push("</tfoot>");
            fieldRef.push("</table>");
            _dragData = titleRef.concat(fieldRef).join("");

            return _dragData;
        }
    </script>
</head>
<body style="height:100%;">
    <form id="form1" runat="server">

    <div class="menubar">
      <div class="topnav">
        <span style="float:left;margin-left:10px">
        业务名称：<asp:DropDownList ID="ddlTblName" runat="server" Width="120">
            </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        切换界面：
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;
            <a class="styleLink" href="DefTableUI2.aspx?tblName=<%=tblName %>&styleindex=<%=styleIndex%>&t=1" target="_self">[编辑界面]</a> 
            <a class="styleLink" href="DefTableUI3.aspx?tblName=<%=tblName %>&styleindex=<%=styleIndex%>&t=2" target="_self">[手机界面]</a> 
            <a class="styleLink" href="DefTableUI4.aspx?tblName=<%=tblName %>&styleindex=<%=styleIndex%>&t=3" target="_self">[打印界面]</a> 
            <a class="styleLink" href="DefTableUI2.aspx?tblName=<%=tblName %>&styleindex=<%=styleIndex%>&t=4" target="_self">[查看界面]</a> 

        </span>
        <span style="float:right;margin-right:10px">
            <a href="DefTableStyleList.aspx?tblName=<%=tblName %>" target="_self">返回列表</a>
            <a href="DefTableUI.aspx?tblName=<%=tblName %>&styleindex=<%=styleIndex%>&t=<%=t%>" target="_self">HTML界面</a> 
            <a href="javascript:" id="btnPreview">保存预览</a> 
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" >保存</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        
        </span>
    </div>
    </div>
    <div style="text-align:left;height:100%">
   <table width="100%" height="100%" align="center">
    <tr>
        <td align="left" width="200" valign="top" id="fieldtd">
            <div id="fieldlist">
            <%=fieldlist1.ToString()%>
            <div class='titleZone titleRel'><a href="javascript:" class="linkRelation">添加其它表</a></div>
            </div>
        </td>
        <td align="center" valign="top">
            <asp:TextBox ID="TextBox1" Width="96%" CssClass="hidden tinymce" runat="server"  TextMode="MultiLine"></asp:TextBox>
			  <div class="middlebar">
                <a class="linkbtn" id="linkbtn4" href="javascript:" >插入4列主表</a> 
                    <em class="split">|</em>
                <a class="linkbtn" id="linkbtn6" href="javascript:" >插入6列主表</a>
                    <em class="split">|</em> 
                <a class="linkbtn" id="linkedit" command="[#EDIT#]" href="javascript:" >插入编辑行标识</a> 
                    <em class="split">|</em>
                <a class="linkbtn" id="linkcopy" command="[#COPY#]" href="javascript:" >插入复制行标识</a> 
                    <em class="split">|</em>
                <a class="linkbtn" id="linkdel" command="[#DEL#]" href="javascript:" >插入删除行标识</a> 
                    <em class="split">|</em>
                <a class="linkbtn" id="linkBtn1" command="<a href='javascript:' class='linkAdd'>添加行</a>" href="javascript:" >添加行</a> 
            </div>
        </td>
     </tr>
  </table>
  </div>



    </form>
</body>
</html>