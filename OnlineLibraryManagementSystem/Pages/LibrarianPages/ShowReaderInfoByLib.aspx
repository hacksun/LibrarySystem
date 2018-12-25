<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/LibrarianPages/MasterPage.master" AutoEventWireup="true" CodeFile="ShowReaderInfoByLib.aspx.cs" Inherits="Pages_ShowReaderInfo" %>
<asp:Content ID="header" ContentPlaceHolderID="header" Runat="Server">
    <a> <asp:Label runat="server" Text="<%$ Resources:Resource, ReaderInfo %>" CssClass="navbar-brand"></asp:Label> </a>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" Runat="Server">
    <div class="card">
        <div class="content">
            <div class="form-group">
                <label><asp:Label ID="Label1" runat="server" Text="<%$ Resources:Resource, Telephone %>"></asp:Label></label>
                <asp:TextBox ID="TextBoxPhone" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label><asp:Label ID="Label2" runat="server" Text="<%$ Resources:Resource, Name %>"></asp:Label></label>
                <asp:TextBox ID="TextBoxName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label><asp:Label ID="Label3" runat="server" Text="<%$ Resources:Resource, IDNumber %>"></asp:Label></label>
                <asp:TextBox ID="TextBoxIDNumber" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label><asp:Label ID="Label4" runat="server" Text="<%$ Resources:Resource, Email %>"></asp:Label></label>
                <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label><asp:Label ID="Label5" runat="server" Text="<%$ Resources:Resource, Password %>"></asp:Label></label>
                <asp:TextBox ID="TextBoxPassword" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label><asp:Label ID="Label9" runat="server" Text="<%$ Resources:Resource, Fine %>"></asp:Label></label>
                <asp:TextBox ID="TextBoxFine" runat="server" CssClass="form-control" readonly="true"></asp:TextBox>
            </div>
            <asp:Button ID="SubmitButton" runat="server" Text="<%$ Resources:Resource, Submit %>" CssClass="btn btn-fill btn-default" OnClick="Submit" />
            <asp:Button ID="Button" runat="server" Text="<%$ Resources:Resource, Delete %>" CssClass="btn btn-fill btn-default" OnClick="Delete" />
        </div>
    </div>

       <div class="card">
                <div class="content">
                    <h3><asp:Label ID="LabelName" runat="server" Text="" CssClass="title"></asp:Label></h3>
                    <div class="nav-tabs-navigation">
                    <div class="nav-tabs-wrapper">
                        <ul class="nav nav-tabs" data-tabs="tabs">
                            <li class="active">
                                <a href="#issue" aria-controls="issue" role="tab" data-toggle="tab">
                                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Resource, IssueRecords %>" ></asp:Label>
                                </a>
                            <li>
                            <li class="">
                                <a href="#history" aria-controls="history" role="tab" data-toggle="tab">
                                    <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Resource, returnHistory %>" ></asp:Label>
                                </a>
                            <li>
                            <li class="">
                                <a href="#reversation" aria-controls="reversation" role="tab" data-toggle="tab">
                                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Resource, ReversationRecords %>" ></asp:Label>
                                </a>
                            <li>
                        </ul>
                    </div>
                    </div>
                    <div class="tab-content">
                        <div id="issue" class="tab-pane active" role="tabpanel">
                            <div class="table-responsive">
                                <asp:GridView cssClass="table table-hover" ID="GridView1" runat="server" GridLines="None">
                                    <HeaderStyle cssClass="text-primary"/>
                                </asp:GridView>
                             </div>
                        </div>
                        <div id="history" class="tab-pane" role="tabpanel">
                            <div class="table-responsive">
                                <asp:GridView cssClass="table table-hover" ID="GridView3" runat="server" GridLines="None">
                                    <HeaderStyle cssClass="text-primary"/>
                                </asp:GridView>
                             </div>
                        </div>
                        <div id="reversation" class="tab-pane" role="tabpanel">
                            <div class="table-responsive">
                                <asp:GridView cssClass="table table-hover" ID="GridView2" runat="server" GridLines="None">
                                    <HeaderStyle cssClass="text-primary"/>
                                </asp:GridView>
                             </div>
                        </div>
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
        var income = $124('#body_Category').DataTable({
            "searching": false,
            "lengthChange": false,
            "order": [[0, 'asc']],
            "bStateSave":true,
        });
        
</script>

</asp:Content>