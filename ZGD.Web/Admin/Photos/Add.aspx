<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" ValidateRequest="false" Inherits="ZGD.Web.Admin.Photos.Add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>发布<%=title %></title>
    <script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../js/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="../js/swfupload/swfupload.handlers.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
            //初始化上传控件
            $(".upload-img").each(function () {
                $(this).InitSWFUpload({ sendurl: "../../tools/upload_ajax.ashx", flashurl: "../js/swfupload/swfupload.swf" });
            });
        });
    </script>
</head>
<body class="mainbody">
    <!--导航栏-->
    <div class="location">
        <a href="List.aspx?t=<%=pType %>" class="back"><i></i><span>返回列表</span></a>
        <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
        <i class="arrow"></i>
        <a href="List.aspx?t=<%=pType %>"><span><%=title %>列表</span></a>
        <i class="arrow"></i>
        <span>发布<%=title %></span>
    </div>
    <div class="line10"></div>
    <!--/导航栏-->

    <!--内容-->
    <div class="content-tab-wrap">
        <div id="floatHead" class="content-tab">
            <div class="content-tab-ul-wrap">
                <ul>
                    <li><a href="javascript:;" onclick="tabs(this);" class="selected">发布<%=title %></a></li>
                </ul>
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
        <div class="tab-content">
            <dl id="selType" runat="server" style="display:none;">
                <dt>工艺分类</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlType" runat="server" datatype="*" nullmsg="请选择工艺分类" sucmsg=" ">
                            <asp:ListItem Value="0">请选择工艺分类</asp:ListItem>
                            <asp:ListItem Value="1">文明施工</asp:ListItem>
                            <asp:ListItem Value="2">水电工程</asp:ListItem>
                            <asp:ListItem Value="3">漆工壁纸</asp:ListItem>
                            <asp:ListItem Value="4">家装阶段</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>标题</dt>
                <dd>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*1-100" nullmsg="请输入标题" errormsg="标题在1-100个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>上传图片</dt>
                <dd>
                    <asp:TextBox ID="txtImgUrl" runat="server" datatype="*0-250" CssClass="input normal upload-path" nullmsg="请上传相册封面" />
                    <div class="upload-box upload-img"></div>
                    <span class="Validform_checktip">尺寸800*600px</span>
                </dd>
            </dl>
            <dl>
                <dt>简述</dt>
                <dd>
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="input normal" datatype="*0-200" errormsg="简述在0-200个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
           <%-- <dl>
                <dt>图集</dt>
                <dd>
                    <a href="javascript:;" onclick="showUpImg(1);" style="font-weight: 600; color: Red; text-decoration: underline">点击上传图</a><span>（尺寸710*450px）</span><br />
                    <div id="divHImg"></div>
                    <input type="hidden" id="hidImg" name="hidImg" value="" runat="server" />
                    <input type="hidden" id="hidTitle" name="hidImg" value="" runat="server" />
                </dd>
            </dl>--%>
            <dl>
                <dt>置顶属性</dt>
                <dd>
                    <div class="rule-single-checkbox">
                        <asp:CheckBox ID="cblItem" runat="server" Text="置顶" />
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>序号</dt>
                <dd>
                    <asp:TextBox ID="txtSort" Text="0" runat="server" CssClass="input normal" datatype="/^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/" errormsg="请输入正确的序号" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
        </div>
        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSave" runat="server" Text="保存信息" CssClass="btn" OnClick="btnSave_Click" />
                <asp:Button ID="btnReset" runat="server" Text="清空信息" CssClass="btn yellow" OnClick="btnReset_Click" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
