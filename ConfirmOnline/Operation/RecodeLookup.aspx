<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecodeLookup.aspx.cs" Inherits="ConfirmOnline.Operation.RecodeLookup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="Container" runat="server">
        <div class="row" style="height:20px;"></div>
        <div class="row center-block">
            <div class="well col-md-6 col-md-offset-3 col-centered text-center">
                <h2>欢迎登录</h2>
                <form:form action="/login" method="post" role="form">
                    <div id="divContainer" runat="server">
                    </div>
                    <button type="submit" class="btn btn-success btn-block">登录</button>
                    <asp:Button ID="Button1" runat="server" Text="Button" />
                </form:form>
            </div>
        </div>
    </div>
    <asp:GridView ID="DataGrid1" runat="server">
    </asp:GridView>
</asp:Content>
