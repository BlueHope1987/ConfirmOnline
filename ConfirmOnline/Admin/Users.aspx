<%@ Page Title="管理用户" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="ConfirmOnline.Admin.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Container" runat="server">
        <div class="row " style="height:20px;"></div>
        <div class="row ">
            <div class="col-md-3">
                <ul class="nav nav-pills nav-stacked">
	                <li><a href="AdminPage">Home</a></li>
	                <li><a href="SiteCfgs">全局设定</a></li>
	                <li><a href="WorkTable">工作表</a></li>
	                <li><a href="Editflow">编辑纪录</a></li>
	                <li class="active"><a href="Users">用户</a></li>
                </ul>
            </div>
            <div class="col-md-9">
                <h3>用户查看</h3>
                <div ID="UserMgrNote" class="well col-centered" runat="server">
                    <asp:SqlDataSource runat="server" ID="UserMgrDBVister" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' DeleteCommand="DELETE FROM AspNetUsers FROM AspNetUsers FULL OUTER JOIN AspNetUserRoles ON AspNetUsers.Id = AspNetUserRoles.UserId WHERE (AspNetUsers.Id = @Id)" SelectCommand="SELECT AspNetUsers.Id, AspNetUsers.Email, AspNetUsers.PhoneNumber, AspNetUsers.UserName, AspNetRoles.Name FROM AspNetRoles INNER JOIN AspNetUserRoles ON AspNetRoles.Id = AspNetUserRoles.RoleId FULL OUTER JOIN AspNetUsers ON AspNetUserRoles.UserId = AspNetUsers.Id" UpdateCommand="UPDATE AspNetUsers SET Email = @Email, PhoneNumber = @PhoneNumber, UserName = @UserName">
                        <DeleteParameters>
                            <asp:ControlParameter ControlID="UserMgrLst" Name="Id" PropertyName="SelectedValue" Type="String" />
                        </DeleteParameters>
                        <UpdateParameters>
                            <asp:ControlParameter ControlID="UserMgrLst" Name="Email" PropertyName="SelectedValue" Type="String" />
                            <asp:ControlParameter ControlID="UserMgrLst" Name="PhoneNumber" PropertyName="SelectedValue" Type="String" />
                            <asp:ControlParameter ControlID="UserMgrLst" Name="UserName" PropertyName="SelectedValue" Type="String" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="UserMgrDBSlt" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" DeleteCommand="DELETE FROM AspNetUsers FROM AspNetUsers FULL OUTER JOIN AspNetUserRoles ON AspNetUsers.Id = AspNetUserRoles.UserId WHERE (AspNetUsers.Id = @Id)" SelectCommand="SELECT * FROM [AspNetUsers] WHERE ([Id] = @Id)" UpdateCommand="UPDATE [AspNetUsers] SET [Email] = @Email, [PhoneNumber] = @PhoneNumber, [UserName] = @UserName WHERE [Id] = @Id">
                        <DeleteParameters>
                            <asp:Parameter Name="Id" Type="String" />
                        </DeleteParameters>
                        <SelectParameters>
                            <asp:ControlParameter ControlID="UserMgrLst" Name="Id" PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="Email" Type="String" />
                            <asp:Parameter Name="PhoneNumber" Type="String" />
                            <asp:Parameter Name="UserName" Type="String" />
                            <asp:Parameter Name="Id" Type="String" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                    <asp:ListView ID="UserMgrLst" runat="server" DataSourceID="UserMgrDBVister" DataKeyNames="Id">
                        <AlternatingItemTemplate>
                            <tr style="background-color:#D6E6F5;">
                                <td>
                                    <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("Email") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="PhoneNumberLabel" runat="server" Text='<%# Eval("PhoneNumber") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="UserNameLabel" runat="server" Text='<%# Eval("UserName") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                                </td>
                                <td>
                                    <asp:LinkButton ID="SelectButton" runat="server" CommandName="Select" CssClass="btn btn-primary btn-xs" Text="选择" />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <EditItemTemplate>
                            <tr style="background-color:#008A8C;color: #FFFFFF;">
                                <td>
                                    <asp:TextBox ID="IdTextBox" runat="server" Text='<%# Bind("Id") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="PhoneNumberTextBox" runat="server" Text='<%# Bind("PhoneNumber") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="UserNameTextBox" runat="server" Text='<%# Bind("UserName") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                                </td>
                                <td>
                                    <asp:LinkButton ID="UpdateButton" CssClass="btn btn-danger" runat="server" CommandName="Update" Text="更新" />
                                    <asp:LinkButton ID="CancelButton" CssClass="btn btn-primary" runat="server" CommandName="Cancel" Text="取消" />
                                </td>
                            </tr>
                        </EditItemTemplate>
                        <EmptyDataTemplate>
                            <table runat="server" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                                <tr>
                                    <td>未返回数据。</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <InsertItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:TextBox ID="IdTextBox" runat="server" Text='<%# Bind("Id") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="PhoneNumberTextBox" runat="server" Text='<%# Bind("PhoneNumber") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="UserNameTextBox" runat="server" Text='<%# Bind("UserName") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                                </td>
                                <td>
                                    <asp:LinkButton ID="InsertButton" runat="server" CommandName="Insert" Text="插入" />
                                    <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="清除" />
                                </td>
                            </tr>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <tr style="color: #000000;">
                                <td>
                                    <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("Email") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="PhoneNumberLabel" runat="server" Text='<%# Eval("PhoneNumber") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="UserNameLabel" runat="server" Text='<%# Eval("UserName") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                                </td>
                                <td>
                                    <asp:LinkButton ID="SelectButton" runat="server" CssClass="btn btn-primary btn-xs"  CommandName="Select" Text="选择" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table runat="server">
                                <tr runat="server">
                                    <td runat="server">
                                        <table id="itemPlaceholderContainer" runat="server" class="table table-responsive table-condensed table-hover" style="font-size: small">
                                            <tr runat="server" style="background-color:#85B3DC;color: #000000;">
                                                <th runat="server">用户编号</th>
                                                <th runat="server">邮箱</th>
                                                <th runat="server">电话号码</th>
                                                <th runat="server">用户名</th>
                                                <th runat="server">用户角色</th>
                                                <th runat="server">操作</th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" style="text-align: center;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                                        <asp:DataPager ID="DataPager1" runat="server">
                                            <Fields>
                                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" />
                                            </Fields>
                                        </asp:DataPager>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <SelectedItemTemplate>
                            <tr style="background-color:#008A8C;font-weight: bold;color: #FFFFFF;">
                                <td>
                                    <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("Email") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="PhoneNumberLabel" runat="server" Text='<%# Eval("PhoneNumber") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="UserNameLabel" runat="server" Text='<%# Eval("UserName") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                                </td>
                                <td>
                                    <asp:LinkButton ID="SelectButton" CssClass="btn btn-primary btn-xs" runat="server" CommandName="Select" Text="选择" />
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                <asp:FormView ID="UserMgrForm" runat="server" DataKeyNames="Id" DataSourceID="UserMgrDBSlt">
                    <EditItemTemplate>
                        Email:
                        <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' />
                        <br />
                        PhoneNumber:
                        <asp:TextBox ID="PhoneNumberTextBox" runat="server" Text='<%# Bind("PhoneNumber") %>' />
                        <br />
                        UserName:
                        <asp:TextBox ID="UserNameTextBox" runat="server" Text='<%# Bind("UserName") %>' />
                        <br />
                        <asp:LinkButton ID="UpdateButton" runat="server" CssClass="btn btn-warning btn-xs" CausesValidation="True" CommandName="Update" Text="更新" />
                        &nbsp;<asp:LinkButton ID="UpdateCancelButton" CssClass="btn btn-primary btn-xs" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        用户编号:
                        <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                        <br />
                        邮箱:
                        <asp:Label ID="EmailLabel" runat="server" Text='<%# Bind("Email") %>' />
                        <br />
                        邮箱确认状态:
                        <asp:CheckBox ID="EmailConfirmedCheckBox" runat="server" Checked='<%# Bind("EmailConfirmed") %>' Enabled="false" />
                        <br />
                        编译过的口令:
                        <asp:Label ID="PasswordHashLabel" runat="server" Text='<%# Bind("PasswordHash") %>' />
                        <br />
                        SecurityStamp:
                        <asp:Label ID="SecurityStampLabel" runat="server" Text='<%# Bind("SecurityStamp") %>' />
                        <br />
                        电话号码:
                        <asp:Label ID="PhoneNumberLabel" runat="server" Text='<%# Bind("PhoneNumber") %>' />
                        <br />
                        电话确认状态:
                        <asp:CheckBox ID="PhoneNumberConfirmedCheckBox" runat="server" Checked='<%# Bind("PhoneNumberConfirmed") %>' Enabled="false" />
                        <br />
                        TwoFactorEnabled:
                        <asp:CheckBox ID="TwoFactorEnabledCheckBox" runat="server" Checked='<%# Bind("TwoFactorEnabled") %>' Enabled="false" />
                        <br />
                        LockoutEndDateUtc:
                        <asp:Label ID="LockoutEndDateUtcLabel" runat="server" Text='<%# Bind("LockoutEndDateUtc") %>' />
                        <br />
                        LockoutEnabled:
                        <asp:CheckBox ID="LockoutEnabledCheckBox" runat="server" Checked='<%# Bind("LockoutEnabled") %>' Enabled="false" />
                        <br />
                        AccessFailedCount:
                        <asp:Label ID="AccessFailedCountLabel" runat="server" Text='<%# Bind("AccessFailedCount") %>' />
                        <br />
                        用户名:
                        <asp:Label ID="UserNameLabel" runat="server" Text='<%# Bind("UserName") %>' />
                        <br />
                        <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-warning btn-xs" Text="编辑" />
                        <asp:LinkButton ID="SetAdmin" runat="server" CausesValidation="False" CssClass="btn btn-warning btn-xs" Text="切换管理员" OnClick="SetAdmin_Click" />
                        &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" CssClass="btn btn-danger btn-xs" Text="删除" />
                        &nbsp;<asp:LinkButton ID="NewButton" runat="server" CssClass="btn btn-primary btn-xs" Text="新建" OnClick="NewButton_Click" />
                    </ItemTemplate>
                    </asp:FormView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
