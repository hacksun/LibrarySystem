<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/LibrarianPages/MasterPage.master" AutoEventWireup="true" CodeFile="AddBooks.aspx.cs" Inherits="Pages_Addbooks_ISBN" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    <link href="../../assets/vendors/dropzone/dropzone.min.css" rel="stylesheet" />
    <link href="../../assets/vendors/jquery-ui-1.12.0/jquery-ui.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="header" ContentPlaceHolderID="header" runat="Server">
    <script>
        document.getElementById("book").className = "active";
    </script>
    <a>
        <asp:label runat="server" text="<%$ Resources:Resource, Add_Books %>" cssclass="navbar-brand"></asp:label>
    </a>
</asp:Content>

<asp:Content ID="body" runat="server" ContentPlaceHolderID="body">
    <style type="text/css">
        .style1 {
            width: 100%;
            text-align: center;
            font-family: Consolas;
            margin-top: 0px;
        }
    </style>

    <div class="card">
        <div class="content">
            <fieldset style="text-align: center;">
                <div class="form-group">
                    <div>
                        <asp:image id="Image1" imageurl="~/Images/default.jpg" runat="server" width="200" hight="400" />
                    </div>
                    <div>
                        &nbsp
                    </div>
                    <div>
                        <asp:button id="ButtonUpload" runat="server" text="<%$ Resources:Resource, Upload %>" onclick="ButtonUpload_Click" cssclass="btn btn-fill btn-default" />
                        <input type="button" value="<%$ Resources:Resource, Selectimage%>" runat="server" text="" onclick="fileupload.click()" class="btn btn-fill btn-default" />
                        <input type="file" id="fileupload" name="fileupload" style="display: none;" onchange="filepath.value=this.value" />
                        <label>
                            <input type="Text" id="filepath" name="filepath" value="" class="form-control" style="width: 400px;" onkeypress="return doClick(event);">
                        </label>
                    </div>
                </div>
            </fieldset>
            <fieldset>
                <asp:panel id="panSearch" runat="server" defaultbutton="ButtonSearch">  
	                <div class="form-group" >
                        <label class="col-sm-1 control-label">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Resource, ISBN %>"></asp:Label>
                        </label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="TextBoxISBN" runat="server" Cssclass="form-control"></asp:TextBox>
                        </div>
                        <asp:Button ID="ButtonSearch" runat="server" Text="<%$ Resources:Resource, Search %>" OnClick="ButtonSearch_Click" CssClass="btn btn-fill btn-default" />        
	                </div>
                </asp:panel>
            </fieldset>
            &nbsp
            <fieldset>
                <div class="form-group">
                    <label class="col-sm-1 control-label">
                        <asp:label id="Label5" runat="server" text="<%$ Resources:Resource, Title %>"></asp:label>
                    </label>
                    <div class="col-sm-10">
                        <asp:textbox id="TextBoxTitle" runat="server" cssclass="form-control" onkeypress="return doClick(event);"></asp:textbox>
                    </div>
                </div>
            </fieldset>
            &nbsp
                    <fieldset>
                        <div class="form-group">
                            <label class="col-sm-1 control-label">
                                <asp:label id="Label6" runat="server" text="<%$ Resources:Resource, Author %>"></asp:label>
                            </label>
                            <div class="col-sm-10">
                                <asp:textbox id="TextBoxAuthor" runat="server" cssclass="form-control" onkeypress="return doClick(event);"></asp:textbox>
                            </div>
                        </div>
                    </fieldset>
            &nbsp
            <fieldset>
                <div class="form-group">
                    <label class="col-sm-1 control-label">
                        <asp:label id="Label7" runat="server" text="<%$ Resources:Resource, Pubdate %>"></asp:label>
                    </label>
                    <div class="col-sm-10">
                        <asp:textbox id="TextBoxPubdate" runat="server" cssclass="form-control" onkeypress="return doClick(event);"></asp:textbox>
                    </div>
                </div>
            </fieldset>
            &nbsp
            <fieldset>
                <div class="form-group">
                    <label class="col-sm-1 control-label">
                        <asp:label id="Label8" runat="server" text="<%$ Resources:Resource, Price %>"></asp:label>
                    </label>
                    <div class="col-sm-10">
                        <asp:textbox id="TextBoxPrice" runat="server" cssclass="form-control" onkeypress="return doClick(event);"></asp:textbox>
                    </div>
                </div>
            </fieldset>
            &nbsp
            <fieldset>
                <div class="form-group">
                    <label class="col-sm-1 control-label">
                        <asp:label id="Label9" runat="server" text="<%$ Resources:Resource, ISBN13 %>"></asp:label>
                    </label>
                    <div class="col-sm-10">
                        <asp:textbox id="TextBoxISBN13" runat="server" cssclass="form-control" onkeypress="return doClick(event);"></asp:textbox>
                    </div>
                </div>
            </fieldset>
            &nbsp
            <fieldset>
                <div class="form-group">
                    <label class="col-sm-1 control-label">
                        <asp:label id="Label10" runat="server" text="<%$ Resources:Resource, ISBN10 %>"></asp:label>
                    </label>
                    <div class="col-sm-10">
                        <asp:textbox id="TextBoxISBN10" runat="server" cssclass="form-control" onkeypress="return doClick(event);"></asp:textbox>
                    </div>
                </div>
            </fieldset>
            &nbsp
            <fieldset>
                <div class="form-group">
                    <label class="col-sm-1 control-label">
                        <asp:label id="Label11" runat="server" text="<%$ Resources:Resource, Pages %>"></asp:label>
                    </label>
                    <div class="col-sm-10">
                        <asp:textbox id="TextBoxPages" runat="server" cssclass="form-control" onkeypress="return doClick(event);"></asp:textbox>
                    </div>
                </div>
            </fieldset>
            &nbsp
            <fieldset>
                <div class="form-group">
                    <label class="col-sm-1 control-label">
                        <asp:label id="Label12" runat="server" text="<%$ Resources:Resource, Publisher %>"></asp:label>
                    </label>
                    <div class="col-sm-10">
                        <asp:textbox id="TextBoxPublisher" runat="server" cssclass="form-control" onkeypress="return doClick(event);"></asp:textbox>
                    </div>
                </div>
            </fieldset>
            &nbsp
            <fieldset>
                <div class="form-group">
                    <label class="col-sm-1 control-label">
                        <asp:label id="Label3" runat="server" text="<%$ Resources:Resource, Position %>"></asp:label>
                    </label>
                    <div class="col-sm-2" style="max-height: 60px;">
                        <asp:dropdownlist id="DropDownList1" style="height: 60px; max-height: 60px;" runat="server" cssclass="selectpicker" data-style="btn btn-primary btn-round"></asp:dropdownlist>
                    </div>
                </div>
            </fieldset>
            &nbsp
            <fieldset>
                <div class="form-group">
                    <label class="col-sm-1 control-label">
                        <asp:label id="Label4" runat="server" text="<%$ Resources:Resource, Quantity %>"></asp:label>
                    </label>
                    <div class="col-sm-10">
                        <asp:textbox id="TextBoxQuantity" runat="server" cssclass="form-control" onkeypress="return doClick(event);"></asp:textbox>
                    </div>
                </div>
            </fieldset>
            &nbsp
            <fieldset style="text-align: center;">
                <asp:button id="AddButton" runat="server" text="<%$ Resources:Resource, Add %>" onclick="Addbooks" causesvalidation="False" cssclass="btn btn-fill btn-default" />
                <input type="button" value="<%$ Resources:Resource, Print%>" runat="server" text="" onclick="doPrint()" class="btn btn-fill btn-default" style="display:none">
            </fieldset>
            &nbsp
            <!--startprint-->
            <!--注意要加上html里star和end的这两个标记-->
            <asp:datalist id="DataListbookbarcode" runat="server" repeatcolumns="1" horizontalalign="center" enabled="false">
                <ItemTemplate>
                    <br />
                    <asp:image ID="Imagebarcode" runat="server"  ImageUrl='<%#"~/Images/Barcode/" +Eval("Name")%>'/>     
                    <br />     
                </ItemTemplate>
            </asp:datalist>
            <!--endprint-->
        </div>
    </div>
</asp:Content>

<asp:Content ID="foot" ContentPlaceHolderID="foot" runat="server">
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
        function doPrint() {
            //onreadystatechange = bind();
            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!--startprint-->";
            eprnstr = "<!--endprint-->";
            prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            window.document.body.innerHTML = prnhtml;
            window.print();
            var result ="<%=deletebind() %>";
        }
    </script>
</asp:Content>
