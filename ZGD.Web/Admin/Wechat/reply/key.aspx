<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="key.aspx.cs" Inherits="ZGD.Web.Admin.wechat.reply.key" %>

<%@ Import namespace="ZGD.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>微信回复列表</title>
<script type="text/javascript" src="../../../scripts/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" src="../../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../../js/layout.js"></script>
<link href="../../skin/default/style.css" rel="stylesheet" type="text/css" />
<link  href="../../skin/pagination/pagination.css" rel="stylesheet" type="text/css" />
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
  <a href="../../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <span>微信回复列表</span>
</div>
<!--/导航栏-->

<!--工具栏-->
<div class="toolbar-wrap">
  <div id="floatHead" class="toolbar">
    <div class="l-list">
      <ul class="icon-list">
        <li><a class="add" href="javascript:;" onclick="editKey()"><i></i><span>新增</span></a></li>
        
        <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
        <li><asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete','删除该分类将删除对应的目录及文件；<br />如果该分类下还存在频道则无法删除，是否继续？');" onclick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
      </ul>
    </div>
    <div class="r-list">
      <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" />
      <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" onclick="btnSearch_Click">查询</asp:LinkButton>
    </div>
  </div>
</div>
<!--/工具栏-->

<!--列表-->
<asp:Repeater ID="rptList" runat="server">
<HeaderTemplate>
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
  <tr>
    <th width="6%">选择</th>
    <th align="left">关键字</th>
    <th align="left" width="12%">添加时间</th>
    <th width="10%">操作</th>
  </tr>
</HeaderTemplate>
<ItemTemplate>
  <tr>
    <td align="center"><asp:CheckBox ID="chkId" CssClass="checkall" runat="server" style="vertical-align:middle;" /><asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" /></td>
    <td><%#Eval("reply_key")%></td>
    <td><%#Eval("addtime")%></td>
    <td align="center"><a href="javascript:void(0);" onclick="editKey(this,<%#Eval("id")%>)">修改</a></td>
  </tr>
</ItemTemplate>
<FooterTemplate>
  <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"7\">暂无记录</td></tr>" : ""%>
</table>
</FooterTemplate>
</asp:Repeater>
<!--/列表-->

<!--内容底部-->
<div class="line20"></div>
<div class="pagelist">
  <div class="l-btns">
    <span>显示</span><asp:TextBox ID="txtPageNum" runat="server" CssClass="pagenum" onkeydown="return checkNumber(event);" ontextchanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox><span>条/页</span>
  </div>
  
  <div id="PageContent" runat="server" class="default"></div>
</div>
<!--/内容底部-->
</form>
</body>
</html>
<script>
    function editKey(obj, id) {
        var objNum = arguments.length;

        //如果是修改状态，将对象传进去
        if (objNum > 1) {
            var m = $.dialog({
                fixed: true,
                lock: true,
                max: false,
                min: false,
                title: "修改关键字回复",
                content: 'url:wechat/reply/dialog_key.aspx?id='+id,
                width: 390,height:560
            });
            m.data = { obj: obj, id: id };
            m.fun = reload;
        } else {
            var m = $.dialog({
                fixed: true,
                lock: true,
                max: false,
                min: false,
                title: "添加关键字回复",
                content: 'url:wechat/reply/dialog_key.aspx',
                width: 390, height: 560
            });
            m.fun = reload;
        }
    }
    function reload() {
        location.reload();
    }
</script>