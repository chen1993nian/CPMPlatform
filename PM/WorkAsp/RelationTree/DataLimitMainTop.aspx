<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataLimitMainTop.aspx.cs" Inherits="EIS.WorkAsp.DataLimit.WorkAsp.RelationTree.DataLimitMainTop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>数据范围授权</title>
    <script type="text/javascript" src="../../js/jquery-1.7.js"></script>
    <style>
        body {
            margin:0px;
        }

        .toolbar {
            width: 100%;
            height: 45px;
            margin-left: 5px;
        }

            .toolbar .barBox {
                float: left;
                margin-right: 5px;
                height: 45px;
                width: 180px;
                line-height:45px;
                text-align:center;
                background-color: #ababab;
                color: #4e4e4e;
                border-top-left-radius:5px;
                border-top-right-radius:5px;
            }

            .toolbar .ActiveBox {
                background-color: lightblue;
                color: white;
            }


    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".barBox").click(function() {
                $(".barBox").removeClass("ActiveBox");
                $(this).addClass("ActiveBox");
                var url = $(this).attr("data-url");
                window.open(url, "DataLimitContents");
            });
        });
    </script>

</head>
<body>

    <form id="form1" runat="server">
        <div class="toolbar">
            <div class="barBox" data-url="RelationMain.aspx?RelationID=BB4D6FF4-9711-4357-A521-AF6B6613F38A">人员数据授权</div>
            <div class="barBox" data-url="RelationMain.aspx?RelationID=C2661DED-2FBB-47CF-8269-F63D5E3C5D58">角色数据授权</div>
            <div class="barBox" data-url="RelationMain.aspx?RelationID=1E5E6448-21D7-4D3F-A5CC-4E4B306FD687">岗位数据授权</div>
            <div class="barBox ActiveBox" data-url="RelationMain.aspx?RelationID=4FBE5F3B-949C-4D34-9BB9-E7F22E333564">部门数据授权</div>
        </div>
    </form>
</body>
</html>
