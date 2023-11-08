<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="ZGD.Web.Admin.Feedback.List" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>装修报名</title>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/pagination/pagination.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $(".msgtable tr:nth-child(odd)").addClass("tr_bg"); //隔行变色
            $(".msgtable tr").hover(
               function () {
                   $(this).addClass("tr_hover_col");
               },
               function () {
                   $(this).removeClass("tr_hover_col");
               }
           );
        });
    </script>
</head>
<body class="mainbody">
    <!--导航栏-->
    <div class="location">
        <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
        <i class="arrow"></i>
        <a href="List.aspx"><span>报名列表</span></a>
    </div>
    <!--/导航栏-->
    <form id="form1" runat="server">
        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="l-list">
                    <ul class="icon-list">
                        <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                        <li>
                            <asp:LinkButton ID="lbtnDel" runat="server" OnClientClick="return confirm( '确定要删除这些记录吗？ ');" CssClass="del" OnClick="lbtnDel_Click"><i></i><span>删除</span></asp:LinkButton></li>
                    </ul>
                    <div class="menu-list">
                        <div class="rule-single-select">
                            <asp:DropDownList ID="ddlFType" runat="server" CssClass="select" AutoPostBack="True" OnSelectedIndexChanged="ddlFType_SelectedIndexChanged">
                                <asp:ListItem Text="请选择类型" Value="0"></asp:ListItem>
                                <asp:ListItem Text="装修报价" Value="1"></asp:ListItem>
                                <asp:ListItem Text="参观展厅" Value="2"></asp:ListItem>
                                <asp:ListItem Text="查看工地" Value="3"></asp:ListItem>
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
                        <th align="left">类型</th>
                        <th align="left">姓名</th>
                        <th align="left">户型</th>
                        <th align="left">风格</th>
                        <th align="left">小区</th>
                        <th width="10%">报价时间</th>
                        <th width="90">操作</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <asp:CheckBox ID="cb_id" CssClass="checkall" runat="server" /></td>
                    <td align="center">
                        <asp:Label ID="lb_id" runat="server" Text='<%#HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"Id")%>'></asp:Label></td>
                    <td align="left"><%# GetFType(Convert.ToInt32(HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"fType")))%></td>
                    <td align="left">姓名：<%#HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"UserName").ToString()%><br />电话：<%#HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"UserTel").ToString()%></td><%--（<%#HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"Sex").ToString()%>）--%>
                    <td align="left"><%# new ZGD.BLL.Channel().GetChannelTitle(Convert.ToInt32(HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"ClassId")))%></td>
                    <td align="left"><%# new ZGD.BLL.Channel().GetChannelTitle(Convert.ToInt32(HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"ClassId2")))%></td>
                    <td align="left"><%#HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"House").ToString()%></td>
                    <td align="center"><%#string.Format("{0:g}",HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"AddTime"))%></td>
                    <td align="center">
                        <a href="Edit.aspx?id=<%#HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"Id") %>">详情</a></td>
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
                    NextPageText="下一页" PageSize="10"
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
