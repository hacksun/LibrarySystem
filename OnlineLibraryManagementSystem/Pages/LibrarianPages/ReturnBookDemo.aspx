<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/LibrarianPages/MasterPage.master" AutoEventWireup="true" CodeFile="ReturnBookDemo.aspx.cs" Inherits="Pages_ReturnBookDemo" %>

<asp:Content ID="head" ContentPlaceHolderID="header" runat="Server">
    <script>
        document.getElementById("circulation").className = "active";
    </script>
    <a>
        <asp:label runat="server" text="<%$ Resources:Resource, Return %>" cssclass="navbar-brand"></asp:label>
    </a>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" runat="Server">
    <asp:Panel ID="panEnter" runat="server" DefaultButton="btReturn">
    <div class="card">
        <div class="content">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 40%; text-align: right" class="auto-style1">
                        <label>
                            <asp:label id="lbBarcode" runat="server" text="<%$ Resources:Resource,Barcode %>"></asp:label>
                        </label>
                        &nbsp;
                    </td>
                    <td style="width: 40%; text-align: center" class="auto-style2">
                        <asp:textbox id="tbBarcode" runat="server" width="300px" cssclass="form-control" onkeypress="if ((event.keyCode<48 || event.keyCode>57)) event.returnValue=false"></asp:textbox>
                    </td>
                    <td>
                        <asp:requiredfieldvalidator id="rfvBarcode" runat="server" controltovalidate="tbBarcode" errormessage="*" forecolor="Red"></asp:requiredfieldvalidator>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:button id="btReturn" runat="server" onclick="btReturn_Click" text="<%$ Resources:Resource,Return %>" cssclass="btn btn-default btn-fill" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .auto-style1 {
            height: 20px;
        }
    </style>
</asp:Content>


