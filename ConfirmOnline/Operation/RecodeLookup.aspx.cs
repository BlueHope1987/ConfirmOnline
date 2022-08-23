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
        protected void Page_Load(object sender, EventArgs e)
        {

            //页面Page_Load优先于Master的，不能取Master，可取全局的
            List<string> souCol = new List<string>(((SiteSetting)Application["SystemSet"]).SouColReDef.Split(','));
            List<string> qurMth = new List<string>(((SiteSetting)Application["SystemSet"]).QueryMeth.Split(',')); //TODO:空值处理错误处理

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
            

            ExcelVisiter visiter=new ExcelVisiter(Server.MapPath("App_Data/") + ((SiteSetting)Application["SystemSet"]).DataSource, Server.MapPath("App_Data/") + ((SiteSetting)Application["SystemSet"]).DataTable);
  
            //DataGrid1.DataSource = visiter.getDataSet().Tables[0].DefaultView;
            //DataGrid1.DataBind();

            //Page.Application
        }

        private void CreateTextBoxList(string id,string describe)
        {
            HtmlGenericControl div=new HtmlGenericControl("div");
            HtmlGenericControl span=new HtmlGenericControl("span");
            TextBox txt;
            RegularExpressionValidator rev;

                //创建div   
                div = new HtmlGenericControl();
                div.TagName = "div";
                div.ID = "divTextBox" + id;
                div.Attributes["class"] = "input-group input-group-md col-md-offset-2";

                //创建span   
                span = new HtmlGenericControl();
                span.ID = "spanTextBox" + id;
                span.InnerHtml = describe + ":";
                span.Attributes["class"] = "input-group-addon control-label";
            

                //创建TextBox   
                txt = new TextBox();
                txt.ID = "txt" + id;
                txt.CssClass = "form-control";

                //创建格式验证控件，并且将其关联到对应的TextBox   
                //rev = new RegularExpressionValidator();
                //rev.ID = "rev" + id;
                //rev.ControlToValidate = txt.ID;
                //rev.Display = ValidatorDisplay.Dynamic;
                //rev.ValidationGroup = "ShowListContent";
                //rev.ValidationExpression = @"(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?";
                //rev.ErrorMessage = "Invalid url Address!";

                //添加控件到容器   
                div.Controls.Add(span);
                div.Controls.Add(txt);
                //div.Controls.Add(rev);
                divContainer.Controls.Add(div);

        }


        //protected void btnOK_Click(object sender, EventArgs e)
        //{
        //    TextBox txt;
        //    StringBuilder sbResult = new StringBuilder();
        //    int txtCount = int.Parse(txtTextCount.Text);

        //    //遍历获取动态创建的TextBox们中的Text值   
        //    for (int i = 0; i < txtCount; i++)
        //    {
        //        //注意：这里必须通过上层容器来获取动态创建的TextBox，才能获取取ViewState内容   
        //        txt = divControls.FindControl("txt" + i.ToString()) as TextBox;

        //        if (txt != null && txt.Text.Trim().Length > 0)
        //        {
        //            sbResult.AppendFormat("Url Address{0}: {1}.<br />", i + 1, txt.Text.Trim());
        //        }
        //    }

        //    divMessage.InnerHtml = sbResult.ToString();
        //}


    }
}
