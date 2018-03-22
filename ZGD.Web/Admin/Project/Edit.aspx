<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="ZGD.Web.Admin.Project.Edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑案例</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.handlers.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <script type="text/javascript" src="/scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #divHImg { display: inline-block; height: auto; }
        .uploadImg { width: 135px; height: 130px; float: left; margin: 10px 10px 0 0; position:relative; }
        .uploadImg a{width: 15px; height: 15px;line-height: 15px;position:absolute;text-align:center;display:inline-block;top:4px;right:4px;background-color:#fff;color:#ff0000;font-weight:bold;font-size:14px;}
        .uploadImg div { width: 120px; text-align: center; border: solid 1px #ccc; padding: 5px; margin-bottom: 3px; }
        .uploadImg img { width: 120px; height: 90px; }
        .uploadImg .input { width: 125px; height:16px;min-height:16px;line-height:16px; padding-left:1px; }
        #imgPanel{margin-bottom:5px;}
        #imgPanel img { width: 120px; }
    </style>
    <script type="text/javascript">
        var upimg;
        var _img_type = 1, _hidImg = "hidImg", _hidTitle = "hidTitle", _divHImg = "divHImg";
        function showUpImg(type) {
            var title_name = type == 1 ? "上传案例图" : "上传案例图";
            upimg = $.dialog({
                fixed: true,
                lock: true,
                max: false,
                min: false,
                title: title_name,
                content: 'url:/Admin/Js/mUpload/upload.htm?id=' + type,
                width: 455, height: 380
            });
        }
        function setImg(obj, titleList, type) {
            _img_type = type;
            _hidImg = type == 1 ? "hidImg" : "hidImg_pro";
            _hidTitle = type == 1 ? "hidTitle" : "hidTitle_pro";
            _divHImg = type == 1 ? "divHImg" : "divHImg_pro";
            $("#" + _hidImg).val($("#" + _hidImg).val() + obj);
            $("#" + _hidTitle).val($("#" + _hidTitle).val() + titleList);
            bindImg();
        }
        function deleteImg(index, type) {
            _img_type = type;
            _hidImg = type == 1 ? "hidImg" : "hidImg_pro";
            _hidTitle = type == 1 ? "hidTitle" : "hidTitle_pro";
            _divHImg = type == 1 ? "divHImg" : "divHImg_pro";
            var hidimg = $("#" + _hidImg).val().split(",");
            var hidtitle = $("#" + _hidTitle).val().split(",");
            var hidtemp1 = "";
            var hidtemp2 = "";
            $.each($("#" + _divHImg + " input"), function (i, n) {
                if (i != index) {
                    hidtemp1 += hidimg[i] + ',';
                    hidtemp2 += $(n).val() + ',';
                }
            });
            $("#" + _hidImg).val(hidtemp1);
            $("#" + _hidTitle).val(hidtemp2);
            bindImg();
        }
        function bindData() {
            var hidimg = "", hidtitle = "";
            var hidimg_pro = "", hidtitle_pro = "";
            $.each($("#divHImg img"), function (i, n) {
                hidimg += $(n).attr("src") + ',';
            });
            $.each($("#divHImg input"), function (i, n) {
                hidtitle += $(n).val() + ',';
            });
            $.each($("#divHImg_pro img"), function (i, n) {
                hidimg_pro += $(n).attr("src") + ',';
            });
            $.each($("#divHImg_pro input"), function (i, n) {
                hidtitle_pro += $(n).val() + ',';
            });
            $("#hidImg").val(hidimg);
            $("#hidTitle").val(hidtitle);
            $("#hidImg_pro").val(hidimg_pro);
            $("#hidTitle_pro").val(hidtitle_pro);
        }
        function bindImg() {
            var hidimg = $("#" + _hidImg).val().split(",");
            var hidtitle = $("#" + _hidTitle).val().split(",");
            var hidtemp1 = "";
            var hidtemp2 = "";
            var temphtml = "";
            for (var i = 0; i < hidimg.length - 1; i++) {
                temphtml += '<div class="uploadImg"><div><img src="' + hidimg[i] + '" /></div><input type="text" class="input" value="' + hidtitle[i] + '" /><a href="javascript:void(0)" onclick="deleteImg(' + i + ',' + _img_type + ')">X</a></div>';
                hidtemp1 += hidimg[i] + ",";
                hidtemp2 += hidtitle[i] + ",";
            }
            $("#" + _divHImg).html(temphtml);
            $("#" + _hidImg).val(hidtemp1);
            $("#" + _hidTitle).val(hidtemp2);
        }

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
    <!--导航栏-->
    <div class="location">
        <a href="List.aspx" class="back"><i></i><span>返回列表</span></a>
        <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
        <i class="arrow"></i>
        <a href="List.aspx"><span>案例列表</span></a>
        <i class="arrow"></i>
        <span>编辑案例</span>
    </div>
    <div class="line10"></div>
    <!--/导航栏-->

    <!--内容-->
    <div class="content-tab-wrap">
        <div id="floatHead" class="content-tab">
            <div class="content-tab-ul-wrap">
                <ul>
                    <li><a href="javascript:;" onclick="tabs(this);" class="selected">发布案例</a></li>
                </ul>
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
        <div class="tab-content">
            <dl>
                <dt>案例标题</dt>
                <dd>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*1-100" nullmsg="请输入案例标题" errormsg="案例标题在1-100个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>关键词</dt>
                <dd>
                    <asp:TextBox ID="txtKeyword" runat="server" CssClass="input normal" datatype="*0-50" nullmsg="请输入关键词" errormsg="关键词在0-50个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>案例简述</dt>
                <dd>
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="input normal" datatype="*0-200" errormsg="案例简述在0-200个字符间" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>所属小区</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlHouse" runat="server"></asp:DropDownList>
                    </div>
                    <span>可以不选择</span>
                </dd>
            </dl>
            <dl>
                <dt>设计师</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlDes" datatype="*" nullmsg="请选择设计师" sucmsg=" " runat="server"></asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>案例风格</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlFG" datatype="*" nullmsg="请选择案例风格" sucmsg=" " runat="server"></asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>案例户型</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlHX" datatype="*" nullmsg="请选择案例户型" sucmsg=" " runat="server"></asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>案例功能间</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlGN" datatype="*" nullmsg="请选择功能间" sucmsg=" " runat="server"></asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>面积范围</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlMJ" sucmsg=" " runat="server"></asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>具体面积</dt>
                <dd>
                    <asp:TextBox ID="txtArea" runat="server" CssClass="input normal" datatype="/^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/" nullmsg="请输入具体面积" errormsg="请输入正确的具体面积" sucmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">* 单位：平</span>
                </dd>
            </dl>
            <dl>
                <dt>价格范围</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlJG" datatype="*" nullmsg="请选择房屋价格范围" sucmsg=" " runat="server"></asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>具体价格</dt>
                <dd>
                    <asp:TextBox ID="txtPrice" runat="server" CssClass="input normal" datatype="/^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/" nullmsg="请输入具体价格" errormsg="请输入正确的具体价格" sucmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">* 单位：万</span>
                </dd>
            </dl>
            <dl>
                <dt>案例LOGO</dt>
                <dd>
                    <p id="imgPanel" runat="server"></p>
                    <asp:TextBox ID="txtImgUrl" runat="server" datatype="*1-250" CssClass="input normal upload-path" />
                    <div class="upload-box upload-img"></div>
                    <span class="Validform_checktip">尺寸300*192px</span>
                </dd>
            </dl>
            <dl>
                <dt>案例图集</dt>
                <dd>
                    <a href="javascript:;" onclick="showUpImg(1);" style="font-weight: 600; color: Red; text-decoration: underline">点击上传户型图</a><span>（尺寸850*543px）</span><br />
                    <div id="divHImg"></div>
                    <input type="hidden" id="hidImg" name="hidImg" value="" runat="server" />
                    <input type="hidden" id="hidTitle" name="hidImg" value="" runat="server" />
                </dd>
            </dl>
            <dl>
                <dt>案例备注</dt>
                <dd>
                    <textarea id="txtRemark" runat="server" class="input normal"></textarea>
                </dd>
            </dl>
            <dl>
                <dt>案例属性</dt>
                <dd>
                    <asp:CheckBoxList ID="cblItem" runat="server"
                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="1">置顶</asp:ListItem>
                        <asp:ListItem Value="1">锁定</asp:ListItem>
                    </asp:CheckBoxList>
                </dd>
            </dl>
            <dl>
                <dt>浏览次数</dt>
                <dd>
                    <asp:TextBox ID="txtClick" Text="0" runat="server" CssClass="input normal" datatype="/^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/" errormsg="请输入正确的浏览次数" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
        </div>
        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSave" runat="server" Text="保存案例" CssClass="btn" OnClientClick="bindData();" OnClick="btnSave_Click" />
                <asp:Button ID="btnReset" runat="server" Text="返回列表" CssClass="btn yellow" OnClick="btnReset_Click" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
