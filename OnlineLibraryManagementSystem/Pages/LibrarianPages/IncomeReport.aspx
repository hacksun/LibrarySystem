<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/LibrarianPages/MasterPage.master" AutoEventWireup="true" CodeFile="IncomeReport.aspx.cs" Inherits="Pages_LibrarianPages_IncomeReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../assets/vendors/daterangepicker/styles/vendor.css" rel="stylesheet" />
    <link href="../../assets/vendors/daterangepicker/styles/daterangepicker.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="header" Runat="Server">
    <script>
        document.getElementById("statistics").className = "active";
    </script>
    <a> <asp:Label runat="server" Text="<%$ Resources:Resource, IncomeReport %>" CssClass="navbar-brand"></asp:Label> </a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">

    <div class="card">
        <div class="content">
              <div class="row">
                  <div class='col-md-5'>

                      <input name="date" class="daterangepicker-field"  readonly="readonly" style="z-index: 999;background-color: #F3F2EE;border: 1px solid #e8e7e3;border-radius: 4px;color: #364150;font-size: 14px;padding: 7px 18px;height: 40px;width:210px;"></input>
                      <asp:Button ID="search" runat="server" Text="<%$ Resources:Resource, Search %>" CssClass="btn btn-primary btn-fill" OnClick="search_Click" /> 
                      <asp:Button ID="reset" runat="server" Text="<%$ Resources:Resource, Reset %>" CssClass="btn btn-primary btn-fill" OnClick="reset_Click"/> 
                  </div> 
                </br></br>
            </div>
            
            <div class="material-datatables">
                <asp:GridView ID="Income" runat="server" CssClass="table table-striped table-no-bordered table-hover" AutoGenerateColumns="False" style="width:100%;cellspacing:0">
                    <Columns>
                        <asp:BoundField HeaderText="<%$ Resources:Resource, ReaderId %>" DataField="ReaderId" ReadOnly="true" />
                        <asp:BoundField HeaderText="<%$ Resources:Resource, Type %>" DataField="Type" ReadOnly="true" />
                        <asp:BoundField HeaderText="<%$ Resources:Resource, Amount %>" DataField="Amount" ReadOnly="true" />
                        <asp:BoundField HeaderText="<%$ Resources:Resource, Time %>" DataField="Time" ReadOnly="true" />
                    </Columns>
                </asp:GridView>
            </div>
            </br></br>

            <fieldset>
                <div class="form-group">
                    <h5><asp:Label ID="Total_Deposit" runat="server" Text="<%$ Resources:Resource, Total_Deposit %>" CssClass="col-sm-1 control-label"></asp:Label></h5>
                    <div class="col-sm-2">
                        <asp:TextBox ID="Total_Deposit_Text" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                    </div>
                    <h5><asp:Label ID="Label1" runat="server" Text="<%$ Resources:Resource, Yuan %>" CssClass="col-sm-1 control-label"></asp:Label></h5>
                </div>
            </fieldset>

            <fieldset>
                <div class="form-group">
                    <h5><asp:Label ID="Total_Fine" runat="server" Text="<%$ Resources:Resource, Total_Fine %>" CssClass="col-sm-1 control-label"></asp:Label></h5>
                    <div class="col-sm-2">
                        <asp:TextBox ID="Total_Fine_Text" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                    </div>
                    <h5><asp:Label ID="Label3" runat="server" Text="<%$ Resources:Resource, Yuan %>" CssClass="col-sm-1 control-label"></asp:Label></h5>
                </div>
            </fieldset>

            <fieldset>
                <div class="form-group">
                    <h5><asp:Label ID="Total" runat="server" Text="<%$ Resources:Resource, Total_Income %>" CssClass="col-sm-1 control-label"></asp:Label></h5>
                    <div class="col-sm-2">
                        <asp:TextBox ID="Total_Text" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                    </div>
                    <h5><asp:Label ID="Label5" runat="server" Text="<%$ Resources:Resource, Yuan %>" CssClass="col-sm-1 control-label"></asp:Label></h5>
                </div>
            </fieldset>

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
    
    <script src="../../assets/vendors/daterangepicker/scripts/vendor.js"></script>
    <script src="../../assets/vendors/daterangepicker/scripts/daterangepicker.js"></script>
    <script>
        var income = $124('#body_Income').DataTable({
            "searching": false,
            "lengthChange": false,
            "order": [[3, 'asc']],
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


  



