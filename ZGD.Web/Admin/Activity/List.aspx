<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="ZGD.Web.Admin.Activity.List" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>活动列表</title>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../skin/pagination/pagination.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
</head>

<body class="mainbody">
    <!--导航栏-->
    <div class="location">
        <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
        <i class="arrow"></i>
        <a href="List.aspx"><span>活动列表</span></a>
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
                        <li>
                            <asp:LinkButton ID="lbtnDel" runat="server" OnClientClick="return confirm( '确定要删除这些记录吗？ ');" CssClass="del" OnClick="lbtnDel_Click"><i></i><span>删除</span></asp:LinkButton></li>
                    </ul>
                    <div class="menu-list">
                        <div class="rule-single-select">
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="select"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                <asp:ListItem Value="-2">所有活动</asp:ListItem>
                                <asp:ListItem Value="0">未开始</asp:ListItem>
                                <asp:ListItem Value="1">进行中</asp:ListItem>
                                <asp:ListItem Value="-1">已结束</asp:ListItem>
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
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="ltable">
                    <tr>
                        <th width="6%">选择</th>
                        <th width="6%">编号</th>
                        <th align="left">活动链接</th>
                        <th align="left">活动图片</th>
                        <th align="left">活动名称</th>
                        <th width="10%">创建时间</th>
                        <th>状态</th>
                        <th width="90">操作</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <asp:CheckBox ID="cb_id" CssClass="checkall" runat="server" /></td>
                    <td align="center">
                        <asp:Label ID="lb_id" runat="server" Text='<%#Eval("ID")%>'></asp:Label></td>
                    <td align="left"><a target="_blank" href="<%#string.IsNullOrWhiteSpace(Eval("aUrl").ToString())?"/Activity/"+Eval("ID").ToString():Eval("aUrl").ToString()%>"><%#string.IsNullOrWhiteSpace(Eval("aUrl").ToString())?"/Activity/"+Eval("ID").ToString():Eval("aUrl").ToString()%></a></td>
                    <td align="left">
                        <img src="<%#Eval("ImgUrl")%>" width="71" height="56" /></td>
                    <td align="left"><%#Eval("Title").ToString()%></td>
                    <td align="center"><%#string.Format("{0:g}",Eval("PubTime"))%></td>
                    <td align="center"><%# ZGD.Common.SystemHandler.GetActivityStatus(Convert.ToInt32(Eval("Status")))%></td>
                    <td align="center"><a href="Edit.aspx?id=<%#Eval("Id") %>">详情</a></td>
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
                    NextPageText="下一页" PageSize="9"
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
