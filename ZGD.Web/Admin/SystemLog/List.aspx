<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="ZGD.Web.Admin.SystemLog.List" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统日志管理</title>
    <link rel="stylesheet" type="text/css" href="../Images/style.css" />
    <script type="text/javascript" src="../../Js/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="../../Js/jquery.pagination.js"></script>
    <script type="text/javascript" src="../Js/function.js"></script>
    <script type="text/javascript">
        //隔行变色
        $(function () {
            $(".msgtable tr:nth-child(odd)").addClass("tr_bg");
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
<body style="padding: 10px;">
    <form id="form1" runat="server">
        <div class="navigation"><b>您当前的位置：首页 &gt; 系统管理 &gt; 系统日志管理</b></div>
        <div class="spClear"></div>
        <asp:Repeater ID="rptList" runat="server">
            <HeaderTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
                    <tr>
                        <th style="width: 7%;">选择</th>
                        <th width="6%">编号</th>
                        <th align="left">操作日志</th>
                        <th width="13%">操作用户</th>
                        <th width="13%">IP地址</th>
                        <th align="left" width="16%">操作时间</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <asp:CheckBox ID="cb_id" CssClass="checkall" runat="server" /></td>
                    <td align="center">
                        <asp:Label ID="lb_id" runat="server" Text='<%#HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"Id")%>'></asp:Label></td>
                    <td><%#HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"Title")%></td>
                    <td align="center"><%#HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"UserName").ToString()%></td>
                    <td align="center"><%#HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"IPAddress")%></td>
                    <td><%#string.Format("{0:g}",HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"AddTime"))%></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <div class="spClear"></div>
        <div style="line-height: 30px; height: 30px;">
            <div class="right">
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="formfield"
                    CustomInfoClass="formbutton"
                    CustomInfoHTML="第&lt;font color='red'&gt;&lt;b&gt;%CurrentPageIndex%&lt;/b&gt;&lt;/font&gt;页 共%PageCount%&nbsp;页 %StartRecordIndex%-%EndRecordIndex%"
                    CustomInfoTextAlign="Center" FirstPageText="首页" horizontalalign="Center"
                    InputBoxStyle="width:19px" LastPageText="尾页" meta:resourceKey="AspNetPager1"
                    NextPageText="下一页" PageSize="9"
                    PrevPageText="前一页" showcustominfosection="Left" ShowInputBox="Always"
                    ShowNavigationToolTip="True" style="font-size: 12px"
                    SubmitButtonClass="formfield" SubmitButtonText="GO" width="506px"
                    onpagechanging="AspNetPager1_PageChanging" PageIndexBoxType="TextBox"
                    ShowPageIndexBox="Never" AlwaysShow="True">
                </webdiyer:AspNetPager>
            </div>
            <div class="left">
                <span class="btn_all" onclick="checkAll(this);">全选</span>
                &nbsp;&nbsp;&nbsp;
				<span class="btn_bg">
                    <asp:LinkButton ID="LinkButton1" runat="server"
                        OnClientClick="return confirm( '确定要删除这些记录吗？ ');" OnClick="lbtnDel_Click">批 量 删 除</asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:LinkButton ID="lbtnDel" runat="server"
                     OnClientClick="return confirm( '此操作将会清空7天前的日志，确定要这样做吗？ ');" OnClick="lbtnDel_Click">清空日志</asp:LinkButton>
                </span>
            </div>
        </div>
    </form>
</body>
</html>
