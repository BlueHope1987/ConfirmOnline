<%@ Page Title="管理首页" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="ConfirmOnline.Admin.AdminPage" %>
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
                <p>请注意，为了保证开发效率尽早可用且配置不被随意或意外改动，有些管理项并未进行充分的易用性优化甚至完成实现。</p>
                <p>请不要更改您不明白的选项，一些高级操作请联系相关专家。</p>
            </div>
        </div>
    </div>
</asp:Content>
