using ConfirmOnline.Models;
using ConfirmOnline.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ConfirmOnline.Admin
{
    public partial class WorkTable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void LkBtnStart_Click(object sender, EventArgs e)
        {
            LkBtnStart.Visible = false;


            ExcelVisiter visiter = new ExcelVisiter(Server.MapPath("../App_Data/") + ((SiteSetting)Application["SystemSet"]).DataSource, ((SiteSetting)Application["SystemSet"]).DataTable);
            WorkTableView.DataSource = visiter.getDataSet().Tables[0];
            // Create a BoundField object to display an author's last name.
            BoundField lastNameBoundField = new BoundField();
            lastNameBoundField.DataField = "F1";
            lastNameBoundField.HeaderText = "Last Name";

            //// Create a CheckBoxField object to indicate whether the author
            //// is on contract.
            //CheckBoxField contractCheckBoxField = new CheckBoxField();
            //contractCheckBoxField.DataField = "F2";
            //contractCheckBoxField.HeaderText = "Contract";

            WorkTableView.DataBind();

            WorkTableDiv.Visible = true;



        }

        protected void WorkTableView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            TableCell tc = new TableCell();

            SiteContext context=new SiteContext();

            if (e.Row.RowIndex == -1)
            {
                tc.Text = "修订次数";
                e.Row.Cells.Add(tc);
            }
            else
            {
                List<string> qm = ((SiteSetting)Application["SystemSet"]).QueryMeth.Split(',').ToList();
                string txt="";
                foreach (string q in qm)
                {
                    if (txt != "") txt += ",";
                    txt += ((DataTable)WorkTableView.DataSource).Rows[e.Row.RowIndex][int.Parse(q) - 1];//e.Row.Cells[int.Parse(q) - 1].Text
                }

                IQueryable<EditFlow> query = context.EditFlow.Where(s => s.FixRow == txt);
                tc.Text = query.Count().ToString();
                e.Row.Cells.Add(tc);
            }

        }
    }
}