<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/LibrarianPages/MasterPage.master" AutoEventWireup="true" CodeFile="IssueBookDemo.aspx.cs" Inherits="Pages_IssueBookDemo" %>

<asp:Content ID="head" ContentPlaceHolderID="header" runat="Server">
    <script>
        document.getElementById("circulation").className = "active";
    </script>
    <a>
        <asp:Label runat="server" Text="<%$ Resources:Resource, Issue %>" CssClass="navbar-brand"></asp:Label>
    </a>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="Server">
    <asp:Panel ID="panEnter" runat="server" DefaultButton="btIssue">
    <div class="card">
        <div class="content">
            <table class="nav-justified">
                <tr>
                    <td style="text-align: right;" class="auto-style1">
                        <label><asp:Label ID="lbReaderId" runat="server" Text="<%$ Resources:Resource,ReaderId %>"></asp:Label></label>
                        &nbsp;
                    </td>
                    <td style="text-align: center;" class="auto-style2">
                        <asp:TextBox ID="tbReaderId" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td class="auto-style1">
                        <asp:RequiredFieldValidator ID="rfvReaderId" runat="server" ControlToValidate="tbReaderId" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr style="margin: 5px,0px,10px,0px">
                    <td style="width: 40%; text-align: right; padding: 5px,5px,2.5px,5px;">
                        <label><asp:Label ID="lbBarcode" runat="server" Text="<%$ Resources:Resource,Barcode %>"></asp:Label></label>
                        &nbsp;
                    </td>
                    <td style="width: 40%; text-align: center; padding: 2.5px,5px,5px,5px">
                        <asp:TextBox ID="tbBarcode" runat="server" Width="300px" CssClass="form-control" onKeyPress="if ((event.keyCode<48 || event.keyCode>57)) event.returnValue=false"></asp:TextBox>
                    </td>
                    <td style="width: 20%">
                        <asp:RequiredFieldValidator ID="rfvBarcode" runat="server" ControlToValidate="tbBarcode" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:Button ID="btIssue" runat="server" OnClick="btIssue_Click" Text="<%$ Resources:Resource,Issue %>" CssClass="btn btn-default btn-fill" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </asp:Panel>
</asp:Content>

