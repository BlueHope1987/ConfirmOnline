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
    public partial class RecodeCorrect : System.Web.UI.Page
    {

        public SiteMaster mstPg;


        protected void Page_Load(object sender, EventArgs e)
        {

            //if (!IsPostBack)
            //    if (Page.PreviousPage != null)// 页面本身也是一个类
            //    {
            //        PrePage = (RecodeLookup)Page.PreviousPage;
            //    }
            //    else
            //    {
            //        Response.Redirect("RecodeLookup");
            //    };

            //string eventArgument = Page.Request.Form["__EVENTARGUMENT"];
            //if (!String.IsNullOrEmpty(eventArgument)
            //  && eventArgument.Equals("btnMyQuery"))
            //{
            //    string a = "";
            //    //执行您的回发处理代码
            //}

            if((string)Session["Struct"] != "LookupOK")
                Response.Redirect("RecodeLookup");

            //修订记录检查
            int editHitryCount = GetEditHistory().Count();
            if (editHitryCount != 0)
            {
                if(editHitryCount - 1 >= ((SiteSetting)Application["SystemSet"]).AllowFixTimes && ((SiteSetting)Application["SystemSet"]).AllowFixTimes >= 0)
                {
                    Response.Redirect("RecodeCorrectFinished.aspx?ref=outfixtimes");//
                }
                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Attributes["class"] = "glyphicon glyphicon-info-sign alert alert-warning";
                div.InnerText = "本条记录已被核实"+ editHitryCount.ToString()+"次。";
                div.Style.Add(HtmlTextWriterStyle.Margin, "5px 0 5px");
                div.Style.Add(HtmlTextWriterStyle.Padding, "5px");
                divContainer.Controls.Add(div);
            }

            foreach (string s in (List<string>)Session["souCol"])
            {
                if(!((List<string>)Session["qurMth"]).Exists(ex => ex== s.Split(':')[0]))
                    CreateTextBoxList(s.Split(':')[0], s.Split(':')[1], Convert.ToString(((DataTable)Session["qurResult"]).Rows[0][(int.Parse(s.Split(':')[0]) -1)]));
            }
        }

        public IQueryable<EditFlow> GetEditHistory()
        {
            var _db = new ConfirmOnline.Models.SiteContext();
            //string qk = ((SiteSetting)Application["SystemSet"]).QueryMeth;
            //string qk = String.Join(",", ((List<string>)Session["qurKey"]).ToArray());
            string qk = String.Join(",", ((List<string>)Session["qurVal"]).ToArray());
            IQueryable<EditFlow> query = _db.EditFlow.Where(s => s.FixRow == qk);
            return query;
        }


        private void CreateTextBoxList(string id, string describe, string text)
        {
            HtmlGenericControl div = new HtmlGenericControl("div");
            HtmlGenericControl span = new HtmlGenericControl("span");
            HtmlGenericControl btnspan = new HtmlGenericControl("span");
            HtmlGenericControl btn = new HtmlGenericControl("span");
            //HtmlButton btn = new HtmlButton();
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
            txt.CssClass = "form-control correctDataForm";
            txt.Text = text;
            txt.ReadOnly=true;

            //创建按钮   
            btn.ID = "btnFix" + id;
            btn.InnerHtml = "修订";
            btn.Attributes["class"] = "btn btn-warning glyphicon glyphicon-pencil";
            //btn.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(this, "btnMyQuery"));
            btn.Attributes.Add("data-toggle", "modal");
            btn.Attributes.Add("data-target", "#correctModal");

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
            string a = "";
        }
    }
}
