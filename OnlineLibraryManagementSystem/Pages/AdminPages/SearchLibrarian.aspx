<%@ Page Language="C#" MasterPageFile="~/Pages/AdminPages/MasterPage.master" AutoEventWireup="true" CodeFile="SearchLibrarian.aspx.cs" Inherits="Pages_AdminPages_SearchLibrarian" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="header" runat="server">
    <script>
        document.getElementById("librarian").className = "active";
    </script>
    <a> <asp:Label runat="server" Text="<%$ Resources:Resource, LibrarianManagement %>" CssClass="navbar-brand"></asp:Label> </a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">
    <div class="card">
        <div class="content">
            <div class="form-group">
                <label><asp:Label ID="Label1" runat="server" Text="<%$ Resources:Resource, Account %>"></asp:Label></label>
                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAccount" runat="server" ControlToValidate="TextBox2" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label><asp:Label ID="Label3" runat="server" Text="<%$ Resources:Resource, Password %>"></asp:Label></label>
                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" Text="00010001" onpaste="return false;" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="TextBox3" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label><asp:Label ID="Label4" runat="server" Text="<%$ Resources:Resource, Name %>"></asp:Label></label>
                <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="TextBox4" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:Resource, Add %>" CssClass="btn btn-fill btn-default" OnClick="Button1_Click" />
        </div>
    </div>
    <div class="card">
        <div class="content">
            <div class="material-datatables">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" CssClass="table table-striped table-no-bordered table-hover" OnPageIndexChanging="GridView1_PageIndexChanging" >
                        <Columns>
                            <asp:BoundField DataField="LibrarianId" HeaderText="LibrarianId" ReadOnly="True" HeaderStyle-CssClass="text-primary" ItemStyle-Width="120"/>
                            
                            <asp:BoundField DataField="Account" HeaderText="<%$ Resources:Resource, Account %>" HeaderStyle-CssClass="text-primary"/>
                            <asp:BoundField DataField="Password" HeaderText="<%$ Resources:Resource, Password %>" HeaderStyle-CssClass="text-primary"/>
                            <asp:BoundField DataField="Name" HeaderText="<%$ Resources:Resource, Name %>" HeaderStyle-CssClass="text-primary"/>
                            <asp:CommandField ShowEditButton="True" HeaderStyle-CssClass="text-primary" ItemStyle-Width="100" CausesValidation="false"/>
                            <asp:CommandField ShowDeleteButton="True" HeaderStyle-CssClass="text-primary" ItemStyle-Width="100"/>
                        </Columns>

                    </asp:GridView>


        </div>
        </div>
    </div>

        <script src="../../assets/vendors/DataTables/jQuery-1.12.4/jquery-1.12.4.min.js"></script>
    <script type="text/javascript">
        var $124 = $;
    </script>
    <script src="../../assets/vendors/jquery.datatables.js"></script>
</asp:Content>

<asp:Content ID="content4" ContentPlaceHolderID="foot" runat="server">
    
    <script>
        var income = $124('#body_GridView1').DataTable({
            "lengthChange": false,
            "order": [[0, 'asc']],
            "bStateSave": true,
            columnDefs: [{
                'targets': [2,4,5],
                'orderable': false
            }]
        });
        
</script>

</asp:Content>
