/// <reference path="../intellisense/jquery-1.2.6-vsdoc-cn.js" />
/****************************************
data:[{
id:1, //ID只能包含英文数字下划线中划线
text:"node 1",
value:"1",
showcheck:false,
checkstate:0,         //0,1,2
hasChildren:true,
isexpand:false,
complete:false, 是否已加载子节点
ChildNodes:[] // child nodes
},
..........
]
author:xuanye.wan@gmail.com
***************************************/
(function ($) {
    //去掉c1,添加c2样式
    $.fn.swapClass = function (c1, c2) {
        return this.removeClass(c1).addClass(c2);
    }
    //交换c1和c2样式
    $.fn.switchClass = function (c1, c2) {
        if (this.hasClass(c1)) {
            return this.swapClass(c1, c2);
        }
        else {
            return this.swapClass(c2, c1);
        }
    };
    $.fn.treeview = function (settings) {
        var dfop =
            {
                method: "POST",
                datatype: "json",
                url: false,
                blankpath: "/images/icons/",
                cbiconpath: "/images/icons/",
                icons: ["checkbox_0.gif", "checkbox_1.gif", "checkbox_2.gif"],
                showcheck: false, //是否显示选择            
                oncheckboxclick: false, //当checkstate状态变化时所触发的事件，但是不会触发因级联选择而引起的变化
                aftercheck: false,
                onnodeclick: false,
                cascadecheck: true,
                dataload: false, //当节点数据加载完成，还没有呈现之前
                data: null,
                clicktoggle: true, //点击节点展开和收缩子节点
                theme: "bbit-tree-arrows" //bbit-tree-lines ,bbit-tree-no-lines,bbit-tree-arrows
            };

        $.extend(dfop, settings);
        var treenodes = dfop.data;
        var me = $(this);
        var id = me.attr("id");
        if (id == null || id == "") {
            id = "bbtree" + new Date().getTime();
            me.attr("id", id);
        }

        var html = [];
        buildtree(dfop.data, html);
        me.addClass("bbit-tree").html(html.join(""));
        InitEvent(me);
        html = null;
        //预加载图片
        if (dfop.showcheck) {
            for (var i = 0; i < 3; i++) {
                var im = new Image();
                im.src = dfop.cbiconpath + dfop.icons[i];
            }
        }

        //region 
        function buildtree(data, ht) {
            ht.push("<div class='bbit-tree-bwrap'>"); // Wrap ;
            ht.push("<div class='bbit-tree-body'>"); // body ;
            ht.push("<ul class='bbit-tree-root ", dfop.theme, "'>"); //root
            if (data && data.length > 0) {
                var l = data.length;
                for (var i = 0; i < l; i++) {
                    buildnode(data[i], ht, 0, i, i == l - 1);
                }
            }
            else {
                asnyloadc(null, false, function (data) {
                    if (data && data.length > 0) {
                        if (dfop.dataload)
                            dfop.dataload(data);
                        treenodes = data;
                        dfop.data = data;
                        var l = data.length;
                        for (var i = 0; i < l; i++) {
                            buildnode(data[i], ht, 0, i, i == l - 1);
                        }
                    }
                });
            }
            ht.push("</ul>"); // root and;
            ht.push("</div>"); // body end;
            ht.push("</div>"); // Wrap end;
        }
        //endregion
        function buildnode(nd, ht, deep, path, isend) {
            if (nd.isdel)
                return;
            var nid = nd.id.replace(/[^\w]/gi, "_");
            ht.push("<li class='bbit-tree-node'>");
            ht.push("<div id='", id, "_", nid, "' tpath='", path, "' unselectable='on' title='", nd.text, "'");
            nd.path = path;
            var cs = [];
            cs.push("bbit-tree-node-el");
            if (nd.hasChildren) {
                cs.push(nd.isexpand ? "bbit-tree-node-expanded" : "bbit-tree-node-collapsed");
            }
            else {
                cs.push("bbit-tree-node-leaf");
            }
            if (nd.classes) { cs.push(nd.classes); }

            ht.push(" class='", cs.join(" "), "'>");
            //span indent
            ht.push("<span class='bbit-tree-node-indent'>");
            if (deep == 1) {
                ht.push("<img class='bbit-tree-icon' src='" + dfop.blankpath + "s.gif'/>");
            }
            else if (deep > 1) {
                ht.push("<img class='bbit-tree-icon' src='" + dfop.blankpath + "s.gif'/>");
                for (var j = 1; j < deep; j++) {
                    ht.push("<img class='bbit-tree-elbow-line' src='" + dfop.blankpath + "s.gif'/>");
                }
            }
            ht.push("</span>");
            //img
            cs.length = 0;
            if (nd.hasChildren) {
                if (nd.isexpand) {
                    cs.push(isend ? "bbit-tree-elbow-end-minus" : "bbit-tree-elbow-minus");
                }
                else {
                    cs.push(isend ? "bbit-tree-elbow-end-plus" : "bbit-tree-elbow-plus");
                }
            }
            else {
                cs.push(isend ? "bbit-tree-elbow-end" : "bbit-tree-elbow");
            }
            ht.push("<img class='bbit-tree-ec-icon ", cs.join(" "), "' src='" + dfop.blankpath + "s.gif'/>");
            ht.push("<img class='bbit-tree-node-icon' src='" + dfop.blankpath + "s.gif'/>");
            //checkbox
            if (dfop.showcheck && nd.showcheck) {
                if (nd.checkstate == null || nd.checkstate == undefined) {
                    nd.checkstate = 0;
                }
                ht.push("<img  id='", id, "_", nid, "_cb' class='bbit-tree-node-cb' src='", dfop.cbiconpath, dfop.icons[nd.checkstate], "'/>");
            }
            //a
            ht.push("<a hideFocus class='bbit-tree-node-anchor' tabIndex=1 href='javascript:void(0);'>");
            ht.push("<span unselectable='on'>", nd.text, "</span>");
            ht.push("</a>");
            ht.push("</div>");
            //Child
            if (nd.hasChildren) {
                if (nd.isexpand) {
                    ht.push("<ul  class='bbit-tree-node-ct'  style='z-index: 0; position: static; visibility: visible; top: auto; left: auto;'>");
                    if (nd.ChildNodes) {
                        var l = nd.ChildNodes.length;
                        for (var k = 0; k < l; k++) {
                            nd.ChildNodes[k].parent = nd;
                            buildnode(nd.ChildNodes[k], ht, deep + 1, path + "." + k, k == l - 1);
                        }
                    }
                    ht.push("</ul>");
                }
                else {
                    ht.push("<ul style='display:none;'></ul>");
                }
            }
            else {
                ht.push("<ul style='display:none;'></ul>");
            }
            ht.push("</li>");
            nd.render = true;
        }
        function getItem(path) {
            var ap = path.split(".");
            var t = treenodes;
            for (var i = 0; i < ap.length; i++) {
                if (i == 0) {
                    t = t[ap[i]];
                }
                else {
                    t = t.ChildNodes[ap[i]];
                }
            }
            return t;
        }
        function check(item, state, type) {
            var pstate = item.checkstate;
            if (type == 1) {
                item.checkstate = state;
            }
            else {// 上溯
                var cs = item.ChildNodes;
                var l = cs.length;
                var ch = true;
                for (var i = 0; i < l; i++) {
                    if ((state == 1 && cs[i].checkstate != 1) || state == 0 && cs[i].checkstate != 0) {
                        ch = false;
                        break;
                    }
                }
                if (ch) {
                    item.checkstate = state;
                }
                else {
                    item.checkstate = 2;
                }
            }
            if (dfop.aftercheck) {
                dfop.aftercheck(item);
            }
            //change show
            if (item.render && pstate != item.checkstate) {
                var nid = item.id.replace(/[^\w]/gi, "_");
                var et = $("#" + id + "_" + nid + "_cb");
                if (et.length == 1) {
                    et.attr("src", dfop.cbiconpath + dfop.icons[item.checkstate]);
                }
            }
        }
        //遍历子节点
        function cascade(fn, item, args) {
            if (fn(item, args, 1) != false) {
                if (item.ChildNodes != null && item.ChildNodes.length > 0) {
                    var cs = item.ChildNodes;
                    for (var i = 0, len = cs.length; i < len; i++) {
                        cascade(fn, cs[i], args);
                    }
                }
            }
        }
        //冒泡的祖先
        function bubble(fn, item, args) {
            var p = item.parent;
            while (p) {
                if (fn(p, args, 0) === false) {
                    break;
                }
                p = p.parent;
            }
        }
        function nodeclick(e) {
            var path = $(this).attr("tpath");
            var et = e.target || e.srcElement;
            var item = getItem(path);
            if (et.tagName == "IMG") {
                // +号需要展开
                if ($(et).hasClass("bbit-tree-elbow-plus") || $(et).hasClass("bbit-tree-elbow-end-plus")) {
                    var ul = $(this).next(); //"bbit-tree-node-ct"
                    if (ul.hasClass("bbit-tree-node-ct")) {
                        ul.show();
                    }
                    else {
                        var deep = path.split(".").length;
                        if (item.complete) {
                            item.ChildNodes != null && asnybuild(item.ChildNodes, deep, path, ul, item);
                        }
                        else {
                            $(this).addClass("bbit-tree-node-loading");
                            asnyloadc(item, true, function (data) {
                                if (dfop.dataload)
                                    dfop.dataload(data);
                                item.complete = true;
                                if (item.ChildNodes != null) {
                                    item.ChildNodes = item.ChildNodes.concat(data);
                                }
                                else {
                                    item.ChildNodes = data;
                                }

                                asnybuild(item.ChildNodes, deep, path, ul, item);
                            });
                        }
                    }
                    if ($(et).hasClass("bbit-tree-elbow-plus")) {
                        $(et).swapClass("bbit-tree-elbow-plus", "bbit-tree-elbow-minus");
                    }
                    else {
                        $(et).swapClass("bbit-tree-elbow-end-plus", "bbit-tree-elbow-end-minus");
                    }
                    $(this).swapClass("bbit-tree-node-collapsed", "bbit-tree-node-expanded");
                }
                else if ($(et).hasClass("bbit-tree-elbow-minus") || $(et).hasClass("bbit-tree-elbow-end-minus")) {  //- 号需要收缩                    
                    $(this).next().hide();
                    if ($(et).hasClass("bbit-tree-elbow-minus")) {
                        $(et).swapClass("bbit-tree-elbow-minus", "bbit-tree-elbow-plus");
                    }
                    else {
                        $(et).swapClass("bbit-tree-elbow-end-minus", "bbit-tree-elbow-end-plus");
                    }
                    $(this).swapClass("bbit-tree-node-expanded", "bbit-tree-node-collapsed");
                }
                else if ($(et).hasClass("bbit-tree-node-cb")) // 点击了 Checkbox
                {

                    var deep = path.split(".").length;
                    var ul = $(this).next();
                    if (!item.complete) {
                        $(this).addClass("bbit-tree-node-loading");
                        asnyloadc(item, true, function (data) {
                            if (dfop.dataload)
                                dfop.dataload(data);
                            item.complete = true;
                            if (item.ChildNodes != null) {
                                item.ChildNodes = item.ChildNodes.concat(data);
                            }
                            else {
                                item.ChildNodes = data;
                            }

                            asnybuild(item.ChildNodes, deep, path, ul, item);

                            var pm = $(et).prev().prev();

                            if ($(pm).hasClass("bbit-tree-elbow-plus")) {
                                $(pm).swapClass("bbit-tree-elbow-plus", "bbit-tree-elbow-minus");
                            }
                            else {
                                $(pm).swapClass("bbit-tree-elbow-end-plus", "bbit-tree-elbow-end-minus");
                            }
                            $(this).swapClass("bbit-tree-node-collapsed", "bbit-tree-node-expanded");

                            var s = item.checkstate != 1 ? 1 : 0;
                            var r = true;
                            if (dfop.oncheckboxclick) {
                                r = dfop.oncheckboxclick.call(et, item, s);
                            }
                            if (r != false) {
                                if (dfop.cascadecheck) {
                                    //遍历
                                    cascade(check, item, s);
                                    //上溯
                                    bubble(check, item, s);
                                }
                                else {
                                    check(item, s, 1);
                                }
                            }

                        });
                    }
                    else {
                        var s = item.checkstate != 1 ? 1 : 0;
                        var r = true;
                        if (dfop.oncheckboxclick) {
                            r = dfop.oncheckboxclick.call(et, item, s);
                        }
                        if (r != false) {
                            if (dfop.cascadecheck) {
                                //遍历
                                cascade(check, item, s);
                                //上溯
                                bubble(check, item, s);
                            }
                            else {
                                check(item, s, 1);
                            }
                        }
                    }
                }
            }
            else {
                if (dfop.citem) {
                    var nid = dfop.citem.id.replace(/[^\w]/gi, "_");
                    $("#" + id + "_" + nid).removeClass("bbit-tree-selected");
                }
                dfop.citem = item;
                $(this).addClass("bbit-tree-selected");
                if (dfop.onnodeclick) {
                    if (!item.expand) {
                        item.expand = function () { expandnode.call(item); };
                    }
                    dfop.onnodeclick.call(this, item);
                }
            }
        }
        function expandnode() {
            var item = this;
            var nid = item.id.replace(/[^\w]/gi, "_");
            var img = $("#" + id + "_" + nid + " img.bbit-tree-ec-icon");
            if (img.length > 0) {
                img.click();
            }
        }
        function asnybuild(nodes, deep, path, ul, pnode) {
            var l = nodes.length;
            if (l > 0) {
                var ht = [];
                for (var i = 0; i < l; i++) {
                    nodes[i].parent = pnode;
                    buildnode(nodes[i], ht, deep, path + "." + i, i == l - 1);
                }
                ul.html(ht.join(""));
                ht = null;
                InitEvent(ul);
                ul.addClass("bbit-tree-node-ct").css({ "z-index": 0, position: "static", visibility: "visible", top: "auto", left: "auto", display: "" });

            }
            //增加如果异步加载后没有子结点，则
            else {
                //增加的部分
                var pul = ul
                var pDiv = pul.prev();

                if (pul.children().length == 0) {
                    var prevDiv = pDiv;

                    pul.removeClass("bbit-tree-node-ct");

                    var tempimg = prevDiv.children("img:first");
                    if (tempimg.hasClass("bbit-tree-elbow-end-minus"))
                        tempimg.swapClass("bbit-tree-elbow-end-minus", "bbit-tree-elbow-end");
                    if (tempimg.hasClass("bbit-tree-elbow-minus"))
                        tempimg.swapClass("bbit-tree-elbow-minus", "bbit-tree-elbow");

                    prevDiv.swapClass("bbit-tree-elbow-minus", "bbit-tree-node-leaf");
                }

            }

            ul.prev().removeClass("bbit-tree-node-loading");

        }
        function asnyloadc(pnode, isAsync, callback) {
            if (dfop.url) {
                if (pnode && pnode != null)
                    var param = builparam(pnode);
                $.ajax({
                    type: dfop.method,
                    url: dfop.url,
                    data: param,
                    async: isAsync,
                    dataType: dfop.datatype,
                    success: callback,
                    error: function (e) { alert("error occur!"); }
                });
            }
        }
        function builparam(node) {
            var p = [{ name: "id", value: encodeURIComponent(node.id) }
                    , { name: "text", value: encodeURIComponent(node.text) }
                    , { name: "value", value: encodeURIComponent(node.value) }
                    , { name: "checkstate", value: node.checkstate}];
            return p;
        }
        function bindevent() {
            $(this).hover(function () {
                $(this).addClass("bbit-tree-node-over");
            }, function () {
                $(this).removeClass("bbit-tree-node-over");
            }).click(nodeclick)
             .find("img.bbit-tree-ec-icon").each(function (e) {
                 if (!$(this).hasClass("bbit-tree-elbow")) {
                     $(this).hover(function () {
                         $(this).parent().addClass("bbit-tree-ec-over");
                     }, function () {
                         $(this).parent().removeClass("bbit-tree-ec-over");
                     });
                 }
             });
        }
        function InitEvent(parent) {
            var nodes = $("li.bbit-tree-node>div", parent);
            nodes.each(bindevent);
        }
        function reflash(itemId) {
            var nid = itemId.replace(/[^\w-]/gi, "_");
            var node = $("#" + id + "_" + nid);
            if (node.length > 0) {
                node.addClass("bbit-tree-node-loading");
                var isend = node.hasClass("bbit-tree-elbow-end") || node.hasClass("bbit-tree-elbow-end-plus") || node.hasClass("bbit-tree-elbow-end-minus");
                var path = node.attr("tpath");
                var deep = path.split(".").length;
                var item = getItem(path);
                if (item) {
                    asnyloadc(item, true, function (data) {
                        if (dfop.dataload)
                            dfop.dataload(data);
                        item.complete = true;
                        item.ChildNodes = data;
                        item.isexpand = true;
                        if (data && data.length > 0) {
                            item.hasChildren = true;
                        }
                        else {
                            item.hasChildren = false;
                        }
                        var ht = [];
                        buildnode(item, ht, deep - 1, path, isend);
                        ht.shift();
                        ht.pop();
                        var li = node.parent();
                        li.html(ht.join(""));
                        ht = null;
                        InitEvent(li);
                        bindevent.call(li.find(">div"));
                    });
                }
            }
            else {
                alert("该节点还没有生成");
            }
        }
        function getck(items, c, fn) {
            for (var i = 0, l = items.length; i < l; i++) {
                (items[i].showcheck == true && items[i].checkstate == 1) && c.push(fn(items[i]));
                if (items[i].ChildNodes != null && items[i].ChildNodes.length > 0) {
                    getck(items[i].ChildNodes, c, fn);
                }
            }
        }
        function getCkAndHalfCk(items, c, fn) {
            for (var i = 0, l = items.length; i < l; i++) {
                (items[i].showcheck == true && (items[i].checkstate == 1 || items[i].checkstate == 2)) && c.push(fn(items[i]));
                if (items[i].ChildNodes != null && items[i].ChildNodes.length > 0) {
                    getCkAndHalfCk(items[i].ChildNodes, c, fn);
                }
            }
        }
        me[0].t = {
            getSelectedNodes: function (gethalfchecknode) {
                var s = [];
                if (gethalfchecknode) {
                    getCkAndHalfCk(treenodes, s, function (item) { return item; });
                }
                else {
                    getck(treenodes, s, function (item) { return item; });
                }
                return s;
            },
            getSelectedValues: function () {
                var s = [];
                getck(treenodes, s, function (item) { return item.value; });
                return s;
            },
            getCurrentItem: function () {
                return dfop.citem;
            },

            delNode: function () {

                var curItem = dfop.citem;
                var nid = curItem.id.replace(/[^\w]/gi, "_");
                var curDiv = $("#tree_" + nid);
                var path = curItem.path + "";
                var deep = path.split(".").length;
                var ul = curDiv.next();
                var nodes = $("li.bbit-tree-node", ul);
                if (nodes.length > 0) {
                    alert("存在子结点，不能删除");
                    return;
                }
                if (curItem.ChildNodes) {
                    for (var i = 0; i < curItem.ChildNodes.length; i++) {
                        if (!curItem.ChildNodes[i].isdel) {
                            alert("存在子结点，不能删除");
                            return;
                        }
                    }

                }
                curItem.isdel = true;

                var pul = curDiv.closest("ul");
                var pDiv = pul.prev();

                if (pul.children().length == 1) {
                    var prevDiv = pDiv.addClass("bbit-tree-selected");
                    var path = prevDiv.attr("tpath");
                    var item = getItem(path);
                    dfop.citem = item;
                    pul.removeClass("bbit-tree-node-ct");

                    var tempimg = prevDiv.children("img:first");
                    if (tempimg.hasClass("bbit-tree-elbow-end-minus"))
                        tempimg.swapClass("bbit-tree-elbow-end-minus", "bbit-tree-elbow-end");
                    if (tempimg.hasClass("bbit-tree-elbow-minus"))
                        tempimg.swapClass("bbit-tree-elbow-minus", "bbit-tree-elbow");

                    prevDiv.swapClass("bbit-tree-elbow-minus", "bbit-tree-node-leaf");
                }
                else //如果有同辈元素
                {
                    var prevDiv = curDiv.parent().prev().children("div").addClass("bbit-tree-selected");
                    if (prevDiv.length == 0)
                        prevDiv = curDiv.parent().next().children("div").addClass("bbit-tree-selected");
                    var path = prevDiv.attr("tpath");
                    var item = getItem(path);
                    dfop.citem = item;
                    //如果当前结点是最后一个结点
                    if (curDiv.parent().next("li").length == 0) {
                        var tempimg = prevDiv.children("img:first");
                        if (tempimg.hasClass("bbit-tree-elbow-minus"))
                            tempimg.swapClass("bbit-tree-elbow-minus", "bbit-tree-elbow-end-minus");
                        if (tempimg.hasClass("bbit-tree-elbow-plus"))
                            tempimg.swapClass("bbit-tree-elbow-plus", "bbit-tree-elbow-end-plus");
                        if (tempimg.hasClass("bbit-tree-elbow"))
                            tempimg.swapClass("bbit-tree-elbow", "bbit-tree-elbow-end");
                    }
                }
                curDiv.parent().remove();

            },
            addNode: function (newNode) {

                var curItem = dfop.citem;
                var path = curItem.path + "";
                var deep = path.split(".").length;
                var nid = curItem.id.replace(/[^\w]/gi, "_");
                var curDiv = $("#tree_" + nid);
                var ul = curDiv.next();
                var ht = [];

                //先判断是否加载完
                if (curItem.complete) {

                    //判断子结点是否生成
                    if (ul.hasClass("bbit-tree-node-ct")) {
                        curItem.ChildNodes.push(newNode);
                        newNode.parent = curItem;
                        //生成结点
                        buildnode(newNode, ht, deep, path + "." + (curItem.ChildNodes.length - 1), true);
                    }
                    else {
                        if (curItem.ChildNodes != null) {
                            curItem.ChildNodes.push(newNode);
                        }
                        else {
                            curItem.ChildNodes = [newNode];
                        }
                        //item.ChildNodes != null && asnybuild(item.ChildNodes, deep, path, ul, item);
                        var l = curItem.ChildNodes.length;
                        for (var k = 0; k < l; k++) {
                            curItem.ChildNodes[k].parent = curItem;
                            buildnode(curItem.ChildNodes[k], ht, deep, path + "." + k, k == l - 1);
                        }
                        ul.addClass("bbit-tree-node-ct").css({ "z-index": 0, position: "static", visibility: "visible", top: "auto", left: "auto", display: "" });
                    }



                }
                else {
                    //如果存在没有加载的子结点
                    if (dfop.citem.hasChildren) {
                        //先加载结点
                        $(this).addClass("bbit-tree-node-loading");
                        asnyloadc(ul, item, function (data) {
                            item.complete = true;
                            item.ChildNodes = data;
                            asnybuild(data, deep, path, ul, item);
                        });
                    }
                    else {
                        curItem.complete = true;
                        curItem.ChildNodes = [newNode];
                        newNode.parent = curItem;
                        //生成结点
                        buildnode(newNode, ht, deep, path + "." + 0, true);
                        ul.addClass("bbit-tree-node-ct").css({ "z-index": 0, position: "static", visibility: "visible", top: "auto", left: "auto", display: "" });


                    }
                }
                //统一处理
                //替换倒数第二个结点的图片
                if (ul.children("li").length > 0) {
                    var tempimg = ul.find("li:last>div>img:first");
                    if (tempimg.hasClass("bbit-tree-elbow-end")) {
                        tempimg.swapClass("bbit-tree-elbow-end", "bbit-tree-elbow");
                    }
                    else if (tempimg.hasClass("bbit-tree-elbow-end-minus")) {
                        tempimg.swapClass("bbit-tree-elbow-end-minus", "bbit-tree-elbow-minus");
                    }
                    else if (tempimg.hasClass("bbit-tree-elbow-end-plus")) {
                        tempimg.swapClass("bbit-tree-elbow-end-plus", "bbit-tree-elbow-plus");
                    }
                }

                ul.append(ht.join(""));

                var et = curDiv.children("img")[0];

                if ($(et).hasClass("bbit-tree-elbow-plus")) {
                    $(et).swapClass("bbit-tree-elbow-plus", "bbit-tree-elbow-minus");
                }
                if ($(et).hasClass("bbit-tree-elbow-end-plus")) {
                    $(et).swapClass("bbit-tree-elbow-end-plus", "bbit-tree-elbow-end-minus");
                }
                if ($(et).hasClass("bbit-tree-elbow")) {
                    $(et).swapClass("bbit-tree-elbow", "bbit-tree-elbow-minus");
                }
                if ($(et).hasClass("bbit-tree-elbow-end")) {
                    $(et).swapClass("bbit-tree-elbow-end", "bbit-tree-elbow-end-minus");
                }
                if (curDiv.hasClass("bbit-tree-node-leaf"))
                    curDiv.swapClass("bbit-tree-node-leaf", "bbit-tree-node-expanded");
                if (curDiv.hasClass("bbit-tree-node-collapsed"))
                    curDiv.swapClass("bbit-tree-node-collapsed", "bbit-tree-node-expanded");

                InitEvent(ul);
                ht = null;
                ul.show();

            },
            changeText: function (nodeId, nodeText) {
                var nid = nodeId.replace(/[^\w]/gi, "_");
                var nodeDiv = $("#" + id + "_" + nid);
                var path = nodeDiv.attr("tpath");
                var item = getItem(path);
                item.text = nodeText;
                nodeDiv.find("a>span:first").text(nodeText);

                return item;
            },
            reflash: function (itemOrItemId) {
                var id;
                if (typeof (itemOrItemId) == "string") {
                    id = itemOrItemId;
                }
                else {
                    id = itemOrItemId.id;
                }
                reflash(id);
            },
            expandNode: function (nodeId) {
                var nid = nodeId.replace(/[^\w]/gi, "_");
                var nodeDiv = $("#" + id + "_" + nid);
                nodeDiv.click();
                var path = nodeDiv.attr("tpath");
                //var item = getItem(path);
                var img = $("#" + id + "_" + nid + " img.bbit-tree-ec-icon");

                if (img.hasClass("bbit-tree-elbow-plus") || img.hasClass("bbit-tree-elbow-end-plus")) {

                    if (img.length > 0) {
                        img.click();
                    }
                }
            }
        };
        return me;
    };
    //获取所有选中的节点的Value数组
    $.fn.getTSVs = function () {
        if (this[0].t) {
            return this[0].t.getSelectedValues();
        }
        return null;
    };
    //获取所有选中的节点的Item数组
    $.fn.getTSNs = function (gethalfchecknode) {
        if (this[0].t) {
            return this[0].t.getSelectedNodes(gethalfchecknode);
        }
        return null;
    };
    $.fn.getCurItem = function () {
        if (this[0].t) {
            return this[0].t.getCurrentItem();
        }
        return null;
    };
    $.fn.changeText = function (nodeId, nodeText) {
        if (this[0].t) {
            return this[0].t.changeText(nodeId, nodeText); ;
        }
        return null;
    };
    $.fn.expandNode = function (nodeId) {
        if (this[0].t) {
            return this[0].t.expandNode(nodeId); ;
        }
        return null;
    };
    $.fn.addNode = function (data) {
        if (this[0].t) {
            return this[0].t.addNode(data);
        }
        return null;
    };
    $.fn.delNode = function (data) {
        if (this[0].t) {
            return this[0].t.delNode(data);
        }
        return null;
    };
    $.fn.reflash = function (ItemOrItemId) {
        if (this[0].t) {
            return this[0].t.reflash(ItemOrItemId);
        }
        return null;
    };
})(jQuery);