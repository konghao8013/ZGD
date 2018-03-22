<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ZGD.Web.Admin.Login" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>管理员登录</title>
    <link href="skin/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        $(function () {
            //检测IE
            if ('undefined' == typeof (document.body.style.maxHeight)) {
                window.location.href = 'ie6update.html';
            }
        });
        function ChangeCode() {
            var date = new Date();
            var myImg = document.getElementById("ImageCheck");
            myImg.src = "../Tools/verify_code.ashx?flag=" + date.getMilliseconds();
            return false;
        }
    </script>
</head>

<body class="loginbody">
    <form id="form1" runat="server">
        <div class="login-screen">
            <div class="login-form">
                <h1>系统管理登录</h1>
                <div class="control-group">
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="login-field" placeholder="用户名" title="用户名"></asp:TextBox>
                    <label class="login-field-icon user" for="txtUserName"></label>
                </div>
                <div class="control-group">
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="login-field" TextMode="Password" placeholder="密码" title="密码"></asp:TextBox>
                    <label class="login-field-icon pwd" for="txtPassword"></label>
                </div>
                <div class="control-group" style="text-align: left; padding-left: 6px;">
                    <asp:TextBox ID="txtCode" runat="server" CssClass="login-field" Width="100" placeholder="验证码" title="验证码"></asp:TextBox>
                    <asp:Image ID="ImageCheck" runat="server" onclick="ChangeCode();" style="cursor:pointer;margin-top:-8px;border-radius:5px;" Height="40" ImageUrl="../Tools/verify_code.ashx" ImageAlign="AbsMiddle" ToolTip="看不清，换一个"></asp:Image>
                </div>
                <div>
                    <asp:Button ID="btnSubmit" runat="server" Text="登 录" CssClass="btn-login" OnClick="btnSubmit_Click" /></div>
                <span class="login-tips"><i></i><b id="msgtip" runat="server">请输入用户名和密码</b></span>
            </div>
        </div>
    </form>
</body>
</html>
