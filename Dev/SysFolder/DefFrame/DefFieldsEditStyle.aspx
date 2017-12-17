<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefFieldsEditStyle.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefFieldsEditStyle" ValidateRequest="false"  %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>字段风格维护</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../../css/DefFrame.css"/>
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <script type="text/javascript" src="../../js/lhgdialog.min.js"></script>
    <style type="text/css">
    #maintbl
    {
	    border-collapse: collapse;
        margin-left:auto;
	    margin-right:auto;
	    font-size: 12px;
	    line-height:20px;
	    border:#a0a0a0 1px solid;
	    color:#393939;
	    background:#ebf3ff;
	    width:900px;
    }
    #maintbl>tbody>tr>td,#maintbl th
    {
        border:1px solid #a0a0a0;
        padding:2px;
    }
    .textbox
    {
        border:1px solid #ddd;
        width:96%;
    }
    .txtfieldw
    {
        width:40px;}
    .txtfieldh
    {
        width:40px;}
    .txtdispstylename
    {
        width:100px;
        background-color:transparent;
        border-style:hidden;
        }
    .focus{background:lightgreen;}
    .seldefvaluetype
    {
        display:none;
    }
    .txtdefvalue{display:none;}
    .txtdispstyletxt
    {
    width:240px;
    }
    input[type=button]{
        width:auto;
        padding:2px;
        }
    a:hover{text-decoration:none;color:Red;}
    .selbtn,.defbtn{color:blue;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="menubar">
      <div class="topnav">
        <table width="100%" border="0">
            <tr>
                <td width="300">&nbsp;&nbsp;业务名称：<%=tblName %>&nbsp;&nbsp;</td>
                <td></td>
                <td width="420">
                    字段风格：
                    <asp:DropDownList ID="ddlStyle" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlStyle_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" >【新建风格】</asp:LinkButton>
                    <a href="javascript:" id="btnPreview">【界面预览】</a>
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" >【保 存】</asp:LinkButton>
                </td>
            </tr>
        </table>
       </div>
   </div>
	    <br />
		<table id="maintbl" align="center" border="1">
            <thead>
				<tr>
				    <th align="center" width="30" height="25">序号</th>
					<th align="center" width="100" height="25">字段名</th>
					<th align="center" width="140" >中文名</th>
					<th align="center" width="40">显示</th>
					<th align="center" width="40">只读</th>
					<th align="center" width="40">必填</th>
					<th align="center" width="40">宽度</th>
					<th align="center" width="40">高度</th>
					<th align="center" >编辑风格</th>
					<th align="center" width="100">默认值</th>
				</tr>
            </thead>
            <tbody>
            <%=fieldHTML2 %>
            </tbody>
		</table>
        <br/>
    <asp:TextBox ID="txtxml" TextMode="MultiLine" CssClass="hidden" runat="server"></asp:TextBox>
        <input type="hidden" id="stylecode" />
        <input type="hidden" id="stylename" />
        <input type="hidden" id="styletxt" />
    	<script type="text/javascript">
    	    var _curClass = EIS.Studio.SysFolder.DefFrame.DefFieldsEditStyle;
    	    var _upKeyCode = 38;
    	    var _downKeyCode = 40;
    	    var fldmodel = jQuery(jQuery.parseXML("<xml><root><%= fieldXML %></root></xml>"));
    	    $(function () { //初始化

    	        //界面预览
    	        $("#btnPreview").click(function () {
    	            var url = '../AppFrame/AppInput.aspx?para=' + _curClass.CryptPara('tblName=<%=tblName %>&sindex=' + $("#ddlStyle").val()).value;
    	            var dlg = new $.dialog({
    	                title: '界面预览', page: url
                    , btnBar: false, cover: true, lockScroll: true, width: 1000, height: 600, bgcolor: 'gray'

    	            });
    	            dlg.ShowDialog();
    	        });

    	        $("#LinkButton1").click(chkform);
    	        $("input[type!=button],select").change(updatexml);
    	        $("input[type!=button]").keydown(function () {
    	            var i = parseInt($(this).attr("oindex"));
    	            var ctlName = $(this).attr("name");
    	            if (event.keyCode == _upKeyCode) {
    	                //向上
    	                var ctl = $("#" + ctlName + (i - 1));
    	                if (ctl.length > 0) {
    	                    ctl[0].focus();
    	                    ctl[0].select();
    	                }
    	            }
    	            else if (event.keyCode == _downKeyCode) {
    	                //向下
    	                var ctl = $("#" + ctlName + (i + 1));
    	                if (ctl.length > 0) {
    	                    ctl[0].focus();
    	                    ctl[0].select();
    	                }
    	            }
    	        })
    	        $("#maintbl td").click(function () {
    	            $("tr.focus").removeClass("focus");
    	            $(this).parent("tr").addClass("focus");
    	        });
    	        $("input[type!=button]").focusin(function () {
    	            var i = $(this).attr("oindex");
    	            $(this).toggleClass("focus", "");

    	            $("tr.focus").removeClass("focus");
    	            $(this).closest("tr").addClass("focus");

    	        }).focusout(function () {
    	            var i = $(this).attr("oindex");
    	            $(this).toggleClass("focus", "");

    	            $("tr.focus").removeClass("focus");
    	            $(this).closest("tr").addClass("focus");
    	        });

    	        $(".selbtn").click(function () {
    	            selIndex = $(this).attr("oindex");

    	            $("#stylecode").val($("#txtdispstyle" + selIndex).val());
    	            $("#stylename").val($("#txtdispstylename" + selIndex).val());
    	            $("#styletxt").val($("#txtdispstyletxt" + selIndex).val());

    	            var xmlobj = fldmodel.find("td[oindex='" + selIndex + "']");
    	            var fieldid = xmlobj.attr("fieldid");
    	            var fieldname = xmlobj.find("txtname").text();
    	            var url = '<%=Page.ResolveUrl("~") %>SysFolder/DefFrame/DefSelectStyleFrame.aspx?tblname=<%=tblName %>&fieldid=' + fieldid + "&fieldname=" + fieldname;
    	            var dlg = new $.dialog({
    	                title: '字段风格设置', page: url, maxBtn: false
                    , btnBar: false, cover: true, lockScroll: true, width: 860, height: 620, bgcolor: 'gray'

    	            });
    	            dlg.ShowDialog();
    	            //_openCenter(url, "_blank", 800, 600);

    	        });
    	        $(".defbtn").click(function () {
    	            selIndex = $(this).attr("oindex");
    	            var url = '<%=Page.ResolveUrl("~") %>SysFolder/DefFrame/DefDefaultValue.aspx?oindex=' + selIndex;
    	            var dlg = new $.dialog({
    	                title: '默认值设置', page: url, maxBtn: false
                    , btnBar: false, cover: false, lockScroll: true, width: 560, height: 300, bgcolor: 'gray'

    	            });
    	            dlg.ShowDialog();
    	        });

    	    });
            var selIndex = "";
            function updatexml() {
                var i = $(this).attr("oindex");
                var xmlobj = fldmodel.find("td[oindex=" + i + "]");
                var txtname = this.name;
                var txtvalue = $(this).val() || " ";

                if (this.type == "checkbox") {
                    txtvalue = this.checked ? "1" : "0";
                }
                var fldstate = xmlobj.attr("state");
                if (fldstate == "unchanged") {
                    jQuery(xmlobj).find(txtname).text(txtvalue);
                    xmlobj.attr("state", "changed");
                }
                else {
                    jQuery(xmlobj).find(txtname).text(txtvalue);
                }
                //更新XML

            }

            var regfldcn = /.+/;
            var regwidth = /^[1-9]+\d*$/;
            function chkform()  //判断字段长度输入是否规范
            {
                var f = true;
                $("input.textbox").each(function () {

                    switch (this.name) {
                        case "txtnamecn":
                            if (!regfldcn.test($(this).val())) {
                                alert("字段中文名不符合规定");
                                $(this).select();
                                f = false;
                                return false;
                            }
                            break;
                        case "txtwidth":
                            if (!regwidth.test($(this).val())) {
                                alert("列宽不符合规定");
                                $(this).select();
                                f = false;
                                return false;
                            }
                            break;
                    }
                });
                if (!f)
                    return false;
                $("#txtxml").val(_serializeToString(fldmodel));
            }

            function _serializeToString(objXML) {
                if (window.XMLSerializer) {
                    return (new XMLSerializer()).serializeToString(objXML.find("root")[0])
                }
                else {
                    return objXML.find("root")[0].xml;
                }
            }

            function styleCallBack(ret) {
                var arr = ret.split(":");
                var i = ret.indexOf(":");
                i = ret.indexOf(":", i);
                i = ret.indexOf(":", i + 1);
                var txt = ret.substr(i + 1);
                $("#txtdispstyle" + selIndex).val(arr[0]).change();
                $("#txtdispstylename" + selIndex).val(arr[1]).change();
                $("#txtdispstyletxt" + selIndex).val(txt).change();
                $("#selbtn" + selIndex).text(arr[1]);
            }
            function defaultCallBack(deftype, deftypecn, defvalue) {

                $("#txtdeftype" + selIndex).val(deftype).change();
                $("#txtdefvalue" + selIndex).val(defvalue).change();
                $("#defbtn" + selIndex).text(deftypecn);
            }
            function _openCenter(url, name, width, height) {
                var str = "height=" + height + ",innerHeight=" + height + ",width=" + width + ",innerWidth=" + width;
                if (window.screen) {
                    var ah = screen.availHeight - 30;
                    var aw = screen.availWidth - 10;
                    var xc = (aw - width) / 2;
                    var yc = (ah - height) / 2;
                    str += ",left=" + xc + ",screenX=" + xc + ",top=" + yc + ",screenY=" + yc;
                    str += ",resizable=yes,scrollbars=yes,directories=no,status=no,toolbar=no,menubar=no,location=no";
                }
                return window.open(url, name, str);
            }
		</script>
    </form>
</body>
</html>
