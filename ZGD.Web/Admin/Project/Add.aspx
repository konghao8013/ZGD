<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" ValidateRequest="false" Inherits="ZGD.Web.Admin.Project.Add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>发布图册</title>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../js/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="../js/swfupload/swfupload.handlers.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <script type="text/javascript" src="../js/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <style type="text/css">
        #divHImg { display: inline-block; height: auto; }
        .uploadImg { width: 350px; float: left; margin: 5px 10px 5px 0; position: relative; }
        .uploadImg a { width: 15px; height: 15px; line-height: 15px; position: absolute; text-align: center; display: inline-block; top: 4px; right: 4px; background-color: #fff; color: #ff0000; font-weight: bold; font-size: 14px; }
        .uploadImg div { width: 340px; height: 240px; text-align: center; border: solid 1px #ccc; padding: 5px; margin-bottom: 3px; }
        .uploadImg img { height: 100%; }
        .uploadImg .input { width: 345px; height: 40px; line-height: 16px; padding-left: 1px; }
        #imgPanel { margin-bottom: 5px; }
        #imgPanel img { width: 120px; }
    </style>
    <script type="text/javascript">
        var upimg;
        var _img_type = 1, _hidImg = "hidImg", _hidTitle = "hidTitle", _divHImg = "divHImg";
        function showUpImg(type) {
            var title_name = type == 1 ? "上传图片" : "上传图片";
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
            _img_type = type;
            _hidImg = _img_type == 1 ? "hidImg" : "hidImg_pro";
            _hidTitle = _img_type == 1 ? "hidTitle" : "hidTitle_pro";
            _divHImg = _img_type == 1 ? "divHImg" : "divHImg_pro";
            $("#" + _hidImg).val($("#" + _hidImg).val() + obj);
            $("#" + _hidTitle).val($("#" + _hidTitle).val() + titleList);
            bindImg();
        }
        function deleteImg(index, type) {
            _img_type = type;
            _hidImg = type == 1 ? "hidImg" : "hidImg_pro";
            _hidTitle = type == 1 ? "hidTitle" : "hidTitle_pro";
            _divHImg = type == 1 ? "divHImg" : "divHImg_pro";
            var hidimg = $("#" + _hidImg).val().split(",");
            var hidtitle = $("#" + _hidTitle).val().split(",");
            var hidtemp1 = "";
            var hidtemp2 = "";
            $.each($("#" + _divHImg + " textarea"), function (i, n) {
                if (i != index) {
                    hidtemp1 += hidimg[i] + ',';
                    hidtemp2 += $(n).val() + ',';
                }
            });
            $("#" + _hidImg).val(hidtemp1);
            $("#" + _hidTitle).val(hidtemp2);
            bindImg();
        }
        function bindData() {
            var hidimg = "", hidtitle = "";
            var hidimg_pro = "", hidtitle_pro = "";
            $.each($("#divHImg img"), function (i, n) {
                hidimg += $(n).attr("src") + ',';
            });
            $.each($("#divHImg textarea"), function (i, n) {
                hidtitle += $(n).val() + ',';
            });
            $.each($("#divHImg_pro img"), function (i, n) {
                hidimg_pro += $(n).attr("src") + ',';
            });
            $.each($("#divHImg_pro textarea"), function (i, n) {
                hidtitle_pro += $(n).val() + ',';
            });
            $("#hidImg").val(hidimg);
            $("#hidTitle").val(hidtitle);
            $("#hidImg_pro").val(hidimg_pro);
            $("#hidTitle_pro").val(hidtitle_pro);
        }
        function bindImg() {
            var hidimg = $("#" + _hidImg).val().split(",");
            var hidtitle = $("#" + _hidTitle).val().split(",");
            var hidtemp1 = "";
            var hidtemp2 = "";
            var temphtml = "";
            for (var i = 0; i < hidimg.length - 1; i++) {
                temphtml += '<div class="uploadImg"><div><img src="' + hidimg[i] + '" /></div><textarea type="text" class="input" placeholder="输入图片描述">' + hidtitle[i] + '</textarea><a href="javascript:void(0)" onclick="deleteImg(' + i + ',' + _img_type + ')">X</a></div>';
                hidtemp1 += hidimg[i] + ",";
                hidtemp2 += hidtitle[i] + ",";
            }
            $("#" + _divHImg).html(temphtml);
            $("#" + _hidImg).val(hidtemp1);
            $("#" + _hidTitle).val(hidtemp2);
        }

        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
            //初始化上传控件
            $(".upload-img").each(function () {
                $(this).InitSWFUpload({ sendurl: "../../tools/upload_ajax.ashx", flashurl: "../js/swfupload/swfupload.swf" });
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
        <a href="List.aspx"><span>图册列表</span></a>
        <i class="arrow"></i>
        <span>发布图册</span>
    </div>
    <div class="line10"></div>
    <!--/导航栏-->

    <!--内容-->
    <div class="content-tab-wrap">
        <div id="floatHead" class="content-tab">
            <div class="content-tab-ul-wrap">
                <ul>
                    <li><a href="javascript:;" onclick="tabs(this);" class="selected">发布图册</a></li>
                </ul>
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
        <div class="tab-content">
            <dl>
                <dt>标题</dt>
                <dd>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*1-100" nullmsg="请输入图册标题" errormsg="图册标题在1-100个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>关键词</dt>
                <dd>
                    <asp:TextBox ID="txtKeyword" runat="server" CssClass="input normal" datatype="*0-50" nullmsg="请输入关键词" errormsg="关键词在0-50个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>图册描述</dt>
                <dd>
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="input normal" datatype="*0-200" errormsg="图册简述在0-200个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>作者</dt>
                <dd>
                    <asp:TextBox ID="txtAuthor" runat="server" CssClass="input normal" datatype="*0-50" nullmsg="请输入作者" errormsg="作者在0-50个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>所属分类</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlClassId" datatype="*" nullmsg="请选择图册分类" sucmsg=" " runat="server"></asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>图册LOGO</dt>
                <dd>
                    <asp:TextBox ID="txtImgUrl" runat="server" datatype="*1-250" CssClass="input normal upload-path" nullmsg="请上传图册Logo" errormsg="图册Logo 1-250个字符间" sucmsg=" " />
                    <div class="upload-box upload-img"></div>
                    <span class="Validform_checktip">尺寸440*300px</span>
                </dd>
            </dl>
            <dl>
                <dt>图片集合</dt>
                <dd>
                    <button onclick="showUpImg(1);" type="button" class="btn">点击上传图片</button><span>（尺寸600*410px）</span><br />
                    <div id="divHImg"></div>
                    <input type="hidden" id="hidImg" name="hidImg" value="" runat="server" />
                    <input type="hidden" id="hidTitle" name="hidImg" value="" runat="server" />
                </dd>
            </dl>
            <dl>
                <dt>图册属性</dt>
                <dd>
                    <div class="rule-multi-porp">
                        <asp:CheckBoxList ID="cblItem" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="1">置顶</asp:ListItem>
                            <asp:ListItem Value="1">锁定</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>浏览次数</dt>
                <dd>
                    <asp:TextBox ID="txtClick" Text="0" runat="server" CssClass="input normal" datatype="/^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/" errormsg="请输入正确的浏览次数" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
        </div>
        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSave" runat="server" Text="保存图册" CssClass="btn" OnClientClick="bindData();" OnClick="btnSave_Click" />
                <asp:Button ID="btnReset" runat="server" Text="清空信息" CssClass="btn yellow" OnClick="btnReset_Click" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
