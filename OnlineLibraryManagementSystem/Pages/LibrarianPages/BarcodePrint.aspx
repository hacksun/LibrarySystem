<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/LibrarianPages/MasterPage.master" AutoEventWireup="true" CodeFile="BarcodePrint.aspx.cs" Inherits="Pages_LibrarianPages_BarcodePrint" %>
<asp:Content ID="header" ContentPlaceHolderID="header" Runat="Server">
    <script>
        document.getElementById("book").className = "active";
    </script>
      <a><asp:Label runat="server" Text="<%$ Resources:Resource, Print_Barcode%>" CssClass="navbar-brand"></asp:Label> </a>
     <link href="../../assets/vendors/dropzone/dropzone.min.css" rel="stylesheet" />
    <link href="../../assets/vendors/jquery-ui-1.12.0/jquery-ui.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="body" runat="server" contentplaceholderid="body">
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
    <div style="text-align:center;">
    <input type=button value="<asp:Literal runat="server" Text="<%$ Resources:Resource, Print%>" />" onclick="doPrint()" Class="btn btn-fill btn-default">
    <asp:Button ID="Buttoncancel" runat="server" Text="<%$ Resources:Resource, Cancel %>" Onclick="Buttoncancel_Click"  CausesValidation="False" CssClass="btn btn-fill btn-default" />
    </div>
</asp:Content>
<asp:Content ID="foot" ContentPlaceHolderID="foot" runat="server">
    <script src="../../assets/vendors/jquery.select-bootstrap.js"></script>
    <script type="text/javascript">
        function doPrint() {
            //onreadystatechange = bind();
            bdhtml=window.document.body.innerHTML;    
            sprnstr="<!--startprint-->";    
            eprnstr="<!--endprint-->";    
            prnhtml=bdhtml.substr(bdhtml.indexOf(sprnstr)+17);    
            prnhtml=prnhtml.substring(0,prnhtml.indexOf(eprnstr));    
            window.document.body.innerHTML=prnhtml; 
            window.print();
        }
        window.onload = function () {


            doPrint();

          


        }
    </script>
 </asp:Content>