<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/LibrarianPages/MasterPage.master" AutoEventWireup="true" CodeFile="AddPeriodicals.aspx.cs" Inherits="Pages_LibrarianPages_AddPeriodicals" %>

<asp:Content ID="header" ContentPlaceHolderID="header" Runat="Server">
    <script>
        document.getElementById("book").className = "active";
    </script>
      <a><asp:Label runat="server" Text="<%$ Resources:Resource, AddPeriodical %>" CssClass="navbar-brand"></asp:Label> </a>
    <script src="../../Scripts/art-Template/template-web.js"></script>
    <link href="../../assets/vendors/dropzone/dropzone.min.css" rel="stylesheet" />
    <link href="../../assets/vendors/jquery-ui-1.12.0/jquery-ui.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" Runat="Server">
    <div class="card">
        <div class="content">
             <fieldset style="text-align:center;">    
                <div class="form-group">
                                                <div >
                                                <asp:Image ID="imCover" ImageUrl="~/Images/default.jpg" runat="server" Width="200" Hight="400"/>
                                                </div>
                    <div>
                        &nbsp
                        </div>
                                                <div>   
                                                    <asp:Button ID="ButtonUpload" runat="server" Text="<%$ Resources:Resource, Upload %>" OnClick="ButtonUpload_Click" CssClass="btn btn-fill btn-default"/>                      
                                                    <input type=button value="<asp:Literal runat="server" Text="<%$ Resources:Resource, Selectimage%>" />" onclick=fileupload.click() Class="btn btn-fill btn-default">
                                                        <input type="file" id="fileupload" name="fileupload"  style="display: none;" onchange="filepath.value=this.value"/>                            
                                                        <label><input type="Text" id="filepath" name="filepath" value="" class="form-control" onkeypress="return doClick(event);"></label>
                                                </div>
                    </div>                                        
                </fieldset>
             <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"><asp:label ID="lbTitle" runat="server" text="<%$ Resources:Resource,Title %>"></asp:label></label>
	                   <div class="col-sm-10">
                          <asp:textbox ID="tbTitle" runat="server" Cssclass="form-control" onkeypress="return doClick(event);"></asp:textbox>
	                    </div>
	                 </div>
	              </fieldset>
            &nbsp
             <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"><asp:label ID="lbCountry" runat="server" text="<%$ Resources:Resource,Country %>"></asp:label></label>
	                   <div class="col-sm-10">
                            <asp:textbox ID="tbCountry" runat="server" Cssclass="form-control" onkeypress="return doClick(event);"></asp:textbox>
	                    </div>
	                 </div>
	              </fieldset>
            &nbsp
             <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"><asp:label ID="lbType" runat="server" text="<%$ Resources:Resource,Type %>"></asp:label></label>
	                   <div class="col-sm-2">
                            <asp:dropdownlist ID="ddlType" runat="server" CssClass="selectpicker" data-style="btn btn-primary btn-round">
                                <asp:ListItem Text="<%$ Resources:Resource,Magazine %>" Value="0">Magazine</asp:ListItem>
                                <asp:ListItem Text="<%$ Resources:Resource,Newspaper %>" Value="1">Newspaper</asp:ListItem>
                            </asp:dropdownlist>
	                    </div>
	                 </div>
	              </fieldset>
            &nbsp
             <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"><asp:label ID="lbISSN" runat="server" text="ISSN"></asp:label></label>
	                   <div class="col-sm-10">
                            <asp:textbox ID="tbISSN" runat="server" Cssclass="form-control" onkeypress="return doClick(event);"></asp:textbox>
	                    </div>
	                 </div>
	              </fieldset>
            &nbsp
             <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"><asp:label ID="lbPrice" runat="server" text="<%$ Resources:Resource,Price %>" ></asp:label></label>
	                   <div class="col-sm-10">
                           <asp:textbox ID="tbPrice" runat="server" TextMode="Number" Cssclass="form-control" onkeypress="return doClick(event);"></asp:textbox>
	                    </div>
	                 </div>
	              </fieldset>
            &nbsp
             <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"> <asp:label ID="lbShelf" runat="server" text="<%$ Resources:Resource,Shelf %>"></asp:label></label>
	                   <div class="col-sm-2">
                          <asp:dropdownlist ID="ddlShelf" runat="server" CssClass="selectpicker" data-style="btn btn-primary btn-round"></asp:dropdownlist>
	                    </div>
	                 </div>
	              </fieldset>
            &nbsp
             <fieldset style="text-align:center;">
                    <asp:Button ID="btSubmit" runat="server" Text="<%$ Resources:Resource,Submit %>" OnClick="btSubmit_Click" CssClass="btn btn-fill btn-default"/>
                    </fieldset>
                                </div>
                           </div>  
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
<asp:Content ID="foot" runat="server" ContentPlaceHolderID="foot">
    <script src="../../Scripts/art-Template/template-web.js"></script>
    <script src="../../assets/vendors/dropzone/dropzone.min.js"></script>
        <script src="../../assets/vendors/jquery.select-bootstrap.js"></script>
</asp:Content>
