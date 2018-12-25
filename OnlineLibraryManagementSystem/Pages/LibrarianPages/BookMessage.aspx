<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/LibrarianPages/MasterPage.master" AutoEventWireup="true" CodeFile="BookMessage.aspx.cs" Inherits="Pages_LibrarianPages_BookMessage" %>

<asp:Content ID="header" ContentPlaceHolderID="header" Runat="Server">
    <script>
        document.getElementById("book").className = "active";
    </script>
    <link href="../../assets/vendors/dropzone/dropzone.min.css" rel="stylesheet" />
    <link href="../../assets/vendors/jquery-ui-1.12.0/jquery-ui.css" rel="stylesheet" />
     <a><asp:Label runat="server" Text="<%$ Resources:Resource, BookInfo %>" CssClass="navbar-brand"></asp:Label> </a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div class="card">
        <div class="content">
         <fieldset style="text-align:center;">    
                <div class="form-group">
                   <div >
                      <asp:Image ID="Image1" runat="server" Width="300" Hight="240"/>
                      </div>
                    <div>
                        &nbsp
                        </div>
                           <div> 
                              <asp:Button ID="ButtonUpload" runat="server" Text="<%$ Resources:Resource,Upload %>" OnClick="Upload_Click" CssClass="btn btn-fill btn-default"/>                         
                              <input type=button value="<asp:Literal runat="server" Text="<%$ Resources:Resource, Selectimage%>" />" onclick=fileupload.click() Class="btn btn-fill btn-default">
                              <input type="file" id="fileupload" name="fileupload"  style="display: none;" onchange="filepath.value=this.value"/>                            
                              <label><input type="Text" id="filepath" name="filepath" value="" class="form-control" onkeypress="return doClick(event);"></label>
                          </div>
                    </div>                                        
                </fieldset>
         <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"><asp:Label ID="Label1" runat="server" Text="<%$ Resources:Resource, Title %>"></asp:Label></label>
	                   <div class="col-sm-10">
                           <asp:TextBox ID="TextBoxtitle" runat="server" Cssclass="form-control" onkeypress="return doClick(event);"></asp:TextBox>
	                    </div>
	                 </div>
	              </fieldset>
            &nbsp
         <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"><asp:Label ID="Label2" runat="server" Text="<%$ Resources:Resource, Author %>"></asp:Label></label>
	                   <div class="col-sm-10">
                           <asp:TextBox ID="TextBoxauthor" runat="server" Cssclass="form-control" onkeypress="return doClick(event);"></asp:TextBox>
	                    </div>
	                 </div>
	              </fieldset>
            &nbsp
         <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"><asp:Label ID="Label3" runat="server" Text="<%$ Resources:Resource, Pubdate %>"></asp:Label></label>
	                   <div class="col-sm-10">
                        <asp:TextBox ID="TextBoxpubdate" runat="server" Cssclass="form-control" onkeypress="return doClick(event);"></asp:TextBox>
	                    </div>
	                 </div>
	              </fieldset>
            &nbsp
         <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"><asp:Label ID="Label4" runat="server" Text="<%$ Resources:Resource, Price %>"></asp:Label></label>
	                   <div class="col-sm-10">
                           <asp:TextBox ID="TextBoxprice" runat="server" Cssclass="form-control" onkeypress="return doClick(event);"></asp:TextBox>
	                    </div>
	                 </div>
	              </fieldset>
            &nbsp
         <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"><asp:Label ID="Label5" runat="server" Text="<%$ Resources:Resource, ISBN13 %>"></asp:Label></label>
	                   <div class="col-sm-10">
                           <asp:TextBox ID="TextBoxisbn13" runat="server" Cssclass="form-control" onkeypress="return doClick(event);"></asp:TextBox>
	                    </div>
	                 </div>
	              </fieldset>
            &nbsp
         <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"><asp:Label ID="Label6" runat="server" Text="<%$ Resources:Resource, ISBN10 %>"></asp:Label></label>
	                   <div class="col-sm-10">
                           <asp:TextBox ID="TextBoxisbn10" runat="server" Cssclass="form-control" onkeypress="return doClick(event);"></asp:TextBox>
	                    </div>
	                 </div>
	              </fieldset>
            &nbsp
         <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"><asp:Label ID="Label12" runat="server" Text="<%$ Resources:Resource, Pages %>"></asp:Label></label>
	                   <div class="col-sm-10">
                            <asp:TextBox ID="TextBoxpages" runat="server" Cssclass="form-control" onkeypress="return doClick(event);"></asp:TextBox>
	                    </div>
	                 </div>
	              </fieldset>
            &nbsp
         <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"> <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Resource, Publisher %>"></asp:Label></label>
	                   <div class="col-sm-10">
	                    <asp:TextBox ID="TextBoxpublisher" runat="server" Cssclass="form-control" onkeypress="return doClick(event);"></asp:TextBox>
                       </div>
	                 </div>
	              </fieldset>
                         &nbsp
                     <fieldset>
	                <div class="form-group">
	                   <label class="col-sm-1 control-label"> <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Resource, Category %>"></asp:Label></label>
	                   <div class="col-sm-10">
	                    <asp:TextBox ID="TextBoxCategory" runat="server" Cssclass="form-control" onkeypress="return doClick(event);" ReadOnly="True"></asp:TextBox>
                       </div>
	                 </div>
	              </fieldset>
            &nbsp
            <fieldset style="text-align:center;">
            <asp:Button ID="Alter" runat="server" Text="<%$ Resources:Resource,Alter %>" OnClick="Alter_Click" CssClass="btn btn-fill btn-default"/>        
            </fieldset>
        </div>
    </div>
    <div class="card">
   <div class="content">
       <div class="material-datatables">
        <asp:GridView ID="Category" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-no-bordered table-hover" OnRowEditing="Category_RowEditing"  OnRowUpdating="Category_RowUpdating" style="width:100%;cellspacing:0" OnRowCancelingEdit="Category_RowCancelingEdit" OnRowDeleting="Category_RowDeleting" OnPageIndexChanging="Category_PageIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="<%$ Resources:Resource, CategoryId %>" HeaderStyle-CssClass="text-primary">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("CategoryId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:Resource, CategoryName %>" HeaderStyle-CssClass="text-primary">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtName" runat="server" Text='<%# Eval("Name") %>' onkeypress="doClick(event)"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="true"  CausesValidation="false" />
                <asp:CommandField ShowDeleteButton="true" />
            </Columns>
        </asp:gridview>
    <div class="material-datatables">
        <asp:GridView ID="gvBookBarcodeResult" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-no-bordered table-hover" OnRowEditing="gvBookBarcodeResult_RowEditing"  OnRowUpdating="gvBookBarcodeResult_RowUpdating" style="width:100%;cellspacing:0" OnRowCancelingEdit="gvBookBarcodeResult_RowCancelingEdit" OnRowDeleting="gvBookBarcodeResult_RowDeleting" OnPageIndexChanging="gvBookBarcodeResult_PageIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="<%$ Resources:Resource, Barcode %>" HeaderStyle-CssClass="text-primary">
                    <ItemTemplate>
                        <asp:Label ID="BookBarcode" runat="server" Text='<%# Eval("BookBarcode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>              
                <asp:TemplateField HeaderText="<%$ Resources:Resource, BookId %>" HeaderStyle-CssClass="text-primary">
                    <ItemTemplate>
                        <asp:Label ID="BookId" runat="server" Text='<%# Eval("BookId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:Resource, Position %>" HeaderStyle-CssClass="text-primary">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlShelfId" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="ShelfId" runat="server" Text='<%# Eval("Position") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:Resource, Status %>" HeaderStyle-CssClass="text-primary">
                    <ItemTemplate>
                        <asp:Label ID="Status" runat="server" Text='<%# Eval("newStatus") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="<%$ Resources:Resource, Barcode_Timestamp %>" HeaderStyle-CssClass="text-primary">
                    <ItemTemplate>
                        <asp:Label ID="timestamp" runat="server" Text='<%# Eval("Timestamp") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:Resource, Display_Barcode %>">
                       <ItemTemplate>
                          <asp:Button ID="ButtonPrint_Barcode" runat="server" Text="<%$ Resources:Resource, Display %>" CommandArgument='<%# Eval("BookBarcode") %>' CommandName="getBookBarcode" OnClick="ButtonPrint_Barcode_Click" Cssclass="btn btn-fill btn-default" />                           
                       </ItemTemplate>
                   <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:Resource, Print_Barcode %>">
                     <ItemTemplate>
                    <asp:Button ID="ButtonPrint" runat="server" Text="<%$ Resources:Resource, Print %>"  OnClick="ButtonPrint_Click" Cssclass="btn btn-fill btn-default" />                    
                     </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                 </asp:TemplateField>
                <asp:CommandField ShowEditButton="true"  />
                <asp:CommandField ShowDeleteButton="true" />
            </Columns>
        </asp:GridView>
        &nbsp
<!--startprint--><!--注意要加上html里star和end的这两个标记-->
       <asp:DataList ID="DataListbookbarcode" runat="server" RepeatColumns="1" HorizontalAlign="center" Enabled="false">
           <ItemTemplate>
               <br>
               </br>
                <asp:image ID="Imagebarcode" runat="server"  ImageUrl='<%#"~/Images/Barcode/" +Eval("Name")%>'/>     
               <br>     
               </br>       
            </ItemTemplate>
        </asp:DataList>
<!--endprint-->
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
     <script src="../../assets/vendors/jquery.select-bootstrap.js"></script>
    <script>
        var income = $124('#body_gvBookBarcodeResult').DataTable({
            "searching": false,
            "lengthChange": false,
            "order": [[0, 'asc']],
            "bStateSave": true,
            columnDefs: [{
                'targets': [5,6,7,8],
                'orderable': false
            }]
        });

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
            
            bdhtml=window.document.body.innerHTML;    
            sprnstr="<!--startprint-->";    
            eprnstr="<!--endprint-->";    
            prnhtml=bdhtml.substr(bdhtml.indexOf(sprnstr)+17);    
            prnhtml=prnhtml.substring(0,prnhtml.indexOf(eprnstr));    
            window.document.body.innerHTML=prnhtml; 
            window.print();
            var result="<%=deletebind() %>";
           
        }
</script>
</asp:Content>