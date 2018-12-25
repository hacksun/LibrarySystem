<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/LibrarianPages/MasterPage.master" AutoEventWireup="true" CodeFile="ReaderRegistration.aspx.cs" Inherits="Pages_ReaderRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../assets/vendors/sweetalert/css/sweetalert2.min.css" rel="Stylesheet" >
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="header" runat="server">
    <script>
        document.getElementById("reader").className = "active";
    </script>
        <a> <asp:Label runat="server" Text="<%$ Resources:Resource, ReaderRegister %>" CssClass="navbar-brand"></asp:Label> </a>
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="body">
    <div class="card">
    <div class="content">
    <div class="form-group label-floating">
        <label><asp:Label ID="Label3" runat="server" Text="<%$ Resources:Resource, Name %>" CssClass="control-label"></asp:Label>
        <star>*</star></label>
        <asp:TextBox ID="TextBoxName" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
             ErrorMessage="<%$ Resources:Resource, UserNameError %>" ControlToValidate ="TextBoxName" ForeColor="Red"></asp:RequiredFieldValidator>
    </div>
    <div class="form-group label-floating">
        <label><asp:Label ID="Label4" runat="server" Text="<%$ Resources:Resource, IDNumber %>" CssClass="control-label"></asp:Label>
        <star>*</star></label>
        <asp:TextBox ID="TextBoxIDNumber" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
              ErrorMessage="<%$ Resources:Resource, IdNumberError %>" ControlToValidate="TextBoxIDNumber" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
       <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
              ErrorMessage="<%$ Resources:Resource, IDNumberLegel %>" ControlToValidate="TextBoxIDNumber" ValidationExpression="^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$" Display="Dynamic" ForeColor="Red" ></asp:RegularExpressionValidator>
                    
    </div>
    <div class="form-group label-floating">
        <label><asp:Label ID="Label5" runat="server" Text="<%$ Resources:Resource, Telephone %>" CssClass="control-label"></asp:Label>
        <star>*</star></label>
        <asp:TextBox ID="TextBoxTelephone" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ErrorMessage="<%$ Resources:Resource, PhoneError %>" ControlToValidate ="TextBoxTelephone" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
     </div>
    <div class="form-group label-floating">
        <label><asp:Label ID="Label6" runat="server" Text="<%$ Resources:Resource, Email %>" CssClass="control-label"></asp:Label>
        <star>*</star></label>
        <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
             ErrorMessage="<%$ Resources:Resource, EmailError %>" ControlToValidate ="TextBoxEmail" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>        
    </div>
    <div class="category form-category">
        <star>*</star>
        <asp:Label ID="Lable7" runat="server" Text="<%$ Resources:Resource, RequiredFields %>"> </asp:Label>
    </div>
    <div class="form-footer text-right">
        <div class="form-group">
            <label class="pull-left">
                <asp:checkbox runat="server" Id = "IsDeposit" Text="" ></asp:checkbox>   
            </label>
        </div>
        <asp:Button ID="SubmitButton" runat="server" Text="<%$ Resources:Resource, Register %>"  OnClick="RegisterReader"  CausesValidation="True" CssClass="btn btn-rose btn-fill" />
    </div>
    </div>   
    </div>

</asp:Content>