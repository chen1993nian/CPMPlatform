<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveyMain.aspx.cs" Inherits="EIS.Web.WorkAsp.Survey.SurveyMain" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>【问卷调查】<%=surveyTitle %></title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../../css/password.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../js/tools.js"></script>
    <style type="text/css">
        #maindiv{width:960px;margin-left:auto;margin-right:auto;}
        .header{
            height:100px;
            background:#e6edf5 url(../../img/vote/vote_banner_bg.jpg) no-repeat;
            }
        .center{
            border:1px solid #d6ddef;
            background-color:White;
            padding-bottom:20px;
            text-align:left;
            }
        .subject{
                 line-height:200%;
                 text-align:left;
                 padding:20px 40px;
                 }
        .subject h3{font-weight:bold;font-size:16px;font-family:"宋体",Tahoma,sans-serif;line-height:200%;
                 background:#fefefe url(../../img/common/comments.png) no-repeat left center;
                    padding-left:25px;
                 }
        .subject p{font-size:12px;font-family:"宋体",Tahoma,sans-serif;line-height:200%;
                   padding-left:25px;
                   color:#666;
                   }
        .footer{border:1px solid #d6ddef;height:40px;padding-top:8px;padding-left:60px;text-align:left;}
        .imgbtn{background:url(../../img/common/btn_white_big.png) no-repeat;
            height:32px;line-height:28px;
            width:94px;
            border:0px solid white;
            cursor:pointer;
            color:Green;
            font-weight:bold;
            }
         .qlist{}
         .question{padding-left:60px;margin-top:10px;}
         .question h3{text-align:left;line-height:200%;font-size:14px;color:#444;}
         .question h3 span{color:red;line-height:200%;}
         .question .list{
             border-top:1px solid #dfdfdf;
             }
         .item{line-height:28px;}
         .item label{line-height:28px;cursor:pointer;padding-left:10px;}
         .item textarea{line-height:20px;margin-top:5px;width:460px;}
        input[type=radio]{vertical-align:middle;cursor:pointer;}
        input[type=checkbox]{vertical-align:middle;cursor:pointer;}
        .active{background-color:#e3f8ff;}
        input[type=text]{padding:2px;line-height:22px;height:20px;font-size:12px;color:#444;border:1px solid #bbb;}
        textarea{padding:2px;line-height:22px;font-size:12px;color:#444;border:1px solid #bbb;}
        .item .iother{margin-left:3px;border-width:0px;border-bottom-width:1px;line-height:18px;height:18px;color:Blue;} 
        .tip a{color:Blue;text-decoration:none;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="maindiv">
        <div class="header">
        
        </div>
        <%=Tips%>
        <div class="center">
            <div class="subject">
                <h3>
                    <%=surveyTitle%>
                </h3>
                <p>
                    <%=surveyMemo%>                
                </p>
            </div>
            <div class="qlist">
            <%=listItems%>
            </div>
        </div>
        <div class="footer">
            <button type="submit" id="btnsubmit" class="imgbtn <%=btnClass %>">提交问卷</button>
        </div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    jQuery(function () {
        $(".question").click(function () {
            $(".active").removeClass("active");
            $(this).addClass("active");
        });
        $("#btnsubmit").click(function () {
            var flag = true;
            $(".question").each(function (i, n) {
                var arr = $(this).attr("p").split("|");
                var t = $("h3", this).text();
                if (arr[3] == "1") {
                    if (arr[1] == "单选" || arr[1] == "多选") {
                        var vs = _getGroupValueByName("input" + arr[2]);
                        if (vs.length == 0) {
                            alert("【" + t + "】必选");
                            flag = false;
                            return false;
                        }
                    }
                    else {
                        var ctlArr = document.getElementsByName("input" + arr[2]);
                        if (ctlArr.length > 0) {
                            if (ctlArr[0].value == "") {
                                alert("【" + t + "】必填");
                                flag = false;
                                return false;
                            }
                        }
                    }
                }
            });
            return flag;
        });
    });
    //返回Radio或者CheckBox的值（数组）
    function _getGroupValueByName(ctlName) {
        var ctlArr = document.getElementsByName(ctlName);
        var ctlval = [];
        for (var i = 0; i < ctlArr.length; i++) {
            if (ctlArr[i].checked)
                ctlval.push(ctlArr[i].value);
        }
        return ctlval;
    }
</script>