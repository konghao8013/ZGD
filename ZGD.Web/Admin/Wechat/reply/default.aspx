<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ZGD.Web.Admin.wechat.reply._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>默认回复</title>
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
            <a href="../../center.aspx" class="home"><i></i><span>首页</span></a> <i class="arrow"></i><span>默认回复</span>
        </div>
        <!--/导航栏-->
        <%if (fodder_id == "0")
          {%>
        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="l-list">
                    <ul class="icon-list">
                        <li><a class="" href="javascript:void(0);" onclick="reSelect()"><span>选择素材</span></a></li>
                    </ul>
                </div>
            </div>
        </div>
        <!--/工具栏-->
        <%} else{ %>
        <div class="line20"></div>
        <%  } %>
        <!--列表-->
        <div id="fodder_box">
        </div>
        <div style="clear: both; height: 50px;">
        </div>
        <!--/列表-->
    </form>
</body>
</html>
<%if (type == "txt")
  { %>
<script>
    function parsingTxtData(con) {
        con = replaceAll("&lt;", "<", con);
        con = replaceAll("&gt;", ">", con);
        con = replaceAll("\n", "<br/>", con);
        var html = "";
        html += "<div class=\"fodder_item_box\" style='margin-left:0px;'>";
        html += "  <div class=\"con\">";
        html += "     <div class=\"txt\">" + con + "</div>";
        html += " </div>";
        html += " <div class=\"cmd\">";
        html += "    <div class=\"m\"><a href=\"javascript:void(0);\" class=\"icon-pencil\" onclick=\"reSelect()\">&nbsp;重新选择</a></div>";
        html += " </div>";
        html += "</div>";
        $("#fodder_box").html(html);
    }
    parsingTxtData("<%=json%>");
</script>
<%} %>
<%if (type == "new")
  { %>
<script>
    function parsingNewData(json) {
        var html = "";
        html += " <div class=\"fodder_item_box\" style='margin-left:0px'>";
        html += "    <div class=\"con\">";
        html += "       <div class=\"new\">";
        html += "           <div class=\"title\">" + json.title + "</div>";
        html += "           <div class=\"time\"><%=time %></div>";
        html += "           <div class=\"cover\">";
        html += "               <img src=\"" + json.cover + "\" />";
        html += "           </div>";
        html += "           <div class=\"des\">" + json.des + "</div>";
        html += "       </div>";
        html += "    </div>";
        html += " <div class=\"cmd\">";
        html += "    <div class=\"m\"><a href=\"javascript:void(0);\" class=\"icon-pencil\" onclick=\"reSelect()\">&nbsp;重新选择</a></div>";
        html += " </div>";
        html += "</div>";
        $("#fodder_box").html(html);
    }
    parsingNewData(<%=json%>);
</script>
<%} %>
<%if (type == "news")
  { %>
<script>
    function parsingNewsData(json) {
        var html = "";
        $.each(json, function (i, citem) {
            if (i == 0) {
                html += "      <div class=\"fodder_item_box\"  style='margin-left:0px'>";
                html += "      <div class=\"con\">";
                html += "         <div class=\"news\">";
                html += "             <div class=\"time\"><%=time %></div>";
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
                html += "    <div class=\"m\"><a href=\"javascript:void(0);\" class=\"icon-pencil\" onclick=\"reSelect()\">&nbsp;重新选择</a></div>";
                html += "   </div>";
                html += "</div>";
            }
        });
        $("#fodder_box").html(html);
    }

    parsingNewsData(<%=json%>);

</script>
<%} %>
<script>
    function reSelect() {
        var m = $.dialog({
            fixed: true,
            lock: true,
            max: false,
            min: false,
            title: "选择素材",
            content: 'url:wechat/fodder/dialog_list.aspx',
            width: 850, height: 560
        });
        m.id = "<%=fodder_id %>";
        m.fun = function (id) {
            if (id != m.id && id != 0) {
                $.post("default.aspx", { id: id }, function (e) {
                    parent.jsprint("保存成功", "", "success", function () {
                        location.reload();
                    });
                }, "text");
            }
        }
    }
</script>
