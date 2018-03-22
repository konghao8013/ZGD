<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="ZGD.Web.Admin.Links.Add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>增加链接</title>
    <script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../js/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="../js/swfupload/swfupload.handlers.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
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
    </script>
</head>
<body class="mainbody">
    <!--导航栏-->
    <div class="location">
        <a href="List.aspx" class="back"><i></i><span>返回列表</span></a>
        <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
        <i class="arrow"></i>
        <a href="List.aspx"><span>链接列表</span></a>
        <i class="arrow"></i>
        <span>增加链接</span>
    </div>
    <div class="line10"></div>
    <!--/导航栏-->

    <form id="form1" runat="server">
        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">增加链接</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="tab-content">
            <dl>
                <dt>链接类型</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlClassId" runat="server" datatype="*" nullmsg="请选择链接类型" sucmsg=" "></asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>链接标题</dt>
                <dd>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*1-120" nullmsg="请输入链接标题" errormsg="链接标题在3-120个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>链接地址</dt>
                <dd>
                    <asp:TextBox ID="txtWebUrl" runat="server" CssClass="input normal" Text="#" datatype="*1-250" nullmsg="请输入链接地址" errormsg="链接地址在1-250个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>设置图片</dt>
                <dd>
                    <div class="rule-single-checkbox">
                    <asp:CheckBox ID="cbIsImage" runat="server" Text="图片链接" />
                    </div>
                </dd>
            </dl>
            <dl class="upordown" style="display: none">
                <dt>图片上传</dt>
                <dd>
                    <asp:TextBox ID="txtImgUrl" runat="server" datatype="*0-250" CssClass="input normal upload-path" />
                    <div class="upload-box upload-img"></div>
                    （图片尺寸200*80px）
                </dd>
            </dl>
            <dl>
                <dt>联系人</dt>
                <dd>
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="input normal" datatype="*0-20" errormsg="联系人在1-20个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>联系电话</dt>
                <dd>
                    <asp:TextBox ID="txtUserTel" runat="server" CssClass="input normal" datatype="*0-20" errormsg="联系电话在1-20个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>联系邮箱</dt>
                <dd>
                    <asp:TextBox ID="txtUserMail" runat="server" CssClass="input normal" datatype="*0-200" errormsg="联系邮箱在1-200个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>链接属性</dt>
                <dd>
                    <asp:CheckBoxList ID="cblItem" runat="server"
                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="1">推荐到首页</asp:ListItem>
                        <asp:ListItem Value="1">隐藏</asp:ListItem>
                    </asp:CheckBoxList>
                </dd>
            </dl>
            <dl>
                <dt>联系邮箱</dt>
                <dd>
                    <asp:TextBox ID="txtSortId" runat="server" CssClass="input normal" Text="0"></asp:TextBox>
                </dd>
            </dl>
        </div>
        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
            <asp:Button ID="btnSave" runat="server" Text="确认保存" CssClass="btn" OnClick="btnSave_Click" />
            <asp:Button ID="Button1" runat="server" CssClass="btn yellow" OnClick="Button1_Click" Text="重置" Width="80px" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
