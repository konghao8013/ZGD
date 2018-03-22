<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnCode.aspx.cs" Inherits="ZGD.Web.Admin.EnCode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server" Width="80%"></asp:TextBox><asp:Button ID="Button1" runat="server" Text="编码" OnClick="Button1_Click" /><br />
        <asp:TextBox ID="Label1" TextMode="MultiLine" Width="80%" Height="100" runat="server"></asp:TextBox><br />
        <asp:TextBox ID="TextBox2" runat="server" Width="80%"></asp:TextBox><asp:Button ID="Button2" runat="server" Text="解码" OnClick="Button2_Click" /><br />
        <asp:TextBox ID="Label2" TextMode="MultiLine" Width="80%" Height="100" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
