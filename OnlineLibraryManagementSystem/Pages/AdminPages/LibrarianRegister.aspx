<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AdminPages/MasterPage.master" AutoEventWireup="true" CodeFile="LibrarianRegister.aspx.cs" Inherits="Pages_OtherPages_LibrarianRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
        <a> <asp:Label runat="server" Text="<%$ Resources:Resource, LibrarianRegister %>" CssClass="navbar-brand"></asp:Label> </a>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="body">
    <style type="text/css">
        .style1 {
            width: 100%;
            text-align: center;
            font-family: Consolas;
        }
        .auto-style1 {
            text-align: center;
        }
        .auto-style2 {
            width: 50%;
            text-align: right;
            height: 35px;
            font-size: x-large;
        }

        .auto-style4 {
            width: 50%;
            text-align: left;
            height: 35px;
            font-size: x-large;
        }
        .auto-style5 {
            font-size: xx-large;
        }
        .auto-style7 {
            font-size: large;
        }
    </style>
    <div class="auto-style1">
        <table class ="style1" dir="ltr">
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Resource, Name %>"></asp:Label>
                </td>
                <td class="auto-style4">
                    <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Resource, Account %>"></asp:Label>
                </td>
                <td class="auto-style4">
                    <asp:TextBox ID="TextBoxAccount" runat="server" TextMode="Number"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Resource, Password %>"></asp:Label>
                </td>
                <td class="auto-style4">
                    <asp:TextBox ID="TextBoxPassword" runat="server" Text="00010001"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        
        <br /> <br />
        <div class="style1">
            <asp:Button ID="SubmitButton" runat="server" Text="<%$ Resources:Resource, Register %>" OnClick="RegisterReader"  CausesValidation="False" CssClass="auto-style7" />
            <asp:Button ID="CancelButton" runat="server" Text="<%$ Resources:Resource, Cancel %>" OnClick="Cancel" CausesValidation="False" CssClass="auto-style7" />
        </div>
    </div>
  
      
</asp:Content>