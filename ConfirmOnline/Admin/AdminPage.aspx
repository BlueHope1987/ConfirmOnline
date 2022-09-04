<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="ConfirmOnline.Admin.AdminPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div id="Container" runat="server">
        <div class="row " style="height:20px;"></div>
        <div class="row ">
            <div class="col-md-3">
                <ul class="nav nav-pills nav-stacked">
	                <li class="active"><a href="#">Home</a></li>
	                <li><a href="SiteCfgs">全局设定</a></li>
	                <li><a href="WorkTable">工作表</a></li>
	                <li><a href="Editflow">编辑纪录</a></li>
	                <li><a href="Users">用户</a></li>
                </ul>
            </div>
            <div class="col-md-9">
                <h3>开始您的管理</h3>
                <p>您已进入应用后台，点击左边的目录导航到您需要的配置和管理项。</p>
            </div>
        </div>
    </div>
</asp:Content>
