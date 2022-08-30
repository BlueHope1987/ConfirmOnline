<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SiteCfgs.aspx.cs" Inherits="ConfirmOnline.Admin.SiteCfgs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Container" runat="server">
        <div class="row " style="height:20px;"></div>
        <div class="row ">
            <div class="col-md-3">
                <ul class="nav nav-pills nav-stacked">
	                <li><a href="AdminPage">Home</a></li>
	                <li class="active"><a href="#">配置式</a></li>
	                <li><a href="CfgSetting">全局设定</a></li>
	                <li><a href="WorkTable">工作表</a></li>
	                <li><a href="Editflow">编辑纪录</a></li>
	                <li><a href="Users">用户</a></li>
                </ul>
            </div>
            <div class="col-md-9">
                <asp:ListView ID="ListView1" runat="server" DataSourceID="LinqDataSource1">
                    <AlternatingItemTemplate>
                        <tr style="background-color: #FFF8DC;">
                            <td>
                                <asp:Label ID="CfgIDLabel" runat="server" Text='<%# Eval("CfgID") %>' />
                            </td>
                            <td>
                                <asp:Label ID="CfgNameLabel" runat="server" Text='<%# Eval("CfgName") %>' />
                            </td>
                            <td>
                                <asp:Label ID="CfgCreatorLabel" runat="server" Text='<%# Eval("CfgCreator") %>' />
                            </td>
                            <td>
                                <asp:Label ID="CfgCrtTimeLabel" runat="server" Text='<%# Eval("CfgCrtTime") %>' />
                            </td>
                            <td>
                                <asp:CheckBox ID="CfgIsEnableCheckBox" runat="server" Checked='<%# Eval("CfgIsEnable") %>' Enabled="false" />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <EditItemTemplate>
                        <tr style="background-color: #008A8C; color: #FFFFFF;">
                            <td>
                                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="更新" />
                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="取消" />
                            </td>
                            <td>
                                <asp:TextBox ID="CfgIDTextBox" runat="server" Text='<%# Bind("CfgID") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="CfgNameTextBox" runat="server" Text='<%# Bind("CfgName") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="CfgCreatorTextBox" runat="server" Text='<%# Bind("CfgCreator") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="CfgCrtTimeTextBox" runat="server" Text='<%# Bind("CfgCrtTime") %>' />
                            </td>
                            <td>
                                <asp:CheckBox ID="CfgIsEnableCheckBox" runat="server" Checked='<%# Bind("CfgIsEnable") %>' />
                            </td>
                        </tr>
                    </EditItemTemplate>
                    <EmptyDataTemplate>
                        <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                            <tr>
                                <td>未返回数据。</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <InsertItemTemplate>
                        <tr style="">
                            <td>
                                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="插入" />
                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="清除" />
                            </td>
                            <td>
                                <asp:TextBox ID="CfgIDTextBox" runat="server" Text='<%# Bind("CfgID") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="CfgNameTextBox" runat="server" Text='<%# Bind("CfgName") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="CfgCreatorTextBox" runat="server" Text='<%# Bind("CfgCreator") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="CfgCrtTimeTextBox" runat="server" Text='<%# Bind("CfgCrtTime") %>' />
                            </td>
                            <td>
                                <asp:CheckBox ID="CfgIsEnableCheckBox" runat="server" Checked='<%# Bind("CfgIsEnable") %>' />
                            </td>
                        </tr>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <tr style="background-color: #DCDCDC; color: #000000;">
                            <td>
                                <asp:Label ID="CfgIDLabel" runat="server" Text='<%# Eval("CfgID") %>' />
                            </td>
                            <td>
                                <asp:Label ID="CfgNameLabel" runat="server" Text='<%# Eval("CfgName") %>' />
                            </td>
                            <td>
                                <asp:Label ID="CfgCreatorLabel" runat="server" Text='<%# Eval("CfgCreator") %>' />
                            </td>
                            <td>
                                <asp:Label ID="CfgCrtTimeLabel" runat="server" Text='<%# Eval("CfgCrtTime") %>' />
                            </td>
                            <td>
                                <asp:CheckBox ID="CfgIsEnableCheckBox" runat="server" Checked='<%# Eval("CfgIsEnable") %>' Enabled="false" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table runat="server">
                            <tr runat="server">
                                <td runat="server">
                                    <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;">
                                        <tr runat="server" style="background-color: #DCDCDC; color: #000000;">
                                            <th runat="server">配置式ID</th>
                                            <th runat="server">配置式名称</th>
                                            <th runat="server">创建者</th>
                                            <th runat="server">创建时间</th>
                                            <th runat="server">是否启用</th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" style="text-align: center; background-color: #CCCCCC; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000000;"></td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <SelectedItemTemplate>
                        <tr style="background-color: #008A8C; font-weight: bold; color: #FFFFFF;">
                            <td>
                                <asp:Label ID="CfgIDLabel" runat="server" Text='<%# Eval("CfgID") %>' />
                            </td>
                            <td>
                                <asp:Label ID="CfgNameLabel" runat="server" Text='<%# Eval("CfgName") %>' />
                            </td>
                            <td>
                                <asp:Label ID="CfgCreatorLabel" runat="server" Text='<%# Eval("CfgCreator") %>' />
                            </td>
                            <td>
                                <asp:Label ID="CfgCrtTimeLabel" runat="server" Text='<%# Eval("CfgCrtTime") %>' />
                            </td>
                            <td>
                                <asp:CheckBox ID="CfgIsEnableCheckBox" runat="server" Checked='<%# Eval("CfgIsEnable") %>' Enabled="false" />
                            </td>
                        </tr>
                    </SelectedItemTemplate>
                </asp:ListView>
                <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="ConfirmOnline.Models.SiteContext" EntityTypeName="" Select="new (CfgID, CfgName, CfgCreator, CfgCrtTime, CfgIsEnable)" TableName="SiteSetting">
                </asp:LinqDataSource>

            </div>
        </div>
    </div>
</asp:Content>
