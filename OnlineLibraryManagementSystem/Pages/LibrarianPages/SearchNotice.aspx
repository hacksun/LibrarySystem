<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/LibrarianPages/MasterPage.master" AutoEventWireup="true" CodeFile="SearchNotice.aspx.cs" Inherits="Pages_LibrarianPages_SearchNotice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../assets/vendors/daterangepicker/styles/vendor.css" rel="stylesheet" />
    <link href="../../assets/vendors/daterangepicker/styles/daterangepicker.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="header" runat="Server">
    <script>
        document.getElementById("notice").className = "active";
    </script>
    <a>
        <asp:Label runat="server" Text="<%$ Resources:Resource, Notice %>" CssClass="navbar-brand"></asp:Label>
    </a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="Server">
    <div class="card">
        <div class="content">
            <div class="row">
                <div class='col-md-5'>

                    <input name="date" class="daterangepicker-field" style="z-index: 999; background-color: #F3F2EE; border: 1px solid #e8e7e3; border-radius: 4px; color: #364150; font-size: 14px; padding: 7px 18px; height: 40px; width: 210px;" readonly="readonly"></input>
                    <asp:Button ID="search" runat="server" Text="<%$ Resources:Resource, Search %>" CssClass="btn btn-primary btn-fill" OnClick="search_Click" />
                    <asp:Button ID="reset" runat="server" Text="<%$ Resources:Resource, Reset %>" CssClass="btn btn-primary btn-fill" OnClick="reset_Click" />
                </div>
                </br></br>
            </div>

            <div class="material-datatables">
                <asp:GridView ID="History" runat="server" CssClass="table table-striped table-no-bordered table-hover" AutoGenerateColumns="False" Style="width: 100%; cellspacing: 0" OnRowDeleting="GridView1_RowDeleting" DataKeyNames="NoticeId" OnPageIndexChanging="History_PageIndexChanging" OnRowCancelingEdit="History_RowCancelingEdit" OnRowDataBound="History_RowDataBound" OnRowEditing="History_RowEditing" OnRowUpdating="History_RowUpdating">
                    <Columns>
                        <asp:BoundField HeaderText="<%$ Resources:Resource, NoticeId %>" DataField="NoticeId" ReadOnly="true" />
                        <asp:TemplateField HeaderText="<%$ Resources:Resource, Title %>" HeaderStyle-CssClass="text-primary">
                            <EditItemTemplate>
                                <asp:TextBox ID="ttName" runat="server" Text='<%# Eval("Title") %>' MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="llName" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:Resource, Notice %>" HeaderStyle-CssClass="text-primary">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtName" runat="server" Text='<%# Eval("Details") %>' MaxLength="100"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Details") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="<%$ Resources:Resource, Time %>" DataField="Timestamp" ReadOnly="true" />
                        <asp:CommandField ShowEditButton="true" />
                        <asp:CommandField HeaderText="" DeleteText="<%$ Resources:Resource, Delete %>" ShowDeleteButton="true" />
                    </Columns>
                </asp:GridView>
            </div>
            </br></br>
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
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="Server">
    <script src="../../assets/vendors/daterangepicker/scripts/vendor.js"></script>
    <script src="../../assets/vendors/daterangepicker/scripts/daterangepicker.js"></script>
    <script>
        var income = $124('#body_History').DataTable({
            "searching": false,
            "lengthChange": false,
            "order": [[0, 'asc']],
            columnDefs: [{
                'targets': [4, 5],
                'orderable': false
            }]
        });
        $(".daterangepicker-field").daterangepicker({
            forceUpdate: true,
            startDate: '2018-01-01',
            endDate: new Date(),
            single: true,
            callback: function (startDate, endDate, period) {
                var title = startDate.format('L') + ' – ' + endDate.format('L');
                $(this).val(title);
            }
        });

    </script>
</asp:Content>
