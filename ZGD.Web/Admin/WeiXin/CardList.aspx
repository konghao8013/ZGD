<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardList.aspx.cs" Inherits="ZGD.Web.Admin.WeiXin.CardList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>微名片列表</title>
    <link rel="stylesheet" type="text/css" href="../Images/style.css" />
    <script type="text/javascript" src="../Js/function.js"></script>
    <script type="text/javascript" src="../../Js/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="../../Js/jquery.pagination.js"></script>
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
<body style="padding: 10px;">
    <form id="form1" runat="server">
        <div class="navigation"><span class="add"><a href="CardAdd.aspx">新增微名片</a></span><b>您当前的位置：微信管理 &gt; 微名片列表</b></div>
        <div class="spClear"></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="50" align="center">请选择：</td>
                <td>
                    <asp:DropDownList ID="ddlProperty" runat="server" CssClass="select"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlProperty_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td width="160" align="right">关健字(按名称、电话查询)：</td>
                <td width="150">
                    <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword"></asp:TextBox></td>
                <td width="60" align="center">
                    <asp:Button ID="btnSelect" runat="server" Text="查询" CssClass="submit" OnClick="btnSelect_Click" /></td>
            </tr>
        </table>
        <div class="spClear"></div>
        <asp:Repeater ID="rptList" runat="server">
            <HeaderTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
                    <tr>
                        <th width="6%">选择</th>
                        <th width="6%">编号</th>
                        <th>头像</th>
                        <th width="10%">名称</th>
                        <th width="10%">部门</th>
                        <th width="10%">职位</th>
                        <th width="12%">新增时间</th>
                        <th width="8%">操作</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="left" id="td_<%#Eval("Id") %>">
                        <asp:CheckBox ID="cb_id" CssClass="checkall" runat="server" /></td>
                    <td align="left">
                        <asp:Label ID="lb_id" runat="server" Text='<%#Eval("Id")%>'></asp:Label></td>
                    <td align="left">
                        <img src="<%#Eval("ImgUrl")%>" width="100" height="100" /></td>
                    <td align="left"><%#Eval("uName")%></td>
                    <td align="left"><%#Eval("Dept")%></td>
                    <td align="left"><%#Eval("Role")%></td>
                    <td align="left"><%#string.Format("{0:g}", Eval("PubTime"))%></td>
                    <td align="left"><span><a href="CardEdit.aspx?id=<%#Eval("Id") %>">编辑</a></span></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <div class="spClear"></div>
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
                    SubmitButtonClass="formfield" SubmitButtonText="GO" Width="650px"
                    OnPageChanging="AspNetPager1_PageChanging" PageIndexBoxType="TextBox"
                    ShowPageIndexBox="Never" AlwaysShow="True">
                </webdiyer:AspNetPager>
            </div>
            <div class="left">
                <span class="btn_all" onclick="checkAll(this);">全选</span>
                <span class="btn_bg">
                    <asp:LinkButton ID="lbtnDel" runat="server"
                        OnClientClick="return confirm( '确定要删除这些记录吗？ ');" OnClick="lbtnDel_Click">删 除</asp:LinkButton>
                </span>
            </div>
        </div>
    </form>
</body>
</html>
