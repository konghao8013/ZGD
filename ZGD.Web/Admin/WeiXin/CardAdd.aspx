<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardAdd.aspx.cs" ValidateRequest="false" Inherits="ZGD.Web.Admin.WeiXin.CardAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>发布微名片</title>
    <link rel="stylesheet" type="text/css" href="../Images/style.css" />
    <script type="text/javascript" src="../../Js/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="../../Js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../Js/messages_cn.js"></script>
    <script type="text/javascript" src="../../Js/jquery.form.js"></script>
    <script type="text/javascript" src="../Js/function.js"></script>
    <script type="text/javascript">
        $(function () {
            //表单验证JS
            $("#form1").validate({
                //出错时添加的标签
                errorElement: "span",
                success: function (label) {
                    //正确时的样式
                    label.text(" ").addClass("success");
                }
            });
        });
    </script>
</head>
<body style="padding: 10px;">
    <form id="form1" runat="server">
        <div class="navigation">
            <span class="back"><a href="CardList.aspx">返回列表</a></span><b>您当前的位置：微信管理 &gt;微名片管理 &gt; 发布微名片</b>
        </div>
        <div class="spClear"></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
            <tr>
                <th colspan="2" align="left">微名片详细</th>
            </tr>
            <tr>
                <td width="15%" align="right">名片用户：</td>
                <td width="85%">
                    <asp:TextBox ID="txtName" runat="server" CssClass="input required"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="15%" align="right">所属部门：</td>
                <td width="85%">
                    <asp:DropDownList ID="ddlProperty" runat="server" CssClass="select required">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="15%" align="right">所在职位：</td>
                <td width="85%">
                    <asp:TextBox ID="txtRole" runat="server" CssClass="input required"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="15%" align="right">联系电话：</td>
                <td width="85%">
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="input required"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">名片头像：</td>
                <td>
                    <asp:TextBox ID="txtImgUrl" runat="server" CssClass="input w380 left"></asp:TextBox>
                    <a href="javascript:void(0);" class="files">
                        <input type="file" id="FileUpload" name="FileUpload" onchange="SingleUpload('txtImgUrl','FileUpload')" /></a>
                    <span class="uploading">正在上传，请稍候...</span><span>（尺寸100×100ppx）</span>
                </td>
            </tr>
            <tr>
                <td width="15%" align="right">名片链接：</td>
                <td width="85%">
                    <asp:TextBox ID="txtCardUrl" runat="server" CssClass="input w380 required"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="15%" align="right">备注：</td>
                <td width="85%">
                    <textarea id="txtRemark" runat="server" class="input w380" style="height: 50px"></textarea>
                </td>
            </tr>
        </table>
        <div style="margin-top: 10px; text-align: center;">
            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="submit"
                OnClick="btnSave_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;
   <asp:Button ID="btnReset" runat="server" Text="重置" CssClass="submit"
       OnClick="btnReset_Click" />
        </div>
    </form>
</body>
</html>
