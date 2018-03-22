<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" ValidateRequest="false" Inherits="ZGD.Web.Admin.Des.Add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>发布设计师</title>
    <script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../js/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="../js/swfupload/swfupload.handlers.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/js/kindeditor-4.1.7/themes/default/default.css" />
    <link rel="stylesheet" href="/js/kindeditor-4.1.7/plugins/code/prettify.css" />
    <style type="text/css">
        #divHImg { display: inline-block; height: auto; }
        .uploadImg { width: 130px; height: 145px; float: left; margin: 10px 10px 0 0; position: relative; }
        .uploadImg a { width: 15px; height: 15px; line-height: 15px; position: absolute; text-align: center; display: inline-block; top: 4px; right: 2px; background-color: #fff; color: #ff0000; font-weight: bold; font-size: 14px; }
        .uploadImg div { width: 120px; text-align: center; border: solid 1px #ccc; padding: 5px; margin-bottom: 3px; }
        .uploadImg img { width: 75px; height: 110px; }
        .uploadImg .input { width: 125px; height: 16px; min-height: 16px; line-height: 16px; padding-left: 1px; }
        #imgPanel { margin-bottom: 5px; }
        #imgPanel img { width: 75px; }
    </style>
    <script type="text/javascript" src="/js/kindeditor-4.1.7/kindeditor-min.js"></script>
    <script type="text/javascript" src="/js/kindeditor-4.1.7/lang/zh_CN.js"></script>
    <script type="text/javascript" src="/js/kindeditor-4.1.7/plugins/code/prettify.js"></script>
    <script type="text/javascript" src="/scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
            //初始化上传控件
            $(".upload-img").each(function () {
                $(this).InitSWFUpload({ sendurl: "../../tools/upload_ajax.ashx", flashurl: "../js/swfupload/swfupload.swf" });
            });
        });
        var editor;
        KindEditor.ready(function (K) {
            editor = K.create('textarea[class="keditor"]', {
                resizeType: 1,
                allowPreviewEmoticons: false,
                allowImageUpload: false,
                items: [
                    'source', '|', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
                    'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
                    'insertunorderedlist', '|', 'emoticons', 'link']
            });
        });
    </script>
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
            $.each($("#" + _divHImg + " input"), function (i, n) {
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
            $.each($("#divHImg input"), function (i, n) {
                hidtitle += $(n).val() + ',';
            });
            $.each($("#divHImg_pro img"), function (i, n) {
                hidimg_pro += $(n).attr("src") + ',';
            });
            $.each($("#divHImg_pro input"), function (i, n) {
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
                temphtml += '<div class="uploadImg"><div><img src="' + hidimg[i] + '" /></div><input type="text" class="input" value="' + hidtitle[i] + '" /><a href="javascript:void(0)" onclick="deleteImg(' + i + ',' + _img_type + ')">X</a></div>';
                hidtemp1 += hidimg[i] + ",";
                hidtemp2 += hidtitle[i] + ",";
            }
            $("#" + _divHImg).html(temphtml);
            $("#" + _hidImg).val(hidtemp1);
            $("#" + _hidTitle).val(hidtemp2);
        }
    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="List.aspx" class="back"><i></i><span>返回列表</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <a href="List.aspx"><span>设计师管理</span></a>
            <i class="arrow"></i>
            <span>发布设计师</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">设计师详细</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <dl>
                <dt>设计师名称</dt>
                <dd>
                    <asp:TextBox ID="txtName" runat="server" CssClass="input normal" datatype="*1-10" nullmsg="请输入设计师名称" errormsg="设计师名称在1-10个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>设计师星级</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlStar" runat="server">
                            <asp:ListItem Text="请选择设计师星级" Value="0"></asp:ListItem>
                            <asp:ListItem Text="5星" Value="5"></asp:ListItem>
                            <asp:ListItem Text="4星" Value="4"></asp:ListItem>
                            <asp:ListItem Text="3星" Value="3"></asp:ListItem>
                            <asp:ListItem Text="2星" Value="2"></asp:ListItem>
                            <asp:ListItem Text="1星" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>设计师职称</dt>
                <dd>
                    <asp:TextBox ID="txtcName" runat="server" CssClass="input normal" datatype="*0-50" errormsg="设计师职称在1-50个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>设计师头像</dt>
                <dd>
                    <asp:TextBox ID="txtImgUrl" runat="server" datatype="*1-250" CssClass="input normal upload-path" />
                    <div class="upload-box upload-img"></div>
                    （尺寸300*500px）
                </dd>
            </dl>
            <dl>
                <dt>毕业学校</dt>
                <dd>
                    <asp:TextBox ID="txtSchool" runat="server" CssClass="input normal" datatype="*0-50" errormsg="毕业学校在1-200个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>从业年限</dt>
                <dd>
                    <asp:TextBox ID="txtCyDate" runat="server" CssClass="input normal" datatype="/^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/" Width="120" errormsg="请输入正确的从业年限" sucmsg=" "></asp:TextBox>
                    年
                </dd>
            </dl>
            <dl>
                <dt>设计理念</dt>
                <dd>
                    <textarea id="txtTitle" runat="server" class="input" rows="50" cols="100"></textarea>
                </dd>
            </dl>
            <dl style="display:none;">
                <dt>获奖状况</dt>
                <dd>
                    <asp:TextBox ID="txtRongyu" runat="server" CssClass="input normal" datatype="*0-50" errormsg="获奖状况在1-200个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl style="display:none;">
                <dt>擅长设计</dt>
                <dd>
                    <asp:TextBox ID="txtGoodAtDes" runat="server" CssClass="input normal" datatype="*0-200" errormsg="" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>设计风格</dt>
                <dd>
                    <asp:TextBox ID="txtGoodAt" runat="server" CssClass="input normal" datatype="*0-200" errormsg="" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>排序号</dt>
                <dd>
                    <asp:TextBox ID="txtSort" Text="0" runat="server" CssClass="input normal" datatype="/^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/" errormsg="请输入正确的排序号" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <%--<dl style="display:none;">
                <dt>相册图集</dt>
                <dd>
                    <a href="javascript:;" onclick="showUpImg(1);" style="font-weight: 600; color: Red; text-decoration: underline">点击设计师相册</a><span>（尺寸340*500px）</span><br />
                    <div id="divHImg"></div>
                    <input type="hidden" id="hidImg" name="hidImg" value="" runat="server" />
                    <input type="hidden" id="hidTitle" name="hidImg" value="" runat="server" />
                </dd>
            </dl>--%>
            <dl>
                <dt>置顶属性</dt>
                <dd>
                    <div class="rule-single-checkbox">
                        <asp:CheckBox ID="cblItem" runat="server" Text="置顶" />
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>人气</dt>
                <dd>
                    <asp:TextBox ID="txtClick" runat="server" CssClass="input normal" datatype="*">0</asp:TextBox>
                </dd>
            </dl>
        </div>
        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSave" runat="server" Text="保 存" CssClass="btn" OnClick="btnSave_Click" />
                <asp:Button ID="btnReset" runat="server" Text="重 置" CssClass="btn yellow" OnClick="btnReset_Click" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
