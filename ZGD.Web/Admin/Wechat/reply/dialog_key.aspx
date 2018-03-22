<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dialog_key.aspx.cs" Inherits="ZGD.Web.Admin.wechat.reply.dialog_key" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../js/layout.js"></script>
    <link href="../../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../skin/default/dgstyle.css" rel="stylesheet" />
    <link href="../../../ico_font/style.css" rel="stylesheet" />
    <script type="text/javascript">
        var postJson = { title: "", cover: "", des: "", link: "" }
        //窗口API
        var api = frameElement.api, W = api.opener;
        api.button({
            name: '确定',
            focus: true,
            callback: function () {
                submitForm();
                return false;
            }
        }, {
            name: '取消'
        });
        var action = "add";
        var id = 0;

        function replaceAll(s1, s2, str) {
            return str.replace(new RegExp(s1, "gm"), s2);
        }


        //页面加载完成执行
        $(function () {
            if ($(api.data).length > 0) {

                var parentObj = $(api.data.obj).parent().parent().parent(); //取得节点父对象

                id = api.data.id;
                if (id != 0) {
                    action = "edit";
                }
            }
        });

        //提交表单处理
        function submitForm() {
            var key = $("#txt_key").val();

            if ($.trim(key) == "") {
                W.$.dialog.alert('请写关键字', function () { $("#txt_key").focus(); }, api);
                return false;
            }
            if (fodder_id == "") {
                W.$.dialog.alert('请选择素材', function () {}, api);
                return false;
            }

            $.post("dialog_key.aspx", { action: action, id: id, key: key, fodder_id: fodder_id }, function (e) {
                if (e == "yes") {

                    api.fun();
                    //因为IE9下出现JS提错，所以加上setTimeout
                    setTimeout(function () {
                        api.close();
                    }, 0);


                } else {
                    if (e == "repter") {
                        W.$.dialog.alert('关键字重复', function () { $("#txt_key").focus(); }, api);
                    } else
                        W.$.dialog.alert('保存异常', function () { api.close(); }, api);
                }
            }, "text");
        }
        
    </script>
</head>
<body class="div-content">
<div class="dl-attach-box">
    <dl>
      <dt style=" width:65px;">关键字</dt>
      <dd style=" margin-left:75px;"><input type="text" id="txt_key" class="input txt" value="<%=reply.reply_key %>" /></dd>
    </dl>
    <dl>
      <dt style=" width:65px;">选择素材</dt>
      <dd style=" margin-left:75px;"><span class="btn" onclick="select()">选择素材</span></dd>
    </dl>
  </div>
  <div id="fodder_box" style=" clear:both; height:430px;  overflow:hidden; overflow-y:auto;"></div>
</body>
</html>
<script>
    var fodder_id = "<%=reply.reply_fodder_id %>";
    function select() {
        var m = W.$.dialog({
            fixed: true,
            lock: true,
            max: false,
            min: false,
            title: "选择素材",
            content: 'url:wechat/fodder/dialog_list.aspx',
            width: 850, height: 560
        });
        m.id = fodder_id;
        m.fun = function (e) {
            fodder_id = e;
            loadFodder(fodder_id)
        }
    }
    loadFodder(fodder_id);
    function loadFodder(id) {
        $.post("dialog_key.aspx", { action: "f", id: id }, function (e) {
            var obj = e.ds[0];
            if (obj.fodder_type == "txt") {
                var con = obj.fodder_xml;
                con = replaceAll("&lt;", "<", con);
                con = replaceAll("&gt;", ">", con);
                con = replaceAll("\n", "<br/>", con);
                var html = "";
                html += "<div class=\"fodder_item_box\">";
                html += "  <div class=\"con\">";
                html += "     <div class=\"txt\">" + con + "</div>";
                html += " </div>";
                html += "</div>";
                $("#fodder_box").html(html);
            }
            if (obj.fodder_type == "new") {
                var str = obj.fodder_xml;
                str = replaceAll("\n", "<br>", str);
                json = eval('(' + str + ')');

                var html = "";
                html += " <div class=\"fodder_item_box\">";
                html += "    <div class=\"con\">";
                html += "       <div class=\"new\">";
                html += "           <div class=\"title\">" + json.title + "</div>";
                html += "           <div class=\"time\">"+obj.addtime+"</div>";
                html += "           <div class=\"cover\">";
                html += "               <img src=\"" + json.cover + "\" />";
                html += "           </div>";
                html += "           <div class=\"des\">" + json.des + "</div>";
                html += "       </div>";
                html += "    </div>";
                html += "</div>";
                $("#fodder_box").html(html);
            }
            if (obj.fodder_type == "news") {
                var str = obj.fodder_xml;
                str = replaceAll("\n", "<br>", str);
                json = eval('(' + str + ')');

                var html = "";
                $.each(json, function (i, citem) {
                    if (i == 0) {
                        html += "      <div class=\"fodder_item_box\">";
                        html += "      <div class=\"con\">";
                        html += "         <div class=\"news\">";
                        html += "             <div class=\"time\">"+obj.addtime+"</div>";
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
                        html += "</div>";
                    }
                });
                $("#fodder_box").html(html);
            }

        }, "json")
    }
</script>