using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            EditAreTitle.InnerText = "新建配置式";
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            OpsAre.Visible = false;
            EditAre.Visible = true;
            EditAreTitle.InnerText = "编辑配置式";

        }

        protected void CancelEdit_Click(object sender, EventArgs e)
        {
            OpsAre.Visible = true;
            EditAre.Visible = false;
            EditAre.EnableViewState = false;
        }

    }
}