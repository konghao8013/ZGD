<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="ZGD.Web.Admin.News.Edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑文章</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.handlers.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/js/kindeditor-4.1.7/themes/default/default.css" />
    <link rel="stylesheet" href="/js/kindeditor-4.1.7/plugins/code/prettify.css" />
    <style type="text/css">
        #imgPanel img{width:130px;margin-bottom:5px;}
    </style>

    <script type="text/javascript" src="/js/kindeditor-4.1.7/kindeditor-min.js"></script>
    <script type="text/javascript" src="/js/kindeditor-4.1.7/lang/zh_CN.js"></script>
    <script type="text/javascript" src="/js/kindeditor-4.1.7/plugins/code/prettify.js"></script>
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
            //初始化上传控件
            $(".upload-img").each(function () {
                $(this).InitSWFUpload({ sendurl: "../../tools/upload_ajax.ashx", flashurl: "../../scripts/swfupload/swfupload.swf" });
            });
            $("#cbIsImage").bind("click", function () {
                if ($(this).prop("checked")) {
                    $(".upordown").show();
                } else {
                    $(".upordown").hide();
                }
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
        <a href="List.aspx"><span>文章列表</span></a>
        <i class="arrow"></i>
        <span>编辑文章</span>
    </div>
    <div class="line10"></div>
    <!--/导航栏-->
    <form id="form1" runat="server">
        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">编辑文章</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <dl>
                <dt>文章标题</dt>
                <dd>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*2-100" nullmsg="请输入文章标题" errormsg="标题长度在3-100个字符间" sucmsg=" "></asp:TextBox>
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
                <dt>文章标签</dt>
                <dd>
                    <asp:TextBox ID="txtTags" runat="server" CssClass="input normal" datatype="*3-120" nullmsg="请输入文章标签" errormsg="标签长度在2-100个字符间" sucmsg=" "></asp:TextBox>
                    <span>多个标签，请用英文输入法中的逗号“,”分隔。</span>
                </dd>
            </dl>
            <%--<dl>
                <dt>装修阶段</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlZxType" runat="server" datatype="*" nullmsg="请选择装修阶段" sucmsg=" ">
                            <asp:ListItem Value="0">请选择装修阶段</asp:ListItem>
                            <asp:ListItem Value="1">装修准备</asp:ListItem>
                            <asp:ListItem Value="2">装修进行中</asp:ListItem>
                            <asp:ListItem Value="3">装修完成</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </dd>
            </dl>--%>
            <dl>
                <dt>文章类型</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlClassId" runat="server" datatype="*" nullmsg="请选择文章类型" sucmsg=" "></asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>发布人</dt>
                <dd>
                    <asp:TextBox ID="txtAuthor" runat="server" Text="港宏宜家装饰" CssClass="input normal" datatype="*2-10" nullmsg="请输入文章发布人" errormsg="发布人长度在2-10个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl style="display:none;">
                <dt>设置缩略图</dt>
                <dd>
                    <div class="rule-single-checkbox">
                        <asp:CheckBox ID="cbIsImage" Checked="true" runat="server" Text="图片链接" />
                    </div>
                </dd>
            </dl>
            <dl class="upordown" id="tr_img_panel" runat="server">
                <dt>缩略图上传</dt>
                <dd>
                    <p id="imgPanel" runat="server"></p>
                    <asp:TextBox ID="txtImgUrl" runat="server" datatype="*1-250" nullmsg="请输入文章缩略图" CssClass="input normal upload-path" />
                    <div class="upload-box upload-img"></div>
                    （图片尺寸400*273px）
                </dd>
            </dl>
            <dl>
                <dt>简要描述</dt>
                <dd>
                    <textarea id="txtDesc" runat="server" class="input normal" style="height: 50px"></textarea>
                </dd>
            </dl>
            <dl>
                <dt>文章详情</dt>
                <dd>
                    <textarea id="kEditor" cols="100" rows="8" style="width: 700px; height: 350px; visibility: hidden;" runat="server"></textarea>
                </dd>
            </dl>
            <dl>
                <dt>文章属性</dt>
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
