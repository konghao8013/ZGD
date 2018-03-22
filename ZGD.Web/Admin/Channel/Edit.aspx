<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="ZGD.Web.Admin.Channel.Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>添加栏目</title>
    <script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
        });
        function backUrl() {
            window.location.href = "List.aspx?kindId=<%=kindId %>&pId=<%=pId %>";
        }
    </script>
</head>
<body class="mainbody">
    <!--导航栏-->
    <div class="location">
        <a href="List.aspx" class="back"><i></i><span>返回列表</span></a>
        <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
        <i class="arrow"></i>
        <a href="List.aspx?kindId=<%=kindId %>&classId=<%=classId %>"><span>类型管理</span></a>
        <i class="arrow"></i>
        <span>修改类型</span>
    </div>
    <div class="line10"></div>
    <!--/导航栏-->
    <form id="form1" runat="server">
        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">修改类型</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="tab-content">
            <dl>
                <dt>所属父类型</dt>
                <dd>
                    <asp:Label ID="lblPid" runat="server" Text="0" Visible="False"></asp:Label><asp:Label ID="lblPname" Font-Bold="true" Font-Size="Larger" runat="server"></asp:Label>
                </dd>
            </dl>
            <dl>
                <dt>类型名称</dt>
                <dd>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*1-50" nullmsg="请输入类型名称" errormsg="类型名称长度在1-50个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>优先级别</dt>
                <dd>
                    <asp:TextBox ID="txtSortId" CssClass="input normal" Text="1" runat="server" datatype="*1-50" nullmsg="请输入优先级别" errormsg="优先级别长度在1-50" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl id="cacl_panel" runat="server" style="display:none">
                <dt>现代系列</dt>
                <dd>
                    <asp:TextBox ID="txtCaclNum" CssClass="input normal" Text="0" runat="server" datatype="/^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/" nullmsg="请输入计算单价" errormsg="计算单价必须为数字" sucmsg=" "></asp:TextBox>
                    <span>计算单价，以每平方计算，单位“元”</span>
                </dd>
            </dl>
            <dl id="cacl_panel2" runat="server" style="display:none">
                <dt>世家系列</dt>
                <dd>
                    <asp:TextBox ID="txtCaclNum2" CssClass="input normal" Text="0" runat="server" datatype="/^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/" nullmsg="请输入计算单价" errormsg="计算单价必须为数字" sucmsg=" "></asp:TextBox>
                    <span>计算单价，以每平方计算，单位“元”</span>
                </dd>
            </dl>
            <dl>
                <dt>是否禁用</dt>
                <dd>
                    <div class="rule-single-checkbox">
                        <asp:CheckBox ID="cbIsDelete" runat="server" />
                    </div>
                </dd>
            </dl>
        </div>
        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSave" runat="server" Text="确认保存" CssClass="btn" OnClick="btnSave_Click" />
                <input id="btnReset" type="button" value="返 回" class="btn yellow" onclick="backUrl();" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
