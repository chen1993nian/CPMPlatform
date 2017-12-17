<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jApp.aspx.cs" Inherits="EIS.Web.AppLogic.SysFolder.AppFrame.jApp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
