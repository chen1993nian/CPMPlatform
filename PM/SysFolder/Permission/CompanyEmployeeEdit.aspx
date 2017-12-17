

<<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyEmployeeEdit.aspx.cs" Inherits="EIS.Web.SysFolder.Permission.CompanyEmployeeEdit" %>
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>员工信息</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <link type="text/css" rel="stylesheet" href="../../Css/appInput.css" />
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <script type="text/javascript" src="../../js/tools.js"></script>

    <style type="text/css">
        table.normaltbl {border-color:gray;width:700px;}
        body{background:white;}
        input[type=text]{width:220px;}
        #selPosition{float:left;margin-right:10px;}
        #txtNewPos{background-image:url();background-color:#fff280;}
        .topnav a{color:blue;text-decoration:none;font-size:10pt;}
        .topnav a:hover{color:red;text-decoration:none;font-size:10pt;}
        .tip{width:750px;}
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#selPosition").change(function () {
                var posId = $(this).val();
                if (posId == "") {
                    $("#txtNewPos").show();
                }
                else {
                    $("#txtNewPos").hide();
                }
            }).change();
            $(".Read").attr("readonly", true);
            $(".Wdate").focus(function () {
                WdatePicker({ isShowClear: false, dateFmt: 'yyyy-MM-dd' });
            });
            var validator = $("#form1").validate({
                rules: {
                    txtEmpName: "required",
                    txtOrder: "required",
                    txtLoginName: "required"
                }
            });
            $("#LinkButton1,#LinkButton2").click(function () {
                return $("#form1").valid();
            });
            $("#initPass").click(function () {
                var empId = "<%=EditEmployeeId %>";
                if (empId) {
                    var ret = EIS.Web.SysFolder.Permission.CompanyEmployeeEdit.InitialPassWord(empId);
                    if (ret.error) {
                        alert("初始化过程中出现错误:" + ret.error.Message);
                    }
                    else {
                        alert("已经把密码成功初始化");
                    }
                }
            });
        });
        function notFind() {
            alert("找不到对应的公司");
            window.location = "../../welcome.htm";
        }
        function returnList() {
            window.open("<%=UrlRefer %>", "_self");
        }
    </script>
</head>
<body>
    <form runat="server" id="form1">
    <!-- 工具栏 -->
    <div class="menubar">
        <div class="topnav">
            <span style="float:right;margin-right:30px;">
            <a href="javascript:" onclick="returnList()" >【返回列表】</a> &nbsp;
            <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click">【保存后新增】</asp:LinkButton>&nbsp;
            <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">【保存】</asp:LinkButton>&nbsp;&nbsp;
            </span>
        </div>
    </div>
    
    <div  id="maindiv">
        <%=TipMessage %>
    <table class='normaltbl'  border="1"   align="center">
    <caption>员工基本信息</caption>
    <tbody>
      <tr>
        <td  width="100" >&nbsp;员工姓名</td>
        <td  >
            <asp:TextBox ID="txtEmpName" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
            <span class='RequiredStar'>&nbsp;*</span>
        </td>
        <td   width="100">&nbsp;员工编号</td>
        <td  >
            <asp:TextBox ID="txtEmpCode" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td >&nbsp;性别</td>
        <td class="style1" >
            <asp:RadioButtonList ID="selSex" runat="server" 
                RepeatDirection="Horizontal" RepeatLayout="Flow" Width="193px">
                <asp:ListItem>男</asp:ListItem>
                <asp:ListItem>女</asp:ListItem>
            </asp:RadioButtonList>
         </td>
        <td >&nbsp;出生日期
            &nbsp;</td>
        <td >
            <asp:TextBox ID="txtBirthDay" CssClass="Wdate TextBoxInDate" runat="server" 
                MaxLength="10" ToolTip="只能输入如yyyy-mm-dd格式的日期字符串"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td>&nbsp;排序号</td>
        <td >
            <asp:TextBox ID="txtOrder" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
            <span class='RequiredStar'>&nbsp;*</span>
        </td>
        <td>
            &nbsp;办公电话</td>
        <td>
            <asp:TextBox ID="txtOfficePhone" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
          </td>
        </tr>
              <tr>
        <td>&nbsp;电子邮件</td>
        <td >
            <asp:TextBox ID="txtEmail" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
        </td>
        <td>
            &nbsp;移动电话</td>
        <td>
            <asp:TextBox ID="txtMobile" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
                  </td>
        </tr>
        <tr>
        <td>&nbsp;岗位名称</td>
        <td >
            <asp:DropDownList ID="selPosition" runat="server">
            </asp:DropDownList>
            <asp:TextBox ID="txtNewPos" Width="120" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
            <span class='RequiredStar'>&nbsp;*</span>
        </td>
        <td>
            &nbsp;所属部门</td>
        <td>
            <asp:TextBox ID="txtDeptName" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td>&nbsp;人员类别</td>
        <td >
            <asp:RadioButtonList ID="selType" runat="server" 
                RepeatDirection="Horizontal" RepeatLayout="Flow" Width="193px">
                <asp:ListItem>正式</asp:ListItem>
                <asp:ListItem>非正式</asp:ListItem>
                <asp:ListItem>外部人员</asp:ListItem>
            </asp:RadioButtonList>
            <span class='RequiredStar'>&nbsp;*</span>
        </td>
        <td>
            &nbsp;在岗状态</td>
        <td>
            <asp:RadioButtonList ID="selState" runat="server" 
                RepeatDirection="Horizontal" RepeatLayout="Flow" Width="193px">
                <asp:ListItem>在职</asp:ListItem>
                <asp:ListItem>离职</asp:ListItem>
                <asp:ListItem>退休</asp:ListItem>
                <asp:ListItem>其它</asp:ListItem>
            </asp:RadioButtonList>
            </td>
      </tr> 
      <tr>
        <td>&nbsp;登录帐号</td>
        <td >
        <asp:TextBox ID="txtLoginName" CssClass="TextBoxInChar" runat="server" Width="220px"></asp:TextBox>
        <span class='RequiredStar'>&nbsp;*</span>
        </td>
        <td>
            &nbsp;登录密码</td>
        <td>
            <input type="button" value="初始化密码" id="initPass"/>
            </td>
      </tr>

        <tr>
        <td>&nbsp;是否锁定</td>
        <td class="style1">
            <asp:RadioButtonList ID="selLock" runat="server" 
                RepeatDirection="Horizontal" RepeatLayout="Flow" Width="193px">
                <asp:ListItem>是</asp:ListItem>
                <asp:ListItem>否</asp:ListItem>
            </asp:RadioButtonList>
            <span class='RequiredStar'>&nbsp;*</span>
        </td>
        <td>
            &nbsp;锁定原因</td>
        <td>
            <asp:TextBox ID="txtLockReason" CssClass="TextBoxInChar Read" runat="server"></asp:TextBox>
            </td>
        </tr>

        <tr>
        <td>&nbsp;通讯录</td>
        <td >
            <asp:RadioButtonList ID="selOutList" runat="server" 
                RepeatDirection="Horizontal" RepeatLayout="Flow" Width="193px">
                <asp:ListItem Value="是">不可见</asp:ListItem>
                <asp:ListItem Value="否">可见</asp:ListItem>
            </asp:RadioButtonList>
        </td>
       <td>&nbsp;员工级别</td>
        <td>
              <asp:DropDownList ID="ddljibie" runat="server" Height="50px">
                  <asp:ListItem Value="集团级">集团级</asp:ListItem>
                  <asp:ListItem  Value="公司级">公司级</asp:ListItem>
                  <asp:ListItem  Value="项目级">项目级</asp:ListItem>
                  <asp:ListItem  Value="管理员">管理员</asp:ListItem>
                  <asp:ListItem  Value="教师">教师</asp:ListItem>
                  <asp:ListItem  Value="学生">学生</asp:ListItem>
              </asp:DropDownList>
                <span class='RequiredStar'>&nbsp;*</span>
        </td>
      </tr> 
    </tbody>
    </table>
    </div>
    </form>
</body>
</html>

