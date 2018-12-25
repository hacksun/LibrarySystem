<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/LibrarianPages/MasterPage.master" AutoEventWireup="true" CodeFile="SearchReader.aspx.cs" Inherits="Pages_SearchReader" %>


<asp:Content ID="head" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .resultImg img{
            max-height:84px;
            max-width:60px;
        }
    </style>
    <script src="../../Scripts/art-Template/template-web.js"></script>
    <link href="../../assets/vendors/dropzone/dropzone.min.css" rel="stylesheet" />
    <link href="../../assets/vendors/jquery-ui-1.12.0/jquery-ui.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <script>
        document.getElementById("reader").className = "active";
    </script>
    <a> <asp:Label runat="server" Text="<%$ Resources:Resource, Search %>" CssClass="navbar-brand"></asp:Label> </a>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
      <div style="padding-top:50px">
        <asp:Panel runat="server" DefaultButton="Button2">
        <table style="width: 100%;">
            <tr>
                <td style="width:40%; text-align:right">
                    <div class="col-md-4 col-md-offset-8">
                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="selectpicker" data-style="btn btn-primary btn-round" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged1">
                        <asp:ListItem value="0" Text="<%$ Resources:Resource,Default %>"></asp:ListItem>
                        <asp:ListItem value="1" Text="<%$ Resources:Resource,Account %>" ></asp:ListItem>
                        <asp:ListItem value="2" Text="<%$ Resources:Resource,Name %>" ></asp:ListItem>
                    </asp:DropDownList>
                        </div>
                </td>
                <td style="width:20%">
                    <asp:TextBox ID="TextSearch" runat="server" Width="400px" CssClass="form-control" OnTextChanged="TextSearch_TextChanged"></asp:TextBox>
                </td>
                <td style="width:45%">
                    <div class="col-md-3">
                         </div>
                    <asp:Button ID="Button2" runat="server" Text="<%$ Resources:Resource,Search %>" OnClick="Search"  UseSubmitBehavior="False" CssClass="btn btn-default btn-fill" />
                </td>
            </tr>
            <tr>
                <td style="text-align:center; height:30px" colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <div class="table-responsive">
                <td style="text-align:center" colspan="3">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-no-bordered table-hover" AutoGenerateColumns="False" style="width:100%;cellspacing:0" OnPageIndexChanging="GridView1_PageIndexChanging" >
                        <Columns>
                            <asp:BoundField HeaderText="<%$ Resources:Resource, Name %>" DataField="Name" ReadOnly="true" />
                            <asp:BoundField HeaderText="<%$ Resources:Resource, Phone %>" DataField="Phone" ReadOnly="true" />
                            <asp:BoundField HeaderText="<%$ Resources:Resource, Email %>" DataField="Email" ReadOnly="true" />
                            <asp:TemplateField HeaderText="<%$ Resources:Resource, Check %>">
                             <ItemTemplate>
                                <asp:HyperLink  ID="HyperLink1" runat="server" Text='<%$ Resources:Resource, Check %>' NavigateUrl='<%#Eval("href") %>'></asp:HyperLink>
                             </ItemTemplate>
                         </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                </td>
                </div>
            </tr>
            </table>
            </asp:Panel>
    </div>
    <br />
        <script src="../../assets/vendors/DataTables/jQuery-1.12.4/jquery-1.12.4.min.js"></script>
    <script type="text/javascript">
        var $124 = $;
    </script>
    <script src="../../assets/vendors/jquery.datatables.js"></script>
    <script type="text/javascript">

    </script>
</asp:Content>
<asp:Content ID="foot" runat="server" ContentPlaceHolderID="foot">
    <script src="../../Scripts/art-Template/template-web.js"></script>
    <script src="../../assets/vendors/dropzone/dropzone.min.js"></script>
    <script src="../../assets/vendors/jquery.select-bootstrap.js"></script>
         <script src="../../assets/vendors/daterangepicker/scripts/vendor.js"></script>
    <script src="../../assets/vendors/daterangepicker/scripts/daterangepicker.js"></script>
    <script>
        var income = $124('#body_GridView1').DataTable({
            "searching": false,
            "lengthChange": false,
            "order": [[0, 'asc']],
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
