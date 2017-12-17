<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonalRelation.aspx.cs" Inherits="EIS.Web.WorkAsp.Personal.PersonalRelation" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>关联人员设置</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <link type="text/css" rel="stylesheet" href="../../Css/appInput.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>

    <style type="text/css">
        table{border-color:gray;width:600px;}
        td{padding:5px;border-color:gray;}
        body{background:white;}
        input[type=text]{width:220px;}
    </style>

    <script type="text/javascript">

        $(document).ready(function () {
            $(".Read").attr("readonly", true);

            jQuery("#btnLeader").click(function () {
                openpage('../../SysFolder/Common/UserTree.aspx?method=1&queryfield=empid,empname&cid=txtLeaderId,txtLeader');
            });

            jQuery("#btnHelper").click(function () {
                openpage('../../SysFolder/Common/UserTree.aspx?method=1&queryfield=empid,empname&cid=txtHelperId,txtHelper');
            });

            jQuery("#btnColleague").click(function () {
                openpage('../../SysFolder/Common/UserTree.aspx?method=1&queryfield=empid,empname,posid&cid=txtColleagueId,txtColleague,txtPosId');
            });


            jQuery("#txtLeader,#txtHelper,#txtColleague").focus(function () {
                this.blur();
            });
            $("#initPass").click(function () {
                var empId = "<%=EditEmployeeId %>";
                if (empId) {
                    var ret = EIS.Studio.SysFolder.Permission.CompanyEmployeeEdit.InitialPassWord(empId);
                    if (ret.error) {
                        alert("初始化过程中出现错误:" + ret.error.Message);
                    }
                    else {
                        alert("已经把密码成功初始化");
                    }
                }
            });
        });

        function openCenter(url, name, width, height) {
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

        function openpage(url) {
            openCenter(url, "_blank", 640, 500);
        }
    </script>
</head>
<body>
    <form runat="server" id="form1">
    <!-- 工具栏 -->
    <div class="menubar">
        <div class="topnav">
                    <span style="float:right;">
                    <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">保存</asp:LinkButton>&nbsp;&nbsp;
                    </span>
        </div>
    </div>
    
    <div  id="maindiv">
    <br />
        <%=TipMessage %>
    <table class='normaltbl' style="width:600px;" border="1" cellpadding="3" align="center">
    <caption>常用联系人设置</caption>
    <tbody>
    <tr>
        <td width="100">
        上级领导
        </td>
        <td>
            <asp:TextBox ID="txtLeader" Width="400"  CssClass="TextBoxInChar" runat="server"></asp:TextBox>
            <input type="button" id="btnLeader" value="选择" />
            <asp:HiddenField ID="txtLeaderId" runat="server" />
            <div class="gray" style="clear:both;margin:3px 0px;">
            领导可以查看下级的日程安排、工作日记，也可以给下级安排工作
            </div>
        </td>
    </tr>
    <tr>
        <td>助手/秘书</td>
        <td >
            <asp:TextBox ID="txtHelper" Width="400"  CssClass="TextBoxInChar" runat="server"></asp:TextBox>
            <input type="button" id="btnHelper" value="选择" />
            <asp:HiddenField ID="txtHelperId" runat="server" />
            <div class="gray" style="clear:both;margin:3px 0px;">秘书可以帮领导安排日程，关注催办待办事项</div>
        </td>
        </tr>
        <tr>
            <td>常用联系人</td>
            <td >
                <asp:TextBox ID="txtColleague" Width="400" TextMode="MultiLine" Rows="6"  CssClass="TextBoxInArea" runat="server"></asp:TextBox>
                <input type="button" id="btnColleague" value="选择" />
                <asp:HiddenField ID="txtPosId" runat="server" />
                <asp:HiddenField ID="txtColleagueId" runat="server" />
                <div class="gray" style="clear:both;margin:3px 0px;">选中的同事会出现在“选择人员”的首页</div>
            </td>
        </tr>
    </tbody>
    </table>
    </div>
    <div class="hidden">
        我的下级
        <%=sbUnderling %>
    </div>
    </form>
</body>
</html>


