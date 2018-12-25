<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/LibrarianPages/MasterPage.master" AutoEventWireup="true" CodeFile="FineInfo.aspx.cs" Inherits="Pages_LibrarianPages_FineInfo" %>
<asp:Content ID="header" ContentPlaceHolderID="header" Runat="Server">
    <script>
        document.getElementById("fine").className = "active";
    </script>
    <a> <asp:Label runat="server" Text="<%$ Resources:Resource, Fine %>" CssClass="navbar-brand"></asp:Label> </a>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" Runat="Server">

    <div class="table-responsive">
        <asp:GridView ID="FineOverdue" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
            <Columns>
                <asp:BoundField HeaderText="<%$ Resources:Resource, ReaderId %>" DataField="ReaderID" ReadOnly="true" HeaderStyle-CssClass="text-primary">
                </asp:BoundField>
                <asp:BoundField HeaderText="<%$ Resources:Resource, Barcode %>" DataField="BookBarcode" ReadOnly="true" HeaderStyle-CssClass="text-primary">
                </asp:BoundField>
                <asp:BoundField HeaderText="<%$ Resources:Resource, IssueTime %>" DataField="IssueTime" ReadOnly="true" HeaderStyle-CssClass="text-primary">
                </asp:BoundField>
                <asp:BoundField HeaderText="<%$ Resources:Resource, Status %>" DataField="newStatus" ReadOnly="true" HeaderStyle-CssClass="text-primary">
                </asp:BoundField>
                <asp:BoundField HeaderText="<%$ Resources:Resource, ReturnTime %>" DataField="ReturnTime" ReadOnly="true" HeaderStyle-CssClass="text-primary">
                </asp:BoundField>
                <asp:BoundField HeaderText="<%$ Resources:Resource, OverDueTime %>" DataField="OverdueLength" ReadOnly="true" HeaderStyle-CssClass="text-primary">
                </asp:BoundField>
                <asp:BoundField HeaderText="<%$ Resources:Resource, Fine %>" DataField="Fine" ReadOnly="true" HeaderStyle-CssClass="text-primary">
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>

