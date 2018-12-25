<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/LibrarianPages/MasterPage.master" AutoEventWireup="true" CodeFile="EditShelf.aspx.cs" Inherits="Pages_LibrarianPages_EditShelf" %>
<asp:Content ID="head" ContentPlaceHolderID="header" Runat="Server">
    <script>
        document.getElementById("stack").className = "active";
    </script>
    <a> <asp:Label runat="server" Text="<%$ Resources:Resource, ShelfInfo %>" CssClass="navbar-brand"></asp:Label> </a>

     <link href="../../assets/vendors/dropzone/dropzone.min.css" rel="stylesheet" />
    <link href="../../assets/vendors/jquery-ui-1.12.0/jquery-ui.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="body" runat="server" ContentPlaceHolderId="body">
    <div class="content">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card">
                                    <div class="content">
	                                    <fieldset>
	                                        <div class="form-group">
	                                            <label class="col-sm-1 control-label"><asp:Label ID="Label6" runat="server" Text="<%$ Resources:Resource,ShelfId %>"></asp:Label>
	                                            </label>
                                                <div class="col-sm-10">
                                                    <asp:Label ID="LabelShelfId" runat="server" Text="ShelfId" Cssclass="form-control" onkeypress="return doClick(event);"></asp:Label>
	                                            </div>
	                                        </div>
	                                    </fieldset>
                                        &nbsp
                                        <fieldset>
	                                        <div class="form-group">
	                                            <label class="col-sm-1 control-label"><asp:Label ID="Label10" runat="server" Text="<%$ Resources:Resource,StackId %>"></asp:Label>
	                                            </label>
                                                <div class="col-sm-2">
                                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="selectpicker" data-style="btn btn-primary btn-round">
                                                </asp:DropDownList>
	                                            </div>
	                                        </div>
	                                    </fieldset>
                                        &nbsp
                                         <fieldset>
	                                        <div class="form-group">
	                                            <label class="col-sm-1 control-label"><asp:Label ID="Label8" runat="server" Text="<%$ Resources:Resource, Stack_Summary %>"></asp:Label></asp:Label></label>
	                                            <div class="col-sm-10">
                                                     <asp:TextBox ID="TextBoxSummary" runat="server" Cssclass="form-control" onkeypress="return doClick(event);"></asp:TextBox>
	                                            </div>
	                                        </div>
	                                    </fieldset>
                                        &nbsp
                                         <fieldset>
	                                        <div class="form-group">
	                                            <label Class="col-sm-1 control-label"><asp:Label ID="Label9" runat="server" Text="<%$ Resources:Resource, Shelf_Timestamp %>"></asp:Label></label>
	                                            <div class="col-sm-10">
	                                                <asp:Label ID="LabelShelf_Timestamp" runat="server" Text="Shelf_Timestamp" Cssclass="form-control" onkeypress="return doClick(event);"></asp:Label>
	                                            </div>
	                                        </div>
	                                    </fieldset>
                                        &nbsp
                                        <div class="form-group">
                                            <asp:Button ID="AlterButton" runat="server" Text="<%$ Resources:Resource, Alter %>" OnClick="Alter_ShelfInfo" CausesValidation="False" CssClass="btn btn-fill btn-default" />
                                        <asp:Button ID="Button2" runat="server" Text="<%$ Resources:Resource, Cancel %>" OnClick="Cancel" CausesValidation="False" CssClass="btn btn-fill btn-default" />
                                            </div>
                                    </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
</asp:Content>
<asp:Content ID="foot" runat="server" ContentPlaceHolderID="foot">
    <script src="../../Scripts/art-Template/template-web.js"></script>
    <script src="../../assets/vendors/dropzone/dropzone.min.js"></script>
    <script src="../../assets/vendors/jquery.select-bootstrap.js"></script>
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