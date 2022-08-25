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

            //public SiteMaster mstPg;
            //public List<string> souCol, qurMth, qurKey, qurName, qurVal, errList;//列号:列名,查询列号, 无序列号, 对应列名, 列值, 错误清单
            

            //prossPrePage(sender,e);
            //Request.Form;
            ExcelVisiter visiter=new ExcelVisiter(Server.MapPath("../App_Data/") + ((SiteSetting)Application["SystemSet"]).DataSource,((SiteSetting)Application["SystemSet"]).DataTable);

            //visiter.getDataSet().Tables[0].DefaultView




            //DataGrid1.DataSource = visiter.getDataSet().Tables[0].DefaultView;
            //DataGrid1.DataBind();
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


    }
}
