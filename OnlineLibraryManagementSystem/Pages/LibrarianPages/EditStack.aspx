<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/LibrarianPages/MasterPage.master" AutoEventWireup="true" CodeFile="EditStack.aspx.cs" Inherits="Pages_LibrarianPages_EditStack" %>

<asp:Content ID="header" ContentPlaceHolderID="header" Runat="Server">
    <script>
        document.getElementById("stack").className = "active";
    </script>
    <a> <asp:Label runat="server" Text="<%$ Resources:Resource, StackInfo %>" CssClass="navbar-brand"></asp:Label> </a>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="body">
        <div class="content">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card">
                                    <div class="content">
	                                    <fieldset>
	                                        <div class="form-group">
	                                            <label class="col-sm-1 control-label"><asp:Label ID="Label7" runat="server" Text="<%$ Resources:Resource,StackId %>"></asp:Label>
	                                            </label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="TextBoxStackId" runat="server" CssClass="form-control" onkeypress="return doClick(event);"></asp:TextBox>
	                                            </div>
	                                        </div>
	                                    </fieldset>
                                        &nbsp
	                                    <fieldset>
	                                        <div class="form-group">
	                                            <label class="col-sm-1 control-label"><asp:Label ID="Label8" runat="server" Text="<%$ Resources:Resource,Position %>"></asp:Label></label>
	                                            <div class="col-sm-10">
                                                    <asp:TextBox ID="TextBoxPosition" runat="server" Cssclass="form-control" onkeypress="return doClick(event);"></asp:TextBox>
	                                            </div>
	                                        </div>
	                                    </fieldset>
                                        &nbsp
                                         <fieldset>
	                                        <div class="form-group">
	                                            <label class="col-sm-1 control-label"><asp:Label ID="Label9" runat="server" Text="<%$ Resources:Resource, Stack_Summary %>"></asp:Label></asp:Label></label>
	                                            <div class="col-sm-10">
                                                    <asp:TextBox ID="TextBoxSummary" runat="server"  Cssclass="form-control" onkeypress="return doClick(event);"></asp:TextBox>
	                                            </div>
	                                        </div>
	                                    </fieldset>
                                        &nbsp
                                         <fieldset>
	                                        <div class="form-group">
	                                            <label Class="col-sm-1 control-label"><asp:Label ID="Label10" runat="server" Text="<%$ Resources:Resource, Stack_Timestamp %>"></asp:Label></label>
	                                            <div class="col-sm-10">
	                                                <asp:Label ID="LabelStack_Timestamp" runat="server" Text="LabelStack_Timestamp" Cssclass="form-control" onkeypress="return doClick(event);"></asp:Label>
	                                            </div>
	                                        </div>
	                                    </fieldset>
                                        &nbsp
                                        <div class="form-group">
                                                        <asp:Button ID="AlterButton" runat="server" Text="<%$ Resources:Resource, Alter %>" OnClick="Alter_StackInfo" CausesValidation="False" CssClass="btn btn-fill btn-default" />
                                        <asp:Button ID="Button1" runat="server" Text="<%$ Resources:Resource, Cancel %>" OnClick="Cancel" CausesValidation="False" CssClass="btn btn-fill btn-default" />
                                         </div>
                                      <asp:Label ID="Label6" runat="server" Text="（StackId Example：A-101）" Cssclass="label-primary"></asp:Label>

                                    </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>   
     <div class="card">
   <div class="content">
    <div class="material-datatables">
        <asp:GridView ID="Shelves" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-no-bordered table-hover" OnRowEditing="Shelves_RowEditing"  OnRowUpdating="Shelves_RowUpdating" style="width:100%;cellspacing:0" OnRowCancelingEdit="Shelves_RowCancelingEdit" OnPageIndexChanging="Shelves_PageIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="<%$ Resources:Resource, ShelfId %>" HeaderStyle-CssClass="text-primary">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("ShelfId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:Resource, Shelf_Summary %>" HeaderStyle-CssClass="text-primary">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSummary" runat="server" Text='<%# Eval("Summary") %>' onkeypress="return doClick(event);"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSummary" runat="server" Text='<%# Eval("Summary") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:Resource, Shelf_Timestamp %>" HeaderStyle-CssClass="text-primary">
                    <ItemTemplate>
                        <asp:Label ID="lblTimestamp" runat="server" Text='<%# Eval("Timestamp") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="true"  />
            </Columns>
        </asp:GridView>
        </div>
      </div>
    </div>      
</asp:Content>
<asp:Content ID="foot" runat="server" ContentPlaceHolderID="foot">
    <script type="text/javascript">
        function doClick(event) {
            //    if ($.trim($('#' + buttonId + '').val()) == '') {
            //        shorError();
            //        return;
            //    }
            var key;
 
            if (window.event)
                key = window.event.keyCode;     //IE
            else
                key = event.which;     //firefox
 
            if (key == 13) {
                //                if ($.trim($('#btnVerificationCode').val()) == '') {
                //                    shorError();
                //                }
                try {
                    if (window.event) {//ie
                        window.event.keyCode = 0
                        window.event.returnValue = false;
                    }
                    else {//firefox
                        return false;
                    }
                }
                catch (ex) {
                }
            }
        }
    </script>
</asp:Content>