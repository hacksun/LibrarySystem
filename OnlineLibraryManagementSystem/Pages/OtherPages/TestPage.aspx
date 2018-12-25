<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MasterPage.master" AutoEventWireup="true" CodeFile="TestPage.aspx.cs" Inherits="Pages_TestPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <asp:Label ID="lbTest" runat="server" Text="<%$ Resources:Resource,TestString %>"></asp:Label>
    <br />
    <asp:Label ID="lbBookInfo" runat="server" Text="Book Information Show Failed"></asp:Label>
    <br />
    <asp:Button ID="btShowBarcode" runat="server" OnClick="btShowBarcode_Click" Text="Show Barcode" Cssclass="btn btn-fill btn-default" />
    <br />
    <asp:ChangePassword ID="ChangePassword1" runat="server">
    </asp:ChangePassword>
    <br />
    <br />
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server">
        <WizardSteps>
            <asp:CreateUserWizardStep runat="server" />
            <asp:CompleteWizardStep runat="server" />
        </WizardSteps>
    </asp:CreateUserWizard>
    <br />
    <asp:Login ID="Login1" runat="server" DestinationPageUrl="~/Pages/search.aspx" HelpPageText="Forget Password" PasswordRecoveryText="Reset Password" UserName="PhoneNumber">
    </asp:Login>
    <br />
    <br />
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            <asp:LinkButton ID="LinkButton1" runat="server">Login</asp:LinkButton>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Pages/ReaderPages/ShowReaderInfo.aspx">My Account</asp:HyperLink>
            &nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton2" runat="server">Logout</asp:LinkButton>
        </LoggedInTemplate>
    </asp:LoginView>
    &nbsp;<br />
<br />
    <br />
    <asp:PasswordRecovery ID="PasswordRecovery1" runat="server">
    </asp:PasswordRecovery>
    <br />
    <br />
    <asp:FileUpload ID="FileUpload1" runat="server" />
</asp:Content>

