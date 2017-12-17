<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FunLimitTop.aspx.cs" Inherits="EIS.Studio.SysFolder.Limit.FunLimitTop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>功能节点授权</title>
    <script src="../../Js/jquery-1.7.js" type="text/javascript"></script>
	<script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/lhgdialog.min.js"></script>
    <style type="text/css">
        html{         
            margin:0px;
            padding:0px;}
        body{
             height:38px;
             margin:0px;
             padding:0px;
             background:#f9fafe;
             font-size:12px;
             border-bottom:2px solid #aaa;}
        a{text-decoration:none;line-height:30px;}
        a:hover{background-color:#eee;color:red;}
    </style>
    <script type="text/javascript">
        jQuery(function () {
            $("#DropDownList1").change(function () {
                window.parent.frames["left"].open("FunLimitLeft.aspx?webId=" + $("#DropDownList1").val(), "_self");
            });
            window.parent.frames["left"].open("FunLimitLeft.aspx?webId=" + $("#DropDownList1").val(), "_self");

            jQuery("input[name=limitType]").click(function () {
                var t = getLimitType();
                var funId = $("#funId").val();
                if (funId == "") {
                    alert("请先选择功能节点");
                    return;
                }
                if (t == "1") {
                    window.open("FunLimitDept.aspx?funId=" + funId + "&funName=", "main");
                }
                else if (t == "2") {
                    window.open("FunLimitPosition.aspx?funId=" + funId + "&funName=", "main");
                }
                else if (t == "3") {
                    window.open("FunLimitEmployee.aspx?funId=" + funId + "&funName=", "main");
                }
                else if (t == "4") {
                    window.open("FunLimitRole.aspx?funId=" + funId + "&funName=", "main");
                }
                else if (t == "5") {
                    window.open("FunLimitExclude.aspx?funId=" + funId + "&funName=", "main");
                }
                else if (t == "0") {
                    window.open("FunLimitQuery.aspx?funId=" + funId + "&funName=", "main");
                }
            });

            $(".winHelp").click(function () {
                var url = $(this).attr("data-hlpUrl");
                app_showHelp(url, "节点授权帮助");
            });

        });
        function getLimitType() {
            return jQuery("input[name=limitType]:checked").val();
        }



        function app_showHelp(urlStr, titleStr) {
            if ((titleStr == undefined) || (titleStr == "")) titleStr = "帮助";
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
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" border=0>
            <tr>
                <td height="34" width="300">&nbsp;&nbsp;切换系统：
                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>
                    <input type="radio" value="1" checked="checked" name="limitType" id="limitType1"/><label for="limitType1">【部门权限】</label>
                    <input type="radio" value="2" name="limitType" id="limitType2"/><label for="limitType2">【岗位权限】</label>
                    <input type="radio" value="3" name="limitType" id="limitType3"/><label for="limitType3">【员工权限】</label>
                    <input type="radio" value="4" name="limitType" id="limitType4"/><label for="limitType4">【角色权限】</label>
                    <input type="radio" value="5" name="limitType" id="limitType5"/><label for="limitType5">【排除权限】</label>                
                    <input type="radio" value="0" name="limitType" id="limitType0"/><label for="limitType0" style="color:red;">【权限查询】</label>                
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <a class="winHelp" data-hlpUrl="Help/HelpLimite.aspx#a7" href="javascript:void(0);">帮助</a>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <input type="hidden" id="funId" value="" />
    </form>
</body>
</html>
