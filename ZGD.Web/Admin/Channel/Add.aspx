<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="ZGD.Web.Admin.Channel.Add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>增加<%=typeName %></title>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #imgPanel img { width: 130px; margin-bottom: 5px; }
    </style>

    <script type="text/javascript" src="../js/jquery/jquery-3.7.1.min.js"></script><script type="text/javascript" src="../js/jquery/jquery-migrate-1.4.1.js"></script>
    <script type="text/javascript" src="../js/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../Js/webuploader-0.1.5/webuploader.css" rel="stylesheet" />
    <script type="text/javascript" src="../Js/webuploader-0.1.5/webuploader.min.js"></script>
    <script type="text/javascript" src="../Js/uploadimg.js"></script>

    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
            //初始化上传控件
            imgMethod.init('uploader-list', 'filePicker', 'txtImgUrl', 'event', 1, "back_", '', function (file, resp) {
                return $('<div class="file-box" id="file_back_f1"><div class="file">' +
                    '<div id="f1" class="file-item thumbnail">' +
                    '<img id="img_back_f1" src="' + resp.url + '" data-src="' + resp.url + '" class="img-responsive"><p class="progress img_up">上传排队中</p></div></div></div>');
            });
        });
        function backUrl() {
            window.location.href = "List.aspx?kindId=<%=kindId %>&pId=<%=pId %>";
        }
    </script>
</head>
<body class="mainbody">
    <!--导航栏-->
    <div class="location">
        <a href="List.aspx" class="back"><i></i><span>返回列表</span></a>
        <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
        <i class="arrow"></i>
        <a href="List.aspx?kindId=<%=kindId %>&classId=<%=pId %>"><span><%=typeName %>设置</span></a>
        <i class="arrow"></i>
        <span>参数详情</span>
    </div>
    <div class="line10"></div>
    <!--/导航栏-->

    <form id="form1" runat="server">
        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">增加<%=typeName %></a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="tab-content">
            <dl>
                <dt>所属父<%=typeName %></dt>
                <dd>
                    <asp:Label ID="lblPid" runat="server" Text="0" Visible="False"></asp:Label><asp:Label ID="lblPname" Font-Bold="true" Font-Size="Larger" runat="server"></asp:Label>
                </dd>
            </dl>
            <dl>
                <dt><%=typeName %>名称</dt>
                <dd>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*1-50" nullmsg="请输入<%=typeName %>名称" errormsg="<%=typeName %>名称长度在1-50个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>序号</dt>
                <dd>
                    <asp:TextBox ID="txtSortId" CssClass="input normal" Text="1" runat="server" datatype="*1-50" nullmsg="请输入序号" errormsg="序号长度在1-50" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl class="upordown" id="tr_img_panel" runat="server">
                <dt>缩略图上传</dt>
                <dd>
                    <div id="uploader-demo">
                        <div id="filePicker">选择图片</div>
                        <p class="uploader-line">注：尺寸400*160px</p>
                        <div id="uploader-list" class="uploader-list"></div>
                    </div>
                    <input id="txtImgUrl" type="hidden" name="txtImgUrl" runat="server" />
                </dd>
            </dl>
            <dl>
                <dt>是否禁用</dt>
                <dd>
                    <div class="rule-single-checkbox">
                        <asp:CheckBox ID="cbIsDelete" runat="server" />
                    </div>
                </dd>
            </dl>
        </div>
        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSave" runat="server" Text="确认保存" CssClass="btn" OnClick="btnSave_Click" />
                <input id="btnReset" type="button" value="返 回" class="btn yellow" onclick="backUrl();" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
