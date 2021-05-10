<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" ValidateRequest="false" Inherits="ZGD.Web.Admin.News.Add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>发布文章</title>
    <link rel="stylesheet" href="../skin/default/style.css" type="text/css" />
    <link rel="stylesheet" href="../js/kindeditor/themes/default/default.css" />
    <link rel="stylesheet" href="../js/kindeditor/plugins/code/prettify.css" />
    <link rel="stylesheet" href="../Js/datepicker/skin/WdatePicker.css" />

    <script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery/Validform_v5.3.2_min.js"></script>
    <link href="../Js/webuploader-0.1.5/webuploader.css" rel="stylesheet" />
    <script type="text/javascript" src="../Js/webuploader-0.1.5/webuploader.min.js"></script>
    <script type="text/javascript" src="../Js/uploadimg.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <script type="text/javascript" src="../Js/datepicker/WdatePicker.js"></script>


    <script type="text/javascript" src="../js/kindeditor/kindeditor-min.js?v=20200101"></script>
    <script type="text/javascript" src="../js/kindeditor/lang/zh_CN.js"></script>
    <script type="text/javascript" src="../js/kindeditor/plugins/code/prettify.js"></script>
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
            //初始化上传控件
            imgMethod.init('uploader-list', 'filePicker', 'txtImgUrl', 'event', 1, "back_", '', function (file, resp) {
                return $('<div class="file-box" id="file_back_f1"><div class="file">' +
                    '<div id="f1" class="file-item thumbnail">' +
                    '<img id="img_back_f1" src="' + resp.url + '" data-src="' + resp.url + '" class="img-responsive"><p class="progress img_up">上传排队中</p></div></div></div>');
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
        <a href="<%=url %>" class="back"><i></i><span>返回列表</span></a>
        <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
        <i class="arrow"></i>
        <a href="<%=url %>"><span>文章列表</span></a>
        <i class="arrow"></i>
        <span>发布文章</span>
    </div>
    <div class="line10"></div>
    <!--/导航栏-->
    <form id="form1" runat="server">
        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">发布文章</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <dl>
                <dt>文章标题</dt>
                <dd>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*3-120" nullmsg="请输入文章标题" errormsg="标题长度在3-100个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>发布时间</dt>
                <dd>
                    <asp:TextBox ID="txtPubTime" runat="server" CssClass="input normal" datatype="*" onfocus="WdatePicker({dateFmt:&#39;yyyy-MM-dd&#39;})" nullmsg="请输入文章发布时间" errormsg="错误的时间格式" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl style="display:none;">
                <dt>文章副标题</dt>
                <dd>
                    <asp:TextBox ID="txtSubTitle" runat="server" CssClass="input normal" datatype="*0-120" nullmsg="请输入文章副标题" errormsg="副标题长度在3-100个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl style="display:none;">
                <dt>文章关键词</dt>
                <dd>
                    <asp:TextBox ID="txtKeyword" runat="server" CssClass="input normal" datatype="*0-120" nullmsg="请输入文章关键词" errormsg="标签长度在2-100个字符间" sucmsg=" "></asp:TextBox>
                    <span>多个关键词，请用英文输入法中的逗号“,”分隔。</span>
                </dd>
            </dl>
            <dl style="display:none;">
                <dt>文章标签</dt>
                <dd>
                    <asp:TextBox ID="txtTags" runat="server" CssClass="input normal" datatype="*0-120" nullmsg="请输入文章标签" errormsg="标签长度在2-100个字符间" sucmsg=" "></asp:TextBox>
                    <span>多个标签，请用英文输入法中的逗号“,”分隔。</span>
                </dd>
            </dl>
            <dl>
                <dt>所属版块</dt>
                <dd>
                    <div class="rule-multi-porp">
                        <asp:CheckBoxList ID="ddlClassId" runat="server" Width="600" RepeatLayout="Flow"></asp:CheckBoxList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>所属专题</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlZtId" runat="server"></asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>发布人</dt>
                <dd>
                    <asp:TextBox ID="txtAuthor" runat="server" Text="渝致宣" CssClass="input normal" datatype="*2-10" nullmsg="请输入文章发布人" errormsg="发布人长度在2-10个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl style="display:none">
                <dt>设置Logo</dt>
                <dd>
                    <div class="rule-single-checkbox">
                        <asp:CheckBox ID="cbIsImage" Checked="true" runat="server" Text="图片链接" />
                    </div>
                </dd>
            </dl>
            <dl class="upordown" style="display:none">
                <dt>缩略图上传</dt>
                <dd>
                    <div id="uploader-demo">
                        <div id="filePicker">选择图片</div>
                        <p class="uploader-line">注：尺寸340*228px</p>
                        <div id="uploader-list" class="uploader-list"></div>
                    </div>
                    <input id="txtImgUrl" type="hidden" name="txtImgUrl" runat="server" />
                </dd>
            </dl>
            <dl class="upordown">
                <dt>附件上传</dt>
                <dd>
                    <input type="file" runat="server" name="files" class="input normal" />
                </dd>
            </dl>
            <dl style="display:none;">
                <dt>简要描述</dt>
                <dd>
                    <textarea id="txtDesc" runat="server" class="input normal" style="width: 400px; height: 80px"></textarea>
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
                    <div class="rule-multi-porp">
                        <asp:CheckBoxList ID="cblItem" runat="server" RepeatLayout="Flow">
                            <%--<asp:ListItem Value="1">禁用</asp:ListItem>--%>
                            <asp:ListItem Value="1">置顶</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </dd>
            </dl>
            <dl style="display:none;">
                <dt>浏览次数</dt>
                <dd>
                    <asp:TextBox ID="txtClick" runat="server" CssClass="input normal" datatype="*">0</asp:TextBox>
                </dd>
            </dl>
        </div>

        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSave" runat="server" Text="保存（发布）" CssClass="btn" OnClick="btnSave_Click" />
                <asp:Button ID="btnReset" runat="server" Text="保存（临时）" CssClass="btn yellow" OnClick="btnReset_Click" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
