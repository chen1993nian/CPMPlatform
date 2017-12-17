<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FunLimitQuery.aspx.cs" Inherits="EIS.Web.Doc.Limit.FunLimitQuery" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>授权设置</title>
    <meta content="no-cache" http-equiv="Pragma"/>
    <link href="../../Css/AppStyle.css" rel="stylesheet" type="text/css" />
    <script src="../../Js/jquery-1.7.min.js" type="text/javascript"></script>
    <script src="Js/limit.js" type="text/javascript"></script>
    <style type="text/css">
    a{text-decoration:none;}
	a:hover{text-decoration:none;}
    body
    {
	    font-size: 12px;
	    font-family: "Verdana", "Arial", "宋体", "sans-serif";
	    background:#ffffff;
	    color: #606060;
	    width:100%;
	    height:100%;
	
    }
    /* 容器,主要控件放这里面*/
    .ViewGridContain
    {
	    border:0;
	    position:relative;
	    background-color:#ffffff;
        margin:5px;/*180px;*/
	    width:700px;
	    padding:10px;
	
    }
    .table_title
    {
    	height:26px;
    	background:#efefef;
    }
    input[type=checkbox]{
    	margin-left:5px;
    }
    table.bodystyle{
        table-layout:fixed;
        border-collapse:collapse;
        border:1px solid #a0a0a0;
        }
     table.bodystyle td{
         padding:0px;
         text-align:center;height:20px;
         border:1px solid #a0a0a0;
         }
    table.bodystyle td.nodetd{
        text-align:left;
        border-top-width:0px;
        border-bottom-width:0px;
        }
	.nodetd input{margin-left:0px;}
	.pos{color:Red;}
	input[type=button]{padding:3px 8px;height:28px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">   
            <div class="ViewGridContain">
                <div id="TContent">
                    <table id="" class="bodystyle"  width="100%" border="1" >
                    <thead>
                        <tr class="table_title">
                            <th style="text-align:left;border:1px solid #a0a0a0;" width="320">&nbsp;&nbsp;员工姓名</th>
                            <th >浏览</th>
                            <th >新建</th>
                            <th >上传</th>
                            <th >删除</th>
                            <th >管理</th>
                            <th >备用</th>
                            <th >备用</th>
                        </tr>
                    </thead>
                    <tbody>
                    <%=strTreeHtml%>
                    </tbody>
                    </table>
                
                </div>
            </div>

    </form>
</body>
</html>


