<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SiteCfgs.aspx.cs" Inherits="ConfirmOnline.Admin.SiteCfgs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Container" runat="server">
        <div class="row " style="height: 20px;"></div>
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
                <div id="CfgList" runat="server" class="row">
                    <asp:ListView ID="ListView1" runat="server" DataKeyNames="CfgID" DataSourceID="SqlDataSource1" OnItemDataBound="ListView1_ItemDataBound">
                        <AlternatingItemTemplate>
                            <tr>
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
                                    <asp:Label ID="IfEnable" runat="server" Text="Label">已启用</asp:Label>
                                    <asp:LinkButton ID="CfgDoEnable" runat="server">启用</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="BtnEdit" runat="server" CommandName="Select" OnClick="BtnEdit_Click">编辑</asp:LinkButton>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <EmptyDataTemplate>
                            <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                                <tr>
                                    <td>未返回数据。</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr>
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
                                    <asp:Label ID="IfEnable" runat="server" Text="Label">已启用</asp:Label>
                                    <asp:LinkButton ID="CfgDoEnable" runat="server">启用</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="BtnEdit" runat="server" CommandName="Select" OnClick="BtnEdit_Click">编辑</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table id="itemPlaceholderContainer" runat="server" border="1" class="table table-striped col-md-12">
                                <thead>
                                    <tr runat="server" style="background-color: #DCDCDC; color: #000000;">
                                        <th runat="server">配置式ID</th>
                                        <th runat="server">配置式名称</th>
                                        <th runat="server">创建者</th>
                                        <th runat="server">创建时间</th>
                                        <th runat="server">是否启用</th>
                                        <th runat="server"></th>
                                    </tr>
                                </thead>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <SelectedItemTemplate>
                            <tr style="background-color:#7dcbff; font-weight: bold; color: #FFFFFF;">
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
                                    <asp:Label ID="IfEnable" runat="server" Text="Label">已启用</asp:Label>
                                    <asp:LinkButton ID="CfgDoEnable" runat="server">启用</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="BtnEdit" runat="server" CommandName="Select" OnClick="BtnEdit_Click">编辑</asp:LinkButton>
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SiteContext %>" DeleteCommand="DELETE FROM [SiteSettings] WHERE [CfgID] = @CfgID" InsertCommand="INSERT INTO [SiteSettings] ([CfgName], [CfgCreator], [CfgCrtTime], [CfgIsEnable]) VALUES (@CfgName, @CfgCreator, @CfgCrtTime, @CfgIsEnable)" SelectCommand="SELECT [CfgID], [CfgName], [CfgCreator], [CfgCrtTime], [CfgIsEnable] FROM [SiteSettings]" UpdateCommand="UPDATE [SiteSettings] SET [CfgName] = @CfgName, [CfgCreator] = @CfgCreator, [CfgCrtTime] = @CfgCrtTime, [CfgIsEnable] = @CfgIsEnable WHERE [CfgID] = @CfgID">
                        <DeleteParameters>
                            <asp:Parameter Name="CfgID" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="CfgName" Type="String" />
                            <asp:Parameter Name="CfgCreator" Type="String" />
                            <asp:Parameter Name="CfgCrtTime" Type="DateTime" />
                            <asp:Parameter Name="CfgIsEnable" Type="Boolean" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="CfgName" Type="String" />
                            <asp:Parameter Name="CfgCreator" Type="String" />
                            <asp:Parameter Name="CfgCrtTime" Type="DateTime" />
                            <asp:Parameter Name="CfgIsEnable" Type="Boolean" />
                            <asp:Parameter Name="CfgID" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </div>
                <div id="OpsAre" runat="server" class="row">
                    <asp:LinkButton ID="BtnNew" runat="server" OnClick="BtnNew_Click" type="button" class="btn btn-warning">新建配置式</asp:LinkButton>
                </div>
                <div id="EditAre" runat="server" class="row" visible="false">
                    <div class="panel panel-warning">
                        <div class="panel-heading">
                            <h3 id="EditAreTitle" runat="server" class="panel-title">面板标题</h3>
                        </div>
                        <div class="panel-body">
                            这是一个基本的面板
                                <asp:FormView ID="FormView1" runat="server" DataSourceID="SelectdCfg" DataKeyNames="CfgID">
                                    <EditItemTemplate>
                                         <table class="col-lg-12">
                                                <tr>
                                                    <th>配置式名称</th><td><asp:DynamicControl ID="CfgNameDynamicControl" runat="server" DataField="CfgName" Mode="Edit" /></td>
                                                    <th>站点名称</th><td<asp:DynamicControl ID="SiteNameDynamicControl" runat="server" DataField="SiteName" Mode="Edit" /></td>
                                                    <th>仅登录使用并开放注册</th><td><asp:DynamicControl ID="UserRegEnabDynamicControl" runat="server" DataField="UserRegEnab" Mode="Edit" /></td>
                                                </tr>
                                                <tr>
                                                    <th>欢迎词</th><td><asp:DynamicControl ID="SiteWelcomeWordDynamicControl" runat="server" DataField="SiteWelcomeWord" Mode="Edit" /></td>
                                                    <th>版权信息</th><td><asp:DynamicControl ID="SiteCopyRightStrDynamicControl" runat="server" DataField="SiteCopyRightStr" Mode="Edit" /></td>
                                                    <th>联系信息</th><td><asp:DynamicControl ID="SiteContactStrDynamicControl" runat="server" DataField="SiteContactStr" Mode="Edit" /></td>
                                                </tr>
                                        </table>
                                        CfgName:
                                        CfgCreator:
                                        <asp:DynamicControl ID="CfgCreatorDynamicControl" runat="server" DataField="CfgCreator" Mode="Edit" />
                                        <br />
                                        CfgCrtTime:
                                        <asp:DynamicControl ID="CfgCrtTimeDynamicControl" runat="server" DataField="CfgCrtTime" Mode="Edit" />
                                        <br />
                                        CfgIsEnable:
                                        <asp:DynamicControl ID="CfgIsEnableDynamicControl" runat="server" DataField="CfgIsEnable" Mode="Edit" />
                                        <br />
                                        SiteEnabTimSt:
                                        <asp:DynamicControl ID="SiteEnabTimStDynamicControl" runat="server" DataField="SiteEnabTimSt" Mode="Edit" />
                                        <br />
                                        SiteEnabTimEd:
                                        <asp:DynamicControl ID="SiteEnabTimEdDynamicControl" runat="server" DataField="SiteEnabTimEd" Mode="Edit" />
                                        <br />
                                        SiteName:
                                        SiteWelcomeWord:
                                        SiteCopyRightStr:
                                        SiteContactStr:
                                        UserRegEnab:
                                        AllowFixTimes:
                                        <asp:DynamicControl ID="AllowFixTimesDynamicControl" runat="server" DataField="AllowFixTimes" Mode="Edit" />
                                        <br />
                                        DataSource:
                                        <asp:DynamicControl ID="DataSourceDynamicControl" runat="server" DataField="DataSource" Mode="Edit" />
                                        <br />
                                        DataTable:
                                        <asp:DynamicControl ID="DataTableDynamicControl" runat="server" DataField="DataTable" Mode="Edit" />
                                        <br />
                                        SouColReDef:
                                        <asp:DynamicControl ID="SouColReDefDynamicControl" runat="server" DataField="SouColReDef" Mode="Edit" />
                                        <br />
                                        SouRowRangeStart:
                                        <asp:DynamicControl ID="SouRowRangeStartDynamicControl" runat="server" DataField="SouRowRangeStart" Mode="Edit" />
                                        <br />
                                        SouRowRangeEnd:
                                        <asp:DynamicControl ID="SouRowRangeEndDynamicControl" runat="server" DataField="SouRowRangeEnd" Mode="Edit" />
                                        <br />
                                        QueryMeth:
                                        <asp:DynamicControl ID="QueryMethDynamicControl" runat="server" DataField="QueryMeth" Mode="Edit" />
                                        <br />
                                        QueryMethRef:
                                        <asp:DynamicControl ID="QueryMethRefDynamicControl" runat="server" DataField="QueryMethRef" Mode="Edit" />
                                        <br />
                                        SouEntNum:
                                        <asp:DynamicControl ID="SouEntNumDynamicControl" runat="server" DataField="SouEntNum" Mode="Edit" />
                                        <br />
                                        FixEntNum:
                                        <asp:DynamicControl ID="FixEntNumDynamicControl" runat="server" DataField="FixEntNum" Mode="Edit" />
                                        <br />
                                        <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="更新" ValidationGroup="Insert" />
                                        &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        CfgName:
                                        <asp:DynamicControl ID="CfgNameDynamicControl" runat="server" DataField="CfgName" Mode="Insert" ValidationGroup="Insert" />
                                        <br />
                                        CfgCreator:
                                        <asp:DynamicControl ID="CfgCreatorDynamicControl" runat="server" DataField="CfgCreator" Mode="Insert" ValidationGroup="Insert" />
                                        <br />
                                        CfgCrtTime:
                                        <asp:DynamicControl ID="CfgCrtTimeDynamicControl" runat="server" DataField="CfgCrtTime" Mode="Insert" ValidationGroup="Insert" />
                                        <br />
                                        CfgIsEnable:
                                        <asp:DynamicControl ID="CfgIsEnableDynamicControl" runat="server" DataField="CfgIsEnable" Mode="Insert" ValidationGroup="Insert" />
                                        <br />
                                        SiteEnabTimSt:
                                        <asp:DynamicControl ID="SiteEnabTimStDynamicControl" runat="server" DataField="SiteEnabTimSt" Mode="Insert" ValidationGroup="Insert" />
                                        <br />
                                        SiteEnabTimEd:
                                        <asp:DynamicControl ID="SiteEnabTimEdDynamicControl" runat="server" DataField="SiteEnabTimEd" Mode="Insert" ValidationGroup="Insert" />
                                        <br />
                                        SiteName:
                                        <asp:DynamicControl ID="SiteNameDynamicControl" runat="server" DataField="SiteName" Mode="Insert" ValidationGroup="Insert" />
                                        <br />
                                        SiteWelcomeWord:
                                        <asp:DynamicControl ID="SiteWelcomeWordDynamicControl" runat="server" DataField="SiteWelcomeWord" Mode="Insert" ValidationGroup="Insert" />
                                        <br />
                                        SiteCopyRightStr:
                                        <asp:DynamicControl ID="SiteCopyRightStrDynamicControl" runat="server" DataField="SiteCopyRightStr" Mode="Insert" ValidationGroup="Insert" />
                                        <br />
                                        SiteContactStr:
                                        <asp:DynamicControl ID="SiteContactStrDynamicControl" runat="server" DataField="SiteContactStr" Mode="Insert" ValidationGroup="Insert" />
                                        <br />
                                        UserRegEnab:
                                        <asp:DynamicControl ID="UserRegEnabDynamicControl" runat="server" DataField="UserRegEnab" Mode="Insert" ValidationGroup="Insert" />
                                        <br />
                                        AllowFixTimes:
                                        <asp:DynamicControl ID="AllowFixTimesDynamicControl" runat="server" DataField="AllowFixTimes" Mode="Insert" ValidationGroup="Insert" />
                                        <br />
                                        DataSource:
                                        <asp:DynamicControl ID="DataSourceDynamicControl" runat="server" DataField="DataSource" Mode="Insert" ValidationGroup="Insert" />
                                        <br />
                                        DataTable:
                                        <asp:DynamicControl ID="DataTableDynamicControl" runat="server" DataField="DataTable" Mode="Insert" ValidationGroup="Insert" />
                                        <br />
                                        SouColReDef:
                                        <asp:DynamicControl ID="SouColReDefDynamicControl" runat="server" DataField="SouColReDef" Mode="Insert" ValidationGroup="Insert" />
                                        <br />
                                        SouRowRangeStart:
                                        <asp:DynamicControl ID="SouRowRangeStartDynamicControl" runat="server" DataField="SouRowRangeStart" Mode="Insert" ValidationGroup="Insert" />
                                        <br />
                                        SouRowRangeEnd:
                                        <asp:DynamicControl ID="SouRowRangeEndDynamicControl" runat="server" DataField="SouRowRangeEnd" Mode="Insert" ValidationGroup="Insert" />
                                        <br />
                                        QueryMeth:
                                        <asp:DynamicControl ID="QueryMethDynamicControl" runat="server" DataField="QueryMeth" Mode="Insert" ValidationGroup="Insert" />
                                        <br />
                                        QueryMethRef:
                                        <asp:DynamicControl ID="QueryMethRefDynamicControl" runat="server" DataField="QueryMethRef" Mode="Insert" ValidationGroup="Insert" />
                                        <br />
                                        SouEntNum:
                                        <asp:DynamicControl ID="SouEntNumDynamicControl" runat="server" DataField="SouEntNum" Mode="Insert" ValidationGroup="Insert" />
                                        <br />
                                        FixEntNum:
                                        <asp:DynamicControl ID="FixEntNumDynamicControl" runat="server" DataField="FixEntNum" Mode="Insert" ValidationGroup="Insert" />
                                        <br />
                                        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="插入" ValidationGroup="Insert" />
                                        &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消" />
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        CfgID:
                                        <asp:DynamicControl ID="CfgIDDynamicControl" runat="server" DataField="CfgID" Mode="ReadOnly" />
                                        <br />
                                        CfgName:
                                        <asp:DynamicControl ID="CfgNameDynamicControl" runat="server" DataField="CfgName" Mode="ReadOnly" />
                                        <br />
                                        CfgCreator:
                                        <asp:DynamicControl ID="CfgCreatorDynamicControl" runat="server" DataField="CfgCreator" Mode="ReadOnly" />
                                        <br />
                                        CfgCrtTime:
                                        <asp:DynamicControl ID="CfgCrtTimeDynamicControl" runat="server" DataField="CfgCrtTime" Mode="ReadOnly" />
                                        <br />
                                        CfgIsEnable:
                                        <asp:DynamicControl ID="CfgIsEnableDynamicControl" runat="server" DataField="CfgIsEnable" Mode="ReadOnly" />
                                        <br />
                                        SiteEnabTimSt:
                                        <asp:DynamicControl ID="SiteEnabTimStDynamicControl" runat="server" DataField="SiteEnabTimSt" Mode="ReadOnly" />
                                        <br />
                                        SiteEnabTimEd:
                                        <asp:DynamicControl ID="SiteEnabTimEdDynamicControl" runat="server" DataField="SiteEnabTimEd" Mode="ReadOnly" />
                                        <br />
                                        SiteName:
                                        <asp:DynamicControl ID="SiteNameDynamicControl" runat="server" DataField="SiteName" Mode="ReadOnly" />
                                        <br />
                                        SiteWelcomeWord:
                                        <asp:DynamicControl ID="SiteWelcomeWordDynamicControl" runat="server" DataField="SiteWelcomeWord" Mode="ReadOnly" />
                                        <br />
                                        SiteCopyRightStr:
                                        <asp:DynamicControl ID="SiteCopyRightStrDynamicControl" runat="server" DataField="SiteCopyRightStr" Mode="ReadOnly" />
                                        <br />
                                        SiteContactStr:
                                        <asp:DynamicControl ID="SiteContactStrDynamicControl" runat="server" DataField="SiteContactStr" Mode="ReadOnly" />
                                        <br />
                                        UserRegEnab:
                                        <asp:DynamicControl ID="UserRegEnabDynamicControl" runat="server" DataField="UserRegEnab" Mode="ReadOnly" />
                                        <br />
                                        AllowFixTimes:
                                        <asp:DynamicControl ID="AllowFixTimesDynamicControl" runat="server" DataField="AllowFixTimes" Mode="ReadOnly" />
                                        <br />
                                        DataSource:
                                        <asp:DynamicControl ID="DataSourceDynamicControl" runat="server" DataField="DataSource" Mode="ReadOnly" />
                                        <br />
                                        DataTable:
                                        <asp:DynamicControl ID="DataTableDynamicControl" runat="server" DataField="DataTable" Mode="ReadOnly" />
                                        <br />
                                        SouColReDef:
                                        <asp:DynamicControl ID="SouColReDefDynamicControl" runat="server" DataField="SouColReDef" Mode="ReadOnly" />
                                        <br />
                                        SouRowRangeStart:
                                        <asp:DynamicControl ID="SouRowRangeStartDynamicControl" runat="server" DataField="SouRowRangeStart" Mode="ReadOnly" />
                                        <br />
                                        SouRowRangeEnd:
                                        <asp:DynamicControl ID="SouRowRangeEndDynamicControl" runat="server" DataField="SouRowRangeEnd" Mode="ReadOnly" />
                                        <br />
                                        QueryMeth:
                                        <asp:DynamicControl ID="QueryMethDynamicControl" runat="server" DataField="QueryMeth" Mode="ReadOnly" />
                                        <br />
                                        QueryMethRef:
                                        <asp:DynamicControl ID="QueryMethRefDynamicControl" runat="server" DataField="QueryMethRef" Mode="ReadOnly" />
                                        <br />
                                        SouEntNum:
                                        <asp:DynamicControl ID="SouEntNumDynamicControl" runat="server" DataField="SouEntNum" Mode="ReadOnly" />
                                        <br />
                                        FixEntNum:
                                        <asp:DynamicControl ID="FixEntNumDynamicControl" runat="server" DataField="FixEntNum" Mode="ReadOnly" />
                                        <br />
                                        <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="编辑" />
                                        &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="删除" />
                                        &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="新建" />
                                    </ItemTemplate>
                            </asp:FormView>
                            <asp:SqlDataSource ID="SelectdCfg" runat="server" ConnectionString="<%$ ConnectionStrings:SiteContext %>" DeleteCommand="DELETE FROM [SiteSettings] WHERE [CfgID] = @original_CfgID" InsertCommand="INSERT INTO [SiteSettings] ([CfgName], [CfgCreator], [CfgCrtTime], [CfgIsEnable], [SiteEnabTimSt], [SiteEnabTimEd], [SiteName], [SiteWelcomeWord], [SiteCopyRightStr], [SiteContactStr], [UserRegEnab], [AllowFixTimes], [DataSource], [DataTable], [SouColReDef], [SouRowRangeStart], [SouRowRangeEnd], [QueryMeth], [QueryMethRef], [SouEntNum], [FixEntNum]) VALUES (@CfgName, @CfgCreator, @CfgCrtTime, @CfgIsEnable, @SiteEnabTimSt, @SiteEnabTimEd, @SiteName, @SiteWelcomeWord, @SiteCopyRightStr, @SiteContactStr, @UserRegEnab, @AllowFixTimes, @DataSource, @DataTable, @SouColReDef, @SouRowRangeStart, @SouRowRangeEnd, @QueryMeth, @QueryMethRef, @SouEntNum, @FixEntNum)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [SiteSettings] WHERE ([CfgID] = @CfgID)" UpdateCommand="UPDATE [SiteSettings] SET [CfgName] = @CfgName, [CfgCreator] = @CfgCreator, [CfgCrtTime] = @CfgCrtTime, [CfgIsEnable] = @CfgIsEnable, [SiteEnabTimSt] = @SiteEnabTimSt, [SiteEnabTimEd] = @SiteEnabTimEd, [SiteName] = @SiteName, [SiteWelcomeWord] = @SiteWelcomeWord, [SiteCopyRightStr] = @SiteCopyRightStr, [SiteContactStr] = @SiteContactStr, [UserRegEnab] = @UserRegEnab, [AllowFixTimes] = @AllowFixTimes, [DataSource] = @DataSource, [DataTable] = @DataTable, [SouColReDef] = @SouColReDef, [SouRowRangeStart] = @SouRowRangeStart, [SouRowRangeEnd] = @SouRowRangeEnd, [QueryMeth] = @QueryMeth, [QueryMethRef] = @QueryMethRef, [SouEntNum] = @SouEntNum, [FixEntNum] = @FixEntNum WHERE [CfgID] = @original_CfgID">
                                <DeleteParameters>
                                    <asp:Parameter Name="original_CfgID" Type="Int32" />
                                </DeleteParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="CfgName" Type="String" />
                                    <asp:Parameter Name="CfgCreator" Type="String" />
                                    <asp:Parameter Name="CfgCrtTime" Type="DateTime" />
                                    <asp:Parameter Name="CfgIsEnable" Type="Boolean" />
                                    <asp:Parameter Name="SiteEnabTimSt" Type="DateTime" />
                                    <asp:Parameter Name="SiteEnabTimEd" Type="DateTime" />
                                    <asp:Parameter Name="SiteName" Type="String" />
                                    <asp:Parameter Name="SiteWelcomeWord" Type="String" />
                                    <asp:Parameter Name="SiteCopyRightStr" Type="String" />
                                    <asp:Parameter Name="SiteContactStr" Type="String" />
                                    <asp:Parameter Name="UserRegEnab" Type="Boolean" />
                                    <asp:Parameter Name="AllowFixTimes" Type="Int32" />
                                    <asp:Parameter Name="DataSource" Type="String" />
                                    <asp:Parameter Name="DataTable" Type="String" />
                                    <asp:Parameter Name="SouColReDef" Type="String" />
                                    <asp:Parameter Name="SouRowRangeStart" Type="Int32" />
                                    <asp:Parameter Name="SouRowRangeEnd" Type="Int32" />
                                    <asp:Parameter Name="QueryMeth" Type="String" />
                                    <asp:Parameter Name="QueryMethRef" Type="String" />
                                    <asp:Parameter Name="SouEntNum" Type="Decimal" />
                                    <asp:Parameter Name="FixEntNum" Type="Decimal" />
                                </InsertParameters>
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ListView1" Name="CfgID" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="CfgName" Type="String" />
                                    <asp:Parameter Name="CfgCreator" Type="String" />
                                    <asp:Parameter Name="CfgCrtTime" Type="DateTime" />
                                    <asp:Parameter Name="CfgIsEnable" Type="Boolean" />
                                    <asp:Parameter Name="SiteEnabTimSt" Type="DateTime" />
                                    <asp:Parameter Name="SiteEnabTimEd" Type="DateTime" />
                                    <asp:Parameter Name="SiteName" Type="String" />
                                    <asp:Parameter Name="SiteWelcomeWord" Type="String" />
                                    <asp:Parameter Name="SiteCopyRightStr" Type="String" />
                                    <asp:Parameter Name="SiteContactStr" Type="String" />
                                    <asp:Parameter Name="UserRegEnab" Type="Boolean" />
                                    <asp:Parameter Name="AllowFixTimes" Type="Int32" />
                                    <asp:Parameter Name="DataSource" Type="String" />
                                    <asp:Parameter Name="DataTable" Type="String" />
                                    <asp:Parameter Name="SouColReDef" Type="String" />
                                    <asp:Parameter Name="SouRowRangeStart" Type="Int32" />
                                    <asp:Parameter Name="SouRowRangeEnd" Type="Int32" />
                                    <asp:Parameter Name="QueryMeth" Type="String" />
                                    <asp:Parameter Name="QueryMethRef" Type="String" />
                                    <asp:Parameter Name="SouEntNum" Type="Decimal" />
                                    <asp:Parameter Name="FixEntNum" Type="Decimal" />
                                    <asp:Parameter Name="original_CfgID" Type="Int32" />
                                </UpdateParameters>
                            </asp:SqlDataSource>

                           
                        </div>
                        <div class="panel-footer text-right">
                            <asp:LinkButton ID="CfgSave" runat="server" type="button" class="btn btn-warning">保存</asp:LinkButton>
                            <asp:LinkButton ID="CfgSaveNew" runat="server" type="button" class="btn btn-warning">存为新的</asp:LinkButton>
                            <asp:LinkButton ID="CancelEdit" runat="server" type="button" class="btn btn-primary" OnClick="CancelEdit_Click">取消</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
