<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/LibrarianPages/MasterPage.master" AutoEventWireup="true" CodeFile="Search_Stacks_Shelves.aspx.cs" Inherits="Pages_Search_Stacks_Shelves" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../assets/vendors/dropzone/dropzone.min.css" rel="stylesheet" />
    <link href="../../assets/vendors/jquery-ui-1.12.0/jquery-ui.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="header" runat="Server">
    <script>
        document.getElementById("stack").className = "active";
    </script>
    <a> <asp:Label runat="server" Text="<%$ Resources:Resource, search %>" CssClass="navbar-brand"></asp:Label> </a>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="body">
    <style type="text/css">
        .style1 {
            width: 100%;
            font-family: Consolas;
        }
        .auto-style1 {
            padding-top:50px;
            text-align:center;
        }
        .auto-style2 {
            width: 30%;
            height: 35px;
            text-align: right;
            font-size: x-large;
        }

        .auto-style4 {
            width: 40%;
            height: 35px;
            text-align:left;
            font-size: x-large;
        }
        .auto-style5 {
            width:30%;
            height:35px;
            text-align:left;
            font-size:x-large;
        }
        </style>
    <div class="auto-style1" >
        <table class="style1">
            <tr>
                <td class="auto-style2">
                    <div class="col-md-4 col-md-offset-8">
                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="selectpicker" data-style="btn btn-primary btn-round">
                        <asp:ListItem Text="<%$ Resources:Resource,StackId %>" Value="Stacks"></asp:ListItem>
                        <asp:ListItem Text="<%$ Resources:Resource,ShelfId %>" Value="Shelves"></asp:ListItem>
                    </asp:DropDownList>
                    </div>
                </td>
                <td class="auto-style4">
                    <asp:TextBox ID="TextBoxID" runat="server" Width="450px" CssClass="form-control" onkeypress="return doClick(event);"> </asp:TextBox></td>
                <td class="auto-style5">
                    <asp:Button ID="SearchButton" runat="server" Text="<%$ Resources:Resource,Search %>" OnClick="Search" Cssclass="btn btn-default btn-fill"/>
                    <asp:Button ID="Button1" runat="server" Text="<%$ Resources:Resource, Delete %>" OnClick="Delete"  CausesValidation="False" Cssclass="btn btn-default btn-fill"/>
                </td>
            </tr>
            <tr>
                <td style="text-align:center; height:30px" colspan="3"> &nbsp;</td>
            </tr>
            <tr>
                <div class="table-responsive">
                <td style="text-align:center" colspan="3">
                    <asp:GridView ID="gvStacksResult" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" Enabled="False" AllowSorting="True" OnSorting="gvStacksResult_Sorting" OnPageIndexChanging="gvStacksResult_PageIndexChanging"  CssClass="table table-no-bordered" style="width:65%">
                        <Columns>
                            <asp:HyperLinkField HeaderText="<%$ Resources:Resource, StackId %>" SortExpression="StackId" DataNavigateUrlFields="StackId" DataNavigateUrlFormatString="~/Pages/LibrarianPages/StackInfo.aspx?StackId={0}" DataTextField="StackId">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:HyperLinkField>
                            <asp:BoundField HeaderText="<%$ Resources:Resource, Position %>" SortExpression="Position" DataField="Position" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Summary" HeaderText="<%$ Resources:Resource, Stack_Summary %>" SortExpression="Summary" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="<%$ Resources:Resource, Select %>">
                                <ItemTemplate>
                                <asp:CheckBox id="CheckBoxDeleteStack" runat="Server" style="zoom:200%;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:GridView ID="gvShelvesResult" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" Enabled="False" AllowSorting="True" OnSorting="gvShelvesResult_Sorting" OnPageIndexChanging="gvShelvesResult_PageIndexChanging" CssClass="table table-no-bordered" style="width:65%">
                        <Columns>
                            <asp:HyperLinkField HeaderText="<%$ Resources:Resource, ShelfId %>" SortExpression="ShelfId" DataNavigateUrlFields="ShelfId" DataNavigateUrlFormatString="~/Pages/LibrarianPages/ShelfInfo.aspx?ShelfId={0}" DataTextField="ShelfId">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:HyperLinkField>
                            <asp:BoundField HeaderText="<%$ Resources:Resource, StackId %>" SortExpression="StackId" DataField="StackId" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Summary" HeaderText="<%$ Resources:Resource, Shelf_Summary %>" SortExpression="Summary" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="<%$ Resources:Resource, Select %>">
                                <ItemTemplate>
                                <asp:CheckBox id="CheckBoxDeleteShelf" runat="Server" style="zoom:200%;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
                </div>
            </tr>
        </table>
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