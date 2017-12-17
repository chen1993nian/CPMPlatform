<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jAppDetail.aspx.cs" Inherits="EIS.Web.AppLogic.SysFolder.AppFrame.jAppDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">

        function ShowDialogSelf() {
            var _url = "<%=crypt_url%>";
            if (_url != "") {
                window.open(_url, "_self");
            }
        }

        window.setTimeout("ShowDialogSelf()", 1000);


    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center; vertical-align:central;">
        <img src="../../Img/common/loading.gif" alt="正在加载..." />
    </div>
    </form>
</body>
</html>
