<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/ReaderPages/MasterPage.master" AutoEventWireup="true" CodeFile="bookMessage.aspx.cs" Inherits="Pages_bookMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style2 {
            width: 930px;
            height: 200px;
            text-align:center;
        }
        .auto-style3 {
            width: 618px;
            float: left;
        }
    </style>
        <link href="../../assets/vendors/dropzone/dropzone.min.css" rel="stylesheet" />
    <link href="../../assets/vendors/jquery-ui-1.12.0/jquery-ui.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div class="card">
        <div class="content" style="margin: 0px 100px">
              <fieldset style="text-align:center;">    
                <div class="form-group">
                   <div >
                       <asp:Image ID="Image1" runat="server" Width="200" Hight="180"/>
                      </div>
                    </div>
                    </fieldset>
             <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"><asp:Label ID="Label1" runat="server" Text="<%$ Resources:Resource,Title %>"></asp:Label></label>
	                   <div class="col-sm-11">
	                   <asp:Label ID="title" runat="server" Text="" Cssclass="form-control"></asp:Label>
                            </div>
	                 </div>
	              </fieldset>
             <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"><asp:Label ID="Label2" runat="server" Text="<%$ Resources:Resource,Author %>"></asp:Label></label>
	                   <div class="col-sm-11">
	                   <asp:Label ID="author" runat="server" Text="" Cssclass="form-control"></asp:Label>
                            </div>
	                 </div>
	              </fieldset>
                <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"><asp:Label ID="Label3" runat="server" Text="<%$ Resources:Resource,PublishDate %>"></asp:Label></label>
	                   <div class="col-sm-11">
                           <asp:Label ID="pubDate" runat="server" Text="" Cssclass="form-control"></asp:Label>
                            </div>
	                 </div>
	              </fieldset>
                  <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"><asp:Label ID="Label4" runat="server" Text="<%$ Resources:Resource,Price %>"></asp:Label></label>
	                   <div class="col-sm-11">
                           <asp:Label ID="price" runat="server" Text="" Cssclass="form-control"></asp:Label>
                            </div>
	                 </div>
	              </fieldset>
            <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"><asp:Label ID="LabelCategorytitle" runat="server" Text="<%$ Resources:Resource,Category %>"></asp:Label></label>
	                   <div class="col-sm-11">
                             <asp:Label ID="LabelCatogoryinfo" runat="server" Text="" Cssclass="form-control"></asp:Label>
                            </div>
	                 </div>
	              </fieldset>
                  <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"><asp:Label ID="Label5" runat="server" Text="ISBN13"></asp:Label></label>
	                   <div class="col-sm-11">
                             <asp:Label ID="isbn13" runat="server" Text="" Cssclass="form-control"></asp:Label>
                            </div>
	                 </div>
	              </fieldset>
                <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label">  <asp:Label ID="Label6" runat="server" Text="ISBN10"></asp:Label></label>
	                   <div class="col-sm-11">
                               <asp:Label ID="isbn10" runat="server" Text="" Cssclass="form-control"></asp:Label>
                            </div>
	                 </div>
	              </fieldset>
                      <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"> <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Resource,Pages %>"></asp:Label></label>
	                   <div class="col-sm-11">
                            <asp:Label ID="pages" runat="server" Text="" Cssclass="form-control"></asp:Label>
                            </div>
	                 </div>
	              </fieldset>
                   <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"><asp:Label ID="Label13" runat="server" Text="<%$ Resources:Resource,Publisher %>"></asp:Label></label>
	                   <div class="col-sm-11">
                             <asp:Label ID="publisher" runat="server" Text="" Cssclass="form-control"></asp:Label>
                            </div>
	                 </div>
	              </fieldset>
              <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"><asp:Label ID="Label7" runat="server" Text="<%$ Resources:Resource,Numberofcollections %>"></asp:Label></label>
	                   <div class="col-sm-11">
                             <asp:Label ID="numberofcollections" runat="server" Text="" Cssclass="form-control"></asp:Label>
                            </div>
	                 </div>
	              </fieldset>
                   <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"><asp:Label ID="Label8" runat="server" Text="<%$ Resources:Resource,Numberofborrowables %>"></asp:Label></label>
	                   <div class="col-sm-11">
                             <asp:Label ID="numberofborrowables" runat="server" Text="" Cssclass="form-control"></asp:Label>
                            </div>
	                 </div>
	              </fieldset>
            </div>
        </div>


    <%-- '<%#Eval("ImageURL") %>' --%>
    
    
    
    
    
    
    
    
    
    
<%--    <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="subtitle" runat="server" Text=""></asp:Label><br/>
    <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="origintitle" runat="server" Text=""></asp:Label>
    <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="binding" runat="server" Text=""></asp:Label>
    <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="translator" runat="server" Text=""></asp:Label>
    <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="catalog" runat="server" Text=""></asp:Label>--%>
<div class="card">
    <div class="content">
        <div class="material-datatables">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-no-bordered table-hover" style="width:100%;cellspacing:0">
                        <Columns>                                                        
                            <asp:BoundField DataField="BookBarcode" HeaderText="BookBarcode" HeaderStyle-CssClass="text-primary"/>
                            <asp:BoundField DataField="BookId" HeaderText="<%$ Resources:Resource,BookId %>" HeaderStyle-CssClass="text-primary"/>
                            <asp:BoundField DataField="Position" HeaderText="<%$ Resources:Resource,Position %>" HeaderStyle-CssClass="text-primary"/>
                            <asp:BoundField DataField="newStatus" HeaderText="<%$ Resources:Resource,Status %>" HeaderStyle-CssClass="text-primary"/>
                        <asp:TemplateField HeaderText="<%$ Resources:Resource, Reserve %>">
                       <ItemTemplate>
                          <asp:Button ID="ButtonReserve" runat="server" Text="<%$ Resources:Resource, Reserve %>"  OnClick="reserve_Click" Cssclass="btn btn-info" />                           
                       </ItemTemplate>
                   <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
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
            "searching": false,
            "lengthChange": false,
            "order": [[0, 'asc']],
            "bStateSave": true,
             columnDefs: [{
                'targets': [4],
                'orderable': false
            }]
        });
</script>
</asp:Content>

