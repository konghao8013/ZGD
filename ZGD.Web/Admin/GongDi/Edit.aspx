<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="ZGD.Web.Admin.GongDi.Edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑工地</title>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.handlers.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <script type="text/javascript" src="/scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <style type="text/css">
        #divHImg { width: 98%; display: inline-block; height: auto; }
        #divHImg .gh_imgs { margin-top: 15px; width: 100%; display:inline-block; }
        #divHImg .gh_imgs h3 { background-color: #808080; color: #fff; }
        .uploadImg { width: 135px; height: 130px; float: left; margin: 10px 10px 0 0; position: relative; }
        .uploadImg a { width: 15px; height: 15px; line-height: 15px; position: absolute; text-align: center; display: inline-block; top: 4px; right: 4px; background-color: #fff; color: #ff0000; font-weight: bold; font-size: 14px; }
        .uploadImg div { width: 120px; text-align: center; border: solid 1px #ccc; padding: 5px; margin-bottom: 3px; }
        .uploadImg img { width: 120px; height: 90px; }
        .uploadImg .input { width: 125px; height: 16px; min-height: 16px; line-height: 16px; padding-left: 1px; }
        #imgPanel { margin-bottom: 5px; }
        #imgPanel img{width:130px;height:90px;margin-bottom:5px;}
    </style>

    <script type="text/javascript">
        var upimg;
        var _img_type = 0, _hidImg = "hidImg", _hidStatus = "hidStatus", _hidTitle = "hidTitle", _divHImg = "divHImg";
        function showUpImg(type) {
            var type = $("#ddlJD_Img").val();
            if (type == "" || type == "0") {
                alert("请选择图集所属进度！");
                return false;
            }

            var title_name = $("#ddlJD_Img").find("option:selected").text().replace("├ ", "") + "图集";
            _img_type = type;

            upimg = $.dialog({
                fixed: true,
                lock: true,
                max: false,
                min: false,
                title: title_name,
                content: 'url:/Admin/Js/mUpload/upload.htm?id=' + type,
                width: 455, height: 380
            });
        }
        function setImg(obj, titleList, type) {
            var str = "";
            var count = obj.split(',');
            //console.log("count", count);
            for (var i = 0; i < count.length - 1; i++) {
                str += type + ",";
            }
            //console.log("str", str);

            $("#" + _hidImg).val($("#" + _hidImg).val() + obj);
            $("#" + _hidStatus).val($("#" + _hidStatus).val() + str);
            $("#" + _hidTitle).val($("#" + _hidTitle).val() + titleList);
            bindImg();
        }
        function deleteImg(idx) {
            var hidimg = $("#" + _hidImg).val().split(",");
            var hidstatus = $("#" + _hidStatus).val().split(",");
            var hidtitle = $("#" + _hidTitle).val().split(",");
            console.log("hidimg", hidimg);
            console.log("hidstatus", hidstatus);
            console.log("hidtitle", hidtitle);
            hidimg.splice(idx, 1);
            hidstatus.splice(idx, 1);
            hidtitle.splice(idx, 1);
            $("#uploadImg_" + idx).remove();
            $("#" + _hidImg).val(hidimg.join(","));
            $("#" + _hidStatus).val(hidstatus.join(","));
            $("#" + _hidTitle).val(hidtitle.join(","));
            bindImg();
        }

        function bindImg() {
            var hidimg = $("#" + _hidImg).val().split(",");
            var hidstatus = $("#" + _hidStatus).val().split(",");
            var hidtitle = $("#" + _hidTitle).val().split(",");
            console.log("hidimg", hidimg);
            console.log("hidstatus", hidstatus);
            console.log("hidtitle", hidtitle);
            var temphtml = "";
            $("#divHImg").html("");
            for (var i = 0; i < hidstatus.length - 1; i++) {
                console.log("hidstatus[i]", hidstatus[i]);
                if ($("#uploadImg_" + i).length <= 0) {
                    temphtml = '<div class="uploadImg" id="uploadImg_' + i + '"><div><img src="' + hidimg[i] + '" /></div><input type="text" class="input" value="' + hidtitle[i] + '" /><a href="javascript:void(0)" onclick="deleteImg(' + i + ')">X</a></div>';
                    if ($("#gd_img_" + hidstatus[i]).length > 0) {
                        $("#gd_img_" + hidstatus[i] + " .gd_img_p").append(temphtml);
                    }
                    else {
                        var title_name = $("#ddlJD_Img option[value='" + hidstatus[i] + "']").text().replace("├ ", "") + "图集";
                        $("#divHImg").append("<div id='gd_img_" + hidstatus[i] + "' class='gh_imgs'><h3>" + title_name + "</h3><div class='gd_img_p'>" + temphtml + "</div></div>");
                    }
                }
            }
        }

        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
            //初始化上传控件
            $(".upload-img").each(function () {
                $(this).InitSWFUpload({ sendurl: "../../tools/upload_ajax.ashx", flashurl: "../../scripts/swfupload/swfupload.swf" });
            });
            $("#ddlJD_Img").change(function () {
                _img_type = $(this).val();
            });
            $("#cbIsImage").bind("click", function () {
                if ($(this).prop("checked")) {
                    $(".upordown").show();
                } else {
                    $(".upordown").hide();
                }
            });
        });
    </script>
</head>
<body class="mainbody">
    <!--导航栏-->
    <div class="location">
        <a href="List.aspx" class="back"><i></i><span>返回列表</span></a>
        <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
        <i class="arrow"></i>
        <a href="List.aspx"><span>工地列表</span></a>
        <i class="arrow"></i>
        <span>编辑工地</span>
    </div>
    <div class="line10"></div>
    <!--/导航栏-->
    <form id="form1" runat="server">
        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">编辑工地</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <dl>
                <dt>所属楼盘</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlHouse" runat="server"></asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>工地标题</dt>
                <dd>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*2-100" nullmsg="请输入工地标题" errormsg="标题长度在3-100个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>工地关键词</dt>
                <dd>
                    <asp:TextBox ID="txtKeyword" runat="server" CssClass="input normal" datatype="*3-120" nullmsg="请输入工地关键词" errormsg="标签长度在2-100个字符间" sucmsg=" "></asp:TextBox>
                    <span>多个关键词，请用英文输入法中的逗号“,”分隔。</span>
                </dd>
            </dl>
            <dl>
                <dt>业主姓名</dt>
                <dd>
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="input normal" datatype="*2-10" nullmsg="请输入业主姓名" errormsg="业主姓名在2-10个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>装修方式</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlZxKind" runat="server" datatype="*" nullmsg="请选择装修方式" sucmsg=" ">
                            <asp:ListItem Text="基装" Value="1"></asp:ListItem>
                            <asp:ListItem Text="整装" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>所在区域</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlAreaId" runat="server"></asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>装修进度</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>设计师</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlDes" runat="server"></asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>设计风格</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlFG" runat="server"></asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>户型</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlHX" runat="server"></asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>建筑面积</dt>
                <dd>
                    <asp:TextBox ID="txtArea" runat="server" CssClass="input normal" datatype="/^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/" nullmsg="请输入建筑面积" errormsg="请输入正确的建筑面积" sucmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">* 单位：平</span>
                </dd>
            </dl>
            <dl>
                <dt>工程总价</dt>
                <dd>
                    <asp:TextBox ID="txtPrice" runat="server" CssClass="input normal" datatype="/^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/" nullmsg="请输入工程总价" errormsg="请输入正确的工程总价" sucmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">* 单位：万</span>
                </dd>
            </dl>
            <dl>
                <dt>工地地址</dt>
                <dd>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="input normal" datatype="*3-120" nullmsg="请输入工地地址" errormsg="工地地址在3-100个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>发布人</dt>
                <dd>
                    <asp:TextBox ID="txtAuthor" runat="server" Text="港宏宜家编辑部" CssClass="input normal" datatype="*2-10" nullmsg="请输入工地发布人" errormsg="发布人长度在2-10个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl class="upordown" id="tr_img_panel" runat="server">
                <dt>缩略图上传</dt>
                <dd>
                    <p id="imgPanel" runat="server"></p>
                    <asp:TextBox ID="txtImgUrl" runat="server" datatype="*1-250" nullmsg="请输入工地缩略图" CssClass="input normal upload-path" />
                    <div class="upload-box upload-img"></div>
                    （图片尺寸400*273px）
                </dd>
            </dl>
            <dl>
                <dt>工地图集</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlJD_Img" runat="server"></asp:DropDownList>
                    </div>
                    <a href="javascript:;" onclick="showUpImg();" style="font-weight: 600; color: Red; text-decoration: underline">点击上传案例图</a><span>（尺寸640*450px）</span><br />
                    <div id="divHImg">
                    </div>
                    <input type="hidden" id="hidImg" name="hidImg" value="" runat="server" />
                    <input type="hidden" id="hidStatus" name="hidStatus" value="" runat="server" />
                    <input type="hidden" id="hidTitle" name="hidTitle" value="" runat="server" />
                </dd>
            </dl>
            <dl>
                <dt>简要描述</dt>
                <dd>
                    <textarea id="txtDesc" runat="server" class="input normal" style="height: 50px"></textarea>
                </dd>
            </dl>
            <%--<dl>
                <dt>工地详情</dt>
                <dd>
                    <textarea id="kEditor" cols="100" rows="8" style="width: 700px; height: 350px; visibility: hidden;" runat="server"></textarea>
                </dd>
            </dl>--%>
            <dl>
                <dt>工地属性</dt>
                <dd>
                    <asp:CheckBoxList ID="cblItem" runat="server"
                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="1">锁定</asp:ListItem>
                        <asp:ListItem Value="1">置顶</asp:ListItem>
                    </asp:CheckBoxList>
                </dd>
            </dl>
            <dl>
                <dt>浏览次数</dt>
                <dd>
                    <asp:TextBox ID="txtClick" runat="server" CssClass="input normal" datatype="*">0</asp:TextBox>
                </dd>
            </dl>
        </div>
        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn" OnClick="btnSave_Click" />
                <asp:Button ID="btnReset" runat="server" Text="返回" CssClass="btn yellow" OnClick="btnReset_Click" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
