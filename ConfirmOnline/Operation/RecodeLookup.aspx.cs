using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ConfirmOnline.Logic;
using ConfirmOnline.Models;

namespace ConfirmOnline.Operation
{
    public partial class RecodeLookup : System.Web.UI.Page
    {
        public SiteMaster mstPg;
        private List<string> souCol, qurMth, errList;//列号:列名, 排序后查询列号, 错误清单
        public Dictionary<string, string> qurkey, queryList;//查询项列号-名称 提交列号-赋值

        protected void Page_Load(object sender, EventArgs e)
        {

            //页面Page_Load优先于Master的，不能取Master，可取全局的
            souCol = new List<string>(((SiteSetting)Application["SystemSet"]).SouColReDef.Split(','));
            qurMth = new List<string>(((SiteSetting)Application["SystemSet"]).QueryMeth.Split(',')); //TODO:空值处理错误处理
            qurkey = new Dictionary<string, string>();

            foreach (string s in souCol)
            {
                foreach (string q in qurMth)
                {
                    if (s.IndexOf(q) == 0)
                    {
                        qurkey.Add(s.Split(':')[0], s.Split(':')[1]);
                        CreateTextBoxList(s.Split(':')[0], s.Split(':')[1]);
                    }
                }
            }
        }

        private void CreateTextBoxList(string id,string describe)
        {
            HtmlGenericControl div=new HtmlGenericControl("div");
            HtmlGenericControl span=new HtmlGenericControl("span");
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
            queryList = new Dictionary<string, string>();
            TextBox txb;

            foreach (string s in souCol)
            {
                foreach (string q in qurMth)
                {
                    if (s.IndexOf(q) == 0)
                    {
                        txb = divContainer.FindControl("txt" + s.Split(':')[0]) as TextBox;
                        queryList.Add(q, txb.Text);
                    }
                }
            }
            if (submitChck(queryList))
                Server.Transfer("dataviewtest.aspx",false);
            return;
        }

        protected bool submitChck(Dictionary<string, string> ql)
        {
            errList = new List<string>();
            foreach(var item in ql){
                if (item.Value == "")
                {
                    errList.Add(qurkey[item.Key] + "的值不能为空，请检查后重试。");
                    HtmlGenericControl p = new HtmlGenericControl("p");
                    p.InnerText = qurkey[item.Key] + "的值不能为空，请检查后重试。";
                    p.Style.Add(HtmlTextWriterStyle.Color, "red");
                    p.Style.Add(HtmlTextWriterStyle.TextAlign,"center");
                    p.Style.Add(HtmlTextWriterStyle.FontSize, "10px");
                    p.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                    p.Style.Add(HtmlTextWriterStyle.Margin, "5px 0 5px");
                    divContainer.Controls.Add(p);
                }
            }
            if (errList.Count > 0) return false;

            ExcelVisiter visiter = new ExcelVisiter(Server.MapPath("../App_Data/") + ((SiteSetting)Application["SystemSet"]).DataSource, ((SiteSetting)Application["SystemSet"]).DataTable);

            //List<string> ColName=visiter.columnName;

            return true;
        }
    }
}
