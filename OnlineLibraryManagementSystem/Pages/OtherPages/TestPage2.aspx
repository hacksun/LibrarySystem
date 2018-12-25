<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestPage2.aspx.cs" Inherits="Pages_Blank" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Label" BorderColor="Yellow"></asp:Label>
        &nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" Text="Button" />
            <br />
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        </div>
    </form>
</body>
</html>
