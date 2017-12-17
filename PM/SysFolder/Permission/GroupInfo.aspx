<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupInfo.aspx.cs" Inherits="EIS.Web.SysFolder.Permission.GroupInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>集团信息维护</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <link type="text/css" rel="stylesheet" href="../../Css/appInput.css" />
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>

    <link href="../../ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="../../ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>

    <style type="text/css">
        table{border-color:gray;}
        td{padding:5px;border-color:gray;}
        caption{font-size:14px;font-weight:bold;height:30px;border-width:0px;}
        body{background:white;}
        .ErrorMsg{width:600px;}
    </style>
    <script type="text/javascript">



        $(document).ready(function () {
            $(".Read").attr("readonly", true);
            var validator = $("#form1").validate({
                rules: {
                    txtDeptName: "required",
                    txtDeptCode: "required",
                    txtDeptAbbr: "required",
                    txtDeptSa: "required"
                }
            });
            $("#LinkButton1").click(function () {
                return $("#form1").valid();
            });
        });
        function clo() {

            window.parent.win1.close();

        }
    </script>
</head>
<body>
    <form runat="server" id="form1">
 
    <!-- 工具栏 -->
    <div class="menubar">
        <div class="topnav">
            <ul>
                <li>
                    <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">保存</asp:LinkButton>
                </li>
                <li>
                    <a href="javascript:" onclick="clo();" >关闭</a> 
                </li>
            </ul>
        </div>
    </div>
    
    <div >
    <%=TipMessage %>
    <br />
    <table class="normaltbl"  border="1" style="width:600px"  align="center">
    <caption>集团信息维护</caption>
    <tbody>
      <tr>
        <td  width="100">集团名称</td>
        <td  >
            <asp:TextBox ID="txtDeptName" Width="380px" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
            &nbsp;<span class='RequiredStar'>*</span>
        </td>
    </tr>
    <tr>
        <td  width="18%">集团编码</td>
        <td >
            <asp:TextBox ID="txtDeptCode" Width="380px"  CssClass="TextBoxInChar" runat="server"></asp:TextBox>
            &nbsp;<span class='RequiredStar'>*</span>
         </td>
      </tr>
      <tr>
        <td>集团简称</td>
        <td>
            <asp:TextBox ID="txtDeptAbbr" Width="380px"  CssClass="TextBoxInChar" runat="server"></asp:TextBox>
            &nbsp;<span class='RequiredStar'>*</span>
        </td>
        </tr>
        <tr style="display:none;">
        <td>集团管理员账号</td>
        <td>
            <asp:TextBox ID="txtDeptSa" Width="380px"  CssClass="TextBoxInChar" runat="server"></asp:TextBox>
            &nbsp;<span class='RequiredStar'>*</span>
        </td>
      </tr> 
      <tr>
        <td>集团描述</td>
        <td>
        <asp:TextBox ID="txtDeptNote" CssClass="TextBoxInArea" runat="server" Rows="6" TextMode="MultiLine"></asp:TextBox>
        </td>
      </tr>
    </tbody>
    </table>
    </div>
    </form>
</body>
</html>

