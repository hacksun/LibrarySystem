<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/LibrarianPages/MasterPage.master" AutoEventWireup="true" CodeFile="DamageFine.aspx.cs" Inherits="Pages_LibrarianPages_DamageFine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 37%;
            height: 61px;
        }
        .auto-style2 {
            width: 26%;
            height: 61px;
        }
        .auto-style3 {
            width: 43%;
            height: 61px;
        }
        .auto-style4 {
            width: 43%;
            height: 93px;
        }
        .auto-style5 {
            width: 26%;
            height: 93px;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="header" Runat="Server">
    <script>
        document.getElementById("fine").className = "active";
    </script>
    <a> <asp:Label runat="server" Text="<%$ Resources:Resource, Reparation %>" CssClass="navbar-brand"></asp:Label> </a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div>
    </div>
        <table style="width: 100%;">
            <tr>
                <td style="text-align:right" class="auto-style3">
                    <label><asp:Label ID="lbBarcode" runat="server" Text="<%$ Resources:Resource,Barcode %>"></asp:Label>
                </td>
                <td style="text-align:center" class="auto-style2">
                    <asp:TextBox ID="tbBarcode" runat="server" Width="300px" CssClass="form-control" onKeyPress="if ((event.keyCode<48 || event.keyCode>57)) event.returnValue=false"></asp:TextBox>
                </td>
                <td class="auto-style1">
                    <asp:RequiredFieldValidator ID="rfvBarcode" runat="server" ControlToValidate="tbBarcode" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr>
                <td style="text-align:right" class="auto-style4">
                    <label><asp:Label ID="Type" runat="server" Text="<%$ Resources:Resource,Type %>"></asp:Label></label>
                </td>
                <td style="text-align:center" class="auto-style5">
                    <div class="select">
                        <asp:DropDownList ID="TypeField" runat="server" Width="300px" CssClass="form-control">
                            <asp:ListItem Text="<%$ Resources:Resource,Damage %>" Value="Damage" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="<%$ Resources:Resource,Lost %>" Value="Lost"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align:center">
                    <asp:Button ID="fineReparation" runat="server" Text="<%$ Resources:Resource,Reparation %>" OnClick="fineReparation_Click" CssClass="btn btn-fill btn-default"/>
                </td>
            </tr>
        </table>
    </asp:Content>

