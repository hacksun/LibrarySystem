<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/LibrarianPages/MasterPage.master" AutoEventWireup="true" CodeFile="AddStacks.aspx.cs" Inherits="Pages_AddStacks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <script>
        document.getElementById("stack").className = "active";
    </script>
        <a> <asp:Label runat="server" Text="<%$ Resources:Resource, Add_Stacks %>" CssClass="navbar-brand"></asp:Label> </a>
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
                                        <div class="form-group">
                                            <asp:Button ID="AddButton" runat="server" Text="<%$ Resources:Resource, Add %>" OnClick="AddStacks"  CausesValidation="False" CssClass="btn btn-fill btn-default" />
                                         </div>
                                      <asp:Label ID="Label1" runat="server" Text="（StackId Example：A-101）" Cssclass="label-primary"></asp:Label>

                                    </div>
                            </div>
                        </div>
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