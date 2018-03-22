<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dialog_news.aspx.cs" Inherits="ZGD.Web.Admin.wechat.fodder.dialog_news" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script type="text/javascript" src="../../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../js/layout.js"></script>
    <script type="text/javascript" src="../../../scripts/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="../../../scripts/swfupload/swfupload.queue.js"></script>
    <script type="text/javascript" src="../../../scripts/swfupload/swfupload.handlers.js"></script>
    <link href="../../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../skin/default/dgstyle.css" rel="stylesheet" />
    <link href="../../../ico_font/style.css" rel="stylesheet" />
    <script type="text/javascript">
        var postJson = { title: "", cover: "", link: "" }
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
                    //var jsonStr = $(api.data.obj).parent().parent().find("span").html();
                    //postJson = eval('(' + jsonStr + ')');
                    //init();
                }
            }
        });

        //提交表单处理
        function submitForm() {
            var b = true;
            var postData = "[";
            $(".postjson").each(function (i, item) {
                var jsonStr = $(item).html();
                var itempostJson = eval('(' + jsonStr + ')');
                //验证表单
                if (itempostJson.title == "") {
                    W.$.dialog.alert("请填写图文列表" + (i + 1) + "标题", function () {
                        index = i; init(i);
                        $("#txt_title").focus();
                    }, api);
                    b = false;
                    return false;
                }
                if (itempostJson.cover == "") {
                    W.$.dialog.alert("请上传列表" + (i + 1) + "封面图片", function () { index = i; init(i); }, api);
                    b = false;
                    return false;
                }
//                if (itempostJson.link == "") {
//                    W.$.dialog.alert("请填写图文列表" + (i + 1) + "链接", function () { index = i; init(i); $("#txt_link").focus(); }, api);
//                    b = false;
//                    return false;
//                }
                postData += $(item).html() + ",";
            })
            if (!b) { return false; }
            if (postData.length > 2) {
                postData = postData.substr(0, postData.length - 1);
            }
            postData += "]";

            $.post("dialog_news.aspx", { action: action, id: id, json: postData }, function (e) {
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
<body onbeforeunload="removeChart()"  onunload="removeChart()">
    <div class="div-content">
        <div id="fodder_news_edit">
            <div class="left_edit">
                <div class="ico_3q">
                    <div class="d1">
                        <div class="d2">
                        </div>
                    </div>
                </div>
                <div class="w_edit_box">
                    <div class="wd">
                        标题</div>
                    <div class="w_input">
                        <input id="txt_title" onkeyup="onkeup_title(this)" />
                    </div>
                    <div class="wd">
                        封面<span>大图片建议尺寸：360像素 * 200像素</span></div>
                    <div class="w_cover">
                        <div class="btn_up upload-img">
                            <span><i class="icon-upload"></i></span>
                            <div class="upload-box upload-img">
                            </div>
                        </div>
                        <div class="cover" id="w_cover_box">
                            <img id="cover_img" src="" /><a href="javascript:void(0);" onclick="delCover(this)">删除</a>
                        </div>
                    </div>
                    <div class="wd">
                        链接</div>
                    <div class="w_input">
                        <input id="txt_link" onkeyup="onkeup_link(this)" />
                    </div>
                </div>
            </div>
            <div class="right_temp">
                <%if (json == "")
                  { %>
                <div class="fodder_item_box">
                    <div class="con">
                        <div class="news">
                            <div class="time">
                            </div>
                            <div class="cover default_m_cover temp_cover">
                                <img src="" class="temp_img" />
                                <div class="title temp_title">
                                    &nbsp;标题</div>
                                <div class="edit_cmd">
                                    <span class="icon-pencil" onclick="editItem(0)"></span>
                                </div>
                                <span class="postjson hide">{ title: "", cover: "", link: "" }</span>
                            </div>
                            <div id="item_list">
                                <div class="child_item">
                                    <div class="c_title temp_title">
                                        标题</div>
                                    <div class="c_cover default_s_cover temp_cover">
                                        <img src="" class="temp_img" />
                                    </div>
                                    <div class="edit_cmd">
                                        <div class="l">
                                            <a href="javascript:void(0);" class="icon-pencil" onclick="editItem(this)"></a>
                                        </div>
                                        <div class="r">
                                            <a href="javascript:void(0);" class="icon-remove2" onclick="removeItem(this)"></a>
                                        </div>
                                    </div>
                                    <span class="postjson hide">{ title: "", cover: "", link: "" }</span>
                                </div>
                            </div>
                            <div class="add_item">
                                <div class="icon-plus" onclick="addItem(this)">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%}
                  else
                  { %>
                <script>
                    var str = <%=json %>;
                 //str = replaceAll("\n", "<br>", str);
                 var json =str;// eval('(' + str + ')');

                  var i_html="";
                  $.each(json,function(i,item){
                  if (i == 0) {
                  i_html +="<div class=\"fodder_item_box\">";
                   i_html +=" <div class=\"con\">";
                   i_html +="     <div class=\"news\">";
                   i_html +="         <div class=\"time\"></div>";
                   i_html +="         <div class=\"cover  temp_cover\">";
                   i_html +="             <img src=\""+item.cover+"\" class=\"temp_img\" />";
                    i_html +="            <div class=\"title temp_title\">"+item.title+"</div>";
                   i_html +="             <div class=\"edit_cmd\"><span class=\"icon-pencil\" onclick=\"editItem(0)\"></span></div>";
                    i_html +="            <span class=\"postjson hide\">{ title: \""+item.title+"\", cover: \""+item.cover+"\", link: \""+item.link+"\" }</span>";
                    i_html +="        </div>";
                    i_html +="        <div id=\"item_list\">";
                    }
                    if(i>0)
                    {
                    
                    i_html +="            <div class=\"child_item\">";
                    i_html +="                <div class=\"c_title temp_title\">"+item.title+"</div>";
                    i_html +="                <div class=\"c_cover  temp_cover\">";
                    i_html +="                    <img src=\""+item.cover+"\" class=\"temp_img\" />";
                     i_html +="               </div>";
                     i_html +="               <div class=\"edit_cmd\">";
                     i_html +="                   <div class=\"l\"><a href=\"javascript:void(0);\" class=\"icon-pencil\" onclick=\"editItem(this)\"></a></div>";
                     i_html +="                   <div class=\"r\"><a href=\"javascript:void(0);\" class=\"icon-remove2\" onclick=\"removeItem(this)\"></a></div>";
                     i_html +="               </div>";
                     i_html +="          <span class=\"postjson hide\">{ title: \""+item.title+"\", cover: \""+item.cover+"\", link: \""+item.link+"\" }</span>";
                     i_html +="           </div>";
                      
                     
                     }
                     if(i==(json.length-1))
                     {
                     i_html +="       </div>";
                      i_html +="      <div class=\"add_item\">";
                      i_html +="          <div class=\"icon-plus\" onclick=\"addItem(this)\"></div>";
                      i_html +="      </div>";
                      i_html +="  </div>";
                   i_html +=" </div>";
                    i_html +="</div>";
                    }
                  });
                  $(".right_temp").html(i_html);
                </script>
                <%} %>
            </div>
        </div>
    </div>
</body>
</html>
<script>
    function addItem(e) {
        if ($(".child_item").length < 7) {
            var html = "<div class=\"child_item\">";
            html += "      <div class=\"c_title temp_title\">标题</div>";
            html += "         <div class=\"c_cover default_s_cover temp_cover\">";
            html += "      <img class='temp_img' src=\"\" />";
            html += "      </div>";
            html += "      <div class=\"edit_cmd\">";
            html += "        <div class=\"l\"><a href=\"javascript:void(0);\" class=\"icon-pencil\" onclick=\"editItem(this)\"></a></div>";
            html += "     <div class=\"r\"><a href=\"javascript:void(0);\" class=\"icon-remove2\" onclick=\"removeItem(this)\"></a></div>";
            html += "  </div><span class=\"postjson hide\">{ title: \"\", cover: \"\", link: \"\" }</span>";
            html += "</div>";
            $(e).parent().parent().find("#item_list").append(html);
        } else {
            alert("你最多只可以加入8条图文消息");
        }
    }
    function removeItem(e) {
        if ($(".child_item").length > 1) {
            $(e).parents(".child_item").remove();
        } else {
            alert("无法删除，多条图文至少需要2条消息。");
        }
    }
    function editItem(e) {
        if (e == 0) {
            index = 0;
        } else {
            index = $(".child_item").index($(e).parents(".child_item")) + 1;
        }
        init(index);
    }
</script>
<script>
    var index = 0;
    function init(i) {
        var jsonStr = $(".postjson").eq(index).html();
        postJson = eval('(' + jsonStr + ')');
        //alert(postJson.title);
        $("#txt_title").val(postJson.title);
        $("#cover_img").attr("src", postJson.cover)
        $("#txt_link").val(postJson.link);
    }
    init(0)
    function delCover(e) {
        $(e).parent().find("img").attr("src", "");
        $(e).parents(".w_cover").find(".cover").hide();
    }

    function getCover() {
        setInterval(function () {
            var src = $("#cover_img").attr("src");
            if (postJson.cover != src) {
                postJson.cover = src;
                $(".postjson").eq(index).html(jsonTostr({ title: postJson.title, cover: postJson.cover, link: postJson.link }));
            }
            if (src != "") {
                $(".temp_img").eq(index).attr("src", src);
                $(".temp_img").eq(index).show();
                $("#w_cover_box").show();
                if (index == 0)
                    $(".temp_cover").eq(index).removeClass("default_m_cover");
                else
                    $(".temp_cover").eq(index).removeClass("default_s_cover");
            }
            else {
                $("#w_cover_box").hide();
                $(".temp_img").eq(index).hide();
                if (index == 0)
                    $(".temp_cover").eq(index).addClass("default_m_cover");
                else
                    $(".temp_cover").eq(index).addClass("default_s_cover");
            }

        }, 1);
    }
    getCover();

    function onkeup_title(obj) {
        var v = $(obj).val();
        $(".temp_title").eq(index).html(v);
        v = replaceAll("\"", "\\\"", v);
        postJson.title = v;
        $(".postjson").eq(index).html(jsonTostr({ title: postJson.title, cover: postJson.cover, link: postJson.link }));
    }

    function onkeup_link(obj) {
        var v = $(obj).val();
        postJson.link = v;
        $(".postjson").eq(index).html(jsonTostr({ title: postJson.title, cover: postJson.cover, link: postJson.link }));
    }
</script>
