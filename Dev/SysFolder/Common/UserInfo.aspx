<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="EIS.WebBase.SysFolder.Common.UserInfo" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>员工信息</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <link type="text/css" rel="stylesheet" href="../../Css/appInput.css" />
    <style type="text/css">
        table{border-color:gray;width:600px;}
        .normaltbl tbody tr td{padding:3px;border-color:gray;}
        body{background:white;overflow:auto;}
        .tip{width:746px;}
		ul.posul{margin-left:2em;}
		ul.posul li{list-style-type:decimal;margin:3px;}
    </style>

</head>
<body>
    <form runat="server" id="form1">
    <!-- 工具栏 -->
    <div class="menubar">
        <div class="topnav">
                    <span style="float:right;margin-right:20px;">
					<a href="javascript:" onclick="window.close();" >关闭</a> 
                    </span>
        </div>
    </div>
    
    <div  id="maindiv">
        <%=TipMessage %>
    <table class='normaltbl'  border="1"   align="center">
    <caption>员工基本信息</caption>
    <tbody>
      <tr>
        <td  width="100">&nbsp;员工姓名</td>
        <td >
            <%=model.EmployeeName%>
        </td>
        <td   width="100">&nbsp;员工编号</td>
        <td >
            <%=model.EmployeeCode%>
        </td>
    </tr>
    <tr>
        <td >&nbsp;性别</td>
        <td class="style1" >
              <%=model.Sex%>
         </td>
        <td >&nbsp;出生日期
            &nbsp;</td>
        <td >
            <%=Birthday%>
        </td>
      </tr>
              <tr>
        <td>&nbsp;电子邮件</td>
        <td >
            <%=model.EMail%>
        </td>
        <td>
            &nbsp;移动电话</td>
        <td>
            <%=model.Cellphone%>
                  </td>
        </tr>
        <tr>
            <td>&nbsp;家庭电话</td>
            <td >
                <%=model.Homephone%>
            </td>
            <td>
                &nbsp;办公电话</td>
            <td>
            <%=model.Officephone%>
          </td>
        </tr>
        <tr>
            <td>&nbsp;身份证号</td>
            <td >
                <%=model.IdCard%>
            </td>
            <td>&nbsp;邮 编</td>
            <td >
                <%=model.ZipCode%>
            </td>
        </tr>
            <tr>
            <td>&nbsp;邮寄地址</td>
            <td colspan="3">
                <%=model.Address%>
            </td>
        </tr>
        <tr>
            <td>&nbsp;岗位信息</td>
            <td colspan="3">
				<ul class='posul'>
                <%=sbPos%>
				</ul>
            </td>
        </tr>
    </tbody>
    </table>
    </div>
    </form>
</body>
</html>


