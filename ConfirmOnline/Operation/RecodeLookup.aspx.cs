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
        private List<string> souCol , qurMth;
        public Dictionary<string, string> queryList;

        protected void Page_Load(object sender, EventArgs e)
        {

            //页面Page_Load优先于Master的，不能取Master，可取全局的
            souCol = new List<string>(((SiteSetting)Application["SystemSet"]).SouColReDef.Split(','));
            qurMth = new List<string>(((SiteSetting)Application["SystemSet"]).QueryMeth.Split(',')); //TODO:空值处理错误处理

            foreach (string s in souCol)
            {
                foreach (string q in qurMth)
                {
                    if (s.IndexOf(q) == 0)
                    {
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
            TextBox txt;

            foreach (string s in souCol)
            {
                foreach (string q in qurMth)
                {
                    if (s.IndexOf(q) == 0)
                    {
                        txt = divContainer.FindControl("txt" + s.Split(':')[0]) as TextBox;
                        queryList.Add(q, txt.Text);
                    }
                }
            }
            Server.Transfer("dataviewtest.aspx",false);
            return;
        }
    }
}
