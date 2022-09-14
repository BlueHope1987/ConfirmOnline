using ConfirmOnline.Logic;
using ConfirmOnline.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

namespace ConfirmOnline.Admin
{
    public partial class WorkTable : System.Web.UI.Page
    {
        bool dspOver, dspColor, dspNub, hidFixed, hidInital;//显示最终状态 着色 显示修订次数 隐藏修订 隐藏原始
        ExcelVisiter visiter;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void LkBtnStart_Click(object sender, EventArgs e)
        {
            dspOver = BtnDspOver.CssClass.Contains("active") ? true : false;
            dspColor= BtnColor.CssClass.Contains("active") ? true : false;
            dspNub = BtnDspNub.CssClass.Contains("active") ? true : false;
            hidFixed = BtnHidFixed.CssClass.Contains("active") ? true : false;
            hidInital= BtnHidInital.CssClass.Contains("active") ? true : false;

            WorkTableNote.Visible = false;

            visiter = new ExcelVisiter(Server.MapPath("../App_Data/UploadExcels/") + ((SiteSetting)Application["SystemSet"]).DataSource, ((SiteSetting)Application["SystemSet"]).DataTable);
            DataSet ds= visiter.getDataSet();


            SiteContext context = new SiteContext();
            int curCfgID = ((SiteSetting)Application["SystemSet"]).CfgID;
            int dataRowStart = ((SiteSetting)Application["SystemSet"]).SouRowRangeStart;
            int dataRowEnd = ((SiteSetting)Application["SystemSet"]).SouRowRangeEnd;


            List<string> souCol = new List<string>(((SiteSetting)Application["SystemSet"]).SouColReDef.Split(','));
            List<string> qurName = new List<string>();
            List<int> qurKey = new List<int>();

            foreach (string s in souCol)
            {
                 qurKey.Add(int.Parse(s.Split(':')[0]));
                 qurName.Add(s.Split(':')[1].Replace("&comma&", ","));//转义逗号  
            }

            //TODO:

            //去表头 改列名
            for(int i=0;i<qurKey.Count;i++)
            {
                ds.Tables[0].Columns[qurKey[i] - 1].ColumnName = qurName[i];
                ds.Tables[0].Columns[qurKey[i] - 1].Caption= qurName[i];
            }


            if (ds.Tables[0].Rows.Count >= dataRowStart)
            {
                for (int i = 0; i < dataRowStart - 1; i++) //
                {
                    ds.Tables[0].Rows[i].Delete();
                }
                ds.AcceptChanges();
            }

            if (dspNub || dspOver || hidFixed || hidInital)
            {
                //从应用状态获取系统设置：查询字段 配置式ID
                List<string> qm = ((SiteSetting)Application["SystemSet"]).QueryMeth.Split(',').ToList();

                if (dspNub) ds.Tables[0].Columns.Add("修订次数",typeof(string));

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string qurtxt = "";
                    foreach (string q in qm)
                    {
                        if (qurtxt != "") qurtxt += ",";
                        //txt += ((DataTable)WorkTableView.DataSource).Rows[e.Row.RowIndex][int.Parse(q) - 1];
                        qurtxt += row[int.Parse(q) - 1];
                    }

                    //取编辑记录
                    IQueryable<EditFlow> query = context.EditFlow.Where(s => (s.FixRow == qurtxt && s.CfgID == curCfgID)).OrderBy(x => x.FixerDate);
                    List<EditFlow> editHistory = query.ToList();
                    int edtflwCut= query.Count();
                    if (edtflwCut > 0)
                    {
                        if (hidFixed)
                        {
                            row.Delete();
                            continue;
                        }
                        if (dspNub) row["修订次数"] = query.Count();
                    }
                    else
                    {
                        if (hidInital)
                        {
                            row.Delete();
                            continue;
                        }
                        if (dspNub) row["修订次数"] = "-";
                    }

                    if (dspOver)
                    {
                        foreach (EditFlow flow in editHistory)
                        {
                            List<string> fc = flow.FixCol.Split(',').ToList<string>();//修订列
                            List<string> fn = flow.FixNew.Split(',').ToList<string>();//修订新
                            List<string> fo = flow.FixOld.Split(',').ToList<string>();//修订旧
                            Dictionary<string, string> dic = new Dictionary<string, string>();
                            for (int i = 0; i < fc.Count; i++)
                            {
                                if (((string)row[int.Parse(fc[i]) - 1]).Replace("&nbsp;", "") == fo[i].Replace("&comma&", ",")) //修BUG:需要空格检测？？？？
                                {
                                    row[int.Parse(fc[i]) - 1] = fn[i].Replace("&comma&", ",");//转义逗号
                                }
                                else
                                {
                                    throw new ArgumentNullException();
                                }
                            }
                        }
                    }
                }
            }


            WorkTableView.DataSource = ds;
            WorkTableView.DataBind();

            WorkTableDiv.Visible = true;



        }


        protected void WorkTableView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            TableCell tc = new TableCell();
            SiteContext context = new SiteContext();
            int cot=0;

            if (dspNub || dspOver || dspColor)
            {
                //从应用状态获取系统设置：查询字段 配置式ID
                List<string> qm = ((SiteSetting)Application["SystemSet"]).QueryMeth.Split(',').ToList();
                string qurtxt = "";
                if (e.Row.Cells.Count < qm.Count) return;
                foreach (string q in qm)
                {
                    if (qurtxt != "") qurtxt += ",";
                    //txt += ((DataTable)WorkTableView.DataSource).Rows[e.Row.RowIndex][int.Parse(q) - 1];
                    qurtxt += e.Row.Cells[int.Parse(q) - 1].Text;
                }
                int curCfgID = ((SiteSetting)Application["SystemSet"]).CfgID;

                //取编辑记录
                IQueryable<EditFlow> query = context.EditFlow.Where(s => (s.FixRow == qurtxt && s.CfgID == curCfgID)).OrderBy(x => x.FixerDate);
                List<EditFlow> editHistory = query.ToList();


                if (dspNub) cot = query.Count();

                foreach (EditFlow flow in editHistory)
                {
                    List<string> fc = flow.FixCol.Split(',').ToList<string>();//修订列
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    for (int i = 0; i < fc.Count; i++)
                    {
                        if (dspOver) e.Row.Cells[int.Parse(fc[i]) - 1].Font.Bold = true;
                        if (dspColor) e.Row.Cells[int.Parse(fc[i]) - 1].BackColor = System.Drawing.Color.Orange;
                        if (dspNub) if(cot > 0) e.Row.Cells[e.Row.Cells.Count-1].BackColor = System.Drawing.Color.YellowGreen;
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
            else
            {
                BtnDspOver.CssClass = "btn btn-primary active";
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

        protected void BtnHidFixed_Click(object sender, EventArgs e)
        {
            if (BtnHidFixed.CssClass.Contains("active"))
            {
                BtnHidFixed.CssClass = "btn btn-primary";
            }
            else
            {
                BtnHidFixed.CssClass = "btn btn-primary active";
            }
            LkBtnStart_Click(null, null);
        }

        protected void BtnHidInital_Click(object sender, EventArgs e)
        {
            if (BtnHidInital.CssClass.Contains("active"))
            {
                BtnHidInital.CssClass = "btn btn-primary";
            }
            else
            {
                BtnHidInital.CssClass = "btn btn-primary active";
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

        protected void SaveTable_Click(object sender, EventArgs e)
        {

            LkBtnStart_Click(null, null);

            DataTable dt = new DataTable("XLData");//工作簿已有工作表名

            // 列强制转换
            for (int count = 0; count < ((DataSet)WorkTableView.DataSource).Tables[0].Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(((DataSet)WorkTableView.DataSource).Tables[0].Columns[count].Caption);
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < WorkTableView.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < ((DataSet)WorkTableView.DataSource).Tables[0].Columns.Count; countsub++)
                {
                    if (WorkTableView.Rows[count].Cells[countsub].Text == "&nbsp;")
                    {
                        dr[countsub] = "";
                    }
                    else
                    {
                        dr[countsub] = Convert.ToString(WorkTableView.Rows[count].Cells[countsub].Text);
                    }
                }
                dt.Rows.Add(dr);
            }

            //foreach (GridViewRow row in WorkTableView.Rows)
            //{
            //    dt.Rows.Add(row);
            //}
            string fn = DateTime.Now.ToString("s").Replace(':', '-') + ".xls";
            string ffn = Server.MapPath("../App_Data/DownloadReady/Table") + fn;
            ExcelVisiter.TableToExcelFile(dt, ffn, Server.MapPath("../App_Data/XLSModelFile.xls"));

            FileInfo fileInfo = new FileInfo(ffn);
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + fn);
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            Response.AddHeader("Content-Transfer-Encoding", "binary");
            Response.ContentType = "application/octet-stream";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            Response.WriteFile(ffn);
            Response.Flush();
            Response.End();


            //Response.TransmitFile(fn);
            return;
        }
    }
}