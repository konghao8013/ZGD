<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dialog_list.aspx.cs" Inherits="ZGD.Web.Admin.wechat.fodder.dialog_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>素材</title>
    <script type="text/javascript" src="../../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../../js/layout.js"></script>
    <link href="../../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../skin/pagination/pagination.css" rel="stylesheet" type="text/css" />
    <link href="../../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../ico_font/style.css" rel="stylesheet" />
    <link href="../../skin/default/dgstyle.css" rel="stylesheet" />
    <script>
        var id = 0;
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
        $(function () {

            id = api.id;

            parsingData(<%=json%>);
        });

        //提交表单处理
        function submitForm() {


            api.fun(id);
            //因为IE9下出现JS提错，所以加上setTimeout
            setTimeout(function () {
                api.close();
            }, 0);


        }
    </script>
</head>
<body>
    <div id="fodder_list" style="width: 820px; height: auto;">
        <div class="fodder_list_left">
            <div id="left_box">
            </div>
        </div>
        <div class="fodder_list_right">
            <div id="right_box">
            </div>
        </div>
    </div>
</body>
</html>
<script>
    function parsingData(json) {
        if (json.ds.length > 0) {
            var html_l = "";
            var html_r = "";
            var flag = true;
            $.each(json.ds, function (i, item) {
                var str = item.fodder_xml;
                str = replaceAll("&lt;", "<", str);
                str = replaceAll("&gt;", ">", str);
                str = replaceAll("\n", "<br/>", str);
                //alert(str);
                
                var is_sellect="";
                if(id==item.id){is_sellect="selectss";}

                var html = "<div class=\"fodder_clear\"></div>";
                if(item.fodder_type=="txt")
                {
                    html += "<div class=\"fodder_item_box\">";
                    html += "  <div class=\"con\">";
                    html += "     <div class=\"txt\">" + str + "</div>";
                    html += " </div>";
                    html += " <div class=\"cmd\">";
                    html += "    <div class=\"m\"><a href=\"javascript:void(0);\" class=\"icon-checkmark "+is_sellect+"\" onclick=\"select(this,"+item.id+")\">&nbsp;选择</a></div>";
                    html += " </div>";
                    html += "</div>";
                    html += "<div class=\"fodder_clear\"></div>";
                }

                if (item.fodder_type == "new") {
                    var json = eval('(' + str + ')');
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
                    html += "    <div class=\"m\"><a href=\"javascript:void(0);\" class=\"icon-checkmark "+is_sellect+"\" onclick=\"select(this,"+item.id+")\">&nbsp;选择</a></div>";
                    html += " </div>";
                    html += "</div>";
                }
                if (item.fodder_type == "news") {
                    var json = eval('(' + str + ')');
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
                           html += "    <div class=\"m\"><a href=\"javascript:void(0);\" class=\"icon-checkmark "+is_sellect+"\" onclick=\"select(this,"+item.id+")\">&nbsp;选择</a></div>";
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
    function select(e, _id) {
        id = _id;
        $(".icon-checkmark").removeClass("selectss");
        $(e).addClass("selectss");
    }
</script>
