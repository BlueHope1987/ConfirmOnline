using ConfirmOnline.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ConfirmOnline.Operation
{
    public partial class RecodeCorrect : System.Web.UI.Page
    {

        public SiteMaster mstPg;
        List<string> correctKey, originalVal, correctVal, fixedKey, fixedOld, fixedNew;//参与修订键 全部原始值 全部新值 已修订键 已修订原始值 已修订新值

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

            if ((string)Session["Struct"] != "LookupOK")
                Response.Redirect("RecodeLookup");

            //修订记录检查
            List<EditFlow> editHistory = GetEditHistory().ToList();
            List<string> qResult = new List<string>();

            originalVal = new List<string>();
            correctKey = new List<string>();

            if (editHistory.Count != 0)
            {
                if (editHistory.Count - 1 >= ((SiteSetting)Application["SystemSet"]).AllowFixTimes && ((SiteSetting)Application["SystemSet"]).AllowFixTimes >= 0)
                {
                    Session["Struct"] = "OutFixTimes";
                    Response.Redirect("RecodeCorrectFinished");
                }

                List<string> fixcol = new List<string>();

                for (int i = 0; i < ((DataTable)Session["qurResult"]).Columns.Count; i++)
                {
                    qResult.Add(Convert.ToString(((DataTable)Session["qurResult"]).Rows[0][i]));
                }

                foreach (EditFlow flow in editHistory)
                {
                    List<string> fc = flow.FixCol.Split(',').ToList<string>();
                    List<string> fn = flow.FixNew.Split(',').ToList<string>();
                    List<string> fo = flow.FixOld.Split(',').ToList<string>();
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    for (int i = 0; i < fc.Count; i++)
                    {
                        if (fc[i] == "-1") continue;
                        if (qResult[int.Parse(fc[i]) - 1] == fo[i].Replace("&comma&", ","))
                        {
                            qResult[int.Parse(fc[i]) - 1] = fn[i].Replace("&comma&", ",");//转义逗号
                        }
                        else
                        {
                            throw new ArgumentNullException();
                        }
                    }
                }

                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Attributes["class"] = "glyphicon glyphicon-info-sign alert alert-warning";
                div.InnerText = "本条记录已被核实修订" + editHistory.Count.ToString() + "次。";
                div.Style.Add(HtmlTextWriterStyle.Margin, "5px 0 5px");
                div.Style.Add(HtmlTextWriterStyle.Padding, "5px");
                divContainer.Controls.Add(div);


                foreach (string s in (List<string>)Session["souCol"])
                {
                    if (!((List<string>)Session["qurMth"]).Exists(ex => ex == s.Split(':')[0]))
                        CreateTextBoxList(s.Split(':')[0], s.Split(':')[1].Replace("&comma&", ","), qResult[(int.Parse(s.Split(':')[0]) - 1)]); //转义逗号
                }
            }
            else
            {
                foreach (string s in (List<string>)Session["souCol"])
                {
                    if (!((List<string>)Session["qurMth"]).Exists(ex => ex == s.Split(':')[0]))
                        CreateTextBoxList(s.Split(':')[0], s.Split(':')[1].Replace("&comma&", ","), Convert.ToString(((DataTable)Session["qurResult"]).Rows[0][(int.Parse(s.Split(':')[0]) - 1)]));//转义逗号
                }
            }


        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            List<string> ret = new List<string>(HiddenField.Value.Split(';'));

            correctVal = new List<string>();
            TextBox txb;

            foreach (string s in (List<string>)Session["souCol"])
            {
                if (!((List<string>)Session["qurMth"]).Exists(ex => ex == s.Split(':')[0]))
                {
                    txb = divContainer.FindControl("corrtxt" + s.Split(':')[0]) as TextBox;
                    correctVal.Add(txb.Text);
                }
            }

            fixedKey = new List<string>();
            fixedOld = new List<string>();
            fixedNew = new List<string>();

            //问题：无法得到文本框内容：已解决，使用.Attributes["ReadOnly"]
            for (int s = 0; s < correctKey.Count(); s++)
            {
                if (correctVal[s] != originalVal[s])
                {
                    fixedKey.Add(correctKey[s]);
                    fixedOld.Add(originalVal[s].Replace(",", "&comma&"));//逗号转义
                    fixedNew.Add(correctVal[s].Replace(",", "&comma&"));
                }
            }

            try
            {
                var nEF = new EditFlow();
                nEF.FixerID = "";
                nEF.FixerDetal = Request.UserHostName + "," + Request.UserHostAddress + "," + Request.ServerVariables["HTTP_USER_AGENT"];
                nEF.FixerDate = DateTime.Now;
                nEF.FixNew = String.Join(",", fixedNew);
                nEF.FixOld = String.Join(",", fixedOld);
                if(fixedNew.Count > 0)
                {
                    nEF.FixCol = String.Join(",", fixedKey);
                }
                else
                {
                    nEF.FixCol ="-1";
                }
                nEF.FixRow = String.Join(",", ((List<string>)Session["qurVal"]).ToArray());
                nEF.CfgID = ((SiteSetting)Application["SystemSet"]).CfgID;

                using (SiteContext _db = new SiteContext())
                {
                    // Add product to DB.
                    _db.EditFlow.Add(nEF);
                    _db.SaveChanges();
                }

                Session["Struct"] = "FinishFix";
                Response.Redirect("RecodeCorrectFinished");

            }
            catch (Exception)
            {
                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Attributes["class"] = "glyphicon glyphicon-info-sign alert alert-warning";
                div.InnerText = "修订记录保存出错！";
                div.Style.Add(HtmlTextWriterStyle.Margin, "5px 0 5px");
                div.Style.Add(HtmlTextWriterStyle.Padding, "5px");
                divContainer.Controls.Add(div);
            }
        }
        public IQueryable<EditFlow> GetEditHistory()
        {
            var _db = new ConfirmOnline.Models.SiteContext();
            //string qk = ((SiteSetting)Application["SystemSet"]).QueryMeth;
            //string qk = String.Join(",", ((List<string>)Session["qurKey"]).ToArray());
            string qk = String.Join(",", ((List<string>)Session["qurVal"]).ToArray());
            IQueryable<EditFlow> query = _db.EditFlow.Where(s => s.FixRow == qk).OrderBy(x => x.FixerDate);
            return query;
        }


        private void CreateTextBoxList(string id, string describe, string text)
        {
            correctKey.Add(id);
            originalVal.Add(text);

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
            txt.ID = "corrtxt" + id;
            txt.CssClass = "form-control correctDataForm";
            txt.Text = text;
            //txt.ReadOnly=true; //这样设置后后台得不到值
            txt.Attributes["ReadOnly"] = "true";

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

        //protected void prossPrePage(object sender, EventArgs e)
        //{
        //    Dictionary<string, string> textlist = new Dictionary<string, string>();
        //    TextBox txt;

        //    if (!IsPostBack)
        //    {
        //        if (Page.PreviousPage != null)// 页面本身也是一个类
        //        {
        //            RecodeLookup PrePage = (RecodeLookup)Page.PreviousPage;
        //            //textlist = PrePage.queryList;

        //            //if (PrePage != null)
        //            //{
        //            //    foreach (string s in souCol)
        //            //    {
        //            //        foreach (string q in qurMth)
        //            //        {
        //            //            if (s.IndexOf(q) == 0)
        //            //            {
        //            //                txt = PrePage.FindControl("txt" + s.Split(':')[0]) as TextBox;
        //            //                textlist.Add(q, txt.Text);
        //            //            }
        //            //        }
        //            //    }
        //            //    return;
        //            //}
        //        }
        //    }
        //}

    }
}
