        function GetCommentListHTML(isadd) {
            var clearCaches = new Date().getTime();
            var gridparms = [];
            gridparms.push({ name: "rnd", value: clearCaches });
            gridparms.push({ name: "tblname", value: _mainTblName });
            gridparms.push({ name: "autoid", value: _mainId });
            if (isadd == 1) {
                //提交保存评论
                gridparms.push({ name: "CommentContent", value: document.getElementById("txtCommentContent").value });
            }
            $.ajax({
                type: "post",
                url: "AppDetailComments.ashx",
                data: gridparms,
                dataType: "html",
                success: function (strhtml) {
                    document.getElementById("div_commentlist").innerHTML = strhtml;
                    //微信及平台提醒记录录入人员
                    if (isadd == 1) SendMessageAppDetail();
                }
            });
        }

        function SendMessageAppDetail() {
            var clearCaches = new Date().getTime();
            var gridparms = [];
            gridparms.push({ name: "rnd", value: clearCaches });
            gridparms.push({ name: "tblname", value: _mainTblName });
            gridparms.push({ name: "autoid", value: _mainId });
            gridparms.push({ name: "CommentContent", value: document.getElementById("txtCommentContent").value });
            $.ajax({
                type: "post",
                url: "AppDetailCommentsSendMessage.ashx",
                data: gridparms,
                dataType: "html",
                success: function (strhtml) {
                    
                }
            });
        }

