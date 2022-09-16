<%@ Page Title="管理编辑纪录" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Editflow.aspx.cs" Inherits="ConfirmOnline.Admin.Editflow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Container" runat="server">
        <div class="row " style="height:20px;"></div>
        <div class="row ">
            <div class="col-md-3">
                <ul class="nav nav-pills nav-stacked">
	                <li><a href="AdminPage">Home</a></li>
	                <li><a href="SiteCfgs">全局设定</a></li>
	                <li><a href="WorkTable">工作表</a></li>
	                <li class="active"><a href="#">编辑纪录</a></li>
	                <li><a href="Users">用户</a></li>
                </ul>
            </div>
            <div class="col-md-9">
                <h3>编辑流水记录详览</h3>
                <div ID="EditFlowNote" class="well col-centered" runat="server">
                    <p>在这里罗列显示配置式下工作表的编辑流水记录。</p>
                    <asp:LinkButton ID="LkBtnStart" class="btn btn-warning" runat="server" OnClick="LkBtnStart_Click">点击开始</asp:LinkButton>
                </div>
                <div id="EdFlViewDiv" runat="server" visible="false" class="row">

                    <div class="col-md-4" style="padding-left:0px;">
                        <div class="input-group input-group-sm">
                            <asp:TextBox ID="TbxSearch" runat="server" class="form-control" placeholder="输入您想检索的条目"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:LinkButton ID="BtnSearch" type="button" class="btn btn-primary" runat="server" OnClick="BtnSearch_Click">搜索</asp:LinkButton>
                                <a ID="LinkButton1" class="btn btn-success" href="Editflow.aspx?reset=true">重置</a>
                            </span>
                        </div>
                    </div>

                    <asp:SqlDataSource runat="server" ID="EditFlowDBVister" ConnectionString='<%$ ConnectionStrings:SiteContext %>' SelectCommand="SELECT * FROM [EditFlows] WHERE ([CfgID] = @CfgID)">
                        <SelectParameters>
                            <asp:SessionParameter Name="CfgID" SessionField="currCfgID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="EditFlowDBSelected" runat="server" ConnectionString="<%$ ConnectionStrings:SiteContext %>" SelectCommand="SELECT * FROM [EditFlows] WHERE ([Id] = @Id)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="EdFlLstView" Name="Id" PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                    <asp:GridView ID="EdFlLstView" runat="server" AllowPaging="True" CssClass="table table-responsive table-condensed table-hover"  AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="EditFlowDBVister" BorderStyle="Solid" BackColor="WhiteSmoke" BorderWidth="1px" EmptyDataText="表格中没有数据，请检查您的设置。" Font-Size="Small" GridLines="None" OnRowDataBound="EdFlLstView_RowDataBound" OnRowCommand="EdFlLstView_RowCommand">
                        <AlternatingRowStyle BackColor="#D6E6F5" BorderStyle="None" BorderWidth="1px" />
                        <HeaderStyle BackColor="#85B3DC" />
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="流水号" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                            <asp:BoundField DataField="FixerDate" HeaderText="修订时间" SortExpression="FixerDate" />
<%--                            <asp:BoundField DataField="FixRow" HeaderText="修订条目" SortExpression="FixRow" />--%>
                            <asp:ButtonField DataTextField="FixRow" HeaderText="修订条目" SortExpression="FixRow" CommandName="Search" />
                            <asp:BoundField DataField="FixCol" HeaderText="修订项" SortExpression="FixCol" />
                            <asp:BoundField DataField="FixNew" HeaderText="新内容" SortExpression="FixNew" />
                            <asp:CommandField HeaderText="详情" ShowSelectButton="True" ButtonType="Button" SelectText="查看" />
                        </Columns>
                    </asp:GridView>

                    <asp:DetailsView ID="EdFlDtlView" runat="server" CssClass="table table-responsive table-condensed table-hover"  AutoGenerateRows="False" DataKeyNames="Id" DataSourceID="EditFlowDBSelected" BorderStyle="None">
                        <FieldHeaderStyle BackColor="#C9D9F8" />
                        <Fields>
                            <asp:BoundField DataField="Id" HeaderText="流水号" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                            <asp:BoundField DataField="CfgID" HeaderText="对应配置式编号" SortExpression="CfgID" />
                            <asp:BoundField DataField="FixerID" HeaderText="修订者登录ID" SortExpression="FixerID" />
                            <asp:BoundField DataField="FixerDetal" HeaderText="修订详情" SortExpression="FixerDetal" />
                            <asp:BoundField DataField="FixerDate" HeaderText="修订时间" SortExpression="FixerDate" />
                            <asp:BoundField DataField="FixRow" HeaderText="修订条目" SortExpression="FixRow" />
                            <asp:BoundField DataField="FixCol" HeaderText="修订项" SortExpression="FixCol" />
                            <asp:BoundField DataField="FixNew" HeaderText="新内容" SortExpression="FixNew" />
                            <asp:BoundField DataField="FixOld" HeaderText="旧内容" SortExpression="FixOld" />
                        </Fields>
                        <HeaderStyle BackColor="#C9D9F8" />
                        <RowStyle BackColor="#DDEEFF" />
                    </asp:DetailsView>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
