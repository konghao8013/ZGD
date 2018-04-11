<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="ZGD.Web.Admin.Links.List" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>友情链接管理</title>
    <script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../skin/pagination/pagination.css" rel="stylesheet" type="text/css" />
</head>
<body class="mainbody">
    <!--导航栏-->
    <div class="location">
        <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
        <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
        <i class="arrow"></i>
        <a href="List.aspx"><span>链接列表</span></a>
    </div>
    <!--/导航栏-->
    <form id="form1" runat="server">
        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="l-list">
                    <ul class="icon-list">
                        <li><a class="add" href="Add.aspx"><i></i><span>新增</span></a></li>
                        <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                        <li style="display:none">
                            <asp:LinkButton ID="lbtnAudit" runat="server" OnClientClick="return confirm( '确定要审核这些记录吗？ ');" CssClass="list" OnClick="lbtnAudit_Click"><i></i><span>审核</span></asp:LinkButton></li>
                        <li>
                            <asp:LinkButton ID="lbtnDel" runat="server" OnClientClick="return confirm( '确定要删除这些记录吗？ ');" CssClass="del" OnClick="lbtnDel_Click"><i></i><span>删除</span></asp:LinkButton></li>
                    </ul>
                    <div class="menu-list">
                        <div class="rule-single-select">
                            <asp:DropDownList ID="ddlClassId" runat="server" CssClass="select"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlClassId_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="rule-single-select">
                            <asp:DropDownList ID="ddlProperty" runat="server" CssClass="select"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlProperty_SelectedIndexChanged">
                                <asp:ListItem Value="">所有属性</asp:ListItem>
                                <asp:ListItem Value="IsLock">屏蔽</asp:ListItem>
                                <asp:ListItem Value="IsRed">推荐</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="r-list">
                    <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword"></asp:TextBox>
                    <asp:LinkButton ID="btnSelect" runat="server" CssClass="btn-search" OnClick="btnSelect_Click">查询</asp:LinkButton>
                </div>
            </div>
        </div>
        <!--/工具栏-->
        <asp:Repeater ID="rptList" runat="server">
            <HeaderTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                    <tr>
                        <th width="6%">选择</th>
                        <th width="6%">编号</th>
                        <th align="left">链接名称</th>
                        <th align="left">类型</th>
                        <%--<th width="16%">是否图片</th>--%>
                        <th width="90">排序号</th>
                        <th width="16%">添加时间</th>
                        <th width="90">属性</th>
                        <th width="8%">操作</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <asp:CheckBox ID="cb_id" CssClass="checkall" runat="server" /></td>
                    <td align="center">
                        <asp:Label ID="lb_id" runat="server" Text='<%#Eval("Id")%>'></asp:Label></td>
                    <td><%# Convert.ToInt32(Eval("IsLock")) == 1 ? "<img src=\"../Images/wsh01.gif\" title=\"未审核\" />" : ""%> <a href="<%#Eval("WebUrl")%>" target="_blank"><%#Eval("Title")%></a></td>
                    <td><%#Eval("fl")%></td>
                    <%--<td align="center"><%# Convert.ToInt32(Eval("IsImage")) == 0 ? "文字链接" : "<img src=\"" + Eval("ImgUrl") + "\" width=\"50\" height=\"20\" />"%></td>--%>
                    <td align="center"><%#Eval("SortId") %></td>
                    <td align="center"><%#string.Format("{0:g}",Eval("AddTime"))%></td>
                    <td align="center">
                        <%# Convert.ToInt32(Eval("IsRed")) == 1 ? "<img src=\"../Images/ico-2.png\" title=\"推荐\" />" : ""%> 
                    </td>
                    <td align="center"><span><a href="Edit.aspx?id=<%#Eval("Id") %>">修改</a></span></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <div class="line10"></div>
        <div style="line-height: 30px; height: 30px;">
            <div class="right flickr">
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="formfield"
                    CustomInfoClass="formbutton"
                    CustomInfoHTML="第&lt;font color='red'&gt;&lt;b&gt;%CurrentPageIndex%&lt;/b&gt;&lt;/font&gt;页 共%PageCount%&nbsp;页 %StartRecordIndex%-%EndRecordIndex%"
                    CustomInfoTextAlign="Center" FirstPageText="首页" HorizontalAlign="Center"
                    InputBoxStyle="width:19px" LastPageText="尾页" meta:resourceKey="AspNetPager1"
                    NextPageText="下一页" PageSize="20"
                    PrevPageText="前一页" ShowCustomInfoSection="Left" ShowInputBox="Always"
                    ShowNavigationToolTip="True" Style="font-size: 12px"
                    SubmitButtonClass="formfield" SubmitButtonText="GO" Width="700px"
                    OnPageChanging="AspNetPager1_PageChanging" PageIndexBoxType="TextBox"
                    ShowPageIndexBox="Never" AlwaysShow="True">
                </webdiyer:AspNetPager>
            </div>
        </div>
    </form>
</body>
</html>
