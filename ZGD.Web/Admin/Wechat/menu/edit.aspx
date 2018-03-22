<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="ZGD.Web.Admin.wechat.menu.edit" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>编辑微信菜单</title>
    <script type="text/javascript" src="../../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../../js/layout.js"></script>
    <script type="text/javascript" src="../../js/pinyin.js"></script>
    <link href="../../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../skin/default/dgstyle.css" rel="stylesheet" />
    <link href="../../../ico_font/style.css" rel="stylesheet" />
    <style>
        .tab-content dl .check, .div-content dl .check
        {
            line-height: 9px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
            var temp = $("input[name='temp']").val();
            if (temp == 'click') {
                get_data();
            }
            $(".rule-multi-radio a").click(function () {

                var obj_link = $("#kj_link");
                var obj_fodder = $("#kj_fodder");
                var obj_key = $("#kj_key");

                var _this = $(this);
                if ($.trim(_this.html()).toString() == "view") {
                    $("#sy_kj").append(obj_link);

                    $("#more_kj").append(obj_fodder);
                    $("#more_kj").append(obj_key);
                }
                if ($.trim(_this.html()).toString() == "click") {

                    $("#more_kj").append(obj_link);
                    $("#sy_kj").append(obj_fodder);
                    $("#sy_kj").append(obj_key);
                }

            });
            get_parents();
        });

        function get_parents() {
            $("input[name='parents']").val($("select[name='sel_menu'] option:selected").attr("data-parents"));
        }
    </script>
</head>
<body class="mainbody">
    <form method="post" action='edit.aspx' id="form1" runat="server">
    <input type="text" name="action" value="<%=actions %>" style=" display:none;" />
    <input type="text" name="id" value="<%=editID %>" style=" display:none;" />
    
    <!--导航栏-->
    <div class="location">
        <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
        <a href="/admin/center" class="home"><i></i><span>首页</span></a>
        <i class="arrow"></i><a href="wechat/wechat_menu"><span>微信导航菜单</span></a>
        <i class="arrow"></i><span>编辑微信菜单</span>
    </div>
    <div class="line10">
    </div>
    <!--/导航栏-->
    <!--内容-->
    <div class="content-tab-wrap">
        <div id="floatHead" class="content-tab">
            <div class="content-tab-ul-wrap">
                <ul>
                    <li><a href="javascript:;" onclick="tabs(this);" class="selected">微信导航信息</a></li>
                </ul>
            </div>
        </div>
    </div>
    <div class="tab-content">
        <dl>
            <dt>上级导航</dt>
            <dd>
                <div class="rule-single-select">
                    <asp:DropDownList ID="ddlParentId" runat="server">
                    </asp:DropDownList>
                </div>
            </dd>
        </dl>
        <dl>
            <dt>导航标题</dt>
            <dd>
                <input name="menu_name" type="text" value="<%=n_model.menu_name  %>" class="input normal"
                    datatype="*1-7" sucmsg=" " /><br />
                <span style="margin-left: 0px;" class="Validform_checktip">*目前自定义菜单最多包括3个一级菜单，每个一级菜单最多包含5个二级菜单。一级菜单最多4个汉字，二级菜单最多7个汉字，多出来的部分将会以“...”代替</span>
            </dd>
        </dl>
        <dl>
            <dt>排序数字</dt>
            <dd>
                <input name="menu_sort" type="text" value="<%=n_model.menu_sort  %>" class="input small"
                    datatype="n" sucmsg=" " />
                <span class="Validform_checktip">*数字，越小越向前</span>
            </dd>
        </dl>
        <dl>
            <dt>是否显示</dt>
            <dd>
                <div class="rule-single-checkbox">
                    <input type="checkbox" name="menu_is_show" <%=n_model.menu_is_show==1?"checked='checked'":""  %>/>
                </div>
                <span class="Validform_checktip">*隐藏后不显示在界面导航菜单中。</span>
            </dd>
        </dl>
        <dl>
            <dt>菜单类型</dt>
            <dd>
                <div class="rule-multi-radio">
                    <span>
                        <input id="menu_type1" type="radio" name="menu_type" value="view" <%=n_model.menu_type=="click"?"":"checked='checked'"  %> />
                        <label for="menu_type1">
                            view</label>
                        <input id="menu_type2" type="radio" name="menu_type" value="click" <%=n_model.menu_type=="click"?"checked='checked'":""  %> />
                        <label for="menu_type2">
                            click</label>
                    </span>
                </div>
            </dd>
        </dl>
        <div id="sy_kj">
            <%if (n_model.menu_type == "view")
              { %>
            <dl id="kj_link">
                <dt>链接地址</dt>
                <dd>
                    <input name="menu_url" type="text" value="<%=n_model.menu_url  %>" maxlength="255" class="input normal" datatype="url" sucmsg=" "  />
                    <span class="Validform_checktip">当用户点击后打开的页面地址，有子菜单可以不填写，如：http://www.baidu.com</span>
                </dd>
            </dl>
            <%}
              else
              { %>
           
            <dl id="kj_fodder">
                <dt>选择素材</dt>
                <dd>
                    <span class="btn" onclick="selectFodder()">请选择</span>&nbsp;<a href="javascript:void(0);" onclick="delFodder()">删除</a>
                    <div id="fodder_box" style="width: 350px; padding-top: 20px;">
                    </div>
                    <input name="menu_content_id" id="menu_content_id" value="<%=n_model.menu_content_id  %>" type="text"  style=" display:none;"   />
                </dd>
            </dl>
            <dl id="kj_key">
                <dt>关键字</dt>
                <dd>
                    <input name="menu_key" id="menu_key" type="text" value="<%=n_model.menu_key  %>" maxlength="255" class="input normal" datatype="*2-100" sucmsg=" " />
                    <span class="Validform_checktip">当用户点击后关键字匹配</span>
                </dd>
            </dl>
            <%}
               %>
        </div>
    </div>
    <!--/内容-->
    <!--工具栏-->
    <div class="page-footer">
        <div class="btn-list">
            <input type="submit" name="btnSubmit" onclick="get_parents();" value="提交保存" id="btnSubmit"
                class="btn" />
            <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:history.back(-1);" />
        </div>
        <div class="clear">
        </div>
    </div>
    <!--/工具栏-->
    </form>
    <div id="more_kj" style="display: none;">
<%if (n_model.menu_type == "click")
              { %>
            <dl id="kj_link">
                <dt>链接地址</dt>
                <dd>
                    <input name="menu_url" type="text" value="" maxlength="255" class="input normal" datatype="url" sucmsg=" " />
                    <span class="Validform_checktip">当用户点击后打开的页面地址，有子菜单可以不填写，如：http://www.baidu.com</span>
                </dd>
            </dl>
            <%}
              else
              { %>
           
            <dl id="kj_fodder">
                <dt>选择素材</dt>
                <dd>
                    <span class="btn" onclick="selectFodder()">请选择</span>&nbsp;<a href="javascript:void(0);" onclick="delFodder()">删除</a>
                    <div id="fodder_box" style="width: 350px; padding-top: 20px;">
                    </div>
                    <input name="menu_content_id" id="menu_content_id" value="<%=n_model.menu_content_id  %>" type="text"  style=" display:none;" />
                </dd>
            </dl>
            <dl id="kj_key">
                <dt>关键字</dt>
                <dd>
                    <input name="menu_key" type="text" id="menu_key" value="" maxlength="255" class="input normal" datatype="*2-100" sucmsg=" " />
                    <span class="Validform_checktip">当用户点击后关键字匹配</span>
                </dd>
            </dl>
            <%}
               %>
    </div>
</body>
</html>
<script>
    var fodder_id = "<%=n_model.menu_content_id  %>";
    function selectFodder() {
        var m = $.dialog({
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
            $("#menu_key").val("FODDER_EVENT_KEY_"+e)
            $("#menu_content_id").val(e);
            loadFodder(fodder_id)
        }
    }
    function delFodder() {
        $("#menu_key").val("")
        $("#menu_content_id").val("0");
        $("#fodder_box").html("");
    }
    loadFodder(fodder_id);
    function loadFodder(id) {
        $.post("../reply/dialog_key.aspx", { action: "f", id: id }, function (e) {
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
                html += "           <div class=\"time\">" + obj.addtime + "</div>";
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
                        html += "             <div class=\"time\">" + obj.addtime + "</div>";
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
