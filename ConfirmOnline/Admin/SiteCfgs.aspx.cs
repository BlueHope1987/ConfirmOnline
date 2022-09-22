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

                    case "datastartrow":
                        tableOpsStep = 1;
                        WorkTableSelect_SelectedIndexChanged(null, null);
                        break;
                    case "addworkcol":
                        tableOpsStep = 2;
                        WorkTableSelect_SelectedIndexChanged(null, null);
                        break;
                    case "dataheadrow":
                        tableOpsStep = 3;
                        WorkTableSelect_SelectedIndexChanged(null, null);
                        break;
                    case "datacolrename":
                        tableOpsStep = 4;
                        WorkTableSelect_SelectedIndexChanged(null, null);
                        break;
                    case "datacolkey":
                        tableOpsStep = 5;
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
                GridView WorkTablePv = new GridView();
                WorkTablePv.Style.Add("font-size", "10px");
                WorkTablePv.Style.Add("text-align", "center");
                WorkTablePv.Style.Add("white-space", "nowrap");
                WorkTablePv.CssClass = "table-condensed";
                WorkTablePv.RowDataBound += WorkTablePv_RowDataBound;

                System.Web.UI.HtmlControls.HtmlGenericControl p = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
                switch (tableOpsStep)
                {
                    case 1:
                        p.InnerHtml += "(操作2/5)点击标头依次添加参与查询修订的所有列（<a href=\"javascript:__doPostBack('addworkcol','-1')\" > 完成点这里下一步</a>）";
                        break;
                    case 2:
                        p.InnerHtml += "(操作3/5)点击序号选取需要套取的标题行（<a href=\"javascript:__doPostBack('dataheadrow','-1')\" > 没有点这里下一步</a>）";
                        break;
                    case 3:
                        p.InnerHtml += "(操作4/5)为您选取的各个列更新名称（<a href=\"javascript:__doPostBack('datacolrename','')\" > 完成点这里下一步</a>）";
                        break;
                    case 4:
                        p.InnerHtml += "(操作5/5)选取数个用以访问者检索到唯一记录的列（<a href=\"javascript:__doPostBack('datacolkey','')\" > 点这里完成</a>）";
                        break;
                    case 5:
                        p.InnerHtml += "您已完成向导操作生成设定值，点击完成返回设置。";
                        break;
                    default:
                        p.InnerHtml += "(操作1/5)点击序号选取数据起始行";
                        break;
                }
                p.InnerHtml += "<br/>以下选取 " + FileList.Text + " 文件中 " + WorkTableSelect.Text + " 工作表的前10行。";

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

                switch (Request.Form["__EVENTTARGET"])
                {
                    case "datastartrow":
                        break;
                    case "addworkcol":
                        hc.Text = "行序号";
                        hc.BackColor = System.Drawing.Color.LightBlue;
                        e.Row.Cells.Add(hc);
                        break;
                    case "dataheadrow":
                        break;
                    case "datacolrename":
                        break;
                    case "datacolkey":
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

                switch (Request.Form["__EVENTTARGET"]){
                    case "datastartrow":
                        break;
                    case "addworkcol":
                        tc.Text = "<a href=\"javascript:__doPostBack('dataheadrow','" + (e.Row.RowIndex + 1).ToString() + "')\">" + (e.Row.RowIndex + 1).ToString() + "</a>";
                        tc.BackColor = System.Drawing.Color.AliceBlue;
                        e.Row.Cells.Add(tc);
                        break;
                    case "dataheadrow":
                        break;
                    case "datacolrename":
                        break;
                    case "datacolkey":
                        break;
                    default:
                        tc.Text = "<a href=\"javascript:__doPostBack('datastartrow','" + (e.Row.RowIndex + 1).ToString() + "')\">" + (e.Row.RowIndex + 1).ToString() + "</a>";
                        tc.BackColor = System.Drawing.Color.AliceBlue;
                        e.Row.Cells.Add(tc);
                        break;

                }
            }
        }
    }
}