using ConfirmOnline.Logic;
using ConfirmOnline.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ConfirmOnline.Operation
{
    public partial class RecodeLookup : System.Web.UI.Page
    {
        public SiteMaster mstPg;
        public List<string> souCol, qurMth, qurKey, qurName, qurVal, errList;//列号:列名,查询列号, 无序列号, 对应列名, 列值(放回会话时恢复排序), 错误清单
        public DataTable qurResult;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["Struct"] == "LookupOK")
            {
                Response.Redirect("RecodeCorrect");
            }
            if ((string)Session["Struct"] == "FinishFix")
            {
                Response.Redirect("RecodeCorrectFinished");
            }

            //页面Page_Load优先于Master的，不能取Master，可取全局的
            souCol = new List<string>(((SiteSetting)Application["SystemSet"]).SouColReDef.Split(','));
            qurMth = new List<string>(((SiteSetting)Application["SystemSet"]).QueryMeth.Split(',')); //TODO:空值处理错误处理
            qurName = new List<string>();
            qurKey = new List<string>();

            foreach (string s in souCol)
            {
                foreach (string q in qurMth)
                {
                    if (s.IndexOf(q) == 0)
                    {
                        qurKey.Add(s.Split(':')[0]);
                        qurName.Add(s.Split(':')[1].Replace("&comma&", ","));//转义逗号
                        CreateTextBoxList(s.Split(':')[0], s.Split(':')[1]);
                    }
                }
            }
        }

        private void CreateTextBoxList(string id, string describe)
        {
            HtmlGenericControl div = new HtmlGenericControl("div");
            HtmlGenericControl span = new HtmlGenericControl("span");
            TextBox txt;

            //创建div   
            div = new HtmlGenericControl();
            div.TagName = "div";
            div.ID = "divTextBox" + id;
            div.Attributes["class"] = "input-group input-group-md col-md-offset-2";
            div.Attributes["style"] = "margin-top: 10px;";

            //创建span   
            span = new HtmlGenericControl();
            span.ID = "spanTextBox" + id;
            span.InnerHtml = describe + ":";
            span.Attributes["class"] = "input-group-addon control-label";


            //创建TextBox   
            txt = new TextBox();
            txt.ID = "txt" + id;
            txt.CssClass = "form-control";

            //添加控件到容器   
            div.Controls.Add(span);
            div.Controls.Add(txt);
            divContainer.Controls.Add(div);

        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            qurVal = new List<string>();
            TextBox txb;

            foreach (string s in souCol)
            {
                foreach (string q in qurKey)
                {
                    if (s.IndexOf(q) == 0)
                    {
                        txb = divContainer.FindControl("txt" + s.Split(':')[0]) as TextBox;
                        qurVal.Add(txb.Text);
                    }
                }
            }
            if (submitChck())
            {
                List<string> qv = new List<string>();
                foreach (string s in qurMth)
                {
                    qv.Add(qurVal[qurKey.IndexOf(s)].Replace(",", "&comma&"));//逗号转义
                }
                //Server.Transfer("RecodeCorrect.aspx", false);
                Session["Struct"] = "LookupOK";
                Session["souCol"] = souCol;
                Session["qurMth"] = qurMth;
                Session["qurKey"] = qurKey;
                Session["qurName"] = qurName;
                Session["qurVal"] = qv;//将qurVal转成qurMth顺序
                Session["qurResult"] = qurResult;
                Response.Redirect("RecodeCorrect.aspx");
            }
            return;
        }

        protected bool submitChck()
        {
            errList = new List<string>();
            for (int i = 0; i < qurVal.Count; i++)
            {
                if (qurVal[i] == "")
                {
                    errList.Add(qurName[i] + "的值不能为空，请检查后重试。");
                    //HtmlGenericControl p = new HtmlGenericControl("p");
                    //p.InnerText = qurName[i] + "的值不能为空，请检查后重试。";
                    //p.Style.Add(HtmlTextWriterStyle.Color, "red");
                    //p.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
                    //p.Style.Add(HtmlTextWriterStyle.FontSize, "10px");
                    //p.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                    //p.Style.Add(HtmlTextWriterStyle.Margin, "5px 0 5px");
                    //divContainer.Controls.Add(p);
                    HtmlGenericControl div = new HtmlGenericControl("div");
                    div.Attributes["class"] = "alert alert-danger";
                    div.InnerText = qurName[i] + "的值不能为空，请检查后重试。";
                    div.Style.Add(HtmlTextWriterStyle.Margin, "5px 0 5px");
                    div.Style.Add(HtmlTextWriterStyle.Padding, "5px");
                    divContainer.Controls.Add(div);
                }
            }

            if (errList.Count > 0) return false;

            ExcelVisiter visiter = new ExcelVisiter(Server.MapPath("../App_Data/UploadExcels/") + ((SiteSetting)Application["SystemSet"]).DataSource, ((SiteSetting)Application["SystemSet"]).DataTable);

            qurResult = visiter.getDataSet(qurKey, qurVal);

            if (qurResult.Rows.Count == 0)
            {
                errList.Add("没有查询到条目，请检查后重试。");
                //HtmlGenericControl p = new HtmlGenericControl("p");
                //p.InnerText ="没有查询到条目，请检查后重试。";
                //p.Style.Add(HtmlTextWriterStyle.Color, "red");
                //p.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
                //p.Style.Add(HtmlTextWriterStyle.FontSize, "10px");
                //p.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                //p.Style.Add(HtmlTextWriterStyle.Margin, "5px 0 5px");
                //divContainer.Controls.Add(p);
                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Attributes["class"] = "alert alert-danger";
                div.InnerText = "没有查询到条目，请检查后重试。";
                div.Style.Add(HtmlTextWriterStyle.Margin, "5px 0 5px");
                div.Style.Add(HtmlTextWriterStyle.Padding, "5px");
                divContainer.Controls.Add(div);
                return false;
            }

            if (qurResult.Rows.Count > 1)
            {
                errList.Add("查询到不止一条条目，请与管理员联系。");
                //HtmlGenericControl p = new HtmlGenericControl("p");
                //p.InnerText = "查询到不止一条条目，请与管理员联系。";
                //p.Style.Add(HtmlTextWriterStyle.Color, "red");
                //p.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
                //p.Style.Add(HtmlTextWriterStyle.FontSize, "10px");
                //p.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                //p.Style.Add(HtmlTextWriterStyle.Margin, "5px 0 5px");
                //divContainer.Controls.Add(p);
                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Attributes["class"] = "alert alert-danger";
                div.InnerText = "查询到不止一条条目，请与管理员联系。";
                div.Style.Add(HtmlTextWriterStyle.Margin, "5px 0 5px");
                div.Style.Add(HtmlTextWriterStyle.Padding, "5px");
                divContainer.Controls.Add(div);
                return false;
            }
            return true;
        }
    }
}
