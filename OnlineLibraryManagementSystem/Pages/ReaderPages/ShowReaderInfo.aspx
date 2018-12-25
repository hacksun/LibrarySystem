<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="ShowReaderInfo.aspx.cs" Inherits="Pages_ShowReaderInfo" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <div class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-8">
                    <div class="card">
                        <div class="content">
                            <h3>
                                <asp:Label ID="LabelName" runat="server" Text="" CssClass="title"></asp:Label></h3>
                            <div class="nav-tabs-navigation">
                                <div class="nav-tabs-wrapper">
                                    <ul class="nav nav-tabs" data-tabs="tabs">
                                        <li class="active">
                                            <a href="#issue" aria-controls="issue" role="tab" data-toggle="tab">
                                                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Resource, IssueRecords %>"></asp:Label>
                                            </a>
                                            <li>
                                        <li class="">
                                            <a href="#history" aria-controls="history" role="tab" data-toggle="tab">
                                                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Resource, returnHistory %>"></asp:Label>
                                            </a>
                                            <li>
                                        <li class="">
                                            <a href="#reversation" aria-controls="reversation" role="tab" data-toggle="tab">
                                                <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Resource, ReversationRecords %>"></asp:Label>
                                            </a>
                                            <li>
                                    </ul>
                                </div>
                            </div>
                            <div class="tab-content">
                                <div id="issue" class="tab-pane active" role="tabpanel">
                                    <div class="table-responsive">
                                        <asp:GridView CssClass="table table-hover" ID="GridView1" runat="server" GridLines="None">
                                            <HeaderStyle CssClass="text-primary" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div id="history" class="tab-pane" role="tabpanel">
                                    <div class="table-responsive">
                                        <asp:GridView CssClass="table table-hover" ID="GridView3" runat="server" GridLines="None">
                                            <HeaderStyle CssClass="text-primary" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div id="reversation" class="tab-pane" role="tabpanel">
                                    <div class="table-responsive">
                                        <asp:GridView CssClass="table table-hover" ID="GridView2" runat="server" GridLines="None">
                                            <HeaderStyle CssClass="text-primary" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card card-user" style="padding: 15px">
                        <h4>
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Resource, ReaderInfo %>" CssClass="title"></asp:Label></h4>
                        <div class="form-group label-floating">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Resource, Name %>" CssClass="control-label"></asp:Label>
                            <asp:TextBox ID="TextBoxName" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="form-group label-floating">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Resource, IDNumber %>" CssClass="control-label"></asp:Label>
                            <asp:TextBox ID="TextBoxIDNumber" runat="server" CssClass="form-control" Enabled="False" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="form-group label-floating">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Resource, Telephone %>" CssClass="control-label"></asp:Label>
                            <asp:TextBox ID="TextBoxTelephone" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="form-group label-floating">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Resource, Email %>" CssClass="control-label"></asp:Label>
                            <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="form-group label-floating">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Resource, Fine %>" CssClass="control-label"></asp:Label>
                            <asp:TextBox ID="TextBoxFine" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="form-footer text-right">
                            <asp:LinkButton ID="lbChangePassword" runat="server" Text="<%$ Resources:Resource, ChangePassword %>" PostBackUrl="~/Pages/ReaderPages/ChangePassword.aspx" CssClass="btn btn-rose btn-fill"></asp:LinkButton>
                            <asp:LinkButton ID="lbChangeReaderInfomation" runat="server" Text="<%$ Resources:Resource, ChangeReaderInfomation %>" PostBackUrl="~/Pages/ReaderPages/ChangeReaderInfomation.aspx" CssClass="btn btn-rose btn-fill"></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="foot" ContentPlaceHolderID="foot" runat="server">
</asp:Content>