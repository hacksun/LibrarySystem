<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/LibrarianPages/MasterPage.master" AutoEventWireup="true" CodeFile="CategoryManagement.aspx.cs" Inherits="Pages_LibrarianPages_CategoryManagement" %>

<asp:Content ID="header" ContentPlaceHolderID="header" runat="Server">
    <script>
        document.getElementById("book").className = "active";
    </script>
    <a> <asp:Label runat="server" Text="<%$ Resources:Resource, CategoryManagement %>" CssClass="navbar-brand"></asp:Label> </a>
    <link href="../../assets/vendors/dropzone/dropzone.min.css" rel="stylesheet" />
    <link href="../../assets/vendors/jquery-ui-1.12.0/jquery-ui.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="Server">
    <div class="card">

   <div class="content">
        <fieldset>
        <div class="form-group">
                <label class="col-sm-1 control-label"><asp:Label ID="Label1" runat="server" Text="<%$ Resources:Resource,CategoryName %>"></asp:Label></label>
                 <div class="col-sm-10">
                <asp:TextBox ID="newName" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="newName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            </fieldset>
        <fieldset>
           <div class="form-group">
                 <label class="col-sm-1 control-label"><asp:Label ID="Label10" runat="server" Text="<%$ Resources:Resource,Title %>"></asp:Label></label>
                    <div class="col-sm-2">
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="selectpicker" data-style="btn btn-primary btn-round">
                        </asp:DropDownList>
	                </div>
                </div>
           </fieldset>
       &nbsp
       <asp:Button ID="Add" runat="server" Text="<%$ Resources:Resource, Add %>" CssClass="btn btn-fill btn-default" OnClick="Add_Click" />
       &nbsp
    <div class="material-datatables">
        <asp:GridView ID="Category" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-no-bordered table-hover" OnRowEditing="Category_RowEditing" OnRowDataBound="Category_RowDataBound" OnRowUpdating="Category_RowUpdating" style="width:100%;cellspacing:0" OnRowCancelingEdit="Category_RowCancelingEdit" OnPageIndexChanging="Category_PageIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="<%$ Resources:Resource, CategoryId %>" HeaderStyle-CssClass="text-primary">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("CategoryId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:Resource, CategoryName %>" HeaderStyle-CssClass="text-primary">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtName" runat="server" Text='<%# Eval("Name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="true"  CausesValidation="false" />
            </Columns>
        </asp:gridview>
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
     <script src="../../assets/vendors/dropzone/dropzone.min.js"></script>
    <script src="../../assets/vendors/jquery.select-bootstrap.js"></script>
    <script>
        var income = $124('#body_Category').DataTable({
            "searching": false,
            "lengthChange": false,
            "order": [[0, 'asc']],
            "bStateSave": true,
            columnDefs: [{
                'targets': [2],
                'orderable': false
            }]
        });
    </script>
</asp:Content>
