<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QaList.aspx.cs" Inherits="ZGD.Web.Admin.User.QaList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>文章列表</title>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../skin/pagination/pagination.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
</head>
<body class="mainbody">
    <!--导航栏-->
    <div class="location">
        <a href="List.aspx" class="back"><i></i><span>返回列表</span></a>
        <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
        <i class="arrow"></i>
        <a href="List.aspx"><span>文章列表</span></a>
    </div>
    <!--/导航栏-->

    <form id="form1" runat="server">
        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="l-list">
                    <div class="menu-list">
                        <div class="rule-single-select">
                            <asp:DropDownList ID="ddlClassId" runat="server" CssClass="select"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlClassId_SelectedIndexChanged">
                                <asp:ListItem Value="">所有状态</asp:ListItem>
                                <asp:ListItem Value="1">未解决</asp:ListItem>
                                <asp:ListItem Value="2">已解决</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="rule-single-select">
                            <asp:DropDownList ID="ddlProperty" runat="server" CssClass="select"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlProperty_SelectedIndexChanged">
                                <asp:ListItem Value="">所有属性</asp:ListItem>
                                <asp:ListItem Value="IsLock">禁用</asp:ListItem>
                                <asp:ListItem Value="IsTop">置顶</asp:ListItem>
                                <asp:ListItem Value="IsGood">精华</asp:ListItem>
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

        <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand">
            <HeaderTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                    <tr>
                        <th width="5%">编号</th>
                        <th width="25%">问题标题</th>
                        <th width="30%">问题描述</th>
                        <th>提问时间</th>
                        <th width="50px">屏蔽</th>
                        <th width="50px">置顶</th>
                        <th width="50px">精华</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <asp:Label ID="lb_id" runat="server" Text='<%#Eval("Id")%>'></asp:Label></td>
                    <td align="left"><a href="/wd/<%#Eval("Id")%>" target="_blank"><%#Eval("Title")%></a></td>
                    <td align="left"><%#Eval("Contents")%></td>
                    <td align="center"><%#string.Format("{0:g}", Eval("PubTime"))%></td>
                    <td align="center">
                        <asp:ImageButton ID="ibtnLock" CommandName="ibtnLock" runat="server" ImageUrl='<%# Convert.ToInt32(Eval("IsLock")) == 1 ? "../Images/correct.gif" : "../Images/disable.gif"%>' ToolTip='<%# Convert.ToInt32(Eval("IsLock")) == 1 ? "取消锁定" : "设置锁定"%>' />
                    </td>
                    <td align="center">
                        <asp:ImageButton ID="ibtnTop" CommandName="ibtnTop" runat="server" ImageUrl='<%# Convert.ToInt32(Eval("IsTop")) == 1 ? "../Images/correct.gif" : "../Images/disable.gif"%>' ToolTip='<%# Convert.ToInt32(Eval("IsTop")) == 1 ? "取消置顶" : "设置置顶"%>' />
                    </td>
                    <td align="center">
                        <asp:ImageButton ID="ibtnGood" CommandName="ibtnGood" runat="server" ImageUrl='<%# Convert.ToInt32(Eval("IsGood")) == 1 ? "../Images/correct.gif" : "../Images/disable.gif"%>' ToolTip='<%# Convert.ToInt32(Eval("IsGood")) == 1 ? "取消精华" : "设置精华"%>' />
                    </td>
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
