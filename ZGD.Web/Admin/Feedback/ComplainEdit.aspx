<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComplainEdit.aspx.cs" Inherits="ZGD.Web.Admin.Feedback.ComplainEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>投诉记录</title>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
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
        <a href="complain.aspx" class="back"><i></i><span>返回列表</span></a>
        <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
        <i class="arrow"></i>
        <a href="complain.aspx"><span>投诉记录</span></a>
        <i class="arrow"></i>
        <span>投诉详情</span>
    </div>
    <div class="line10"></div>
    <!--/导航栏-->
    <form id="form1" runat="server">

        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">投诉详情</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="tab-content">
            <dl>
                <dt>投诉类型</dt>
                <dd>
                    <asp:DropDownList ID="ddlType" runat="server" CssClass="select">
                        <asp:ListItem Value="0">所有投诉</asp:ListItem>
                        <asp:ListItem Value="1">设计投诉</asp:ListItem>
                        <asp:ListItem Value="2">施工投诉</asp:ListItem>
                        <asp:ListItem Value="3">其他投诉</asp:ListItem>
                    </asp:DropDownList>
                </dd>
            </dl>
            <dl>
                <dt>投诉时间</dt>
                <dd>
                    <asp:TextBox ID="txtPubTime" ReadOnly="true" runat="server" CssClass="input"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>联系人</dt>
                <dd>
                    <asp:TextBox ID="txtName" ReadOnly="true" runat="server" CssClass="input"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>联系电话</dt>
                <dd>
                    <asp:TextBox ID="txtPhone" ReadOnly="true" runat="server" CssClass="input"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>小区</dt>
                <dd>
                    <asp:TextBox ID="txtArea" ReadOnly="true" runat="server" CssClass="input"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>投诉内容</dt>
                <dd>
                    <asp:TextBox ID="txtCon" ReadOnly="true" TextMode="MultiLine" runat="server" CssClass="input" Style="width: 500px; height: 80px;"></asp:TextBox>
                </dd>
            </dl>
        </div>

        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnretset" runat="server" Text="返回" CssClass="btn yellow" OnClick="btnretset_Click" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>

</body>
</html>
