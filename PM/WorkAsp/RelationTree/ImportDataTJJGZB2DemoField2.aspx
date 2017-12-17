<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportDataTJJGZB2DemoField2.aspx.cs" Inherits="EIS.Web.ImportDataTJJGZB.ImportDataTJJGZB2DemoField2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../style.css" type="text/css" rel="stylesheet"/>
    <script language="javascript" type="text/javascript">
        function OpenDataView() {
            var url = "ImportDataTJJGZB2DemoField3.aspx?TableName=<%=TableName %>&dbname=tjjgzbdb20140106";
            window.open(url, "RelationMainTB");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input id="Button1" type="button" value="预览数据" onclick="OpenDataView();" /><br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="FieldName" HeaderText="字段名称" />
                <asp:BoundField DataField="FieldNameCn" HeaderText="中文名称" />
            </Columns>
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
