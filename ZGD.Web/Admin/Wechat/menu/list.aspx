<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="ZGD.Web.Admin.wechat.menu.list" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>微信导航菜单</title>
    <script type="text/javascript" src="../../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../../js/layout.js"></script>
    <link href="../../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../skin/pagination/pagination.css" rel="stylesheet" type="text/css" />
</head>
<body class="mainbody">

    <form id="form1" runat="server">
    <!--导航栏-->
    <div class="location">
        <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
        <a href="/admin/center" class="home"><i></i><span>首页</span></a>
        <i class="arrow"></i><span>微信导航菜单</span>
    </div>
    <!--/导航栏-->
    <!--工具栏-->
    <div class="toolbar-wrap">
        <div id="floatHead" class="toolbar">
            <div class="l-list">
                <ul class="icon-list">
                    <li><a class="add" href="edit.aspx?action=add"><i></i><span>新增</span></a> </li>
                    <li>
                        <asp:LinkButton ID="btnSave" runat="server" CssClass="save" OnClick="btnSave_Click"><i></i><span>保存</span></asp:LinkButton></li>
                    <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                    <li>
                        <asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete','删除该分类将删除对应的目录及文件；<br />如果该分类下还存在频道则无法删除，是否继续？');"
                            OnClick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
                            <li>&nbsp;<asp:Button ID="btn_create_menu" runat="server" class="btn" Text="生成菜单" 
                                    onclick="btn_create_menu_Click"/></li>
                </ul>
            </div>
        </div>
    </div>
    <!--/工具栏-->
    <!--列表-->
    <asp:Repeater ID="rptList" runat="server"  onitemdatabound="rptList_ItemDataBound">
        <HeaderTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                <tr>
                    <th width="8%">
                        选择
                    </th>
                    <th align="left">
                        菜单名称
                    </th>
                    <th width="10%">
                        类型
                    </th>
                    <th width="10%">
                        排序
                    </th>
                    <th width="10%">
                        是否显示
                    </th>
                     <th width="10%">
                        添加子级
                    </th>
                    <th width="10%">
                        操作
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td align="center">
                    <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle;" /><asp:HiddenField
                        ID="hidId" Value='<%#Eval("id")%>' runat="server" />
                        <asp:HiddenField ID="hidLayer" Value='<%#Eval("menu_parent_id") %>' runat="server" />
                </td>
                <td>
                    <asp:Literal ID="LitFirst" runat="server"></asp:Literal>
      <a href="edit.aspx?action=edit&id=<%#Eval("id")%>"><%#Eval("menu_name")%></a>
                </td>
                <td align="center">
                    <%#Eval("menu_type")%>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtSortId" runat="server" Text='<%#Eval("menu_sort")%>' CssClass="sort"
                        onkeydown="return checkNumber(event);" />
                </td>
                <td align="center">
                    <%#Convert.ToInt32(Eval("menu_is_show")) == 1 ? "是" : "否"%>
                </td>
                <td align="center">
                <script>
                var pid=<%#Eval("menu_parent_id") %>;
                if(pid==0)
                window.document.write("<a href=\"edit.aspx?action=add&id=<%#Eval("id")%>\">添加子级</a>");
                </script>
                </td>
                <td align="center">
                    <a href="edit.aspx?action=edit&id=<%#Eval("id")%>">
                        修改</a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"7\">暂无记录</td></tr>" : ""%>
            </table>
        </FooterTemplate>
    </asp:Repeater>

    
    </form>
</body>
</html>
