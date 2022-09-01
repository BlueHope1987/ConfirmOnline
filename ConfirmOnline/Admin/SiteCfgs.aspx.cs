using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ConfirmOnline.Models;

namespace ConfirmOnline.Admin
{
    public partial class SiteCfgs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
            OpsAre.Visible= false;
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
    }
}