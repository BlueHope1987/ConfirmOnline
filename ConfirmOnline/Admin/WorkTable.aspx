<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WorkTable.aspx.cs" Inherits="ConfirmOnline.Admin.WorkTable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Container" runat="server">
        <div class="row " style="height:20px;"></div>
        <div class="row ">
            <div class="col-md-3">
                <ul class="nav nav-pills nav-stacked">
	                <li><a href="AdminPage">Home</a></li>
	                <li><a href="SiteCfgs">全局设定</a></li>
	                <li class="active"><a href="#">工作表</a></li>
	                <li><a href="Editflow">编辑纪录</a></li>
	                <li><a href="Users">用户</a></li>
                </ul>
            </div>
            <div class="col-md-9">
                <h3>工作表详览</h3>
                <p>在线显示您当前配置式下工作表及修订状态，操作较大的工作表会耗费大量的服务器资源。</p>
                <asp:LinkButton ID="LkBtnStart" class="btn btn-warning" runat="server" OnClick="LkBtnStart_Click">点击开始</asp:LinkButton>
                <div id="WorkTableDiv" runat="server" visible="false" class="row">
                    <asp:GridView ID="WorkTableView" runat="server" CssClass="table table-striped col-md-12" OnRowCreated="WorkTableView_RowCreated"></asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
