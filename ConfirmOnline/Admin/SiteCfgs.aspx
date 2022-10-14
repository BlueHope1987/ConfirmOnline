<%@ Page Title="管理配置式和全局设定" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SiteCfgs.aspx.cs" Inherits="ConfirmOnline.Admin.SiteCfgs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Container" runat="server">
        <div class="row " style="height: 20px;"></div>
        <div class="row ">
            <div class="col-md-3">
                <ul class="nav nav-pills nav-stacked">
                    <li><a href="AdminPage">Home</a></li>
                    <li class="active"><a href="#">全局设定</a></li>
                    <li><a href="WorkTable">工作表</a></li>
                    <li><a href="Editflow">编辑纪录</a></li>
                    <li><a href="Users">用户</a></li>
                </ul>
            </div>


            <asp:SqlDataSource ID="GetCfgList" runat="server" ConnectionString="<%$ ConnectionStrings:SiteContext %>" DeleteCommand="DELETE FROM [SiteSettings] WHERE [CfgID] = @CfgID" InsertCommand="INSERT INTO [SiteSettings] ([CfgName], [CfgCreator], [CfgCrtTime], [CfgIsEnable]) VALUES (@CfgName, @CfgCreator, @CfgCrtTime, @CfgIsEnable)" SelectCommand="SELECT [CfgID], [CfgName], [CfgCreator], [CfgCrtTime], [CfgIsEnable] FROM [SiteSettings]" UpdateCommand="UPDATE [SiteSettings] SET [CfgName] = @CfgName, [CfgCreator] = @CfgCreator, [CfgCrtTime] = @CfgCrtTime, [CfgIsEnable] = @CfgIsEnable WHERE [CfgID] = @CfgID">
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

            <asp:SqlDataSource ID="SelectdCfg" runat="server" ConnectionString="<%$ ConnectionStrings:SiteContext %>" DeleteCommand="DELETE FROM [SiteSettings] WHERE [CfgID] = @original_CfgID" InsertCommand="INSERT INTO [SiteSettings] ([CfgName], [CfgCreator], [CfgCrtTime], [CfgIsEnable], [SiteEnabTimSt], [SiteEnabTimEd], [SiteName], [SiteWelcomeWord], [SiteCopyRightStr], [SiteContactStr], [UserRegEnab], [AllowFixTimes], [DataSource], [DataTable], [SouColReDef], [SouRowRangeStart], [SouRowRangeEnd], [QueryMeth], [QueryMethRef], [SouEntNum], [FixEntNum]) VALUES (@CfgName, @CfgCreator, @CfgCrtTime, @CfgIsEnable, @SiteEnabTimSt, @SiteEnabTimEd, @SiteName, @SiteWelcomeWord, @SiteCopyRightStr, @SiteContactStr, @UserRegEnab, @AllowFixTimes, @DataSource, @DataTable, @SouColReDef, @SouRowRangeStart, @SouRowRangeEnd, @QueryMeth, @QueryMethRef, @SouEntNum, @FixEntNum)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [SiteSettings] WHERE ([CfgID] = @CfgID)" UpdateCommand="UPDATE [SiteSettings] SET [CfgName] = @CfgName, [SiteEnabTimSt] = @SiteEnabTimSt, [SiteEnabTimEd] = @SiteEnabTimEd, [SiteName] = @SiteName, [SiteWelcomeWord] = @SiteWelcomeWord, [SiteCopyRightStr] = @SiteCopyRightStr, [SiteContactStr] = @SiteContactStr, [UserRegEnab] = @UserRegEnab, [AllowFixTimes] = @AllowFixTimes, [DataSource] = @DataSource, [DataTable] = @DataTable, [SouColReDef] = @SouColReDef, [SouRowRangeStart] = @SouRowRangeStart, [SouRowRangeEnd] = @SouRowRangeEnd, [QueryMeth] = @QueryMeth, [QueryMethRef] = @QueryMethRef WHERE [CfgID] = @original_CfgID">
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
                    <asp:Parameter Name="original_CfgID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>

            <div class="col-md-9">
                <h3>配置式和全局设定</h3>
                <p>配置式是一整套设置，控制网站的全局设定、对应处理的核对表格及查询方法等。不同的核对表格运行在不同的配置式下。</p>
                <div id="CfgList" runat="server" class="row">
                    <asp:ListView ID="ListView1" runat="server" DataKeyNames="CfgID" DataSourceID="GetCfgList" OnItemDataBound="ListView1_ItemDataBound">
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
                                    <asp:LinkButton ID="CfgDoEnable" CommandArgument='<%#Eval("CfgID")%>' OnClick="BtnCfgEnable_Click" runat="server">启用</asp:LinkButton>
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
                                    <asp:LinkButton ID="CfgDoEnable" CommandArgument='<%#Eval("CfgID")%>' OnClick="BtnCfgEnable_Click" runat="server">启用</asp:LinkButton>
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
                                    <asp:LinkButton ID="CfgDoEnable" CommandArgument='<%#Eval("CfgID")%>' OnClick="BtnCfgEnable_Click" runat="server">启用</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="BtnEdit" runat="server" CommandName="Select" OnClick="BtnEdit_Click">编辑</asp:LinkButton>
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                </div>
                <div id="OpsAre" runat="server" class="row">
                    <asp:LinkButton ID="BtnNew" runat="server" OnClick="BtnNew_Click" type="button" class="btn btn-warning">新建配置式</asp:LinkButton>
                </div>



                <div id="EditAre" runat="server" class="row" visible="false">
                    <div class="panel panel-warning row">
                        <div class="panel-heading">
                            <h3 id="EditAreTitle" runat="server" class="panel-title">面板标题</h3>
                        </div>


                        <asp:FormView ID="CfgEditForm" runat="server" DataKeyNames="CfgID" DataSourceID="SelectdCfg" DefaultMode="Edit" EnableViewState="True">
                            <EditItemTemplate>

                                <div class="panel-body">
                                    <table width="100%">
                                                <tr height="40px">
                                                    <th>配置式名称</th><td><asp:TextBox ID="CfgNameTextBox" runat="server" Text='<%# Bind("CfgName") %>' CssClass="form-control input-sm" /></td>
                                                    <th>站点名称</th><td><asp:TextBox ID="SiteNameTextBox" runat="server" Text='<%# Bind("SiteName") %>' CssClass="form-control input-sm"  /></td>
                                                    <th>仅登录使用并开放注册</th><td><asp:CheckBox ID="UserRegEnabCheckBox" runat="server" Checked='<%# Bind("UserRegEnab") %>' /></td>
                                                </tr>
                                                <tr height="40px">
                                                    <th>欢迎词</th><td><asp:TextBox ID="SiteWelcomeWordTextBox" runat="server" Text='<%# Bind("SiteWelcomeWord") %>' CssClass="form-control input-sm"  /></td>
                                                    <th>版权信息</th><td><asp:TextBox ID="SiteCopyRightStrTextBox" runat="server" Text='<%# Bind("SiteCopyRightStr") %>' CssClass="form-control input-sm"  /></td>
                                                    <th>联系信息</th><td><asp:TextBox ID="SiteContactStrTextBox" runat="server" Text='<%# Bind("SiteContactStr") %>' CssClass="form-control input-sm"  /></td>
                                                </tr>
                                                <tr height="40px">
                                                    <th>开放时间</th><td><asp:TextBox ID="SiteEnabTimStTextBox" runat="server" Text='<%# Bind("SiteEnabTimSt") %>' CssClass="form-control input-sm"  /></td>
                                                    <th>停止时间</th><td><asp:TextBox ID="SiteEnabTimEdTextBox" runat="server" Text='<%# Bind("SiteEnabTimEd") %>' CssClass="form-control input-sm"  /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6"><div class="alert alert-info" style="padding: 5px; margin-top: 10px;">以下项目有不太清楚的，可使用<b><a href="javascript:__doPostBack('startGuide','')" >我们提供的生成向导</a></b>，并进行文件管理和工作表浏览。</div></td>
                                                </tr>
                                                <tr height="40px">
                                                    <th>Excel文件</th><td><asp:TextBox ID="DataSourceTextBox" runat="server" Text='<%# Bind("DataSource") %>' OnClick="DataSource_Click" CssClass="form-control input-sm"  /></td>
                                                    <th>Excel工作表</th><td><asp:TextBox ID="DataTableTextBox" runat="server" Text='<%# Bind("DataTable") %>' OnClick="DataSource_Click" CssClass="form-control input-sm"  /></td>
                                                    <th>允许的核实修订次数</th><td><asp:TextBox ID="AllowFixTimesTextBox" runat="server" Text='<%# Bind("AllowFixTimes") %>' CssClass="form-control input-sm"  /></td>
                                                </tr>
                                                <tr height="40px">
                                                    <th>涉及列</th><td><asp:TextBox ID="SouColReDefTextBox" runat="server" Text='<%# Bind("SouColReDef") %>' CssClass="form-control input-sm"  /></td>
                                                    <th>查询列</th><td><asp:TextBox ID="QueryMethTextBox" runat="server" Text='<%# Bind("QueryMeth") %>' CssClass="form-control input-sm"  /></td>
                                                    <th>查询参考</th><td><asp:TextBox ID="QueryMethRefTextBox" runat="server" Text='<%# Bind("QueryMethRef") %>' CssClass="form-control input-sm"  /></td>
                                                </tr>
                                                <tr height="40px">
                                                    <th>起始行</th><td><asp:TextBox ID="SouRowRangeStartTextBox" runat="server" Text='<%# Bind("SouRowRangeStart") %>' CssClass="form-control input-sm"  /></td>
                                                    <th>结束行</th><td> <asp:TextBox ID="SouRowRangeEndTextBox" runat="server" Text='<%# Bind("SouRowRangeEnd") %>' CssClass="form-control input-sm"  /></td>
                                                    <th></th><td></td>
                                                </tr>
                                        </table>
                                    </div>
                                    
                            </EditItemTemplate>
                            <InsertItemTemplate>

                                <div class="panel-body">
                                    <table width="100%">
                                                <tr height="40px">
                                                    <th>配置式名称</th><td><asp:TextBox ID="CfgNameTextBox" runat="server" Text='<%# Bind("CfgName") %>' CssClass="form-control input-sm" /></td>
                                                    <th>站点名称</th><td><asp:TextBox ID="SiteNameTextBox" runat="server" Text='<%# Bind("SiteName") %>' CssClass="form-control input-sm"  /></td>
                                                    <th>仅登录使用并开放注册</th><td><asp:CheckBox ID="UserRegEnabCheckBox" runat="server" Checked='<%# Bind("UserRegEnab") %>' /></td>
                                                </tr>
                                                <tr height="40px">
                                                    <th>欢迎词</th><td><asp:TextBox ID="SiteWelcomeWordTextBox" runat="server" Text='<%# Bind("SiteWelcomeWord") %>' CssClass="form-control input-sm"  /></td>
                                                    <th>版权信息</th><td><asp:TextBox ID="SiteCopyRightStrTextBox" runat="server" Text='<%# Bind("SiteCopyRightStr") %>' CssClass="form-control input-sm"  /></td>
                                                    <th>联系信息</th><td><asp:TextBox ID="SiteContactStrTextBox" runat="server" Text='<%# Bind("SiteContactStr") %>' CssClass="form-control input-sm"  /></td>
                                                </tr>
                                                <tr height="40px">
                                                    <th>开放时间</th><td><asp:TextBox ID="SiteEnabTimStTextBox" runat="server" Text='<%# Bind("SiteEnabTimSt") %>' CssClass="form-control input-sm"  /></td>
                                                    <th>停止时间</th><td><asp:TextBox ID="SiteEnabTimEdTextBox" runat="server" Text='<%# Bind("SiteEnabTimEd") %>' CssClass="form-control input-sm"  /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6"><div class="alert alert-info" style="padding: 5px; margin-top: 10px;">以下项目有不太清楚的，可使用<b><a href="javascript:__doPostBack('startGuide','')" >我们提供的生成向导</a></b>，并进行文件管理和工作表浏览。</div></td>
                                                </tr>
                                                <tr height="40px">
                                                    <th>Excel文件</th><td><asp:TextBox ID="DataSourceTextBox" runat="server" Text='<%# Bind("DataSource") %>' OnClick="DataSource_Click" CssClass="form-control input-sm"  /></td>
                                                    <th>Excel工作表</th><td><asp:TextBox ID="DataTableTextBox" runat="server" Text='<%# Bind("DataTable") %>' OnClick="DataSource_Click" CssClass="form-control input-sm"  /></td>
                                                    <th>允许的核实修订次数</th><td><asp:TextBox ID="AllowFixTimesTextBox" runat="server" Text='<%# Bind("AllowFixTimes") %>' CssClass="form-control input-sm"  /></td>
                                                </tr>
                                                <tr height="40px">
                                                    <th>涉及列</th><td><asp:TextBox ID="SouColReDefTextBox" runat="server" Text='<%# Bind("SouColReDef") %>' CssClass="form-control input-sm"  /></td>
                                                    <th>查询列</th><td><asp:TextBox ID="QueryMethTextBox" runat="server" Text='<%# Bind("QueryMeth") %>' CssClass="form-control input-sm"  /></td>
                                                    <th>查询参考</th><td><asp:TextBox ID="QueryMethRefTextBox" runat="server" Text='<%# Bind("QueryMethRef") %>' CssClass="form-control input-sm"  /></td>
                                                </tr>
                                                <tr height="40px">
                                                    <th>起始行</th><td><asp:TextBox ID="SouRowRangeStartTextBox" runat="server" Text='<%# Bind("SouRowRangeStart") %>' CssClass="form-control input-sm"  /></td>
                                                    <th>结束行</th><td> <asp:TextBox ID="SouRowRangeEndTextBox" runat="server" Text='<%# Bind("SouRowRangeEnd") %>' CssClass="form-control input-sm"  /></td>
                                                    <th></th><td></td>
                                                </tr>
                                        </table>
                                </div>

                            </InsertItemTemplate>
                        </asp:FormView>

                        <div class="panel-footer text-right">
                            <asp:LinkButton ID="CfgSave" runat="server" type="button" class="btn btn-warning" OnClick="CfgSave_Click">保存</asp:LinkButton>
                            <asp:LinkButton ID="CfgSaveNew" runat="server" type="button" class="btn btn-warning" OnClick="CfgSaveNew_Click">存为新的</asp:LinkButton>
                            <asp:LinkButton ID="CfgDelete" runat="server" type="button" class="btn btn-danger" OnClick="CfgDelete_Click">删除这个</asp:LinkButton>
                            <asp:LinkButton ID="CancelEdit" runat="server" type="button" class="btn btn-primary" OnClick="CancelEdit_Click">取消</asp:LinkButton>
                        </div>
                    </div>
                    <div runat="server" class="panel panel-warning row" id="GuideDiv" visible="False">
                        <div class="panel-heading">
                            <h3 id="H1" runat="server" class="panel-title">表格选择</h3>
                        </div>
                        <div class="panel-body" style="background-color: aliceblue;">
                            <table>
                                <tr class="row" style="vertical-align:top;">
                                    <td class="col-md-4" style="background-color:lightblue;padding-left:5px;padding-right:0px;vertical-align:middle;">
                                        <asp:ListBox ID="FileList" runat="server" OnPreRender="FileList_PreRender" CssClass="form-select col-md-12" Rows="8" Height="290px"></asp:ListBox>
                                    </td>
                                    <td class="col-md-1" style="border-top-right-radius: 10px;border-bottom-right-radius: 10px;padding: 0px;padding-top: 5px;background-color: lightblue;text-align: center;">
                                        <div class="btn-group-vertical">
                                            <asp:LinkButton ID="FleSelect" runat="server" type="button" class="btn btn-info btn-sm disabled" OnClick="FleSelect_Click">访问</asp:LinkButton>
                                            <a id="FleUploadBtn" type="button" class="btn btn-warning btn-sm">上传</a>
                                            <asp:LinkButton ID="FleDelete" runat="server" type="button" class="btn btn-warning btn-sm disabled" OnClick="FleDelete_Click">删除</asp:LinkButton>
                                        </div>
                                    </td>
                                    <td class="col-md-7" style="padding-left:10px; padding-right:2px; font-size: 10px;">
                                        <asp:DropDownList ID="WorkTableSelect" runat="server" CssClass="col-md-12" AutoPostBack="True" OnSelectedIndexChanged="WorkTableSelect_SelectedIndexChanged" Height="20px" Visible="False"></asp:DropDownList>
                                        <div id="WorkTablePvBox" Class="col-md-12" style="width: 600px; height: 300px; margin-top: 2px; padding: 2px; overflow:auto;" runat="server">
                                        </div>
                                    </td>
                                </tr>
                            </table>

                            <script type="text/javascript">
                                $('[id$=FileList]').change(function (){
                                    $('[id$=FleSelect]').removeClass("disabled");
                                    $('[id$=FleDelete]').removeClass("disabled");
                                });
                                $('[id$=FileList]').children("option").addClass("glyphicon glyphicon-calendar");
                                $('[id$=FileList]').children("option").css('display', 'block');
                                $('#FleUploadBtn').after('<input type="file" id="fileLoad" name="file" accept=".xls, .xlsx" multiple style="display:none" onchange="fleUpload()">');
                                $('#FleUploadBtn').click(function () { $('#fileLoad').click(); });
                                function fleUpload() {
                                    __doPostBack('UploadFiles', $('#fileLoad').val());
                                }

                                /*
                                //尝试改变选中项背景色 不好用
                                $(function () {
                                    $('[id$=FileList]').css("", "transparent");
                                    $('[id$=FileList]').children("option").css("color", "lightblue");
                                    $('[id$=FileList]').change(function () {
                                        var selItem = $(this).val();
                                        if (selItem == $(this).find('option:first').val()) {
                                            $(this).css("color", "transparent");
                                        } else {
                                            $(this).css("color", "lightblue");
                                        }
                                    });
                                })
                                */
                            </script>
                        </div>
                        <div class="panel-footer text-right">
                            <asp:LinkButton ID="BtnGuideFin" runat="server" type="button" class="btn btn-primary" OnClick="BtnGuideFin_Click">关闭向导</asp:LinkButton>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
