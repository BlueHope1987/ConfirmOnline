<%@ Page Title="管理工作表" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WorkTable.aspx.cs" Inherits="ConfirmOnline.Admin.WorkTable" %>
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
                <div ID="WorkTableNote" class="well col-centered" runat="server">
                    <p>在线显示您当前配置式下工作表及修订状态，展开大型的工作表会耗费较多的服务器资源。</p>
                    <asp:LinkButton ID="LkBtnStart" class="btn btn-warning" runat="server" OnClick="LkBtnStart_Click">点击开始</asp:LinkButton>
                </div>
                <div id="WorkTableDiv" runat="server" visible="false" class="row">
                    <div class="col-md-4" style="padding-left:0px;">
                        <div class="input-group input-group-sm">
                            <asp:TextBox ID="TbxSearch" runat="server" class="form-control" placeholder="输入您想检索的内容"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:LinkButton ID="BtnSearch" type="button" class="btn btn-primary" runat="server" OnClick="BtnSearch_Click">搜索</asp:LinkButton>
                            </span>
                        </div>
                    </div>
                    <div class="btn-group btn-group-sm pull-right cod-md-6">
                        <asp:LinkButton ID="BtnDspOver" type="button" class="btn btn-primary" runat="server" OnClick="BtnDspOver_Click">显示最终状态</asp:LinkButton>
                        <asp:LinkButton ID="BtnColor" type="button" class="btn btn-primary" runat="server" OnClick="BtnColor_Click">着色修订项</asp:LinkButton>
                        <asp:LinkButton ID="BtnDspNub" type="button" class="btn btn-primary" runat="server" OnClick="BtnDspNub_Click">显示修订次数</asp:LinkButton>
                        <asp:LinkButton ID="BtnHidFixed" type="button" class="btn btn-primary" runat="server" OnClick="BtnHidFixed_Click" >隐藏已修订</asp:LinkButton>
                        <asp:LinkButton ID="BtnHidInital" type="button" class="btn btn-primary" runat="server" OnClick="BtnHidInital_Click" >隐藏未修订</asp:LinkButton>
                        <asp:LinkButton ID="SaveTable" type="button" class="btn btn-primary" runat="server" OnClick="SaveTable_Click">保存下面的数据</asp:LinkButton>
                    </div>
                    <asp:GridView ID="WorkTableView" runat="server" CssClass="table table-responsive table-condensed table-hover" OnRowDataBound="WorkTableView_RowDataBound" BorderStyle="Solid" BackColor="WhiteSmoke" BorderWidth="1px" EmptyDataText="表格中没有数据，请检查您的设置。" Font-Size="Small" GridLines="None">
                        <AlternatingRowStyle BackColor="#D6E6F5" BorderStyle="None" BorderWidth="1px" />
                        <HeaderStyle BackColor="#85B3DC" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
