using ConfirmOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace ConfirmOnline.Admin
{
    public partial class Editflow : System.Web.UI.Page
    {
        List<string> souCol;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["currCfgID"] = ((SiteSetting)Application["SystemSet"]).CfgID;
            souCol = new List<string>(((SiteSetting)Application["SystemSet"]).SouColReDef.Split(','));

        }

        protected void EdFlLstView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex == -1) return;
            if (e.Row.Cells.Count > 4)
            {
                List<string> ot = e.Row.Cells[3].Text.Split(',').ToList();
                List<string> nt = new List<string>();
                foreach (string s in ot)
                {
                    foreach (string q in souCol)
                    {
                        if (q.Split(':')[0] == s)
                            nt.Add(s + "[" + q.Split(':')[1].Replace("&comma&", ",") + "]");
                    }
                }
                e.Row.Cells[3].Text = string.Join(",", nt);
            }
        }

        protected void LkBtnStart_Click(object sender, EventArgs e)
        {
            EditFlowNote.Visible = false;
            EdFlViewDiv.Visible = true;
        }
    }
}