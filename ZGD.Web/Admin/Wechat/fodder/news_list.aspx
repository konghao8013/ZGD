<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="news_list.aspx.cs" Inherits="ZGD.Web.Admin.wechat.fodder.news_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>图文素材</title>
    <script type="text/javascript" src="../../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../../js/layout.js"></script>
    <link href="../../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../skin/pagination/pagination.css" rel="stylesheet" type="text/css" />
    <link href="../../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../ico_font/style.css" rel="stylesheet" />
    <link href="../../skin/default/dgstyle.css" rel="stylesheet" />
</head>
<body class="mainbody" onbeforeunload="removeChart()"  onunload="removeChart()">
    <form name="form1" id="form1">
    <!--导航栏-->
    <div class="location">
        <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
        <a href="../../center.aspx" class="home"><i></i><span>首页</span></a> <i class="arrow">
        </i><span>图文素材</span>
    </div>
    <!--/导航栏-->
    <!--工具栏-->
    <div class="toolbar-wrap">
        <div id="floatHead" class="toolbar">
            <div class="l-list">
                <ul class="icon-list">
                    <li><a class="" href="javascript:void(0);"><span>图文素材列表（共<span id="itemCount"><%=itemCount %></span>个）</span></a></li>
                </ul>
            </div>
        </div>
    </div>
    <!--/工具栏-->
    <!--列表-->
    <div id="fodder_list">
        <div class="fodder_list_left">
            <div class="fodder_add_box">
                <div class="news">
                    <div class="icon-plus">
                    </div>
                    <div class="news_li">
                        <span class="box" onclick="addNewFodder()"><i class="icon-image2"></i><span>单图文消息</span></span>
                        <span class="box" onclick="addNewsFodder()"><i class="icon-images"></i><span>多图文消息</span></span>
                    </div>
                </div>
            </div>
            <div class="fodder_clear">
            </div>
            <div id="left_box">
            </div>
        </div>
        <div class="fodder_list_right">
            <div id="right_box">
            </div>
        </div>
    </div>
    <div style="clear: both; height: 50px;">
    </div>
    <!--/列表-->
    </form>
</body>
</html>
<script>

    function parsingData(json) {
        if (json.ds.length > 0) {
            var html_l = "";
            var html_r = "";
            var flag = true;
            $("#itemCount").html(json.ds.length);
            $.each(json.ds, function (i, item) {
                var str = item.fodder_xml;
                str = replaceAll("\n", "<br>", str);
                var json = eval('(' + str + ')');
                var html = "";
                if (item.fodder_type == "new") {
                    html += " <div class=\"fodder_item_box\">";
                    html += "    <div class=\"con\">";
                    html += "       <div class=\"new\">";
                    html += "           <div class=\"title\">" + json.title + "</div>";
                    html += "           <div class=\"time\">" + item.addtime + "</div>";
                    html += "           <div class=\"cover\">";
                    html += "               <img src=\"" + json.cover + "\" />";
                    html += "           </div>";
                    html += "           <div class=\"des\">" + json.des + "</div>";
                    html += "       </div>";
                    html += "    </div>";
                    html += " <div class=\"cmd\"><span class='hide'>" + str + "</span>";
                    html += "    <div class=\"l\"><a href=\"javascript:void(0);\" class=\"icon-pencil\" onclick=\"addNewFodder(this," + item.id + ")\"></a></div>";
                    html += "    <div class=\"r\"><a href=\"javascript:void(0);\" class=\"icon-remove2\" onclick=\"del(" + item.id + ")\"></a></div>";
                    html += " </div>";
                    html += "</div>";
                }
                if (item.fodder_type == "news") {

                    $.each(json, function (i, citem) {
                        if (i == 0) {
                            html += "      <div class=\"fodder_item_box\">";
                            html += "      <div class=\"con\">";
                            html += "         <div class=\"news\">";
                            html += "             <div class=\"time\">" + item.addtime + "</div>";
                            html += "             <div class=\"cover\">";
                            html += "                 <img src=\"" + citem.cover + "\" />";
                            html += "                <div class=\"title\">" + citem.title + "</div>";
                            html += "            </div>";
                        }
                        if (i > 0) {
                            html += "            <div class=\"child_item\">";
                            html += "                <div class=\"c_title\">" + citem.title + "</div>";
                            html += "                <div class=\"c_cover\">";
                            html += "                   <img src=\"" + citem.cover + "\" />";
                            html += "                </div>";
                            html += "            </div>";
                        }
                        if (i == (json.length - 1)) {
                            html += "       </div>";
                            html += "   </div>";
                            html += "   <div class=\"cmd\">";
                            html += "       <div class=\"l\"><a href=\"javascript:void(0);\" class=\"icon-pencil\" onclick=\"addNewsFodder(this," + item.id + ")\"></a></div>";
                            html += "       <div class=\"r\"><a href=\"javascript:void(0);\" class=\"icon-remove2\" onclick=\"del(" + item.id + ")\"></a></div>";
                            html += "   </div>";
                            html += "</div>";
                        }
                    });
                }
                html += "<div class=\"fodder_clear\"></div>";
                if (flag) {
                    html_l += html;
                    flag = false;
                } else {
                    html_r += html;
                    flag = true;
                }
            });

            $("#left_box").html(html_l);
            $("#right_box").html(html_r);
        } else {
            $("#left_box").html(""); $("#right_box").html("");
        }
    }
</script>
<script>
    parsingData(<%=json%>);
</script>
<script>
    //创建窗口
    function addNewsFodder(obj, id) {
        var objNum = arguments.length;

        //如果是修改状态，将对象传进去
        if (objNum > 1) {
            var m = $.dialog({
                fixed: true,
                lock: true,
                max: false,
                min: false,
                title: "修改多图文素材",
                content: 'url:wechat/fodder/dialog_news.aspx?id='+id,
                width: 850, height: 560
            });
            m.data = { obj: obj, id: id };
            m.fun = reload;
        } else {
            var m = $.dialog({
                fixed: true,
                lock: true,
                max: false,
                min: false,
                title: "添加多图文素材",
                content: 'url:wechat/fodder/dialog_news.aspx',
                width: 850, height: 560
            });
            m.fun = reload;
        }
    }
    function addNewFodder(obj, id) {
        var objNum = arguments.length;

        //如果是修改状态，将对象传进去
        if (objNum > 1) {
            var m = $.dialog({
                fixed: true,
                lock: true,
                max: false,
                min: false,
                title: "修改单图文素材",
                content: 'url:wechat/fodder/dialog_new.aspx',
                width: 850, height: 560
            });
            m.data = { obj: obj, id: id };
            m.fun = reload;
        } else {
            var m = $.dialog({
                fixed: true,
                lock: true,
                max: false,
                min: false,
                title: "添加单图文素材",
                content: 'url:wechat/fodder/dialog_new.aspx',
                width: 850, height: 560
            });
            m.fun = reload;
        }
    }
    function reload() {
        $.post("news_list.aspx", { action: "list" }, function (e, s) {
            parsingData(e);
        }, "json");
    }
    function del(id) {
        $.dialog.confirm("删除记录后不可恢复，您确定吗？", function () {
            $.post("txt_list.aspx", { action: "del", id: id }, function (e) {
                if (e == "yes") {
                    parent.jsprint("删除成功", "", "success", function () {
                        reload();
                    });

                } else {
                    alert(e);
                }

            }, "text");
        });
    }
</script>
