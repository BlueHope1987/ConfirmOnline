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
        bool dspOver, dspColor, dspNub;//显示最终状态 着色 显示修订次数
        ExcelVisiter visiter;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void LkBtnStart_Click(object sender, EventArgs e)
        {
            if (BtnDspOver.CssClass.Contains("active"))
            {
                dspOver = true;
            }
            else
            {
                dspOver = false;
            }

            if (BtnColor.CssClass.Contains("active"))
            {
                dspColor = true;
            }
            else
            {
                dspColor = false;
            }

            if (BtnDspNub.CssClass.Contains("active"))
            {
                dspNub = true;
            }
            else
            {
                dspNub = false;
            }

            WorkTableNote.Visible = false;

            visiter = new ExcelVisiter(Server.MapPath("../App_Data/") + ((SiteSetting)Application["SystemSet"]).DataSource, ((SiteSetting)Application["SystemSet"]).DataTable);
            WorkTableView.DataSource = visiter.getDataSet();
            WorkTableView.DataBind();

            // Create a BoundField object to display an author's last name.
            //BoundField lastNameBoundField = new BoundField();
            //lastNameBoundField.DataField = "F1";
            //lastNameBoundField.HeaderText = "Last Name";

            //// Create a CheckBoxField object to indicate whether the author
            //// is on contract.
            //CheckBoxField contractCheckBoxField = new CheckBoxField();
            //contractCheckBoxField.DataField = "F2";
            //contractCheckBoxField.HeaderText = "Contract";


            WorkTableDiv.Visible = true;



        }

        //protected void WorkTableView_RowCreated(object sender, GridViewRowEventArgs e)
        //{
        //}

        protected void WorkTableView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            TableCell tc = new TableCell();
            SiteContext context = new SiteContext();

            if(dspNub|| dspOver || dspColor)
            {
                List<string> qm = ((SiteSetting)Application["SystemSet"]).QueryMeth.Split(',').ToList();
                string qurtxt = "";
                foreach (string q in qm)
                {
                    if (qurtxt != "") qurtxt += ",";
                    //txt += ((DataTable)WorkTableView.DataSource).Rows[e.Row.RowIndex][int.Parse(q) - 1];
                    qurtxt += e.Row.Cells[int.Parse(q) - 1].Text;
                }

                IQueryable<EditFlow> query = context.EditFlow.Where(s => s.FixRow == qurtxt).OrderBy(x => x.FixerDate);
                List<EditFlow> editHistory = query.ToList();

                if (e.Row.RowIndex == -1)
                {
                    if (dspNub)
                    {
                        TableHeaderCell hc = new TableHeaderCell();
                        hc.Text = "修订次数";
                        e.Row.Cells.Add(hc);
                    }
                }
                else
                {
                    if (dspNub)
                    {
                        int cot = query.Count();
                        tc.Text = cot.ToString();
                        if (cot > 0) {
                            tc.BackColor= System.Drawing.Color.YellowGreen;
                        }
                        e.Row.Cells.Add(tc);
                    }

                    if (dspOver||dspColor)
                    {
                        foreach (EditFlow flow in editHistory)
                        {
                            List<string> fc = flow.FixCol.Split(',').ToList<string>();
                            List<string> fn = flow.FixNew.Split(',').ToList<string>();
                            List<string> fo = flow.FixOld.Split(',').ToList<string>();
                            Dictionary<string, string> dic = new Dictionary<string, string>();
                            for (int i = 0; i < fc.Count; i++)
                            {
                                if (dspOver)
                                    //
                                    //if (((DataSet)WorkTableView.DataSource).Tables[0].Rows[e.Row.RowIndex][int.Parse(fc[i]) - 1].ToString() == fo[i].Replace("&comma&", ","))
                                        
                                    if (e.Row.Cells[int.Parse(fc[i]) - 1].Text.Replace("&nbsp;","") == fo[i].Replace("&comma&", ",")) //修BUG:需要空格检测？？？？
                                    {
                                        e.Row.Cells[int.Parse(fc[i]) - 1].Text = fn[i].Replace("&comma&", ",");//转义逗号
                                        e.Row.Cells[int.Parse(fc[i]) - 1].Font.Bold = true;
                                    }
                                    else
                                    {
                                        throw new ArgumentNullException();
                                    }

                                if (dspColor) e.Row.Cells[int.Parse(fc[i]) - 1].BackColor = System.Drawing.Color.Orange;
                            }
                        }
                    }
                }
            }
        }




        protected void BtnDspOver_Click(object sender, EventArgs e)
        {
            if (BtnDspOver.CssClass.Contains("active"))
            {
                BtnDspOver.CssClass = "btn btn-primary";
            }
            else {
                BtnDspOver.CssClass= "btn btn-primary active";
            }
            LkBtnStart_Click(null, null);
        }

        protected void BtnColor_Click(object sender, EventArgs e)
        {
            if (BtnColor.CssClass.Contains("active"))
            {
                BtnColor.CssClass = "btn btn-primary";
            }
            else
            {
                BtnColor.CssClass = "btn btn-primary active";
            }
            LkBtnStart_Click(null, null);
        }

        protected void BtnDspNub_Click(object sender, EventArgs e)
        {
            if (BtnDspNub.CssClass.Contains("active"))
            {
                BtnDspNub.CssClass = "btn btn-primary";
            }
            else
            {
                BtnDspNub.CssClass = "btn btn-primary active";
            }
            LkBtnStart_Click(null, null);
        }
    }
}