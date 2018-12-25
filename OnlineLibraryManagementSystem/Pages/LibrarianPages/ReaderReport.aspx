<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/LibrarianPages/MasterPage.master" AutoEventWireup="true" CodeFile="ReaderReport.aspx.cs" Inherits="Pages_LibrarianPages_ReaderReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../assets/vendors/daterangepicker/styles/vendor.css" rel="stylesheet" />
    <link href="../../assets/vendors/daterangepicker/styles/daterangepicker.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="header" Runat="Server">
    <script>
        document.getElementById("statistics").className = "active";
    </script>
    <a> <asp:Label runat="server" Text="<%$ Resources:Resource, ReaderReport %>" CssClass="navbar-brand"></asp:Label> </a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">

    <div class="card">
        <div class="content">
            
            <div class="material-datatables">
                <asp:GridView ID="Reader" runat="server" CssClass="table table-striped table-no-bordered table-hover" AutoGenerateColumns="False" style="width:100%;cellspacing:0">
                    <Columns>
                        <asp:BoundField HeaderText="<%$ Resources:Resource, ReaderId %>" DataField="ReaderId" ReadOnly="true" />
                        <asp:BoundField HeaderText="<%$ Resources:Resource, Arrears %>" DataField="Arrears" ReadOnly="true" />
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
    <script type="text/javascript">

    </script>
</asp:Content>
<asp:Content ID="content4" ContentPlaceHolderID="foot" runat="server">
    
    <script>
        var income = $124('#body_Reader').DataTable({
            "searching": false,
            "lengthChange": false,
            "order": [[1, 'desc']],
        });
        
</script>

</asp:Content>




