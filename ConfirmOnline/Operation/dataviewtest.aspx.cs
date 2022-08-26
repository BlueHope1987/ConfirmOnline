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
    public partial class dataviewtest : System.Web.UI.Page
    {

        public SiteMaster mstPg;
        RecodeLookup PrePage;


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
                if (Page.PreviousPage != null)// 页面本身也是一个类
                {
                    PrePage = (RecodeLookup)Page.PreviousPage;
                }
                else
                {
                    Response.Redirect("RecodeLookup");
                };

            foreach (string s in PrePage.souCol)
            {
                if(!PrePage.qurMth.Exists(ex => ex== s.Split(':')[0]))
                    CreateTextBoxList(s.Split(':')[0], s.Split(':')[1], Convert.ToString(PrePage.qurResult.Rows[0][(int.Parse(s.Split(':')[0]) -1)]));
            }
        }

        private void CreateTextBoxList(string id, string describe, string text)
        {
            HtmlGenericControl div = new HtmlGenericControl("div");
            HtmlGenericControl span = new HtmlGenericControl("span");
            HtmlGenericControl btnspan = new HtmlGenericControl("span");
            HtmlButton btn = new HtmlButton();
            TextBox txt;

            //创建div   
            div.TagName = "div";
            div.ID = "divTextBox" + id;
            div.Attributes["class"] = "input-group input-group-md col-md-offset-2";
            div.Attributes["style"] = "margin-top: 10px;";

            //创建span   
            span.ID = "spanTextBox" + id;
            span.InnerHtml = describe + ":";
            span.Attributes["class"] = "input-group-addon control-label";


            //创建TextBox   
            txt = new TextBox();
            txt.ID = "txt" + id;
            txt.CssClass = "form-control";
            txt.Text = text;
            txt.ReadOnly=true;

            //创建按钮   
            btn.ID = "btnFix" + id;
            btn.InnerHtml = "修订";
            btn.Attributes["class"] = "btn btn-warning glyphicon glyphicon-pencil";

            //创建修订span   
            btnspan = new HtmlGenericControl();
            btnspan.ID = "btnSpan" + id;
            btnspan.Attributes["class"] = "input-group-btn";
            btnspan.Style.Add("float", "left");
            btnspan.Controls.Add(btn);

            //添加控件到容器   
            div.Controls.Add(span);
            div.Controls.Add(txt);
            div.Controls.Add(btnspan);
            divContainer.Controls.Add(div);

        }

        protected void prossPrePage(object sender, EventArgs e)
        {
            Dictionary<string, string> textlist = new Dictionary<string, string>();
            TextBox txt;

            if (!IsPostBack)
            {
                if (Page.PreviousPage != null)// 页面本身也是一个类
                {
                    RecodeLookup PrePage = (RecodeLookup)Page.PreviousPage;
                    //textlist = PrePage.queryList;

                    //if (PrePage != null)
                    //{
                    //    foreach (string s in souCol)
                    //    {
                    //        foreach (string q in qurMth)
                    //        {
                    //            if (s.IndexOf(q) == 0)
                    //            {
                    //                txt = PrePage.FindControl("txt" + s.Split(':')[0]) as TextBox;
                    //                textlist.Add(q, txt.Text);
                    //            }
                    //        }
                    //    }
                    //    return;
                    //}
                }
            }

        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {

        }
    }
}
