<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="dataviewtest.aspx.cs" Inherits="ConfirmOnline.Operation.dataviewtest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Container" runat="server">
        <div class="row" style="height:20px;"></div>
        <div class="row center-block">
            <div class="well col-md-6 col-md-offset-3 col-centered text-center">
                <h2>请核实确认您的信息</h2>
                    <div id="divContainer" style="margin-top: 30px;" runat="server">
                    </div>
                    <asp:Button ID="btn_Submit" type="submit" class="btn btn-success btn-block" style="margin-top: 25px;" runat="server" Text="确认" OnClick="btn_Submit_Click"/>
            </div>
        </div>
    </div>

    <asp:GridView ID="DataGrid1" runat="server">
    </asp:GridView>
</asp:Content>
