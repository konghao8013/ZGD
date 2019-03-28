<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="content.aspx.cs" Inherits="ZGD.Web.Admin.Contents.content" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>公司简介</title>
    <link rel="stylesheet" href="../skin/default/style.css" type="text/css" />
    <link rel="stylesheet" href="../js/kindeditor/themes/default/default.css" />
    <link rel="stylesheet" href="../js/kindeditor/plugins/code/prettify.css" />

    <script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../js/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <script type="text/javascript" src="../js/kindeditor/kindeditor-min.js"></script>
    <script type="text/javascript" src="../js/kindeditor/lang/zh_CN.js"></script>
    <script type="text/javascript" src="../js/kindeditor/plugins/code/prettify.js"></script>

    <script type="text/javascript">
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
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <a href="#"><span>单页内容</span></a>
            <i class="arrow"></i>
            <span><%=typeName%></span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected"><%=typeName%></a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="tab-content">
            <dl>
                <dt>内容类型</dt>
                <dd>
                    <asp:RadioButtonList ID="cbType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" AutoPostBack="True"
                        OnSelectedIndexChanged="cbType_SelectedIndexChanged" CellPadding="5" CellSpacing="5">
                        <asp:ListItem Text="集团介绍" Value="About" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="公司荣誉" Value="Rongyu"></asp:ListItem>
                        <asp:ListItem Text="公司文化" Value="WenHua"></asp:ListItem>
                        <asp:ListItem Text="公司架构" Value="JiaGou"></asp:ListItem>
                        <asp:ListItem Text="服务流程" Value="LiuCheng"></asp:ListItem>
                        <asp:ListItem Text="材料展示" Value="CaiLiao"></asp:ListItem>
                        <asp:ListItem Text="合作伙伴" Value="HuoBan"></asp:ListItem>
                        <asp:ListItem Text="人才招聘" Value="Job"></asp:ListItem>
                        <asp:ListItem Text="联系我们" Value="Contact"></asp:ListItem>
                        <asp:ListItem Text="超国标" Value="GuoBiao"></asp:ListItem>
                        <asp:ListItem Text="工程工艺" Value="GongYi"></asp:ListItem>
                        <asp:ListItem Text="验收体系" Value="TiXi"></asp:ListItem>
                        <asp:ListItem Text="在建工地" Value="GongDi"></asp:ListItem>
                    </asp:RadioButtonList>
                </dd>
            </dl>
            <dl>
                <dt>内容类型</dt>
                <dd>
                    <textarea id="kEditor" cols="100" rows="8" style="width: 90%; height: 400px; visibility: hidden;" runat="server"></textarea>
                </dd>
            </dl>
        </div>

        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSave" runat="server" Text="确认保存" CssClass="btn" OnClick="btnSave_Click" />&nbsp;&nbsp; 
              <asp:Button ID="btnretset" runat="server" Text="重置信息" CssClass="btn yellow" OnClick="btnretset_Click" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
