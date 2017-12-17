<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectWorkFlow.aspx.cs" Inherits="EIS.WebBase.SysFolder.WorkFlow.SelectWorkFlow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择发起流程</title>
    <link href="../../Css/defStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        
        input,button{
            padding:3px;
            }
            select{font-size:14px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding:30px;">
        <table>
            <tr>
                <td height="40"><h3>请选择下面的流程</h3></td>
                <td align="right">                    <asp:Button ID="Button1" runat="server" Text="发 起" onclick="Button1_Click" />&nbsp;&nbsp;
                    <button value="关 闭" onclick="javascript:window.close();" >关 闭</button></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:ListBox ID="ListBox1" runat="server" Width="600" Rows="10"></asp:ListBox>
                
                </td>
            </tr>
            <tr>
                <td height="25" colspan="2"><h4 style="color:Red;margin-top:5px;"><%=Infos %></h4></td>
            </tr>

        </table>
    
    </div>
    </form>
</body>
</html>
