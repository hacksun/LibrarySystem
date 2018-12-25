<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AdminPages/MasterPage.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Pages_AdminPages_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="header" Runat="Server">
    <script>
        document.getElementById("changePassword").className = "active";
    </script>
    <a> <asp:Label runat="server" Text="<%$ Resources:Resource, ChangePassword %>" CssClass="navbar-brand"></asp:Label> </a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">
 <div>
        <table style="width: 100%;">
            <tr>
                <td style="width: 45%; text-align: right">
                    <label><asp:Label ID="aOldPassword" runat="server" Text="<%$ Resources:Resource,OldPassword %>"></asp:Label></label>
                    &nbsp;
                </td>
                <td style="width: 20%; text-align: center">
                    <asp:TextBox ID="tbOldPassword" runat="server" Width="300px" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </td>
                <td style="width: 35%">
                    <asp:RequiredFieldValidator ID="rfvOldPassword" runat="server" ControlToValidate="tbOldPassword" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 45%; text-align: right">
                    <label><asp:Label ID="aNewPassword" runat="server" Text="<%$ Resources:Resource,NewPassword %>"></asp:Label></label>
                    &nbsp;
                </td>
                <td style="width: 20%; text-align: center">
                    <asp:TextBox ID="tbNewPassword" runat="server" Width="300px" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </td>
                <td style="width: 35%">
                    <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="tbNewPassword" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 45%; text-align: right">
                    <label><asp:Label ID="aConfirmNewPassword" runat="server" Text="<%$ Resources:Resource,ConfirmNewPassword %>"></asp:Label></label>
                    &nbsp;
                </td>
                <td style="width: 20%; text-align: center">
                    <asp:TextBox ID="tbConfirmNewPassword" runat="server" Width="300px" TextMode="Password" CssClass="form-control" ></asp:TextBox>
                </td>
                <td style="width: 35%">
                    <asp:RequiredFieldValidator ID="rfvConfirmNewPassword" runat="server" ControlToValidate="tbConfirmNewPassword" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvNewPassword" runat="server" ControlToCompare="tbConfirmNewPassword" ControlToValidate="tbNewPassword" ErrorMessage="x" Display="Dynamic" Operator="Equal" ForeColor="Red"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: center">
                    <asp:Button ID="btChangePassword" runat="server" OnClick="btChangePassword_Click" Text="<%$ Resources:Resource,ChangePassword %>" CssClass="btn btn-default btn-fill" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" Runat="Server">
</asp:Content>

