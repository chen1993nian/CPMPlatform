<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupInfo.aspx.cs" Inherits="EIS.Studio.SysFolder.Permission.GroupInfo" %>
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>集团信息维护</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <link type="text/css" rel="stylesheet" href="../../Css/appInput.css" />
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/lhgdialog.min.js"></script>
    <script type="text/javascript" src="../../help/helpRefer.js"></script>


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
                    &nbsp;&nbsp;
                        <a class="winHelpLinkBtn" data-hlpUrl="Help/HelpGroupCompanyList.aspx#a2" data-hlpTitle="集团信息维护帮助" href="javascript:void(0);">帮助</a>
                    &nbsp;&nbsp;
                </li>
               <%-- <li>
                    <a href="javascript:" onclick="window.close();" >关闭</a> 
                </li>--%>
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
        <tr>
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

