<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="ZGD.Web.Admin.Activity.Add" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>发布活动</title>
    <script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../js/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="../js/swfupload/swfupload.handlers.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <script src="../js/datepicker/WdatePicker.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../js/datepicker/skin/WdatePicker.css" rel="stylesheet" />
    <link rel="stylesheet" href="/js/kindeditor-4.1.7/themes/default/default.css" />
    <link rel="stylesheet" href="/js/kindeditor-4.1.7/plugins/code/prettify.css" />

    <script type="text/javascript" src="/js/kindeditor-4.1.7/kindeditor-min.js"></script>
    <script type="text/javascript" src="/js/kindeditor-4.1.7/lang/zh_CN.js"></script>
    <script type="text/javascript" src="/js/kindeditor-4.1.7/plugins/code/prettify.js"></script>

    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
            //初始化上传控件
            $(".upload-img").each(function () {
                $(this).InitSWFUpload({ sendurl: "../../tools/upload_ajax.ashx", flashurl: "../js/swfupload/swfupload.swf" });
            });
        });
        KindEditor.ready(function (K) {
            var editor1 = K.create('#kEditor', {
                cssPath: '/js/kindeditor-4.1.7/plugins/code/prettify.css',
                uploadJson: '/Tools/editer_upfiles.ashx',
                fileManagerJson: '/Tools/file_manager.ashx',
                allowFileManager: true,
                afterCreate: function () {
                    var self = this;
                    K.ctrl(document, 13, function () {
                        self.sync();
                        K('form[name=form1]')[0].submit();
                    });
                    K.ctrl(self.edit.doc, 13, function () {
                        self.sync();
                        K('form[name=form1]')[0].submit();
                    });
                }
            });
            prettyPrint();
        });
    </script>
</head>
<body class="mainbody">
    <!--导航栏-->
    <div class="location">
        <a href="List.aspx" class="back"><i></i><span>返回列表</span></a>
        <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
        <i class="arrow"></i>
        <a href="List.aspx"><span>活动列表</span></a>
        <i class="arrow"></i>
        <span>发布活动</span>
    </div>
    <div class="line10"></div>
    <!--/导航栏-->
    <!--内容-->
    <div class="content-tab-wrap">
        <div id="floatHead" class="content-tab">
            <div class="content-tab-ul-wrap">
                <ul>
                    <li><a href="javascript:;" onclick="tabs(this);" class="selected">发布活动</a></li>
                </ul>
            </div>
        </div>
    </div>

    <form id="form1" runat="server">
        <div class="tab-content">
            <dl>
                <dt>活动标题</dt>
                <dd>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*1-50" nullmsg="请输入活动标题" errormsg="标题长度在1-100个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>文章关键词</dt>
                <dd>
                    <asp:TextBox ID="txtKeyword" runat="server" CssClass="input normal" datatype="*3-120" nullmsg="请输入文章关键词" errormsg="标签长度在2-100个字符间" sucmsg=" "></asp:TextBox>
                    <span>多个关键词，请用英文输入法中的逗号“,”分隔。</span>
                </dd>
            </dl>
            <dl>
                <dt>简要描述</dt>
                <dd>
                    <textarea id="txtDesc" runat="server" class="input normal" style="height: 150px"></textarea>
                </dd>
            </dl>
            <dl>
                <dt>活动时间</dt>
                <dd>
                    <asp:TextBox ID="txtsDate" runat="server" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" CssClass="input" nullmsg="请输入活动开始时间" errormsg="错误的时间格式" sucmsg=" "></asp:TextBox> ~
                    <asp:TextBox ID="txteDate" runat="server" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" CssClass="input" nullmsg="请输入活动结束时间" errormsg="错误的时间格式" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>缩略图上传</dt>
                <dd>
                    <asp:TextBox ID="txtImgUrl" runat="server" datatype="*1-250" nullmsg="请输入活动缩略图" CssClass="input normal upload-path" />
                    <div class="upload-box upload-img"></div>
                    （图片尺寸340*228px）
                </dd>
            </dl>
            <dl>
                <dt>活动链接</dt>
                <dd>
                    <asp:TextBox ID="txtActivityUrl" runat="server" CssClass="input normal" datatype="*0-200" ></asp:TextBox>（该链接为空则读取本站链接）
                </dd>
            </dl>
            <dl>
                <dt>新闻详情</dt>
                <dd>
                    <textarea id="kEditor" cols="100" rows="8" style="width: 700px; height: 350px; visibility: hidden;" runat="server"></textarea>
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
                <asp:Button ID="btnretset" runat="server" class="btn yellow" Text="重置" OnClick="btnretset_Click" />
            </div>
        </div>
    </form>
</body>
</html>
