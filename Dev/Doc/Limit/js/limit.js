jQuery(function () {
    jQuery(".bodystyle td").hover(function () {
        $(this).parent().css("background-color", "#00BFFF");
    }
    , function () {
        $(this).parent().css("background-color", "white");
    });

    jQuery("#TContent").click(function (event) { });
});
//显示隐藏接点
//参数：图片对象，子节点层对象
function ShowHide(oImg, wbs) {
    var curnode = LimitData.find("tn[id=\"" + wbs + "\"]");
    if (oImg.src.indexOf("plus.gif") != -1) {
        oImg.src = oImg.src.replace("plus.gif", "minus.gif");
        var curtr = $("#tr" + wbs);
        curtr.attr("state", "open");
        opentree(curnode);
    }
    else {
        oImg.src = oImg.src.replace("minus.gif", "plus.gif");
        var curtr = $("#tr" + wbs);
        curtr.attr("state", "close");
        colsetree(curnode);
    }
}

function opentree(curnode) {
    var children = curnode.children();
    for (var i = 0; i < children.length; i++) {
        var sonnode = children.eq(i);
        var sonwbs = sonnode.attr("id");
        var sontr = $("#tr" + sonwbs);
        var sonnum = sontr.attr("sonnum");
        sontr.show();

        if (sontr.attr("state") == "open" && sonnum > 0) {
            opentree(sonnode);
        }
    }

}

function colsetree(curnode) {
    var children = curnode.children();
    for (var i = 0; i < children.length; i++) {
        var sonnode = children.eq(i);

        var sonwbs = sonnode.attr("id");
        var sontr = $("#tr" + sonwbs);
        var sonnum = sontr.attr("sonnum");
        sontr.hide();

        if (sontr.attr("state") == "open" && sonnum > 0) {
            colsetree(sonnode);
        }
    }
}

function chklimit(objwbs, prefix) {
    var chkRel = document.getElementById("chkrelation").checked;
    curval = document.getElementById(prefix + objwbs).checked;
    curvalint = curval == true ? "1" : "0";
    var bitp = parseInt(prefix.substr(3, 4));
    var curnode = LimitData.find("tn[id=" + objwbs + "]");
    if (curnode) //如果找到
    {
        if (chkRel) //级联
        {
            setchildren(curnode, prefix);
        }

        var rights = curnode.attr("limit");
        rights = rights.substring(0, bitp - 1) + curvalint + rights.substr(bitp);

        curnode.attr("limit", rights);
        curnode.attr("EditFlag", "1");

    }
    else    //如果找不到
    {
        alert("找不到对应的XML数据");
    }
}

function setchildren(xmlnode, prefix) {
    var bitp = parseInt(prefix.substr(3, 4));
    var children = xmlnode.children();
    for (var i = 0; i < children.length; i++) {
        var sunnode = children.eq(i);
        var objwbs = sunnode.attr("id");
        rights = sunnode.attr("limit");
        rights = rights.substring(0, bitp - 1) + curvalint + rights.substr(bitp);

        sunnode.attr("limit", rights);
        sunnode.attr("EditFlag", "1");
        $("#" + prefix + objwbs).attr("checked", curval);
        setchildren(sunnode, prefix);
    }
}

function selrow(objwbs) {
    var chkRel = document.getElementById("chkrelation").checked;
    curval = document.getElementById("chk0" + objwbs).checked;
    curvalint = curval == true ? "1" : "0";
    var curnode = LimitData.find("tn[id=" + objwbs + "]");
    if (curnode) //如果找到
    {
        var curlist = document.getElementsByName("chk" + objwbs);
        curlist[0].checked = curval;
        curlist[1].checked = curval;
        curlist[2].checked = curval;
        curlist[3].checked = curval;
        curlist[4].checked = curval;
        curlist[5].checked = curval;
        curlist[6].checked = curval;
        curlist[7].checked = curval;
        curvalrow = curval == true ? "1111111111" : "0000000000";
        curnode.attr("limit", curvalrow);
        curnode.attr("EditFlag", "1");

        if (chkRel)//级联
        {
            setchildrenrow(curnode);
        }

    }
    else {
        alert("找不到对应的XML数据");
    }
}

function setchildrenrow(xmlnode) {
    var soncount = xmlnode.children().length;
    for (var i = 0; i < soncount; i++) {
        var sunnode = xmlnode.children().eq(i);
        var objwbs = sunnode.attr("id");
        sunnode.attr("limit", curvalrow);
        sunnode.attr("EditFlag", "1");

        var curlist = document.getElementsByName("chk" + objwbs);

        curlist[0].checked = curval;
        curlist[1].checked = curval;
        curlist[2].checked = curval;
        curlist[3].checked = curval;
        curlist[4].checked = curval;
        curlist[5].checked = curval;
        curlist[6].checked = curval;
        curlist[7].checked = curval;

        setchildrenrow(sunnode);
    }
}

function loadlimit(LimitData) {
    setcheckbox(LimitData.documentElement);
}

function setcheckbox(xmlnode) {
    var soncount = xmlnode.childNodes.length;
    for (var i = 0; i < soncount; i++) {
        var sunnode = xmlnode.childNodes[i];
        var objwbs = sunnode.attributes.getNamedItem("id").text;
        var limit = sunnode.attributes.getNamedItem("limit").text;

        var curlist = document.getElementsByName("chk" + objwbs);

        curlist[1].checked = (limit.substr(0, 1) == "1");
        curlist[2].checked = (limit.substr(1, 1) == "1");
        curlist[3].checked = (limit.substr(2, 1) == "1");
        curlist[4].checked = (limit.substr(3, 1) == "1");
        curlist[5].checked = (limit.substr(4, 1) == "1");
        curlist[6].checked = (limit.substr(5, 1) == "1");
        curlist[7].checked = (limit.substr(6, 1) == "1");
        setcheckbox(sunnode);

    }
}

function tableclick() {
    if (event.srcElement.tagName == "INPUT" && event.srcElement.type == "checkbox") {
        var objid = event.srcElement.id;
        var objwbs = objid.substr(4);
        var prefix = event.srcElement.id.substr(0, 4);

        switch (prefix) {
            case "chk0":
                selrow(objwbs);
                break;
            case "chk1":
                chklimit(objwbs, prefix);
                break;
            case "chk2":
                chklimit(objwbs, prefix);
                break;
            case "chk3":
                chklimit(objwbs, prefix);
                break;
            case "chk4":
                chklimit(objwbs, prefix);
                break;
            case "chk5":
                chklimit(objwbs, prefix);
                break; 
            case "chk6":
                chklimit(objwbs, prefix);
                break;
            case "chk7":
                chklimit(objwbs, prefix);
                break;
            default:
                break;
        }
    }

}

//序列化
function _serializeToString(objXML) {
    if (window.XMLSerializer) {
        return (new XMLSerializer()).serializeToString(objXML[0])
    }
    else {
        return objXML[0].xml;
    }
}