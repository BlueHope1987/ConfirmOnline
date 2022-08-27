<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecodeLookup.aspx.cs" Inherits="ConfirmOnline.Operation.RecodeLookup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="Container" runat="server">
        <div class="row" style="height:20px;"></div>
        <div class="row center-block">
            <div class="well col-md-6 col-md-offset-3 col-centered text-center">
                <h2>请输入您的信息以开始检索</h2>
                    <div id="divContainer" style="margin-top: 30px;" runat="server">
                    </div>
                    <asp:Button ID="btn_Submit" type="submit" class="btn btn-success btn-block" style="margin-top: 25px;" runat="server" Text="查询" OnClick="btn_Submit_Click"/>
<%--                    <asp:Button ID="btn_Submit" type="submit" class="btn btn-success btn-block" style="margin-top: 25px;" runat="server" Text="查询" PostBackUrl="dataviewtest.aspx"/>--%>
            </div>
        </div>
    </div>
</asp:Content>
