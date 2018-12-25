<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/LibrarianPages/MasterPage.master" AutoEventWireup="true" CodeFile="ShelfInfo.aspx.cs" Inherits="Pages_ShelfInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="header" ContentPlaceHolderID="header" Runat="Server">
    <a> <asp:Label runat="server" Text="<%$ Resources:Resource, ShelfInfo %>" CssClass="navbar-brand"></asp:Label> </a>
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
	                                            <label class="col-sm-1 control-label"><asp:Label ID="Label6" runat="server" Text="<%$ Resources:Resource,ShelfId %>"></asp:Label>
	                                            </label>
                                                <div class="col-sm-10">
                                                    <asp:Label ID="LabelShelfId" runat="server" Text="ShelfId" Cssclass="form-control"></asp:Label>
	                                            </div>
	                                        </div>
	                                    </fieldset>
                                        &nbsp
                                        <fieldset>
	                                        <div class="form-group">
	                                            <label class="col-sm-1 control-label"><asp:Label ID="Label10" runat="server" Text="<%$ Resources:Resource,StackId %>"></asp:Label>
	                                            </label>
                                                <div class="col-sm-10">
                                                    <asp:Label ID="LabelStackId" runat="server" Text="StackId" Cssclass="form-control"></asp:Label>
	                                            </div>
	                                        </div>
	                                    </fieldset>
                                        &nbsp
                                         <fieldset>
	                                        <div class="form-group">
	                                            <label class="col-sm-1 control-label"><asp:Label ID="Label8" runat="server" Text="<%$ Resources:Resource, Stack_Summary %>"></asp:Label></label>
	                                            <div class="col-sm-10">
                                                    <asp:Label ID="LabelShelf_Summary" runat="server" Text="Shelf_Summary" Cssclass="form-control"></asp:Label>
	                                            </div>
	                                        </div>
	                                    </fieldset>
                                        &nbsp
                                         <fieldset>
	                                        <div class="form-group">
	                                            <label Class="col-sm-1 control-label"><asp:Label ID="Label9" runat="server" Text="<%$ Resources:Resource, Shelf_Timestamp %>"></asp:Label></label>
	                                            <div class="col-sm-10">
	                                                <asp:Label ID="LabelShelf_Timestamp" runat="server" Text="Shelf_Timestamp" Cssclass="form-control"></asp:Label>
	                                            </div>
	                                        </div>
	                                    </fieldset>
                                        &nbsp
                                        <div class="form-group">
                                        <asp:Button ID="Button1" runat="server" Text="<%$ Resources:Resource, Edit %>"  OnClick="Edit_ShelfInfo" CausesValidation="False" CssClass="btn btn-fill btn-default" />
                                        <asp:Button ID="Button2" runat="server" Text="<%$ Resources:Resource, Cancel %>" OnClick="Cancel" CausesValidation="False" CssClass="btn btn-fill btn-default" />
                                            </div>
                                    </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>   
</asp:Content>