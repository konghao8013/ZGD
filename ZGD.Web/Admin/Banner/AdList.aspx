<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdList.aspx.cs" Inherits="ZGD.Web.Admin.Banner.AdList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../skin/pagination/pagination.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery/jquery-3.7.1.min.js"></script><script type="text/javascript" src="../js/jquery/jquery-migrate-1.4.1.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <script type="text/javascript" src="/Content/Scripts/layer/layer.js"></script>
    <link href="../Js/webuploader-0.1.5/webuploader.css" rel="stylesheet" />
    <script type="text/javascript" src="../Js/webuploader-0.1.5/webuploader.min.js"></script>
    <script type="text/javascript" src="../Js/uploadimg.js"></script>
    <style type="text/css">
        .red{color:red;}
    </style>

    <script type="text/javascript" language="javascript">
        var handlerUrl = "../Handler/Ad.ashx"; // 处理页面 

        var dialog, load;
        function ShowAdPanel(id) {
            $.ajax({
                type: "post",
                url: handlerUrl,
                data: { action: 'GetAd', id: id },
                dataType: "json",
                beforeSend: function () {
                    load = layer.load(1, {
                        shade: [0.1, '#fff']
                    });
                },
                success: function (msg) {
                    dialog = layer.open({
                        title: '设置图片',
                        shift: 5,
                        type: 1,
                        area: '500px',
                        content: $("#divUpload").html(),
                        success: function (layero, index) {
                            layer.close(load);
                            if (msg != null && msg != "") {
                                $("#hidId").val(msg.ID);
                                $("#ddlType").val(msg.aType);
                                $("#txtUrl").val(msg.Url);
                                $("#txtTitle").val(msg.Title);
                                $("#txtDesc").val(msg.Description);
                                $("#txtSort").val(msg.Sort);
                                $("#txtSize").val(msg.Size);
                                $("#txtImgUrl").val(msg.ImgUrl);
                                if (msg.IsLock == 1) $("#cbIsLock").attr("checked", true); else $("#cbIsLock").removeAttr("checked");
                                $("#uploader-list").html("<a href=\"" + msg._url + "\" target=\"_blank\"><img src=\"" + msg._imgurl + "\" height=\"80\" /></a>").show();
                            }

                            //初始化上传控件
                            imgMethod.init('uploader-list', 'filePicker', 'txtImgUrl', 'event', 1, "back_", '', function (file, resp) {
                                return $('<div class="file-box" id="file_back_f1"><div class="file">' +
                                    '<div id="f1" class="file-item thumbnail">' +
                                    '<img id="img_back_f1" src="' + resp.url + '" data-src="' + resp.url + '" class="img-responsive"><p class="progress img_up">上传排队中</p></div></div></div>');
                            });

                            $("#btnSave").click(function () {
                                var d = {};
                                d.action = "SaveAd";
                                d.id = $("#hidId").val();
                                d.type = $("#ddlType").val();
                                d.url = $("#txtUrl").val();
                                d.title = $("#txtTitle").val();
                                d.desc = $("#txtDesc").val();
                                d.sort = $("#txtSort").val();
                                d.size = $("#txtSize").val();
                                d.imgurl = $("#txtImgUrl").val();
                                d.islock = $("#cbIsLock").prop("checked") ? 1 : 0;
                                $.ajax({
                                    type: "post",
                                    url: handlerUrl,
                                    data: d,
                                    beforeSend: function () {
                                        load = layer.load(1, {
                                            shade: [0.1, '#fff']
                                        });
                                    },
                                    success: function (msg) {
                                        layer.close(load);
                                        if (msg != null && msg != "" && msg == "1") {
                                            window.location.href = window.location.href;
                                        }
                                        else
                                            alert("提交失败！");
                                    }
                                });
                            })
                        }
                    });
                }
            });
        }

        function AddPanel() {
            dialog = layer.open({
                title: '新增信息',
                shift: 5,
                type: 1,
                area: '500px',
                content: $("#divUpload").html(),
                success: function (layero, index) {
                    $(".msgtable input[type='text']").val("");
                    //初始化上传控件
                    imgMethod.init('uploader-list', 'filePicker', 'txtImgUrl', 'event', 1, "back_", '', function (file, resp) {
                        return $('<div class="file-box" id="file_back_f1"><div class="file">' +
                            '<div id="f1" class="file-item thumbnail">' +
                            '<img id="img_back_f1" src="' + resp.url + '" data-src="' + resp.url + '" class="img-responsive"><p class="progress img_up">上传排队中</p></div></div></div>');
                    });

                    $("#btnSave").click(function () {
                        var d = {};
                        d.action = "AddAd";
                        d.type = $("#ddlType").val();
                        d.url = $("#txtUrl").val();
                        d.title = $("#txtTitle").val();
                        d.desc = $("#txtDesc").val();
                        d.sort = $("#txtSort").val();
                        d.size = $("#txtSize").val();
                        d.imgurl = $("#txtImgUrl").val();
                        d.islock = $("#cbIsLock").prop("checked") ? 1 : 0;
                        $.ajax({
                            type: "post",
                            url: handlerUrl,
                            data: d,
                            beforeSend: function () {
                                load = layer.load(1, {
                                    shade: [0.1, '#fff']
                                });
                            },
                            success: function (msg) {
                                layer.close(load);
                                if (msg != null && msg != "" && msg == "1") {
                                    window.location.href = window.location.href;
                                }
                                else
                                    alert("提交失败！");
                            }
                        });
                    })
                }
            });
        }
    </script>
</head>
<body class="mainbody">
    <!--导航栏-->
    <div class="location">
        <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
        <i class="arrow"></i>
        <a href="List.aspx"><span>页面广告</span></a>
    </div>
    <!--/导航栏-->
    <form id="form1" runat="server">
        <!--工具栏-->
        <%--<div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="l-list">
                    <ul class="icon-list">
                        <li><a class="add" href="javascript:;" onclick="AddPanel();"><i></i><span>新增</span></a></li>
                        <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                        <li>
                            <asp:LinkButton ID="lbtnDel" runat="server" OnClientClick="return confirm( '确定要删除这些记录吗？ ');" CssClass="del" OnClick="lbtnDel_Click"><i></i><span>删除</span></asp:LinkButton></li>
                    </ul>
                </div>
            </div>
        </div>--%>
        <!--/工具栏-->
        <div class="line10"></div>
        <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand">
            <HeaderTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                    <tr>
                        <th width="6%">选择</th>
                        <th width="6%">编号</th>
                        <th>类型</th>
                        <th>图片</th>
                        <th>尺寸（宽×高）</th>
                        <th align="left">标题</th>
                        <th width="15%">发布时间</th>
                        <th>排序号</th>
                        <th>状态</th>
                        <th width="10%">操作</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <asp:CheckBox ID="cb_id" CssClass="checkall" runat="server" /></td>
                    <td align="center">
                        <asp:Label ID="lb_id" runat="server" Text='<%#HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"Id")%>'></asp:Label></td>
                    <td align="center"><%# ZGD.Common.StringHandler.GetBannerType(HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"aType").ToString())%></td>
                    <td align="center"><a href="<%#HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"Url")%>" target="_blank">
                        <img src="<%#HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"ImgUrl")%>" height="50" /></a></td>
                    <td align="center"><%#HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"Size")%></td>
                    <td><%#HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"Title")%></td>
                    <td align="center"><%#string.Format("{0:g}", HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"PubTime"))%></td>
                    <td align="center"><%#HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"Sort")%></td>
                    <td align="center">
                        <asp:ImageButton ID="ibtnLock" CommandName="ibtnLock" runat="server" ImageUrl='<%# Convert.ToInt32(HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"IsLock")) == 1 ? "../Images/disable.gif" : "../Images/correct.gif"%>' ToolTip='<%# Convert.ToInt32(HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"IsLock")) == 1 ? "取消锁定" : "设置锁定"%>' />
                    </td>
                    <td align="center"><span><a href="javascript:ShowAdPanel(<%#HtmlEncodeBinder.HtmlEncodeEval(Container.DataItem,"Id")%>);">修改</a></span></td>
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
                    NextPageText="下一页" PageSize="20"
                    PrevPageText="前一页" ShowCustomInfoSection="Left" ShowInputBox="Always"
                    ShowNavigationToolTip="True" Style="font-size: 12px"
                    SubmitButtonClass="formfield" SubmitButtonText="GO" Width="700px"
                    OnPageChanging="AspNetPager1_PageChanging" PageIndexBoxType="TextBox"
                    ShowPageIndexBox="Never" AlwaysShow="True">
                </webdiyer:AspNetPager>
            </div>
        </div>
    </form>

    <script type="text/html" id="divUpload">
        <div style="padding: 15px 20px;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
                <tr>
                    <td align="right">上传图片
                    </td>
                    <td>
                        <div id="uploader-demo">
                            <div id="uploader-list" class="uploader-list"></div>
                            <div id="filePicker">选择图片</div>
                        </div>
                        <input id="txtImgUrl" type="hidden" />
                        <input type="hidden" id="hidId" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <font class="red">*</font>图片类型
                    </td>
                    <td>
                        <select id="ddlType">
                            <option value="2" selected="selected">页面广告</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <font class="red">*</font>图片尺寸
                    </td>
                    <td>
                        <input type="text" class="input" id="txtSize" value="#" />
                        <span>宽×高（像素）</span>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <font class="red">*</font>链接地址
                    </td>
                    <td>
                        <input type="text" class="input normal" id="txtUrl" value="#" />
                    </td>
                </tr>
                <tr>
                    <td align="right">标题
                    </td>
                    <td>
                        <input type="text" id="txtTitle" class="input normal" />
                    </td>
                </tr>
                <tr>
                    <td align="right">描述
                    </td>
                    <td>
                        <input type="text" id="txtDesc" class="input normal" />
                    </td>
                </tr>
                <tr>
                    <td align="right">排序
                    </td>
                    <td>
                        <input type="text" id="txtSort" class="input" value="0" />
                    </td>
                </tr>
                <tr>
                    <td align="right">状态
                    </td>
                    <td>
                        <input id="cbIsLock" type="checkbox" />
                        关闭
                    </td>
                </tr>
                <tr>
                    <td align="right"></td>
                    <td>
                        <input id="btnSave" type="button" value="确认提交" class="btn" />
                    </td>
                </tr>
            </table>
        </div>
    </script>
</body>
</html>
