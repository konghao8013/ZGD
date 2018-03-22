<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bind.aspx.cs" Inherits="ZGD.Web.Admin.wechat.bind" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>系统参数设置</title>
<script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../../scripts/swfupload/swfupload.js"></script>
<script type="text/javascript" src="../../scripts/swfupload/swfupload.handlers.js"></script>
<script type="text/javascript" src="../js/layout.js"></script>
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
        //初始化上传控件
        $(".upload-img").each(function () {
            $(this).InitSWFUpload({ sendurl: "../../tools/upload_ajax.ashx", flashurl: "../../scripts/swfupload/swfupload.swf" });
        });
    });
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <span>系统参数设置</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div class="content-tab-wrap">
  <div id="floatHead" class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a href="javascript:;" onclick="tabs(this);" class="selected">基本信息</a></li>
      </ul>
    </div>
  </div>
</div>

<!--网站基本信息-->
<div class="tab-content">
  <dl>
    <dt>微信名称</dt>
    <dd>
      <asp:TextBox ID="wechat_name" runat="server" CssClass="input normal" datatype="*1-50" sucmsg=" " />
      <span class="Validform_checktip">*任意字符</span>
    </dd>
  </dl>
  <dl>
    <dt>微信号</dt>
    <dd>
      <asp:TextBox ID="wechat_no" runat="server" CssClass="input normal" datatype="*1-50" sucmsg=" " />
      <span class="Validform_checktip">*任意字符</span>
    </dd>
  </dl>
  <dl>
    <dt>微信LOGO</dt>
    <dd>
      <asp:TextBox ID="wechat_logo" runat="server" datatype="*1-250" CssClass="input normal upload-path" />
      <div class="upload-box upload-img"></div>
    </dd>
  </dl>
  <dl>
    <dt>验证模式</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="wechat_check" runat="server" />
      </div>
      <span class="Validform_checktip">*是否开启验证模式</span>
    </dd>
  </dl>
  <dl>
    <dt>对接地址</dt>
    <dd>
      <asp:TextBox ID="wechat_url" runat="server" CssClass="input normal" datatype="url" sucmsg=" " />
      <span class="Validform_checktip">*以“http://”开头</span>
    </dd>
  </dl>
  <dl>
    <dt>微信登录账号</dt>
    <dd>
      <asp:TextBox ID="wechat_account" runat="server" CssClass="input normal" datatype="*1-50"/>
    </dd>
  </dl>
  <dl>
    <dt>微信登录密码</dt>
    <dd>
      <asp:TextBox ID="wechat_pwd" runat="server" CssClass="input normal" datatype="*1-50"/>
    </dd>
  </dl>
  <dl>
    <dt>appid</dt>
    <dd>
      <asp:TextBox ID="appid" runat="server" CssClass="input normal" datatype="*1-150"/>
      <span class="Validform_checktip"></span>
    </dd>
  </dl>
  <dl>
    <dt>appsecret</dt>
    <dd>
      <asp:TextBox ID="appsecret" runat="server" CssClass="input normal" datatype="*1-150"/>
      <span class="Validform_checktip"></span>
    </dd>
  </dl>
  <dl>
    <dt>token</dt>
    <dd>
      <asp:TextBox ID="token" runat="server" CssClass="input txt" datatype="*1-50" sucmsg=" " />
      <span class="Validform_checktip"></span>
    </dd>
  </dl>
</div>


<!--工具栏-->
<div class="page-footer">
  <div class="btn-list">
    <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" onclick="btnSubmit_Click" />
  </div>
  <div class="clear"></div>
</div>
<!--/工具栏-->
</form>
</body>
</html>