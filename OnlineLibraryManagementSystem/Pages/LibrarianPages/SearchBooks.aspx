<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/LibrarianPages/MasterPage.master" AutoEventWireup="true" CodeFile="SearchBooks.aspx.cs" Inherits="Pages_LibrarianPages_SearchBooks" %>
<asp:Content ID="head" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .resultImg img{
            max-height:84px;
            max-width:60px;
        }
    </style>
    <script src="../../Scripts/art-Template/template-web.js"></script>
    <link href="../../assets/vendors/dropzone/dropzone.min.css" rel="stylesheet" />
    <link href="../../assets/vendors/jquery-ui-1.12.0/jquery-ui.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="header" ContentPlaceHolderID="header" Runat="Server">
    <script>
        document.getElementById("book").className = "active";
    </script>
      <a><asp:Label runat="server" Text="<%$ Resources:Resource, search %>" CssClass="navbar-brand"></asp:Label> </a>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" Runat="Server">
    <div style="padding-top:50px">
        <table style="width: 100%;">
            <tr>
                <td style="width:40%; text-align:right">
                    <div class="col-md-4 col-md-offset-8">
                    <asp:DropDownList ID="ddlField" runat="server" CssClass="selectpicker" data-style="btn btn-primary btn-round">
                        <asp:ListItem Text="<%$ Resources:Resource,Title %>" Value="Title"></asp:ListItem>
                        <asp:ListItem Text="<%$ Resources:Resource,Author %>" Value="Author"></asp:ListItem>
                        <asp:ListItem Text="ISBN" Value="ISBN"></asp:ListItem>
                        <asp:ListItem Text="ISSN" Value="ISSN" Enabled="false"></asp:ListItem>
                    </asp:DropDownList>
                        </div>
                </td>
                <td style="width:20%">
                    <asp:TextBox ID="tbSearch" runat="server" Width="380px" CssClass="form-control" onkeypress="return doClick(event);"></asp:TextBox>
                </td>
                <td style="width:70%;text-align:left;">
                    <div class="col-md-5">
                    <asp:DropDownList ID="ddlClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" CssClass="selectpicker" data-style="btn btn-primary btn-round">
                        <asp:ListItem Text="<%$ Resources:Resource,Book %>" Value="Books" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="<%$ Resources:Resource,Periodical %>" Value="Periodicals"></asp:ListItem>
                    </asp:DropDownList>
                         </div>
                    <asp:Button ID="brSearch" runat="server" Text="<%$ Resources:Resource,Search %>" OnClick="brSearch_Click" CssClass="btn btn-default btn-fill" />
                    <asp:Button ID="brDelete" runat="server" Text="<%$ Resources:Resource,Delete %>" OnClick="brDelete_Click" CssClass="btn btn-default btn-fill" />
                </td>
            </tr>
            <tr>
                <td style="text-align:center; height:30px" colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <div class="table-responsive">
                <td style="text-align:center" colspan="3">
                    <asp:GridView ID="gvBookResult" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" Enabled="False" AllowSorting="True" OnSorting="gvBookResult_Sorting" OnPageIndexChanging="gvBookResult_PageIndexChanging" CssClass="table table-no-bordered" >
                        <Columns>
                            <asp:ImageField HeaderText="<%$ Resources:Resource, Cover %>" DataImageUrlField="ImageURL" ReadOnly="True">
                                <ItemStyle Height="84px" HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" CssClass="resultImg" />
                            </asp:ImageField>
                            <asp:HyperLinkField HeaderText="<%$ Resources:Resource, BookTitle %>" SortExpression="Title" DataNavigateUrlFields="BookId" DataNavigateUrlFormatString="~/Pages/LibrarianPages/BookMessage.aspx?book_id={0}" DataTextField="Title">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:HyperLinkField>
                            <asp:BoundField HeaderText="<%$ Resources:Resource, Author %>" SortExpression="Author" DataField="Author" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Publisher" HeaderText="<%$ Resources:Resource, Publisher %>" SortExpression="Publisher" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="<%$ Resources:Resource, Select %>">
                                <ItemTemplate>
                                <asp:CheckBox id="CheckBoxDeleteBook" runat="Server" style="zoom:200%;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:GridView ID="gvPeriodicalResult" runat="server" HorizontalAlign="Center"  AutoGenerateColumns="False"  Enabled="False" AllowSorting="True" OnSorting="gvBookResult_Sorting" OnPageIndexChanging="gvBookResult_PageIndexChanging" CssClass="table table-no-bordered">
                        <Columns>
                             <asp:ImageField HeaderText="<%$ Resources:Resource, Cover %>" DataImageUrlField="ImageURL" ReadOnly="True">
                                <ItemStyle Height="84px" HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" CssClass="resultImg" />
                            </asp:ImageField>
                             <asp:BoundField HeaderText="<%$ Resources:Resource, Title %>"  DataField="Title" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$ Resources:Resource, ISSN %>" SortExpression="Title" DataField="ISSN" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$ Resources:Resource, Country %>"  DataField="Country" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$ Resources:Resource, Type %>"  DataField="NewType" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$ Resources:Resource, Price %>" SortExpression="Price" DataField="Price" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="<%$ Resources:Resource, Select %>">
                                <ItemTemplate>
                                <asp:CheckBox id="CheckBoxDeletePeriodical" runat="Server" style="zoom:200%;" />
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
