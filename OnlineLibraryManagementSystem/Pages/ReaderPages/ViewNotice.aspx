<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/ReaderPages/MasterPage.master" AutoEventWireup="true" CodeFile="ViewNotice.aspx.cs" Inherits="Pages_ReaderPages_ViewNotice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../assets/vendors/daterangepicker/styles/vendor.css" rel="stylesheet" />
    <link href="../../assets/vendors/daterangepicker/styles/daterangepicker.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">
    <div class="card">
        <div class="content">
              <div class="row">
                  <div class='col-md-5'>

                      <input name="date" class="daterangepicker-field" style="z-index: 999;background-color: #F3F2EE;border: 1px solid #e8e7e3;border-radius: 4px;color: #364150;font-size: 14px;padding: 7px 18px;height: 40px;width:210px;"  readonly="readonly"></input>
                      <asp:Button ID="search" runat="server" Text="<%$ Resources:Resource, Search %>" CssClass="btn btn-primary btn-fill" OnClick="search_Click" /> 
                      <asp:Button ID="reset" runat="server" Text="<%$ Resources:Resource, Reset %>" CssClass="btn btn-primary btn-fill" OnClick="reset_Click"/> 
                  </div> 
                </br></br>
            </div>
            
            <div class="material-datatables">
                <asp:GridView ID="History" runat="server" CssClass="table table-striped table-no-bordered table-hover" AutoGenerateColumns="False" style="width:100%;cellspacing:0"  DataKeyNames="NoticeId">
                    <Columns>
                        <asp:BoundField HeaderText="<%$ Resources:Resource, NoticeId %>" DataField="NoticeId" ReadOnly="true"  />
                        <asp:BoundField HeaderText="<%$ Resources:Resource, Title %>" DataField="Title" ReadOnly="true"  />
                        <asp:BoundField HeaderText="<%$ Resources:Resource, Notice %>" DataField="Details" ReadOnly="true" />
                        <asp:BoundField HeaderText="<%$ Resources:Resource, Time %>" DataField="Timestamp" ReadOnly="true" />
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
<asp:Content ID="Content4" ContentPlaceHolderID="foot" Runat="Server">
     <script src="../../assets/vendors/daterangepicker/scripts/vendor.js"></script>
    <script src="../../assets/vendors/daterangepicker/scripts/daterangepicker.js"></script>
    <script>
        var income = $124('#body_History').DataTable({
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

