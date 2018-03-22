<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="ZGD.Web.Admin.ST.Add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>增加客服</title>
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
        <a href="List.aspx" class="back"><i></i><span>返回列表</span></a>
        <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
        <i class="arrow"></i>
        <a href="List.aspx"><span>客服列表</span></a>
        <i class="arrow"></i>
        <span>增加客服</span>
    </div>
    <div class="line10"></div>
    <!--/导航栏-->

    <form id="form1" runat="server">
        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">增加客服</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="tab-content">
            <dl>
                <dt>客服类型</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlClassId" runat="server" datatype="*" nullmsg="请选择客服类型" sucmsg=" ">
                            <asp:ListItem Value="0" Text="请选择客服类型"></asp:ListItem>
                            <asp:ListItem Value="1" Text="QQ" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>客服标题</dt>
                <dd>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*1-50" nullmsg="请输入客服标题" errormsg="客服标题在1-50个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>客服号码</dt>
                <dd>
                    <asp:TextBox ID="txtNum" runat="server" CssClass="input normal" Text="#" datatype="*1-250" nullmsg="请输入客服号码" errormsg="客服号码在1-50个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>是否禁用</dt>
                <dd>
                    <div class="rule-single-checkbox">
                        <asp:CheckBox ID="cbStatus" runat="server" Text="是否禁用" />
                    </div>
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
