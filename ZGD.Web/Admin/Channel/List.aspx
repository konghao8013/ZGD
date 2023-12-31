﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="ZGD.Web.Admin.Channel.List" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>版块管理</title>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .del{color:red !important;}
    </style>
    <script type="text/javascript" src="/content/scripts/jquery-3.7.1.min.js"></script><script type="text/javascript" src="/content/scripts/jquery-migrate-1.4.1.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
</head>

<body class="mainbody">
    <!--导航栏-->
    <div class="location">
        <a href="List.aspx" class="back"><i></i><span>返回列表</span></a>
        <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
        <i class="arrow"></i>
        <a href="List.aspx"><span><%=typeName %>列表</span></a>
    </div>
    <!--/导航栏-->
    <form id="form1" runat="server">
        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="l-list">
                    <ul class="icon-list">
                        <li><a class="add" href="Add.aspx?kindId=<%=kindId %>&pId=<%=pId %>"><i></i><span>新增<%=typeName %></span></a></li>
                    </ul>
                </div>
            </div>
        </div>
        <!--/工具栏-->

        <asp:Repeater ID="rptClassList" runat="server" OnItemCommand="rptClassList_ItemCommand" OnItemDataBound="rptClassList_ItemDataBound">
            <HeaderTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                    <tr>
                        <th width="7%">编号</th>
                        <th align="left"><%=typeName %>名称</th>
                        <th width="90">序号</th>
                        <th width="90">状态</th>
                        <th width="150">操作</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <asp:HiddenField ID="txtClassId" runat="server" Value='<%#HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"Id") %>' />
                        <asp:HiddenField ID="txtClassLayer" runat="server" Value='<%#HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"ClassLayer") %>' />
                    </td>
                    <td>
                        <asp:Literal ID="LitFirst" runat="server"></asp:Literal>
                        <%# HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"Title") %>
                    </td>
                    <td align="center"><%# HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"SortId") %></td>
                    <td align="center">
                        <asp:ImageButton ID="ibtnLock" CommandName="ibtnDelete" runat="server" ImageUrl='<%# Convert.ToInt32(HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"IsDelete")) == 0 ? "../Images/correct.gif" : "../Images/disable.gif"%>' ToolTip='<%# Convert.ToInt32(HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"IsDelete")) == 1 ? "取消禁用" : "设置禁用"%>' style='<%#HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"Id").ToString() != "38" ? "" : "display:none;" %>' />
                        <asp:ImageButton ID="ibtnTop" CommandName="ibtnTop" runat="server" ImageUrl='<%# Convert.ToInt32(HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"IsTop")) == 1 ? "../Images/ico-1.png" : "../Images/ico-1_.png"%>' ToolTip='<%# Convert.ToInt32(HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"IsTop")) == 1 ? "首页不显示" : "首页显示"%>' />
                    </td>
                    <td align="center">
                        <%--<span><a href="Add.aspx?kindId=<%=kindId %>&classId=<%# HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"Id") %>">添加子类</a></span>--%>
                        <span><a href="Edit.aspx?kindId=<%=kindId %>&pId=<%# HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"ParentId") %>&classId=<%# HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"Id") %>" style='<%#HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"Id").ToString() != "38" || HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"ParentId").ToString() != "0" ? "" : "display:none;" %>'>修改</a></span>
                        <span><asp:LinkButton ID="lbDel" CssClass="del" CommandName="btndel" runat="server" OnClientClick="return confirm( '该操作会删除该专题，确定要删除吗？ ');">删除</asp:LinkButton></span>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </form>
</body>
</html>
