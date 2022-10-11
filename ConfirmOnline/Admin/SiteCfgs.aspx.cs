using ConfirmOnline.Models;
using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ConfirmOnline.Logic;
using System.Collections.Generic;
using System.Data;

namespace ConfirmOnline.Admin
{
    public partial class SiteCfgs : System.Web.UI.Page
    {
        int tableOpsStep;//五步数据操作序号
        GridView WorkTablePv;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                switch (Request.Form["__EVENTTARGET"]){
                    case "UploadFiles":
                        //文件上传逻辑
                        //Request.Form["__EVENTARGUMENT"]
                        for (int fn = 0; fn < Request.Files.Count; fn++)
                        {
                            System.Web.HttpPostedFile f = Request.Files[fn];
                            if (f.FileName.ToLower().Contains("xls") || f.FileName.ToLower().Contains("xlsx"))
                                if (!File.Exists(Server.MapPath("~/App_Data/UploadExcels/" + f.FileName.Split('\\').Last())))
                                    f.SaveAs(Server.MapPath("~/App_Data/UploadExcels/" + f.FileName.Split('\\').Last()));
                                else
                                {
                                    int j;
                                    for (j = 2; File.Exists(Server.MapPath("~/App_Data/UploadExcels/" + j.ToString() + "_" + f.FileName.Split('\\').Last())); j++) ;
                                    f.SaveAs(Server.MapPath("~/App_Data/UploadExcels/" + j.ToString() + "_" + f.FileName.Split('\\').Last()));//重名逻辑
                                }
                        }
                        break;

                    case "startGuide":
                        GuideDiv.Visible = true;
                        break;

                    case "datastartrow":
                        Session["tmpdatastartrow"] = int.Parse(Request.Form["__EVENTARGUMENT"]);
                        Session["tmpworkcol"] = null;
                        Session["tmpdataheadrow"] = null;
                        Session["tmpcolkey"] = null;
                        Session["tmpcolrename"] = null;
                        tableOpsStep = 1;
                        WorkTableSelect_SelectedIndexChanged(null, null);
                        break;
                    case "addworkcol":
                        if (int.Parse(Request.Form["__EVENTARGUMENT"]) == -1)
                        {
                            tableOpsStep = 2;
                        }
                        else {
                            if (int.Parse(Request.Form["__EVENTARGUMENT"]) >= 0)
                            {
                                if (Session["tmpworkcol"] == null)
                                    Session["tmpworkcol"] = new List<int>();
                                ((List<int>)Session["tmpworkcol"]).Add(int.Parse(Request.Form["__EVENTARGUMENT"]));
                            }
                            tableOpsStep = 1;
                        };
                        WorkTableSelect_SelectedIndexChanged(null, null);
                        break;
                    case "dataheadrow":
                        Session["tmpdataheadrow"] = int.Parse(Request.Form["__EVENTARGUMENT"]);
                        tableOpsStep = 3;
                        WorkTableSelect_SelectedIndexChanged(null, null);
                        break;
                    case "datacolrename":
                        Session["tmpcolrename"] = new List<string>();
                        foreach(int i in (List<int>)Session["tmpworkcol"])
                        {
                            ((List<string>)Session["tmpcolrename"]).Add(i.ToString() + ":" + ((string)Request.Form["tmpdataheadrow-cell"+(i-1)]).Replace(",","&comma&").Replace(":", "&colon&"));//转义逗号冒号
                        }
                        tableOpsStep = 4;
                        WorkTableSelect_SelectedIndexChanged(null, null);
                        break;
                    case "datacolkey":
                        if (Request.Form["__EVENTARGUMENT"] == "")
                        {
                            tableOpsStep = 5;
                        }
                        else
                        {
                            if (int.Parse(Request.Form["__EVENTARGUMENT"]) >= 0)
                            {
                                if (Session["tmpcolkey"] == null)
                                    Session["tmpcolkey"] = new List<int>();
                                ((List<int>)Session["tmpcolkey"]).Add(int.Parse(Request.Form["__EVENTARGUMENT"]));
                            }
                            tableOpsStep = 4;
                        };
                        WorkTableSelect_SelectedIndexChanged(null, null);
                        break;
                    default:

                        break;
                }
            }
            Page.Form.Enctype = "multipart/form-data";
        }
        protected bool GetVisible(object objVal)
        {
            return (bool)objVal;
        }

        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataitem = (ListViewDataItem)e.Item;
                System.Web.UI.WebControls.Label ie = (System.Web.UI.WebControls.Label)dataitem.FindControl("IfEnable");
                System.Web.UI.WebControls.LinkButton de = (System.Web.UI.WebControls.LinkButton)dataitem.FindControl("CfgDoEnable");
                if ((bool)DataBinder.Eval(dataitem.DataItem, "CfgIsEnable"))
                {
                    ie.Visible = true;
                    de.Visible = false;
                }
                else
                {
                    ie.Visible = false;
                    de.Visible = true;
                }
            }
        }

        protected void BtnNew_Click(object sender, EventArgs e)
        {
            OpsAre.Visible = false;
            EditAre.Visible = true;
            CfgSave.Visible = false;
            CfgDelete.Visible = false;
            EditAreTitle.InnerText = "新建配置式";
            CfgEditForm.ChangeMode(FormViewMode.Insert);
        }

        protected void BtnCfgEnable_Click(object sender, EventArgs e)
        {
            LinkButton lbenable = (LinkButton)sender;

            if (lbenable.CommandArgument != "")
            {
                //启用逻辑
                int id = int.Parse(lbenable.CommandArgument);

                SiteContext context = new Models.SiteContext();
                IQueryable<SiteSetting> queryTrue = context.SiteSetting.Where(s => s.CfgIsEnable == true);
                foreach (SiteSetting setting in queryTrue)
                    setting.CfgIsEnable = false;

                IQueryable<SiteSetting> queryID = context.SiteSetting.Where(s => s.CfgID == id);
                queryID.First().CfgIsEnable = true;

                context.SaveChanges();
                Application["SystemSet"] = queryID.First();
                CancelEdit_Click(null, null);
            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            OpsAre.Visible = false;
            EditAre.Visible = true;
            CfgSave.Visible = true;
            EditAreTitle.InnerText = "编辑配置式";
            CfgEditForm.ChangeMode(FormViewMode.Edit);

            SiteContext context = new SiteContext();
            if (context.SiteSetting.Count() > 1)
            {
                CfgDelete.Visible = true;
            }
            else
            {
                CfgDelete.Visible = false;
            }

        }

        protected void CancelEdit_Click(object sender, EventArgs e)
        {
            OpsAre.Visible = true;
            EditAre.Visible = false;
            EditAre.EnableViewState = false;
            ListView1.DataBind();
        }

        protected void CfgSave_Click(object sender, EventArgs e)
        {
            CfgEditForm.UpdateItem(true);
        }

        protected void CfgSaveNew_Click(object sender, EventArgs e)
        {
            FormViewRow row = CfgEditForm.Row;

            SiteSetting ss = new SiteSetting
            {
                AllowFixTimes = int.Parse(((TextBox)(row.FindControl("AllowFixTimesTextBox"))).Text),
                CfgCreator = User.Identity.Name,
                CfgCrtTime = DateTime.Now,
                CfgName = ((TextBox)(row.FindControl("CfgNameTextBox"))).Text,
                DataSource = ((TextBox)(row.FindControl("DataSourceTextBox"))).Text,
                DataTable = ((TextBox)(row.FindControl("DataTableTextBox"))).Text,
                FixEntNum = 0,
                QueryMeth = ((TextBox)(row.FindControl("QueryMethTextBox"))).Text,
                QueryMethRef = ((TextBox)(row.FindControl("QueryMethRefTextBox"))).Text,
                SiteContactStr = ((TextBox)(row.FindControl("SiteContactStrTextBox"))).Text,
                SiteCopyRightStr = ((TextBox)(row.FindControl("SiteCopyRightStrTextBox"))).Text,
                SiteEnabTimEd = DateTime.Parse(((TextBox)(row.FindControl("SiteEnabTimEdTextBox"))).Text),
                SiteEnabTimSt = DateTime.Parse(((TextBox)(row.FindControl("SiteEnabTimStTextBox"))).Text),
                CfgIsEnable = false,
                SiteName = ((TextBox)(row.FindControl("SiteNameTextBox"))).Text,
                SiteWelcomeWord = ((TextBox)(row.FindControl("SiteWelcomeWordTextBox"))).Text,
                SouColReDef = ((TextBox)(row.FindControl("SouColReDefTextBox"))).Text,
                SouEntNum = 0,
                SouRowRangeEnd = int.Parse(((TextBox)(row.FindControl("SouRowRangeEndTextBox"))).Text),
                SouRowRangeStart = int.Parse(((TextBox)(row.FindControl("SouRowRangeStartTextBox"))).Text),
                UserRegEnab = ((CheckBox)(row.FindControl("UserRegEnabCheckBox"))).Checked,
            };

            SiteContext context = new SiteContext();
            ss = context.SiteSetting.Add(ss);
            context.SaveChanges();
            //ListView1.SelectItem(ListView1.Items.IndexOf();
            CancelEdit_Click(null, null);
        }

        protected void CfgDelete_Click(object sender, EventArgs e)
        {
            CfgEditForm.DeleteItem();
            CancelEdit_Click(null, null);
        }

        protected void DataSource_Click(object sender, EventArgs e)
        {

        }

        protected void FileList_PreRender(object sender, EventArgs e)
        {
            string sltitem = FileList.Text;
            DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/App_Data/UploadExcels"));
            FileInfo[] fiArray = di.GetFiles("*.xls*");
            FileList.Items.Clear();
            if (fiArray.Count() > 0)
            {
                foreach(FileInfo fn in fiArray)
                {
                    FileList.Items.Add(fn.Name);
                }
            }
            if(FileList.Items.FindByText(sltitem)!=null)
                FileList.Items.FindByText(sltitem).Selected = true;
        }

        protected void FleDelete_Click(object sender, EventArgs e)
        {
            if (File.Exists(Server.MapPath("~/App_Data/UploadExcels/" + FileList.Text)))
                File.Delete(Server.MapPath("~/App_Data/UploadExcels/" + FileList.Text));
        }

        protected void FileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            FleDelete.CssClass = "btn btn-warning";
        }

        protected void FleSelect_Click(object sender, EventArgs e)
        {
            if (File.Exists(Server.MapPath("~/App_Data/UploadExcels/" + FileList.Text)))
            {
                List<string> fl = new List<string>();
                WorkTableSelect.Items.Clear();
                WorkTableSelect.Visible = true;
                try
                {
                    fl=ExcelVisiter.ToExcelsSheetList(Server.MapPath("~/App_Data/UploadExcels/" + FileList.Text));

                }
                catch
                {
                    fl.Add("枚举工作表失败，请检查文件");
                }

                WorkTableSelect.Items.Add("下拉以开始选择工作表");

                foreach (string s in fl)
                {
                    WorkTableSelect.Items.Add(s);
                }
            }
        }

        protected void WorkTableSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!(WorkTableSelect.Text== "枚举工作表失败，请检查文件" || WorkTableSelect.Text== "下拉以开始选择工作表"))
            {
                ExcelVisiter ev = new ExcelVisiter(Server.MapPath("~/App_Data/UploadExcels/" + FileList.Text), WorkTableSelect.Text);
                WorkTablePv = new GridView();
                WorkTablePv.Style.Add("font-size", "10px");
                WorkTablePv.Style.Add("text-align", "center");
                WorkTablePv.Style.Add("white-space", "nowrap");
                WorkTablePv.CssClass = "table-condensed";
                WorkTablePv.RowDataBound += WorkTablePv_RowDataBound;

                WorkTableSelect.Visible = false;
                System.Web.UI.HtmlControls.HtmlGenericControl p = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
                p.InnerHtml += "<font color=\"Silver\">以下选取 " + FileList.Text + " 文件中 " + WorkTableSelect.Text + " 工作表的前10行。</font><br/>";

                switch (tableOpsStep)
                {
                    case 1:
                        if (Session["tmpworkcol"] != null)
                        {
                            p.InnerHtml += "<span class=\"label label-warning\">(步骤2/5:涉及列)点击标头依次添加参与查询修订的所有列（<a href=\"javascript:__doPostBack('addworkcol','-1')\" > 点这里完成</a>）</span>";
                        }
                        else
                        {
                            p.InnerHtml += "<span class=\"label label-warning\">(步骤2/5:涉及列)点击标头依次添加参与查询修订的所有列（请按照您最终想呈现的顺序点击）</span>";
                        }
                        break;
                    case 2:
                        p.InnerHtml += "<span class=\"label label-warning\">(步骤3/5:套取项目)点击序号选取需要套取的标题行（<a href=\"javascript:__doPostBack('dataheadrow','-1')\" > 没有点这里下一步</a>）</span>";
                        break;
                    case 3:
                        p.InnerHtml += "<span class=\"label label-warning\">(步骤4/5:重设项目)为您选取的各个列更新名称（<a href=\"javascript:__doPostBack('datacolrename','')\" > 完成点这里下一步</a>）</span>";
                        break;
                    case 4:
                        if (Session["tmpcolkey"] != null)
                        {
                            p.InnerHtml += "<span class=\"label label-warning\">(步骤5/5:查询列)选取数个用以访问者检索到唯一记录的列（<a href=\"javascript:__doPostBack('datacolkey','')\" > 点这里完成</a>）</span>";
                        }
                        else
                        {
                            p.InnerHtml += "<span class=\"label label-warning\">(步骤5/5:查询列)选取数个用以访问者检索到唯一记录的列";
                        }                            
                        break;
                    case 5:
                        p.InnerHtml += "<span class=\"label label-warning\">您已完成向导操作生成设定值，点击完成向导按钮返回设置。</span>";
                        BtnGuideFin.Text = "完成向导";
                        BtnGuideFin.CssClass = "btn btn-warning";
                        break;
                    default:
                        p.InnerHtml += "<span class=\"label label-warning\">(步骤1/5:起始行)点击序号选取数据起始行</span>";
                        break;
                }

                p.Style.Add(HtmlTextWriterStyle.Margin, "0");

                WorkTablePvBox.Controls.Add(p);
                WorkTablePvBox.Controls.Add(WorkTablePv);

                WorkTablePv.DataSource=ev.getDataSet("SELECT TOP 10 * FROM [" + WorkTableSelect.Text + "$]");
                WorkTablePv.DataBind();

            }
        }

        private void WorkTablePv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex == -1)
            {
                TableHeaderCell hc = new TableHeaderCell();

                switch (tableOpsStep)
                {
                    case 1:
                        e.Row.BackColor = System.Drawing.Color.AliceBlue;
                        foreach (TableCell tc in e.Row.Cells)
                        {
                            if(Session["tmpworkcol"]!=null)
                            {
                                if (((List<int>)Session["tmpworkcol"]).Exists(x => x == e.Row.Cells.GetCellIndex(tc) + 1))
                                    tc.Text = tc.Text + "（No." + (((List<int>)Session["tmpworkcol"]).IndexOf(e.Row.Cells.GetCellIndex(tc) + 1) + 1) + "）";
                                else
                                    tc.Text = "<a href=\"javascript:__doPostBack('addworkcol','" + (e.Row.Cells.GetCellIndex(tc) + 1) + "')\">" + tc.Text + "</a>";
                            }
                            else
                            {
                                tc.Text = "<a href=\"javascript:__doPostBack('addworkcol','" + (e.Row.Cells.GetCellIndex(tc) + 1) + "')\">" + tc.Text + "</a>";
                            }
                        }
                        break;
                    case 2:
                        hc.Text = "行序号";
                        hc.BackColor = System.Drawing.Color.LightBlue;
                        e.Row.Cells.Add(hc);
                        break;
                    case 3://名称更新
                        e.Row.BackColor = System.Drawing.Color.AliceBlue;
                        if (Session["tmpdataheadrow"] != null)
                            if((int)Session["tmpdataheadrow"] != -1)
                            {
                                foreach (TableCell tc in e.Row.Cells)
                                {
                                    if (((List<int>)Session["tmpworkcol"]).Exists(x => x == e.Row.Cells.GetCellIndex(tc) + 1)) { 
                                        int idx = e.Row.Cells.GetCellIndex(tc);
                                        tc.Text = "<input name=\"tmpdataheadrow-cell" + idx + "\" type=\"text\" value=\"" + (string)((DataSet)WorkTablePv.DataSource).Tables[0].Rows[((int)Session["tmpdataheadrow"]) - 1][idx] + "\" id=name=\"tmpdataheadrow-cell" + idx + "\" style=\"width: 100%;\">";
                                    }
                                }
                            }
                            else
                            {
                                foreach (TableCell tc in e.Row.Cells)
                                {
                                    if (((List<int>)Session["tmpworkcol"]).Exists(x => x == e.Row.Cells.GetCellIndex(tc) + 1))
                                    {
                                        int idx = e.Row.Cells.GetCellIndex(tc);
                                        tc.Text = "<input name=\"tmpdataheadrow-cell" + idx + "\" type=\"text\" value=\"" + tc.Text + "\" id=name=\"tmpdataheadrow-cell" + idx + "\" style=\"width: 100%;\">";
                                    }
                                }
                            }
                                
                        break;
                    case 4:
                        e.Row.BackColor = System.Drawing.Color.AliceBlue;
                        foreach (TableCell tc in e.Row.Cells)
                        {
                            if (((List<int>)Session["tmpworkcol"]).Exists(x => x == e.Row.Cells.GetCellIndex(tc) + 1))
                            {
                                if (Session["tmpcolrename"] != null)
                                {
                                    foreach (string str in ((List<String>)Session["tmpcolrename"]))
                                    {
                                        if (int.Parse(str.Split(':')[0]) - 1 == e.Row.Cells.GetCellIndex(tc))
                                            tc.Text = str.Split(':')[1].Replace("&comma&", ",").Replace("&colon&", ":");//逗号冒号转义
                                    }
                                }

                                if (Session["tmpcolkey"] != null)
                                {
                                    if (((List<int>)Session["tmpcolkey"]).Exists(x => x == e.Row.Cells.GetCellIndex(tc) + 1))
                                        tc.Text = tc.Text + "（索引列）";
                                    else
                                        tc.Text = "<a href=\"javascript:__doPostBack('datacolkey','" + (e.Row.Cells.GetCellIndex(tc) + 1) + "')\">" + tc.Text + "</a>";
                                }
                                else
                                {
                                    tc.Text = "<a href=\"javascript:__doPostBack('datacolkey','" + (e.Row.Cells.GetCellIndex(tc) + 1) + "')\">" + tc.Text + "</a>";
                                }
                            }
                        }
                        break;
                    case 5:
                        e.Row.BackColor = System.Drawing.Color.AliceBlue;
                        foreach (TableCell tc in e.Row.Cells)
                        {
                            if (((List<int>)Session["tmpworkcol"]).Exists(x => x == e.Row.Cells.GetCellIndex(tc) + 1))
                            {
                                if (Session["tmpcolrename"] != null)
                                {
                                    foreach (string str in ((List<String>)Session["tmpcolrename"]))
                                    {
                                        if (int.Parse(str.Split(':')[0]) - 1 == e.Row.Cells.GetCellIndex(tc))
                                            tc.Text = str.Split(':')[1].Replace("&comma&", ",").Replace("&colon&", ":");//逗号冒号转义
                                    }
                                }

                                if (Session["tmpcolkey"] != null)
                                {
                                    if (((List<int>)Session["tmpcolkey"]).Exists(x => x == e.Row.Cells.GetCellIndex(tc) + 1))
                                        tc.Text = "<b>" + tc.Text + "（索引列）" + "</b>";
                                    else
                                        tc.Text = "<b>" + tc.Text + "(工作列)" + "</b>";
                                }
                                else
                                {
                                    tc.Text = "<b>" + tc.Text + "(工作列)"+ "</b>";
                                }
                            }
                        }
                        break;
                    default:
                        hc.Text = "行序号";
                        hc.BackColor = System.Drawing.Color.LightBlue;
                        e.Row.Cells.Add(hc);
                        break;

                }
            }
            else
            {
                TableCell tc = new TableCell();

                switch (tableOpsStep){
                    case 1:
                        break;
                    case 2:
                        tc.Text = "<a href=\"javascript:__doPostBack('dataheadrow','" + (e.Row.RowIndex + 1).ToString() + "')\">" + (e.Row.RowIndex + 1).ToString() + "</a>";
                        tc.BackColor = System.Drawing.Color.PowderBlue;
                        e.Row.Cells.Add(tc);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    default:
                        tc.Text = "<a href=\"javascript:__doPostBack('datastartrow','" + (e.Row.RowIndex + 1).ToString() + "')\">" + (e.Row.RowIndex + 1).ToString() + "</a>";
                        tc.BackColor = System.Drawing.Color.PowderBlue;
                        e.Row.Cells.Add(tc);
                        break;

                }
            }
        }

        protected void BtnGuideFin_Click(object sender, EventArgs e)
        {
            if(BtnGuideFin.CssClass == "btn btn-warning")
            {
                ((TextBox)CfgEditForm.FindControl("SouRowRangeStartTextBox")).Text = ((int)Session["tmpdatastartrow"]).ToString(); //起始行
                Session["tmpdatastartrow"] = null;
                ((TextBox)CfgEditForm.FindControl("QueryMethTextBox")).Text = string.Join(",", ((List<int>)Session["tmpcolkey"])); //查询列
                Session["tmpcolkey"] = null;
                ((TextBox)CfgEditForm.FindControl("SouColReDefTextBox")).Text = string.Join(",", ((List<string>)Session["tmpcolrename"]));//涉及列
                Session["tmpdataheadrow"] = null;
                Session["tmpcolrename"] = null;
                Session["tmpworkcol"] = null;
                ((TextBox)CfgEditForm.FindControl("DataSourceTextBox")).Text = FileList.Text; //excel文件
                ((TextBox)CfgEditForm.FindControl("DataTableTextBox")).Text = WorkTableSelect.Text; //工作表
                BtnGuideFin.CssClass = "btn btn-primary";
                BtnGuideFin.Text = "关闭向导";
            }
            WorkTableSelect.Items.Clear();
            WorkTableSelect.Visible= false;
            GuideDiv.Visible = false;
        }
    }
}