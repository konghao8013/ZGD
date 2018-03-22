<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="repairEdit.aspx.cs" Inherits="ZGD.Web.Admin.Feedback.repairEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>回复留言</title>
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
        <a href="repair.aspx" class="back"><i></i><span>返回列表</span></a>
        <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
        <i class="arrow"></i>
        <a href="repair.aspx"><span>保修列表</span></a>
        <i class="arrow"></i>
        <span>保修详情</span>
    </div>
    <div class="line10"></div>
    <!--/导航栏-->
    <form id="form1" runat="server">
        
        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">保修详情</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="tab-content">
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
                <dt>楼盘地址</dt>
                <dd>
                    <asp:TextBox ID="txtAddress" ReadOnly="true" runat="server" CssClass="input"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>原设计师</dt>
                <dd>
                    <asp:TextBox ID="txtManager" ReadOnly="true" runat="server" CssClass="input"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>楼盘地址</dt>
                <dd>
                    <asp:TextBox ID="txtDeser" ReadOnly="true" runat="server" CssClass="input"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>维修时间</dt>
                <dd>
                    <asp:TextBox ID="txtRapairTime" ReadOnly="true" runat="server" CssClass="input"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>维修说明</dt>
                <dd>
                    <asp:TextBox ID="txtCon" ReadOnly="true" TextMode="MultiLine" runat="server" CssClass="input" Style="width: 500px; height: 80px;"></asp:TextBox>
                </dd>
            </dl>
        </div>

        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <%--<asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn" OnClick="btnSave_Click" />--%>
                <asp:Button ID="btnretset" runat="server" Text="返回" CssClass="btn yellow" OnClick="btnretset_Click" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>

</body>
</html>
