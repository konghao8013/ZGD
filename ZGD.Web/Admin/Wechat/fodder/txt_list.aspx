<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="txt_list.aspx.cs" Inherits="ZGD.Web.Admin.wechat.fodder.txt_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>文本素材</title>
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
<body class="mainbody">

    <form name="form1" id="form1">
        <!--导航栏-->
        <div class="location">
            <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
            <a href="../../center.aspx" class="home"><i></i><span>首页</span></a> <i class="arrow"></i><span>文本素材</span>
        </div>
        <!--/导航栏-->
        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="l-list">
                    <ul class="icon-list">
                        <li><a class="" href="javascript:void(0);"><span>文本素材列表（共<span id="itemCount"><%=itemCount %></span>个）</span></a></li>
                    </ul>
                </div>
            </div>
        </div>
        <!--/工具栏-->
        <!--列表-->
        <div id="fodder_list">
            <div class="fodder_list_left">
                <div class="fodder_add_box">
                    <div class="txt icon-plus" onclick="addTxtFodder()"></div>
                </div>
                <div class="fodder_clear"></div>
                <div id="left_box"></div>
            </div>
            <div class="fodder_list_right">
                <div id="right_box"></div>
            </div>
        </div>
        <div style="clear: both; height: 50px;"></div>
        <!--/列表-->
    </form>
</body>
</html>
<script>
    function replaceAll(s1, s2, str) {
        return str.replace(new RegExp(s1, "gm"), s2);
    }

    function parsingData(json) {
        if (json.ds.length > 0) {
            var html_l = "";
            var html_r = "";
            var flag = true;
            $("#itemCount").html(json.ds.length);
            $.each(json.ds, function (i, item) {
                var con = item.fodder_xml;

                con = replaceAll("&lt;", "<", con);
                con = replaceAll("&gt;", ">", con);
                con = replaceAll("\n", "<br/>", con);

                if (flag) {
                    html_l += "<div class=\"fodder_item_box\">";
                    html_l += "  <div class=\"con\">";
                    html_l += "     <div class=\"txt\">" + con + "</div>";
                    html_l += " </div>";
                    html_l += " <div class=\"cmd\">";
                    html_l += "    <div class=\"l\"><a href=\"javascript:void(0);\" class=\"icon-pencil\" onclick=\"addTxtFodder(this," + item.id + ")\"></a></div>";
                    html_l += "    <div class=\"r\"><a href=\"javascript:void(0);\" class=\"icon-remove2\" onclick=\"del(" + item.id + ")\"></a></div>";
                    html_l += " </div>";
                    html_l += "</div>";
                    html_l += "<div class=\"fodder_clear\"></div>";
                    flag = false;
                }
                else {
                    html_r += "<div class=\"fodder_item_box\">";
                    html_r += "  <div class=\"con\">";
                    html_r += "     <div class=\"txt\">" + con + "</div>";
                    html_r += " </div>";
                    html_r += " <div class=\"cmd\">";
                    html_r += "    <div class=\"l\"><a href=\"javascript:void(0);\" class=\"icon-pencil\" onclick=\"addTxtFodder(this," + item.id + ")\"></a></div>";
                    html_r += "    <div class=\"r\"><a href=\"javascript:void(0);\" class=\"icon-remove2\" onclick=\"del(" + item.id + ")\"></a></div>";
                    html_r += " </div>";
                    html_r += "</div>";
                    html_r += "<div class=\"fodder_clear\"></div>";
                    flag = true;
                }
            });

            $("#left_box").html(html_l);
            $("#right_box").html(html_r);
        } else
        {
            $("#left_box").html(""); $("#right_box").html("");
        }
    }
</script>
<script>
    parsingData(<%=json%>);
</script>

<script>
    //创建窗口
    function addTxtFodder(obj, id) {
        var objNum = arguments.length;

        //如果是修改状态，将对象传进去
        if (objNum > 1) {
            var m = $.dialog({
                fixed: true,
                lock: true,
                max: false,
                min: false,
                title: "修改文本素材",
                content: 'url:wechat/fodder/dialog_txt.aspx',
                width: 600
            });
            m.data = { obj: obj, id: id };
        } else {
            var m = $.dialog({
                fixed: true,
                lock: true,
                max: false,
                min: false,
                title: "添加文本素材",
                content: 'url:wechat/fodder/dialog_txt.aspx',
                width: 600
            });
            m.fun = reload;
        }
    }
    function reload() {
        $.post("txt_list.aspx", { action: "list" }, function (e,s) {
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
