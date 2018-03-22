<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dialog_new.aspx.cs" Inherits="ZGD.Web.Admin.wechat.fodder.dialog_new" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script type="text/javascript" src="../../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../js/layout.js"></script>
    <script type="text/javascript" src="../../../scripts/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="../../../scripts/swfupload/swfupload.queue.js"></script>
    <script type="text/javascript" src="../../../scripts/swfupload/swfupload.handlers.js?v=<%=time %>>"></script>
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
                    var jsonStr = $(api.data.obj).parent().parent().find("span").html();
                    postJson = eval('(' + jsonStr + ')');
                    init();
                }
            }
        });

        //提交表单处理
        function submitForm() {
            //验证表单
            if (postJson.title == "") {
                W.$.dialog.alert('请填写图文标题', function () { $("#txt_title").focus(); }, api);
                return false;
            }
            if (postJson.cover == "") {
                W.$.dialog.alert('请上传封面图片', function () { }, api);
                return false;
            }
            if (postJson.des == "") {
                W.$.dialog.alert('请填写图文摘要', function () { }, api);
                return false;
            }
//            if (postJson.link == "") {
//                W.$.dialog.alert('请填写图文链接', function () { }, api);
//                return false;
//            }
            $.post("dialog_new.aspx", { action: action, id: id, json: jsonTostr(postJson) }, function (e) {
                if (e == "yes") {
                    
                        api.fun();
                        //因为IE9下出现JS提错，所以加上setTimeout
                        setTimeout(function () {
                            api.close();
                        }, 0);


                } else {
                    W.$.dialog.alert('保存异常', function () { api.close(); }, api);
                }
            }, "text");
        }
        $(function () {
            //初始化上传控件
            $(".upload-img").each(function () {
                $(this).InitsSWFUpload({ sendurl: "../../../tools/upload_ajax.ashx", flashurl: "../../../scripts/swfupload/swfupload.swf" });
            });
        })
    </script>
</head>
<body>
    <div class="div-content">
        <div id="fodder_news_edit">
            <div class="left_edit">
                <div class="ico_3q">
                    <div class="d1">
                        <div class="d2"></div>
                    </div>
                </div>
                <div class="w_edit_box">
                    <div class="wd">标题</div>
                    <div class="w_input">
                        <input id="txt_title" onkeyup="onkeup_title(this)" />
                    </div>
                    <div class="wd">封面<span>大图片建议尺寸：360像素 * 200像素</span></div>
                    <div class="w_cover">
                        <div class="btn_up upload-img">
                            <span><i class="icon-upload"></i></span>
                            <div class="upload-box upload-img"></div>
                        </div>
                        <div class="cover" id="w_cover_box">
                            <img id="cover_img" src="" /><a href="javascript:void(0);" onclick="delCover(this)">删除</a>
                        </div>
                    </div>
                    <div class="wd">摘要</div>
                    <div class="w_zhaiyao">
                        <textarea id="txt_zhaiyao" onkeyup="onkeup_zhaiyao(this)"></textarea>
                    </div>
                    <div class="wd">链接</div>
                    <div class="w_input">
                        <input id="txt_link" onkeyup="onkeup_link(this)" />
                    </div>
                </div>
            </div>
            <div class="right_temp">
                <div class="fodder_item_box">
                    <div class="con">
                        <div class="new">
                            <div class="title" id="temp_title">标题</div>
                            <div class="cover  temp_cover default_m_cover">
                                <img class="temp_img hide" src="" />
                            </div>
                            <div class="des" id="temp_zhaiyao"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
<script>
    
    function init()
    {
        if (postJson.title != "")
        {
            $("#txt_title").val(postJson.title);
            onkeup_title($("#txt_title"))

            $("#cover_img").attr("src",postJson.cover)

            var des = replaceAll("<br>", "\n", postJson.des);
            $("#txt_zhaiyao").val(des);
            onkeup_zhaiyao($("#txt_zhaiyao"))

            $("#txt_link").val(postJson.link);
        }
    }

    function delCover(e) {
        $(e).parent().find("img").attr("src", "");
        $(e).parents(".w_cover").find(".cover").hide();
    }
    function getCover() {
        setInterval(function () {
            var src = $("#cover_img").attr("src");
            postJson.cover = src;
            if (src != "") {
                $(".temp_img").attr("src", src);
                $(".temp_img").show();
                $("#w_cover_box").show();
                $(".temp_cover").removeClass("default_m_cover");
            }
            else {
                $("#w_cover_box").hide();
                $(".temp_img").hide();
                $(".temp_cover").addClass("default_m_cover");
            }
        }, 1);
    }
    getCover();

    function onkeup_title(obj) {
        var v = $(obj).val();
        postJson.title = v;
        $("#temp_title").html(v);
    }
    function onkeup_zhaiyao(obj) {
        var v = $(obj).val();
        postJson.des = v;
        v = replaceAll("\n", "<br>", v);
        $("#temp_zhaiyao").html(v);
    }
    function onkeup_link(obj) {
        var v = $(obj).val();
        postJson.link = v;
    }
</script>
