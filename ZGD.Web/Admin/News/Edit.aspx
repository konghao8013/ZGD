﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="ZGD.Web.Admin.News.Edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑文章</title>
    <link rel="stylesheet" href="../skin/default/style.css" type="text/css" />
    <link rel="stylesheet" href="../js/kindeditor/themes/default/default.css" />
    <link rel="stylesheet" href="../js/kindeditor/plugins/code/prettify.css" />
    <link rel="stylesheet" href="../Js/datepicker/skin/WdatePicker.css" />
    <style type="text/css">
        #imgPanel img { width: 130px; margin-bottom: 5px; }
    </style>

    <script type="text/javascript" src="/content/scripts/jquery-3.7.1.min.js"></script><script type="text/javascript" src="/content/scripts/jquery-migrate-1.4.1.js"></script>
    <script type="text/javascript" src="../js/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../js/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="../js/swfupload/swfupload.handlers.js"></script>
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
            $(".upload-img").each(function () {
                $(this).InitSWFUpload({ sendurl: "../../tools/upload_ajax.ashx", flashurl: "../js/swfupload/swfupload.swf" });
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
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*3-120" nullmsg="请输入文章标题" errormsg="标题长度在3-100个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>发布时间</dt>
                <dd>
                    <asp:TextBox ID="txtPubTime" runat="server" CssClass="input" datatype="*" onfocus="WdatePicker({dateFmt:&#39;yyyy-MM-dd&#39;})" nullmsg="请输入文章发布时间" errormsg="错误的时间格式" sucmsg=" "></asp:TextBox>
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
                    <span>多关键词，请用英文输入法中的逗号“,”分隔。</span>
                </dd>
            </dl>
            <dl style="display:none;">
                <dt>文章标签</dt>
                <dd>
                    <asp:TextBox ID="txtTags" runat="server" CssClass="input normal" datatype="*0-120" nullmsg="请输入文章标签" errormsg="标签长度在2-100个字符间" sucmsg=" "></asp:TextBox>
                    <span>多标签，请用英文输入法中的逗号“,”分隔。</span>
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
                    <asp:TextBox ID="txtAuthor" runat="server" Text="致公党" CssClass="input normal" datatype="*2-10" nullmsg="请输入文章发布人" errormsg="发布人长度在2-10个字符间" sucmsg=" "></asp:TextBox>
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
            <dl class="upordown" id="tr_img_panel" runat="server" style="display:none">
                <dt>缩略图上传</dt>
                <dd>
                    <p id="imgPanel" runat="server"></p>
                    <asp:TextBox ID="txtImgUrl" runat="server" datatype="*0-250" nullmsg="请输入文章缩略图" CssClass="input normal upload-path" />
                    <div class="upload-box upload-img"></div>
                    <span id="img_size">（<%=size %>）</span>
                    <script>
                        $(function () {
                            $(".select-items li").click(function () {
                                if ($(this).html() != "请选择专题") {
                                    $("#img_size").html("图片尺寸400*100px");
                                }
                                else {
                                    $("#img_size").html("图片尺寸400*280px");
                                }
                            })
                        })
                    </script>
                </dd>
            </dl>
            <dl class="upordown">
                <dt>附件上传</dt>
                <dd>
                    <div id="fileDiv" runat="server"></div>
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
                        <asp:CheckBoxList ID="cblItem" runat="server"
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
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
