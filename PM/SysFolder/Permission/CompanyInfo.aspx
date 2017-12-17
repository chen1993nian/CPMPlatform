<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyInfo.aspx.cs" Inherits="EIS.Web.SysFolder.Permission.CompanyInfo" %>
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>公司信息维护</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <link type="text/css" rel="stylesheet" href="../../Css/appInput.css" />
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>

    <style type="text/css">
        table{border-color:gray;width:500px;}
        td{padding:5px;border-color:gray;}
        caption{font-size:14px;font-weight:bold;height:30px;border-width:0px;}
        body{background:white;}
        input[type=text]{width:280px;}
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".Read").attr("readonly", true);
            var validator = $("#form1").validate({
                rules: {
                    txtDeptName: "required",
                    txtDeptCode: "required",
                    txtDeptAbbr: "required",
                    txtOrder: "required",
                    txtDeptSa: "required"
                }
            });
            $("#LinkButton1").click(function () {
                return $("#form1").valid();
            });
        });
        function notFind() {
            alert("找不到对应的公司");
            window.location = "../../welcome.htm";
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
            </ul>
        </div>
    </div>
    
    <div >
    <%=TipMessage %>
    <table class="normaltbl"  border="1"   align="center">
    <caption>单位信息</caption>
    <tbody>
      <tr>
        <td  width="120"><span class='RequiredStar'>*</span>单位名称</td>
        <td  >
            <asp:TextBox ID="txtDeptName" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td ><span class='RequiredStar'>*</span>单位编码</td>
        <td >
            <asp:TextBox ID="txtDeptCode" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
         </td>
      </tr>
      <tr>
        <td><span class='RequiredStar'>*</span>单位简称</td>
        <td>
            <asp:TextBox ID="txtDeptAbbr" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
        </td>
        </tr>
              <tr>
        <td><span class='RequiredStar'>*</span>单位类型</td>
        <td>
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
        </td>
        </tr>
              <tr>
        <td><span class='RequiredStar'>*</span>同级排序</td>
        <td>
            <asp:TextBox ID="txtOrder" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td><span class='RequiredStar'>*</span>单位管理员账号</td>
        <td>
            <asp:TextBox ID="txtDeptSa" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
        </td>
      </tr> 
      <tr>
        <td>单位描述</td>
        <td>
        <asp:TextBox ID="txtDeptNote" CssClass="TextBoxInArea" runat="server" Rows="6" TextMode="MultiLine"></asp:TextBox>
        </td>
      </tr>


        <tr>
        <td><span class='RequiredStar'>*</span>上级单位</td>
        <td>
            <asp:TextBox ID="txtParentName" CssClass="TextBoxInChar Read" runat="server"></asp:TextBox>
        </td>
        </tr>
    </tbody>
    </table>
    </div>
    </form>
</body>
</html>

