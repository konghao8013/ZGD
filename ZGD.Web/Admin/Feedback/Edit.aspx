<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="ZGD.Web.Admin.Feedback.Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>回复留言</title>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/content/scripts/jquery-3.7.1.min.js"></script><script type="text/javascript" src="/content/scripts/jquery-migrate-1.4.1.js"></script>
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
        <a href="List.aspx" class="back"><i></i><span>返回列表</span></a>
        <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
        <i class="arrow"></i>
        <a href="List.aspx"><span>报名列表</span></a>
        <i class="arrow"></i>
        <span>报名详情</span>
    </div>
    <div class="line10"></div>
    <!--/导航栏-->
    <form id="form1" runat="server">
        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">报名详情</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="tab-content">
            <dl>
                <dt>报名类型</dt>
                <dd>
                    <asp:DropDownList ID="ddlFType" runat="server" CssClass="select">
                        <asp:ListItem Text="请选择类型" Value="0"></asp:ListItem>
                        <asp:ListItem Text="装修报价" Value="1"></asp:ListItem>
                        <asp:ListItem Text="参观展厅" Value="2"></asp:ListItem>
                        <asp:ListItem Text="查看工地" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </dd>
            </dl>
            <dl>
                <dt>报价户型</dt>
                <dd>
                    <asp:DropDownList ID="ddlHXType" runat="server" CssClass="select">
                    </asp:DropDownList>
                </dd>
            </dl>
            <dl>
                <dt>报价风格</dt>
                <dd>
                    <asp:DropDownList ID="ddlFGType" runat="server" CssClass="select">
                    </asp:DropDownList>
                </dd>
            </dl>
            <dl>
                <dt>联系人</dt>
                <dd>
                    <asp:TextBox ID="txtName" runat="server" Text="" CssClass="input normal" datatype="*2-10" nullmsg="请输入联系人" errormsg="联系人长度在2-10个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>性别</dt>
                <dd>
                    <asp:RadioButtonList ID="rbtnSex" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="先生" Value="先生"></asp:ListItem>
                        <asp:ListItem Text="女士" Value="女士"></asp:ListItem>
                    </asp:RadioButtonList>
                </dd>
            </dl>
            <dl>
                <dt>联系电话</dt>
                <dd>
                    <asp:TextBox ID="txtPhone" runat="server" Text="" CssClass="input normal" datatype="*8-11" nullmsg="请输入联系电话" errormsg="联系电话长度在8-11个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>联系QQ</dt>
                <dd>
                    <asp:TextBox ID="txtQQ" runat="server" Text="" CssClass="input normal" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>房屋面积</dt>
                <dd>
                    <asp:TextBox ID="txtArea" runat="server" Text="" CssClass="input normal" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>客户楼盘</dt>
                <dd>
                    <asp:TextBox ID="txtHouse" runat="server" Text="" CssClass="input normal" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>客户需求</dt>
                <dd>
                    <asp:TextBox ID="txtXQ" runat="server" Text="" CssClass="input normal" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>报价时间</dt>
                <dd>
                    <asp:TextBox ID="txtAddTime" runat="server" Text="" CssClass="input normal"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>备注</dt>
                <dd>
                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Text="" CssClass="input normal" Style="width: 500px; height: 80px;"></asp:TextBox>
                </dd>
            </dl>
        </div>
        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn" OnClick="btnSave_Click" />
                <asp:Button ID="btnretset" runat="server" Text="返回" CssClass="btn yellow" OnClick="btnretset_Click" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
