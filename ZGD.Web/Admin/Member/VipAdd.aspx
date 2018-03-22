<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VipAdd.aspx.cs" ValidateRequest="false" Inherits="ZGD.Web.Admin.Member.VipAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>发布VIP卡</title>
    <script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
        });
    </script>
</head>
<body class="mainbody">
    <!--导航栏-->
    <div class="location">
        <a href="VipList.aspx" class="back"><i></i><span>返回列表</span></a>
        <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
        <i class="arrow"></i>
        <a href="List.aspx"><span>会员管理</span></a>
        <i class="arrow"></i>
        <span>发布VIP卡</span>
    </div>
    <div class="line10"></div>
    <!--/导航栏-->

    <form id="form1" runat="server">
        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">发布VIP卡</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <dl>
                <dt>卡位数</dt>
                <dd>
                    <asp:TextBox ID="txtCount" runat="server" CssClass="input normal" datatype="*1-6" nullmsg="请输入卡位数" errormsg="卡位数在1-6位数" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>起始号码</dt>
                <dd>
                    <asp:TextBox ID="txtStart" runat="server" CssClass="input normal" datatype="*1-10" nullmsg="请输入起始号码" errormsg="起始号码在1-10位数" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>结束号码</dt>
                <dd>
                    <asp:TextBox ID="txtEnd" runat="server" CssClass="input normal" datatype="*1-10" nullmsg="请输入结束号码" errormsg="结束号码在1-10位数" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>排除号码</dt>
                <dd>
                    <asp:TextBox ID="txtElse" runat="server" CssClass="input normal" datatype="*1-10" nullmsg="请输入排除号码" errormsg="排除号码在1-10位数" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>已生成号码</dt>
                <dd>
                    <asp:TextBox ID="txtCode" runat="server" TextMode="MultiLine" CssClass="input normal" Height="150"></asp:TextBox>
                </dd>
            </dl>
        </div>
        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn" OnClick="btnSave_Click" />
                <asp:Button ID="btnReset" runat="server" Text="重置" CssClass="btn yellow" OnClick="btnReset_Click" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
