<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecodeLookup.aspx.cs" Inherits="ConfirmOnline.Operation.RecodeLookup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center" id="divContainer" runat="server">
        <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>
    </div>
    <asp:GridView ID="DataGrid1" runat="server">
    </asp:GridView>
</asp:Content>
