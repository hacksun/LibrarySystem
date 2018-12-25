<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/ReaderPages/MasterPage.master" AutoEventWireup="true" CodeFile="ChangeReaderInfomation.aspx.cs" Inherits="Pages_ChangeReaderInfomation" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" runat="Server">
    <asp:Panel ID="panEnter" runat="server" DefaultButton="btNewInfomation">
    <div>
        <table style="width: 100%;">
            <tr>
                <td style="width: 45%; text-align: right">
                    <asp:Label ID="lbNewEmail" runat="server" style="font-size:18px; color:black; background-color: rgba(255, 255, 255, 0.5); border:1px solid rgba(255, 255, 255, 0.5); border-radius: 5px;" Text="<%$ Resources:Resource,NewEmail %>"></asp:Label>
                </td>
                <td style="width: 20%; text-align: center">
                    <asp:TextBox ID="tbNewEmail" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                </td>
                <td style="width: 35%">
                    <asp:RequiredFieldValidator ID="rfvNewEmail" runat="server" ControlToValidate="tbNewEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 45%; text-align: right">
                    <asp:Label ID="lbNewPhone" runat="server" style="font-size:18px; color:black; background-color: rgba(255, 255, 255, 0.5); border:1px solid rgba(255, 255, 255, 0.5); border-radius: 5px;" Text="<%$ Resources:Resource,NewPhone %>"></asp:Label>
                </td>
                <td style="width: 20%; text-align: center">
                    <asp:TextBox ID="tbNewPhone" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                </td>
                <td style="width: 35%">
                    <asp:RequiredFieldValidator ID="rfvNewPhone" runat="server" ControlToValidate="tbNewPhone" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: center">
                    <asp:Button ID="btNewInfomation" runat="server" OnClick="btNewInfomation_Click" Text="<%$ Resources:Resource,Submit %>" CssClass="btn btn-default btn-fill" />
                </td>
            </tr>
        </table>
    </div>
    </asp:Panel>
</asp:Content>
