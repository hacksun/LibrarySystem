<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/ReaderPages/MasterPage.master" AutoEventWireup="true" CodeFile="SearchDemo.aspx.cs" Inherits="Pages_SearchDemo" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="Server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scle=1.0" />

    <link rel="stylesheet" href="../../assets/vendors/dropzone/dropzone.min.css" />
    <link rel="stylesheet" href="../../assets/vendors/jquery-ui-1.12.0/jquery-ui.css" />
    <link rel="stylesheet" href="../../assets/slider/css/edslider.css" />
    <link rel="stylesheet" href="../../assets/slider/css/styles.css" />
    <link rel="stylesheet" href="../../assets/slider/css/animate-custom.css" />
    <link rel="stylesheet" type="text/css" href="http://fonts.googleapis.com/css?family=Open+Sans+Condensed:300,700" />
    <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Material+Icons" />

    <style type="text/css">
        .resultImg img {
            max-height: 84px;
            max-width: 60px;
        }
    </style>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="Server">
    <asp:Panel DefaultButton="brSearch" runat="server">
        <div style="padding-top: 50px">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 25%; text-align: right">
                        <div class="col-md-4 col-md-offset-8">
                            <asp:DropDownList ID="ddlField" runat="server" CssClass="selectpicker" data-style="btn btn-primary btn-round">
                                <asp:ListItem Text="<%$ Resources:Resource,Title %>" Value="Title"></asp:ListItem>
                                <asp:ListItem Text="<%$ Resources:Resource,Author %>" Value="Author"></asp:ListItem>
                                <asp:ListItem Text="ISBN" Value="ISBN"></asp:ListItem>
                                <asp:ListItem Text="ISSN" Value="ISSN" Enabled="false"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td style="width: 50%">
                        <asp:TextBox ID="tbSearch" runat="server" Style="width: 100%; padding: 0px; margin: 0px;" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td style="width: 25%">
                        <div class="col-md-4" style="margin-left: 1px; width: 130px">
                            <asp:DropDownList ID="ddlClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" CssClass="selectpicker" data-style="btn btn-primary btn-round">
                                <asp:ListItem Text="<%$ Resources:Resource,Book %>" Value="Books" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="<%$ Resources:Resource,Periodical %>" Value="Periodicals"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <asp:Button ID="brSearch" runat="server" Text="<%$ Resources:Resource,Search %>" OnClick="brSearch_Click" CssClass="btn btn-default btn-fill" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; height: 30px" colspan="3">&nbsp;</td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <div class="table-responsive" style="margin: 0px 150px">
        <asp:GridView ID="gvBookResult" runat="server" AutoGenerateColumns="False" Enabled="False" CssClass="table table-striped" GridLines="None" DataKeyNames="BookId" OnRowDataBound="gvBookResult_RowDataBound" Width="80%" HorizontalAlign="Center">
            <Columns>
                <asp:ImageField HeaderText="<%$ Resources:Resource, Cover %>" DataImageUrlField="ImageURL" ReadOnly="True" HeaderStyle-CssClass="text-center">
                    <ItemStyle Height="84px" HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" CssClass="resultImg" />
                </asp:ImageField>
                <asp:HyperLinkField HeaderText="<%$ Resources:Resource, BookTitle %>" DataNavigateUrlFields="BookId" DataNavigateUrlFormatString="~/Pages/ReaderPages/bookMessage.aspx?book_id={0}" DataTextField="Title" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"></asp:HyperLinkField>
                <asp:BoundField HeaderText="<%$ Resources:Resource, Author %>" DataField="Author" ReadOnly="True" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"></asp:BoundField>
                <asp:BoundField DataField="Publisher" HeaderText="<%$ Resources:Resource, Publisher %>" ReadOnly="True" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"></asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        </td></tr>
                        <tr>
                            <td colspan="100%">
                                <div class="accordion" id="simple-accordion">
                                    <div class="accordion-header">
                                        <label>
                                            <asp:Label ID="copy" runat="server"></asp:Label></label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <label>
                                            <asp:Label ID="copy0" runat="server"></asp:Label></label>
                                    </div>
                                    <div class="accordion-content" data-wrapper="true" style="height: 0px; position: relative; overflow: hidden;" aria-expanded="false">
                                        <div>
                                            <asp:GridView ID="gvCopy" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" OnRowCommand="gvCopy_RowCommand" DataKeyNames="BookBarcode">
                                                <Columns>
                                                    <asp:BoundField DataField="BookBarcode" HeaderText="<%$ Resources:Resource,Barcode %>" HeaderStyle-CssClass="text-primary" />
                                                    <asp:BoundField DataField="Position" HeaderText="<%$ Resources:Resource,Position %>" HeaderStyle-CssClass="text-primary" />
                                                    <asp:BoundField DataField="newStatus" HeaderText="<%$ Resources:Resource,Status %>" HeaderStyle-CssClass="text-primary" />
                                                    <asp:TemplateField ItemStyle-CssClass="text-center" ItemStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:Button ID="ButtonReserve" Text="<%$ Resources:Resource, Reserve %>" CssClass="btn btn-info" CommandName="Btn_reserve" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="table-responsive" style="margin: 0px 150px">
        <asp:GridView ID="gvPeriodicalResult" runat="server" AutoGenerateColumns="False" Enabled="False" CssClass="table table-striped" GridLines="None">
            <Columns>
                <asp:ImageField HeaderText="<%$ Resources:Resource, Cover %>" DataImageUrlField="ImageURL" ReadOnly="True" HeaderStyle-CssClass="text-center">
                    <ItemStyle Height="84px" HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" CssClass="resultImg" />
                </asp:ImageField>
                <asp:BoundField HeaderText="<%$ Resources:Resource, Title %>" DataField="Title" ReadOnly="True" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"></asp:BoundField>
                <asp:BoundField HeaderText="<%$ Resources:Resource, ISSN %>" DataField="ISSN" ReadOnly="True" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"></asp:BoundField>
                <asp:BoundField HeaderText="<%$ Resources:Resource, Country %>" DataField="Country" ReadOnly="True" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"></asp:BoundField>
                <asp:BoundField HeaderText="<%$ Resources:Resource, Type %>" DataField="NewType" ReadOnly="True" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"></asp:BoundField>
                <asp:BoundField HeaderText="<%$ Resources:Resource, Price %>" DataField="Price" ReadOnly="True" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"></asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    <div id="notice" class="container" runat="server">
        <ul class="mySlideshow">
            <li class="first">
                <a href="LibrarianPages/SearchNotice.aspx" class="animated fadeInLeft">
                    <asp:Label ID="title1" runat="server" Text="No title" />
                </a>
                <div class="animated fadeInRight">
                    <asp:Label ID="notice1" runat="server" Text="No announcement!"></asp:Label>
                </div>
                <div class="animated fadeInRight" style="top: 175px; font-size: 22px; color: aliceblue">
                    <asp:Label ID="date1" runat="server" Text="No date!"></asp:Label>
                </div>
            </li>
            <li class="second">
                <a href="LibrarianPages/SearchNotice.aspx" class="animated fadeInRight">
                    <asp:Label ID="title2" runat="server" Text="No title" />
                </a>
                <div class="animated fadeInLeft">
                    <asp:Label ID="notice2" runat="server" Text="No announcement!"></asp:Label>
                </div>
                <div class="animated fadeInRight" style="top: 175px; font-size: 22px; color: aliceblue">
                    <asp:Label ID="date2" runat="server" Text="No date!"></asp:Label>
                </div>
            </li>
            <li class="third">
                <a href="LibrarianPages/SearchNotice.aspx" class="animated fadeInLeft">
                    <asp:Label ID="title3" runat="server" Text="No title" />
                </a>
                <div class="animated fadeInRight">
                    <asp:Label ID="notice3" runat="server" Text="No announcement!"></asp:Label>
                </div>
                <div class="animated fadeInRight" style="top: 175px; font-size: 22px; color: aliceblue">
                    <asp:Label ID="date3" runat="server" Text="No date!"></asp:Label>
                </div>
            </li>
        </ul>
        <br />
        <br />
        <br />
        <div style="text-align: center;">
            <a href="ReaderPages/ViewNotice.aspx" style="color: cadetblue; font-size: 16px">
                <asp:Label ID="search" runat="server" Text="<%$ Resources:Resource, MoreNotices %>" />
            </a>
        </div>
    </div>
</asp:Content>

<asp:Content ID="foot" runat="server" ContentPlaceHolderID="foot">
    <script>
        $(".accordion").accordion({
            collapsible: true,
            animate: 200
        });
        $('body').on('click', '#btn-color-targets > .btn', function () {
            var color = $(this).data('target-color');
            $('#modalColor').attr('data-modal-color', color);
        });
    </script>

    <script src="../../Scripts/art-Template/template-web.js"></script>
    <script src="../../assets/vendors/dropzone/dropzone.min.js"></script>
    <script src="../../assets/vendors/jquery.select-bootstrap.js"></script>
    <script src="../../assets/slider/js/jquery-1.11.0.min.js"></script>
    <script src="../../assets/slider/js/jquery.edslider.js"></script>

    <script>
        $(document).ready(function () {
            //Call plugin
            $('.mySlideshow').edslider({
                width: '100%',
                height: 250
            });
        });
    </script>

    <script>
        $("#<%=tbSearch.ClientID %>").bind('input propertychange', function () {
            var body_notice = document.getElementById('<%=notice.ClientID %>');
            if ($(this).val()) {
                body_notice.setAttribute("style", "visibility:hidden;");
            }
            else {
                body_notice.setAttribute("style", "visibility:visible;");
            }
        });
    </script>
</asp:Content>
