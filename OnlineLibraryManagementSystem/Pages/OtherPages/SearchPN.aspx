<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MasterPage.master" AutoEventWireup="true" CodeFile="SearchPN.aspx.cs" Inherits="Pages_SearchPN" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .right {
            text-align:right;
            height:25px;
             width: 85%;
        }

        .style1 {
            width: 100%;
            text-align: center;
            font-family: Consolas;
        }
        .style2{
            height: 35px;
           
        }
        .auto-style2 {
            width: 50%;
            text-align: right;
            height: 35px;
            font-size: x-large;
        }

        .auto-style4 {
            width: 50%;
            text-align: left;
            height: 35px;
            font-size: x-large;
        }
        </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="body" Runat="Server">
    
    <div class="style2">

    </div>
    
    <table class ="style1" dir="ltr">

        <tr>
                <td class="auto-style2">
                    <asp:TextBox ID="TextSearch" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    <asp:Button ID="Button2" runat="server" Text="<%$ Resources:Resource, Search %>" BorderColor="Black" ForeColor="Black" Height="40px" Width="100px" OnClick="Search" />
                    
                </td>
        </tr>

        </table>

    <div class="style1">
    
        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
            <ItemTemplate>
                <%--<%#Eval("Title") %> <a href="<%#Eval("href") %>" ><asp:Label ID="Label1" runat="server" Text="<%$ Resources:Resource, Check %>"></asp:Label></a>--%>
                <%#Eval("Title") %>
            </ItemTemplate>
        </asp:Repeater>
    
    </div>

        </asp:Content>

